using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollStudentExamService : IEnrollStudentExamService
    {

        public EnrollStudentExam GetEnrollStudentExamByEnrollCourseExamId(int EnrollCourseExamId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentExams = db.EnrollStudentExams.FirstOrDefault(s => s.EnrollCourseExamId == EnrollCourseExamId  && s.Status == (int)GeneralEnums.StatusEnum.Active);
                return EnrollStudentExams;
            }
        }

        public EnrollStudentExam GetEnrollStudentExamByEnrollCourseExamId(int EnrollCourseExamId, int EnrollStudentCourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentExams = db.EnrollStudentExams.FirstOrDefault(s => s.EnrollCourseExamId == EnrollCourseExamId && s.EnrollStudentCourseId == EnrollStudentCourseId && s.Status == (int)GeneralEnums.StatusEnum.Active);
                return EnrollStudentExams;
            }
        }
        public EnrollStudentExam GetEnrollStudentExamByEnrollStudentCourseId(int EnrollStudentCourseId,int EnrollCourseExamId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentExams = db.EnrollStudentExams.OrderByDescending(r=>r.Id).Include(e=>e.EnrollStudentExamAnswers).FirstOrDefault(s =>  s.EnrollStudentCourseId == EnrollStudentCourseId && s.EnrollCourseExamId == EnrollCourseExamId && s.Status == (int)GeneralEnums.StatusEnum.Active);
                return EnrollStudentExams;
            }
        }
        public IPagedList<EnrollStudentExam> GetEnrollStudentExam(int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? EnrollCourseExamId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentExams = db.EnrollStudentExams.Include(r => r.EnrollCourseExam).ThenInclude(r => r.EnrollCourseExamQuestions).ThenInclude(r => r.Question).ThenInclude(r => r.Question).Include(r => r.EnrollStudentCourse).ThenInclude(d => d.Student).ThenInclude(r => r.Contact).Where(r =>
                          r.Status != (int)GeneralEnums.StatusEnum.Deleted);


                if (EnrollCourseExamId > 0)
                {
                    EnrollStudentExams = EnrollStudentExams.Where(r => r.EnrollCourseExamId == EnrollCourseExamId);
                }


                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = EnrollStudentExams;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);


                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {

                        var transContactTranslations = db.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.ContactId == item.EnrollStudentCourse.Student.ContactId && r.Contact.Status != (int)GeneralEnums.StatusEnum.Deleted);
                        if (transContactTranslations != null)
                        {
                            item.EnrollStudentCourse.Student.Contact.FullName  = transContactTranslations.FullName;
                        }

                    }
                }


                return output;
            }
        }
        public EnrollStudentExam AddEnrollStudentExam(EnrollStudentExamViewModel enrollStudentExamViewModel, LearningManagementSystemContext db)
        {
            
                var enrollStudentExam = new EnrollStudentExam()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedBy = enrollStudentExamViewModel.CreatedBy,
                    MarkAfterConversion = enrollStudentExamViewModel.MarkAfterConversion,
                    MarkHeGot = enrollStudentExamViewModel.MarkHeGot,
                    FinalMark = enrollStudentExamViewModel.FinalMark,
                    EnrollCourseExamId = enrollStudentExamViewModel.EnrollCourseExamId,
                    EnrollStudentCourseId = enrollStudentExamViewModel.EnrollStudentCourseId,
                   
                };

                db.EnrollStudentExams.Add(enrollStudentExam);
                db.SaveChanges();

                return enrollStudentExam;
            
        }
        public void EditMarkHeGotEnrollStudentExam(EnrollStudentExamViewModel enrollStudentExamViewModel, EnrollStudentExam enrollStudentExam, LearningManagementSystemContext db)
        {

            enrollStudentExam.FinalMark = enrollStudentExamViewModel.FinalMark;
            enrollStudentExam.MarkHeGot = enrollStudentExamViewModel.MarkHeGot;
            enrollStudentExam.MarkAfterConversion = enrollStudentExamViewModel.MarkAfterConversion;
            db.Entry(enrollStudentExam).State = EntityState.Modified;
            db.SaveChanges();

        }
        public EnrollStudentExam GetEnrollStudentExam(int Id)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollStudentExam = db.EnrollStudentExams.FirstOrDefault(r => r.Id == Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return EnrollStudentExam;
            }
        }
        public string CheckAbilityToEnrollStudentExam(int EnrollCourseExamId, int EnrollStudentCourseId, int StudentId, bool IsExamPreRequest = false, bool TakeAgain = false)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {

                    var EnrollStudentExam = db.EnrollStudentExams.FirstOrDefault(s => s.EnrollCourseExamId == EnrollCourseExamId && s.EnrollStudentCourseId == EnrollStudentCourseId && s.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var EnrollCourseExam = db.EnrollCourseExams.Include(r => r.EnrollTeacherCourse).FirstOrDefault(r => r.Id == EnrollCourseExamId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var EnrollCourseExamQuestions = db.EnrollCourseExamQuestions.Where(r => r.EnrollCourseExamId == EnrollCourseExamId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Count();
                    var EnrollStudentCourses = db.EnrollStudentCourses.FirstOrDefault(r => r.CourseId == EnrollCourseExam.EnrollTeacherCourseId && r.StudentId== StudentId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var enrollTeacherCours = db.EnrollTeacherCourses.FirstOrDefault(r=>r.Id== EnrollStudentCourses.CourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var IsOnlineLearningMethod = enrollTeacherCours.LearningMethodId == (int)GeneralEnums.LearningMethodEnum.Online;
                    if (!IsExamPreRequest)
                    {
                        if (EnrollStudentCourses == null)
                        {
                            return "StudentNotEnrolledInCourse";
                        }

                        if (EnrollStudentExam != null)
                        {
                            return "StudentSubmittedExamBefore";
                        }

                        if (EnrollCourseExamQuestions == 0)
                        {
                            return "ZeroQuestionsInEnrollCourseExam";
                        }

                            if (!IsOnlineLearningMethod)
                            {
                                if (LookupHelper.ConvertTimeToSystemTimeZone(EnrollCourseExam.PublishDate).Value > LookupHelper.ConvertTimeToSystemTimeZone(DateTime.Now).Value)
                                {
                                    return "ExamNotStartYet";
                                }
                                if (!LookupHelper.IsBewteenTwoDates(LookupHelper.ConvertTimeToSystemTimeZone(DateTime.Now).Value, LookupHelper.ConvertTimeToSystemTimeZone(EnrollCourseExam.PublishDate).Value, LookupHelper.ConvertTimeToSystemTimeZone(EnrollCourseExam.PublishEndDate).Value))
                                {
                                    return "ExpiredDates";
                                }
                            }
                    }
                    else
                    {

                        if (EnrollStudentExam != null && TakeAgain == false)
                        {
                            return "StudentSubmittedExamBefore";
                        }
                        if (EnrollCourseExamQuestions == 0)
                        {
                            return "ZeroQuestionsInEnrollCourseExam";
                        }
                    }

                }
                catch (Exception ex)
                {
                    return "Fail";
                }

                return "done";
            }
        }

        public string CheckHasPreRequestsExam(int EnrollTeacherCourseId, int StudentId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                try
                {
                    var enrollTeacherCourse = db.EnrollTeacherCourses.FirstOrDefault(r => r.Id == EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    var EnrollCourseExams = db.EnrollCourseExams.Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId && r.IsPrerequisite == true && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                    //EnrollCourseExams
                    foreach (var _enrollCourseExam in EnrollCourseExams)
                    {
                        var EnrollStudentExam_Prerequistis = db.EnrollStudentExams.Include(h => h.EnrollStudentCourse).OrderByDescending(e=>e.Id).FirstOrDefault(r => r.EnrollCourseExamId == _enrollCourseExam.Id && r.EnrollStudentCourse.StudentId == StudentId && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.EnrollStudentCourse.Status == (int)GeneralEnums.StatusEnum.Active);
                   
                        if (EnrollStudentExam_Prerequistis != null) 
                        {
                            var MarkHeGot = ((EnrollStudentExam_Prerequistis.MarkHeGot.HasValue) ? EnrollStudentExam_Prerequistis.MarkHeGot.Value : 0);
                            var FinalMark = ((EnrollStudentExam_Prerequistis.FinalMark.HasValue) ? EnrollStudentExam_Prerequistis.FinalMark.Value : 0);
                            if(!(MarkHeGot >= (FinalMark / 2))) //mean the student not succeeded in the exam
                                return "HasPreRequestsExam";
                        }
                        else
                        {
                            return "HasPreRequestsExam";
                        }

                    }

                }
                catch (Exception ex)
                {
                    return "Fail";
                }

                return "done";
            }
        }
    }
}
