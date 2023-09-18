namespace Shopthoitrang.Models
{
    public class CartModel
    {
        public long productid { get; set; }
        public string productname { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public int Quanity { get; set; }
        public string Image { get; set; }
        public decimal Total
        {
            get { return Quanity * price; }
        }
        public CartModel()
        {

        }
        public CartModel(ProductModel product)
        {
            productid = product.ProductId;
            productname=product.ProductName;
            price = product.Price;
            description = product.Description;
            Image = product.Image;
            Quanity = 1;

        }
    }
}
