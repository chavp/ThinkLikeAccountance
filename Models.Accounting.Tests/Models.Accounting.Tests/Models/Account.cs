using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Accounting.Tests.Models
{
    public class Account
    {
        public long ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }

    public enum AccountType
    {
        Assets,
        Liabilities,
        Equity
    }
}
