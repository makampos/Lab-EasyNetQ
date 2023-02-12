using EasyNetQ;
using Messages.Polymorphic;

namespace PurchaseOrderTopicSubscriber;

class Program
{
    private static void Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost"))
        {
            bus.PubSub.Subscribe<IPayment>("payments", Handler, x => x.WithTopic("payment.purchaseorder"));

            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Console.ReadLine();

        }
    }

    public static void Handler(IPayment payment)
    {
        var purchaseOrder = payment as PurchaseOrder;

        if (purchaseOrder != null)
        {
            Console.WriteLine("Processing Purchase Order = <" +
                              purchaseOrder.CompanyName + ", " +
                              purchaseOrder.PoNumber + ", " +
                              purchaseOrder.PaymentDayTerms + ", " +
                              purchaseOrder.Amount + ">");
        }
    }
      
}