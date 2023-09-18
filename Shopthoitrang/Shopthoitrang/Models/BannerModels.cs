using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopthoitrang.Models
{
    public class BannerModels
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Nhập tên banner")]
        public string BannerName { get; set; }
        public string Imgpath { get; set; }
        [Required(ErrorMessage = "Nhập mô tả")]
        public string Description { get; set;}
    }
}
