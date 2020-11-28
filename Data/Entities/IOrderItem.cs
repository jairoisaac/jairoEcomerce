namespace jairoEcomerce.Data.Entities
{
    public interface IOrderItem
    {
        int Id { get; set; }
        Order Order { get; set; }
        Product Product { get; set; }
        int Quantity { get; set; }
        decimal UnitPrice { get; set; }
    }
}