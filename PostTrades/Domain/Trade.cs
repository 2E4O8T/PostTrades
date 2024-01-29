using System.ComponentModel.DataAnnotations;

namespace PostTrades.Domain
{
    public class Trade
    {
        public int TradeId { get; set; }
        [Required(ErrorMessage = "Account is required.")]
        public string Account { get; set; }
        [Required(ErrorMessage = "Account Type is required.")]
        public string AccountType { get; set; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; set; }
        [Required(ErrorMessage = "Trade Security is required.")]
        public string TradeSecurity { get; set; }
        [Required(ErrorMessage = "Trade Status is required.")]
        public string TradeStatus { get; set; }
        [Required(ErrorMessage = "Trader is required.")]
        public string Trader { get; set; }
        [Required(ErrorMessage = "Benchmark is required.")]
        public string Benchmark { get; set; }
        [Required(ErrorMessage = "Book is required.")]
        public string Book { get; set; }
        [Required(ErrorMessage = "Creation Name is required.")]
        public string CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        [Required(ErrorMessage = "Revision Name is required.")]
        public string RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        [Required(ErrorMessage = "Deal Name is required.")]
        public string DealName { get; set; }
        [Required(ErrorMessage = "Deal Type is required.")]
        public string DealType { get; set; }
        [Required(ErrorMessage = "Source List Id is required.")]
        public string SourceListId { get; set; }
        [Required(ErrorMessage = "Side is required.")]
        public string Side { get; set; }
    }
}
