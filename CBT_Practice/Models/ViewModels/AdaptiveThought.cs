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

        /// <summary>
        /// 入力済み適応的思考のリスト化
        /// </summary>
        public ADAPTIVE_THOUGHT getAdaptiveThoughtEntity(DateTime createTime)
        {
            var adaptiveThoughtEntity = new ADAPTIVE_THOUGHT
            {
                BEFORE_THOUGHT = BeforeThought,
                CONJUNCTION_THOUGHT = Conjunction,
                AFTER_THOUGHT = AfterThought,
                CREATED_AT = createTime
            };

            setEmotionEntityList(adaptiveThoughtEntity, createTime);
            
            return adaptiveThoughtEntity;
        }

        /// <summary>
        /// 対応する感情リストのEntity化
        /// </summary>
        private void setEmotionEntityList(ADAPTIVE_THOUGHT adaptiveThoughtEntity, DateTime createTime)
        {
            foreach (var emotion in EmotionList)
            {
                var emotionEntity = new ADAPTIVE_THOUGHT_EMOTION
                {
                    EMOTION = emotion.Name,
                    POINT = emotion.Point,
                    CREATED_AT = createTime,
                    ADAPTIVE_THOUGHTS = adaptiveThoughtEntity,
                };

                adaptiveThoughtEntity.ADAPTIVE_THOUGHT_EMOTIONs.Add(emotionEntity);
            }
        }
    }
}
