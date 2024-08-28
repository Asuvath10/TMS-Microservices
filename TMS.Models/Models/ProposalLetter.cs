using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class ProposalLetter
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public int? PreparerId { get; set; }
        public int? ReviewerId { get; set; }
        public int? ApproverId { get; set; }
        public string? AssessmentYear { get; set; }
        [Required]
        public int PlstatusId { get; set; }
        public bool? Draft { get; set; }
        public string? ApproverSignUrl { get; set; }
        public string? PdfUrl { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<Form>? Forms { get; set; }
        public virtual Plstatus? PLStatus { get; set; }
    }
}
