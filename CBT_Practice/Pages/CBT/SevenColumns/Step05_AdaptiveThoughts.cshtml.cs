using CBT_Practice.Helpers;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step05_AdaptiveThoughtsModel : PageModel
    {
        [BindProperty]
        public AdaptiveThought AdaptiveThought { get; set; } = new();

        [BindProperty]
        public string? MainAutoThought { get; set; }

        [BindProperty]
        public string? CounterEvidence { get; set; }

        /// <summary>
        /// 感情入力欄の初期表示数
        /// </summary>
        public int DefaultEmotionCount { get; set; } = 3;

        /// <summary>
        /// 接続詞の初期値
        /// </summary>
        public List<SelectListItem> ConjunctionList { get; } = new()
        {
            new SelectListItem { Value = "better", Text = "に越したことはないが" },
            new SelectListItem { Value = "maybe",  Text = "かもしれないが" }
        };

        public void OnGet()
        {
            // すでに Session があれば読み込む（戻るボタン押下時）
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession");
            if (sessionData?.AutoThoughtList != null && sessionData?.MainThoughtIndex != null)
            {
                // セッションデータを画面Modelに格納
                MainAutoThought = sessionData
                    .AutoThoughtList[sessionData.MainThoughtIndex]
                    .Thought;
            }

            if (sessionData?.CounterEvidence != null)
            {
                // セッションデータを画面Modelに格納
                CounterEvidence = sessionData.CounterEvidence.Counter;
            }

            if(sessionData?.AdaptiveThought != null)
            {
                // セッションデータを画面Modelに格納
                AdaptiveThought = sessionData.AdaptiveThought;
                DefaultEmotionCount = sessionData.AdaptiveThought.EmotionList.Count;
            }
            else
            {
                // セッションに画面Modelが格納されていない場合は感情リストを初期化
                var NewEmotionList = new List<Emotion>();
                for (int i = 0; i < DefaultEmotionCount; i++)
                {
                    NewEmotionList.Add(new Emotion());
                }
                AdaptiveThought.EmotionList = NewEmotionList;
            }
        }

        public IActionResult OnPost()
        {
            // Session から取得
            var sessionData =
                HttpContext.Session.GetObject<CbtSession>("CbtSession")
                ?? new();

            // Step05 の内容を保存
            sessionData.AdaptiveThought = AdaptiveThought;

            // Session に保存して次ページへ
            HttpContext.Session.SetObject("CbtSession", sessionData);
            return RedirectToPage("Step06_Result");
        }

    }
}
