using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Prisoner
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Key]
        public int PrisonerId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string  LastName { get; set; }
        [Required]
        public string OtherName { get; set; }

        public string Gender { get; set; }

        public int? QualificationId { get; set; }
        public virtual Qualification Qualification { get; set; }

        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public int? StateId { get; set; }
        public virtual State State { get; set; }

        public string StateName { get; set; }

        public string LGA { get; set; }
        public int? LocalGovId { get; set; }
        public virtual LocalGovt LocalGovt { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        [Phone(ErrorMessage = "Phone No format is Incorrect.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNo { get; set; }
        [Required]
        public string NextOfKin { get; set; }
        public string MotherName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string MotherAddress { get; set; }
        [Required]
        public string FatherAddress { get; set; }

        public string Genotype { get; set; }

        public string BloodGroup { get; set; }

        public bool CaseScheduled { get; set; }

        public int? CourtId { get; set; }
        public virtual Court Court { get; set; }
        public bool TrainingScheduled { get; set; }

        public string CaseDate { get; set; }

        public string JudgeName { get; set; }

       

        public int? TrainingTypeId { get; set; }
        public virtual TrainingType TrainingType { get; set; }
        public int? CrimeId { get; set; }
        public virtual Crime Crime { get; set; }


        public string PrisonerRegNo { get; set; }
        
        
    }
}