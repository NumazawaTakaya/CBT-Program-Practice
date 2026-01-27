using CBT_Practice.Data;
using CBT_Practice.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CBT_Practice.Pages
{
    public class DbTestPageModel : PageModel
    {
        private readonly AppDbContext _context;

        public DbTestPageModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string _content { get; set; } = "";

        public List<dbTest> DbTests { get; set; } = new();

        public void OnGet()
        {
            DbTests = _context.dbTests
                .OrderBy(x => x.CreatedAt)
                .ToList();
        }

        /// <summary>
        /// Inputに入力済みの文字列をDB登録
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostCreate()
        {
            if(_content != null)
            {
                var model = new dbTest
                {
                    Content = _content,
                    ChangeType = "Create",
                    CreatedAt = DateTime.Now
                };

                _context.dbTests.Add(model);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }

        /// <summary>
        /// 最新作成日のレコードをInput入力値で更新する
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostUpdate()
        {
            if(_content != null)
            {
                var model = _context.dbTests
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();

                // レコードが1件以上存在する場合にのみ処理を実行
                if (model != null)
                {
                    model.Content = _content;
                    model.ChangeType = "Updated";
                    _context.SaveChanges();
                }
            }
            return RedirectToPage();
        }

        /// <summary>
        /// 最新作成日のレコードを削除する
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPostDelete() { 
            var model = _context.dbTests
                .OrderByDescending(x =>  x.CreatedAt)
                .FirstOrDefault();

            // レコードが1件以上存在する場合にのみ削除実行
            if(model != null)
            {
                _context.dbTests.Remove(model);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
