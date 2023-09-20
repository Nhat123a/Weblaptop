using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Shopthoitrang.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string Order_code { get; set; }
		public string Username { get; set; }
		public DateTime Creatiem { get; set; }
		public int status { get; set; }
		public int phoneNumber { get; set; }
		public string Address { get; set; }

		public string Descripton { get; set; }
	}
}
