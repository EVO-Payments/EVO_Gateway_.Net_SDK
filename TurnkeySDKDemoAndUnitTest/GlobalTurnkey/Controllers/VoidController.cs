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
    public class VoidController : ApiController
    {
        public async Task<object> Post()

        {
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

            VoidCall voidCall = new VoidCall(config, requestData);
            Dictionary<string, string> response = voidCall.execute();

            //return requestData;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
