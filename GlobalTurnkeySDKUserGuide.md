# Summary
This document introduces the file structure of SDK project, available classes and functions for invoiking. And also provide the guidline to tell user how to utilize the SDK step by step. User can also refrence the demo
and unit test to get more knowlege about the SDK.

# Details
## Part1 - The file structure of SDK project:
* "GlobalTurnkeySourceCode" is for SDK library source code, you can open the sollution using visual studio and buid it to get a *.dll file. 
* "TurnkeySDKDemoAndUnitTest" is for some demos which shows all needed parameters and the excution result, and unit test represent the detail logic of each transaction and dependency.

## Part2 - User guidline of using SDK
The library provides integration access to the Turnkey Global Api.You can use it to quickly integrate with the Payments system and submit transactions, check their status and more.
### Before you Begin
Before using the Payments .NET SDK you should be familiar with the contents of the [API Specification for Merchants](docs/API-Specification.pdf) document as it describes all fields and their meaning within a given payment transaction.
### Setup your Project
Payments .NET SDK is delivered as .NET Class Library project.
It depends on a third party library: Newtonsoft.Json(version:12.0.0.0)
requirements: .NET Framework 4.5 (or newer)
### Possible Requests
Every payment operation has its own Call Object. To successfully perform any request one needs to create the object (configure it) and then call its execute() method.
* __GetAvailablePaymentSolutionsCall__ queries the list of the possible payment solutions (ie. credit card) (based on the country/currency)
* __TokenizeCall__ tokenizes the card for future use.
* __AuthCall__ requests authorisation for a payment.
* __CaptureCall__ performs a capture operation on an authorized payment.
* __VerifyCall__ performs a verify operation on an authorized payment.
* __VoidCall__ cancels a previously authenticated payment.
* __PurchaseCall__ does an authorize and capture operations at once (and cannot be voided).
* __PurchaseTokenCall__ performs a tokeniza request operation to prepare a token for load payment request using.
* __RefundCall__ refunds a previous capture operation, partially or in full.
* __StatusCheckCall__ returns the status of an already issued payment transaction, as such it doesn’t actually generate a new transaction.
All classes are descendants of the _ApiCall_ class.
For more information on payment transactions please check the [API Specification for Merchants](docs/API-Specification.pdf) document.
Some of the possible request/call chains (ie. tokenize -> auth -> capture) can be seen in the unit test to.
### Typical Flow
  I. Access the ApplicationConfig object like this:
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

  II. Create the a Call object:
      Dictionary<String, String> params = new Dictionary<String, String>();
      inputParams.Add("country", "FR");
      inputParams.Add("currency", "EUR");
      ApiCall call = new GetAvailablePaymentSolutionsCall(config, params);
      The call parameters have to supplied via a Dictionary. 
      For more information on the possible/needed dictionary parameters please check the [API Specification for Merchants](docs/API-Specification.pdf) document.
      The constructor will do a simple "pre" validation on the params Dictionary. It will only check for the required keys (without an HTTP/API call).

  III. Execute the call:
       Dictionary<String, String> result = call.execute();
       For more information on the possible result values (KeyValuePair) please check the [API Specification for Merchants](docs/API-Specification.pdf) document.

  IV. Watch for Exceptions
      Occasionally the SDK will not be able to perform your request and it will throw an Exception. This could be due to some unexpected conditions like no connectivity to the API. 

     try {
	     ApiCall call = new GetAvailablePaymentSolutionsCall(config, params);
     } catch (RequiredParamException e) {
	     // notify the user, exit the program, redirect to the error page etc.
     }

     try {
	     Dictionary<String, String> result = call.execute();
     } catch (ActionCallException e) {
	     // notify the user, exit the program, redirect to the error page etc.
### Payments Errors
Occasionally your payment processing API will not be able to successfully complete a request and it will return an error. Please check out the [API Specification for Merchants](docs/API-Specification.pdf) document to find out more about errors and what causes them to occur.


## Part3 - SDK demo and unit test usage
SDK demo and unit test is an individual project, you can open it with visual studio.

The demo reference SDK library(.dll file) and invoke the api method to complete related transactions.It is designed as web application, you can input all required parameters and submit your request to get result.

Unit test present all transaction api invoiking, prepare parameters, process the result, also you can get the knowlege of relationship among the transactions.


