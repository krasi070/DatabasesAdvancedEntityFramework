namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(params string[] data)
        {
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command ShareAlbum not valid!");
            }

            int albumId = int.Parse(data[0]);
            string albumName = string.Empty;
            string username = data[1];
            string permission = data[2];

            using (var context = new PhotoShareContext())
            {
                var album = context.Albums
                    .FirstOrDefault(a => a.Id == albumId);
                if (album == null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                albumName = album.Name;
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User {user} not found!");
                }

                if (album.AlbumRoles.FirstOrDefault(ar => ar.User.Username == Data.CurrentUser.Username && ar.Role == Role.Owner) == null)
                {
                    throw new InvalidOperationException("Invalid Credentials!");
                }

                if (permission != "Owner" && permission != "Viewer")
                {
                    throw new ArgumentException("Permission must be either \"Owner\" or \"Viewer\"!");
                }

                var albumRole = new AlbumRole();
                albumRole.Album = album;
                albumRole.User = user;
                albumRole.Role = (Role)Enum.Parse(typeof(Role), permission);
                context.AlbumRoles.Add(albumRole);
                album.AlbumRoles.Add(albumRole);

                context.SaveChanges();
            }

            return $"Username {username} added to album {albumName} ({permission})";
        }
    }
}