namespace SharpShop.Models.Base;

public class OrderModel
{
    public OrderModel() { }
    public OrderModel(int totalPrice, string deliveryAddress, bool payed)
    {
        TotalPrice = totalPrice;
        DeliveryAddress = deliveryAddress;
        Payed = payed;
    }

    public int Id { get; set; }
    public int TotalPrice { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public bool Payed { get; set; }

    public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
