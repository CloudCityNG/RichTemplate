/*
 * Copyright 2005, 2008 PayPal, Inc. All Rights Reserved.
 *
 * DoDirectPayment NVP example; last modified 08MAY23. 
 *
 * Process a credit card payment.  
 */
using System;
using System.Collections;
using com.paypal.sdk.services;
using com.paypal.sdk.profiles;
using com.paypal.sdk.util;
using NSIDAAB;
/**
 * PayPal .NET SDK sample code
 */
namespace GenerateCodeNVP
{
    public class DoDirectPayment
    {
        #region Constructor
        public DoDirectPayment()
        {
            sCCNumber = string.Empty;
            sCCVerificationCode = string.Empty;
            iCCExpMonth = 0;
            iCCExpYear = 0;
            sCCOwnerFirstName = string.Empty;
            sCCOwnerLastName = string.Empty;
            sCCOwnerStreet1 = string.Empty;
            CCOwnerStreet2 = string.Empty;
            sCCOwnerCityName = string.Empty;
            sCCOwnerStateOrProvince = string.Empty;
            sCCOwnerPostalCode = string.Empty;
            sCCOwnerCountryCode = string.Empty;
            fPaymentOrderTotal = 0;
        }
        #endregion

        #region properties
        private string sCCNumber;
        public string CCNumber { get { return (sCCNumber).Replace("-", "").Replace(" ", "").Trim(); } set { sCCNumber = value; } }
        private CreditCardType sCCType;
        public CreditCardType CCType { get { return sCCType; } set { sCCType = value; } }
        private string sCCVerificationCode;
        public string CCVerificationCode { get { return sCCVerificationCode; } set { sCCVerificationCode = value; } }
        private int iCCExpMonth;
        public int CCExpMonth { get { return iCCExpMonth; } set { iCCExpMonth = value; } }
        private int iCCExpYear;
        public int CCExpYear { get { return iCCExpYear; } set { iCCExpYear = value; } }

        private string sCCOwnerFirstName;
        public string CCOwnerFirstName { get { return sCCOwnerFirstName; } set { sCCOwnerFirstName = value; } }
        private string sCCOwnerLastName;
        public string CCOwnerLastName { get { return sCCOwnerLastName; } set { sCCOwnerLastName = value; } }
        private string sCCOwnerStreet1;
        public string CCOwnerStreet1 { get { return sCCOwnerStreet1; } set { sCCOwnerStreet1 = value; } }
        private string sCCOwnerStreet2;
        public string CCOwnerStreet2 { get { return sCCOwnerStreet2; } set { sCCOwnerStreet2 = value; } }
        private string sCCOwnerCityName;
        public string CCOwnerCityName { get { return sCCOwnerCityName; } set { sCCOwnerCityName = value; } }
        private string sCCOwnerStateOrProvince;
        public string CCOwnerStateOrProvince { get { return sCCOwnerStateOrProvince; } set { sCCOwnerStateOrProvince = value; } }
        private string sCCOwnerPostalCode;
        public string CCOwnerPostalCode { get { return sCCOwnerPostalCode; } set { sCCOwnerPostalCode = value; } }
        private string sCCOwnerCountryCode;
        public string CCOwnerCountryCode { get { return sCCOwnerCountryCode; } set { sCCOwnerCountryCode = value; } }

        private float fPaymentOrderTotal;
        public float PaymentOrderTotal { get { return fPaymentOrderTotal; } set { fPaymentOrderTotal = value; } }

        public string sCCExpDate
        {
            get
            {
                return (iCCExpMonth.ToString().Trim().Length == 1) ? string.Concat("0", iCCExpMonth.ToString(), iCCExpYear) : string.Concat(iCCExpMonth.ToString(), iCCExpYear);
            }
        }
        #endregion

        #region Methods
        public Hashtable DoDirectPaymentCode()
        {
            NVPCallerServices caller = new NVPCallerServices();
            IAPIProfile profile = ProfileFactory.createSignatureAPIProfile();
            /*
             WARNING: Do not embed plaintext credentials in your application code.
             Doing so is insecure and against best practices.
             Your API credentials must be handled securely. Please consider
             encrypting them for use in any production environment, and ensure
             that only authorized individuals may view or modify them.
             */

            // Set up your API credentials, PayPal end point, API operation and version.
            profile.APIUsername = Constants.PayPal_Username;//"sdk-three_api1.sdk.com";
            profile.APIPassword = Constants.PayPal_Password;//"QFZCWN5HZM8VBG7Q";
            profile.APISignature = Constants.PayPal_Signature;//"AVGidzoSQiGWu.lGj3z15HLczXaaAcK6imHawrjefqgclVwBe8imgCHZ";
            profile.Environment = Constants.PayPal_PaymentAccount; //"live";// "sandbox";
            caller.APIProfile = profile;

            NVPCodec encoder = new NVPCodec();
            encoder["VERSION"] = "51.0";
            encoder["METHOD"] = "DoDirectPayment";

            // Add request-specific fields to the request.
            encoder["PAYMENTACTION"] = "Sale"; //paymentAction;
            encoder["AMT"] = fPaymentOrderTotal.ToString();
            encoder["CREDITCARDTYPE"] = (sCCType.Equals(CreditCardType.AmericanExpress)) ? "Amex" : sCCType.ToString();
            encoder["ACCT"] = CCNumber;
            encoder["EXPDATE"] = sCCExpDate;
            encoder["CVV2"] = sCCVerificationCode;
            encoder["FIRSTNAME"] = sCCOwnerFirstName;
            encoder["LASTNAME"] = sCCOwnerLastName;
            encoder["STREET"] = (sCCOwnerStreet2.Trim().Length > 0) ? string.Concat(sCCOwnerStreet1, ", ", sCCOwnerStreet2) : sCCOwnerStreet1;
            encoder["CITY"] = sCCOwnerCityName;
            encoder["STATE"] = sCCOwnerStateOrProvince;
            encoder["ZIP"] = sCCOwnerPostalCode;
            encoder["COUNTRYCODE"] = sCCOwnerCountryCode;
            encoder["CURRENCYCODE"] = "USD";

            // Execute the API operation and obtain the response.
            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = caller.Call(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            Hashtable htResult = new Hashtable();
            foreach (string st in decoder.AllKeys)
            {
                htResult.Add(st, decoder[st]);
            }
            return htResult;
        }
        #endregion
    }
}

