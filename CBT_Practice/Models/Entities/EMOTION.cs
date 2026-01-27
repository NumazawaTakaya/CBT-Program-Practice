using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class EMOTION
{
    public long ID { get; set; }

    public long THOUGHTS_ID { get; set; }

    public string EMOTION1 { get; set; } = null!;

    public int POINT { get; set; }

    public DateTime CREATED_AT { get; set; }

    public virtual AUTO_THOUGHT THOUGHTS { get; set; } = null!;
}
