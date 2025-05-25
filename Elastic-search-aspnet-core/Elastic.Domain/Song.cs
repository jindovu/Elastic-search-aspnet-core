namespace Elastic.Domain
{
    public class Song
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public long AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}
