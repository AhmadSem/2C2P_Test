using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CurrencyRepo
    {
        private readonly TransactionsDBContext _context;
        public CurrencyRepo(TransactionsDBContext context)
        {
            _context = context;
        }
        public async Task<List<Currency>> GetAll()
        {
            try
            {
                return await _context.Currencies.ToListAsync();

            }
            catch
            {
                return new List<Currency>();
            }
        }

        public async Task<int> GetID(string code)
        {
            try
            {
                return (await _context.Currencies.FirstOrDefaultAsync(r => r.Code == code)).Id;

            }
            catch
            {
                return 0;
            }
        }
    }
}
