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
    
    public partial class Groups_Tests
    {
        public int ID_Group { get; set; }
        public int ID_Tests { get; set; }
        public int ID { get; set; }
    
        public virtual Groups Groups { get; set; }
        public virtual Tests Tests { get; set; }
    }
}
