using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string NameOfCompany { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string AccountHolder { get; set; }
        public string Bank { get; set; }
        public string IBAN { get; set; }
        public string BICCode { get; set; }
        public string GroupIds { get; set; }
        public string SubGroupId { get; set; }
        public string LEINumber { get; set; }
        public string FurtherField4 { get; set; }
        public int Salutation { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FurtherField1 { get; set; }
        public string FurtherField2 { get; set; }
        public string FurtherField3 { get; set; }
        public string UserTypeId { get; set; }
        public string RatingAgentur1 { get; set; }
        public string RatingAgenturValue1 { get; set; }
        public string RatingAgentur2 { get; set; }
        public string RatingAgenturValue2 { get; set; }
        public int DepositInsurance { get; set; }
        public int ClientGroupId { get; set; }
        public bool AgreeToThePrivacyPolicy { get; set; }
        public bool AgreeToTheRatingsMayPublish { get; set; }
        public bool AgreeThatInformationOfCompanyMayBePublished { get; set; }
        public bool AcceptAGBS { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int ModifiedBy { get; set; }
        public string NewPassword { get; set; }

    }
}