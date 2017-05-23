using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accela.Web.SDK.Models;
using System.IO;

namespace Accela.Web.SDK
{
    public interface IPayment
    {
        PaymentResult GetPayments(string token, PaymentInfo paymentInfo);
        PaymentResult VoidPayment(string token, string paymentId, PaymentMisc paymentMisc);
        List<FeeSchedule> GetFeeSchedule(string token, string feeScheduleId, string fields = null, string version = null);
    }
}
