using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PLManagement.Models
{
    public partial class ProposalApproval
    {
        public int Id { get; set; }
        public int Plid { get; set; }
        public string ApproverSign { get; set; }
        public string Pdf { get; set; }
        public DateTime? ApprovedOn { get; set; }
        [JsonIgnore]
        public virtual ProposalLetter Pl { get; set; }
    }
}
