using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Models.Domain
{
    public class CashFlow
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProposerId { get; set; }
        public string ReceiptId { get; set; }
    }

    public class Receipt
    {
        [Key]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public double Money { get; set; }
        public string Trader { get; set; }
        public string AccountNamesId { get; set; }
    }

    public class AccountName
    {
        [Key]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }

    }
}
