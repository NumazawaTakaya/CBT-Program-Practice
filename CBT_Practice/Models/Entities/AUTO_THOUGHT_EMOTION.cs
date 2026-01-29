using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class AUTO_THOUGHT_EMOTION
{
    public long ID { get; set; }

    public long AUTO_THOUGHTS_ID { get; set; }

    public string EMOTION { get; set; } = null!;

    public int POINT { get; set; }

    public DateTime CREATED_AT { get; set; }

    public virtual AUTO_THOUGHT AUTO_THOUGHTS { get; set; } = null!;
}
