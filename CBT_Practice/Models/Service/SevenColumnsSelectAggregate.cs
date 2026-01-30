using CBT_Practice.Data;
using CBT_Practice.Models.Entities;
using CBT_Practice.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Models.Service
{
    public class SevenColumnsSelectAggregate : SevenColumnsBaseAggregate
    {
        public SITUATION? GetSITUATION()
            => Root.SITUATIONs.FirstOrDefault();

        public List<AUTO_THOUGHT>? GetAUTO_THOUGHTs()
            => Root.AUTO_THOUGHTs.ToList();

        public SevenColumnsSelectAggregate(AppDbContext dbContext, long? sevenColumnsId)
        {
            var root = dbContext.SEVEN_COLUMNs
            .Include(x => x.SITUATIONs)
            .Include(x => x.AUTO_THOUGHTs)
            .FirstOrDefault(x => x.ID == sevenColumnsId);

            if (root != null)
            {
                Root = root;
            }
            else
            {
                Root = new SEVEN_COLUMN
                {
                    CREATED_DAY = DateTime.Now,
                    IS_COMPLETE = false,
                    IS_DELETE = false
                };
            }
        }
    }
}
