using CBT_Practice.Helpers;
using CBT_Practice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step01_SituationModel : PageModel
    {
        [BindProperty]
        public Situation Situation { get; set; } = new Situation();

        public void OnGet()
        {
            // すでに Session があれば読み込む（戻るボタン押下時）
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession");
            if (sessionData?.Situation != null)
            {
                Situation = sessionData.Situation;
            }
        }

        public IActionResult OnPost()
        {
            // Session から取得 or 新規作成
            var sessionData =
                HttpContext.Session.GetObject<CbtSession>("CbtSession")
                ?? new CbtSession();

            // Step01 の内容を保存
            sessionData.Situation = Situation;

            // Session に保存
            HttpContext.Session.SetObject("CbtSession", sessionData);
            return RedirectToPage("Step02_AutoThoughts");
        }
    }
}
