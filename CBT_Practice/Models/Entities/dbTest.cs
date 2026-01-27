using System;
using System.Collections.Generic;

namespace CBT_Practice.Models.Entities;

public partial class dbTest
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string ChangeType { get; set; } = null!;
}
