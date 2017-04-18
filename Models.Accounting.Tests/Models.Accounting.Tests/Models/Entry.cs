using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Accounting.Tests.Models
{
    public class Entry
    {
        public long ID { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public EntryType EntryType { get; set; }
        public DateTime BookedDate { get; set; }
    }

    public enum EntryType
    {
        Debit, Credit
    }

    public enum Currency
    {
        THB, USD
    }
}
