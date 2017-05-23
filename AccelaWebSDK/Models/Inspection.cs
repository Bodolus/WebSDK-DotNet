using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accela.Web.SDK.Models
{
    public class Inspection
    {
        public Address address { get; set; }
        public string billable { get; set; }
        public string category { get; set; }
        public string commentDisplay { get; set; }
        public List<string> commentPublicVisible { get; set; }
        public string completedAMPM { get; set; }
        public DateTime completedDate { get; set; }
        public string completedTime { get; set; }
        public Contact contact { get; set; }
        public string contactFirstName { get; set; }
        public string contactLastName { get; set; }
        public string contactMiddleName { get; set; }
        public string desiredAMPM { get; set; }
        public DateTime desiredDate { get; set; }
        public string desiredTime { get; set; }
        public float endMileage { get; set; }
        public DateTime endTime { get; set; }
        public string estimatedEndTime { get; set; }
        public string estimatedStartTime { get; set; }
        public string gisAreaName { get; set; }
        public string grade { get; set; }
        public long id { get; set; }
        public string inspectorFullName { get; set; }
        public string inspectorId { get; set; }
        public string isAutoAssign { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public long majorViolation { get; set; }
        public string overtime { get; set; }
        public double priority { get; set; }
        public string publicVisible { get; set; }
        public RecordId recordId { get; set; }
        public RecordType recordType { get; set; }
        public string requestAMPM { get; set; }
        public string requestComment { get; set; }
        public DateTime requestDate { get; set; }
        public string requestorFirstName { get; set; }
        public string requestorLastName { get; set; }
        public string requestorMiddleName { get; set; }
        public string requestorPhone { get; set; }
        public string requestorPhoneIDD { get; set; }
        public string requestorUserId { get; set; }
        public string requestTime { get; set; }
        public string requiredInspection { get; set; }
        public string resultComment { get; set; }
        public string resultType { get; set; }
        public DateTime scheduleDate { get; set; }
        public string scheduleEndAMPM { get; set; }
        public string scheduleEndTime { get; set; }
        public string scheduleStartAMPM { get; set; }
        public string scheduleStartTime { get; set; }
        public string serviceProviderCode { get; set; }
        public float startMileage { get; set; }
        public DateTime startTime { get; set; }
        public Status status { get; set; }
        public string submitAMPM { get; set; }
        public DateTime submitDate { get; set; }
        public string submitTime { get; set; }
        public float totalMileage { get; set; }
        public long totalScore { get; set; }
        public double totalTime { get; set; }
        public InspectionType type { get; set; }
        public string unitNumber { get; set; }
        public double units { get; set; }
        public string vehicleId { get; set; }
    }

    public class Inspection2
    {
        public string recordId { get; set; }
        public List<InspectionType> inspectionTypes { get; set; }

    }

    public class InspectionType
    {
        public string value { get; set; }
        public long id { get; set; }
        public string group { get; set; }
        public string text { get; set; }
        public string ivrNumber { get; set; }
        public string priority { get; set; }
        public string totalScoreOption { get; set; }
        public string publicVisible { get; set; }
        public string isRequired { get; set; }
        public double units { get; set; }
        public string isAutoAssign { get; set; }
        public string inspectionEditable { get; set; }
        public string flowEnabledFlag { get; set; }
        public string allowMultiInspections { get; set; }
        public string resultGroup { get; set; }
        public string hasFlowFlag { get; set; }
        public string allowFailChecklistItems { get; set; }
        public GuideType guideGroup { get; set; }
        public string hasNextInspectionAdvance { get; set; }
        public List<string> disciplines { get; set; }
    }

    public class InspectionID
    {
        public string INSPECTION_ID { get; set; }
    }

    public class Checklist
    {
        public string checklistDesc { get; set; }
        public string checklistStatus { get; set; }
        public string customId { get; set; }
        public string defaultOrderBy { get; set; }
        public string entityType { get; set; }
        public string group { get; set; }
        public GuideType guideType { get; set; }
        public long id { get; set; }
        public string inspectionId { get; set; }
        public string isRequired { get; set; }
        public List<Items> items { get; set; }
        public RecordId recordId { get; set; }
        public string serviceProviderCode { get; set; }
    }

    public class Items
    {
        public string checklist { get; set; }
        public long checklistId { get; set; }
        public ChecklistItem checklistItem { get; set; }
        public Comment comment { get; set; }
        public string customId { get; set; }
        public long displayOrder { get; set; }
        public long id { get; set; }
        public string isCritical { get; set; }
        public string isMajorViolation { get; set; }
        public long score { get; set; }
        public string serviceProviderCode { get; set; }
        public Status status { get; set; }
    }

    public class GuideType
    {
        public string text { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }
    public class ChecklistItem
    {
        public string text { get; set; }
        public string value { get; set; }
    }

    public class InspectionInfoIVR
    {
        public string IVR_NUMBER { get; set; }
        public string CATEGORY_NAME { get; set; }
    }
}
