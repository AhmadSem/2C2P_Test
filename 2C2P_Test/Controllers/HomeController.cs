using _2C2P_Test.Models;
using DAL.Models;
using DAL.Repositories;
using LINQtoCSV;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;

namespace _2C2P_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TransactionsDBContext _context;

        public HomeController(ILogger<HomeController> logger, TransactionsDBContext context)
        {
            _logger = logger;
            _context = context;
        }




        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                string ext = Path.GetExtension(postedFile.FileName).ToLower();
                if (ext != ".csv" && ext != ".xml")
                {
                    ViewBag.Message = "The process only supports .csv or .xml files!";
                    return View();
                }
                if (postedFile.Length / 1024.00 > 1)
                {
                    ViewBag.Message = "The file should not excceed 1 MB!";
                    return View();
                }
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.GetFullPath(postedFile.FileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                if (ext == ".csv")
                {
                    var result = await ProcessCSV(fileName, filePath);
                    if (!result)
                    {
                        ViewBag.Message = "CSV file has wrong format!";
                        return View();
                    }
                }
                else
                {
                    var result = ProcessXML(fileName, filePath);
                    if (!await result)
                    {
                        ViewBag.Message = "XML file has wrong format!";
                        return View();
                    }
                }
                ViewBag.Message = "The file was processed successfuly!";
                return View();
            }
            else
            {
                ViewBag.Message = "Please choose a file to process!";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<bool> ProcessCSV(string fileName, string filePath)
        {
            try
            {
                List<Transaction> ToProcess = new List<Transaction>();
                CultureInfo enUS = new CultureInfo("en-US");
                string csvData = System.IO.File.ReadAllText(filePath);
                CsvContext cc = new CsvContext();
                CsvFileDescription inputFileDescription = new CsvFileDescription
                {
                    SeparatorChar = ',',
                    FirstLineHasColumnNames = false,
                    EnforceCsvColumnAttribute = true
                };
                IEnumerable<RawTransactionModel> fromCSV = cc.Read<RawTransactionModel>(filePath, inputFileDescription);
                CurrencyRepo CRepo = new CurrencyRepo(_context);
                TransactionRepo TRepo = new TransactionRepo(_context);
                foreach (var row in fromCSV)
                {
                    if (await TRepo.Exists(row.ID))
                        return false;
                    Transaction transaction = new Transaction();
                    transaction.Id = row.ID;
                    if (!decimal.TryParse(row.Amount, out decimal amount))
                    {
                        return false;
                    }
                    transaction.Amount = amount;
                    if (!IsCultureInfoFromCurrencyISO(row.CurrencyCode))
                    {
                        return false;
                    }
                    transaction.CurrencyId = await CRepo.GetID(row.CurrencyCode);
                    if (transaction.CurrencyId == 0)
                        return false;
                    if (!DateTime.TryParseExact(row.RawTransactionDate, "dd/MM/yyyy hh:mm:ss", enUS, DateTimeStyles.None, out DateTime FixedDate))
                        return false;
                    transaction.TrasactionDate = FixedDate;
                    switch (row.Status.ToLower())
                    {
                        case "approved":
                            transaction.StatusId = 1;
                            break;
                        case "failed":
                            transaction.StatusId = 2;
                            break;
                        case "finished":
                            transaction.StatusId = 3;
                            break;
                        default:
                            return false;
                    }
                    ToProcess.Add(transaction);

                }

                if (!await TRepo.AddTransactions(ToProcess))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> ProcessXML(string fileName, string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XMLRootTransactions));
                XMLRootTransactions RawItems = new XMLRootTransactions();
                using (Stream reader = new FileStream(filePath, FileMode.Open))
                {
                    // Call the Deserialize method to restore the object's state.
                    RawItems = (XMLRootTransactions)serializer.Deserialize(reader);
                }
                CurrencyRepo CRepo = new CurrencyRepo(_context);
                TransactionRepo TRepo = new TransactionRepo(_context);
                CultureInfo enUS = new CultureInfo("en-US");
                List<Transaction> ToProcess = new List<Transaction>();
                foreach (var row in RawItems.Details)
                {
                    if (await TRepo.Exists(row.ID))
                        return false;
                    Transaction transaction = new Transaction();
                    transaction.Id = row.ID;
                    if (!decimal.TryParse(row.Details.Amount, out decimal amount))
                    {
                        return false;
                    }
                    transaction.Amount = amount;
                    if (!IsCultureInfoFromCurrencyISO(row.Details.CurrencyCode))
                    {
                        return false;
                    }
                    transaction.CurrencyId = await CRepo.GetID(row.Details.CurrencyCode);
                    if (transaction.CurrencyId == 0)
                        return false;
                    transaction.TrasactionDate = DateTime.ParseExact(row.TDate, "yyyy-MM-ddTHH:mm:ss", null);
                    switch (row.Status.ToLower())
                    {
                        case "approved":
                            transaction.StatusId = 1;
                            break;
                        case "rejected":
                            transaction.StatusId = 2;
                            break;
                        case "done":
                            transaction.StatusId = 3;
                            break;
                        default:
                            return false;
                    }
                    ToProcess.Add(transaction);

                }
                if (!await TRepo.AddTransactions(ToProcess))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool IsCultureInfoFromCurrencyISO(string isoCode)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo ci in cultures)
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                if (ri.ISOCurrencySymbol == isoCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}