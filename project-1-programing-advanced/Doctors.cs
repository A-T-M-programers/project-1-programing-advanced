using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace project_1_programing_advanced
{
    [Table ("Doctors",Schema ="dbo")]
    public class Doctors
    {
        [Column("FirstN",Order =1),Required]
        public string FirstN { set; get; }
        [Column("LastN",Order =2),Required]
        public string LastN { set; get; }
        [Key]
        [Column(Order =0)]
        public int ID { set; get; }
        [Column("Age",Order =3)]
        public int Age { set; get; }
    }
    public class DContext : DbContext
    {
        public DbSet<Doctors> D { set; get; }

    }
}