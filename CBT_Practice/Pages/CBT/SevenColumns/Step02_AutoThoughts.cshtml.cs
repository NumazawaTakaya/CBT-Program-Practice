using CBT_Practice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBT_Practice.Pages.CBT.SevenColumns
{
    public class Step02_AutoThoughtsModel : PageModel
    {
        [BindProperty]
        public AutoThought AutoThought { get; set; } = new AutoThought();

        public void OnGet()
        {

        }
    }
}
