using CBT_Practice.Helpers;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step01_SituationModel : PageModel
    {
        [BindProperty]
        public Situation Situation { get; set; } = new ();

        /// <summary>
        /// 一時保存用タイトル
        /// </summary>
        [BindProperty]
        public string? Title { get; set; }

        /// <summary>
        /// 画面読み込み時処理
        /// </summary>
        public void OnGet()
        {
            // すでに Session があれば読み込む（戻るボタン押下時）
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession");
            if (sessionData?.Situation != null)
            {
                Situation = sessionData.Situation;
            }
        }

        /// <summary>
        /// submit時処理
        /// </summary>
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

        /// <summary>
        /// 一時保存ボタン押下時
        /// </summary>
        public IActionResult OnPostSave()
        {

            var title = Title;
            return RedirectToPage("Index");
        }
    }
}
