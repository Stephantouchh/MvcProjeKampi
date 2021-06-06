﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Heading> Headings { get; set; }
        public virtual DbSet<Writer> Writers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<ImageFile> ImageFiles { get; set; }
    }
}
