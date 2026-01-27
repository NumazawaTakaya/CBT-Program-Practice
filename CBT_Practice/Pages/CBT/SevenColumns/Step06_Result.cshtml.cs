using CBT_Practice.Data;
using CBT_Practice.Helpers;
using CBT_Practice.Models.Entities;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step06_ResultModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        /// <summary>Step01_自動思考の発生状況</summary>
        [BindProperty]
        public Situation? Situation { get; set; }
        
        /// <summary>発生状況のまとめ</summary>
        public string? SituationString { get; set; }

        /// <summary>Step02_自動思考と感情</summary>
        [BindProperty]
        public AutoThought? MainAutoThought { get; set; }

        /// <summary>Step03_自動思考の根拠</summary>
        [BindProperty]
        public Evidence? Evidence { get; set; }

        /// <summary>Step04_反証</summary>
        [BindProperty]
        public CounterEvidence? CounterEvidence { get; set; }

        /// <summary>Step05_適応的思考</summary>
        [BindProperty]
        public AdaptiveThought? AdaptiveThoght { get; set; }

        /// <summary>適応的思考のまとめ</summary>
        public string? AdaptiveThoughtString { get; set; }

        /// <summary>自動思考発生時の感情件数</summary>
        public int PreviouceEmotionCount { get; set; } = 3;

        /// <summary>適応的思考到達時の感情件数</summary>
        public int PresentEmotionCount { get; set; } = 3;


        public void OnGet()
        {
            // Sessionの内容を取り込む
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession");

            // Step01 の内容取得
            if (sessionData?.Situation != null)
            {
                Situation = sessionData.Situation;
                SituationString =
                        $"{Situation.HappenedTime} の {Situation.HappenedTimeDetail} に\n"
                        + $"{Situation.HappenedPlace} で\n"
                        + $"{Situation.CharacterFrom} が {Situation.CharacterTo} に\n"
                        + $"{Situation.ProposalObject} を\n"
                        + $"{Situation.Approach}\n";
            }

            // Step02 の内容取得
            if (sessionData?.AutoThoughtList != null && sessionData?.MainThoughtIndex != null)
            {
                MainAutoThought = sessionData.AutoThoughtList[sessionData.MainThoughtIndex];
                PreviouceEmotionCount = MainAutoThought.EmotionList.Count;
            }

            // Step03 の内容取得
            if (sessionData?.Evidence != null)
            {
                Evidence = sessionData.Evidence;
            }

            // Step04 の内容取得
            if(sessionData?.CounterEvidence != null)
            {
                CounterEvidence = sessionData.CounterEvidence;
            }

            // Step05 の内容取得
            if (sessionData?.AdaptiveThought != null)
            {
                AdaptiveThoght = sessionData.AdaptiveThought;
                AdaptiveThoughtString =
                        $"{AdaptiveThoght.BeforeThought}\n" 
                        + $"{AdaptiveThoght.Conjunction}\n" 
                        + $"{AdaptiveThoght.AfterThought}";

                PresentEmotionCount = AdaptiveThoght.EmotionList.Count;
            }
        }

        /// <summary>
        /// 保存ボタン押下時処理
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            // 最新のIDを取得
            var latestRecord = _dbContext.SEVEN_COLUMNs
                                .OrderByDescending(x => x.ID)
                                .FirstOrDefault();
            long sevenColumnsId = 1;
            if (latestRecord != null) {
                sevenColumnsId = latestRecord.ID + 1;
            }
            
            /* 各Entityモデルの作成 */
            // 作成日時を定義
            DateTime createTime = DateTime.Now;

            // Situation型Entityモデルへの格納
            if(Situation != null)
            {
                SITUATION SITUATION = new SITUATION
                {
                    HAPPEND_TIME = Situation.HappenedTime,
                    HAPPEND_TIME_DETAIL = Situation.HappenedTimeDetail,
                    HAPPEND_PLACE = Situation.HappenedPlace,
                    CHARACTER_FROM = Situation.CharacterFrom,
                    CHARACTER_TO = Situation.CharacterTo,
                    PROPOSAL_OBJECT = Situation.ProposalObject,
                    APPROACH = Situation.Approach,
                    OTHER_INFO = Situation.OtherBackgroundInfo,
                    CREATED_AT = createTime
                };
            }

            // AutoThoughts型Entityモデルへの格納
            if (MainAutoThought != null)
            {
                AUTO_THOUGHT AUTO_THOUGHT = new AUTO_THOUGHT
                {

                }
            }

            return RedirectToPage("Index");
        }
    }
}
