using ArtWarehouse.Models.ModelsView;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace ArtWarehouse.Services
{
    public class Sale_DbService
    {
        SqlConnection _db;
        public IConfiguration Configuration { get; }

        public Sale_DbService(IConfiguration configuration)
        {
            Configuration = configuration;
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public void SaleTransaction(Sale_MV sale)
        {
            _db.Open();

            var transaction = _db.BeginTransaction();

            try
            {
                string query = @"INSERT INTO Orders (type_order) VALUES ('sale of goods')";
                _db.Execute(query, null, transaction);

                query = @"SELECT SCOPE_IDENTITY()";
                int order_id = _db.ExecuteScalar<int>(query, null, transaction);

                for (int i = 0; i < sale.GoodsIds.Count; i++)
                {
                    query = $@"
INSERT INTO 
    Orders_Goods 
    (Id_Order, Id_Goods, Quantity)
VALUES (@argorder_id, @arggoods_id, @argremaining_goods)
";
                    var obj = new
                    {
                        argorder_id = order_id,
                        arggoods_id = sale.GoodsIds[i],
                        argremaining_goods = sale.GoodsCount[i]
                    };

                    _db.Execute(query, obj, transaction);

                    query = @"SELECT remaining_goods FROM goods WHERE goods_id = @arggoods_id";
                    var obj1 = new
                    {
                        arggoods_id = sale.GoodsIds[i]
                    };
                    var oldRemaining_goods = _db.Query<int>(query, obj1, transaction).First();

                    query = @"
UPDATE
    goods
SET 
    remaining_goods = @argremaining_goods
WHERE
    goods_id = @arggoods_id";
                    var obj2 = new
                    {
                        argremaining_goods = oldRemaining_goods - sale.GoodsCount[i],
                        arggoods_id = sale.GoodsIds[i]
                    };

                    _db.Execute(query, obj2, transaction);
                }
                transaction.Commit();
                _db.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _db.Close();

                throw new Exception(ex.Message, ex);
            }
        }
    }
}
