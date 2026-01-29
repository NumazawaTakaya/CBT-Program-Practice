using CBT_Practice.Data;
using CBT_Practice.Models.Entities;
using CBT_Practice.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Models.Service
{
    public class SevenColumnsService
    {
        public SEVEN_COLUMN Root {  get; private set; }

        public SITUATION? SITUATION { get; private set; }

        public List<AUTO_THOUGHT?> AUTO_THOUGHT_LIST { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SevenColumnsService(DateTime createTime)
        {
            Root = new SEVEN_COLUMN
            {
                CREATED_DAY = createTime,
                IS_COMPLETE = false,
                IS_DELETE = false,
            };
        }

        /// <summary>
        /// タイトル設定
        /// </summary>
        public void SetTitle(string title)
        {
            Root.TITLE = title;
        }

        /// <summary>
        /// SITUATION型Entityの作成
        /// </summary>
        public void SetSituation(ViewModels.Situation vm, DateTime createTime)
        {
            var situation = new SITUATION
            {
                HAPPEND_TIME = vm.HappenedTime,
                HAPPEND_TIME_DETAIL = vm.HappenedTimeDetail,
                HAPPEND_PLACE = vm.HappenedPlace,
                CHARACTER_FROM = vm.CharacterFrom,
                CHARACTER_TO = vm.CharacterTo,
                PROPOSAL_OBJECT = vm.ProposalObject,
                APPROACH = vm.Approach,
                OTHER_INFO = vm.OtherBackgroundInfo,
                CREATED_AT = createTime,

                SEVEN_COLUMNS = Root
            };

            Root.SITUATIONs.Add(situation);
        }

        /// <summary>
        /// AUTO_THOUGH型Entityの作成
        /// </summary>
        public void SetAutoThoughtList(List<ViewModels.AutoThought> vmList
            , int mainThoughtIndex
            , DateTime createTime
            , ViewModels.Evidence vm1
            , ViewModels.CounterEvidence vm2)
        {
        }

        /// <summary>
        /// EVIDENCE型Entityの作成
        /// </summary>
        public void SetEvidence(ViewModels.Evidence vm1, ViewModels.CounterEvidence vm2)
        {

        }

        /// <summary>
        /// SEVEN_COLUMNSのCREATE処理を実行（ナビゲーションプロパティを利用）
        /// </summary>
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
