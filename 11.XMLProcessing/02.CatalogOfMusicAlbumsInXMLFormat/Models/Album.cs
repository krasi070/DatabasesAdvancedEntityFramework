namespace _02.CatalogOfMusicAlbumsInXMLFormat.Models
{
    using System.Collections.Generic;

    public class Album
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }

        public string Title { get; set; }
        
        public ICollection<Song> Songs { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }
    }
}