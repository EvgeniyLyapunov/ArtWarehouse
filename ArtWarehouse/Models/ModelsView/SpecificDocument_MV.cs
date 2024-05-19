using System;
using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class SpecificDocument_MV
    {
        public int Id_Order { get; set; }
        public string type_order { get; set; }
        public DateTime date_order { get; set; }
        public List<SpecificDocumentGoods_MV> GoodsList { get; set; }
    }
}
