using CBT_Practice.Data;
using CBT_Practice.Helpers;
using CBT_Practice.Models.Service;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step04_CounterEvidenceModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        [BindProperty]
        public CounterEvidence CounterEvidence { get; set; } = new();

        /// <summary>
        /// 自動思考の根拠
        /// </summary>
        [BindProperty]
        public string? AutoThoughtEvidence { get; set; }

        /// <summary>
        /// 一時保存用タイトル
        /// </summary>
        [BindProperty]
        public string? Title { get; set; }

        public Step04_CounterEvidenceModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        /// <summary>
        /// 一時保存ボタン押下時処理
        /// </summary>
        public async Task<IActionResult> OnPostSave()
        {
            // セッションの定義
            var session = HttpContext.Session.GetObject<CbtSession>("CbtSession") ?? new();
            session.CounterEvidence = this.CounterEvidence;
            session.Title = this.Title;

            // 保存内容の定義
            var aggregate = new SevenColumnsCreateAggregate();
            aggregate.ApplyFromSession(session);

            // 保存処理を実施
            await aggregate.CreateAsync(_dbContext);
            return RedirectToPage("Index");
        }
    }
}
