using System.Collections;
using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class GoodsCompleteInfo_MV
    {
        public IEnumerable<Goods_Model> goodsList { get; set; }
        public IEnumerable<GoodsCategory_Model> categoriesList { get; set; }
        public IEnumerable<Maker_Model> makersList { get; set; }
    }
}
