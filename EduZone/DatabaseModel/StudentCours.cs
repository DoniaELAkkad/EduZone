//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class StudentCours
    {
        public int Id { get; set; }
        public int studentId { get; set; }
        public int gameCodingId { get; set; }
        public int moduleId { get; set; }
    
        public virtual GameCoding GameCoding { get; set; }
        public virtual Module Module { get; set; }
        public virtual Student Student { get; set; }
    }
}
