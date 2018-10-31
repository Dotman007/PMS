using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Case
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Key]
        public int CaseId { get; set; }
        public string CaseName { get; set; }
        public string CaseDate { get; set; }

        public int? PrisonerId { get; set; }
        public virtual Prisoner Prisoner { get; set; }
        public string JudgeName { get; set; }
        public string CourtName { get; set; }

        public string CaseDescription { get; set; }

        public bool Scheduled { get; set; }
       
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

       

    }
}