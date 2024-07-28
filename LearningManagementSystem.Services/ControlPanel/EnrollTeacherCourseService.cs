using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static LearningManagementSystem.Core.Constants;
using System.Threading.Tasks;
using EllipticCurve;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollTeacherCourseService : IEnrollTeacherCourseService
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ISettingService _settingService;
        private readonly ISemesterService _semesterService;
        private readonly IEnrollSectionOfCourseService _enrollSectionOfCourseService;
        private readonly ICourseService _courseService;
        private readonly IContactsService _contactsService;
        public EnrollTeacherCourseService(LearningManagementSystemContext context, ISettingService settingService, ISemesterService semesterService, IEnrollSectionOfCourseService enrollSectionOfCourseService, ICourseService courseService, IContactsService contactsService)
        {
            _context = context;
            _settingService = settingService;
            _semesterService = semesterService;
            _enrollSectionOfCourseService = enrollSectionOfCourseService;
            _courseService = courseService;
            _contactsService = contactsService;
        }
        public IPagedList<EnrollTeacherCourse> GetEnrollTeacherCourses(int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? CourseId = 0, int? TeacherId = 0, int? LearningMethodId = 0, int? SemesterId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourses = db.EnrollTeacherCourses.Include(r => r.EnrollTeacherCourseTranlations).Include(r => r.Semester).Include(r => r.Teacher).ThenInclude(r => r.Contact).Where(r =>
                          r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (TeacherId > 0)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.TeacherId == TeacherId);
                if (CourseId > 0)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.CourseId == CourseId);
                if (LearningMethodId > 0)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.LearningMethodId == LearningMethodId);
                if (SemesterId > 0 && SemesterId != null)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.SemesterId == SemesterId);

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = enrollTeacherCourses;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            if (!string.IsNullOrEmpty(trans.SectionName))
                                item.SectionName = trans.SectionName;
                            if (!string.IsNullOrEmpty(trans.CourseName))
                                item.CourseName = trans.CourseName;
                            if (!string.IsNullOrEmpty(trans.NotesForEnrolled))
                                item.NotesForEnrolled = trans.NotesForEnrolled;
                        }
                    }
                }

                foreach (var item in output)
                {

                    if (item.SemesterId != null)
                    {
                        item.Semester.Name = "--";
                        var trans = _semesterService.GetSemesterById(item.SemesterId.Value, languageId);
                        if (trans != null && trans.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            item.Semester.Name = trans.Name;

                        }
                    }

                    var transCourse = _courseService.GetCourseById(item.CourseId, languageId);
                    item.Course = new Course();
                    if (transCourse != null && transCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        item.CourseName = transCourse.CourseName;

                    }


                    else
                    {
                        item.CourseName = "--";
                    }
                    var transContact = _contactsService.GetContact(item.Teacher.ContactId, languageId);

                    if (transContact != null && transContact.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        item.Teacher.Contact.FullName = transContact.FullName;

                    }
                    else
                    {
                        item.Teacher.Contact.FullName = "--";
                    }
                }



                return output;
            }
        } 
        
        public IPagedList<EnrollTeacherCourse> GetEnrollTeacherSupportCourses(int? page, int languageId, int pagination, int TeacherId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourses = db.PracticalEnrollmentExamTrainers.Where(r =>r.TrainerId == TeacherId).Include(r=>r.PracticalEnrollmentExam.EnrollTeacherCourse.Semester)
                    .Include(r=> r.PracticalEnrollmentExam.EnrollTeacherCourse.Teacher.Contact);

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = enrollTeacherCourses.Select(r=>r.PracticalEnrollmentExam.EnrollTeacherCourse).Distinct().ToList();
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in output)
                    {
                        var trans = db.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId && r.EnrollCourseId == item.Id);
                        if (trans != null)
                        {
                            if (!string.IsNullOrEmpty(trans.SectionName))
                                item.SectionName = trans.SectionName;
                            if (!string.IsNullOrEmpty(trans.CourseName))
                                item.CourseName = trans.CourseName;
                            if (!string.IsNullOrEmpty(trans.NotesForEnrolled))
                                item.NotesForEnrolled = trans.NotesForEnrolled;
                        }
                    }

                foreach (var item in output)
                {

                    if (item.SemesterId != null)
                    {
                        item.Semester.Name = "--";
                        var trans = _semesterService.GetSemesterById(item.SemesterId.Value, languageId);
                        if (trans != null && trans.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            item.Semester.Name = trans.Name;

                        }
                    }

                    var transCourse = _courseService.GetCourseById(item.CourseId, languageId);
                    item.Course = new Course();
                    if (transCourse != null && transCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        item.CourseName = transCourse.CourseName;
                    else
                        item.CourseName = "--";

                    var transContact = _contactsService.GetContact(item.Teacher.ContactId, languageId);

                    if (transContact != null && transContact.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        item.Teacher.Contact.FullName = transContact.FullName;
                    else
                        item.Teacher.Contact.FullName = "--";
                }

                return output;
            }
        }

        public List<EnrollTeacherCourse> GetEnrollTeacherCoursesIds(int? CourseId = 0, int? TeacherId = 0, int? SemesterId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourses = db.EnrollTeacherCourses.Where(r =>r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (TeacherId > 0)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.TeacherId == TeacherId);
                if (CourseId > 0)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.CourseId == CourseId);
                if (SemesterId > 0 && SemesterId != null)
                    enrollTeacherCourses = enrollTeacherCourses.Where(r => r.SemesterId == SemesterId);

                return enrollTeacherCourses.ToList();
            }
        }

        public EnrollTeacherCourse GetEnrollTeacherCourseById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourse = db.EnrollTeacherCourses.Include(d => d.Course.Category).Include(r => r.EnrollTeacherCourseTranlations).Include(r => r.EnrollCourseAssigments)
                    .FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return enrollTeacherCourse;
            }
        }

        public EnrollTeacherCourse GetEnrollById(int id, int languageId)
        {
            if(id == 0) return null; //TODO
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourses = db.EnrollTeacherCourses.Include(d => d.Teacher.Contact.ContactTranslations).Include(d => d.Course.Category).Include(r => r.Course.CourseTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (enrollTeacherCourses == null)
                    return null;    

                if (languageId != CultureHelper.GetDefaultLanguageId() && enrollTeacherCourses != null)
                {
                    var transCourse = enrollTeacherCourses.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (transCourse != null)
                        enrollTeacherCourses.CourseName = transCourse.CourseName;
                    var transContact = enrollTeacherCourses.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (transContact != null)
                        enrollTeacherCourses.Teacher.Contact.FullName = transContact.FullName;
                }
                else
                    enrollTeacherCourses.CourseName = enrollTeacherCourses?.Course?.CourseName??"";

                return enrollTeacherCourses;
            }
        }

        public EnrollTeacherCourseViewModel GetEnrollTeacherCourseById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.EnrollTeacherCourseTranlations.Include(r => r.EnrollCourse).FirstOrDefault(r => r.LanguageId == languageId && r.EnrollCourse.Id == id);
                    if (aboutTran != null)
                    {
                        return new EnrollTeacherCourseViewModel(aboutTran);
                    }
                }
                var enrollTeacherCourses = db.EnrollTeacherCourses.FirstOrDefault(d => d.Id == id);
                return new EnrollTeacherCourseViewModel(enrollTeacherCourses);
            }
        }
        public EnrollTeacherCourse GetEnrollTeacherCourseByTeacherIdAndCourseId(int TeacherId, int CourseId, string SectionName = "")
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCoursesTeacher = db.EnrollTeacherCourses.FirstOrDefault(r => r.TeacherId == TeacherId && r.CourseId == CourseId && (!string.IsNullOrEmpty(SectionName) ? r.SectionName == SectionName : r.SectionName == null) && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return EnrollCoursesTeacher;
            }
        }
        public List<EnrollTeacherCourse> GetEnrollTeacherCourseByCourseId(int CourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCoursesTeacher = db.EnrollTeacherCourses.Where(r => r.CourseId == CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return EnrollCoursesTeacher.ToList();
            }
        }
        public List<Course> GetCourseByTeacherId(int TeacherId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCoursesTeacherCourseIds = db.EnrollTeacherCourses.Include(r => r.EnrollTeacherCourseTranlations).Where(r => r.TeacherId == TeacherId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.CourseId).ToList();
                var EnrollCoursesTeacher = db.Courses.Include(r => r.CourseTranslations).Where(r => EnrollCoursesTeacherCourseIds.Contains(r.Id) && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in EnrollCoursesTeacher)
                    {
                        var trans = item.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.CourseName = trans.CourseName;
                        }
                    }


                }
                return EnrollCoursesTeacher;
            }
        }
        public List<EnrollTeacherCourse> GetEnrollTeacherCourseByTeacherId(int TeacherId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCoursesTeacher = db.EnrollTeacherCourses.Where(r => r.TeacherId == TeacherId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.EnrollTeacherCourseTranlations).Include(r => r.Course.CourseTranslations).ToList();
                foreach (var EnrollTeacherCoursesData in EnrollCoursesTeacher)
                {
                    if (!string.IsNullOrEmpty(EnrollTeacherCoursesData.SectionName))
                        EnrollTeacherCoursesData.CourseName = EnrollTeacherCoursesData.Course.CourseName + " / " + EnrollTeacherCoursesData.SectionName;
                }
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in EnrollCoursesTeacher)
                    {
                        var trans = item.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        var trans1 = item.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null && trans1 != null)
                        {
                            if (!string.IsNullOrEmpty(trans1.SectionName))
                                item.CourseName = trans.CourseName + " / " + trans1.SectionName;
                            else if (!string.IsNullOrEmpty(item.SectionName))
                                item.CourseName = trans.CourseName + " / " + item.SectionName;
                            else
                                item.CourseName = trans.CourseName;

                            item.NotesForEnrolled = trans1.NotesForEnrolled;
                        }
                    }


                }
                return EnrollCoursesTeacher;
            }
        }

        public EnrollTeacherCourse AddEnrollTeacherCourse(EnrollTeacherCourseViewModel enrollTeacherCourseViewModel, Course course)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourse = new EnrollTeacherCourse()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CourseName = course.CourseName,
                    LearningMethodId = enrollTeacherCourseViewModel.LearningMethodId,
                    AgeAllowedForRegistration = enrollTeacherCourseViewModel.AgeAllowedForRegistration,
                    AgeGroup = enrollTeacherCourseViewModel.AgeGroup,
                    TeacherId = enrollTeacherCourseViewModel.TeacherId,
                    SemesterId = enrollTeacherCourseViewModel.SemesterId,
                    SectionName = enrollTeacherCourseViewModel.SectionName,
                    PublicationDate = enrollTeacherCourseViewModel.PublicationDate,
                    PublicationEndDate = enrollTeacherCourseViewModel.PublicationEndDate,
                    WorkStartDate = enrollTeacherCourseViewModel.WorkStartDate,
                    WorkEndDate = enrollTeacherCourseViewModel.WorkEndDate,
                    CourseId = enrollTeacherCourseViewModel.CourseId,
                    CountOfStudent = enrollTeacherCourseViewModel.CountOfStudent,
                    CreatedBy = enrollTeacherCourseViewModel.CreatedBy,
                    NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled,
                    CalculationTypeId = enrollTeacherCourseViewModel.CalculationTypeId,
                    AgeGroupTo = enrollTeacherCourseViewModel.AgeGroupTo,
                };
                db.EnrollTeacherCourses.Add(enrollTeacherCourse);
                db.SaveChanges();

                enrollTeacherCourse.Id = enrollTeacherCourse.Id;

                if (enrollTeacherCourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    if (course.CourseTranslations != null)
                    {
                        foreach (var CourseTranslations in course.CourseTranslations)
                        {
                            var enrollTeacherCourseTranlation = new EnrollTeacherCourseTranlation()
                            {
                                CourseName = CourseTranslations.CourseName,
                                LanguageId = CourseTranslations.LanguageId,
                                SectionName = enrollTeacherCourseViewModel.SectionName,
                                NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled,
                                EnrollCourseId = enrollTeacherCourse.Id
                            };
                            db.EnrollTeacherCourseTranlations.Add(enrollTeacherCourseTranlation);
                            db.SaveChanges();
                        }
                    }
                }
                return enrollTeacherCourse;
            }
        }

        public EnrollTeacherCourse AddEnrollTeacherCourse_WithoutUsing(EnrollTeacherCourseViewModel enrollTeacherCourseViewModel, Course course, LearningManagementSystemContext db)
        {

            var enrollTeacherCourse = new EnrollTeacherCourse()
            {
                CreatedOn = DateTime.Now,
                Status = enrollTeacherCourseViewModel.Status,
                CourseName = course.CourseName,
                LearningMethodId = enrollTeacherCourseViewModel.LearningMethodId,
                AgeAllowedForRegistration = enrollTeacherCourseViewModel.AgeAllowedForRegistration,
                AgeGroup = enrollTeacherCourseViewModel.AgeGroup,
                TeacherId = enrollTeacherCourseViewModel.TeacherId,
                SemesterId = enrollTeacherCourseViewModel.SemesterId,
                SectionName = enrollTeacherCourseViewModel.SectionName,
                PublicationDate = enrollTeacherCourseViewModel.PublicationDate,
                PublicationEndDate = enrollTeacherCourseViewModel.PublicationEndDate,
                WorkStartDate = enrollTeacherCourseViewModel.WorkStartDate,
                WorkEndDate = enrollTeacherCourseViewModel.WorkEndDate,
                CourseId = enrollTeacherCourseViewModel.CourseId,
                CountOfStudent = enrollTeacherCourseViewModel.CountOfStudent,
                CreatedBy = enrollTeacherCourseViewModel.CreatedBy,
                NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled,
                CalculationTypeId = enrollTeacherCourseViewModel.CalculationTypeId,
                AgeGroupTo = enrollTeacherCourseViewModel.AgeGroupTo,
            };
            db.EnrollTeacherCourses.Add(enrollTeacherCourse);
            db.SaveChanges();

            enrollTeacherCourse.Id = enrollTeacherCourse.Id;
            if (enrollTeacherCourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                if (course.CourseTranslations != null)
                {
                    foreach (var CourseTranslations in course.CourseTranslations)
                    {
                        var enrollTeacherCourseTranlation = new EnrollTeacherCourseTranlation()
                        {
                            CourseName = CourseTranslations.CourseName,
                            LanguageId = CourseTranslations.LanguageId,
                            SectionName = enrollTeacherCourseViewModel.SectionName,
                            EnrollCourseId = enrollTeacherCourse.Id,
                            NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled,
                        };
                        db.EnrollTeacherCourseTranlations.Add(enrollTeacherCourseTranlation);
                        db.SaveChanges();
                    }
                }
            }
            return enrollTeacherCourse;

        }

        public EnrollTeacherCourse EditEnrollTeacherCourse(EnrollTeacherCourseViewModel enrollTeacherCourseViewModel, EnrollTeacherCourse enrollTeacherCourse, Course course)
        {
            using (var db = new LearningManagementSystemContext())
            {

                enrollTeacherCourse.CourseId = enrollTeacherCourseViewModel.CourseId;
                enrollTeacherCourse.LearningMethodId = enrollTeacherCourseViewModel.LearningMethodId;
                enrollTeacherCourse.AgeAllowedForRegistration = enrollTeacherCourseViewModel.AgeAllowedForRegistration;
                enrollTeacherCourse.AgeGroup = enrollTeacherCourseViewModel.AgeGroup;
                enrollTeacherCourse.TeacherId = enrollTeacherCourseViewModel.TeacherId;
                enrollTeacherCourse.SemesterId = enrollTeacherCourseViewModel.SemesterId;
                enrollTeacherCourse.PublicationDate = enrollTeacherCourseViewModel.PublicationDate;
                enrollTeacherCourse.PublicationEndDate = enrollTeacherCourseViewModel.PublicationEndDate;
                enrollTeacherCourse.WorkStartDate = enrollTeacherCourseViewModel.WorkStartDate;
                enrollTeacherCourse.WorkEndDate = enrollTeacherCourseViewModel.WorkEndDate;
                enrollTeacherCourse.CountOfStudent = enrollTeacherCourseViewModel.CountOfStudent;
                enrollTeacherCourse.Status = enrollTeacherCourseViewModel.Status;
                enrollTeacherCourse.AgeGroupTo = enrollTeacherCourseViewModel.AgeGroupTo;
                //enrollTeacherCourse.CalculationTypeId = enrollTeacherCourseViewModel.CalculationTypeId;

                if (enrollTeacherCourseViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    enrollTeacherCourse.SectionName = enrollTeacherCourseViewModel.SectionName;
                    enrollTeacherCourse.CourseName = course.CourseName;
                    enrollTeacherCourse.NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled;
                }

                db.Entry(enrollTeacherCourse).State = EntityState.Modified;
                db.SaveChanges();
                if (enrollTeacherCourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    if (course.CourseTranslations != null)
                    {
                        foreach (var CourseTranslations in course.CourseTranslations)
                        {
                            var enrollTeacherCourseTranslation = db.EnrollTeacherCourseTranlations.FirstOrDefault(r =>
                            r.LanguageId == enrollTeacherCourseViewModel.LanguageId && r.EnrollCourseId == enrollTeacherCourseViewModel.Id);

                            if (enrollTeacherCourseTranslation != null)
                            {
                                enrollTeacherCourseTranslation.SectionName = enrollTeacherCourseViewModel.SectionName;
                                enrollTeacherCourseTranslation.CourseName = CourseTranslations.CourseName;
                                enrollTeacherCourseTranslation.NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled;


                                db.Entry(enrollTeacherCourseTranslation).State = EntityState.Modified;
                            }
                            else
                            {
                                var enrollTeacherCourseTran = new EnrollTeacherCourseTranlation()
                                {
                                    SectionName = enrollTeacherCourseViewModel.SectionName,
                                    NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled,
                                    CourseName = CourseTranslations.CourseName,
                                    LanguageId = CourseTranslations.LanguageId,
                                    EnrollCourseId = enrollTeacherCourseViewModel.Id
                                };
                                db.EnrollTeacherCourseTranlations.Add(enrollTeacherCourseTran);
                            }

                            db.SaveChanges();
                        }


                    }
                }
                return enrollTeacherCourse;
            }
        }

        public void EditEnrollTeacherCourseNote(string note, EnrollTeacherCourse enrollTeacherCourse, DateTime start, DateTime end)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollTeacherCourse.NotesForEnrolled = "<h6>Team Meating Bettween " + start.ToShortDateString() + " and " + end.ToShortDateString() + "</h6><a href='" + note + "'>Team Link<a>";
                db.Entry(enrollTeacherCourse).State = EntityState.Modified;
                var tran = enrollTeacherCourse.EnrollTeacherCourseTranlations.FirstOrDefault();
                if (tran != null)
                {
                    tran.NotesForEnrolled = "<h6>اجتماع تيم بين " + start.ToShortDateString() + " و " + end.ToShortDateString() + " </h6><a href='" + note + "'>تيم لينك<a>";
                    db.Entry(tran).State = EntityState.Modified;
                }
                else
                {
                    var tranCourse = new EnrollTeacherCourseTranlation()
                    {
                        CourseName = enrollTeacherCourse.CourseName,
                        EnrollCourseId = enrollTeacherCourse.Id,
                        LanguageId = (int)GeneralEnums.LanguageEnum.Arabic,
                        NotesForEnrolled = "<h6>اجتماع تيم بين " + start.ToShortDateString() + " و " + end.ToShortDateString() + " </h6><a href='" + note + "'>تيم لينك<a>",
                        SectionName = enrollTeacherCourse.SectionName,
                    };
                    db.EnrollTeacherCourseTranlations.Add(tranCourse);
                }
                db.SaveChanges();
            }
        }

        public EnrollTeacherCourse EditEnrollTeacherCourse_WithoutUsing(EnrollTeacherCourseViewModel enrollTeacherCourseViewModel, EnrollTeacherCourse enrollTeacherCourse, Course course, LearningManagementSystemContext db)
        {
            enrollTeacherCourse.CourseId = enrollTeacherCourseViewModel.CourseId;
            enrollTeacherCourse.LearningMethodId = enrollTeacherCourseViewModel.LearningMethodId;
            enrollTeacherCourse.AgeAllowedForRegistration = enrollTeacherCourseViewModel.AgeAllowedForRegistration;
            enrollTeacherCourse.AgeGroup = enrollTeacherCourseViewModel.AgeGroup;
            enrollTeacherCourse.TeacherId = enrollTeacherCourseViewModel.TeacherId;
            enrollTeacherCourse.SemesterId = enrollTeacherCourseViewModel.SemesterId;
            enrollTeacherCourse.PublicationDate = enrollTeacherCourseViewModel.PublicationDate;
            enrollTeacherCourse.PublicationEndDate = enrollTeacherCourseViewModel.PublicationEndDate;
            enrollTeacherCourse.WorkStartDate = enrollTeacherCourseViewModel.WorkStartDate;
            enrollTeacherCourse.WorkEndDate = enrollTeacherCourseViewModel.WorkEndDate;
            enrollTeacherCourse.CountOfStudent = enrollTeacherCourseViewModel.CountOfStudent;
            enrollTeacherCourse.Status = enrollTeacherCourseViewModel.Status;
            enrollTeacherCourse.CalculationTypeId = enrollTeacherCourseViewModel.CalculationTypeId;
            enrollTeacherCourse.AgeGroupTo = enrollTeacherCourseViewModel.AgeGroupTo;

            if (enrollTeacherCourseViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
            {
                enrollTeacherCourse.SectionName = enrollTeacherCourseViewModel.SectionName;
                enrollTeacherCourse.CourseName = course.CourseName;
                enrollTeacherCourse.NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled;
            }


            db.Entry(enrollTeacherCourse).State = EntityState.Modified;
            db.SaveChanges();
            if (enrollTeacherCourseViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                if (course.CourseTranslations != null)
                {
                    foreach (var CourseTranslations in course.CourseTranslations)
                    {
                        var enrollTeacherCourseTranslation = db.EnrollTeacherCourseTranlations.FirstOrDefault(r =>
                        r.LanguageId == enrollTeacherCourseViewModel.LanguageId && r.EnrollCourseId == enrollTeacherCourseViewModel.Id);

                        if (enrollTeacherCourseTranslation != null)
                        {
                            enrollTeacherCourseTranslation.SectionName = enrollTeacherCourseViewModel.SectionName;
                            enrollTeacherCourseTranslation.NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled;
                            enrollTeacherCourseTranslation.CourseName = CourseTranslations.CourseName;
                            db.Entry(enrollTeacherCourseTranslation).State = EntityState.Modified;
                        }
                        else
                        {
                            var enrollTeacherCourseTran = new EnrollTeacherCourseTranlation()
                            {
                                SectionName = enrollTeacherCourseViewModel.SectionName,
                                NotesForEnrolled = enrollTeacherCourseViewModel.NotesForEnrolled,
                                CourseName = CourseTranslations.CourseName,
                                LanguageId = CourseTranslations.LanguageId,
                                EnrollCourseId = enrollTeacherCourseViewModel.Id
                            };
                            db.EnrollTeacherCourseTranlations.Add(enrollTeacherCourseTran);
                        }

                        db.SaveChanges();
                    }


                }
            }
            return enrollTeacherCourse;

        }
        public void DeleteEnrollTeacherCourse(EnrollTeacherCourse enrollTeacherCourse)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollTeacherCourse.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollTeacherCourse.DeletedOn = DateTime.Now;
                db.Entry(enrollTeacherCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteEnrollTeacherCourse_WithoutUsing(EnrollTeacherCourse enrollTeacherCourse, LearningManagementSystemContext db)
        {

            enrollTeacherCourse.Status = (int)GeneralEnums.StatusEnum.Deleted;
            enrollTeacherCourse.DeletedOn = DateTime.Now;
            db.Entry(enrollTeacherCourse).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void PassStudent(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCourse = db.EnrollStudentCourses.First(s => s.Id == id);
                EnrollStudentCourse.IsPass = true;
                db.Entry(EnrollStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public IPagedList<EnrollStudentCourse> GetEnrollmentCourse(int id, int? page, int pagination, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourses = db.EnrollStudentCourses.Include(r => r.EnrollStudentCourseAttachments).Include(r => r.Student).ThenInclude(s => s.Contact).ThenInclude(s => s.ContactTranslations).Where(r => r.CourseId == id &&
                          r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.EnrollStudentCourseAttachments.Any());

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = enrollTeacherCourses;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Student.Contact.FullName = trans.FullName;
                        }
                    }
                }

                return output;
            }
        }

        public void ChangeEnrollStudent(int ChangeEnrollStudentId, int EnrollTeacherCourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentCourse = db.EnrollStudentCourses.First(s => s.Id == ChangeEnrollStudentId);
                EnrollStudentCourse.CourseId = EnrollTeacherCourseId;
                db.Entry(EnrollStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public int GetEnrollmentCourseIdFormStudentId(int enrollTeacherCourseId, int studentId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var s = db.EnrollStudentCourses.FirstOrDefault(r => r.CourseId == enrollTeacherCourseId && r.StudentId == studentId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return s?.Id ?? 0;
            }
        }

        public struct Data
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<Data> GetEnrollmentCourseList(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollTeacherCourses = db.EnrollTeacherCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.EnrollTeacherCourseTranlations).Include(r => r.Course.CourseTranslations).Include(r => r.Semester.SemesterTranslations).Include(r => r.Teacher.Contact.ContactTranslations).AsNoTracking().ToList();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in enrollTeacherCourses)
                    {
                        var transEnroll = item.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transEnroll != null)
                            item.SectionName = transEnroll.SectionName;
                        var trans = item.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.CourseName = trans.CourseName;
                        var transContact = item.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transContact != null)
                            item.Teacher.Contact.FullName = transContact.FullName;
                        var transSemester = item.Semester?.SemesterTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transSemester != null)
                            item.Semester.Name = transSemester.Name;
                    }

                return enrollTeacherCourses.Select(r => new Data() { Id = r.Id, Name = r.CourseName + " : (" + (r.Semester?.Name ?? "No Semester") + " / " + (r.SectionName ?? "No SectionName") + ") : " + r.Teacher?.Contact?.FullName }).OrderByDescending(r => r.Id).ToList();
            }
        }

        public List<Course> GetCourseList(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.Courses.Include(a=>a.CourseTranslations).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                var output = courses;
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in output)
                    {
                        var trans = item.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.CourseName = trans.CourseName;
                    }

                return output.Select(r => new Course()
                {
                    Id = r.Id,
                    CourseName = r.CourseName,
                }).ToList();
            }
        }

        public List<Course> GetEnrollCourseListForSemester(int? semester, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.EnrollTeacherCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Course.CourseTranslations).AsEnumerable();

                if (semester > 0)
                    courses = courses.Where(r => r.SemesterId == semester);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in courses)
                    {
                        var trans = item.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.CourseName = trans.CourseName;
                    }

                var result = courses.Select(r => r.Course).GroupBy(c => c.Id).Select(g => g.FirstOrDefault()).ToList();

                return result.Select(r => new Course()
                    {
                        Id = r.Id,
                        CourseName = r.CourseName
                    }).ToList();
            }
        }

        public List<Data> GetTeacherList(int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var teachers = db.Trainers.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Contact.ContactTranslations).AsQueryable();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in teachers)
                    {
                        var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.Contact.FullName = trans.FullName;
                    }

                return teachers.Select(r => new Data() { Id = r.Id, Name = r.Contact.FullName }).ToList();
            }
        }

        public List<Data> GetTeacherListByCourseId(int? semester, int? courseId ,int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var teachers = db.EnrollTeacherCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Teacher.Contact.ContactTranslations).AsEnumerable();

                if (semester > 0)
                    teachers = teachers.Where(r => r.SemesterId == semester);
                if (courseId > 0)
                    teachers = teachers.Where(r => r.CourseId == courseId);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in teachers)
                    {
                        var trans = item.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.Teacher.Contact.FullName = trans.FullName;
                    }

                var result = teachers.Select(r => r.Teacher).Where(r=> r.Status != (int)GeneralEnums.StatusEnum.Deleted).GroupBy(c => c.Id).Select(g => g.FirstOrDefault()).ToList();

                return result.Select(r => new Data() { Id = r.Id, Name = r.Contact.FullName }).ToList();
            }
        }

        public decimal GetCourseMark(int courseId)
        {
            var course = _context.EnrollTeacherCourses.Include(r => r.Course).Include(r => r.PracticalEnrollmentExams).Include(r => r.EnrollCourseExams).Include(r => r.EnrollCourseQuizzes)
                .FirstOrDefault(r => r.Id == courseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (course != null)
            {
                decimal total = (course?.Course?.AssignmentMark ?? 0) + (decimal)(course?.EnrollCourseExams?.Sum(r => r.ExamFinalMark) ?? 0) + (course?.EnrollCourseQuizzes?.Count() > 0 ? 30 : 0);
                foreach (var item in course.PracticalEnrollmentExams)
                {
                    var practical = _context.PracticalExams.Find(item.PracticalExamId);
                    if (practical != null)
                        total = total + (practical.MarkAfterConversion ?? 0);
                }

                return total;
            }
            return 0;
        }

        public void CloseCourse(int courseId)
        {
            var course = GetEnrollTeacherCourseById(courseId);
            course.IsCourseDone = true;
            _context.Entry(course).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
