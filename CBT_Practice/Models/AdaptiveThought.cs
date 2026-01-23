namespace CBT_Practice.Models
{
    public class AdaptiveThought
    {
        /// <summary>
        /// 適応的思考：前半
        /// </summary>
        public string? BeforeThought { get; set; }

        /// <summary>
        /// 適応的思考：接続詞
        /// </summary>
        public string? Conjunction { get; set; }

        /// <summary>
        /// 適応的思考：後半
        /// </summary>
        public string? AfterThought { get; set; }


        public List<Emotion> EmotionList { get; set; } = new();
    }
}
