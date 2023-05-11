using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string AccountName { get; set; } = null!;

    public string? TaxId { get; set; }

    public string? RestaurantName { get; set; }

    public string? RestaurantAddress { get; set; }

    public string? RestaurantPhone { get; set; }

    public int DistrictId { get; set; }

    public int? Seats { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public TimeSpan? OpeningTime { get; set; }

    public TimeSpan? ClosingTime { get; set; }

    public string? Website { get; set; }

    public byte[]? StoreImage { get; set; }

    public int? Rating { get; set; }

    public string? StoreMail { get; set; }

    public string Password { get; set; } = null!;

    public int? AccountStatus { get; set; }

    public virtual AccountStatus? AccountStatusNavigation { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<MealMenu> MealMenus { get; set; } = new List<MealMenu>();

    public virtual ICollection<MealOrder> MealOrders { get; set; } = new List<MealOrder>();

    public virtual ICollection<MemberAction> MemberActions { get; set; } = new List<MemberAction>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StoreAdImage> StoreAdImages { get; set; } = new List<StoreAdImage>();

    public virtual ICollection<StoreDecorationImage> StoreDecorationImages { get; set; } = new List<StoreDecorationImage>();

    public virtual ICollection<StoreEvaluate> StoreEvaluates { get; set; } = new List<StoreEvaluate>();

    public virtual ICollection<StoreMealImage> StoreMealImages { get; set; } = new List<StoreMealImage>();

    public virtual ICollection<StoreReserved> StoreReserveds { get; set; } = new List<StoreReserved>();

    public virtual ICollection<StoreType> StoreTypes { get; set; } = new List<StoreType>();
}
