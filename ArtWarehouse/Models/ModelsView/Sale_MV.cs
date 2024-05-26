using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class Sale_MV
    {
        public List<int> GoodsIds { get; set; }
        public List<int> GoodsCount { get; set; }

        public Sale_MV()
        {
            GoodsIds = new List<int>();
            GoodsCount = new List<int>();
        }
    }
}
