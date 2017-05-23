using Accela.Web.SDK.Contracts;
using Accela.Web.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Accela.Web.SDK.Handlers
{
    public class InspectionHandler : BaseHandler, IInspection
    {
        public InspectionHandler(string appId, string appSecret, ApplicationType appType, string language, IConfigurationProvider configManager)
            : base(appId, appSecret, appType, language, configManager)
        {
        }


        public List<Inspection> GetInspectionsByRecordId(string recordId, string token)
        {
            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(recordId))
                {
                    throw new Exception("Null record Id provided");
                }

                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("GetInspectionsByRecordId").Replace("{recordId}", recordId));

                RESTResponse response = HttpHelper.SendGetRequest(url.ToString(), token, this.appId);

                List<Inspection> insps = new List<Inspection>();
                insps = (List<Inspection>)HttpHelper.ConvertToSDKResponse(insps, response);
                
                return insps;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Get Inspections By RecordId :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Get Inspections By RecordId :"));
            }
        }

        public List<Inspection> GetInspectionsByStatus(string recordId, string status, string token)
        {
            try
            {
                //if (status == null || status.Length == 0)
                //    throw new Exception("Null or 0Length status provided");

                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(recordId) || String.IsNullOrWhiteSpace(status))
                {
                    throw new Exception("Null Approval Id || Status provided");
                }

                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("GetInspectionsByRecordId").Replace("{recordId}", recordId));

                RESTResponse response = HttpHelper.SendGetRequest(url.ToString(), token, this.appId);

                List<Inspection> insps = new List<Inspection>();
                insps = (List<Inspection>)HttpHelper.ConvertToSDKResponse(insps, response);

                List<Inspection> inspsByStatus = new List<Inspection>();
                
                if (insps != null)
                {
                    // Get the Scheduled or Pending insps
                    if ("Scheduled".ToUpper().Equals(status.ToUpper()) || "Pending".ToUpper().Equals(status.ToUpper()))
                        inspsByStatus = insps.FindAll(s => s.status.value.ToUpper().Equals(status.ToUpper()));
                    // Otherwise get the Completed insps
                    else
                        inspsByStatus = insps.FindAll(s => !s.status.value.ToUpper().Equals("Scheduled".ToUpper()) && !s.status.value.ToUpper().Equals("Pending".ToUpper()));
                }

                return inspsByStatus;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Get Inspections By Status :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Get Inspections By Status :"));
            }
        }


        public Inspection GetInspection(string inspId, string token)
        {
            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(inspId))
                {
                    throw new Exception("Null Inspection Id provided");
                }

                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("GetInspection").Replace("{ids}", inspId));

                ///TODO add in language handler for query

                RESTResponse response = HttpHelper.SendGetRequest(url.ToString(), token, this.appId);

                List<Inspection> inspection = new List<Inspection>();
                inspection = (List<Inspection>)HttpHelper.ConvertToSDKResponse(inspection, response);

                return (inspection != null) ? inspection.First() : null;
            }
            catch (WebException webException)
            {
                if (webException.Message.Equals("The remote server returned an error: (404) Not Found."))
                    return null;

                throw new Exception(HttpHelper.HandleWebException(webException, "Error in Get Inspection :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Get Inspection :"));
            }
        }

        public Inspection SchedulePendingInspection(string approvalId, string inspTypeId, string date, string inspId, string token)
        {
            EmptyRequestBody temp = new EmptyRequestBody();

            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(inspId))
                {
                    throw new Exception("Null inspId provided");
                }

                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("SchedulePendingInspection").Replace("{inspectionId}", inspId));

                RESTResponse response = HttpHelper.SendPutRequest(url.ToString(), temp, token, this.appId);

                Inspection insp = new Inspection();
                insp = (Inspection)HttpHelper.ConvertToSDKResponse(insp, response);

                return insp;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Cancel Inspections :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Cancel Inspections :"));
            }
        }

        public Inspection ScheduleInspection(string approvalId, string inspTypeId, string date, string inspId, string token)
        {
            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(inspId))
                {
                    throw new Exception("Null inspId provided");
                }

                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("ScheduleInspection"));

                Inspection insp = new Inspection()
                {
                    type = new InspectionType() { id = long.Parse(inspTypeId) },
                    recordId = new RecordId() { id = approvalId },
                    requestDate = DateTime.Now,
                    scheduleDate = DateTime.Parse(date)
                };

                RESTResponse response = HttpHelper.SendPostRequest(url.ToString(), insp, token, this.appId);

                Inspection scheduledInsp = new Inspection();
                scheduledInsp = (Inspection)HttpHelper.ConvertToSDKResponse(scheduledInsp, response);

                return scheduledInsp;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Cancel Inspections :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Cancel Inspections :"));
            }
        }

        public string CancelInspection(string inspId, string token)
        {
            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(inspId))
                {
                    throw new Exception("Null inspId provided");
                }

                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("CancelInspection").Replace("{inspectionId}", inspId));

                RESTResponse response = HttpHelper.SendDeleteRequest(url.ToString(), token, this.appId);

                if (response != null && response.Result != null && response.Status == 200)
                    return "SUCCESS";
                else
                    return "FAILURE";
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Cancel Inspections :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Cancel Inspections :"));
            }
        }


        public List<Checklist> GetInspectionChecklists(string inspId, string token)
        {
            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(inspId))
                {
                    throw new Exception("Null Inspection Id provided");
                }

                StringBuilder url = new StringBuilder(apiUrl + ConfigurationReader.GetValue("GetInspectionChecklists").Replace("{inspectionId}", inspId));

                RESTResponse response = HttpHelper.SendGetRequest(url.ToString(), token, this.appId);

                List<Checklist> inspChecklists = new List<Checklist>();
                inspChecklists = (List<Checklist>)HttpHelper.ConvertToSDKResponse(inspChecklists, response);

                return inspChecklists;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Get Inspection Checklists :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Get Inspection Checklists :"));
            }
        }

        public List<Items> GetInspectionChecklistItems(string inspId, string checklistId, string token)
        {
            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(inspId) || String.IsNullOrWhiteSpace(checklistId))
                {
                    throw new Exception("Null parameter provided");
                }

                StringBuilder url = new StringBuilder(apiUrl).Append(ConfigurationReader.GetValue("GetInspectionChecklistItems"))
                                                             .Replace("{inspectionId}", inspId)
                                                             .Replace("{checklistId}",checklistId);

                RESTResponse response = HttpHelper.SendGetRequest(url.ToString(), token, this.appId);

                List<Items> inspChecklistItems = new List<Items>();
                inspChecklistItems = (List<Items>)HttpHelper.ConvertToSDKResponse(inspChecklistItems, response);

                return inspChecklistItems;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Get Inspection Checklist Items :"));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Get Inspection Checklist Items :"));
            }
        }

        public List<DateTime> GetAvailableDates(string recordId, string inspTypeId, string date, string token)
        {
            StringBuilder url = null;

            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(recordId) || String.IsNullOrWhiteSpace(inspTypeId) || String.IsNullOrWhiteSpace(date))
                {
                    throw new Exception("Null parameter provided");
                }

                url = new StringBuilder(apiUrl)
                            .Append(ConfigurationReader.GetValue("GetInspectionAvailableDates"))
                            .Replace("{recordId}", recordId)
                            .Replace("{inspTypeId}", inspTypeId)
                            .Replace("{startDate}", date);

                RESTResponse response = HttpHelper.SendGetRequest(url.ToString(), token, this.appId);

                List<DateTime> availableDates = new List<DateTime>();
                availableDates = (List<DateTime>)HttpHelper.ConvertToSDKResponse(availableDates, response);

                return availableDates;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Get Available Dates / recordId = " + recordId.ToString() + " / inspTypeId = " + inspTypeId + " / date = " + date + " / url = " + url + " : "));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Get Available Dates / recordId = " + recordId.ToString() + " / inspTypeId = " + inspTypeId + " / date = " + date + " / url = " + url + " : "));
            }
        }

        public bool IsInspectionAlwaysAvailableAndCustomerRequestable(string inspTypeId, string token)
        {


            return false;
        }

        public int GetInspectionIVRByInspTypeID(string inspTypeId, string token)
        {
            StringBuilder url = null;

            try
            {
                RequestValidator.ValidateToken(token);
                if (String.IsNullOrWhiteSpace(inspTypeId))
                {
                    throw new Exception("Null parameter provided");
                }

                url = new StringBuilder(apiUrl)
                            .Replace("v4", "v3")   // Use the old API version to access this generic query script
                            .Append(ConfigurationReader.GetValue("GetInspectionInfoIVR"));

                InspectionID inspType = new InspectionID() { INSPECTION_ID = inspTypeId };

                RESTResponse response = HttpHelper.SendPostRequest(url.ToString(), inspType, token, this.appId);

                List<InspectionInfoIVR> inspInfoIVR = new List<InspectionInfoIVR>();
                inspInfoIVR = (List<InspectionInfoIVR>) HttpHelper.ConvertToSDKResponse(inspInfoIVR, response);

                int ivrNumber = 0;

                if (inspInfoIVR.Count > 0)
                {
                    ivrNumber = int.Parse(inspInfoIVR[0].IVR_NUMBER);

                    foreach (InspectionInfoIVR inspInfo in inspInfoIVR)
                    {
                        if (ivrNumber.CompareTo(int.Parse(inspInfo.IVR_NUMBER)) != 0)
                        {
                            return -1;
                        }
                    }
                }
                
                return ivrNumber;
            }
            catch (WebException webEx)
            {
                throw new Exception(HttpHelper.HandleWebException(webEx, "Error in Get InspectionIVR By InspTypeId / inspTypeId = " + inspTypeId + " / url = " + url + " : "));
            }
            catch (Exception e)
            {
                throw new Exception(HttpHelper.HandleException(e, "Error in Get InspectionIVR By InspTypeId / inspTypeId = " + inspTypeId + " / url = " + url + " : "));
            }
        }

        public class EmptyRequestBody{}

    }
}
