//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddressBook
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactEmail
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string EmailAddress { get; set; }
    
        public virtual Contact Contact { get; set; }
    }
}
