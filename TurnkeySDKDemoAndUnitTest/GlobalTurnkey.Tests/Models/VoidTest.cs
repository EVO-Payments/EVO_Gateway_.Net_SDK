using System;
using System.Collections.Generic;
using GlobalTurnkey.code;
using GlobalTurnkey.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class VoidTest
    {
        [TestMethod]
        public void AuthToVoidExTestCall()
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

            if (authResult["result"] == "success" && authResult["status"] == "NOT_SET_FOR_CAPTURE")
            {
                // VOID
                Dictionary<String, String> inputParams = new Dictionary<String, String>();
                inputParams.Add("originalMerchantTxId", authResult["merchantTxId"]);
                //inputParams.Add("country", "FR");
                //inputParams.Add("currency", "EUR");

                VoidCall call = new VoidCall(config, inputParams);
                Dictionary<String, String> result = call.execute();

                Assert.AreEqual(result["result"], "success");
            }

        }

        [TestMethod]
        public void CaptureToVoidExTestCall()
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

            if (authResult["result"] == "success" && authResult["status"] == "NOT_SET_FOR_CAPTURE")
            {
                // CAPTURE
                Dictionary<String, String> captrueInputParams = new Dictionary<String, String>();
                captrueInputParams.Add("originalMerchantTxId", authResult["merchantTxId"]);
                captrueInputParams.Add("amount", "20.0");

                CaptureCall captureCall = new CaptureCall(config, captrueInputParams);
                Dictionary<String, String> captureResult = captureCall.execute();
                if (captureResult["result"] == "success")
                {
                    // VOID
                    Dictionary<String, String> inputParams = new Dictionary<String, String>();
                    inputParams.Add("originalMerchantTxId", authResult["merchantTxId"]);
                    //inputParams.Add("country", "FR");
                    //inputParams.Add("currency", "EUR");

                    VoidCall call = new VoidCall(config, inputParams);
                    Dictionary<String, String> result = call.execute();

                    Assert.AreEqual(result["result"], "success");
                }

            }

        }

        [TestMethod]
        public void PurchaseToVoidTestCall()
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

            PurchaseCall authCall = new PurchaseCall(config, authParams);
            Dictionary<String, String> purchaseResult = authCall.execute();

            Assert.AreEqual(purchaseResult["result"], "success");

            if (purchaseResult["result"] == "success")
            {
                // VOID
                Dictionary<String, String> inputParams = new Dictionary<String, String>();
                inputParams.Add("originalMerchantTxId", purchaseResult["merchantTxId"]);
                //inputParams.Add("country", "FR");
                //inputParams.Add("currency", "EUR");

                VoidCall call = new VoidCall(config, inputParams);
                Dictionary<String, String> result = call.execute();

                Assert.AreEqual(result["result"], "failure");
            }

        }
    }
}
