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

    public int? DistrictId { get; set; }

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

    public virtual District? District { get; set; }

    public virtual ICollection<MemberAction> MemberActions { get; set; } = new List<MemberAction>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StoreEvaluate> StoreEvaluates { get; set; } = new List<StoreEvaluate>();

    public virtual ICollection<StoreImage> StoreImages { get; set; } = new List<StoreImage>();

    public virtual ICollection<StoreType> StoreTypes { get; set; } = new List<StoreType>();
}
