using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class Form
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Plid { get; set; }
        public string? Content { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ProposalLetter? Pl { get; set; }
    }
}
