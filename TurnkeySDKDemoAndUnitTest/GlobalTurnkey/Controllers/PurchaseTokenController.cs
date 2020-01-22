using GlobalTurnkey.config;
using GlobalTurnkey.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GlobalTurnkey.Controllers
{
    public class PurchaseTokenController : ApiController
    {
        public async Task<object> Post()

        {
            try
            {
                HttpContent requestContent = Request.Content;
                string res = requestContent.ReadAsStringAsync().Result;
                Dictionary<String, String> inputParams = Tools.requestToDictionary(res);

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

                Dictionary<String, String> executeData = new PurchaseTokenCall(config, inputParams).execute();

                inputParams["merchantId"] = config.MerchantId;
                inputParams["token"] = executeData["token"];

                //return requestData;
                return Request.CreateResponse(HttpStatusCode.OK, inputParams);

            }
            catch (RequireParamException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing fields: " + ex.ToString());
            }
            catch (TokenAcquirationException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Could not acquire token: " + ex.ToString());
            }
            catch (PostToApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Outgoing POST failed: " + ex.ToString());
            }
            catch (GeneralException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "General SDK error: " + ex.ToString());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error: " + ex.ToString());
            }


        }
    }
}
