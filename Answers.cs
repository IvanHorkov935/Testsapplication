//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tests_application
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answers
    {
        public int ID { get; set; }
        public int ID_Question { get; set; }
        public string Correctness { get; set; }
        public string Contents { get; set; }
    
        public virtual Questions Questions { get; set; }
    }
}
