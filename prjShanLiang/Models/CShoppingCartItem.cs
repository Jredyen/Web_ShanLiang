namespace prjShanLiang.Models
{
    public class CShoppingCartItem
    {
        public int mealId { get; set; }
        public int count { get; set; }
        public decimal price { get; set; }
        public decimal 小計 { get { return this.count * this.price; } }
        public MealMenu? mealmenu { get; set; }
    }
}
