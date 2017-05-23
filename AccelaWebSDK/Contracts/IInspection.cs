using Accela.Web.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accela.Web.SDK.Contracts
{
    public interface IInspection
    {
        // Inspections
        Inspection GetInspection(string inspId, string token);
        List<Inspection> GetInspectionsByStatus(string approvalId, string status, string token);
        Inspection SchedulePendingInspection(string approvalId, string inspTypeId, string date, string inspId, string token);
        string CancelInspection(string inspId, string token);
    }
}
