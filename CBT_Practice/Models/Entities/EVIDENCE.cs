using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class EVIDENCE
{
    public long ID { get; set; }

    public long THOUGHTS_ID { get; set; }

    public string EVIDENCE1 { get; set; } = null!;

    public string? INSIDE_BELIEF { get; set; }

    public string? CORE_BELIEF { get; set; }

    public string? COUNTER_EVIDENCE { get; set; }

    public DateTime CREATED_AT { get; set; }

    public virtual AUTO_THOUGHT THOUGHTS { get; set; } = null!;
}
