using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class AUTO_THOUGHT
{
    public long ID { get; set; }

    public long SEVEN_COLUMNS_ID { get; set; }

    public string AUTO_THOUGHT1 { get; set; } = null!;

    public bool IS_MAIN { get; set; }

    public DateTime CREATED_AT { get; set; }

    public virtual ICollection<EMOTION> EMOTIONs { get; set; } = new List<EMOTION>();

    public virtual ICollection<EVIDENCE> EVIDENCEs { get; set; } = new List<EVIDENCE>();

    public virtual SEVEN_COLUMN SEVEN_COLUMNS { get; set; } = null!;
}
