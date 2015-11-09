namespace ActorsNet.Web.Messages
{
    public class Echo
    {
        public Echo(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}