﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Writer
    {
        [Key]
        public int WriterId { get; set; }

        [StringLength(50)]
        public string WriterName { get; set; }

        [StringLength(50)]
        public string WriterSurName { get; set; }

        [StringLength(250)]
        public string WriterImage { get; set; }

        [StringLength(100)]
        public string WriterAbout { get; set; }

        [StringLength(200)]
        public string WriterMail { get; set; }

        [StringLength(200)]
        public string WriterPassword { get; set; }

        [StringLength(50)]
        public string WriterTitle { get; set; }

        public bool WriterStatus { get; set; }

        [StringLength(1)]
        public string WriterRole { get; set; }

        public virtual ICollection<Heading> Headings { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}
