using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TransactionRepo
    {
        private readonly TransactionsDBContext _context;
        public TransactionRepo(TransactionsDBContext context)
        {
            _context = context;
        }
        public async Task<bool> AddTransactions(List<Transaction> ToAdd)
        {
            try
            {
                await _context.AddRangeAsync(ToAdd);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Exists(string ID)
        {
            try
            {
                return (await _context.Transactions.FirstOrDefaultAsync(r => r.Id == ID)) != null;
            }
            catch
            {
                return true;
            }
        }

        public async Task<List<TransactionsAPIModel>> GetAll(string currenccyy, string status, DateTime? from, DateTime? to)
        {
            try
            {
                int CurrencyID = 0;
                if (!string.IsNullOrEmpty(currenccyy))
                {
                    CurrencyRepo CRepo = new CurrencyRepo(_context);
                    CurrencyID = await CRepo.GetID(currenccyy);
                }
                byte StatusID = 0;
                if (!string.IsNullOrEmpty(status))
                {
                    switch (status.ToLower())
                    {
                        case "a":
                        case "approved":
                            StatusID = 1;
                            break;
                        case "r":
                        case "rejected ":
                        case "failed ":
                            StatusID = 2;
                            break;
                        case "d":
                        case "done":
                        case "finished":
                            StatusID = 3;
                            break;
                        default:
                            break;
                    }
                }
                var fromDB = _context.Transactions.Where(r =>
                ((!string.IsNullOrEmpty(currenccyy) && r.CurrencyId == CurrencyID) || string.IsNullOrEmpty(currenccyy))
                && ((!string.IsNullOrEmpty(status) && r.StatusId == StatusID) || string.IsNullOrEmpty(status))
                && ((from.HasValue && r.TrasactionDate >= from.Value) || !from.HasValue)
                && ((to.HasValue && r.TrasactionDate <= to.Value) || !to.HasValue)
                );
                return await fromDB.Select(r => new TransactionsAPIModel { Id = r.Id, Payment = r.Amount.ToString()+r.Currency.Code, Status = r.Status.Name}).ToListAsync();
            }
            catch
            {
                return new List<TransactionsAPIModel>();
            }
        }
    }
}
