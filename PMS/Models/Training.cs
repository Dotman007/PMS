
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Training
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Key]
        public int TrainingId { get; set; }

        public int TrainingTypeId  { get; set; }
        public virtual TrainingType TrainingType { get; set; }

        public string Duration { get; set; }
        public bool Trained { get; set; }

        //public DateTime RegistrationDate { get; set; }

        //public DateTime EndDate { get; set; }

        public int? PrisonerId { get; set; }

        public virtual Prisoner Prisoner { get; set; }

        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}