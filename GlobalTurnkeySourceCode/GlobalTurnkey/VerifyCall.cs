using GlobalTurnkey.code;
using GlobalTurnkey.config;
using GlobalTurnkey.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalTurnkey
{
    public class VerifyCall : ApiCall
    {

        public VerifyCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {

        }

        protected override void preValidateParams(Dictionary<String, String> inputParams)
        {
            List<String> requiredParams = new List<String>();
            requiredParams.Add("channel");
            requiredParams.Add("country");
            requiredParams.Add("currency");
            requiredParams.Add("merchantNotificationUrl");
            requiredParams.Add("amount");
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
            tokenParam.Add("action", ActionType.VERIFY.getCode());
            GenerateRestParameters(inputParams, tokenParam, new ParamAuthDirectToken());
            return tokenParam;
        }

        protected override Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token)
        {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", config.MerchantId);
            actionParams.Add("token", token);
            GenerateRestParameters(inputParams, actionParams, new ParamAuthDirectAction());

            return actionParams;
        }



    }
}