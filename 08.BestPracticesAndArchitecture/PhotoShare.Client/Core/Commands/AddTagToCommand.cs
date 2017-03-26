namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class AddTagToCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // AddTagTo <albumName> <tag>
        public string Execute(params string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command AddTagTo not valid!");
            }

            var albumName = data[0];
            var tagName = data[1].ValidateOrTransform();
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var album = context.Albums
                    .FirstOrDefault(a => a.Name == albumName);
                var tag = context.Tags
                    .FirstOrDefault(t => t.Name == tagName);

                if (album == null || tag == null)
                {
                    throw new ArgumentException($"Either tag or album do not exist!");
                }

                if (album.AlbumRoles
                    .FirstOrDefault(ar => ar.User.Username == Data.CurrentUser.Username && ar.Role == Role.Owner) == null)
                {
                    throw new InvalidOperationException("Invalid Credentials!");
                }

                context.SaveChanges();
            }

            return $"Tag {tagName} added to {albumName}!";
        }
    }
}