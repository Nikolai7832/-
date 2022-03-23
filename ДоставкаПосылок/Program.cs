public enum DeliveryTypes
{
    PickPoint,
    Transport
}
class Attachment
{
    private int LastID = 0;
    internal protected string CustomerID;
    internal protected int ID;
    internal int Value;
    internal int Weight;
    internal Attachment(int Value, int Weight, Customer customer)
    {
        ID = LastID++;
        LastID = LastID++;
        this.Value = Value;
        this.Weight = Weight;
        CustomerID = customer.PhoneNumber;
    }
}
class Package<TAttachment> where TAttachment : Attachment
{
    private TAttachment Attachment1;
    private TAttachment Attachment2;
    private TAttachment Attachment3;
    public string ID;
    public int Value;
    public int Weight;

    internal Package(Attachment Attachment1, Attachment Attachment2, Attachment Attachment3)
    {

        ID = Attachment3.ID + "PAC";
        Value = Attachment1.Value + Attachment2.Value + Attachment3.Value;
        Weight = Attachment1.Weight + Attachment2.Weight + Attachment3.Weight;
    }
    internal Package(Attachment Attachment1, Attachment Attachment2)
    {
        ID = Attachment2.ID + "PAC";
        Value = Attachment1.Value + Attachment2.Value;
        Weight = Attachment1.Weight + Attachment2.Weight;
    }
    internal Package(Attachment Attachment1)
    {
        ID = Attachment1.ID + "PAC";
        Value = Attachment1.Value;
        Weight = Attachment1.Weight;
    }
}
public struct Customer

{
    internal string Adress;
    internal string Name;
    internal string PhoneNumber;
    internal Customer(string Adress, string Name, string PhoneNumber)
    {
        this.Adress = Adress;
        this.Name = Name;
        this.PhoneNumber = PhoneNumber;
    }
}
struct Shop
{
    internal string PhoneNumber;
    internal string Adress;
    internal double Speed;
    internal static string name = "DeliveryShop";
    internal Shop(string PhoneNumber, string Adress, double FrequencyTrips)
    {
        this.PhoneNumber = PhoneNumber;
        this.Adress = Adress;
        Speed = FrequencyTrips;

    }
}

public struct DeliveryCompany
{
    internal string Name;
    internal string PhoneNumber;
    internal string Adress;
    DeliveryTypes DeliveryType;
    internal double Speed;
    internal int cost;
    internal DeliveryCompany(DeliveryTypes DeliveryType, string Name, string Adress, string PhoneNumber, double Speed, int cost)
    {
        this.DeliveryType = DeliveryType;
        this.Name = Name;
        this.PhoneNumber = PhoneNumber;
        this.Adress = Adress;
        this.Speed = Speed;
        this.cost = cost;

    }
}
abstract class Delivery
{
    internal object Adress;
    internal object Speed;
    internal object Name;
    internal object Phone;
    internal object Company;

    public Delivery(string adress, double speed, string name, string phone, string company)
    {
        Adress = adress;
        Speed = speed;
        Name = name;
        Phone = phone;
        Company = company;
    }
}

class HomeDelivery : Delivery
{
    internal HomeDelivery(Customer customer, DeliveryCompany deliveryCompany) : base(customer.Adress, deliveryCompany.Speed, deliveryCompany.Name, deliveryCompany.PhoneNumber, deliveryCompany.Adress)
    { }

}
class PickPointDelivery : Delivery
{
    public PickPointDelivery(DeliveryCompany deliveryCompany) : base(deliveryCompany.Adress, deliveryCompany.Speed, deliveryCompany.Name, deliveryCompany.PhoneNumber, deliveryCompany.Adress)
    { }
}

class ShopDelivery : Delivery
{
    public ShopDelivery(Shop shop) : base(shop.Adress, shop.Speed, Shop.name, shop.PhoneNumber, shop.Adress)
    { }

}

internal class Order
{


