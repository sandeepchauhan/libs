using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications
{
    public class BankTransaction
    {
        public int Id { get; set; }

        public BankTransaction()
        {
            this.Details = "NA";
            this.ReferenceNumber = "NA";
            this.SourceFile = "NA";
            this.Type = BankTransactionType.Debit;
        }

        public BankTransactionInstrumentType InstrumentType { get; set; }

        public BankTransactionType Type { get; set; }

        public DateTime Date { get; set; }

        public string ReferenceNumber { get; set; }

        public string Details { get; set; }

        public int Amount { get; set; }

        public int IntlAmount { get; set; }

        public string SourceFile { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }
    }
}
