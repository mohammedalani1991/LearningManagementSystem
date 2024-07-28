using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;

namespace DataEntity.Models.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel()
        {

        }               

        public int Status { get; set; }
        public int CommunicationsChannel { get; set; }
        public int CommunicationsChannel1 { get; set; }
        public int CommunicationsChannel2 { get; set; }
        public string SearchText { get; set; }
        public string SectionName { get; set; }
        public string User { get; set; }
        public string searchText { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromPublisheDate { get; set; }
        public DateTime ToPublisheDate { get; set; }
        public int Type { get; set; }
        public int? ReportType { get; set; }
        public int Fee { get; set; }
        public int Program { get; set; }
        public int Advisor { get; set; }
        public int? FastSubscription { get; set; }
        public int HowSerious { get; set; }
        public int Reason { get; set; }
        public int Job { get; set; }
        public int JobType { get; set; }
        public int ContactType { get; set; }
        public int CourseNum { get; set; }
        public int Price { get; set; }
        public string Source { get; set; }
        public string CreatedBy { get; set; }
        public DateTime FromDateCancel { get; set; }
        public DateTime ToDateCancel { get; set; }
        public DateTime FromDateFessAddition { get; set; }
        public DateTime ToDateFessAddition { get; set; }
        public int FeeType { get; set; }
        public int Gender { get; set; }
        public int EducationalLevel { get; set; }
        public int IsJudgement { get; set; }
        public string FromToDate { get; set; }
        public string FromToPublisheDate { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public int MarkFrom { get; set; }
        public int MarkTo { get; set; }
        public int Course { get; set; }
        public List<int> Courses { get; set; }
        public int Project { get; set; }
        public int Teacher { get; set; }
        public int Semester { get; set; }
        public int LanguageId { get; set; }
        public int SuccessStatus { get; set; }
        public int AttendanceTo { get; set; }
        public int AttendanceFrom { get; set; }
        public int WarningNumber { get; set; }
        public int CourseCategory { get; set; }
        public int MarkAdoption { get; set; }
        public int CertificateAdoption { get; set; }
        public bool SecondOpen { get; set; }
    }
}
