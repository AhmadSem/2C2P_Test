using LINQtoCSV;
using System.Xml.Serialization;

namespace _2C2P_Test.Models
{
    public class RawTransactionModel
    {
        [CsvColumn(FieldIndex = 0, CanBeNull = false)]
        public string ID { get; set; }
        [CsvColumn(FieldIndex = 1, CanBeNull = false)]
        public string Amount { get; set; }
        [CsvColumn(FieldIndex = 2, CanBeNull = false)]
        public string CurrencyCode { get; set; }
        [CsvColumn(FieldIndex = 3, CanBeNull = false)]
        public string RawTransactionDate { get; set; }
        [CsvColumn(FieldIndex = 4, CanBeNull = false)]
        public string Status { get; set; }
    }

    [XmlRoot("Transactions")]
    public class XMLRootTransactions
    {
        public XMLRootTransactions()
        {
            Details = new List<Transactions>();
        }
        [XmlElement(ElementName = "Transaction")]
        public List<Transactions> Details { get; set; }
    }

    public class Transactions
    {
        [XmlAttribute("id")]
        public string ID { get; set; }
        [XmlElement(ElementName = "PaymentDetails")]
        public PaymentDetals Details { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "TransactionDate")]
        public string TDate { get; set; }

    }

    public class PaymentDetals
    {
        [XmlElement(ElementName = "Amount")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "CurrencyCode")]
        public string CurrencyCode { get; set; }
    }

    public enum CSVTransactionStatus
    {
        Approved = 1,
        Failed = 2,
        Finished= 3
    }

    public enum XMLTransactionStatus
    {
        Approved = 1,
        Rejected = 2,
        Done = 3
    }
}
