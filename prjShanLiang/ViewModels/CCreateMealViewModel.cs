using System.ComponentModel;

namespace prjShanLiang.ViewModels
{
    public class CCreateMealViewModel
    {

        public int MealId { get; set; }

        public int? StoreId { get; set; }
        [DisplayName("餐點名稱")]
        public string? MealName { get; set; }
        [DisplayName("餐點價格")]
        public decimal? MealPrice { get; set; }
        [DisplayName("照片")]
        public string? MealImagePath { get; set; }
        [DisplayName("描述")]
        public string? Recommendation { get; set; }
        public byte[]? MealImage { get; set; }
    }
}
