using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using X.PagedList;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore.Storage;
using LearningManagementSystem.Core;
using static iTextSharp.text.pdf.AcroFields;
using System.Web.Mvc;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollStudentCourseService : IEnrollStudentCourseService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;

        public EnrollStudentCourseService(ISettingService settingService, LearningManagementSystemContext context)
        {
            _settingService = settingService;
            _context = context;
        }

        public IPagedList<EnrollStudentCourse> GetEnrollStudentCourses(int? page, int? CourseId = null, int? studentId = null, int? TeacherId = null, int? EnrollTeacherCourseID = null, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, string searchText = "")
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCoursees = db.EnrollStudentCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    .Include(s => s.Course.EnrollCourseTimes).Include(s => s.Course.Course.CourseTranslations).Where(r => r.Course.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(s => s.Student.Contact.ContactTranslations)
                    .Include(s => s.CourseAttendances).AsQueryable();

                if (EnrollTeacherCourseID != null && EnrollTeacherCourseID > 0)
                    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Course.Id == EnrollTeacherCourseID);
                if (CourseId != null && CourseId > 0)
                    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Course.CourseId == CourseId);
                if (studentId != null && studentId > 0)
                    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.StudentId == studentId);
                if (TeacherId != null && TeacherId > 0)
                    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Course.TeacherId == TeacherId);

                if (!string.IsNullOrWhiteSpace(searchText))
                    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Student.Contact.FullName.Replace(" ", "").Contains(searchText.Replace(" ", "")) || r.Student.Contact.ContactTranslations.Any(t => t.FullName.Replace(" ", "").Contains(searchText.Replace(" ", "")) && t.LanguageId == languageId));

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = EnrollStudentCoursees;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in output)
                    {
                        var trans = item.Course.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.Course.Course.CourseName = trans.CourseName;
                        var transContact = item.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transContact != null)
                            item.Student.Contact.FullName = transContact.FullName;
                    }

                return output;
            }
        }

        public IPagedList<EnrollStudentCourse> GetEnrollStudentCoursesForQuiz(int? page, int? EnrollTeacherCourseID, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, string searchText = "")
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCoursees = db.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.Course.Id == EnrollTeacherCourseID)
                    .Include(r => r.EnrollCourseQuizPointes).Include(r => r.PracticalEnrollmentExamStudents).Include(s => s.Student.Contact.ContactTranslations).AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchText))
                {

                    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Student.Contact.FullName.Contains(searchText) || r.Student.Contact.ContactTranslations.Any(t => t.FullName.Contains(searchText) && t.LanguageId == languageId));

                    //if (languageId == CultureHelper.GetDefaultLanguageId())
                    //    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Student.Contact.FullName.Contains(searchText));
                    //else
                    //    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Student.Contact.FullName.Contains(searchText) || r.Student.Contact.ContactTranslations.Any(t => t.FullName.Contains(searchText) && t.LanguageId == languageId));
                }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = EnrollStudentCoursees;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);


                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in output)
                    {
                        var transContact = item.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transContact != null)
                            item.Student.Contact.FullName = transContact.FullName;
                    }

                return output;
            }
        }

        public int GetCountEnrollStudentCourses(int EnrollTeacherCourseId, int studentId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCoursees = db.EnrollStudentCourses.Include(s => s.Course).Include(s => s.Student).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Course.Id == EnrollTeacherCourseId);
                if (studentId > 0)
                {
                    EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.StudentId == studentId);
                }
                return EnrollStudentCoursees.Count();
            }
        }

        public int GetAttendanceDays(int EnrollTeacherCourseId)
        {
            var calculate = bool.Parse(_settingService.GetOrCreate("Calculate_Attendance_Days", "False").Value);
            var course = _context.EnrollTeacherCourses.Include(r => r.EnrollCourseTimes).Include(r => r.Course).Include(r => r.EnrollSectionOfCourses).ThenInclude(r => r.EnrollLectures).FirstOrDefault(r => r.Id == EnrollTeacherCourseId);
            if (calculate)
            {
                var houers = course.EnrollCourseTimes.Select(r => (r.ToTime - r.FromTime).GetValueOrDefault().TotalMinutes);
                var weeks = Math.Ceiling((course.WorkEndDate - course.WorkStartDate).GetValueOrDefault().TotalDays / 7);
                decimal totel = (decimal)(course.Course?.CourseDuration ?? 0) * 60;
                var num = 0;
                for (int i = 0; i < weeks; i++)
                    foreach (var item1 in houers)
                        if (totel - (decimal)item1 > 0)
                        {
                            totel = totel - (decimal)item1;
                            num++;
                        }

                return num;
            }
            else
            {
                var count = course.EnrollSectionOfCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).SelectMany(r => r.EnrollLectures).Count();
                return count;
            }
        }


        public EnrollStudentCourse GetEnrollStudentCourseById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCourse = db.EnrollStudentCourses.Include(r => r.Course.Course).Include(r => r.Course.Teacher.Contact.ContactTranslations)
                    .Include(r => r.Student).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return EnrollStudentCourse;
            }
        }

        public string GetStudentNameFromEnrollStudentCourseById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCourse = db.EnrollStudentCourses.Include(r => r.Student.Contact.ContactTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var tran = EnrollStudentCourse.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (tran != null)
                        EnrollStudentCourse.Student.Contact.FullName = tran.FullName;
                }
                return EnrollStudentCourse.Student.Contact.FullName;
            }
        }
        public EnrollStudentCourseViewModel GetEnrollStudentCourseByStudentIdAndEnrollTeacherCourseId(int studentId, int EnrollTeacherCourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentCourse = db.EnrollStudentCourses.FirstOrDefault(s => s.StudentId == studentId && s.CourseId == EnrollTeacherCourseId && s.Status == (int)GeneralEnums.StatusEnum.Active);

                if (EnrollStudentCourse == null)
                {
                    return new EnrollStudentCourseViewModel();
                }

                return new EnrollStudentCourseViewModel(EnrollStudentCourse);
            }
        }

        public EnrollStudentCourseViewModel GetEnrollStudentCourseByStudentIdAndCourseId(int studentId, int courseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourses = db.EnrollTeacherCourses.Where(r => r.CourseId == courseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(s => s.Id);

                var EnrollStudentCourse = db.EnrollStudentCourses.Where(s => s.StudentId == studentId && enrollTeacherCourses.Contains(s.CourseId) && s.Status == (int)GeneralEnums.StatusEnum.Active).Include(r => r.Course).OrderBy(r => r.Course.WorkEndDate).LastOrDefault();

                if (EnrollStudentCourse == null)
                {
                    return new EnrollStudentCourseViewModel();
                }

                return new EnrollStudentCourseViewModel(EnrollStudentCourse);
            }
        }


        public EnrollStudentCourseViewModel GetEnrollStudentCourseById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentCourse = db.EnrollStudentCourses.Find(id);
                return new EnrollStudentCourseViewModel(EnrollStudentCourse);
            }
        }

        public List<EnrollStudentCourse> GetEnrollStudentCourseByStudentId(int StudentId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentCourse = db.EnrollStudentCourses.Include(r => r.Course).Where(r => r.StudentId == StudentId && r.Status == (int)GeneralEnums.StatusEnum.Active).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in EnrollStudentCourse)
                    {
                        var trans = db.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.EnrollCourseId == item.Course.Id && r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Course.CourseName = trans.CourseName;
                        }
                    }
                }
                return EnrollStudentCourse;
            }
        }

        public EnrollStudentCourse GetEnrollStudentCourse(int StudentId, int enrollTeacherCourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCourse = db.EnrollStudentCourses.FirstOrDefault(r => r.StudentId == StudentId && r.CourseId == enrollTeacherCourseId && r.Status == (int)GeneralEnums.StatusEnum.Active);
                return EnrollStudentCourse;
            }
        }

        public int AddEnrollStudentCourse(EnrollStudentCourseViewModel EnrollStudentCourseViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var IsExistsStudent = db.EnrollStudentCourses.Where(r => r.StudentId == EnrollStudentCourseViewModel.StudentId && r.CourseId == EnrollStudentCourseViewModel.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Count();
                if (IsExistsStudent == 0)
                {
                    var EnrollStudentCourse = new EnrollStudentCourse()
                    {
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        CourseId = EnrollStudentCourseViewModel.CourseId,
                        IsPass = EnrollStudentCourseViewModel.IsPass,
                        Mark = EnrollStudentCourseViewModel.Mark,
                        NeedApproval = EnrollStudentCourseViewModel.NeedApproval,
                        Price = EnrollStudentCourseViewModel.Price,
                        StudentId = EnrollStudentCourseViewModel.StudentId,
                        CurrencyRate = EnrollStudentCourseViewModel.CurrencyRate,
                        CustomerCurrencyCode = EnrollStudentCourseViewModel.CustomerCurrencyCode,
                    };

                    db.EnrollStudentCourses.Add(EnrollStudentCourse);
                    db.SaveChanges();
                    return EnrollStudentCourse.Id;
                }
                return 0;
            }
        }
        public string CheckAbilityToEnrollStudentInCourse(int EnrollTeacherCourseId, int StudentId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {

                    //HasPreRequestsCourse
                    var enrollTeacherCourse = db.EnrollTeacherCourses.FirstOrDefault(r => r.Id == EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var coursesPrerequisite = db.CoursePrerequisites.Include(d => d.Course).Include(d => d.PrerequisiteCourse).Where(r => r.CourseId == enrollTeacherCourse.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                    foreach (var _coursePrerequistis in coursesPrerequisite)
                    {
                        var enrollTeacherCourses_Prerequistis = db.EnrollTeacherCourses.Where(r => r.CourseId == _coursePrerequistis.PrerequisiteCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.Id).ToList();
                        var EnrollStudentCourses_Prerequistis = db.EnrollStudentCourses.Where(r => enrollTeacherCourses_Prerequistis.Contains(r.CourseId) && r.StudentId == StudentId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                        if (EnrollStudentCourses_Prerequistis.Count() > 0)
                        {
                            foreach (var _enrollStudentCourse in EnrollStudentCourses_Prerequistis)
                            {
                                if (!(_enrollStudentCourse.IsPass.HasValue && _enrollStudentCourse.IsPass.Value))
                                {
                                    return "HasPreRequestsCourse";
                                }

                            }
                        }
                        else
                        {
                            return "HasPreRequestsCourse";
                        }

                    }

                    var CoursDetails = db.EnrollTeacherCourses.Include(d => d.Course).FirstOrDefault(r => r.Id == EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var StudentDetails = db.Students.Include(c => c.Contact).First(c => c.Id == StudentId);
                    var CountEnrollStudent = GetCountEnrollStudentCourses(EnrollTeacherCourseId);
                    if (CoursDetails.CountOfStudent == null)
                        CoursDetails.CountOfStudent = 0;

                    //FailMaxEnrollStudent
                    if ((CountEnrollStudent + 1) > CoursDetails.CountOfStudent && CoursDetails.CountOfStudent != 0)
                    {
                        return "FailMaxEnrollStudent";
                    }
                    //FailExistsStudent
                    var CheckExistsStudent = GetCountEnrollStudentCourses(EnrollTeacherCourseId, StudentId);
                    if (CheckExistsStudent > 0)
                    {
                        return "FailExistsStudent";
                    }

                    string Check_student_AgeAndGender_WhenAddedTo_Course = _settingService.GetOrCreate("Check_student_AgeAndGender_WhenAddedTo_Course", "on").Value;
                    if (Check_student_AgeAndGender_WhenAddedTo_Course.ToLower() == "on")
                    {
                        //FailAgeAllowedForRegistration
                        var age = LookupHelper.GetAge(StudentDetails.BirthDate.Value);
                        if (StudentDetails.BirthDate != null && (age < CoursDetails.AgeAllowedForRegistration || age > CoursDetails.AgeGroupTo))
                        {
                            return "FailAgeAllowedForRegistration";
                        }

                        //FailAgeGroup
                        var lookupsCourseAgeGroup = LookupHelper.GetLookupDetailsByMasterCode(GeneralEnums.MasterLookupCodeEnums.CourseAgeGroup.ToString(), 8);
                        var lookupsGender = LookupHelper.GetLookupDetailsByMasterCode(GeneralEnums.MasterLookupCodeEnums.Gender.ToString(), 8);
                        switch (lookupsCourseAgeGroup.Where(g => g.Id == CoursDetails.AgeGroup).Select(e => e.Code).FirstOrDefault())
                        {
                            case "Males":
                                if (StudentDetails.Contact.GenderId != lookupsGender.Where(g => g.Code == "M").Select(e => e.Id).FirstOrDefault())
                                {
                                    return "FailAgeGroup";
                                }
                                break;

                            case "Females":
                                if (StudentDetails.Contact.GenderId != lookupsGender.Where(g => g.Code == "F").Select(e => e.Id).FirstOrDefault())
                                {
                                    return "FailAgeGroup";
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "fail";
                }

                return "done";
            }
        }

        public DateTime? CheckIfHasBeenExpilled(int id)
        {
            var course = _context.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Expelled && r.StudentId == id);
            List<Expulsion> expulsion = new List<Expulsion>();
            foreach (var item in course)
                expulsion.AddRange(_context.Expulsions.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.ExpelledFrom <= item.ExpelledDate && r.ExpelledTo >= item.ExpelledDate).ToList());

            if (!expulsion.Any())
                return null;
            return expulsion.OrderByDescending(r => r.ExpulsionEnd).Select(e => e.ExpulsionEnd).FirstOrDefault();
        }

        public void AddEnrollStudentCourse_WthoutUsing(EnrollStudentCourseViewModel EnrollStudentCourseViewModel, LearningManagementSystemContext db)
        {
            var IsExistsStudent = db.EnrollStudentCourses.Where(r => r.StudentId == EnrollStudentCourseViewModel.StudentId && r.CourseId == EnrollStudentCourseViewModel.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Count();
            if (IsExistsStudent == 0)
            {
                var EnrollStudentCourse = new EnrollStudentCourse()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CourseId = EnrollStudentCourseViewModel.CourseId,
                    IsPass = EnrollStudentCourseViewModel.IsPass,
                    Mark = EnrollStudentCourseViewModel.Mark,
                    NeedApproval = EnrollStudentCourseViewModel.NeedApproval,
                    Price = EnrollStudentCourseViewModel.Price,
                    StudentId = EnrollStudentCourseViewModel.StudentId,
                    CustomerCurrencyCode = EnrollStudentCourseViewModel.CustomerCurrencyCode,
                    CurrencyRate = EnrollStudentCourseViewModel.CurrencyRate,
                };

                db.EnrollStudentCourses.Add(EnrollStudentCourse);
                db.SaveChanges();
            }

        }
        public void EditEnrollStudentCourse(EnrollStudentCourseViewModel enrollStudentCourseViewModel, EnrollStudentCourse enrollStudentCourse)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollStudentCourse.CourseId = enrollStudentCourseViewModel.CourseId;
                enrollStudentCourse.IsPass = enrollStudentCourseViewModel.IsPass;
                enrollStudentCourse.Mark = enrollStudentCourseViewModel.Mark;
                enrollStudentCourse.NeedApproval = enrollStudentCourseViewModel.NeedApproval;
                enrollStudentCourse.Price = enrollStudentCourseViewModel.Price;
                enrollStudentCourse.StudentId = enrollStudentCourseViewModel.StudentId;

                db.Entry(enrollStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteEnrollStudentCourse(EnrollStudentCourse EnrollStudentCourse)
        {
            using (var db = new LearningManagementSystemContext())
            {
                EnrollStudentCourse.Status = (int)GeneralEnums.StatusEnum.Deleted;
                EnrollStudentCourse.DeletedOn = DateTime.Now;
                db.Entry(EnrollStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void ExpelEnrollStudentCourse(EnrollStudentCourse EnrollStudentCourse, string createdBy)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var historys = db.StudentExpulsionHistories.Where(r => r.EnrollStudentCourseId == EnrollStudentCourse.Id && r.StudentId == EnrollStudentCourse.StudentId && r.Status == (int)GeneralEnums.StatusEnum.Active).ToList();
                foreach (var item in historys)
                {
                    item.Status = (int)GeneralEnums.StatusEnum.Deactive;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();

                var history = new StudentExpulsionHistory()
                {
                    CreatedBy = createdBy,
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    StudentId = EnrollStudentCourse.StudentId,
                    EnrollStudentCourseId = EnrollStudentCourse.Id,
                    TypeId = (int)GeneralEnums.ExpulsionStatus.Expel,
                };
                db.StudentExpulsionHistories.Add(history);
                db.SaveChanges();

                EnrollStudentCourse.Status = (int)GeneralEnums.StatusEnum.Expelled;
                EnrollStudentCourse.ExpelledDate = DateTime.Now;
                db.Entry(EnrollStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void CancelExpulsionEnrollStudentCourse(EnrollStudentCourse EnrollStudentCourse, string createdBy)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var historys = db.StudentExpulsionHistories.Where(r => r.EnrollStudentCourseId == EnrollStudentCourse.Id && r.StudentId == EnrollStudentCourse.StudentId && r.Status == (int)GeneralEnums.StatusEnum.Active).ToList();
                foreach (var item in historys)
                {
                    item.Status = (int)GeneralEnums.StatusEnum.Deactive;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();

                var history = new StudentExpulsionHistory()
                {
                    CreatedBy = createdBy,
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    StudentId = EnrollStudentCourse.StudentId,
                    EnrollStudentCourseId = EnrollStudentCourse.Id,
                    TypeId = (int)GeneralEnums.ExpulsionStatus.Readmit,
                };
                db.StudentExpulsionHistories.Add(history);
                db.SaveChanges();

                EnrollStudentCourse.Status = (int)GeneralEnums.StatusEnum.Active;
                EnrollStudentCourse.ExpelledDate = null;
                db.Entry(EnrollStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //public IPagedList<EnrollStudentCourse> GetEnrollStudentCoursesForTrainer(int? page, int TeacherId , int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? CourseId=0)
        //{
        //    using (var db = new LearningManagementSystemContext())
        //    {
        //        var EnrollStudentCoursees = db.EnrollStudentCourses.Include(s => s.Student).Include(s => s.Course).Include(s => s.Student.Contact).Where(r =>
        //              r.Status != (int)GeneralEnums.StatusEnum.Deleted);

        //        if (CourseId > 0)
        //        {
        //            EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Course.CourseId == CourseId);
        //        }

        //        if (TeacherId > 0)
        //        {
        //            EnrollStudentCoursees = EnrollStudentCoursees.Where(r => r.Course.TeacherId == TeacherId);
        //        }

        //        var pageSize = pagination;
        //        var pageNumber = (page ?? 1);
        //        var result = EnrollStudentCoursees;
        //        var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
        //        return output;
        //    }
        //}

        public bool IsPassed(int CourseId, int StudentId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var c = db.EnrollStudentCourses.FirstOrDefault(r => r.CourseId == CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.StudentId == StudentId);
                if (c != null)
                    if (c.IsPass != null)
                        if (c.IsPass.Value == true)
                            return true;
                return false;
            }
        }

        public Byte[] GetCertificate(EnrollStudentCourse course, int templateId, int languageId, string baseUri)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var template = db.TemplateHtmls.Find(templateId).RanderHtml.Replace("\r\n", string.Empty).Replace("\"", "\'");
                var name = db.Students.Include(r => r.Contact.ContactTranslations).FirstOrDefault(r => r.Id == course.StudentId);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var tran = name.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (tran != null)
                        name.Contact.FullName = tran.FullName;
                    var tran1 = course.Course.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (tran1 != null)
                        course.Course.Teacher.Contact.FullName = tran1.FullName;
                    var tran2 = course.Course.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (tran2 != null)
                        course.Course.CourseName = tran2.CourseName;

                }

                template = template.Replace("[Name]", name.Contact.FullName);
                template = template.Replace("[Teacher]", course.Course.Teacher.Contact.FullName);
                template = template.Replace("[CourseDuration]", course.Course?.Course?.CourseDuration.ToString());
                template = template.Replace("[Score]", course?.Mark.ToString());
                template = template.Replace("[ScoreText]", GetEvaluation(course.Id, languageId));
                template = template.Replace("[WorkStartDate]", GetMonthName(course.Course.WorkStartDate.Month));
                template = template.Replace("[WorkEndDate]", GetMonthName(course.Course.WorkEndDate?.Month ?? 0));
                template = template.Replace("[Year]", course.Course.WorkStartDate.Year.ToString());
                template = template.Replace("[Img]", course.Course.Teacher.Signature);
                template = template.Replace("[CourseName]", course.Course.CourseName);
                template = template.Replace("[BaseUrl]", baseUri);



                //StringReader sr = new StringReader(html);
                Document pdfDoc = new Document(new Rectangle(1160f, 840f), 0f, 0f, 0f, 0f);



                byte[] bytes;
                using (MemoryStream memoryStream = new MemoryStream())
                {

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    var xmlWorker = XMLWorkerHelper.GetInstance();
                    MemoryStream htmlContent = new MemoryStream(Encoding.UTF8.GetBytes("<!DOCTYPE html><html><head>" + "<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'/> <meta charset='utf-8'/><style>body {font-family:Tahoma;}</style>" + "</head><body>" + template + "</body></html>"));
                    xmlWorker.ParseXHtml(writer, pdfDoc, htmlContent, null, Encoding.UTF8, new UnicodeFontFactory());

                    pdfDoc.Close();
                    memoryStream.Close();

                    return memoryStream.ToArray();
                }


            }
        }
        public class UnicodeFontFactory : FontFactoryImp
        {

            private static readonly string FontPath = Path.Combine($"{Directory.GetCurrentDirectory()}\\wwwroot\\fonts", "TAHOMA.TTF");

            private readonly BaseFont _baseFont;

            public UnicodeFontFactory()
            {
                _baseFont = BaseFont.CreateFont(FontPath,
                 BaseFont.IDENTITY_H,
                 BaseFont.EMBEDDED);

            }

            public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
          bool cached)
            {

                return new Font(_baseFont, size, style, color);
            }
        }
        static string GetMonthName(int number)
        {
            switch (number)
            {
                case 1:
                    return "يناير";
                case 2:
                    return "فبراير";
                case 3:
                    return "مارس";
                case 4:
                    return "أبريل";
                case 5:
                    return "مايو";
                case 6:
                    return "يونيو";
                case 7:
                    return "يوليو";
                case 8:
                    return "أغسطس";
                case 9:
                    return "سبتمبر";
                case 10:
                    return "أكتوبر";
                case 11:
                    return "نوفمبر";
                case 12:
                    return "ديسمبر";
                default:
                    return "()";
            }
        }

        public int GetStudentCount(int enrollTeacherCourseId)
        {
            var count = _context.EnrollStudentCourses.Where(r => r.CourseId == enrollTeacherCourseId && r.Status == (int)GeneralEnums.StatusEnum.Active).Count();
            return count;
        }

        public string GetEvaluation(int id, int languageId)
        {
            var studentCourse = GetEnrollStudentCourseById(id);
            var evaluation = _context.CourseMarks.Include(r => r.CourseMarkTranslations).FirstOrDefault(r => r.CourseId == studentCourse.Course.CourseId && r.Value <= (decimal)(studentCourse.Mark ?? 0) && r.ValueTo >= (decimal)(studentCourse.Mark ?? 0));
            if (languageId != CultureHelper.GetDefaultLanguageId())
                if (evaluation?.CourseMarkTranslations.FirstOrDefault(r => r.LanguageId == languageId) != null)
                    evaluation.Title = evaluation.CourseMarkTranslations.FirstOrDefault(r => r.LanguageId == languageId)?.Title;

            if (evaluation != null)
                return evaluation.Title;
            return "";
        }

        public void CalculateMarks(EnrollTeacherCourse enrollTeacherCourse)
        {
            var sussessMark = decimal.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Success_Mark, "100")?.Value);

            var students = _context.EnrollStudentCourses.Where(r => r.CourseId == enrollTeacherCourse.Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                .Include(r => r.EnrollStudentAssigments.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active)).ThenInclude(r=>r.EnrollStudentAssigmentAnswers)
                .Include(r => r.EnrollStudentExams).Include(r => r.PracticalEnrollmentExamStudents).Include(r => r.EnrollCourseQuizPointes);
            foreach (var student in students.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active))
            {
                double total = 0;

                foreach (var item in student.PracticalEnrollmentExamStudents)
                    total += (double)(item.MarkAfterConversion ?? 0);
                foreach (var item in student.EnrollStudentExams)
                    total += item.MarkAfterConversion ?? 0;

                var averageOne = student.EnrollCourseQuizPointes.Where(r => r.QuestionOne >= 0).Average(r => r.QuestionOne);
                var averageTwo = student.EnrollCourseQuizPointes.Where(r => r.QuestionTwo >= 0).Average(r => r.QuestionTwo);
                var averageThree = student.EnrollCourseQuizPointes.Where(r => r.QuestionThree >= 0).Average(r => r.QuestionThree);
                if (averageThree > 0)
                    total += (double)((averageOne ?? 0) * 11 / 10) + (double)((averageTwo ?? 0) * 11 / 10) + (double)((averageThree ?? 0) * 9 / 10);
                else
                    total += (double)((averageOne ?? 0) * 16 / 10) + (double)((averageTwo ?? 0) * 14 / 10);

                if (student?.EnrollStudentAssigments.Count() > 0 && enrollTeacherCourse?.Course?.AssignmentMark > 0 && enrollTeacherCourse?.EnrollCourseAssigments.Count() > 0)
                {
                    var s = (double)student.EnrollStudentAssigments.Where(r=>r.EnrollStudentAssigmentAnswers.Any()).Count() / (double)(enrollTeacherCourse?.EnrollCourseAssigments.Count() ?? 1);
                    total += (double)(s * (double)enrollTeacherCourse?.Course?.AssignmentMark);
                }
                student.Mark = Math.Round(total, 2); ;
                student.IsPass = (double)(enrollTeacherCourse?.Course?.SuccessMark ?? sussessMark) <= total;
                _context.Entry(student).State = EntityState.Modified;
            }

            foreach (var student in students.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Active))
            {
                student.Mark = null;
                student.IsPass = null;
                _context.Entry(student).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }
    }
}
