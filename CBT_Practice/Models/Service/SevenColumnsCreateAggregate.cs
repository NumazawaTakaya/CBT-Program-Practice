using CBT_Practice.Data;
using CBT_Practice.Models.Entities;
using CBT_Practice.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace CBT_Practice.Models.Service
{
    public class SevenColumnsCreateAggregate : SevenColumnsBaseAggregate
    {
        public SITUATION? SITUATION { get; private set; }

        public List<AUTO_THOUGHT> AUTO_THOUGHT_LIST { get; private set; } = new();

        public ADAPTIVE_THOUGHT ADAPTIVE_THOUGHT { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SevenColumnsCreateAggregate()
        {
            Root = new SEVEN_COLUMN
            {
                CREATED_DAY = Now,
                IS_COMPLETE = false,
                IS_DELETE = false,
            };

            SITUATION = new();
            AUTO_THOUGHT_LIST = new();
            ADAPTIVE_THOUGHT = new();
        }

        /// <summary>
        /// セッションの内容を基にEntityクラスの内容を設定
        /// </summary>
        public void ApplyFromSession(CbtSession session, bool isComplete = false)
        {
            // 編集状況を更新
            Root.IS_COMPLETE = isComplete;

            // タイトルを設定
            if (session.Title != null) 
            {
                ApplyTitle(session.Title);
            }

            // SITUATIONを設定
            if(session.Situation != null)
            {
                SetSituation(session.Situation);
            }

            // AUTO_THOUGHTを設定
            if(session.AutoThoughtList != null) 
            {
                SetAutoThoughtList(session.AutoThoughtList, session.MainThoughtIndex);

                // AUTO_THOUGHT_EMOTIONを設定
                var mainEmotion = session.AutoThoughtList[session.MainThoughtIndex].EmotionList;

                // EVIDENCEを設定
                if (session.Evidence != null && session.CounterEvidence == null)
                {
                    SetEvidence(session.Evidence);
                }
                else if (session.Evidence != null && session.CounterEvidence != null)
                {
                    SetEvidence(session.Evidence, session.CounterEvidence);
                }

                // ADAPTIVE_THOUGHTを設定
                if(session.AdaptiveThought != null)
                {
                    SetAdaptiveThought(session.AdaptiveThought);
                }
            }
        }

        /// <summary>
        /// SITUATION型Entityの作成
        /// </summary>
        public void SetSituation(ViewModels.Situation vm)
        {
            SITUATION = new SITUATION
            {
                HAPPEND_TIME = vm.HappenedTime,
                HAPPEND_TIME_DETAIL = vm.HappenedTimeDetail,
                HAPPEND_PLACE = vm.HappenedPlace,
                CHARACTER_FROM = vm.CharacterFrom,
                CHARACTER_TO = vm.CharacterTo,
                PROPOSAL_OBJECT = vm.ProposalObject,
                APPROACH = vm.Approach,
                OTHER_INFO = vm.OtherBackgroundInfo,
                CREATED_AT = Now,

                SEVEN_COLUMNS = Root
            };

            Root.SITUATIONs.Add(SITUATION);
        }

        /// <summary>
        /// AUTO_THOUGHT型／AUTO_THOUGHT_EMOTION型Entityの作成
        /// </summary>
        public void SetAutoThoughtList(List<ViewModels.AutoThought> vmList
            , int mainThoughtIndex)
        {
            for (int i = 0; i < vmList.Count; i++) 
            {
                // AUTO_THOUGHT型
                var autoThoughtEntity = new AUTO_THOUGHT
                {
                    AUTO_THOUGHT1 = vmList[i].Thought ?? "(未設定)",
                    IS_MAIN = i == mainThoughtIndex,
                    CREATED_AT = Now,
                    SEVEN_COLUMNS = Root
                };
                AUTO_THOUGHT_LIST.Add(autoThoughtEntity);

                // EMOTION型
                SetAutoThoughtEmotionList(vmList[i], autoThoughtEntity);

                // Rootとの紐づけ
                Root.AUTO_THOUGHTs.Add(autoThoughtEntity);

            }
        }

        /// <summary>
        /// EMOTION型Entityの作成
        /// </summary>
        public void SetAutoThoughtEmotionList(ViewModels.AutoThought vm, AUTO_THOUGHT entity)
        {
            foreach (var emotion in vm.EmotionList)
            {
                var emotionEntity = new AUTO_THOUGHT_EMOTION
                {
                    EMOTION = emotion.Name ?? "(未設定)",
                    POINT = emotion.Point,
                    CREATED_AT = Now,
                    AUTO_THOUGHTS = entity,
                };

                entity.AUTO_THOUGHT_EMOTIONs.Add(emotionEntity);
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
                CREATED_AT = Now,
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
                CREATED_AT = Now,
                THOUGHTS = mainAutoThought
            };
            mainAutoThought.EVIDENCEs.Add(evidence);
        }

        /// <summary>
        /// ADAPTIVE_THOUGHT／ADAPTIVE_THOUGHT_EMOTION型Entityの作成
        /// </summary>
        public void SetAdaptiveThought(ViewModels.AdaptiveThought vm)
        {
            var mainAutoThought = AUTO_THOUGHT_LIST.FirstOrDefault(at => at.IS_MAIN);
            if (mainAutoThought == null) return;

            // ADAPTIVE_THOUGHT型
            ADAPTIVE_THOUGHT = new ADAPTIVE_THOUGHT
            {
                BEFORE_THOUGHT = vm.BeforeThought ?? "(未設定)",
                CONJUNCTION_THOUGHT = vm.Conjunction ?? "(未設定)",
                AFTER_THOUGHT = vm.AfterThought ?? "(未設定)",
                CREATED_AT = Now,
                THOUGHTS = mainAutoThought
            };
            mainAutoThought.ADAPTIVE_THOUGHTs.Add(ADAPTIVE_THOUGHT);

            SetAdaptiveThoughtEmotionList(vm);
        }

        /// <summary>
        /// ADAPTIVE_THOUGHT_EMOTION型Entityの作成
        /// </summary>
        public void SetAdaptiveThoughtEmotionList(ViewModels.AdaptiveThought vm)
        {
            foreach(var emotion in vm.EmotionList)
            {
                var emotionEntity = new ADAPTIVE_THOUGHT_EMOTION
                {
                    EMOTION = emotion.Name ?? "(未設定)",
                    POINT = emotion.Point,
                    CREATED_AT = Now,
                    ADAPTIVE_THOUGHTS = ADAPTIVE_THOUGHT
                };

                ADAPTIVE_THOUGHT.ADAPTIVE_THOUGHT_EMOTIONs.Add(emotionEntity);
            }
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
