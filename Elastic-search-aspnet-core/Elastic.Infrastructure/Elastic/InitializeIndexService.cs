using Elastic.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Elastic.Infrastructure.Elastic;

public interface IInitializeIndexService
{
    Task Run();
}

public class InitializeIndexService : IInitializeIndexService
{
    private readonly IAppDbContext _context;
    private readonly IElasticClient _elasticClient;
    private readonly IConfiguration _configuration;
    
    public InitializeIndexService(IAppDbContext context, IElasticClient elasticClient, IConfiguration configuration)
    {
        _context = context;
        _elasticClient = elasticClient;
        _configuration = configuration;
    }

    public async Task Run()
    {
        var index = _configuration.GetSection("Elastic:Index").Value;

        await _elasticClient.Indices.DeleteAsync(index);

        var response = await _elasticClient.Indices.CreateAsync(index,
            x => x.Map<ElasticSong>(xx => xx.AutoMap()));
        
        var songs = await _context.Songs.AsNoTracking()
            .Include(x => x.Album)
            .ThenInclude(x => x!.Genre)
            .Include(x => x.Album)
            .ThenInclude(x => x!.Artist)
            .ToListAsync();

        var elasticSongs = songs.Select(x => x.ToElasticSong());

        await _elasticClient.BulkAsync(x => x
            .Index(index)
            .IndexMany(elasticSongs));
    }
}