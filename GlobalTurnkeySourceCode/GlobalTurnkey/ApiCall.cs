using GlobalTurnkey.config;
using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using GlobalTurnkey.Parameter;
using System.IO;
using GlobalTurnkey.exception;

namespace GlobalTurnkey
{
    public abstract class ApiCall
    {
       
	    protected ApplicationConfig config;
        private static readonly HttpClient client = new HttpClient();
        private Dictionary<String, String> inputParams;
        private string validationMessage = string.Empty;
        private static Dictionary<String, String> tokenParams = new Dictionary<string, string>();
        public Dictionary<String, String> TokenParameters
        {
            get
            {
                tokenParams.Clear();
                tokenParams.Add("merchantId", config.MerchantId);
                tokenParams.Add("password", config.Password);
                tokenParams.Add("timestamp", (Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds)).ToString());
                tokenParams.Add("allowOriginUrl", config.AllowOriginUrl);
                return tokenParams;
            }
        }

         public ApiCall(ApplicationConfig config, Dictionary<String, String> inputParams) {
             
             try
             {
                 this.config = config;
                 FormatInputParams(inputParams);
                 this.inputParams = inputParams;
                 preValidateParams(inputParams); 
             }
             catch(RequireParamException ex)
             {
                validationMessage = ex.Message;
             }
             catch(Exception ex) {
                throw ex;
             }
         }

        private void FormatInputParams(Dictionary<String, String> inputParams)
        {
            try
            {
                List<string> allNeedChangedKeys = new List<string>();
                foreach (var item in inputParams)
                {
                    if (item.Value.ToLower().StartsWith("http"))
                        allNeedChangedKeys.Add(item.Key);
                }

                foreach (string item in allNeedChangedKeys)
                {
                    inputParams[item] = HttpUtility.UrlDecode(inputParams[item], Encoding.GetEncoding(936));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void InputParamAfterValidation(List<String> requiredParams)
        {
            if (requiredParams.Count > 0)
            {
                string message = string.Empty;
                foreach (string param in requiredParams)
                {
                    message += param + ",";
                }
                message = message.Substring(0, message.Length - 1);
                throw new RequireParamException("Pre-validation failed: " + message + " required.");
            }
        }

        public static async Task<String> postToApi(String url, Dictionary<String, String> paramMap) {
            String apiResponseString = "";
            try
            {
                var wb = new WebClient();
                NameValueCollection nameValueCollection = new NameValueCollection();
                foreach (var kvp in paramMap)
                {
                    nameValueCollection.Add(kvp.Key.ToString(), kvp.Value.ToString());
                }
                
                var response = wb.UploadValues(url, "POST", nameValueCollection);
                apiResponseString = Encoding.UTF8.GetString(response);

            }
            catch (Exception ex) {
               var exception =  new PostToApiException("HTTP POST error",ex);
                throw exception;
            }


            return apiResponseString;
        }

        protected void GenerateRestParameters(Dictionary<String, String> inputParams, Dictionary<String, String> outputParams,ParamBase parameter)
        {
            Dictionary<String, String> allParams = Tools.GetDictionaryFromObject(parameter);
            foreach (string value in allParams.Values)
            {
                if (inputParams.ContainsKey(value)&& !outputParams.ContainsKey(value))
                    outputParams.Add(value, inputParams[value]);
            }
        }
        protected abstract Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams);

        protected abstract Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token);

        protected abstract void preValidateParams(Dictionary<String, String> inputParams);

        public Dictionary<String, String> execute()
        {
            try
            {  
                if(!string.IsNullOrEmpty(validationMessage))
                {
                    Dictionary<string, string> result = new Dictionary<string, string>();
                    result.Add("result","failure");
                    result.Add("message", validationMessage);
                    return result;
                }
                Task<String> tokenResponse = postToApi(config.SessionTokenRequestUrl, getTokenParams(inputParams));

                Dictionary<String, String> values = Tools.JsonToDictionary(tokenResponse.Result);
                if (values["result"] != "failure")
                {
                    String token = values["token"];

                    Dictionary<String, String> actionParams = getActionParams(inputParams, token);
                    if (actionParams == null)
                    {
                        return values;
                    }
                    Task<String> actionResponse = postToApi(config.PaymentOperationActionUrl, actionParams);

                    values = Tools.JsonToDictionary(actionResponse.Result);

                    return values;
                }
                else
                {
                    return values;
                }
            }
            catch (Exception ex)
            {
                var exception = new ActionCallException("Action call error", ex);
                throw exception;
            }
        }

        public void WriteLogToFile(string filePath,string content)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    string logFilePath = filePath;
                    File.AppendAllText(logFilePath, content + "\r\n");
                }
            }
            catch(Exception ex) { throw ex; }
        }
    }
}