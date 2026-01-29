using CBT_Practice.Data;
using CBT_Practice.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        public List<SevenColumnsIndex> SevenColumnsList { get; set; } = new();

        public IndexModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            SevenColumnsList = SevenColumnsIndex.getSevenColumnsList(_dbContext);
        }
    }
}
