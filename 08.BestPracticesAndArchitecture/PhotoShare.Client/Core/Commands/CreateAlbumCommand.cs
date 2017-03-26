namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    public class CreateAlbumCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(params string[] data)
        {
            if (data.Length < 3)
            {
                throw new InvalidOperationException($"Command CreateAlbum not valid!");
            }

            string username = data[0];
            string albumTitle = data[1];
            string bgColor = data[2];
            string[] inputTags = data.Skip(3).ToArray();

            if (username != Data.CurrentUser.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            using (var context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (context.Albums.FirstOrDefault(a => a.Name == albumTitle) != null)
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                if (!Enum.GetValues(typeof(Color)).Cast<Color>().Select(c => c.ToString()).ToList().Contains(bgColor))
                {
                    throw new ArgumentException($"Color {bgColor} not found!");
                }

                var tags = new List<Tag>();
                foreach (var tag in inputTags)
                {
                    string transformedTag = tag.ValidateOrTransform();
                    var candidatTag = context.Tags.FirstOrDefault(t => t.Name == transformedTag);
                    if (candidatTag == null)
                    {
                        throw new ArgumentException($"Invalid tags!");
                    }

                    tags.Add(candidatTag);
                }

                var albumRole = new AlbumRole();
                albumRole.User = user;
                user.AlbumRoles.Add(albumRole);
                albumRole.Role = Role.Owner;

                var album = new Album();
                album.Name = albumTitle;
                albumRole.Album = album;
                album.AlbumRoles.Add(albumRole);
                foreach (var tag in tags)
                {
                    album.Tags.Add(tag);
                }

                album.BackgroundColor = (Color)Enum.Parse(typeof(Color), bgColor);

                context.AlbumRoles.Add(albumRole);
                context.Albums.Add(album);
                context.SaveChanges();
            }

            return $"Album {albumTitle} successfully created!";
        }
    }
}
