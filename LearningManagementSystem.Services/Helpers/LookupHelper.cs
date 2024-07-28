using System;
using System.Collections.Generic;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using LearningManagementSystem.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningManagementSystem.Services.Helpers
{
    public class LookupHelper
    {

        public static List<SuperAdmin> GetSuperAdmins()
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.SuperAdmins.ToList();
            }
        }

        public static string GetFullNameByEmail(string email, int? languageId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var contact = db.UserProfiles.Include(x => x.Contact.ContactTranslations).FirstOrDefault(x => x.Username == email && x.Status != (int)GeneralEnums.StatusEnum.Deleted)?.Contact;

                if (contact == null)
                    return email;

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var trans = contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        contact.FullName = trans.FullName;
                }
                return contact.FullName;
            }
        }

        public static SuperAdminSetting GetSettings()
        {
            using (var db = new LearningManagementSystemContext())
            {
                var setting = db.SuperAdminSettings.FirstOrDefault();
                return setting;
            }
        }

        public static string GetUserRole(string name)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var user = db.AspNetUsers.Include(a => a.AspNetUserRoles).FirstOrDefault(r => r.UserName == name);
                var role = db.AspNetRoles.Find(user?.AspNetUserRoles.FirstOrDefault()?.RoleId);
                return role.Name;
            }
        }

        public static bool CheckIfClassOnline(int couseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var course = db.EnrollTeacherCourses.FirstOrDefault(r => r.Id == couseId && r.Status == (int)GeneralEnums.StatusEnum.Active);
                if (course?.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.ElectronicOnce)
                    return true;
                else
                    return false;
            }
        }



        public static DateTime? ConvertTimeToSystemTimeZone(DateTime? ConverDateTime)
        {

            try
            {
                using (var db = new LearningManagementSystemContext())
                {
                    var setting = db.SystemSettings.FirstOrDefault(r =>
                     r.Name == Constants.SystemSettings.TimeZone && r.Status == (int)GeneralEnums.StatusEnum.Active);

                    if (setting != null && !string.IsNullOrEmpty(setting.Value) && ConverDateTime.HasValue)
                    {
                        var ConverDateTimeWithTimeZone = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                                             ConverDateTime.Value, setting.Value);
                        return ConverDateTimeWithTimeZone;
                    }
                    else
                    {
                        return null;
                    }

                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"ConvertTimeToSystemTimeZone");
                throw new Exception("ConvertTimeToSystemTimeZone");
            }
        }

        public static Employee GetEmployee(int? contectId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var employee = db.Employees.Include(a => a.Contact).FirstOrDefault(r => r.ContactId == contectId);
                return employee;
            }
        }

        public static List<PracticalExam> GetPracticalEnrollmentExam(int? id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var practicalEnrollmentExam = db.PracticalExamCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.CourseId == id).Include(a => a.PracticalExam.PracticalExamTranslations).Where(r => r.PracticalExam.Status == (int)GeneralEnums.StatusEnum.Active);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in practicalEnrollmentExam)
                    {
                        var trans = item.PracticalExam.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.PracticalExam.Name = trans.Name;
                    }

                return practicalEnrollmentExam.Select(r => r.PracticalExam).ToList();
            }
        }

        public static EnrollStudentExam GetEnrollStudentExamByEnrollStudentCourseId(int EnrollStudentCourseId, int EnrollCourseExamId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentExams = db.EnrollStudentExams.OrderByDescending(r => r.Id).Include(e => e.EnrollStudentExamAnswers).FirstOrDefault(s => s.EnrollStudentCourseId == EnrollStudentCourseId && s.EnrollCourseExamId == EnrollCourseExamId && s.Status == (int)GeneralEnums.StatusEnum.Active);
                return EnrollStudentExams;
            }
        }
        public static Employee GetEmployeeById(int? id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var employee = db.Employees.Include(a => a.Contact).FirstOrDefault(r => r.Id == id);
                return employee;
            }
        }
        public static CourseViewModel GetCoursesById(int? id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CourseTranslations.Include(r => r.Course).FirstOrDefault(r => r.LanguageId == languageId && r.Course.Status != (int)GeneralEnums.StatusEnum.Deleted && r.CourseId == id);
                    if (aboutTran != null)
                    {
                        return new CourseViewModel(aboutTran);
                    }
                }
                var course = db.Courses.FirstOrDefault(x => x.Id == id && x.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (course != null)
                    return new CourseViewModel(course);
                else
                    return null;
            }
        }
        public static List<EmployeeViewModel> GetObservers(int? branch)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var master = GetMasterLookupsByCode("Job");
                var telecom = GetLookupDetailsByCode("Telecom", master)?.Id;
                var communicationOfficer = GetLookupDetailsByCode("CommunicationOfficer", master)?.Id;

                var Observers = db.Employees.Include(r => r.Contact).Include(r => r.Contact.ContactTypes)
                          .Where(r => r.Contact.Status == (int)GeneralEnums.StatusEnum.Active && (r.JobId == telecom || r.JobId == communicationOfficer) && r.Contact.ContactTypes.Any(s => s.TypeId == (int)GeneralEnums.ContactTypeEnum.Employee));

                if (branch > 0)
                    Observers = Observers.Where(r => r.Contact.BranchId == branch);

                return Observers.Select(r => new EmployeeViewModel() { Id = r.Contact.Id, Name = r.Contact.FullName, EmployeeId = r.Id }).ToList();
            }
        }

        public static List<EmployeeViewModel> GetAdvisersList()
        {
            using (var db = new LearningManagementSystemContext())
            {
                var Advisers = db.Employees.Include(r => r.Contact).Include(r => r.Contact.ContactTypes)
                          .Where(r => r.Contact.Status == (int)GeneralEnums.StatusEnum.Active && r.Contact.ContactTypes.Any(s => s.TypeId == (int)GeneralEnums.ContactTypeEnum.Advisors));


                return Advisers.Select(r => new EmployeeViewModel() { Id = r.Contact.Id, Name = r.Contact.FullName, EmployeeId = r.Id, Email = r.Contact.Email }).ToList();
            }
        }


        public static List<EmployeeViewModel> GetEnrollStudents(int courseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var Advisers = db.EnrollStudentCourses.Include(r => r.Student.Contact.ContactTranslations)
                          .Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.Student.Contact.Status == (int)GeneralEnums.StatusEnum.Active
                          && r.CourseId == courseId);


                return Advisers.Select(r => new EmployeeViewModel() { Id = r.Id, Name = r.Student.Contact.FullName }).ToList();
            }
        }



        public static List<EmployeeViewModel> GetAllowUsersRate(int langId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var Advisers = db.Contacts.Include(r => r.ContactTypes)
                          .Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.ContactTypes.Any(s => s.TypeId == (int)GeneralEnums.ContactTypeEnum.Employee))
                          .Select(r => new EmployeeViewModel() { Id = r.Id, Name = r.FullName, Email = r.Email }).ToList();

                return Advisers;
            }
        }

        public static List<StatusViewModel> GetTestType(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.TestType.Exam,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Exam" : "اختبار"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.TestType.Quiz,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Quiz" : "مسابقة"
                }
            };
        }

        public static List<StatusViewModel> GetStatuses(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Active,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Active" : "فعال"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Deactive,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Deactive" : "معطل"
                }
            };
        }

        public static List<StatusViewModel> GetFilterdStatuses(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Active,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Active" : "فعال"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Deactive,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Deactive" : "معطل"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Expelled,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Expelled" : "مطرود"
                }
                //new StatusViewModel()
                //{
                //    Id = (int) GeneralEnums.StatusEnum.Expelled,
                //    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Expelled" : "مطرود"
                //}
            };
        }

        public static List<StatusViewModel> GetSuccessStatuses(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SuccessStatusEnum.Success,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Success" : "نجح"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SuccessStatusEnum.Failed,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Failed" : "رسب"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SuccessStatusEnum.NotFinished,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Not Finished" : "غير منتهي"
                }
            };
        }

        public static List<StatusViewModel> GetPaymentStatuses(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Paid,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Paid" : "مدفوع"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.NotPaid,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Not Paid" : "غير مدفوع"
                }
            };
        }

        public static List<StatusViewModel> GetCourseType(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.CourseType.System,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Calculate Automatically" : "الحساب تلقائيا"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.CourseType.Admin,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Calculate After Approval" : "الحساب بعد الموافقة"
                }
            };
        }

        public static List<StatusViewModel> GetDaysOfTheWeek(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.WeekDaysEnum.Saturday,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Saturday" : "السبت"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.WeekDaysEnum.Sunday,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Sunday" : "الأحد"
                },
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.WeekDaysEnum.Monday,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Monday" : "الاثنين"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.WeekDaysEnum.Tuesday,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Tuesday" : "الثلاثاء"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.WeekDaysEnum.Wednesday,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Wednesday" : "الأربعاء"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.WeekDaysEnum.Thursday,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Thursday" : "الخميس"
                },
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.WeekDaysEnum.Friday,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Friday" : "الجمعة"
                },
            };
        }
        public static StatusViewModel GetDaysOfTheWeekById(int id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            return GetDaysOfTheWeek(languageId).FirstOrDefault(r => r.Id == id);
        }

        public static List<StatusViewModel> GetStatusesToFilter(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Processing,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Processing" : "يتم المعالجة"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.FollowUp,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Follow Up" : "متابعة"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Fetching,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Fetching" : "يتم الجلب"
                }
            };
        }

        public static List<StatusViewModel> GetSubscriptionStatuses(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionsEnum.Active,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Active" : "فعال"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionsEnum.Stoped,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Stoped" : "متوقف"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionsEnum.Finished,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Finished" : "منتهي"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionsEnum.Delayed,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Delayed" : "مؤجل"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionsEnum.Graduate,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Graduate" : "متخرج"
                }
            };
        }

        public static List<StatusViewModel> GetSMSTypes(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsTypeEnum.Student,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Student" : "طالب"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsTypeEnum.DataBank,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "DataBank" : "بنك البيانات"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsTypeEnum.Employee,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Employee" : "موظف"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsTypeEnum.Company,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Company" : "شركة"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsTypeEnum.Identifier,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Identifier" : "معرف الشركة"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsTypeEnum.FetchData,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Fetch Data" : "جلب البيانات"
                }
            };
        }


        public static List<StatusViewModel> GetSMSourses(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.Company,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.Company : "صفحة الشركات"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.Identifier,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.Identifier : "صفحة المعرفين في الشركات"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.Student,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.Student : "صفحة الطالب"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.Employee,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.Employee : "صفحة الموظفين في مركز الإتصال"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.DataBank,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.DataBank : "صفحة بنك البيانات في المبيعات"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.DataBankCallCenter,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.DataBankCallCenter : "صفحة المتابعات  في مركز الإتصال"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.StudentCallCenter,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.StudentCallCenter : "صفحة  متابعات الطلاب في مركز الإتصال"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.StudentSales,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.StudentSales : "صفحة الطلاب في المبيعات"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.StudentAutomatic,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.StudentAutomatic : "رسائل الطالب التلقائية"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.StudentOperation,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.StudentOperation : "ملخصات العمليات"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.Users,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.Users : "صفحة المستخدمين"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.IntrestReport,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.IntrestReport : "تقرير الإهتمام"
                },
                 new StatusViewModel()
                {
                    Code = Constants.SmsSource.SelesFetchData,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? Constants.SmsSource.SelesFetchData : "جلب البيانات داخل المبيعات"
                }
            };
        }

        public static List<StatusViewModel> GetSMSStatuses(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsStatusEnum.Send,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Sent" : "تم الإرسال"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.SmsStatusEnum.NotSend,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Not Sent" : "فشل الإرسال"
                }
            };
        }

        public static List<StatusViewModel> GetQuestionsType(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.QuestionEnum.Text,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Text" : "تعبئة الفراغ"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.QuestionEnum.CheckBox,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "CheckBox" : "متعددة الإجابات "
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.QuestionEnum.RadioButton,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "RadioButton" : "الاختيار من متعدد"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.QuestionEnum.UploadAttachment,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Upload Attachment" : "رفع مرفق"
                }
            };
        }

        public static List<AspNetRole> GetActiveRoles(int roleId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var roles = db.AspNetRoles.ToList();
                if (roleId != 1)
                    roles = db.AspNetRoles.Where(r => r.Id != "1").ToList();
                return roles;
            }
        }
        public static StatusViewModel GetTestTypeById(int id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            var s = new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.TestType.Exam,
                     Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Exam" : "اختبار"
                },
                  new StatusViewModel()
                {
                  Id = (int) GeneralEnums.TestType.Quiz,
                   Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Quiz" : "مسابقة"
                },

            };
            return s.FirstOrDefault(r => r.Id == id);
        }

        public static StatusViewModel GetExpulsionStatusById(int id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            var s = new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.ExpulsionStatus.Expel,
                     Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Expelled" : "استبعاد"
                },
                  new StatusViewModel()
                {
                  Id = (int) GeneralEnums.ExpulsionStatus.Readmit,
                   Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Readmit" : "إعادة الاضافة"
                },

            };
            return s.FirstOrDefault(r => r.Id == id);

        }

        public static StatusViewModel GetStatusById(int id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            var x = GetStatuses(languageId).FirstOrDefault(r => r.Id == id);
            if (x != null)
                return x;
            var s = new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Processing,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Processing" : "يتم المعالجة"
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.FollowUp,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Follow Up" : "متابعة"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Fetching,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Fetching" : "الجلب"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Expelled,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Expelled" : "مطرود"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.Paid,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Paid" : "مدفوع"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StatusEnum.NotPaid,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Not Paid" : "غير مدفوع"
                }
            };
            x = s.FirstOrDefault(r => r.Id == id);
            return x;
        }
        public static StatusViewModel GetInvoicesPayStatusById(int id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            var s = new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.InvoicesPayStatusEnum.Accepted,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Accepted" : "مقبول"
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.InvoicesPayStatusEnum.Pending,
                    Name=languageId == CultureHelper.GetDefaultLanguageId() ? "Pending" : "قيد الانتظار"
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.InvoicesPayStatusEnum.Rejected,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Rejected" : "مرفوض"
                },
            };
            return s.FirstOrDefault(r => r.Id == id);
        }

        public static StatusViewModel GetSubscrptionStatusById(int id)
        {
            var s = new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionsEnum.Active,
                    Name = "Active"// Resources.Status.Active
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionsEnum.Canceled,
                    Name = "Canceled"// Resources.Status.Active
                },
            };
            return s.FirstOrDefault(r => r.Id == id);
        }

        public static StatusViewModel GetSubscrptionInstalmentStatusById(int id)
        {
            var s = new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionInstallmentEnum.Active,
                    Name = "Active"// Resources.Status.Active
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.StudentSubscriptionInstallmentEnum.Canceled,
                    Name = "Canceled"// Resources.Status.Active
                },
            };
            return s.FirstOrDefault(r => r.Id == id);
        }

        public static StatusViewModel GetEmailStatusById(int id)
        {
            var x = GetStatuses().FirstOrDefault(r => r.Id == id);
            if (x != null)
                return x;
            var s = new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.EmailStatusEnum.Send,
                    Name = "Send"// Resources.Status.Active
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.EmailStatusEnum.NotSend,
                    Name = "Not Send"// Resources.Status.Active
                },
            };
            x = s.FirstOrDefault(r => r.Id == id);
            return x;
        }

        public static DetailsLookupViewModel GetLookupDetailsById(int lookupId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var lookups = db.DetailsLookups.Include(e => e.DetailsLookupTranslations).SingleOrDefault(r => r.Id == lookupId && r.Status == (int)GeneralEnums.StatusEnum.Active);
                    if (lookups != null)
                    {
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var trans = lookups.DetailsLookupTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (trans != null)
                            {
                                lookups.Value = trans.Value;
                            }
                        }
                        var output = new DetailsLookupViewModel()
                        {
                            MasterId = lookups.MasterId,
                            Id = lookups.Id,
                            Name = lookups.Value,
                            Code = lookups.Code
                        } ?? LanguageFallbackHelper.GetDefaultLookupDetailsById(lookupId);
                        return output;
                    }
                    else
                    {
                        return LanguageFallbackHelper.GetDefaultLookupDetailsById(lookupId);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details By lookup Id {lookupId}");
                    return new DetailsLookupViewModel();
                }

            }
        }

        public static int GetMasterLookupsByCode(string code)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var lookups = db.MasterLookups.FirstOrDefault(r => r.Code == code && r.Status == (int)GeneralEnums.StatusEnum.Active);

                    return lookups?.Id ?? 0;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Master Lookups By lookup name {code}");
                    return 0;
                }

            }
        }

        public static int GetLookupDetailsByName(string name)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var lookups = db.DetailsLookups.FirstOrDefault(r => r.Value == name && r.Status == (int)GeneralEnums.StatusEnum.Active);

                    return lookups?.Id ?? 0;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details By lookup name {name}");
                    return 0;
                }

            }
        }

        public static List<ContactTypeSelectList> GetContactType(int? languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                return new List<ContactTypeSelectList> {
              new ContactTypeSelectList() { Selected = false, Text = "طالب", Value = (int)GeneralEnums.ContactTypeEnum.Student , DivInfo = "StudentInfo" } ,
              new ContactTypeSelectList() { Selected = false, Text = "مدرب", Value = (int)GeneralEnums.ContactTypeEnum.Trainer , DivInfo = "TrainerInfo" } ,
              new ContactTypeSelectList() { Selected = false, Text = "موظف", Value = (int)GeneralEnums.ContactTypeEnum.Employee , DivInfo = "EmployeeInfo" } ,
              new ContactTypeSelectList() { Selected = false, Text = "مسؤل", Value = (int)GeneralEnums.ContactTypeEnum.Admin , DivInfo = "" } ,
                    //new ContactTypeSelectList() { Selected = false, Text = "Supervisor", Value = (int)GeneralEnums.ContactTypeEnum.Supervisor , DivInfo = "" } ,
                };
            }
            else
            {
                return new List<ContactTypeSelectList> {
              new ContactTypeSelectList() { Selected = false, Text = "Student", Value = (int)GeneralEnums.ContactTypeEnum.Student , DivInfo = "StudentInfo" } ,
              new ContactTypeSelectList() { Selected = false, Text = "Trainer", Value = (int)GeneralEnums.ContactTypeEnum.Trainer , DivInfo = "TrainerInfo" } ,
              new ContactTypeSelectList() { Selected = false, Text = "Employee", Value = (int)GeneralEnums.ContactTypeEnum.Employee , DivInfo = "EmployeeInfo" } ,
              new ContactTypeSelectList() { Selected = false, Text = "Admin", Value = (int)GeneralEnums.ContactTypeEnum.Admin , DivInfo = "" } ,
                    //new ContactTypeSelectList() { Selected = false, Text = "Supervisor", Value = (int)GeneralEnums.ContactTypeEnum.Supervisor , DivInfo = "" } ,
                };
            }
        }

        public static List<StatusViewModel> GetVerifyEmailList()
        {
            var list = new List<StatusViewModel>() {
            new StatusViewModel(){Id=0, Name="False" },
            new StatusViewModel(){Id=1, Name="True" },
            };
            return list;
        }

        public static Dictionary<int, string> GetContactIndexPagesTitle(int? languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            var titels = new Dictionary<int, string>();
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                titels.Add((int)GeneralEnums.ContactTypeEnum.Student, "طالب");
                titels.Add((int)GeneralEnums.ContactTypeEnum.Trainer, "مدرب");
                titels.Add((int)GeneralEnums.ContactTypeEnum.Employee, "موظف");
                titels.Add((int)GeneralEnums.ContactTypeEnum.Admin, "مسؤل");
                //titels.Add((int)GeneralEnums.ContactTypeEnum.Supervisor, "Supervisor");
            }
            else
            {
                titels.Add((int)GeneralEnums.ContactTypeEnum.Student, "Students");
                titels.Add((int)GeneralEnums.ContactTypeEnum.Trainer, "Trainers");
                titels.Add((int)GeneralEnums.ContactTypeEnum.Employee, "Employee");
                titels.Add((int)GeneralEnums.ContactTypeEnum.Admin, "Admin");
            }
            return titels;
        }

        public static Dictionary<int, string> GetContactCrearePagesTitle()
        {
            var titels = new Dictionary<int, string>();
            titels.Add((int)GeneralEnums.ContactTypeEnum.Employee, "Create Employee");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Identifier, "Create Identifier");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Student, "Create Student");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Trainer, "Create Trainer");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Visetor, "Create Lead");
            titels.Add((int)GeneralEnums.ContactTypeEnum.DataBank, "Create Date Bank");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Advisors, "Create Advisor");
            return titels;
        }


        public static Dictionary<int, string> GetContactEditPagesTitle()
        {
            var titels = new Dictionary<int, string>();
            titels.Add((int)GeneralEnums.ContactTypeEnum.Employee, "Edit Employee");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Identifier, "Edit Identifier");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Student, "Edit Student");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Trainer, "Edit Trainer");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Visetor, "Edit Lead");
            titels.Add((int)GeneralEnums.ContactTypeEnum.DataBank, "Edit Date Bank");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Advisors, "Edit Advisor");
            return titels;
        }

        public static Dictionary<int, string> GetContactDeletePagesTitle()
        {
            var titels = new Dictionary<int, string>();
            titels.Add((int)GeneralEnums.ContactTypeEnum.Employee, "Delete Employee");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Identifier, "Delete Identifier");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Student, "Delete Student");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Trainer, "Delete Trainer");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Visetor, "Delete Lead");
            titels.Add((int)GeneralEnums.ContactTypeEnum.DataBank, "Delete Date Bank");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Advisors, "Delete Advisor");
            return titels;
        }

        public static Dictionary<int, string> GetContactDetailsPagesTitle()
        {
            var titels = new Dictionary<int, string>();
            titels.Add((int)GeneralEnums.ContactTypeEnum.Employee, "Employee Details");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Identifier, "Identifier Details");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Student, "Student Details");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Trainer, "Trainer Details");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Visetor, "Lead Details");
            titels.Add((int)GeneralEnums.ContactTypeEnum.DataBank, "Lead Date Bank");
            titels.Add((int)GeneralEnums.ContactTypeEnum.Advisors, "Advisor Details");
            return titels;
        }

        public static string GetLookupDetailsByName1(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var lookups = db.DetailsLookups.FirstOrDefault(r => r.Id == id && r.Status == (int)GeneralEnums.StatusEnum.Active);

                    return lookups?.Value;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details By lookup id {id}");
                    return null;
                }

            }
        }

        public static DetailsLookupViewModel GetLookupDetailsByCode(string type, int masterId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    DetailsLookupViewModel lookup;
                    lookup = db.DetailsLookups.Where(r =>
                                r.Code == type && r.MasterId == masterId && r.Status == (int)GeneralEnums.StatusEnum.Active)
                            .Select(x => new DetailsLookupViewModel()
                            {
                                Id = x.Id,
                                Name = x.Value,
                                Status = x.Status,
                                Code = x.Code,
                            }).FirstOrDefault();
                    return lookup;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details");
                    return new DetailsLookupViewModel();
                }
            }
        }

        public static DetailsLookupViewModel GetLookupDetailsByCode(string code)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    DetailsLookupViewModel lookup;
                    lookup = db.DetailsLookups.Where(r =>
                                r.Code == code && r.Status == (int)GeneralEnums.StatusEnum.Active)
                            .Select(x => new DetailsLookupViewModel()
                            {
                                Id = x.Id,
                                Name = x.Value,
                                Status = x.Status,
                                Code = x.Code,
                            }).FirstOrDefault();
                    return lookup;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details");
                    return new DetailsLookupViewModel();
                }
            }
        }

        public static List<DetailsLookupViewModel> GetLookupDetailsByMasterId(int masterLookupId, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    List<DetailsLookupViewModel> lookups;

                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        lookups = db.DetailsLookupTranslations.Where(r =>
                                r.DetailsLookup.MasterId == masterLookupId && r.DetailsLookup.Status == (int)GeneralEnums.StatusEnum.Active && r.LanguageId == languageId)
                            .Select(x => new DetailsLookupViewModel
                            {
                                Id = x.DetailsLookupId,
                                Name = x.Value,
                                Status = x.DetailsLookup.Status,
                                Code = x.DetailsLookup.Code,
                            }).ToList();
                        var ids = lookups.Select(r => r.Id).ToList();
                        lookups.AddRange(db.DetailsLookups.Where(r => !ids.Contains(r.Id) &&
                                r.MasterId == masterLookupId && r.Status == (int)GeneralEnums.StatusEnum.Active)
                            .Select(x => new DetailsLookupViewModel
                            {
                                Id = x.Id,
                                Name = x.Value,
                                Status = x.Status,
                                Code = x.Code,
                            }).ToList());
                    }
                    else
                    {
                        lookups = db.DetailsLookups.Where(r =>
                                r.MasterId == masterLookupId && r.Status == (int)GeneralEnums.StatusEnum.Active)
                            .Select(x => new DetailsLookupViewModel()
                            {
                                Id = x.Id,
                                Name = x.Value,
                                Status = x.Status,
                                Code = x.Code,
                            }).ToList();
                    }

                    return lookups;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details By Master lookup Id {masterLookupId}");
                    return new List<DetailsLookupViewModel>();
                }
            }
        }

        public static List<CommunicationChannelViewModel> GetCommunicationChannel(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    List<CommunicationChannelViewModel> lookups;

                    lookups = db.CommunicationChannels.Include(r => r.CommunicationChannelTranslations).Where(r =>
                                  r.Status == (int)GeneralEnums.StatusEnum.Active)
                             .Select(x => new CommunicationChannelViewModel
                             {
                                 Id = x.Id,
                                 Name = languageId != CultureHelper.GetDefaultLanguageId() ? x.CommunicationChannelTranslations.FirstOrDefault().Name ?? x.Name : x.Name,
                                 Status = x.Status,
                             }).ToList();

                    return lookups;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Communication Channels");
                    return new List<CommunicationChannelViewModel>();
                }
            }
        }

        public static List<CommunicationChannelViewModel> GetSubChannels(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    List<CommunicationChannelViewModel> lookups;

                    lookups = db.SubCommunicationChannels.Include(r => r.SubCommunicationChannelTranslations).Where(r =>
                                  r.Status == (int)GeneralEnums.StatusEnum.Active)
                             .Select(x => new CommunicationChannelViewModel
                             {
                                 Id = x.Id,
                                 Name = languageId != CultureHelper.GetDefaultLanguageId() ? x.SubCommunicationChannelTranslations.FirstOrDefault().Name ?? x.Name : x.Name,
                                 Status = x.Status,
                             }).ToList();

                    return lookups;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Sub Communication Channels");
                    return new List<CommunicationChannelViewModel>();
                }
            }
        }

        public static int GetCommunicationChannelId(string name)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var lookups = db.CommunicationChannels.FirstOrDefault(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.Name == name);
                    if (lookups == null)
                    {
                        var lookupTran = db.CommunicationChannelTranslations.FirstOrDefault(r => r.CommunicationChannel.Status == (int)GeneralEnums.StatusEnum.Active && r.Name == name);
                        return lookupTran.CommunicationChannelId;
                    }
                    return lookups.Id;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Communication Channels");
                    return 0;
                }
            }
        }

        public static List<DetailsLookupViewModel> GetLookupDetailsByMasterCode(string masterLookupCode, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    List<DetailsLookupViewModel> lookups = new List<DetailsLookupViewModel>();
                    var masterLookup = db.MasterLookups.FirstOrDefault(m => m.Code.Equals(masterLookupCode));

                    if (masterLookup != null)
                    {
                        if (masterLookupCode == GeneralEnums.MasterLookupCodeEnums.Pagination.ToString())
                        {
                            lookups = db.DetailsLookups.Where(r =>
                                   r.MasterId == masterLookup.Id && r.Status == (int)GeneralEnums.StatusEnum.Active).OrderBy(o => o.Code.Length).ThenBy(o => o.Code)
                               .Select(x => new DetailsLookupViewModel(x)).ToList();

                        }
                        else
                        {
                            if (languageId != CultureHelper.GetDefaultLanguageId())
                            {
                                lookups = db.DetailsLookupTranslations.Where(r =>
                                        r.DetailsLookup.MasterId == masterLookup.Id && r.DetailsLookup.Status == (int)GeneralEnums.StatusEnum.Active && r.LanguageId == languageId)
                                    .Select(x => new DetailsLookupViewModel
                                    {
                                        Id = x.DetailsLookupId,
                                        Name = x.Value,
                                        Status = x.DetailsLookup.Status,
                                        Code = x.DetailsLookup.Code,
                                    }).ToList();
                                var ids = lookups.Select(r => r.Id).ToList();
                                lookups.AddRange(db.DetailsLookups.Where(r => !ids.Contains(r.Id) &&
                                        r.MasterId == masterLookup.Id && r.Status == (int)GeneralEnums.StatusEnum.Active)
                                    .Select(x => new DetailsLookupViewModel(x)).ToList());
                            }
                            else
                            {


                                lookups = db.DetailsLookups.Where(r =>
                                        r.MasterId == masterLookup.Id && r.Status == (int)GeneralEnums.StatusEnum.Active)
                                    .Select(x => new DetailsLookupViewModel(x)).ToList();
                            }
                        }
                    }
                    return lookups;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details By Master lookup Code {masterLookupCode}");
                    return new List<DetailsLookupViewModel>();
                }
            }
        }

        public static List<DetailsLookupViewModel> GetYears()
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    List<DetailsLookupViewModel> year = new List<DetailsLookupViewModel> { new DetailsLookupViewModel { Name = " " } };
                    year = Enumerable.Range(2010, DateTime.Now.Year + 10).Select(a => new DetailsLookupViewModel
                    {
                        Id = a,
                        Name = (a.ToString() + '-' + (a + 1).ToString())
                    }).ToList();

                    return year;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Years");
                    return new List<DetailsLookupViewModel>();
                }
            }
        }

        public static Dictionary<string, List<DetailsLookupViewModel>> GetLookupDetailsByMasterCode(HashSet<string> masterLookupCodeList, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    List<DetailsLookupViewModel> lookups = new List<DetailsLookupViewModel>();
                    var detailLookups = db.DetailsLookups.Include(r => r.DetailsLookupTranslations)
                        .Where(m => m.Master.Status != (int)GeneralEnums.StatusEnum.Deleted && masterLookupCodeList.Contains(m.Master.Code) && m.Status == (int)GeneralEnums.StatusEnum.Active);

                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        lookups = detailLookups
                            .Select(x => new DetailsLookupViewModel()
                            {
                                Id = x.Id,
                                Name = x.DetailsLookupTranslations.FirstOrDefault().Value ?? x.Value,
                                Status = x.Status,
                                Code = x.Code,
                                MasterCode = x.Master.Code
                            }).ToList();
                    }
                    else
                    {
                        lookups = detailLookups
                            .Select(x => new DetailsLookupViewModel()
                            {
                                Id = x.Id,
                                Name = x.Value,
                                Status = x.Status,
                                Code = x.Code,
                                MasterCode = x.Master.Code
                            }).ToList();
                    }

                    var result = lookups.GroupBy(r => r.MasterCode).ToDictionary(x => x.Key, r => r.ToList());

                    if (masterLookupCodeList.Any(m => !result.Keys.Contains(m)))
                    {
                        var emptyMasterCode = masterLookupCodeList.Where(m => !result.Keys.Contains(m));
                        foreach (var item in emptyMasterCode)
                        {
                            result.Add(item, new List<DetailsLookupViewModel>());
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Lookup Details By Master lookups");
                    return new Dictionary<string, List<DetailsLookupViewModel>>();
                }
            }
        }

        public static List<ModuleViewModel> GetModules(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var modules = db.Modules.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var output = modules.ToList();
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        foreach (var item in output)
                        {
                            var trans = db.ModuleTranslations.FirstOrDefault(r => r.ModuleId == item.Id && r.LanguageId == languageId);
                            if (trans != null)
                            {
                                item.Name = trans.Name;
                                item.Description = trans.Description;
                            }
                        }
                    }
                    return output.Select(x => new ModuleViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code
                    }).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Modules");
                    return new List<ModuleViewModel>();
                }
            }
        }

        public static List<BranchViewModel> GetBranches(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var branches = db.Branches.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var output = branches.ToList();
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        foreach (var item in output)
                        {
                            var trans = db.BranchTranslations.FirstOrDefault(r => r.BranchId == item.Id && r.LanguageId == languageId);
                            if (trans != null)
                            {
                                item.Name = trans.Name;
                            }
                        }
                    }
                    return output.Select(x => new BranchViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code
                    }).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Branches");
                    return new List<BranchViewModel>();
                }
            }
        }
        public static List<DetailsLookupViewModel> GetCountries(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var countries = db.Countries.Include(s => s.CountryTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                        foreach (var item in countries)
                        {
                            var trans = db.CountryTranslations.FirstOrDefault(r => r.CountryId == item.Id && r.LanguageId == languageId);
                            if (trans != null)
                                item.Name = trans.Name;
                        }

                    var output = countries.ToList();

                    if (languageId != CultureHelper.GetDefaultLanguageId())
                        output = output.OrderBy(r => r.Name, StringComparer.Create(new CultureInfo("ar-SA"), true)).ToList();
                    else
                        output = output.OrderBy(r => r.Name).ToList();

                    return output.Select(x => new DetailsLookupViewModel() { Id = x.Id, Name = x.Name }).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Country");
                    return new List<DetailsLookupViewModel>();
                }
            }
        }
        public static List<DetailsLookupViewModel> GetCities(int languageId, int? countryId = null)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var cities = db.Cities.Include(s => s.CityTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);

                    if (countryId != null)
                        cities = cities.Where(s => s.CountryId == countryId);

                    if (languageId != CultureHelper.GetDefaultLanguageId())
                        foreach (var item in cities)
                        {
                            var trans = db.CityTranslations.FirstOrDefault(r => r.CityId == item.Id && r.LanguageId == languageId);
                            if (trans != null)
                                item.Name = trans.Name;
                        }

                    var output = cities.ToList();

                    if (languageId != CultureHelper.GetDefaultLanguageId())
                        output = output.OrderBy(r => r.Name, StringComparer.Create(new CultureInfo("ar-SA"), true)).ToList();
                    else
                        output = output.OrderBy(r => r.Name).ToList();

                    return output.Select(x => new DetailsLookupViewModel() { Id = x.Id, Name = x.Name }).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting City");
                    return new List<DetailsLookupViewModel>();
                }
            }
        }

        public static string GetCountryById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var country = db.Countries.Include(s => s.CountryTranslations).FirstOrDefault(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.Id == id);
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var trans = country.CountryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            country.Name = trans.Name;
                    }

                    return country.Name;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Country");
                    return null;
                }
            }
        }
        public static string GetCityById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var city = db.Cities.Include(s => s.CityTranslations).FirstOrDefault(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.Id == id);
                    if (city == null) return "";
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        var trans = city.CityTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            city.Name = trans.Name;
                    }
                    return city.Name;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting City");
                    return null;
                }
            }
        }
        public static List<DetailsLookupViewModel> GetEnrollTeacherCourse(int languageId, int courseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var branches = db.EnrollTeacherCourses.Where(r => r.CourseId == courseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(s => s.EnrollTeacherCourseTranlations)
                        .Include(r => r.Teacher.Contact.ContactTranslations).Include(r => r.Course.CourseTranslations).AsQueryable();
                    var output = branches.ToList();
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                        foreach (var item in output)
                        {
                            var trans = item?.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                            var trans1 = item.Teacher?.Contact?.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            var trans2 = item.Course?.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (trans != null)
                                item.SectionName = trans.SectionName;
                            if (trans1 != null)
                                item.Teacher.Contact.FullName = trans1.FullName;
                            if (trans2 != null)
                                item.Course.CourseName = trans2.CourseName;
                        }
                    return output.Select(x => new DetailsLookupViewModel() { Id = x.Id, Name = x.Course?.CourseName + (!string.IsNullOrEmpty(x?.Teacher?.Contact?.FullName) ? " / " : "") + x.Teacher?.Contact?.FullName + (!string.IsNullOrEmpty(x.SectionName) ? " / " : "") + x.SectionName }).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting City");
                    return new List<DetailsLookupViewModel>();
                }
            }
        }

        public static List<int> GetFullCourses(int courseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCoursesIds = new List<int>();

                var enrollTeacherCourses = db.EnrollTeacherCourses.Where(r => r.CourseId == courseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                var enrollStudentCourses = db.EnrollStudentCourses.Where(s => enrollTeacherCourses.Select(s => s.Id).Contains(s.CourseId) && s.Status == (int)GeneralEnums.StatusEnum.Active).ToList();

                foreach (var item in enrollTeacherCourses)
                {
                    var enrolsCount = enrollStudentCourses.Count(s => s.CourseId == item.Id);
                    if ((item.CountOfStudent != null && (item.CountOfStudent <= enrolsCount) || (item.PublicationEndDate <= DateTime.Now || item.PublicationDate >= DateTime.Now)))
                    {
                        enrollTeacherCoursesIds.Add(item.Id);
                    }
                }

                return enrollTeacherCoursesIds;
            }
        }


        public static List<NotificationViewModel> GetNotifications(int contactId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var notifications = db.Notifications.Include(a => a.Generalization).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.ContactId == contactId && r.IsRead == false);
                    var output = notifications.ToList();
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        foreach (var item in output)
                        {
                            var trans = db.GeneralizationTranslations.FirstOrDefault(r => r.GeneralizationId == item.GeneralizationId && r.LanguageId == languageId);
                            if (trans != null)
                            {
                                item.Generalization.Title = trans.Title;
                                item.Generalization.Description = trans.Description;
                            }
                        }
                    }
                    return output.Select(x => new NotificationViewModel()
                    {
                        Id = x.Id,
                        Title = x.Generalization.Title,
                        Description = x.Generalization.Description,
                        IsRead = x.IsRead,
                        CreatedOn = x.CreatedOn,
                        CreatedBy = GetContactByUserName(x.CreatedBy).FirstName + " " + GetContactByUserName(x.CreatedBy).LastName,
                    }).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Notifications");
                    return new List<NotificationViewModel>();
                }
            }
        }

        public static Branch GetBrancheName(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var branche = db.Branches.Include(r => r.BranchTranslations).FirstOrDefault(r => r.Id == id);
                    if (branche != null)
                        branche.Name = languageId != CultureHelper.GetDefaultLanguageId() ? branche.BranchTranslations.FirstOrDefault()?.Name ?? branche.Name : branche.Name;

                    return branche;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Branche By Id");
                    return null;
                }
            }
        }
        public static int GetBrancheByName(string name)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var branche = db.Branches.Include(r => r.BranchTranslations).FirstOrDefault(r => r.Name == name);
                    return branche?.Id ?? 0;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Branche By Id");
                    return 0;
                }
            }
        }

        public static int GetBrancheByCode(string code)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var branche = db.Branches.Include(r => r.BranchTranslations).FirstOrDefault(r => r.Code == code);
                    return branche?.Id ?? 0;
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Branche By Id");
                    return 0;
                }
            }
        }

        public static List<Contact> GetEmployeeContacts(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var contacts = db.Contacts.Include(c => c.ContactTranslations).Include(c => c.Employees).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted
                                     && r.ContactTypes.Select(c => c.TypeId).Contains((int)GeneralEnums.ContactTypeEnum.Employee));
                    var output = contacts.ToList();
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        foreach (var item in output)
                        {
                            var trans = db.ContactTranslations.FirstOrDefault(r => r.ContactId == item.Id && r.LanguageId == languageId);
                            if (trans != null)
                            {
                                item.FullName = trans.FullName;
                            }
                        }
                    }
                    return output.ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Contact");
                    return new List<Contact>();
                }
            }
        }

        public static List<ContactViewModel> GetContacts(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var contacts = db.Contacts.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(e => e.ContactTranslations).ToList();
                    var output = contacts.ToList();
                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        foreach (var item in output)
                        {
                            var trans = db.ContactTranslations.FirstOrDefault(r => r.ContactId == item.Id && r.LanguageId == languageId);
                            if (trans != null)
                            {
                                item.FullName = trans.FullName;
                            }
                        }
                    }

                    return output.Select(x => new ContactViewModel()
                    {
                        Id = x.Id,
                        FullName = x.FullName

                    }).ToList();
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Contacts Lookup View Model");
                    return new List<ContactViewModel>();
                }
            }
        }

        public static BranchViewModel GetBrancheById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var branch = db.Branches.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Id == id);
                    var output = branch.FirstOrDefault();
                    if (output != null)
                    {
                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var trans = db.BranchTranslations.FirstOrDefault(r => r.BranchId == output.Id && r.LanguageId == languageId);
                            if (trans != null)
                            {
                                output.Name = trans.Name;
                            }
                        }
                        return new BranchViewModel()
                        {
                            Id = output.Id,
                            Name = output.Name,
                            Code = output.Code
                        };
                    }
                    else
                        return new BranchViewModel();


                }
                catch (Exception ex)
                {
                    LogHelper.LogException("", ex, $"Error While Getting Branches");
                    return new BranchViewModel();
                }
            }

        }

        public static int GetQuarter(string date)
        {
            int month = DateTime.Now.Month;
            int month1 = DateTime.ParseExact(date, "MMMM", CultureInfo.CurrentCulture).Month;
            if (month >= CheckIfNum(month1) && month <= CheckIfNum(month1 + 2))
                return 1;
            else if (month >= CheckIfNum(month1 + 3) && month <= CheckIfNum(month1 + 5))
                return 2;
            else if (month >= CheckIfNum(month1 + 6) && month <= CheckIfNum(month1 + 8))
                return 3;
            else
                return 4;
        }

        public static int CheckIfNum(int num)
        {
            if (num > 12)
                return num - 12;
            return num;
        }

        public static string GetHalf(DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 6)
                return "First Half ";
            else
                return "Second Half ";
        }

        public static GeneralEnums.TargetTypesEnum GetTargetTypesEnum(int val)
        {
            return (GeneralEnums.TargetTypesEnum)val;
        }

        public static string GetFollowUpTypeEnum(int val)
        {
            var s = (GeneralEnums.FollowUpType)val;

            return s.ToString().Replace("_", " ");
        }

        public static string GetWeekDaysEnum(int val)
        {
            var s = (GeneralEnums.WeekDaysEnum)val;

            return s.ToString().Replace("_", " ");
        }

        public static Contact GetContactByUserName(string name)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var contact = db.Contacts.FirstOrDefault(r => r.Email == name);
                return contact;
            }
        }



        public static List<SelectListItem> GetProjectCategoryList(int languageId, int? id = null)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var result = db.CmsProjectCategories.Include(r => r.CmsProjectCategoryTranslations).Where(r =>
                       r.Status == (int)GeneralEnums.StatusEnum.Active);

                if (id != null)
                    result = result.Where(r => r.Id == id);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in result)
                    {
                        var trans = item.CmsProjectCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                        }
                    }
                }


                return result.OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            }
        }


        public static List<StatusViewModel> GetQuestionType(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.QuestionEnum.Text,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Text" : "نص ادخال"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.QuestionEnum.CheckBox,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "CheckBox" : "اختيار متعدد"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.QuestionEnum.RadioButton,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Radio Button" : "اختيار واحد"
                }
            };
        }

        public static List<StatusViewModel> GetPaymentType(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.PaymentType.SenangPay,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Senang Pay" : "Senang Pay"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.PaymentType.Invoice,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Invoice" : "فاتورة"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.PaymentType.Balance,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Balance" : "رصيد"
                }
            };
        }

        public static StatusViewModel GetPaymentTypeById(int id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            return GetPaymentType(languageId).FirstOrDefault(r => r.Id == id);
        }

        public static List<StatusViewModel> GetExamsAndAssignmentsType(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                  new StatusViewModel()
                {
                    Id = 0,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Show All" : "أظهار الجميع"
                },
                 new StatusViewModel()
                {
                    Id = (int) GeneralEnums.ExamsAndAssignmentsEnums.Exam,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Exam" : "الامتحانات"
                },
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.ExamsAndAssignmentsEnums.PracticalExams,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Practical Exams" : "الامتحانات العملية"
                },
                   new StatusViewModel()
                {
                    Id = (int) GeneralEnums.ExamsAndAssignmentsEnums.Assignments,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Assignments" : "الواجبات"
                }
            };
        }

        public static StatusViewModel GetExamsAndAssignmentsTypeById(int id, int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            return GetExamsAndAssignmentsType(languageId).FirstOrDefault(r => r.Id == id);
        }

        public static List<StatusViewModel> GetinvoicPayStatus(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {

            return new List<StatusViewModel>()
            {
                new StatusViewModel()
                {
                    Id = (int) GeneralEnums.InvoicesPayStatusEnum.Accepted,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Accepted" : "مقبول"
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.InvoicesPayStatusEnum.Pending,
                    Name=languageId == CultureHelper.GetDefaultLanguageId() ? "Pending" : "قيد الانتظار"
                },
                  new StatusViewModel()
                {
                    Id = (int) GeneralEnums.InvoicesPayStatusEnum.Rejected,
                    Name =languageId == CultureHelper.GetDefaultLanguageId() ? "Rejected" : "مرفوض"
                },
            };
        }
        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }

        public static int GetFileType(string fileExtension)
        {
            string[] IMAGE_TYPES = { ".bmp", ".jpg", ".jpeg", ".png", ".gif" };
            IMAGE_TYPES = IMAGE_TYPES.Select(c => c.ToLower()).ToArray();

            string[] DOC_TYPES = { ".doc", ".docx", ".xls", ".xlsx", ".pptx" };
            DOC_TYPES = DOC_TYPES.Select(c => c.ToLower()).ToArray();

            string[] PDF_TYPES = { ".pdf" };
            PDF_TYPES = PDF_TYPES.Select(c => c.ToLower()).ToArray();

            string[] video_TYPES = { ".MP4", ".MOV", ".WMV", ".AVI", ".AVCHD", ".FLV", ".F4V", ".SWF", ".MKV", ".WEBM", ".HTML5", ".MPEG-2" };
            video_TYPES = video_TYPES.Select(c => c.ToLower()).ToArray();

            if (IMAGE_TYPES.Contains(fileExtension.ToLower()))
                return (int)GeneralEnums.ResourceTypeEnum.Image;

            if (DOC_TYPES.Contains(fileExtension.ToLower()))
                return (int)GeneralEnums.ResourceTypeEnum.Doc;

            if (PDF_TYPES.Contains(fileExtension.ToLower()))
                return (int)GeneralEnums.ResourceTypeEnum.Pdf;

            if (video_TYPES.Contains(fileExtension.ToLower()))
                return (int)GeneralEnums.ResourceTypeEnum.Video;

            return (int)GeneralEnums.ResourceTypeEnum.Other;

        }
        public static int GetAge(DateTime? birthDate)
        {
            try
            {
                var today = DateTime.Today;
                var age = today.Year - birthDate?.Year;
                var monthDiff = today.Month - birthDate?.Month;
                var dayDiff = today.Day - birthDate?.Day;

                if (dayDiff < 0)
                {
                    monthDiff--;
                }
                if (monthDiff < 0)
                {
                    age--;
                }
                return age ?? 0;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"Error While getAge");
                return 0;
            }

        }

        public static bool IsBewteenTwoDates(DateTime dt, DateTime start, DateTime end)
        {
            try
            {
                return dt >= start && dt <= end;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("", ex, $"Error While IsBewteenTwoDates");
                return false;
            }

        }


    }
}