    private string ID;
    private int Weight;
    private int Value;
    private string Customer;
    private string CustomerPhone;
    private string CustomerAdress;
    private string Adress;
    public string Company;
    public string CompanyAdress;
    public string CompanyPhone;
    private DateTime date;



    public Order(Delivery delivery, Customer customer, Package<Attachment> package)
    {
        Weight = package.Weight;
        Value = package.Value;
        Customer = customer.Name;
        CustomerPhone = customer.PhoneNumber;
        CustomerAdress = customer.Adress;
        ID = "ORD" + package.ID;
        Adress = (string)delivery.Adress;
        date = DateTime.Now;
        date = date.AddDays((double)delivery.Speed);
        Company = (string)delivery.Name;
        CompanyAdress = (string)delivery.Company;
        CompanyPhone = (string)delivery.Phone;
    }
    static internal void ShowDesc(Order order)
    {
        Console.WriteLine(
            "Номер заказа - {0}" +
            "\nВес - {1}" +
            "\nОбъем - {2}" +
            "\nПолучатель - {3}" +
            "\nАдрес доставки - {4}" +
            "\nДата доставки{5}" +
            "\nКурьерская служба - {6}",
            order.ID, order.Weight, order.Value, order.Customer, order.Adress, order.date, order.Company);
    }
    static internal void ShowDelDesc(Order order)
    {
        Console.WriteLine(
           "Курьерская служба - {0}" +
           "\nТелефон - {1}" +
           "\nАдрес - {2}",
           order.Company, order.CompanyPhone, order.CompanyAdress);
    }
    static internal void ShowCustDesc(Order order)
    {
        Console.WriteLine(
           "Имя - {0}" +
           "\nТелефон - {1}" +
           "\nАдрес -{2}",
           order.Customer, order.CustomerPhone, order.CustomerAdress);
    }
}

class Programm
{
    public static void Main(string[] args)
    {
        Customer Anatoliy = new("Smolensk", "Anatoliy", "89787576");

        Shop Shop = new("0898676747", "Smolensk", 3);
        DeliveryCompany FAF = new(DeliveryTypes.Transport, "FastAndFurious", "Wherever Vin Diesel is", "121212", 0, 100);
        DeliveryCompany SAL = new(DeliveryTypes.PickPoint, "SlowAndLazy", "???", "8900075322456787990", 100, 0);


        HomeDelivery ForAnatoliyHD = new(Anatoliy, FAF);
        PickPointDelivery ForAnatoliyPPD = new(SAL);
        ShopDelivery ForAnatoliySD = new(Shop);


        Attachment Pen = new(5, 6, Anatoliy);
        Attachment Apple = new(2, 8, Anatoliy);
        Attachment Pineaple = new(1, 5, Anatoliy);


        Package<Attachment> Pac1 = new(Pen);
        Package<Attachment> Pac2 = new(Pen, Apple);
        Package<Attachment> Pac3 = new(Pen, Apple, Pineaple);


        Order First = new(ForAnatoliyHD, Anatoliy, Pac1);


        Order Second = new(ForAnatoliyPPD, Anatoliy, Pac2);


        Order Third = new(ForAnatoliyHD, Anatoliy, Pac3);
        static void Description(Order order, string description)
        {
            if (description == "All")
            {
                Order.ShowDelDesc(order);
                Console.WriteLine();

                Order.ShowDesc(order);
                Console.WriteLine();

                Order.ShowCustDesc(order);
                Console.WriteLine();
            }
            if (description == "Delivery")
            {
                Order.ShowDelDesc(order);
                Console.WriteLine();
            }
            if (description == "Order")
            {
                Order.ShowDesc(order);
                Console.WriteLine();
            }
            if (description == "Customer")
            {
                Order.ShowCustDesc(order);
                Console.WriteLine();
            }

        }
        Description(First, "All");
        Description(Second, "Customer");
        Description(Third, "Delivery");
        Description(Third, "Order");
    }

}