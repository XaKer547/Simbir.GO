using Simbir.GO.DataAccess.Data;
using Simbir.GO.Domain.Services;

namespace Simbir.GO.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly SimbirGoDbContext _context;
        private const double MONEY_INCREASE = 250000;
        public PaymentService(SimbirGoDbContext context)
        {
            _context = context;
        }

        public async Task IncreaseBalance()
        {
            var users = _context.Users.ToArray();

            foreach (var user in users)
            {
                user.Balance += MONEY_INCREASE;
            }

            _context.Users.UpdateRange(users);

            await _context.SaveChangesAsync();
        }

        public async Task IncreaseBalance(long userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user is null)
                return;

            user.Balance += MONEY_INCREASE;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }
    }
}
