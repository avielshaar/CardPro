namespace CardPro.Server.Models
{
    public class Card
    {
        public string Number { get; init; }
        public DateOnly IssueDate { get; init; }
        public string ImagePath { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDigital { get; set; }
        public float Limit { get; set; }
        public string BankCode {  get; set; }
    }
}
