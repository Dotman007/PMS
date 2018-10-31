using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class Visitor
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Key]
        public int VisitorId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }
        public string Gender { get; set; }


        public string  Relationship { get; set; }


        public string  PrisonerName  { get; set; }

        public string PrisonerRegistrationNo { get; set; }

        public int? PrisonerId { get; set; }

        public virtual Prisoner Prisoner { get; set; }



        
           }
}