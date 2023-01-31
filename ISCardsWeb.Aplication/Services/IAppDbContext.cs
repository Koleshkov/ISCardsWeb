using Microsoft.EntityFrameworkCore;
using ISCardsWeb.Shared.Models;

namespace ISCardsWeb.Aplication.Services
{
    public interface IAppDbContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; }

        Task<int>  SaveChangesAsync(CancellationToken cancellationToken);
    }
}
