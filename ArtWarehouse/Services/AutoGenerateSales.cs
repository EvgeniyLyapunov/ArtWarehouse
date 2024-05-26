using ArtWarehouse.Models.ModelsView;
using System;

namespace ArtWarehouse.Services
{
    public class AutoGenerateSales
    {
        public static bool FirstSaleAutoGenerate = false;
        public static void SalesGenerator(Warehouse_DbService warehouse_Db, Sale_DbService sale_Db)
        {
            Random rnd = new Random();
            int countOfOrders = rnd.Next(1, 6);
            var listOfGoods = warehouse_Db.ListGoods_Get();

            for (int i = 0; i < countOfOrders; i++)
            {
                int countOfGoodsInOrder = rnd.Next(1, 6);

                Sale_MV sale = new Sale_MV();

                for (int j = 0; j < countOfGoodsInOrder; j++)
                {
                    int goodsIndex = 0;
                    int goodsCount = 0;

                    int tryingCount = 3;

                    while (tryingCount > 0)
                    {
                        goodsIndex = rnd.Next(0, listOfGoods.Count);
                        if (listOfGoods[goodsIndex].remaining_goods == 0)
                        {
                            tryingCount--;
                        }
                        else
                        {
                            goodsCount = rnd.Next(1, listOfGoods[goodsIndex].remaining_goods + 1);
                            sale.GoodsIds.Add(listOfGoods[goodsIndex].goods_id);
                            sale.GoodsCount.Add(goodsCount);
                            listOfGoods[goodsIndex].remaining_goods = listOfGoods[goodsIndex].remaining_goods - goodsCount;

                            break;
                        }
                    }
                }

                sale_Db.SaleTransaction(sale);
            }
        }
    }
}
