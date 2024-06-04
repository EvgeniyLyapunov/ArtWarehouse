using System.Collections.Generic;

namespace ArtWarehouse.Models.ModelsView
{
    public class Movement_MV
    {
        public List<MovementGoods_MV> movementGoods { get; set; }
        public List<string> NamesOfGoods { get; set; }
    }
}
