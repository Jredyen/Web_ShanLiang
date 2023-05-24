using System.ComponentModel;

namespace prjShanLiang.ViewModels
{
    public class CMealViewModel
    {

        public int MealId { get; set; }

        public int? StoreId { get; set; }
        [DisplayName("餐點名稱")]
        public string? MealName { get; set; }
        [DisplayName("餐點價格")]
        public decimal? MealPrice { get; set; }
        [DisplayName("照片")]
        public string? MealImagePath { get; set; }
        [DisplayName("餐點介紹")]
        public string? Recommendation { get; set; }
        public IFormFile? MealImage { get; set; }
    }
}
