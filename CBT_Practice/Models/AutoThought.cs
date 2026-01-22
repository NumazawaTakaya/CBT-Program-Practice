namespace CBT_Practice.Models
{
    public class AutoThought
    {       
        /// <summary>
        /// 自動思考
        /// </summary>
        public string? Thought { get; set; }

        /// <summary>
        /// 自動思考が湧いた際の感情リスト
        /// </summary>
        public List<Emotion> EmotionList { get; set; } = new();
    }

    public class Emotion
    {
        /// <summary>
        /// 感情名
        /// </summary>
        public string? Name {  get; set; }

        /// <summary>
        /// 感情の度合いを示す点数
        /// </summary>
        public int Point {  get; set; }
    }
}
