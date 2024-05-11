using ArtWarehouse.Models.Enums;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class ListForPrint_MV
    {
        [JsonProperty("PrintViewType")]
        public PrintViewType PrintViewType { get; set; }

        [JsonProperty("GoodsId")]
        public string[] GoodsId { get; set; }
    }
}
