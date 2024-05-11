using ArtWarehouse.Models.Enums;
using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class GoodsCompleteForPrint_MV
    {
        public PrintViewType PrintViewType { get; set; }
        public List<Goods_Model> GoodsList { get; set; }
        public List<GoodsCategory_Model> CategoriesList { get; set; }
        public List<Maker_Model> MakersList { get; set; }

    }
}
