using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PMS.Models;

namespace PMS.DAL
{
    public class PMSContext:DbContext
    {
        public PMSContext():base("PMSContext")
        {

        }
        public DbSet<Prisoner> Prisoners { get; set; }
       
        public DbSet<Court> Courts { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Crime> Crime { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<LocalGovt> LocalGovts { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
       
        public DbSet<Visitor> Visitors { get; set; }

    }
}