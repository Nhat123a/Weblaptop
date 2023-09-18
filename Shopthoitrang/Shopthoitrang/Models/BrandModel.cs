using System.ComponentModel.DataAnnotations;

namespace Shopthoitrang.Models
{
    public class BrandModel
        
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required, MinLength(5, ErrorMessage = "Vui lòng nhập Mô tả")]
        public string Description { get; set; }
        public string Slug { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
