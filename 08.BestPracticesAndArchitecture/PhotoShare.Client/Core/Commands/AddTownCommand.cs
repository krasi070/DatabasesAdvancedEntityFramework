namespace PhotoShare.Client.Core.Commands
{
    using System.Linq;
    using Models;
    using System;

    public class AddTownCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // AddTown <townName> <countryName>
        public string Execute(params string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command AddTown not valid!");
            }

            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Towns
                    .FirstOrDefault(t => t.Name == townName && t.Country == country) != null)
                {
                    throw new ArgumentException($"Town {townName} was already added!");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();
            }

            return townName + " was added to database!";
        }
    }
}