namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ModifyUserCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return true;
            }
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(params string[] data)
        {
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command ModifyUser not valid!");
            }

            string username = data[0];
            string property = data[1];
            string newValue = data[2];

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

                switch (property)
                {
                    case "Password":
                        if (newValue == newValue.ToUpper() || 
                            newValue == Regex.Replace(newValue, "[0-9]", ""))
                        {
                            throw new ArgumentException($"Value {newValue} not valid. Invalid Password!");
                        }

                        user.Password = newValue;
                        break;
                    case "BornTown":
                        var bornTown = context.Towns.FirstOrDefault(t => t.Name == newValue);
                        if (bornTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid. Town {newValue} not found!");
                        }

                        bornTown.UsersBornInTown.Remove(user);
                        user.BornTown = bornTown;
                        bornTown.UsersBornInTown.Add(user);
                        break;
                    case "CurrentTown":
                        var currTown = context.Towns.FirstOrDefault(t => t.Name == newValue);
                        if (currTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid. Town {newValue} not found!");
                        }

                        currTown.UsersCurrentlyLivingInTown.Remove(user);
                        user.CurrentTown = currTown;
                        currTown.UsersCurrentlyLivingInTown.Add(user);
                        break;
                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                }

                context.SaveChanges();
            }

            return $"User {username} {property} is {newValue}.";
        }
    }
}