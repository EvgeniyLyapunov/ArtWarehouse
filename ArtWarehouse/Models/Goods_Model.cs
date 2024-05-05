using System;

namespace ArtWarehouse.Models
{
    public class Goods_Model
    {
        public int goods_id { get; set; }
        public string goods_name { get; set; }
        public string goods_descr { get; set; }
        public int goods_category_id { get; set; }
        public int maker_id { get; set; }
        public decimal goods_price { get; set; }
        public int remaining_goods {  get; set; }
        public DateTime update_date { get; set; }
    }
}
