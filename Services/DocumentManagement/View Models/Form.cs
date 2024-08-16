using System;
using System.Collections.Generic;

namespace DocumentManagement.Models
{
    public partial class Form
    {
        public int Id { get; set; }
        public int Plid { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ProposalLetter Pl { get; set; }
    }
}
