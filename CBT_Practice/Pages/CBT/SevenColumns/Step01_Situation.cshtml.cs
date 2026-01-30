using CBT_Practice.Data;
using CBT_Practice.Helpers;
using CBT_Practice.Models.Entities;
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

        [BindProperty]
        public string? Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public long? SevenColumnsId { get; set; }

        public bool IsEdit => SevenColumnsId.HasValue;


        public Step01_SituationModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 画面読み込み時処理
        /// </summary>
        public async Task OnGetAsync()
        {
            // セッションを作成
            var sessionData = HttpContext.Session.GetObject<CbtSession>("CbtSession")
                ?? new CbtSession();

            // 編集データが存在する場合は取り込む
            if(IsEdit)
            {
                var aggregate = new SevenColumnsSelectAggregate(_dbContext, SevenColumnsId);
                var situationEntity = aggregate.GetSITUATION();
                var autoThoughtEntityList = aggregate.GetAUTO_THOUGHTs();

                // SITUATION型Entityをセッションに反映
                if (situationEntity != null)
                {
                    // Session に保存
                    sessionData.Situation = Situation.SetViewModel(situationEntity);
                    sessionData.IsEdit = this.IsEdit;
                }

                // AUTO_THOUGHT型Entityをセッションに反映
                if (autoThoughtEntityList != null)
                {
                    var autoThoughtList = new List<AutoThought>();
                    for (int i = 0; i < autoThoughtEntityList.Count; i++)
                    {
                        // AutoThought型ViewModeの取得
                        var autoThoughtEntity = autoThoughtEntityList[i];
                        var autoThoughtModel = AutoThought.GetAutoThought(autoThoughtEntity);
                        
                        // メインとなる自動思考のインデックスを定義
                        if (autoThoughtEntity.IS_MAIN)
                        {
                            sessionData.MainThoughtIndex = i;
                        }

                        // Evidence / CounterEvidence型ViewModelの取得
                        var evidenceEntity = autoThoughtEntity.EVIDENCEs.FirstOrDefault();
                        if (evidenceEntity != null)
                        {
                            sessionData.Evidence = Evidence.GetEvidence(evidenceEntity);
                            sessionData.CounterEvidence = CounterEvidence.GetCounterEvidence(evidenceEntity);
                        }

                        // 適応的思考の取得
                        var adaptiveThoughtEntity = autoThoughtEntity.ADAPTIVE_THOUGHTs.FirstOrDefault();
                        if(adaptiveThoughtEntity != null)
                        {
                            sessionData.AdaptiveThought = AdaptiveThought.GetAdaptiveThought(adaptiveThoughtEntity);
                        }
                    }
                }

                // 取得結果を保存
                HttpContext.Session.SetObject("CbtSession", sessionData);
            }
            
            // すでに Session があれば読み込む
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
        public async Task<IActionResult> OnPostTempSave()
        {
            // セッションの定義
            var session = HttpContext.Session.GetObject<CbtSession>("CbtSession");
            session.Situation = this.Situation;
            session.Title = this.Title;

            // 編集フラグに応じた処理を実施
            if (session.IsEdit)
            {
                var root = _dbContext.SEVEN_COLUMNs
                    .Include(x => x.SITUATIONs)
                    .Include(x => x.AUTO_THOUGHTs)
                    .FirstOrDefault(x => x.ID == SevenColumnsId);

                if(root != null)
                {
                    // 更新処理を実施
                    var updateAggregate = new SevenColumnsUpdateAggregate(root);
                    updateAggregate.ApplyFromSession(session);
                    await updateAggregate.UpdateAsync(_dbContext);
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
