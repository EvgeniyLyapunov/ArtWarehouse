using System;
using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class GoodsInfoForCard_MV
    {
        public int goods_id { get; set; }
        public string goods_name { get; set; }
        public string goods_descr { get; set; }
        public string category_name { get; set; }
        public string maker_name { get; set; }
        public decimal goods_price { get; set; }
        public int remaining_goods { get; set; }
        public DateTime update_date { get; set; }
    }
}
