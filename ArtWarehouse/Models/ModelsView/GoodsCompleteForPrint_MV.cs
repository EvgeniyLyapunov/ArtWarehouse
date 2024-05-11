using ArtWarehouse.Models.Enums;
using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class GoodsCompleteForPrint_MV
    {
        public PrintViewType PrintViewType { get; set; }
        public IEnumerable<Goods_Model> GoodsList { get; set; }
        public IEnumerable<GoodsCategory_Model> CategoriesList { get; set; }
        public IEnumerable<Maker_Model> MakersList { get; set; }

        public GoodsCompleteForPrint_MV(PrintViewType printViewType, GoodsCompleteInfo_MV completeInfo_MV)
        {
            PrintViewType = printViewType;
            GoodsList = completeInfo_MV.goodsList;
            CategoriesList = completeInfo_MV.categoriesList;
            MakersList = completeInfo_MV.makersList;
        }
    }
}
