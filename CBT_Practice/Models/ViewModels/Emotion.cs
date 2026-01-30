using CBT_Practice.Models.Entities;

namespace CBT_Practice.Models.ViewModels
{
    public class Emotion
    {
        /// <summary>
        /// 感情名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 感情の度合いを示す点数
        /// </summary>
        public int Point { get; set; }

        public static Emotion GetEmotion(AUTO_THOUGHT_EMOTION entity) 
        {
            return new Emotion()
            {
                Name = entity.EMOTION,
                Point = entity.POINT
            };
        }

        public static Emotion GetEmotion(ADAPTIVE_THOUGHT_EMOTION entity)
        {
            return new Emotion()
            {
                Name = entity.EMOTION,
                Point = entity.POINT
            };
        }
    }
}
