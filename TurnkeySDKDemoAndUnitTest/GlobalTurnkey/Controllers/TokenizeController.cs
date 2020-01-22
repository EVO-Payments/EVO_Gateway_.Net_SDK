using GlobalTurnkey.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GlobalTurnkey.Controllers
{
    public class TokenizeController : ApiController
    {

        public async Task<object> Post()
        {
            /*Get request data from http*/
            string test = Properties.Settings.Default.password;
            HttpContent requestContent = Request.Content;
            string res = requestContent.ReadAsStringAsync().Result;
            Dictionary<String, String> requestData = Tools.requestToDictionary(res);

            /*Init appliction configuration*/
            ApplicationConfig config = new ApplicationConfig()
            {
                MerchantId = Properties.Settings.Default.merchantId,
                Password = Properties.Settings.Default.password,
                SessionTokenRequestUrl = Properties.Settings.Default.sessionTokenRequestUrl,
                PaymentOperationActionUrl = Properties.Settings.Default.paymentOperationActionUrl,
                AllowOriginUrl = Properties.Settings.Default.allowOriginUrl,
                MerchantNotificationUrl = Properties.Settings.Default.merchantNotificationUrl,
                MerchantLandingPageUrl = Properties.Settings.Default.merchantLandingPageUrl,
                CashierUrl = Properties.Settings.Default.cashierUrl,
            };

            /*Create action call api and invoke it*/
            TokenizeCall tokenize = new TokenizeCall(config, requestData);
            Dictionary<string, string> response = tokenize.execute();

            //return requestData;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}
