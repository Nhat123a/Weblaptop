using Microsoft.EntityFrameworkCore;
using Shopthoitrang.DataContext;
using Shopthoitrang.Models;
using System.Runtime.ConstrainedExecution;

namespace Shopthoitrang.Data
{
    public class Seeding
    {
        public static void seed(Datacontext _context)
        {
            _context.Database.Migrate();

            /*
             * if (!_context.Product.Any())
            {
                CategoryModelcs acer = new CategoryModelcs { CategoryName = "Laptop Acer", Slug = "lap-top-acer", Description = "Acer ", Status = 1 };
                CategoryModelcs asus = new CategoryModelcs { CategoryName = "Laptop Asus", Slug = "lap-top-asus", Description = "Asus ", Status = 1 };
                CategoryModelcs dell = new CategoryModelcs { CategoryName = "Laptop Dell", Slug = "lap-top-dell", Description = "Dell ", Status = 1 };
                BrandModel Acer = new BrandModel { BrandName = "ACER", Slug = "thuong-hieu-acer", Description = "laptopacer", Status = 1 };
                BrandModel Asus = new BrandModel { BrandName = "ASUS", Slug = "thuong-hieu-asus", Description = "laptopacer", Status = 1 };
                BrandModel Dell = new BrandModel { BrandName = "DELL", Slug = "thuong-hieu-dell", Description = "laptopacer", Status = 1 };
                _context.Product.AddRange(
                   new ProductModel { ProductName = "LAPTOP ACER ASPIRE 3 A314-23M-R4TX (NX.KEXSV.001) (R5 7520U/8GB RAM/512GB", Slug = "lap-top-acer-aspire-3-a314-23m-r4tx", Description = "demo", Image = "1.jpg", Price = 11900, Category = acer, Brand = Acer, Status = 1 },
                   new ProductModel { ProductName = "LAPTOP ACER ASPIRE A514-54-5127 (NX.A28SV.007) (I5 1135G7/8GB RAM/512GB SSD/14.0 INCH FHD/WIN11/BẠC/VỎ NHÔM)", Slug = "lap-top-acer-aspire-a514-254-r4tx", Description = "demo", Image = "2.jpg", Price = 14900, Category = acer, Brand = Acer, Status = 1 },
                   new ProductModel { ProductName = "ASUS VIVOBOOK 14 OLED A1405VA-KM095W (I5 13500H/16GB RAM/512GB SSD/14 2.8K OLED/WIN11/BẠC/CHUỘT)", Slug = "lap-top-asus-vivobook-14-oled-a1405va-km095w-i5-13500h-16gb-ram-512gb-ssd-14-2-8k-oled-win11-bac-chuot", Description = "demo", Image = "3.jpg", Price = 17000, Category = asus, Brand = Asus, Status = 1 }

                );

                _context.SaveChanges();
            }
             */
        }
    }
}
