
namespace LearningManagementSystem.Core.SystemEnums
{
    public class GeneralEnums
    {
        public enum ExpulsionStatus
        {
            Expel = 1,
            Readmit = 2
        }

        public enum PaymentType
        {
            SenangPay = 1,
            Invoice = 2,
            Balance = 3,
        }

        public enum FollowUpType
        {
            Fetch_Transfer = 1,
            From_Follow_Up = 2,
            From_Student = 3,
        }

        public enum PageEnum
        {
            UserProfile = 7,
            Lookup = 19,
            SystemSetting = 21,
            ContactUs = 23,
            Permission = 25,
        }

        public enum ItemFilesTypes
        {
            Education = 1,
            Experience = 2,
            Project = 3,
            Service = 4,
            Recommendations = 5,
            Blog = 6
        }

        public enum CommunicationTypes
        {
            Sms = 1,
            Email = 2,
        }

        public enum SenangPayPaymentType
        {
            AddBalance = 1,
            DonateForProjects = 2,
            BuyingCourse = 3,
        } 

        public enum InvoicesPayStatusEnum
        {
            Accepted = 1,
            Pending = 2,
            Rejected = 3
        }

        public enum TestType
        {
            Exam = 1,
            Quiz = 2

        }

        public enum CourseType
        {
            System = 1,
            Admin = 2,
        }

        public enum SliderImageTypes
        {
            HomePageSlider = 1
        }

        public enum SuccessStatusEnum
        {
            Success = 1,
            Failed = 2,
            NotFinished = 3,
        }

        public enum ExamsAndAssignmentsEnums
        {
            Exam = 1,
            PracticalExams = 2,
            Assignments = 3,
        }

        public enum StatusEnum
        {
            Active = 1,
            Deleted = 2,
            Deactive = 3,
            Approved = 4,
            Processing = 5,
            Denied = 6,
            Fetching = 7,
            FollowUp = 8,
            Student = 9,
            Finished = 10,
            Paid = 11,
            Expelled = 12,
            NotPaid = 13,
        }

        public enum ResourceSourceEnum
        {
            ExternalUrl = 1,
            UploadFile = 2
        }
        public enum ResourceTypeEnum
        {

            Doc = 3132,
            Pdf = 3131,
            Video = 3130,
            Other = 3129,
            Image = 3128
        }

        public enum StudentSubscriptionsEnum
        {
            Active = 1,
            Deleted = 2,
            Stoped = 3,
            Finished = 4,
            Delayed = 5,
            Graduate = 6,
            Canceled = 7,
        }

        public enum EmailStatusEnum
        {
            Send = 1,
            NotSend = 2,
        }

        public enum SmsStatusEnum
        {
            Send = 1,
            NotSend = 2,
        }

        public enum AuditStatusEnum
        {
            Active = 1,
            Accounting = 2,
            Logs = 3,
        }

        public enum AuditCategoryEnum
        {
            Subscription = 1,
            Installment = 2,
            Payment = 3,
            Discount = 4,
            Fee = 5,
            FeePayment = 6,
        }

        public enum AuditTypeEnum
        {
            Create = 1,
            Edit = 2,
            Delete = 3,
            Cancel = 4,
            Refund = 5,
        }

        public enum SmsTypeEnum
        {
            Company = 1,
            Identifier = 2,
            Employee = 3,
            DataBank = 4,
            Student = 5,
            FetchData = 6,
        }

        public enum StudentSubscriptionPaymentsEnum
        {
            Active = 1,
            Deleted = 2,
            Canceled = 3,

        }

        public enum StudentSubscriptionInstallmentEnum
        {
            Active = 1,
            Deleted = 2,
            Canceled = 3,
        }

        public enum StudentSubscriptionFeesEnum
        {
            Active = 1,
            Deleted = 2,
            Canceled = 3,
        }

        public enum SectionCourseStatus
        {
            BeingProcessed = 1,
            Deleted = 2,
            Ongoing = 3,
            Done = 4,
        }

        public enum ResultStatusEnum
        {
            Success = 1,
            Failed = 2,
        }

