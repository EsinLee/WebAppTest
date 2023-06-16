using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Models.Domain
{
    public class AddCashFlow
    {
        public string ProposerId { get; set; }
        public string ReceiptId { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public double Money { get; set; }
        public string Trader { get; set; }
        public string AccountNamesId { get; set; }

        // -------------------------------------------------
        public bool BoolDoAlert { get; set; }
        public string AlertString { get; set; }
    }
    public class UpdateCashFlow
    {
        public Guid Id { get; set; }
        public Guid ProposerId { get; set; }
        public string ReceiptId { get; set; }
    }

    public class AddReceipt
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public double Money { get; set; }
        public string Trader { get; set; }
        public string AccountNamesId { get; set; }
    }
    public class UpdateReceipt
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public double Money { get; set; }
        public string Trader { get; set; }
        public string AccountNamesId { get; set; }
    }

    public class AddAccountName
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
    }
    public class UpdateAccountName
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
    }
}
