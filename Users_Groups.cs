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
    
    public partial class Users_Groups
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public int ID_Group { get; set; }
    
        public virtual Groups Groups { get; set; }
        public virtual Users Users { get; set; }
    }
}
