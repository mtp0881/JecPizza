using JecPizza.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JecPizza.Models
{

    public class ReserveModel:Reserve
    {
        public DataTable GetReserveData { get; set; }
        public Boolean Reserveflag { get; set; }
        public Boolean Reservedflag { get; set; }
        public DataTable ReservedTable { get; set; }
    }
}