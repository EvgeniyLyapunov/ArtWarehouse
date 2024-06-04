using ArtWarehouse.Models;
using ArtWarehouse.Models.ModelsView;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace ArtWarehouse.Services
{
    public class Documents_DbService
    {
        SqlConnection _db;
        public IConfiguration Configuration { get; }

        public Documents_DbService(IConfiguration configuration)
        {
            Configuration = configuration;
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public DocumentsList_MV DocumentsList_Get()
        {
            DocumentsList_MV documentsList_MV = new DocumentsList_MV();

            _db.Open();

            string query = @"SELECT * FROM Orders";

            documentsList_MV.Documents = _db.Query<Document>(query, null).ToList();

            return documentsList_MV;
        }

        public DocumentsList_MV DocumentsList_GetByDate(DateTime date)
        {
            DocumentsList_MV documentsList_MV = new DocumentsList_MV();

            _db.Open();

            string query = @"SELECT * FROM Orders o WHERE o.date_order = @argDate";

            var obj = new
            {
                argDate = date,
            };

            documentsList_MV.Documents = _db.Query<Document>(query, obj, null).ToList();

            return documentsList_MV;
        }


        public SpecificDocument_MV SpecificDocument_Get(int id)
        {
            _db.Open();

            var transaction = _db.BeginTransaction();
            
            try
            {
                string firstQuery = @"SELECT * FROM Orders o WHERE o.Id_Order = @argId";

                var obj = new
                {
                    argId = id,
                };

                var doc = _db.Query<Document>(firstQuery, obj, transaction).First();

                string query = @"
SELECT
    g.goods_id,
    g.goods_name,
    g.goods_descr,
    gc.category_name,
    gm.maker_name,
    g.goods_price,
    og.quantity
FROM
    Orders_Goods og
    join goods g
    on og.Id_Goods = g.goods_id
    join goods_categories gc
    on g.goods_category_id = gc.category_id
    join goods_makers gm
    on g.maker_id = gm.maker_Id
WHERE og.Id_Order = @argId";

                var goodsList = _db.Query<SpecificDocumentGoods_MV>(query, obj, transaction).ToList();

                transaction.Commit();

                SpecificDocument_MV specificDocument_MV = new SpecificDocument_MV
                {
                    Id_Order = id,
                    type_order = doc.Type_Order,
                    date_order = doc.Date_Order,
                    GoodsList = goodsList
                };

                return specificDocument_MV;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _db.Close();

                throw new Exception(ex.Message, ex);
            }
        }

        public Movement_MV MovementGoods_Get()
        {
            _db.Open();
            try
            {
                string query = @"
SELECT 
g.goods_name as Name,
og.Quantity,
o.type_order as Type,
o.date_order as Date_Order
FROM
    goods g
JOIN
    Orders_Goods og
ON g.goods_id = og.Id_Goods
JOIN 
    Orders o
ON o.Id_Order = og.Id_Order
ORDER BY g.goods_name, o.type_order
";
                List<MovementGoods_MV> movementGoods_MV = _db.Query<MovementGoods_MV>(query, null).ToList();

                query = @"
SELECT DISTINCT
g.goods_name as Name
FROM
    goods g
JOIN
    Orders_Goods og
ON g.goods_id = og.Id_Goods
JOIN 
    Orders o
ON o.Id_Order = og.Id_Order
ORDER BY g.goods_name
";
                List<string> NamesOfGoods = _db.Query<string>(query, null).ToList();

                Movement_MV model = new Movement_MV
                {
                    movementGoods = movementGoods_MV,
                    NamesOfGoods = NamesOfGoods
                };

                return model;
            }
            catch(Exception ex)
            {
                _db.Close();

                throw new Exception(ex.Message, ex);
            }
        }
    }
}
