using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using System.IO;

namespace GlobalTurnkey.Controllers
{
    public class TransactionResultCallbackController : ApiController
    {
        public async Task<object> Post(){
           
                HttpContent requestContent = Request.Content;
            string res = requestContent.ReadAsStringAsync().Result;
            Dictionary<String, String> inputParams = Tools.requestToDictionary(res);
            
            
            return Request.CreateResponse(HttpStatusCode.OK, inputParams);
            
        }

        
    }
}
