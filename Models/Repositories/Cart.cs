namespace JecPizza.Models.Repositories
{
    public class Cart
    {
        public string CartId { get; set; }

        public string GoodsId { get; set; }
        public string ToppingCartId { get; set; }
        public string MemberId { get; set; }

        public int Num { get; set; }
    }
}