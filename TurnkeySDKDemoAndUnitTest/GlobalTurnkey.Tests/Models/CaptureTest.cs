using GlobalTurnkey.config;
using GlobalTurnkey.code;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class CaptureTest
    {
        [TestMethod]
        public void CaptureTestCall()
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
            Dictionary<String, String> tokenizeParams = new Dictionary<string, string>();
            tokenizeParams.Add("number", "5424180279791732");
            tokenizeParams.Add("nameOnCard", "mastercard");
            tokenizeParams.Add("expiryYear", "2021");
            tokenizeParams.Add("expiryMonth", "04");

            TokenizeCall tokenizeCall = new TokenizeCall(config, tokenizeParams);
            Dictionary<String, String> tokenizeResult = tokenizeCall.execute();

            //AUTH
            Dictionary<String, String> authParams = new Dictionary<String, String>();
            authParams.Add("amount", "20.0");
            authParams.Add("channel", Channel.ECOM.getCode());
            authParams.Add("country", CountryCode.US.getCode());
            authParams.Add("currency", CurrencyCode.USD.getCode());
            authParams.Add("paymentSolutionId", "500");
            authParams.Add("customerId", tokenizeResult["customerId"]);
            authParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            authParams.Add("specinCreditCardCVV", "111");
            authParams.Add("merchantNotificationUrl", "http://localhost:8080/api/TransactionResultCallback");

            AuthCall authCall = new AuthCall(config, authParams);
            Dictionary<String, String> authResult = authCall.execute();

            Assert.AreEqual(authResult["result"], "success");

            // CAPTURE
            Dictionary<String, String> inputParams = new Dictionary<String, String>();
            inputParams.Add("originalMerchantTxId", authResult["merchantTxId"]);
            inputParams.Add("amount", "20.0");

            CaptureCall call = new CaptureCall(config, inputParams);
            Dictionary<String, String> result = call.execute();

            Assert.AreEqual(result["result"], "success");
        }
    }
}
