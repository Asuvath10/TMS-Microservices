using System;
using System.Collections.Generic;

namespace PLManagement.Models
{
    public partial class ProposalLetter
    {
        public ProposalLetter()
        {
            ProposalApprovals = new HashSet<ProposalApproval>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PreparerId { get; set; }
        public int? ReviewerId { get; set; }
        public int? ApproverId { get; set; }
        public string AssessmentYear { get; set; }
        public int PlstatusId { get; set; }
        public string Content { get; set; }
        public bool? Draft { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<ProposalApproval> ProposalApprovals { get; set; }
    }
}
