using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            Transactions = new HashSet<Transaction>();
        }

        public byte Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
