using GlobalTurnkey.code;
using GlobalTurnkey.config;
using GlobalTurnkey.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalTurnkey
{
    public class PurchaseTokenCall : ApiCall
    {
        public PurchaseTokenCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {

        }

        protected override void preValidateParams(Dictionary<String, String> inputParams)
        {
            List<String> requiredParams = new List<String>();
            requiredParams.Add("amount");
            requiredParams.Add("channel");
            requiredParams.Add("merchantNotificationUrl");
            requiredParams.Add("country");
            requiredParams.Add("currency");
            requiredParams.Add("paymentSolutionId");
            foreach (KeyValuePair<String, String> entry in inputParams)
            {
                if (entry.Value != null && entry.Value.Trim().Length > 0)
                {
                    requiredParams.Remove(entry.Key);
                }
            }

            InputParamAfterValidation(requiredParams);
        }

        protected override Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams)
        {
            Dictionary<String, String> tokenParam = TokenParameters;
            tokenParam.Add("action", ActionType.PURCHASE.getCode());
            GenerateRestParameters(inputParams, tokenParam, new ParamAuthHostedPaymentToken());
            return tokenParam;
        }

        protected override Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token)
        {
            return null;
        }
    }
}