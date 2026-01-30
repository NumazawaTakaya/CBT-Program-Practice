using CBT_Practice.Data;
using CBT_Practice.Helpers;
using CBT_Practice.Models.Service;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step01_SituationModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        [BindProperty]
        public Situation Situation { get; set; } = new ();

        /// <summary>
        /// 一時保存用タイトル
        /// </summary>
        [BindProperty]
        public string? Title { get; set; }

        public Step01_SituationModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
            // セッションの定義
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession") ?? new();
            sessionData.Situation = Situation;

            // Session に保存
            HttpContext.Session.SetObject("CbtSession", sessionData);
            return RedirectToPage("Step02_AutoThoughts");
        }

        /// <summary>
        /// 一時保存ボタン押下時処理
        /// </summary>
        public async Task<IActionResult> OnPostSave()
        {
            // セッションの定義
            var session = HttpContext.Session.GetObject<CbtSession>("CbtSession") ?? new();
            session.Situation = this.Situation;
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
