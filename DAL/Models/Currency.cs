using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
