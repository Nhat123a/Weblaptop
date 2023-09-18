using System.ComponentModel.DataAnnotations;

namespace Shopthoitrang.Models
{
    public class CategoryModelcs
    {
        [Key]
        public int ID { get; set; }
        [Required ,MinLength(4 ,ErrorMessage ="Vui lòng nhập tên danh mục")]
        public string CategoryName { get; set; }
        //Slug là cái sinh ra từ tên danh mục (ví dụ nhiều danh mục con sinh ra)

        public string Slug { get; set; }
        [Required ]
        public int Status { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
