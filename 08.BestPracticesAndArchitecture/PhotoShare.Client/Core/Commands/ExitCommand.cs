namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ExitCommand : IExecutable
    {
        public bool RequiredLogin
        {
            get
            {
                return false;
            }
        }

        public string Execute(params string[] data)
        {
            if (data.Length != 0)
            {
                throw new InvalidOperationException($"Command Exit not valid!");
            }

            Console.WriteLine("Good Bye!");
            Environment.Exit(0);

            return "Good Bye!";
        }
    }
}
