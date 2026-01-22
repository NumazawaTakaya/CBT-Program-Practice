namespace CBT_Practice.Models
{
    public class CbtSession
    {
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
    }
}
