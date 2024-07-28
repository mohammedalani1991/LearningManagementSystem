using System;

namespace DataEntity.Models.EfModels
{
    public class InstallmentsReport
    {
        public string IdNumber;
        public string StudentName;
        public string PhoneNumber;
        public string SubscriptionName;
        public string SubscriptionType;
        public int SubscriptionPrice;
        public DateTime SubscriptionDate;
        public int InstallmentsCount;
        public int InstallmentsAmount;
        public int RemainingCoursesCount;
        public bool CheckExist;
        public string EmployName;
        public string BranchName;
        public string CommunicationChanel;
        public string SubCommunicationChanel;
        public string SecondCommunicationChanel;
    }
}