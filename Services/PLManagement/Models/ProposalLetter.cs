using System;
using System.Collections.Generic;

namespace PLManagement.Models
{
    public partial class ProposalLetter
    {
        public ProposalLetter()
        {
            Forms = new HashSet<Form>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PreparerId { get; set; }
        public int? ReviewerId { get; set; }
        public int? ApproverId { get; set; }
        public string AssessmentYear { get; set; }
        public int PlstatusId { get; set; }
        public bool? Draft { get; set; }
        public string ApproverSignUrl { get; set; }
        public string PdfUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<Form> Forms { get; set; }
    }
}
