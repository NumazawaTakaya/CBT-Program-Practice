using CBT_Practice.Data;

namespace CBT_Practice.Models.ViewModels
{
    public class SevenColumnsIndex
    {

        /// <summary>
        /// 題名
        /// </summary>
        public string? SevenColumnsTitle {  get; set; }

        /// <summary>
        /// 自動思考
        /// </summary>
        public string? MainAutoThough {  get; set; }

        /// <summary>
        /// 主な感情
        /// </summary>
        public string? MainEmotion { get; set; }

        /// <summary>
        /// 発生日
        /// </summary>
        public DateTime? HappenedTime { get; set; }

        /// <summary>
        /// 完了状況
        /// </summary>
        public bool IsCompleted { get; set; }


        public static List<SevenColumnsIndex> getSevenColumnsList(AppDbContext context)
        {
            var list = context.SEVEN_COLUMNs
                 .Where(sc => !sc.IS_DELETE)
                 .Select(sc => new SevenColumnsIndex
                 {
                     // タイトルを取得（Step06_Result.cshtml にて入力）
                     SevenColumnsTitle = string.IsNullOrEmpty(sc.TITLE)
                        ? "未設定" : sc.TITLE,

                     // 自動思考を取得（Step02_AutoThoughts.cshtml にて入力）
                     MainAutoThough = sc.AUTO_THOUGHTs
                        .OrderBy(at => at.CREATED_AT)
                        .Select(at => at.AUTO_THOUGHT1)
                        .FirstOrDefault() ?? "",

                     // 主な感情（自動思考の中で最大の点数）
                     MainEmotion = sc.AUTO_THOUGHTs
                        .OrderBy(at => at.CREATED_AT)
                        .SelectMany(at => at.AUTO_THOUGHT_EMOTIONs)
                        .OrderByDescending(e => e.POINT)
                        .Select(e => e.EMOTION + "(" + e.POINT + ")")
                        .FirstOrDefault() ?? "",

                     // 発生日を取得（Step01_Situation.cshtml にて入力）
                     HappenedTime = sc.SITUATIONs
                        .OrderBy(s => s.CREATED_AT)
                        .Select(s => s.HAPPEND_TIME)
                        .FirstOrDefault(),

                     // 完了状況を取得（Step06_Result.cshtml で保存ボタン押下ならtrue）
                     IsCompleted = sc.IS_COMPLETE
                 })
                 .OrderBy(x => x.HappenedTime)
                 .ToList();

            return list;
        }
    }
}
