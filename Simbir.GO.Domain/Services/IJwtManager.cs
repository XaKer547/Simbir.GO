namespace Simbir.GO.Domain.Services
{
    public interface IJwtManager
    {
        Task<string> CreateToken(long userId, string role);
    }
}
