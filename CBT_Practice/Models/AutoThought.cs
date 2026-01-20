namespace CBT_Practice.Models
{
    public class AutoThought
    {
        /// <summary>
        /// </summary>
        public List<string>? AutoThoughtList { get; set; }
        
        /// <summary>
        /// 自動思考
        /// </summary>
        public string? OneAutoThought { get; set; }

        /// <summary>
        /// 自動思考が湧いた際の感情リスト
        /// </summary>
        public List<string>? EmotionList { get; set; }

        /// <summary>
        /// 各感情の点数
        /// </summary>
        public List<string>? EmotionPointList { get; set; }


        public AutoThought()
        {
            AutoThoughtList = new();
            EmotionList = new();
            EmotionPointList = new();
        }
    }
}
