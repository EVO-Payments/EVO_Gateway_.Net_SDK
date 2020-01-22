using System;
using System.Collections.Generic;
using GlobalTurnkey.config;
using GlobalTurnkey.code;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class GetAvailablePaymentSolutionsTest
    {
        [TestMethod]
        public void GetAvailablePaymentSolutionsTestCall()
        {
            ApplicationConfig config = new ApplicationConfig()
            {
                MerchantId = "167862",
                Password = "56789",
                SessionTokenRequestUrl = "https://api.test.universalpay.es/token",
                PaymentOperationActionUrl = "https://api.test.universalpay.es/payments",
                AllowOriginUrl = "http://localhost:8080",
                MerchantNotificationUrl = "http://localhost:8080/api/TransactionResultCallback",
                MerchantLandingPageUrl = "http://localhost:8080/",
                CashierUrl = "https://cashierui.test.universalpay.es/ui/cashier",
            };
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("country", CountryCode.FR.getCode());
            inputParams.Add("currency", CurrencyCode.EUR.getCode());

            GetAvailablePaymentSolutionsCall call = new GetAvailablePaymentSolutionsCall(config, inputParams);
            Dictionary<String, String> result = call.execute();


            Assert.AreEqual(result["result"],"success");
        }
    }
}
