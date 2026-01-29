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

        public List<AUTO_THOUGHT>? AUTO_THOUGHT_LIST { get; private set; } = new();

        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SevenColumnsService()
        {
            CreateTime = DateTime.Now;

            Root = new SEVEN_COLUMN
            {
                CREATED_DAY = CreateTime,
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
        /// セッションの内容を基にEntityクラスの内容を設定
        /// </summary>
        public void CreateEntitiesFromSession(CbtSession session)
        {
            // タイトルを設定
            if (session.Title != null) 
            { 
                SetTitle(session.Title);
            }
        }

        /// <summary>
        /// SITUATION型Entityの作成
        /// </summary>
        public void SetSituation(ViewModels.Situation vm)
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
                CREATED_AT = CreateTime,

                SEVEN_COLUMNS = Root
            };

            Root.SITUATIONs.Add(situation);
        }

        /// <summary>
        /// AUTO_THOUGH型Entityの作成
        /// </summary>
        public void SetAutoThoughtList(List<ViewModels.AutoThought> vmList
            , int mainThoughtIndex)
        {
            for (int i = 0; i < vmList.Count; i++) 
            {
                var autoThoughtEntity = new AUTO_THOUGHT
                {
                    AUTO_THOUGHT1 = vmList[i].Thought ?? "(未設定)",
                    IS_MAIN = i == mainThoughtIndex,
                    CREATED_AT = CreateTime,
                    SEVEN_COLUMNS = Root
                };
            }
        }

        /// <summary>
        /// EMOTION型Entityの作成
        /// </summary>
        public void SetEmotionEntityList(List<ViewModels.Emotion> vmList)
        {
            var mainAutoThought = AUTO_THOUGHT_LIST.FirstOrDefault(at => at.IS_MAIN);
            if (mainAutoThought == null) return;

            foreach (var emotion in vmList)
            {
                var emotionEntity = new AUTO_THOUGHT_EMOTION
                {
                    EMOTION = emotion.Name ?? "(未設定)",
                    POINT = emotion.Point,
                    CREATED_AT = CreateTime,
                    AUTO_THOUGHTS = mainAutoThought
                };

                mainAutoThought.AUTO_THOUGHT_EMOTIONs.Add(emotionEntity);
            }
        }

        /// <summary>
        /// EVIDENCE型Entityの作成
        /// （Step03まで作成済みの場合に利用）
        /// </summary>
        public void SetEvidence(ViewModels.Evidence vm1)
        {
            var mainAutoThought = AUTO_THOUGHT_LIST.FirstOrDefault(at => at.IS_MAIN);
            if (mainAutoThought == null) return;

            var evidence = new EVIDENCE
            {
                EVIDENCE1 = vm1.AutoThoughtEvidence ?? "(未設定)",
                INSIDE_BELIEF = vm1.InsideBelief,
                CORE_BELIEF = vm1.CoreBelief,
                CREATED_AT = CreateTime,
                THOUGHTS = mainAutoThought
            };
            mainAutoThought.EVIDENCEs.Add(evidence);
        }

        /// <summary>
        /// EVIDENCE型Entityの作成
        /// （Step04まで作成済みの場合に利用）
        /// </summary>
        public void SetEvidence(ViewModels.Evidence vm1, ViewModels.CounterEvidence vm2)
        {
            var mainAutoThought = AUTO_THOUGHT_LIST.FirstOrDefault(at => at.IS_MAIN);
            if (mainAutoThought == null) return;

            var evidence = new EVIDENCE
            {
                EVIDENCE1 = vm1.AutoThoughtEvidence ?? "(未設定)",
                INSIDE_BELIEF = vm1.InsideBelief,
                CORE_BELIEF = vm1.CoreBelief,
                COUNTER_EVIDENCE = vm2.Counter,
                CREATED_AT = CreateTime,
                THOUGHTS = mainAutoThought
            };
            mainAutoThought.EVIDENCEs.Add(evidence);
        }

        /// <summary>
        /// ADAPTIVE_THOUGHT型Entityの作成
        /// </summary>
        public void SetAdaptiveThought(ViewModels.AdaptiveThought vm)
        {
            var mainAutoThought = AUTO_THOUGHT_LIST.FirstOrDefault(at => at.IS_MAIN);
            if (mainAutoThought == null) return;

            var adaptiveThought = new ADAPTIVE_THOUGHT
            {
                BEFORE_THOUGHT = vm.BeforeThought ?? "(未設定)",
                CONJUNCTION_THOUGHT = vm.Conjunction ?? "(未設定)",
                AFTER_THOUGHT = vm.AfterThought ?? "(未設定)",
                CREATED_AT = CreateTime,
                THOUGHTS = mainAutoThought
            };
            mainAutoThought.ADAPTIVE_THOUGHTs.Add(adaptiveThought);
        }

        /// <summary>
        /// SEVEN_COLUMNSのCREATE処理を実行（ナビゲーションプロパティを利用）
        /// </summary>
        public async Task CreateAsync(AppDbContext dbContext)
        {
            using var tx = await dbContext.Database.BeginTransactionAsync();
            try
            {
                dbContext.SEVEN_COLUMNs.Add(Root);
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
