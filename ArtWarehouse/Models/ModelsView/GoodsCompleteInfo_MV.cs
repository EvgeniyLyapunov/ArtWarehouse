using System.Collections;
using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class GoodsCompleteInfo_MV
    {
        public List<Goods_Model> goodsList { get; set; }
        public List<GoodsCategory_Model> categoriesList { get; set; }
        public List<Maker_Model> makersList { get; set; }
    }
}
