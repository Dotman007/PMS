using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PMS.Models
{
    public class LocalGovt
    {
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [Key]
        public int LocalGovtId { get; set; }
        public string LocalGovtName { get; set; }

        public int? StateId { get; set; }

        public virtual State State { get; set; }
    }
}