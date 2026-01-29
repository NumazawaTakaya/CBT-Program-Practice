using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class ADAPTIVE_THOUGHT
{
    public long ID { get; set; }

    public long THOUGHTS_ID { get; set; }

    public string BEFORE_THOUGHT { get; set; } = null!;

    public string CONJUNCTION_THOUGHT { get; set; } = null!;

    public string AFTER_THOUGHT { get; set; } = null!;

    public DateTime CREATED_AT { get; set; }

    public virtual ICollection<ADAPTIVE_THOUGHT_EMOTION> ADAPTIVE_THOUGHT_EMOTIONs { get; set; } = new List<ADAPTIVE_THOUGHT_EMOTION>();

    public virtual AUTO_THOUGHT THOUGHTS { get; set; } = null!;
}
