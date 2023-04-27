using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public int? CityId { get; set; }

    public string? DistrictName { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
