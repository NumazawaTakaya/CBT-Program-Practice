using CBT_Practice.Helpers;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step03_EvidenceModel : PageModel
    {
        [BindProperty]
        public Evidence Evidence { get; set; } = new ();

        [BindProperty]
        public string? MainAutoThought { get; set; }

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

            if(sessionData?.Evidence != null)
            {
                // セッションデータを画面Modelに格納
                Evidence = sessionData.Evidence;
            }
        }

        public IActionResult OnPost()
        {
            // Session から取得
            var sessionData =
                HttpContext.Session.GetObject<CbtSession>("CbtSession")
                ?? new();

            // Step03 の内容を保存
            sessionData.Evidence = Evidence;
            HttpContext.Session.SetObject("CbtSession", sessionData);

            return RedirectToPage("Step04_CounterEvidence");
        }
    }
}
