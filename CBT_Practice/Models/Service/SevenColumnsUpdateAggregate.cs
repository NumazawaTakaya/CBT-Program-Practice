using CBT_Practice.Data;
using CBT_Practice.Models.Entities;
using CBT_Practice.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace CBT_Practice.Models.Service
{
    public class SevenColumnsUpdateAggregate : SevenColumnsBaseAggregate
    {
        public SevenColumnsUpdateAggregate(SEVEN_COLUMN sevenColumns) 
        {
            Root = sevenColumns;
        }

        /// <summary>
        /// SEVEN_COLUMNSのUPDATE処理を実行（ナビゲーションプロパティを利用）
        /// </summary>
        public async Task UpdateAsync(AppDbContext dbContext, CbtSession session, bool isComplete = false)
        {
            using var tx = await dbContext.Database.BeginTransactionAsync();
            try
            {
                // 既存の関連データを削除
                RemoveRelatedEntities(dbContext);

                // 同じRootに新しいEntityを作成
                var createAggregate = new SevenColumnsCreateAggregate(Root);
                createAggregate.ApplyFromSession(session,isComplete);

                // DBへ反映
                await dbContext.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        private void RemoveRelatedEntities(AppDbContext dbContext)
        {
            // Navigation PropertyをList化
            var autoThoughts = Root.AUTO_THOUGHTs.ToList();

            // =============================
            // AUTO_THOUGHT配下のデータを削除
            // =============================
            foreach (var autoThought in autoThoughts)
            {
                // -------------------------
                // ADAPTIVE_THOUGHT配下
                // -------------------------
                var adaptiveThoughts =
                    autoThought.ADAPTIVE_THOUGHTs.ToList();

                foreach (var adaptiveThought in adaptiveThoughts)
                {
                    dbContext.ADAPTIVE_THOUGHT_EMOTIONs.RemoveRange(
                        adaptiveThought.ADAPTIVE_THOUGHT_EMOTIONs);
                }

                dbContext.ADAPTIVE_THOUGHTs.RemoveRange(
                    adaptiveThoughts);


                // -------------------------
                // AUTO_THOUGHT配下
                // -------------------------
                dbContext.AUTO_THOUGHT_EMOTIONs.RemoveRange(
                    autoThought.AUTO_THOUGHT_EMOTIONs);

                dbContext.EVIDENCEs.RemoveRange(
                    autoThought.EVIDENCEs);
            }


            // =============================
            // AUTO_THOUGHTを削除
            // =============================
            dbContext.AUTO_THOUGHTs.RemoveRange(autoThoughts);


            // =============================
            // SITUATIONを削除
            // =============================
            dbContext.SITUATIONs.RemoveRange(
                Root.SITUATIONs);
        }
    }
}
