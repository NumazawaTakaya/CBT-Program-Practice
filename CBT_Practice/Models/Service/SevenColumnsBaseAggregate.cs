using CBT_Practice.Models.Entities;

namespace CBT_Practice.Models.Service
{
    public class SevenColumnsBaseAggregate
    {
        protected SEVEN_COLUMN Root { get; set; } = null!;

        protected DateTime Now => DateTime.Now;

        protected void ApplyTitle(string? title)
        {
            if (title != null)
                Root.TITLE = title;
        }
    }
}
