﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EduZoneDBEntities : DbContext
    {
        public EduZoneDBEntities()
            : base("name=EduZoneDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<STEM> STEMs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<MoneyList> MoneyLists { get; set; }
        public virtual DbSet<AgeRange> AgeRanges { get; set; }
        public virtual DbSet<GameCoding> GameCodings { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<StudentCours> StudentCourses { get; set; }
        public virtual DbSet<ToyReservationHistory> ToyReservationHistories { get; set; }
    }
}
