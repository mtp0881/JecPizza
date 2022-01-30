namespace JecPizza.Models.Repositories
{
    public class ToppingCart
    {
        public string TpCId { get; set; }
        public Topping ToppingId { get; set; }
        public int Num { get; set; }
    }
}