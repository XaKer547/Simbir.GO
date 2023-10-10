namespace Simbir.GO.Domain.Services
{
    public interface IJwtManager
    {
        Task<string> CreateToken(int userId, string role);
    }
}
