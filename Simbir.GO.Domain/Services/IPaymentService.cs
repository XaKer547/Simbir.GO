namespace Simbir.GO.Domain.Services
{
    public interface IPaymentService
    {
        Task IncreaseBalance();
        Task IncreaseBalance(long userId);
    }
}
