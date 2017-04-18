using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Accounting.Tests.Models
{
    public class AccountingTransaction
    {
        public Account Account { get; set; }
        public Entry Entry { get; set; }
        public string TransactionCode { get; set; }
    }
}
