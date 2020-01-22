using System;
using System.Collections.Generic;
using GlobalTurnkey.code;
using GlobalTurnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class PurchaseTest
    {
        [TestMethod]
        public void PurchaseTestCall()
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
            // TOKENIZE
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            TokenizeCall tokenizeCall = new TokenizeCall(config, inputParams);
            Dictionary<String, String> tokenizeResult = tokenizeCall.execute();

            // PURCHASE
            Dictionary<String, String> purchaseParams = new Dictionary<String, String>();
            purchaseParams.Add("amount", "20.0");
            purchaseParams.Add("channel", Channel.ECOM.getCode());
            purchaseParams.Add("country", CountryCode.US.getCode());
            purchaseParams.Add("currency", CurrencyCode.USD.getCode());
            purchaseParams.Add("paymentSolutionId", "500");
            purchaseParams.Add("customerId", tokenizeResult["customerId"]);
            purchaseParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            purchaseParams.Add("specinCreditCardCVV", "111");
            purchaseParams.Add("merchantNotificationUrl", "http://localhost:8080/api/TransactionResultCallback");

            PurchaseCall call = new PurchaseCall(config, purchaseParams);
            Dictionary<String, String> result = call.execute();

            Assert.AreEqual(result["result"], "success");
        }

    }
}
