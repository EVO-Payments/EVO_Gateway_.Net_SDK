using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using GlobalTurnkey.Parameter;
using System.Xml;

namespace GlobalTurnkey.config
{
    public class ApplicationConfig
    {
        public string SessionTokenRequestUrl { get; set; }
        public string PaymentOperationActionUrl { get; set; }
        public string CashierUrl { get; set; }
        public string AllowOriginUrl { get; set; }
        public string MerchantNotificationUrl { get; set; }
        public string MerchantLandingPageUrl { get; set; }
        public string MerchantId { get; set; }
        public string Password { get; set; }

        public ApplicationConfig() { }
        public ApplicationConfig(string merchantId,string password,string sessionTokenRequestUrl,string paymentOperationActionUrl,string allowOriginUrl,
                                 string merchantNotificationUrl,string merchantLandingPageUrl,string cashierUrl)
        {
            MerchantId = merchantId;
            Password = password;
            SessionTokenRequestUrl = sessionTokenRequestUrl;
            PaymentOperationActionUrl = paymentOperationActionUrl;
            AllowOriginUrl = allowOriginUrl;
            MerchantNotificationUrl = merchantNotificationUrl;
            MerchantLandingPageUrl = merchantLandingPageUrl;
            CashierUrl = cashierUrl;
        }
    }
}