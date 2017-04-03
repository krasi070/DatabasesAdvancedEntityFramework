namespace _02.CatalogOfMusicAlbumsInXMLFormat
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            XDocument xDoc = XDocument.Load("../../Import/music.xml");
            var artists = ParseArtists(xDoc.Root);
            foreach (var artist in artists)
            {
                Console.WriteLine(artist.ToString());
            }
        }

        private static ICollection<Artist> ParseArtists(XElement root)
        {
            var artists = new HashSet<Artist>();
            foreach (var artist in root.Elements())
            {
                var albums = new HashSet<Album>();
                foreach (var albumElement in artist.Elements())
                {
                    var album = new Album()
                    {
                        Title = albumElement.Attribute("title").Value
                    };

                    foreach (var song in albumElement.Elements().Where(e => e.Name.LocalName == "song"))
                    {
                        int[] args = song.Attribute("length").Value
                            .Split(':')
                            .Select(int.Parse)
                            .ToArray();
                        album.Songs.Add(new Song()
                        {
                            Title = song.Attribute("title").Value,
                            LengthInSeconds = args[0] * 60 + args[1]
                        });
                    }

                    album.Description = albumElement.Element("description").Value;
                    album.Link = albumElement.Element("description").Attribute("link").Value;
                    albums.Add(album);
                }

                artists.Add(new Artist()
                {
                    Name = artist.Attribute("name").Value,
                    Albums = albums
                });
            }
            

            return artists;
        }
    }
}