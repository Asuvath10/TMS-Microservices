using System;
using System.Collections.Generic;

namespace PLManagement.Models
{
    public partial class ProposalApproval
    {
        public int Id { get; set; }
        public int Plid { get; set; }
        public byte[] ApproverSign { get; set; }
        public byte[] Pdf { get; set; }
        public DateTime? ApprovedOn { get; set; }

        public virtual ProposalLetter Pl { get; set; }
    }
}
