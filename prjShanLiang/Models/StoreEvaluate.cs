using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreEvaluate
{
    public int EvaluateNo { get; set; }

    public int? StoreId { get; set; }

    public int? MemberId { get; set; }

    public string? Comments { get; set; }

    public int? Rating { get; set; }

    public DateTime? EvaluateDate { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Store? Store { get; set; }
}
