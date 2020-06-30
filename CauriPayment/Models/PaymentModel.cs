using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CauriPayment.Models
{
    public class CreateUser
    {
        //[Required]
        [Display(Name = "project")]
        public string project { get; set; }

        [Required]
        [Display(Name = "identifier")]
        public string identifier { get; set; }

        [Required]
        [Display(Name = "display_name")]
        public string display_name { get; set; }

        [Required]
        [Display(Name = "email")]
        [RegularExpression(@"([a-z0-9_.-]+)@([a-z0-9_.-]+)\.([a-z.]{2,6})")]
        public string email { get; set; }

        [Required]
        [Display(Name = "phone")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "locale")]
        public string locale { get; set; }

        [Required]
        [Display(Name = "ip")]
        [RegularExpression(@"(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)")]
        public string ip { get; set; }

        [Required]
        [Display(Name = "signature")]
        public string signature { get; set; }
    }
    public class UserResponse
    {
        public int id { get; set; }
    }
    public class CreateToken
    {
        //[Required]
        [Display(Name = "project")]
        public string project { get; set; }

        [Required]
        [Display(Name = "number")]
        public string number { get; set; }
       
        [Required]
        [Display(Name = "expiration_month")]
        public string expiration_month { get; set; }

        [Required]
        [Display(Name = "expiration_year")]
        public string expiration_year { get; set; }

        [Required]
        [Display(Name = "security_code")]
        public string security_code { get; set; }
    }
    public class TokenResult
    {        
        public string id { get; set; }
        public string expiresAt { get; set; }
        public Card card { get; set; }

    }

    public class PaymentResult
    {
        public string id { get; set; }
        public bool success { get; set; }
        public Card card { get; set; }
        public Acs acs{ get; set; }
        public Recurring recurring { get; set; }
    }

    public class Refund
    {
        public string id { get; set; }
        public string amount { get; set; }
        public string order_id { get; set; }
        public string comment { get; set; }
    }

    public class RefundResult
    {
        public int id { get; set; }
        public bool success { get; set; }
    }
    public class Reverse
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string comment { get; set; }
    }
    public class ReverseResult
    {
        public int id { get; set; }
        public bool success { get; set; }
    }

    public class PaymentTransaction
    {
        public string project { get; set; }

        [Required]
        [Display(Name = "order_id")]
        public string order_id { get; set; }

        [Required]
        [Display(Name = "description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "user")]
        public string user { get; set; }

        [Required]
        [Display(Name = "card_token")]
        public string card_token { get; set; }

        [Required]
        [Display(Name = "price")]
        public string price { get; set; }

        [Required]
        [Display(Name = "currency")]
        public string currency { get; set; }

        [Display(Name = "acs_return_url")]
        public string acs_return_url { get; set; }

        [Display(Name = "recurring")]
        public string recurring { get; set; }

        [Display(Name = "recurring_interval")]
        public string recurring_interval { get; set; }

        [Display(Name = "attr_server")]
        public string attr_server { get; set; }

        [Display(Name = "attr_landing")]
        public string attr_landing { get; set; }

        [Display(Name = "signature")]
        public string signature { get; set; }
    }
    public class ErrorResult
    {
        public error error { get; set; }
    }
    public class error
    {
        public int code { get; set; }
        public string message { get; set; }
    }
    public class Status
    {
        public string id { get; set; }
        public string order_id { get; set; }
    }
    public class StatusResult
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string description { get; set; }
        public string user { get; set; }
        public string price { get; set; }
        public string earned { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public bool error { get; set; }
        public bool sandbox { get; set; }
        public string auth_code { get; set; }
        public string response_code { get; set; }
        public bool can_reverse { get; set; }
        public bool can_refund { get; set; }
        public bool can_partial_refund { get; set; }
    }
}
