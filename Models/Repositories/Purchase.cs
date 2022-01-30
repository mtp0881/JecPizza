using System;

namespace JecPizza.Models.Repositories
{
    public class Purchase
    {
        public char PurId { get; set; }

        public string MemId { get; set; }

        public int CardNum { get; set; }

        public string CartId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Address { get; set; }

        public int Total { get; set; }
    }
}