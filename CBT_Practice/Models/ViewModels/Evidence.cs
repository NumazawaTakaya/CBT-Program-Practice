using CBT_Practice.Models.Entities;

namespace CBT_Practice.Models.ViewModels
{
    public class Evidence
    {
        /// <summary>
        /// 自動思考の根拠
        /// </summary>
        public string? AutoThoughtEvidence { get; set; }

        /// <summary>
        /// 中間信念
        /// </summary>
        public string? InsideBelief { get; set; }

        /// <summary>
        /// 中核信念
        /// </summary>
        public string? CoreBelief { get; set; }

        /// <summary>
        /// VMの作成
        /// </summary>
        public static Evidence GetEvidence(EVIDENCE entity)
        {
            return new Evidence
            {
                AutoThoughtEvidence = entity.EVIDENCE1,
                InsideBelief = entity.INSIDE_BELIEF,
                CoreBelief = entity.CORE_BELIEF,
            };
        }
    }
}
