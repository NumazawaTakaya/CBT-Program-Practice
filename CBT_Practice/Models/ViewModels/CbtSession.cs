namespace CBT_Practice.Models.ViewModels
{
    public class CbtSession
    {
        /// <summary>
        /// 7つのコラムのタイトル
        /// </summary>
        public string? Title {  get; set; }

        /// <summary>
        /// Step01_Situation.cshtmlの入力項目
        /// </summary>
        public Situation? Situation { get; set; }

        /// <summary>
        /// Step02_AutoThought.cshtmlの入力項目
        /// </summary>
        public List<AutoThought>? AutoThoughtList { get; set; }

        /// <summary>
        /// Step02_AutoThought.cshtmlでメインに扱う自動思考番号
        /// </summary>
        public int MainThoughtIndex { get; set; }

        /// <summary>
        /// Step03_Evidence.cshtml の入力項目
        /// </summary>
        public Evidence? Evidence { get; set; }

        /// <summary>
        /// Step04_CounterEvidence.cshtml の入力項目
        /// </summary>
        public CounterEvidence? CounterEvidence { get; set; }

        /// <summary>
        /// Step05_AdaptiveThoughts.cshtml の入力項目
        /// </summary>
        public AdaptiveThought? AdaptiveThought { get; set; }
    }
}
