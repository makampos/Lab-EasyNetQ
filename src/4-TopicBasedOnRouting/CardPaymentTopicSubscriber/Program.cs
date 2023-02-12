using EasyNetQ;
using Messages.Polymorphic;

namespace CardPaymentTopicSubscriber;

class Program
{
    public static void Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost"))
        {
            bus.PubSub.Subscribe<IPayment>("payments", Handler, x => x.WithTopic("payment.cardpayment"));

            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Console.ReadLine();
        }
    }

    public static void Handler(IPayment payment)
    {
        if (payment is CardPayment cardPayment)
        {
            Console.WriteLine("Processing Card Payment = <" +
                              cardPayment.CardNumber + ", " +
                              cardPayment.CardHolderName + ", " +
                              cardPayment.ExpiryDate + ", " +
                              cardPayment.Amount + ">");
        }
    }
}