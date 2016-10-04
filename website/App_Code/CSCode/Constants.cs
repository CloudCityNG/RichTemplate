using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants
{   
    public const string PageId = "PageId";


    #region Paypal
    public static bool siteIsLive = ReadConfiguration.siteIsLive;
    public static string PayPal_Username = (siteIsLive) ? "mmorris_api1.ndi-inc.org" : "maninder_api1.netsolutionsindia.com";
    public static string PayPal_Password = (siteIsLive) ? "ERLMEFKU4N4JNE6Z" : "HNEF96RR4QEHELWM";
    public static string PayPal_Signature = (siteIsLive) ? "An5ns1Kso7MWUdW4ErQKJJJ4qi4-ADVDYCViSkM3SZedKFAyvBFdXgoI" : "AFcWxV21C7fd0v3bYYYRCpSSRl31ATq34QNCCvdVJuq.u-gV5OLwmDr-";
    public static string PayPal_PaymentAccountName = (siteIsLive) ? "mmorris@ndi-inc.org" : "maninder@netsolutions.com";
    public static string PayPal_PaymentAccount = (siteIsLive) ? "live" : "sandbox";
    public const string PayPal_OrderDesc = "Card(s) Payment.";
    public const string PayPal_Url = "https://api-aa.paypal.com/2.0/";

    public const string PayPal_ShortErrorDesc = "L_SHORTMESSAGE0";//"Short error:";
    public const string PayPal_LongError = "L_LONGMESSAGE0";//"Long error:";
    public const string PayPal_ErrorCode = "L_ERRORCODE0";//"Error code:";
    public const string PayPal_APIResponse = "ACK";//"API response:";
    public const string PayPal_TransactionID = "TRANSACTIONID";//"TransactionID:";
    public const string PayPal_Timestamp = "TIMESTAMP";//"Timestamp:";

    public const string PayPal_APIResponse_Fail = "Failure";
    public const string PayPal_APIResponse_Sucess = "Success";
    public const string PayPal_APIResponse_SucessWithWarning = "SuccessWithWarning";
    #endregion

}
