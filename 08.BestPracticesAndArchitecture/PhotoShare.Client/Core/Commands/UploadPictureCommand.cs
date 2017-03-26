namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(params string[] data)
        {
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command UploadPicture not valid!");
            }

            string albumName = data[0];
            string pictureTitle = data[1];
            string path = data[2];

            using (var context = new PhotoShareContext())
            {
                var album = context.Albums
                    .FirstOrDefault(a => a.Name == albumName);
                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                if (album.AlbumRoles
                    .FirstOrDefault(ar => ar.User.Username == Data.CurrentUser.Username && ar.Role == Role.Owner) == null)
                {
                    throw new InvalidOperationException("Invalid Credentials");
                }

                var picture = new Picture();
                picture.Albums.Add(album);
                picture.Title = pictureTitle;
                picture.Path = path;
                album.Pictures.Add(picture);
                context.Pictures.Add(picture);

                context.SaveChanges();
            }

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}