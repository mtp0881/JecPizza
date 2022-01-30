namespace JecPizza.Models.Repositories
{
    public class Goods
    {
        public string GoodsId { get; set; }

        public string GoodsName { get; set; }

        public int Price { get; set; }

        public string GoodsImage { get; set; }

        public string Detail { get; set; }

        public bool Recommend { get; set; }

        public bool NewGoods { get; set; }

        public bool HasTopping { get; set; }

        public GoodsGroup GoodsGroupId { get; set; }

        public bool IsShow { get; set; }

    }
}