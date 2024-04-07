using CardPro.Server.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CardPro.Server.Services
{
    public class CardsService : ICardsService
    {
        private readonly List<Bank> banks;
        private readonly List<Card> cards;

        public CardsService()
        {
            banks = new List<Bank>()
            {
                new Bank {Code = "10", Name = "Leumi", Description = "Bank Leumi Israel Ltd"},
                new Bank {Code = "12", Name = "Hapoalim", Description = "Bank Hapoalim Ltd"},
                new Bank {Code = "20", Name = "Mizrahi Tefahot", Description =  "Bank Mizrahi Tefahot Ltd"},
                new Bank {Code = "11", Name = "Discount", Description = "Bank Discount Ltd"},
                new Bank {Code = "31", Name = "HaBeinleumi", Description = "\r\nFirst International Bank of Israel Ltd"}
            };
            cards = new List<Card>()
            {
                new Card {Number = "1234567812345678", IssueDate = new DateOnly(2000, 1, 1), ImagePath = "Images/Gus.jpg", IsBlocked = false, IsDigital = true, Limit = 10000, BankCode = "10"},
                new Card {Number = "8765432187654321", IssueDate = new DateOnly(2022, 1, 1), ImagePath = "Images/Hank.jpg", IsBlocked = false, IsDigital = false, Limit = 20000, BankCode = "12"},
                new Card {Number = "9999888877776666", IssueDate = new DateOnly(2010, 5, 15), ImagePath = "Images/Jane.jpg", IsBlocked = false, IsDigital = true, Limit = 15000, BankCode = "12"},
                new Card {Number = "1111222233334444", IssueDate = new DateOnly(2015, 8, 20), ImagePath = "Images/Jesse.jpg", IsBlocked = false, IsDigital = false, Limit = 25000, BankCode = "10"},
                new Card {Number = "5555666677778888", IssueDate = new DateOnly(2018, 12, 10), ImagePath = "Images/Mike.jpg", IsBlocked = false, IsDigital = true, Limit = 18000, BankCode = "20"},
                new Card {Number = "4444333322221111", IssueDate = new DateOnly(2019, 4, 5), ImagePath = "Images/Saul.jpg", IsBlocked = false, IsDigital = false, Limit = 22000, BankCode = "11"},
                new Card {Number = "9999000099990000", IssueDate = new DateOnly(2020, 6, 30), ImagePath = "Images/Skyler.jpg", IsBlocked = false, IsDigital = true, Limit = 12000, BankCode = "20"},
                new Card {Number = "7777888899990000", IssueDate = new DateOnly(2017, 10, 25), ImagePath = "Images/Todd.jpg", IsBlocked = true, IsDigital = false, Limit = 30000, BankCode = "10"},
                new Card {Number = "3333444455556666", IssueDate = new DateOnly(2014, 3, 12), ImagePath = "Images/Tuco.jpg", IsBlocked = false, IsDigital = true, Limit = 17000, BankCode = "31"},
                new Card {Number = "2222111133334444", IssueDate = new DateOnly(2023, 7, 8), ImagePath = "Images/Walter.jpg", IsBlocked = true, IsDigital = false, Limit = 28000, BankCode = "31"}
            };
        }

        public List<Bank> GetBanks()
        {
            return banks;
        }

        public List<Card> GetCards(string? isBlocked, string? cardNumber, string? bankCode)
        {
            var queriedCards = cards.ToList();
            if (isBlocked == "true")
            {
                queriedCards = queriedCards.Where(c => c.IsBlocked == true).ToList();
            }
            else if (isBlocked == "false")
            {
                queriedCards = queriedCards.Where(c => c.IsBlocked == false).ToList();
            }
            if (cardNumber != null)
            {
                queriedCards = queriedCards.Where(c => c.Number == cardNumber).ToList();
            }
            if (bankCode != null)
            {
                queriedCards = queriedCards.Where(c => c.BankCode == bankCode).ToList();
            }
            return queriedCards;
        }

        public void IncreaseCreditLimit(string cardNumber, string requestedCreditLimit, string employmentType, string averageMonthlyIncome)
        {
            var card = cards.Find(c => c.Number == cardNumber);
            if (card == null)
            {
                throw new Exception("Request denied - card was not found");
            }
            if (!float.TryParse(requestedCreditLimit, out _) || !float.TryParse(averageMonthlyIncome, out _))
            {
                throw new Exception("Request denied - only numbers are valid as input");
            }
            float requestedCreditLimitNumber = float.Parse(requestedCreditLimit);
            float averageMonthlyIncomeNumber = float.Parse(averageMonthlyIncome);
            if (requestedCreditLimitNumber > 100000)
            {
                throw new Exception("Request denied - requested credit limit is more than 100,000 NIS");
            }
            if (requestedCreditLimitNumber < card.Limit)
            {
                throw new Exception("Request denied - requested credit limit is less than the current credit limit");
            }
            if (averageMonthlyIncomeNumber < 12000)
            {
                throw new Exception("Request denied - average monthly income is less than 12,000 NIS");
            }
            if (card.IssueDate >= DateOnly.FromDateTime(DateTime.Today).AddMonths(-3))
            {
                throw new Exception("Request denied - card has issued in the last 3 monthes");
            }
            if (employmentType != "employed" && employmentType != "self employed")
            {
                throw new Exception("Request denied - must be employed or a self employed to increase credit limit");
            }
            if (employmentType == "employed" && requestedCreditLimitNumber >= averageMonthlyIncomeNumber / 2
                || employmentType == "self employed" && requestedCreditLimitNumber >= averageMonthlyIncomeNumber / 3)
            {
                throw new Exception("Request denied - average monthly income is too low");
            }
            card.Limit = requestedCreditLimitNumber;
        }
    }
}
