using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjShanLiang.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string AccountName { get; set; } = null!;
    [DisplayName("統一編號")]
    public string? TaxId { get; set; }
    [DisplayName("餐廳名稱")]
    public string? RestaurantName { get; set; }
    [DisplayName("地址")]
    public string? RestaurantAddress { get; set; }
    [DisplayName("電話")]
    public string? RestaurantPhone { get; set; }
    [DisplayName("行政區")]
    public int? DistrictId { get; set; }
    [DisplayName("座位")]
    public int? Seats { get; set; }
    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }
    [DisplayName("開門時間")]
    public TimeSpan? OpeningTime { get; set; }
    [DisplayName("閉店時間")]
    public TimeSpan? ClosingTime { get; set; }
    [DisplayName("個人網站")]
    public string? Website { get; set; }

    public byte[]? StoreImage { get; set; }
    [DisplayName("評分")]
    public int? Rating { get; set; }

    public string? StoreMail { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<MemberAction> MemberActions { get; set; } = new List<MemberAction>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StoreEvaluate> StoreEvaluates { get; set; } = new List<StoreEvaluate>();

    public virtual ICollection<StoreImage> StoreImages { get; set; } = new List<StoreImage>();

    public virtual ICollection<StoreType> StoreTypes { get; set; } = new List<StoreType>();
}
