namespace CBT_Practice.Models.ViewModels
{
    public class Situation
    {
        /// <summary>
        /// 自動思考の発生時期
        /// </summary>
        public DateTime HappenedTime { get; set; }

        /// <summary>
        /// 自動思考の発生時期_詳細
        /// </summary>
        public string? HappenedTimeDetail { get; set; }

        /// <summary>
        /// 自動思考が発生した場所
        /// </summary>
        public string? HappenedPlace { get; set; }

        /// <summary>
        /// 自動思考が発生した際にいた人（～が）
        /// </summary>
        public string? CharacterFrom { get; set; }

        /// <summary>
        /// 自動思考が発生した際にいた人（～に）
        /// </summary>
        public string? CharacterTo { get; set; }

        /// <summary>
        /// 行動を起こした目的
        /// </summary>
        public string? ProposalObject { get; set; }

        /// <summary>
        /// 行動をとった方法
        /// </summary>
        public string? Approach { get; set; }

        /// <summary>
        /// その他の背景情報
        /// </summary>
        public string? OtherBackgroundInfo { get; set; }
    }
}
