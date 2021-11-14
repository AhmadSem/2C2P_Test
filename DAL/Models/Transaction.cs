using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Transaction
    {
        public string Id { get; set; } = null!;
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public DateTime TrasactionDate { get; set; }
        public byte StatusId { get; set; }

        public virtual Currency Currency { get; set; } = null!;
        public virtual TransactionStatus Status { get; set; } = null!;
    }
}
