namespace ActorsNet.Web.Messages
{
    public class Greet
    {
        public Greet(string who)
        {
            Who = who;
        }

        public string Who { get; private set; }
    }
}