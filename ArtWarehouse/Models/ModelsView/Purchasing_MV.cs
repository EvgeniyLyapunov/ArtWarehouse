using System.ComponentModel.DataAnnotations;

namespace ArtWarehouse.Models.ModelsView
{
    public class Purchasing_MV
    {
        public int[] goods_id { get; set; }
        [Required]
        public string[] remaining_goods { get; set; }
    }
}
