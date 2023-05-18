using prjShanLiang.Models;
using System.ComponentModel;

namespace prjShanLiang.ViewModels
{
    public class CCreateStoreAccountViewModel
    {
        public Store sto; 
        [DisplayName("帳號")]
        public string AccountName { get; set; }
        [DisplayName("密碼")]
        public string AccountPassword { get; set; }
        [DisplayName("統一編號")]
        public string? TaxID { get; set; }
        [DisplayName("餐廳名稱")]
        public string RestaurantName { get; set; }
        [DisplayName("餐廳電話")]
        public string RestaurantPhone { get; set; }
        [DisplayName("餐廳地址")]
        public string RestaurantAddress { get; set; }
        [DisplayName("行政區")]
        public int DistrictId { get; set; }
        [DisplayName("座位數量")]
        public int Seats { get; set; }
        [DisplayName("E-mail")]
        public string StoreMail { get; set; }

        public string Website { get; set; }
        public byte[]? StoreImage { get; set; }

        public string districtName { get; set; }

        public string storeDistrict { get; set; }
        public DateTime OpenningTime { get; set; }
        public DateTime ClosingTime { get; set; }
    }
}
