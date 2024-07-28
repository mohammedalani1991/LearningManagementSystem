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

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollCourseExamService : IEnrollCourseExamService
    {
        private readonly ISettingService _settingService;

        public EnrollCourseExamService(ISettingService settingService)
        {
            _settingService = settingService;

        }

        public IPagedList<EnrollCourseExam> GetEnrollCourseExams(int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? EnrollTeacherCourseId = 0, int? TeacherId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseExams = db.EnrollCourseExams.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.EnrollCourseExamTranslations).Include(d => d.EnrollStudentExams).Include(d => d.EnrollTeacherCourse.EnrollTeacherCourseTranlations).AsQueryable();

                if (TeacherId > 0)
                    EnrollCourseExams = EnrollCourseExams.Where(r => r.EnrollTeacherCourse.TeacherId == TeacherId);

                if (EnrollTeacherCourseId > 0)
                    EnrollCourseExams = EnrollCourseExams.Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId);

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = EnrollCourseExams;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);


                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var transExamTemplates = item.EnrollCourseExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transExamTemplates != null)
                        {
                            item.Name = transExamTemplates.Name;
                            item.Description = transExamTemplates.Description;

                        }
                        var EnrollTeacherCourseTranlations = item.EnrollTeacherCourse.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (EnrollTeacherCourseTranlations != null)
                        {
                            item.EnrollTeacherCourse.CourseName = EnrollTeacherCourseTranlations.CourseName;
                        }
                    }
                }

                return output;
            }
        }
        public EnrollCourseExam AddEnrollCourseExam(EnrollCourseExamViewModel EnrollCourseExamViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollCourseExam = new EnrollCourseExam()
                {
                    CreatedOn = DateTime.Now,
                    Status = EnrollCourseExamViewModel.Status,
                    Name = EnrollCourseExamViewModel.Name,
                    Description = EnrollCourseExamViewModel.Description1,
                    EnrollTeacherCourseId = EnrollCourseExamViewModel.EnrollTeacherCourseId,
                    ExamTemplateId = EnrollCourseExamViewModel.ExamTemplateId > 0 ? EnrollCourseExamViewModel.ExamTemplateId : null,
                    PublishDate = EnrollCourseExamViewModel.IsOnlineLearningMethod ? null : EnrollCourseExamViewModel.PublishDate,
                    PublishEndDate = EnrollCourseExamViewModel.IsOnlineLearningMethod ? null : EnrollCourseExamViewModel.PublishEndDate,
                    IsPrerequisite = EnrollCourseExamViewModel.IsPrerequisite,
                    ExamFinalMark = EnrollCourseExamViewModel.ExamFinalMark,
                    Duration = EnrollCourseExamViewModel.Duration,
                    TestTypeId = EnrollCourseExamViewModel.TestTypeID,
                    EnrollLectureId = EnrollCourseExamViewModel.EnrollLectureId > 0 ? EnrollCourseExamViewModel.EnrollLectureId : null,
                    CreatedBy = EnrollCourseExamViewModel.CreatedBy,
                    Shuffle = EnrollCourseExamViewModel.Shuffle,
                };
                db.EnrollCourseExams.Add(enrollCourseExam);
                db.SaveChanges();

                enrollCourseExam.Id = enrollCourseExam.Id;

                if (EnrollCourseExamViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {

                    var enrollCourseExamTran = new EnrollCourseExamTranslation()
                    {
                        Name = EnrollCourseExamViewModel.Name,
                        Description = EnrollCourseExamViewModel.Description1,
                        LanguageId = EnrollCourseExamViewModel.LanguageId,
                        EnrollCourseExamId = enrollCourseExam.Id
                    };
                    db.EnrollCourseExamTranslations.Add(enrollCourseExamTran);
                    db.SaveChanges();

                }
                return enrollCourseExam;
            }
        }

        public EnrollCourseExam AddEnrollCourseExam_WithoutUsing(EnrollCourseExamViewModel EnrollCourseExamViewModel, LearningManagementSystemContext db)
        {
            var enrollCourseExam = new EnrollCourseExam()
            {
                CreatedOn = DateTime.Now,
                Status = EnrollCourseExamViewModel.Status,
                Name = EnrollCourseExamViewModel.Name,
                Description = EnrollCourseExamViewModel.Description1,
                EnrollTeacherCourseId = EnrollCourseExamViewModel.EnrollTeacherCourseId,
                ExamTemplateId = EnrollCourseExamViewModel.ExamTemplateId > 0 ? EnrollCourseExamViewModel.ExamTemplateId : null,
                PublishDate = EnrollCourseExamViewModel.IsOnlineLearningMethod ? null : EnrollCourseExamViewModel.PublishDate,
                PublishEndDate = EnrollCourseExamViewModel.IsOnlineLearningMethod ? null : EnrollCourseExamViewModel.PublishEndDate,
                IsPrerequisite = EnrollCourseExamViewModel.IsPrerequisite,
                ExamFinalMark = EnrollCourseExamViewModel.ExamFinalMark,
                Duration = EnrollCourseExamViewModel.Duration,
                TestTypeId = EnrollCourseExamViewModel.TestTypeID,
                EnrollLectureId = EnrollCourseExamViewModel.EnrollLectureId > 0 ? EnrollCourseExamViewModel.EnrollLectureId : null,
                CreatedBy = EnrollCourseExamViewModel.CreatedBy,
                Shuffle = EnrollCourseExamViewModel.Shuffle,
            };
            db.EnrollCourseExams.Add(enrollCourseExam);
            db.SaveChanges();

            enrollCourseExam.Id = enrollCourseExam.Id;

            if (EnrollCourseExamViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {

                var enrollCourseExamTran = new EnrollCourseExamTranslation()
                {
                    Name = EnrollCourseExamViewModel.Name,
                    Description = EnrollCourseExamViewModel.Description1,
                    LanguageId = EnrollCourseExamViewModel.LanguageId,
                    EnrollCourseExamId = enrollCourseExam.Id
                };
                db.EnrollCourseExamTranslations.Add(enrollCourseExamTran);
                db.SaveChanges();

            }
            return enrollCourseExam;
        }

        public EnrollCourseExam EditEnrollCourseExam(EnrollCourseExamViewModel EnrollCourseExamViewModel, EnrollCourseExam enrollCourseExam)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollCourseExam.EnrollTeacherCourseId = EnrollCourseExamViewModel.EnrollTeacherCourseId;
                enrollCourseExam.ExamTemplateId = EnrollCourseExamViewModel.ExamTemplateId > 0 ? EnrollCourseExamViewModel.ExamTemplateId : null;
                enrollCourseExam.PublishDate = EnrollCourseExamViewModel.PublishDate;
                enrollCourseExam.PublishEndDate = EnrollCourseExamViewModel.PublishEndDate;
                enrollCourseExam.IsPrerequisite = EnrollCourseExamViewModel.IsPrerequisite;
                enrollCourseExam.Duration = EnrollCourseExamViewModel.Duration;
                enrollCourseExam.ExamFinalMark = EnrollCourseExamViewModel.ExamFinalMark;
                enrollCourseExam.TestTypeId = EnrollCourseExamViewModel.TestTypeID;
                enrollCourseExam.EnrollLectureId = EnrollCourseExamViewModel.EnrollLectureId > 0 ? EnrollCourseExamViewModel.EnrollLectureId : null;
                enrollCourseExam.Status = EnrollCourseExamViewModel.Status;
                enrollCourseExam.Shuffle = EnrollCourseExamViewModel.Shuffle;

                if (EnrollCourseExamViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    enrollCourseExam.Name = EnrollCourseExamViewModel.Name;
                    enrollCourseExam.Description = EnrollCourseExamViewModel.Description1;
                }

                db.Entry(enrollCourseExam).State = EntityState.Modified;
                db.SaveChanges();

                if (EnrollCourseExamViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var EnrollCourseExamTranslations = db.EnrollCourseExamTranslations.FirstOrDefault(r =>
                        r.LanguageId == EnrollCourseExamViewModel.LanguageId &&
                        r.EnrollCourseExamId == enrollCourseExam.Id);
                    if (EnrollCourseExamTranslations != null)
                    {
                        EnrollCourseExamTranslations.Name = EnrollCourseExamViewModel.Name;
                        EnrollCourseExamTranslations.Description = EnrollCourseExamViewModel.Description1;
                        db.Entry(EnrollCourseExamTranslations).State = EntityState.Modified;
                    }
                    else
                    {
                        var EnrollCourseExamTranslation = new EnrollCourseExamTranslation()
                        {
                            Name = EnrollCourseExamViewModel.Name,
                            Description = EnrollCourseExamViewModel.Description1,
                            EnrollCourseExamId = enrollCourseExam.Id,
                            LanguageId = EnrollCourseExamViewModel.LanguageId
                        };
                        db.EnrollCourseExamTranslations.Add(EnrollCourseExamTranslation);
                    }

                    db.SaveChanges();
                }

                return enrollCourseExam;
            }

        }
        public EnrollCourseExam AddEnrollCourseExamForAdmin(ExamTemplate ExamTemplate, int EnrollCourseId, LearningManagementSystemContext db)
        {

            var enrollCourseExam = new EnrollCourseExam()
            {
                CreatedOn = DateTime.Now,
                Status = (int)GeneralEnums.StatusEnum.Active,
                Name = ExamTemplate.Name,
                Description = ExamTemplate.Description,
                EnrollTeacherCourseId = EnrollCourseId,
                ExamTemplateId = ExamTemplate.Id,
                Duration = ExamTemplate.Duration,
                CreatedBy = ExamTemplate.CreatedBy
            };
            db.EnrollCourseExams.Add(enrollCourseExam);
            db.SaveChanges();

            enrollCourseExam.Id = enrollCourseExam.Id;


            if (ExamTemplate.ExamTemplateTranslations != null)
            {
                foreach (var ExamTemplateTranslations in ExamTemplate.ExamTemplateTranslations)
                {
                    var enrollCourseExamTran = new EnrollCourseExamTranslation()
                    {
                        Name = ExamTemplateTranslations.Name,
                        Description = ExamTemplateTranslations.Description,
                        LanguageId = ExamTemplateTranslations.LanguageId,
                        EnrollCourseExamId = enrollCourseExam.Id
                    };
                    db.EnrollCourseExamTranslations.Add(enrollCourseExamTran);
                    db.SaveChanges();
                }
            }

            return enrollCourseExam;

        }


        public EnrollCourseExamViewModel GetEnrollCourseExamById(int Id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExams = db.EnrollCourseExams.Include(r => r.EnrollTeacherCourse).Include(r => r.ExamTemplate).Include(r => r.EnrollLecture).Include(s => s.EnrollCourseExamTranslations).FirstOrDefault(r => r.Id == Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {

                    var trans = EnrollCourseExams.EnrollCourseExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        EnrollCourseExams.Name = trans.Name;
                        EnrollCourseExams.Description = trans.Description;
                    }
                    var trans2 = db.ExamTemplateTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.ExamId == EnrollCourseExams.ExamTemplateId);
                    if (trans2 != null)
                    {
                        EnrollCourseExams.ExamTemplate.Name = trans2.Name;
                    }
                    var trans3 = db.EnrollLectureTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.EnrollLectureId == EnrollCourseExams.EnrollLectureId);
                    if (trans3 != null)
                    {
                        EnrollCourseExams.EnrollLecture.LectureName = trans3.LectureName;
                    }
                }
                return new EnrollCourseExamViewModel(EnrollCourseExams);
            }
        }

        public EnrollCourseExam GetEnrollCourseExamModelById(int Id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.EnrollCourseExams.Include(r => r.EnrollTeacherCourse).Include(s => s.EnrollCourseExamTranslations).FirstOrDefault(r => r.Id == Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            }
        }

        public EnrollCourseExamViewModel GetEnrollCourseExamById(int Id)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExams = db.EnrollCourseExams.Include(r => r.EnrollTeacherCourse).Include(s => s.EnrollCourseExamTranslations).FirstOrDefault(r => r.Id == Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (EnrollCourseExams != null)
                {
                    return new EnrollCourseExamViewModel(EnrollCourseExams);
                }
                else
                {
                    return null;
                }

            }
        }
        public EnrollCourseExam GetEnrollCourseExamByCourseExamId(int Id)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExams = db.EnrollCourseExams.Include(r => r.EnrollTeacherCourse).Include(s => s.EnrollCourseExamTranslations).FirstOrDefault(r => r.Id == Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return EnrollCourseExams;
            }
        }

        public List<EnrollCourseExam> GetEnrollCourseExamByEnrollTeacherCourseId(int EnrollTeacherCourseId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExams = db.EnrollCourseExams.Include(s => s.EnrollCourseExamQuestions).Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId && r.TestTypeId == (int)GeneralEnums.TestType.Exam && r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.IsPrerequisite != true).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var EnrollCourseExam in EnrollCourseExams)
                    {
                        var trans = db.EnrollCourseExamTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollCourseExamId == EnrollCourseExam.Id);
                        if (trans != null)
                        {
                            EnrollCourseExam.Name = trans.Name;
                            EnrollCourseExam.Description = trans.Description;
                        }
                    }


                }
                return EnrollCourseExams;
            }
        }
        public List<EnrollCourseExam> GetEnrollCourseQuizByEnrollTeacherCourseId(int EnrollTeacherCourseId, int? EnrollLectureId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExams = db.EnrollCourseExams.Include(s => s.EnrollCourseExamQuestions).Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId && r.TestTypeId == (int)GeneralEnums.TestType.Quiz && r.EnrollLectureId == EnrollLectureId && EnrollLectureId.HasValue && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var EnrollCourseExam in EnrollCourseExams)
                    {
                        var trans = db.EnrollCourseExamTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollCourseExamId == EnrollCourseExam.Id);
                        if (trans != null)
                        {
                            EnrollCourseExam.Name = trans.Name;
                            EnrollCourseExam.Description = trans.Description;
                        }
                    }


                }
                return EnrollCourseExams;
            }
        }
        public List<EnrollCourseExam> GetPrerequisiteEnrollCourseExam(int EnrollTeacherCourseId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExams = db.EnrollCourseExams.Include(r => r.EnrollCourseExamQuestions).Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId && r.IsPrerequisite == true && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var EnrollCourseExam in EnrollCourseExams)
                    {
                        var trans = db.EnrollCourseExamTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollCourseExamId == EnrollCourseExam.Id);
                        if (trans != null)
                        {
                            EnrollCourseExam.Name = trans.Name;
                            EnrollCourseExam.Description = trans.Description;
                        }
                    }


                }
                return EnrollCourseExams;
            }
        }

        public List<EnrollCourseExam> GetEnrollCourseExamByEnrollTeacherCourseId(int EnrollTeacherCourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var EnrollCourseExams = db.EnrollCourseExams.Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return EnrollCourseExams;
            }
        }
        public EnrollCourseExam GetEnrollCourseExamById_WithoutUsing(int CourseExamID, LearningManagementSystemContext db)
        {

            var EnrollCourseExams = db.EnrollCourseExams.Where(r => r.Id == CourseExamID && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefault();
            return EnrollCourseExams;

        }
        public List<EnrollCourseExam> GetEnrollCourseExamByEnrollTeacherCourseId_WithoutUsing(int EnrollTeacherCourseId, LearningManagementSystemContext db)
        {

            var EnrollCourseExams = db.EnrollCourseExams.Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
            return EnrollCourseExams;

        }
        public int DeleteEnrollCourseExam(EnrollCourseExam enrollCourseExam)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollCourseExam.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollCourseExam.DeletedOn = DateTime.Now;
                db.Entry(enrollCourseExam).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public void DeleteEnrollCourseExamByEnrollTeacherCourseID(int EnrollTeacherCourseID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseExams = db.EnrollCourseExams.Where(e => e.EnrollTeacherCourseId == EnrollTeacherCourseID).ToList();
                foreach (var EnrollCourseExam in EnrollCourseExams)
                {
                    var EnrollCourseExamTranslations = db.EnrollCourseExamTranslations.Where(e => e.EnrollCourseExamId == EnrollCourseExam.Id).ToList();
                    db.EnrollCourseExamTranslations.RemoveRange(EnrollCourseExamTranslations);
                }
                db.EnrollCourseExams.RemoveRange(EnrollCourseExams);
                db.SaveChanges();
            }

        }
        public void DeleteEnrollCourseExamByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db)
        {

            var EnrollCourseExams = db.EnrollCourseExams.Where(e => e.EnrollTeacherCourseId == EnrollTeacherCourseID).ToList();
            foreach (var EnrollCourseExam in EnrollCourseExams)
            {
                var EnrollCourseExamTranslations = db.EnrollCourseExamTranslations.Where(e => e.EnrollCourseExamId == EnrollCourseExam.Id).ToList();
                db.EnrollCourseExamTranslations.RemoveRange(EnrollCourseExamTranslations);
            }
            db.EnrollCourseExams.RemoveRange(EnrollCourseExams);
            db.SaveChanges();


        }
        //public EnrollCourseExamViewModel GetEnrollCourseExamById(int id)
        //{
        //    using (var db = new LearningManagementSystemContext())
        //    {
        //        return new EnrollCourseExamViewModel(db.EnrollCourseExams.First(s => s.Id == id));
        //    }

        //}

        public void DeleteEnrollCourseExamByID_WithoutUsing(int CourseExamID, LearningManagementSystemContext db)
        {

            var EnrollCourseExams = db.EnrollCourseExams.Where(e => e.Id == CourseExamID).FirstOrDefault();
            var EnrollCourseExamTranslations = db.EnrollCourseExamTranslations.Where(e => e.EnrollCourseExamId == EnrollCourseExams.Id).ToList();
            db.EnrollCourseExamTranslations.RemoveRange(EnrollCourseExamTranslations);

            db.EnrollCourseExams.RemoveRange(EnrollCourseExams);
            db.SaveChanges();


        }
        public void EditMarkAdoption(EnrollCourseExam enrollCourseExam, bool adopt)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollCourseExam.MarkAdoption = adopt;
                db.Entry(enrollCourseExam).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
