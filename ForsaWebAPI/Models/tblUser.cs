//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ForsaWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUser
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string BankName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string LongTermRatingAgency { get; set; }
        public string LongTermRating { get; set; }
        public string ShortTermRatingAgency { get; set; }
        public string ShortTermRating { get; set; }
        public Nullable<bool> PromissaryNotesLenderOn { get; set; }
        public Nullable<bool> PromissaryNotesBorrower { get; set; }
        public Nullable<bool> MoneyMarket { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
}
