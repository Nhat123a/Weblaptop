using System.ComponentModel.DataAnnotations;

namespace Shopthoitrang.Models.ViewModels
{
	public class LoginviewModel
	{
		public int Id { get; set; }
		[Required, MinLength(4, ErrorMessage = "Vui lòng nhập tài khoản")]
		public string Username { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
		public string Password { get; set; }
		public string url { get; set; }
	}
}
