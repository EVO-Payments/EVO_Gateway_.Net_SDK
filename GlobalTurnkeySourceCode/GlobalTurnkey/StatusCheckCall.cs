using GlobalTurnkey.code;
using GlobalTurnkey.config;
using GlobalTurnkey.exception;
using GlobalTurnkey.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalTurnkey
{
    public class StatusCheckCall : ApiCall
    {
        public StatusCheckCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {

        }

        protected override void preValidateParams(Dictionary<String, String> inputParams)
        {
            if(!inputParams.ContainsKey("txId") && !inputParams.ContainsKey("merchantTxId"))
                throw new RequireParamException("Filed txId or merchantTxId must be provided at least one.");
        }

        protected override Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams)
        {
            Dictionary<String, String> tokenParam = TokenParameters;
            tokenParam.Add("action", ActionType.GET_STATUS.getCode());

            GenerateRestParameters(inputParams, tokenParam, new ParamGetStatusSessionToken());

            return tokenParam;
        }

        protected override Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token)
        {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", config.MerchantId);
            actionParams.Add("token", token);

            GenerateRestParameters(inputParams, actionParams, new ParamGetStatusAction());

            return actionParams;
        }
    }
}