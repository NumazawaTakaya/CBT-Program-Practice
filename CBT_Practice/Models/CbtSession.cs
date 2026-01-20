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
        public AutoThought? AutoThought { get; set; }
    }
}
