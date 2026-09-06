using CBT_Practice.Data;
using CBT_Practice.Helpers;
using CBT_Practice.Models.Service;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step02_AutoThoughtsModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        [BindProperty]
        public List<AutoThought> AutoThoughtList { get; set; } = new ();

        [BindProperty]
        public int MainThoughtIndex { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public long? SevenColumnsId { get; set; }

        /// <summary>
        /// 一時保存用タイトル
        /// </summary>
        [BindProperty]
        public string? Title { get; set; }

        /// <summary>
        /// 自動思考部の初期表示件数
        /// </summary>
        public int DefaultAutoThoughtCount { get; set; } = 2;

        /// <summary>
        /// 感情入力部の初期表示件数
        /// </summary>
        public int DefaultEmotionCount { get; set; } = 3;

        /// <summary>
        /// 自動思考管理用Index
        /// </summary>
        public int AutoThoughtIndex { get; set; } = 1;

        public Step02_AutoThoughtsModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            // すでに Session があれば読み込む（戻るボタン押下時）
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession");
            if (sessionData?.AutoThoughtList != null)
            {
                // セッションデータを画面Modelに格納
                SevenColumnsId = sessionData.SevensColumnsID;
                AutoThoughtList = sessionData.AutoThoughtList;
                MainThoughtIndex = sessionData.MainThoughtIndex;

                // 初期化件数を更新
                DefaultAutoThoughtCount = AutoThoughtList.Count;
            }
            // Sessionがない初期表示の場合、リストの内容を初期化
            else
            {
                for(int i = 0; i < DefaultAutoThoughtCount; i++)
                {
                    var NewEmotionList = new List<Emotion>();
                    for(int j = 0; j < DefaultEmotionCount; j++)
                    {
                        NewEmotionList.Add(new Emotion());
                    }

                    AutoThoughtList.Add(new AutoThought { EmotionList = NewEmotionList });
                }
            }
        }

        public IActionResult OnPost()
        {
            // Session から取得
            var sessionData =
                HttpContext.Session.GetObject<CbtSession>("CbtSession")
                ?? new();

            // Step02 の内容を保存
            sessionData.AutoThoughtList = AutoThoughtList;
            sessionData.MainThoughtIndex = MainThoughtIndex;

            // Session に保存
            HttpContext.Session.SetObject("CbtSession", sessionData);
            return RedirectToPage("Step03_Evidence");
        }

        /// <summary>
        /// 一時保存ボタン押下時処理
        /// </summary>
        public async Task<IActionResult> OnPostSave()
        {
            // セッションの定義
            var session = HttpContext.Session.GetObject<CbtSession>("CbtSession") ?? new();
            session.AutoThoughtList = this.AutoThoughtList;
            session.MainThoughtIndex = this.MainThoughtIndex;
            session.Title = this.Title;

            if (session.IsEdit) 
            {
                var root = _dbContext.SEVEN_COLUMNs
                .Include(x => x.SITUATIONs)
                .Include(x => x.AUTO_THOUGHTs)
                    .ThenInclude(x => x.AUTO_THOUGHT_EMOTIONs)
                .Include(x => x.AUTO_THOUGHTs)
                    .ThenInclude(x => x.EVIDENCEs)
                .Include(x => x.AUTO_THOUGHTs)
                    .ThenInclude(x => x.ADAPTIVE_THOUGHTs)
                    .ThenInclude(x => x.ADAPTIVE_THOUGHT_EMOTIONs)
                .FirstOrDefault(x => x.ID == SevenColumnsId);

                if (root != null)
                {
                    // 更新処理を実施
                    var updateAggregate = new SevenColumnsUpdateAggregate(root);
                    await updateAggregate.UpdateAsync(_dbContext, session);
                }
            }
            else
            {
                // 登録処理を実施
                var createAggregate = new SevenColumnsCreateAggregate();
                createAggregate.ApplyFromSession(session);
                await createAggregate.CreateAsync(_dbContext);
            }
                return RedirectToPage("Index");
        }
    }
}
