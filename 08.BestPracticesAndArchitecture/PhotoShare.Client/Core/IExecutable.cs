namespace PhotoShare.Client.Core
{
    public interface IExecutable
    {
        string Execute(params string[] data);

        bool RequiredLogin { get; }
    }
}