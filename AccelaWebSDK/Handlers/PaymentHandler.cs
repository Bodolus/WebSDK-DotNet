using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accela.Web.SDK.Models;
using System.Net;

namespace Accela.Web.SDK
{
    public class PaymentHandler : BaseHandler, IPayment
    {
        public PaymentHandler(string appId, string appSecret, ApplicationType appType, string language, IConfigurationProvider configManager)
            : base(appId, appSecret, appType, language, configManager)
        {
        }

        public PaymentResult GetPayments(string token, PaymentInfo paymentInfo)
        {
            try
            {
                // Validate
                RequestValidator.ValidateToken(token);
                if (paymentInfo == null)
                {
                    throw new Exception("Null payment information provided");
                }

                // make payment
                string url = apiUrl + ConfigurationReader.GetValue("Payment");
                if (this.language != null)
                    url += "?lang=" + this.language;
                RESTResponse response = HttpHelper.SendPostRequest(url.ToString(), paymentInfo, token, this.appId);

                // create response
                PaymentResult payResult = new PaymentResult();
                payResult = (PaymentResult)HttpHelper.ConvertToSDKResponse(payResult, response);
                return payResult;
            }
            catch (WebException webException)
            {
                throw new Exception(HttpHelper.HandleWebException(webException, "Error in Make Payment :"));
            }
            catch (Exception exception)
            {
                throw new Exception(HttpHelper.HandleException(exception, "Error in Make Payment :"));
            }
        }

        public PaymentResult VoidPayment(string token, string paymentId, PaymentMisc paymentMisc)
        {
            try
            {
                // Validate
                RequestValidator.ValidateToken(token);
                if (string.IsNullOrWhiteSpace(paymentId) || paymentMisc == null)
                {
                    throw new Exception("Null payment information provided");
                }

                // void payment
                string url = apiUrl + ConfigurationReader.GetValue("VoidPayment").Replace("{paymentId}", paymentId);
                
                RESTResponse response = HttpHelper.SendPutRequest(url.ToString(), paymentMisc, token, this.appId);

                // create response
                PaymentResult paymentVoidResult = new PaymentResult();
                paymentVoidResult = (PaymentResult)HttpHelper.ConvertToSDKResponse(paymentVoidResult, response);
                return paymentVoidResult;
            }
            catch (WebException webException)
            {
                throw new Exception(HttpHelper.HandleWebException(webException, "Error in Void Payment :"));
            }
            catch (Exception exception)
            {
                throw new Exception(HttpHelper.HandleException(exception, "Error in Void Payment :"));
            }
        }

        public List<ETransactionInfo> GetETransactionInfo(string token, string TRANSACTION_NBR)
        {
            StringBuilder url = null;

            try
            {
                // Validate
                RequestValidator.ValidateToken(token);
                if (string.IsNullOrWhiteSpace(TRANSACTION_NBR))
                {
                    throw new Exception("Null transaction number provided");
                }

                // get eTransaction Info
                url = new StringBuilder(apiUrl).Replace("v4", "v3").Append(ConfigurationReader.GetValue("GetETransactionInfo"));

                TransactionNBR transNBR = new TransactionNBR() { TRANSACTION_NBR = TRANSACTION_NBR };

                RESTResponse response = HttpHelper.SendPostRequest(url.ToString(), transNBR, token, this.appId);

                // create response
                List<ETransactionInfo> paymentDetails = new List<ETransactionInfo>();
                paymentDetails = (List<ETransactionInfo>) HttpHelper.ConvertToSDKResponse(paymentDetails, response);

                return paymentDetails;
            }
            catch (WebException webException)
            {
                throw new Exception(HttpHelper.HandleWebException(webException, "Error in GetETransactionInfo call :"));
            }
            catch (Exception exception)
            {
                throw new Exception(HttpHelper.HandleException(exception, "Error in GetETransactionInfo call :"));
            }
        }

        public int ProcessRemittance(string token, ProcessRemittanceData inputData)
        {
            try
            {
                // Validate
                RequestValidator.ValidateToken(token);
                if (inputData == null)
                {
                    throw new Exception("Null input data provided");
                }

                // get eTransaction Info
                string url = apiUrl + ConfigurationReader.GetValue("ProcessRemittance");

                RESTResponse response = HttpHelper.SendPostRequest(url.ToString(), inputData, token, this.appId);

                // create response
                string RESULT = string.Empty;
                RESULT = (string) HttpHelper.ConvertToSDKResponse(RESULT, response);
                return int.Parse(RESULT);
            }
            catch (WebException webException)
            {
                throw new Exception(HttpHelper.HandleWebException(webException, "Error in GetETransactionInfo call :"));
            }
            catch (Exception exception)
            {
                throw new Exception(HttpHelper.HandleException(exception, "Error in GetETransactionInfo call :"));
            }
        }

        public List<FeeSchedule> GetFeeSchedule(string token, string feeScheduleId, string fields = null, string version = null)
        {
            try
            {
                // Validate
                RequestValidator.ValidateToken(token);
                if (string.IsNullOrEmpty(feeScheduleId))
                {
                    throw new Exception("Null Fee Schedule Id provided");
                }

                // get fee schedule
                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("GetFeeScheule").Replace("{scheduleId}", feeScheduleId));

                if (this.language != null)
                    url.Append("&lang=").Append(this.language);
                if (fields != null)
                    url.Append("&fields=").Append(fields);
                if (version != null)
                    url.Append("&version=").Append(version);

                RESTResponse response = HttpHelper.SendGetRequest(url.ToString(), token, appId);

                // create response
                List<FeeSchedule> feeSchedules = new List<FeeSchedule>();
                feeSchedules = (List<FeeSchedule>)HttpHelper.ConvertToSDKResponse(feeSchedules, response);
                return feeSchedules;
            }
            catch (WebException webException)
            {
                throw new Exception(HttpHelper.HandleWebException(webException, "Error in getting fee schedule :"));
            }
            catch (Exception exception)
            {
                throw new Exception(HttpHelper.HandleException(exception, "Error in getting fee schedule :"));
            }
        }
    }
}
