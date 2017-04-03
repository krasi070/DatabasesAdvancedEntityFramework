namespace _02.CatalogOfMusicAlbumsInXMLFormat.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Artist
    {
        public Artist()
        {
            this.Albums = new HashSet<Album>();
        }

        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }

        public override string ToString()
        {
            string result = $"Artist: {this.Name}\nAlbums:";
            foreach (var album in this.Albums)
            {
                result += $"\n--{album.Title}: {string.Join(", ", album.Songs.Select(s => s.Title))}";
            }

            return result;
        }
    }
}