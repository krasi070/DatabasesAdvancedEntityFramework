namespace PhotoShare.Client.Core
{
    using System.Collections.Generic;
    using Commands;
    using System.Linq;
    using System;

    public class CommandDispatcher
    {
        private Dictionary<string, IExecutable> commands;

        public CommandDispatcher()
        {
            this.commands = new Dictionary<string, IExecutable>();
            this.AddCommands();
        }

        public string DispatchCommand(string[] commandParameters)
        {
            string commandName = commandParameters[0];
            if (!this.commands.Keys.Contains(commandName))
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }

            if (this.commands[commandName].RequiredLogin && Data.IsUserLoggedIn == false)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            return this.commands[commandName].Execute(commandParameters.Skip(1).ToArray());
        }

        private void AddCommands()
        {
            this.commands.Add("Login", new LoginCommand());
            this.commands.Add("Logout", new LogoutCommand());
            this.commands.Add("RegisterUser", new RegisterUserCommand());
            this.commands.Add("AddTown", new AddTownCommand());
            this.commands.Add("ModifyUser", new ModifyUserCommand());
            this.commands.Add("DeleteUser", new DeleteUser());
            this.commands.Add("AddTag", new AddTagCommand());
            this.commands.Add("CreateAlbum", new CreateAlbumCommand());
            this.commands.Add("AddTagTo", new AddTagToCommand());
            this.commands.Add("MakeFriends", new MakeFriendsCommand());
            this.commands.Add("ListFriends", new PrintFriendsListCommand());
            this.commands.Add("ShareAlbum", new ShareAlbumCommand());
            this.commands.Add("UploadPicture", new UploadPictureCommand());
            this.commands.Add("Exit", new ExitCommand());
        }
    }
}