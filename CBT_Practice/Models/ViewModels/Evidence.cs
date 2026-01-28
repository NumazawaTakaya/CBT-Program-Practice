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
        /// Evidence型Entityモデルの作成
        /// </summary>
        public EVIDENCE getEvidenceEntity(CounterEvidence counterEvidence, DateTime createTime)
        {
            return new EVIDENCE
            {
                EVIDENCE1 = AutoThoughtEvidence,
                INSIDE_BELIEF = InsideBelief,
                CORE_BELIEF = CoreBelief,
                COUNTER_EVIDENCE = counterEvidence.Counter,
                CREATED_AT = createTime
            };
        }
    }
}
