using CBT_Practice.Models.Entities;

namespace CBT_Practice.Models.ViewModels
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
        

        public static AutoThought GetAutoThought(AUTO_THOUGHT entity)
        {
            var autoThoughtVm = new AutoThought();
            autoThoughtVm.Thought = entity.AUTO_THOUGHT1;

            // 感情型ViewModelのリストを取得
            var emotionVmList = new List<Emotion>();
            foreach (var emotionEntity in entity.AUTO_THOUGHT_EMOTIONs)
            {
                emotionVmList.Add(Emotion.GetEmotion(emotionEntity));
            }
            autoThoughtVm.EmotionList = emotionVmList;

            return autoThoughtVm;
        }
    }
}
