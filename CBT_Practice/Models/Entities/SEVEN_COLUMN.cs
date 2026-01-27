using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class SEVEN_COLUMN
{
    public long ID { get; set; }

    public string TITLE { get; set; } = null!;

    public bool IS_COMPLETE { get; set; }

    public DateTime CREATED_DAY { get; set; }

    public DateTime UPDATED_DAY { get; set; }

    public bool IS_DELETE { get; set; }

    public virtual ICollection<AUTO_THOUGHT> AUTO_THOUGHTs { get; set; } = new List<AUTO_THOUGHT>();

    public virtual ICollection<SITUATION> SITUATIONs { get; set; } = new List<SITUATION>();
}
