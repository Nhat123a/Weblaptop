using Shopthoitrang.Data.Valication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopthoitrang.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Nhập tên sản phẩm")]
        public string ProductName { get; set; }
        public string Slug { set; get; }
        [Required,MinLength(4,ErrorMessage ="Vui lòng nhập mô tả")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(20,2)")]
        public decimal Price { get; set; }
        public int Status { get; set; }
        [Required,Range(1,int.MaxValue,ErrorMessage ="Chọn danh mục ")]
        public int CategoryId { get;set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn thương hiệu ")]
        public int BrandID { get; set; }
		public CategoryModelcs Category { get; set; }
        public BrandModel Brand { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile Imgload { get; set; }
    }
}
