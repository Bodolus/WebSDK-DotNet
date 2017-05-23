using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accela.Web.SDK.Models
{
    public class BillingAddress
    {
        public string addressLine1 { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
        public string countryCode { get; set; }
    }

    public class CreditCard
    {
        public BillingAddress billingAddress { get; set; }
        public string businessName { get; set; }
        public string cardNumber { get; set; }
        public string cardType { get; set; }
        public string email { get; set; }
        public int expirationMonth { get; set; }
        public int expirationYear { get; set; }
        public string holderName { get; set; }
        public string phone { get; set; }
        public bool pos { get; set; }
        public string securityCode { get; set; }
    }

    public class PaymentInfo
    {
        public CreditCard creditCard { get; set; }
        public string currency { get; set; }
        public double amount { get; set; }
        public string entityId { get; set; }
        public string entityType { get; set; }
        public string message { get; set; }
        public string paymentMethod { get; set; }
    }

    public class PayTrail
    {
        public string referenceId { get; set; }
        public string transactionId { get; set; }
        public string authCode { get; set; }
        public string type { get; set; }
        public string payAmount { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public bool processFee { get; set; }
    }

    public class PaymentResult
    {
        public string entityId { get; set; }
        public string entityType { get; set; }
        public string receiptNumber { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string transactionId { get; set; }
    }

    public class PaymentMisc
    {
        public string comment { get; set; }
        public PaymentReasonDetail reason { get; set; }
    }

    public class PaymentReasonDetail
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class TransactionNBR
    {
        public string TRANSACTION_NBR { get; set; }
    }

    public class ETransactionInfo
    {
        public string B1_ALT_ID { get; set; }
        public string PAYMENT_SEQ_NBR { get; set; }
        public string BATCH_TRANSACTION_NBR { get; set; }
        public string RECEIPT_NBR { get; set; }
        public string INVOICE_NBR { get; set; }
        public string PAYMENT_STATUS { get; set; }
        public string PAYMENT_AMOUNT { get; set; }
        public string F4PAYMENT_UDF1 { get; set; }
        public string F4PAYMENT_UDF2 { get; set; }
        public string F4PAYMENT_UDF3 { get; set; }
        public string F4PAYMENT_UDF4 { get; set; }
    }

    public class ProcessRemittanceData
    {
        public string ALT_ID { get; set; }
        public long PMT_SEQ_NUM { get; set; }
        public string CLEAR_DATE { get; set; }
        public string STATUS { get; set; }
        public string RECEIVED_TYPE { get; set; }

    }
}
