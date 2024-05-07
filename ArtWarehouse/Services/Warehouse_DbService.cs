using ArtWarehouse.Models;
using ArtWarehouse.Models.ModelsView;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace ArtWarehouse.Services
{
    public class Warehouse_DbService
    {
        SqlConnection _db;
        public IConfiguration Configuration { get; }

        public Warehouse_DbService(IConfiguration configuration)
        {
            Configuration = configuration;
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public GoodsCompleteInfo_MV GoodsCompleteInfo_Get()
        {
            GoodsCompleteInfo_MV goodsCompleteInfo_MV = new GoodsCompleteInfo_MV();

            _db.Open();

            string query = @"SELECT * FROM goods";
            goodsCompleteInfo_MV.goodsList = _db.Query<Goods_Model>(query, null).OrderBy(u => u.goods_name);

            query = @"SELECT * FROM goods_categories";
            goodsCompleteInfo_MV.categoriesList = _db.Query<GoodsCategory_Model>(query, null).OrderBy(u => u.category_name);

            query = @"SELECT * FROM goods_makers";
            goodsCompleteInfo_MV.makersList = _db.Query<Maker_Model>(query, null).OrderBy(u => u.maker_name);

            _db.Close();

            return goodsCompleteInfo_MV;
        }

        public GoodsInfoForCard_MV GoodsInfoForCard_Get(int id)
        {
            GoodsInfoForCard_MV goodsInfoForCard;

            _db.Open();

            string query = @"
SELECT 
    g.goods_id,
    g.goods_name,
    g.goods_descr,
    gc.category_name,
    gm.maker_name,
    g.goods_price,
    g.remaining_goods,
    g.update_date
FROM goods g
    join goods_categories gc
    on g.goods_category_id = gc.category_id
    join goods_makers gm
    on g.maker_id = gm.maker_Id
WHERE g.goods_id = @argid
";

            goodsInfoForCard = _db.Query<GoodsInfoForCard_MV>(query, new { argid = id }).First();

            _db.Close();

            return goodsInfoForCard;
        }
    }
}
