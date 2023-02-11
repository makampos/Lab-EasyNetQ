using EasyNetQ;
using Messages;

namespace Sender;

public class Sender
{
    public static void Main(string [] args)
    {
        Console.WriteLine("Prearing to send messages...");
        
        for (int i = 0; i < 10; i++)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.PubSub.Publish(new TextMessage
                {
                    Text = i + ": Hello World from EasyNetQ"
                });
            }
        }
        
        Console.WriteLine("Messages sent out...");
    }
}