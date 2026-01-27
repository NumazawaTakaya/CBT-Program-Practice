using CBT_Practice.Helpers;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step04_CounterEvidenceModel : PageModel
    {
        [BindProperty]
        public CounterEvidence CounterEvidence { get; set; } = new();

        /// <summary>
        /// 自動思考の根拠
        /// </summary>
        [BindProperty]
        public string? AutoThoughtEvidence { get; set; }

        public void OnGet()
        {
            // すでに Session があれば読み込む（戻るボタン押下時）
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession");
            if (sessionData?.Evidence != null)
            {
                // セッションデータから前画面の情報を取得
                AutoThoughtEvidence = sessionData.Evidence.AutoThoughtEvidence;
            }

            if(sessionData?.CounterEvidence != null)
            {
                // セッションデータから当画面の情報を取得
                CounterEvidence = sessionData.CounterEvidence;
            }
        }


        public IActionResult OnPost()
        {
            // Session から取得
            var sessionData =
                HttpContext.Session.GetObject<CbtSession>("CbtSession")
                ?? new();

            // Step04 の内容を保存
            sessionData.CounterEvidence = CounterEvidence;

            // Session に保存
            HttpContext.Session.SetObject("CbtSession", sessionData);
            return RedirectToPage("Step05_AdaptiveThoughts");
        }
    }
}
