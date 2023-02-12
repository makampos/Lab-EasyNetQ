using EasyNetQ;
using Messages;

namespace Subscribe;

class Program
{
    public static void Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost"))
        {
            bus.PubSub.Subscribe<CardPaymentRequestMessage>("cardPayment", HandleCardPaymentMessage);

            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Console.ReadLine();
        }
    }

    static void HandleCardPaymentMessage(CardPaymentRequestMessage paymentMessage)
    {
        Console.WriteLine("Payment = <" +
                          paymentMessage.CardNumber + ", " +
                          paymentMessage.CardHolderName + ", " +
                          paymentMessage.ExpiryDate + ", " +
                          paymentMessage.Amount + ">");
    }
}