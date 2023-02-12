using EasyNetQ;
using Messages.Polymorphic;

public class Program
{
    public static void Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost"))
        {
            bus.PubSub.Subscribe<IPayment>("accounts", Handler, x => x.WithTopic("payment.*"));

            Console.WriteLine("Listening for (payment.*) messages. Hit <return> to quit.");
            Console.ReadLine();
        }
        
    }
    private static void Handler(IPayment payment)
    {
        if (payment is PurchaseOrder purchaseOrder)
        {
            Console.WriteLine("Processing Purchase Order = <" +
                              purchaseOrder.CompanyName + ", " +
                              purchaseOrder.PoNumber + ", " +
                              purchaseOrder.PaymentDayTerms + ", " +
                              purchaseOrder.Amount + ">");
        }

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