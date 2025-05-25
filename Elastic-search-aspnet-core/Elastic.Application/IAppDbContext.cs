using Elastic.Domain;
using Microsoft.EntityFrameworkCore;

namespace Elastic.Application
{
    public interface IAppDbContext
    {
        DbSet<Artist> Artists { get; set; }
        DbSet<Album> Albums { get; set; }
        DbSet<Song> Songs { get; set; }
        DbSet<Genre> Genres { get; set; }
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
