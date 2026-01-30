using CBT_Practice.Data;
using CBT_Practice.Helpers;
using CBT_Practice.Models.Service;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step03_EvidenceModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        [BindProperty]
        public Evidence Evidence { get; set; } = new ();

        [BindProperty]
        public string? MainAutoThought { get; set; }

        /// <summary>
        /// 一時保存用タイトル
        /// </summary>
        [BindProperty]
        public string? Title { get; set; }

        public Step03_EvidenceModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        /// <summary>
        /// 一時保存ボタン押下時処理
        /// </summary>
        public async Task<IActionResult> OnPostSave()
        {
            // セッションの定義
            var session = HttpContext.Session.GetObject<CbtSession>("CbtSession") ?? new();
            session.Evidence = this.Evidence;
            session.Title = this.Title;

            // 保存内容の定義
            var aggregate = new SevenColumnsService();
            aggregate.CreateEntitiesFromSession(session);

            // 保存処理を実施
            await aggregate.CreateAsync(_dbContext);
            return RedirectToPage("Index");
        }
    }
}
