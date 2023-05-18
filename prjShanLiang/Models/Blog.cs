using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? BlogHeader { get; set; }

    public string? BlogContent { get; set; }

    public string? BlogPic { get; set; }
    public string? CityName { get; set; }
    public string? DistrictName { get; set; }
    public string? RestaurantName { get; set; }

}
