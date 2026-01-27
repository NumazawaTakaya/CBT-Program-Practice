using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class SITUATION
{
    public long ID { get; set; }

    public long SEVEN_COLUMNS_ID { get; set; }

    public DateTime HAPPEND_TIME { get; set; }

    public string? HAPPEND_TIME_DETAIL { get; set; }

    public string? HAPPEND_PLACE { get; set; }

    public string? CHARACTER_FROM { get; set; }

    public string? CHARACTER_TO { get; set; }

    public string PROPOSAL_OBJECT { get; set; } = null!;

    public string? APPROACH { get; set; }

    public string? OTHER_INFO { get; set; }

    public DateTime CREATED_AT { get; set; }

    public DateTime UPDATED_AT { get; set; }

    public virtual SEVEN_COLUMN SEVEN_COLUMNS { get; set; } = null!;
}
