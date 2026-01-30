using CBT_Practice.Models.Entities;

namespace CBT_Practice.Models.ViewModels
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

        public static AdaptiveThought GetAdaptiveThought(ADAPTIVE_THOUGHT entity)
        {
            var adaptiveThoughtVm = new AdaptiveThought();
            adaptiveThoughtVm.BeforeThought = entity.BEFORE_THOUGHT;
            adaptiveThoughtVm.Conjunction = entity.CONJUNCTION_THOUGHT;
            adaptiveThoughtVm.AfterThought = entity.AFTER_THOUGHT;

            var emotionVmList = new List<Emotion>();
            foreach(var emotionEntity in entity.ADAPTIVE_THOUGHT_EMOTIONs)
            {
                emotionVmList.Add(Emotion.GetEmotion(emotionEntity));
            }
            adaptiveThoughtVm.EmotionList = emotionVmList;

            return adaptiveThoughtVm;
        }
    }
}