        public enum GroupeTypesEnum
        {
            StrategicMeasures = 1,
            Measures = 2
        }

        public enum TargetTypesEnum
        {
            Monthly = 1,
            Quarter = 2,
            Half = 3,
            Yearly = 4
        }

        public enum UserTypesEnum
        {
            User = 1,
            Admin = 2
        }

        public enum FileEnum
        {
            Image = 1,
            File = 2,
            EnrollStudentCourse = 3,
            InvoiceStudent = 4,
            StudentExamAttachments = 5,
        }
        public enum EmailEnum
        {
            ForgetPassword = 1,
            ErrorEmail = 2,
            NewsLetterEmail = 3,
        }

        public enum MasterLookupCodeEnums
        {
            Languages = 1,
            Gender = 2,
            Pagination = 3,
            Colors = 4,
            Job = 5,
            JobType = 6,
            SettingTypes = 7,
            Jobs = 8,
            GeneralizationTypes = 9,
            ContactTypes = 10,
            CompanyRate = 11,
            EmloyeeNumber = 12,
            ChamberCommerceLevel = 13,
            CompanyType = 14,
            EducationalLevel = 15,
            RightTime = 16,
            PaymentWay = 18,
            CompanyNoteType = 19,
            GeneralSpecialty = 27,
            CourseType = 28,
            CertificateIssued = 29,
            CourseDependency = 30,
            ProgramTypeId = 31,
            MainDrawing = 32,
            CommunicationChannel = 33,
            FirstSubChannel = 34,
            SecondSubChannel = 35,
            Campaign = 36,
            TrainingConsultant = 37,
            CourseTypeDdl = 38,
            HowSerious = 39,
            Weekdays = 40,
            TargetTypeId = 41,
            StartedFinancialMonth = 42,
            TypeEquipment = 1042,
            NoteType = 43,
            CollegeExam = 44,
            AcademicTest = 45,
            Reasons = 46,
            DiscountType = 47,
            SmsMessage = 48,
            CancelStudentSubscriptionReason = 49,
            CalendarType = 50,
            LearningMethod = 51,
            ResourceType = 52,
            CourseAgeGroup = 53,
            PaymentTypeForProject = 3038,
            ProjectStatus = 3039,
            TemplateType = 69,
            SubjectType = 70,
            PracticalQuestionType = 71,
            TrainerRateMeasureType = 72,
            ManagementStandardType = 73,
            AlertType = 74,
        }

        public enum LearningMethodEnum
        {
            Online = 3133,
            Offline = 3134,
            Mix = 3135,
            ElectronicOnce = 3155

        }
        public enum UserEnum
        {
            IsUser = 1,
            NotUser = 2

        }
        public enum RolesEnum
        {
            SuperAdmin = 1,
        }

        public enum LanguageEnum
        {
            Arabic = 7,
            English = 8
        }

        public enum ContactTypeEnum
        {
            Employee = 1,
            Identifier = 2,
            Student = 3,
            Trainer = 4,
            Visetor = 5,
            Advisors = 6,
            DataBank = 7,
            Admin = 8,
            Supervisor = 9
        }

        public enum CourseTypeDdl
        {
            CourseType = 1,
            CertificateIssued = 2,
            CourseDependency = 3
        }

        public enum WeekDaysEnum
        {
            //
            // Summary:
            //     Indicates Sunday.
            Sunday = 21,
            //
            // Summary:
            //     Indicates Monday.
            Monday = 22,
            //
            // Summary:
            //     Indicates Tuesday.
            Tuesday = 23,
            //
            // Summary:
            //     Indicates Wednesday.
            Wednesday = 24,
            //
            // Summary:
            //     Indicates Thursday.
            Thursday = 25,
            //
            // Summary:
            //     Indicates Friday.
            Friday = 26,
            //
            // Summary:
            //     Indicates Saturday.
            Saturday = 20
        }


        public enum QuestionEnum
        {
            Text = 1,
            CheckBox = 2,
            RadioButton = 3,
            UploadAttachment = 4,
        }
    }
}
