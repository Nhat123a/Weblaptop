namespace Shopthoitrang.Models
{
	public class OrderdeltailModel
	{

		public int Id { get; set; }
		public string Order_code { get; set; }
		public string Username { get; set; }
		public int ProductId { get; set; }
		public decimal Price { get; set; }
		public int Quanity { get; set; }
		public string Description { get; set; }
	}
}
