using System;

namespace JecPizza.Models.Repositories
{
    public class Reserve
    {
        public string ReserveId { get; set; }
        public string MemberId { get; set; }
        public DateTime ReserveDateTime { get; set; }
        public int Num { get; set; }
        public int TableNo { get; set; }
    }
}