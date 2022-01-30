using System.Data;

namespace JecPizza.Models
{
    public class CartModel
    {
        public DataTable GetMemberCart { get; set; }
        public DataTable GetMemberCard { get; set; }

    }
}