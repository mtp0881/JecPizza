using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JecPizza.Models.Repositories;

namespace JecPizza.Models
{
    public class AccountModel
    {
        public Member AccountData { get; set; }
        public DataTable AccountPurchaseData { get; set; }

    }
}