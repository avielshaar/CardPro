using CardPro.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardPro.Server.Services
{
    public interface ICardsService
    {
        List<Bank> GetBanks();
        List<Card> GetCards(string? isBlocked, string? cardNumber, string? bankCode);
        void IncreaseCreditLimit(string cardNumber, string requestedCreditLimit, string employmentType, string averageMonthlyIncome);
    }
}
