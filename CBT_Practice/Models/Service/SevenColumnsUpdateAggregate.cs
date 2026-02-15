using CBT_Practice.Data;
using CBT_Practice.Models.Entities;
using CBT_Practice.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Models.Service
{
    public class SevenColumnsUpdateAggregate : SevenColumnsBaseAggregate
    {
        public SevenColumnsUpdateAggregate(SEVEN_COLUMN sevenColumns) 
        {
            Root = sevenColumns;
        }

        public void ApplyFromSession(CbtSession session)
        {
            // タイトルを設定
            if (session.Title != null)
            {
                ApplyTitle(session.Title);
            }

            // SITUATIONを設定
            if (session.Situation != null)
            {
                UpdateSituation(session.Situation);
            }

            // AUTO_THOUGHTを設定
            if (session.AutoThoughtList != null)
            {
                UpdateAutoThoughtList(session.AutoThoughtList, session.MainThoughtIndex);
            }
        }

        public void UpdateSituation(ViewModels.Situation vm)
        {
            SITUATION? situation = Root.SITUATIONs.FirstOrDefault();
            if(situation != null)
            {
                situation.HAPPEND_TIME = vm.HappenedTime;
                situation.HAPPEND_TIME_DETAIL = vm.HappenedTimeDetail;
                situation.HAPPEND_PLACE = vm.HappenedPlace;
                situation.CHARACTER_FROM = vm.CharacterFrom;
                situation.CHARACTER_TO = vm.CharacterTo;
                situation.PROPOSAL_OBJECT = vm.ProposalObject;
                situation.APPROACH = vm.Approach;
                situation.OTHER_INFO = vm.OtherBackgroundInfo;
                situation.UPDATED_AT = Now;
            }
        }

        public void UpdateAutoThoughtList(List<ViewModels.AutoThought> vmList, int mainThoughtIndex)
        {
            // 既存自動思考を辞書化
            var existing = Root.AUTO_THOUGHTs.ToDictionary(x => x.ID);

            // 画面側ID一覧
            var vmIds = vmList
                .Where(x => x.Id.HasValue)
                .Select(x => x.Id.Value)
                .ToList();
        }

        public void UpdateAutoThought(ViewModels.AutoThought vm)
        {

        }

        public void UpdateEvidence(ViewModels.Evidence vm)
        {
            EVIDENCE? evidence = Root.AUTO_THOUGHTs.FirstOrDefault().EVIDENCEs.FirstOrDefault();

            // 対応するEVIDENCEが存在する場合の処理
            if (evidence != null)
            {
                // UPDATE
            }
            else
            {
                // CREATE
            }
        }

        /// <summary>
        /// SEVEN_COLUMNSのUPDATE処理を実行（ナビゲーションプロパティを利用）
        /// </summary>
        public async Task UpdateAsync(AppDbContext dbContext)
        {
            using var tx = await dbContext.Database.BeginTransactionAsync();
            try
            {
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
