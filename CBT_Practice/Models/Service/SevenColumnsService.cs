using CBT_Practice.Data;
using CBT_Practice.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Models.Service
{
    public class SevenColumnsService
    {
        public static async Task CreateAsync(AppDbContext dbContext, SEVEN_COLUMN sevenColumn)
        {
            using var tx = await dbContext.Database.BeginTransactionAsync();
            try
            {
                dbContext.SEVEN_COLUMNs.Add(sevenColumn);
                await dbContext.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
