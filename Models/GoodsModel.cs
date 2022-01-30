using System.Collections.Generic;
using System.Data;
using JecPizza.Models.Repositories;

namespace JecPizza.Models
{
    public class GoodsModel
    {
        public DataTable GoodsDataTable { get; set; }
        public DataTable RecGoodsDataTable { get; set; }
        public DataTable NewGoodsTab { get; set; }
        public  IList<Topping> GeToppings { get; set; }
        public DataTable SearchGoodsDataTable { get; set; }
    }
  
}