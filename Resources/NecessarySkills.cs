//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DefaultWPF.Resources
{
    using System;
    using System.Collections.Generic;
    
    public partial class NecessarySkills
    {
        public int Id { get; set; }
        public int SkillId { get; set; }
        public int InternshirId { get; set; }
    
        public virtual Internships Internships { get; set; }
        public virtual Skills Skills { get; set; }
    }
}