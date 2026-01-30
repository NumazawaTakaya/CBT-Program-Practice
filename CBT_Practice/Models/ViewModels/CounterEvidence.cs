using CBT_Practice.Models.Entities;

namespace CBT_Practice.Models.ViewModels
{
    public class CounterEvidence
    {
        /// <summary>
        /// 反証
        /// </summary>
        public string? Counter { get; set; }

        
        public static CounterEvidence GetCounterEvidence(EVIDENCE entity)
        {
            return new CounterEvidence{
                Counter = entity.COUNTER_EVIDENCE
            };
        }
    }
}
