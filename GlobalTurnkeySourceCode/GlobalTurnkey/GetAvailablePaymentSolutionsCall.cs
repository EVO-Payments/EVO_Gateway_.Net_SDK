using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using GlobalTurnkey.code;
using GlobalTurnkey.config;
using GlobalTurnkey.Parameter;

namespace GlobalTurnkey
{
    public class GetAvailablePaymentSolutionsCall : ApiCall
    {
        public GetAvailablePaymentSolutionsCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {
            
        }

        protected override void preValidateParams(Dictionary<String, String> inputParams) {
            List<String> requiredParams = new List<String>();
            requiredParams.Add("country");
            requiredParams.Add("currency");
            foreach (KeyValuePair<String, String> entry in inputParams) {
                if (entry.Value != null && entry.Value.Trim().Length > 0) {
                    requiredParams.Remove(entry.Key);
                }
            }

            InputParamAfterValidation(requiredParams);
        }

        protected override Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams) {
            Dictionary<String, String> tokenParam = TokenParameters;
            tokenParam.Add("action", ActionType.GET_AVAILABLE_PAYSOLS.getCode());
            GenerateRestParameters(inputParams, tokenParam, new ParamGetAvailablePaymentSolutionsSessionToken());
            return tokenParam;
        }

        protected override Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token) {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", config.MerchantId);
            actionParams.Add("token", token);
            GenerateRestParameters(inputParams, actionParams, new ParamGetAvailablePaymentSolutionsAction());

            return actionParams;
        }

    }
}