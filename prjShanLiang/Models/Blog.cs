using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? BlogHeader { get; set; }

    public string? BlogContent { get; set; }

    public string? BlogPic { get; set; }
}
