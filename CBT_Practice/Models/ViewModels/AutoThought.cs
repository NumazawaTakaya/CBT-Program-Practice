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
        
        /// <summary>
        /// 入力済み自動思考リストのEntity化
        /// </summary>
        public static void setSevenColumnsEntity(SEVEN_COLUMN sevenColumnsModel
            ,EVIDENCE evidenceEntity
            ,ADAPTIVE_THOUGHT adaptiveThought
            ,List<AutoThought> autoThoughtList
            ,int mainThoughtIndex
            ,DateTime createTime)
        {
            for (int i = 0; i < autoThoughtList.Count; i++)
            {
                if (autoThoughtList[i].Thought != null)
                {
                    // AutoThought型Entityの作成
                    var autoThoughtEntity = new AUTO_THOUGHT
                    {
                        AUTO_THOUGHT1 = autoThoughtList[i].Thought,
                        IS_MAIN = i == mainThoughtIndex,
                        CREATED_AT = createTime,
                        SEVEN_COLUMNS = sevenColumnsModel
                    };

                    // Emotion型Entityの作成、紐づけ
                    autoThoughtList[i].setEmotionEntityList(autoThoughtEntity, createTime);

                    // Evidence型、AdaptiveThought型Entityの紐づけ
                    if(i == mainThoughtIndex)
                    {
                        autoThoughtEntity.EVIDENCEs.Add(evidenceEntity);
                        autoThoughtEntity.ADAPTIVE_THOUGHTs.Add(adaptiveThought);

                        evidenceEntity.THOUGHTS = autoThoughtEntity;
                        adaptiveThought.THOUGHTS = autoThoughtEntity;
                    }

                    // 親テーブルと紐づけ
                    sevenColumnsModel.AUTO_THOUGHTs.Add(autoThoughtEntity);
                }
            }
        }

        /// <summary>
        /// 対応する感情リストのEntity化
        /// </summary>
        public void setEmotionEntityList(AUTO_THOUGHT autoThoughtEntity,DateTime createTime)
        {
            foreach(var emotion in EmotionList)
            {
                var emotionEntity = new AUTO_THOUGHT_EMOTION
                {
                    EMOTION = emotion.Name,
                    POINT = emotion.Point,
                    CREATED_AT = createTime,
                    AUTO_THOUGHTS = autoThoughtEntity,
                };

                autoThoughtEntity.AUTO_THOUGHT_EMOTIONs.Add(emotionEntity);
            }
        }
    }
}
