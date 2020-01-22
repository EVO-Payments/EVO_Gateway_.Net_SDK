using System;
using System.Collections.Generic;
using GlobalTurnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class TokenizeTest
    {
        [TestMethod]
        public void noExTestCall()
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");
            inputParams.Add("customerId", "123456789");
            inputParams.Add("cardDescription", "test");


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
            TokenizeCall call = new TokenizeCall(config, inputParams);
            Dictionary<String, String> result = call.execute();

            Assert.AreEqual(result["result"],"success");
        }

        [TestMethod]
        [Description("Expected: expiryMonth is required.")]
        public void ExExpTestCall()
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2010");

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
            TokenizeCall call = new TokenizeCall(config, inputParams);
            Dictionary<String, String> result = call.execute();

            Assert.AreEqual(result["result"],"failure");
        }
    }
}
