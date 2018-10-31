using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PMS.Models
{
    public class Employee
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int QualificationId { get; set; }

        public virtual Qualification Qualification  { get; set; }

        public int EmployeeTypeId { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        [Phone(ErrorMessage = "Phone No format is Incorrect.")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNo { get; set; }


    }
}