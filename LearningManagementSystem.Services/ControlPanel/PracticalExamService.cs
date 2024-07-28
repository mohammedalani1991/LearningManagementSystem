using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class PracticalExamService : IPracticalExamService
    {
        private readonly LearningManagementSystemContext _context;

        public PracticalExamService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public IPagedList<PracticalExam> GetPracticalExams(int? type, string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var PracticalExams = _context.PracticalExams.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.PracticalExamTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    PracticalExams = PracticalExams.Where(r => r.Name.Contains(searchText));
                else
                    PracticalExams = PracticalExams.Where(r => r.PracticalExamTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
            }
            if (type != null)
                PracticalExams = PracticalExams.Where(r => r.TypeId == type);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = PracticalExams;
            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Name = trans.Name;
                }

            return output;
        }

        public IPagedList<PracticalExamCourse> GetPracticalExamForCourse(int courseId, int? type, string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var PracticalExams = _context.PracticalExamCourses.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.CourseId == courseId)
                .Include(r => r.PracticalExam.PracticalExamTranslations).Where(r=>r.PracticalExam.Status != (int)GeneralEnums.StatusEnum.Deleted).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    PracticalExams = PracticalExams.Where(r => r.PracticalExam.Name.Contains(searchText));
                else
                    PracticalExams = PracticalExams.Where(r => r.PracticalExam.PracticalExamTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
            }
            if (type != null)
                PracticalExams = PracticalExams.Where(r => r.PracticalExam.TypeId == type);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = PracticalExams;
            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.PracticalExam.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.PracticalExam.Name = trans.Name;
                }

            return output;
        }

        public PracticalExam GetPracticalExamById(int id)
        {
            var practicalExam = _context.PracticalExams.Find(id);
            return practicalExam;
        }

        public PracticalExam GetPracticalExamById(int id, int languageId)
        {
            var practicalExam = _context.PracticalExams.Include(r => r.PracticalExamTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = practicalExam?.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    practicalExam.Name = trans.Name;
            }
            return practicalExam;
        }

        public PracticalExam GetPracticalExamByIdWitoutStatus(int id, int languageId)
        {
            var practicalExam = _context.PracticalExams.Include(r => r.PracticalExamTranslations).FirstOrDefault(r => r.Id == id);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = practicalExam?.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    practicalExam.Name = trans.Name;
            }
            return practicalExam;
        }

        public void AddPracticalExam(PracticalExamViewModel practicalExamViewModel)
        {
            var practicalExam = new PracticalExam()
            {
                CreatedOn = DateTime.Now,
                Status = practicalExamViewModel.Status,
                Name = practicalExamViewModel.Name,
                Mark = practicalExamViewModel.Mark,
                MarkAfterConversion = practicalExamViewModel.MarkAfterConversion,
                CreatedBy = practicalExamViewModel.CreatedBy,
                TypeId = practicalExamViewModel.TypeId,

            };
            _context.PracticalExams.Add(practicalExam);
            _context.SaveChanges();

            practicalExam.Id = practicalExam.Id;

            if (practicalExamViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var practicalExamTran = new PracticalExamTranslation()
                {
                    Name = practicalExamViewModel.Name,
                    LanguageId = practicalExamViewModel.LanguageId,
                    PracticalExamId = practicalExam.Id,
                };
                _context.PracticalExamTranslations.Add(practicalExamTran);
                _context.SaveChanges();
            }
        }

        public void EditPracticalExam(PracticalExamViewModel practicalExamViewModel, PracticalExam practicalExam)
        {
            practicalExam.Status = practicalExamViewModel.Status;
            practicalExam.Mark = practicalExamViewModel.Mark;
            practicalExam.MarkAfterConversion = practicalExamViewModel.MarkAfterConversion;

            if (practicalExamViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                practicalExam.Name = practicalExamViewModel.Name;

            _context.Entry(practicalExam).State = EntityState.Modified;
            _context.SaveChanges();

            if (practicalExamViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var practicalExamTranslation = _context.PracticalExamTranslations.FirstOrDefault(r =>
                    r.LanguageId == practicalExamViewModel.LanguageId &&
                    r.PracticalExamId == practicalExam.Id);
                if (practicalExamTranslation != null)
                {
                    practicalExamTranslation.Name = practicalExamViewModel.Name;
                    _context.Entry(practicalExamTranslation).State = EntityState.Modified;
                }
                else
                {
                    var practicalExamTran = new PracticalExamTranslation()
                    {
                        Name = practicalExamViewModel.Name,
                        LanguageId = practicalExamViewModel.LanguageId,
                        PracticalExamId = practicalExamViewModel.Id
                    };
                    _context.PracticalExamTranslations.Add(practicalExamTran);
                }
                _context.SaveChanges();
            }
        }

        public void DeletePracticalExam(PracticalExam practicalExam)
        {
            practicalExam.Status = (int)GeneralEnums.StatusEnum.Deleted;
            practicalExam.DeletedOn = DateTime.Now;
            _context.Entry(practicalExam).State = EntityState.Modified;
            _context.SaveChanges();
        }



        public List<PracticalExamQuestion> GetPracticalExamQuestions(int id)
        {
            var PracticalExams = _context.PracticalExamQuestions.Where(r => r.PracticalExamId == id);
            return PracticalExams.ToList();
        }

        public PracticalExamQuestion GetExamQuestion(int id, int questionId)
        {
            var examQuestion = _context.PracticalExamQuestions.FirstOrDefault(r => r.PracticalExamId == id && r.PracticalQuestionId == questionId);
            return examQuestion;
        }
        public void AddPracticalExamQuestion(PracticalExamQuestion practicalExamQuestion)
        {
            _context.PracticalExamQuestions.Add(practicalExamQuestion);
            _context.SaveChanges();
        }

        public void RemovePracticalExamQuestion(PracticalExamQuestion practicalExamQuestion)
        {
            _context.PracticalExamQuestions.Remove(practicalExamQuestion);
            _context.SaveChanges();
        }

        public PracticalExamCourse GetPracticalExamCourse(int id)
        {
            var practicalExams = _context.PracticalExamCourses.FirstOrDefault(r => r.Id == id);
            return practicalExams;
        }
        public List<PracticalExamCourse> GetPracticalExamForCourse(int id)
        {
            var practicalExams = _context.PracticalExamCourses.Where(r => r.CourseId == id);
            return practicalExams.ToList();
        }
        public PracticalExamCourse GetCourseExam(int courseId, int examId)
        {
            var examCourse = _context.PracticalExamCourses.FirstOrDefault(r => r.CourseId == courseId && r.PracticalExamId == examId);
            return examCourse;
        }
        public void AddPracticalExamCourse(PracticalExamCourse practicalExamCourse)
        {
            _context.PracticalExamCourses.Add(practicalExamCourse);
            _context.SaveChanges();
        }

        public void RemovePracticalExamCourse(PracticalExamCourse practicalExamCourse)
        {
            _context.PracticalExamCourses.Remove(practicalExamCourse);
            _context.SaveChanges();
        }
        public void EditPracticalExamCourseMark(PracticalExamCourse practicalExamCourse, int mark)
        {
            practicalExamCourse.SubjectMark = mark;
            _context.Entry(practicalExamCourse).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public List<PracticalExamCourseSubject> GetSubjectsForPracticalExam(int id)
        {
            var practicalExams = _context.PracticalExamCourseSubjects.Where(r => r.PracticalExamCourseId == id);
            return practicalExams.ToList();
        }

        public PracticalExamCourseSubject GetPracticalExamCourseSubject(int practicalExamCourseId, int subjectId)
        {
            var examCourse = _context.PracticalExamCourseSubjects.FirstOrDefault(r => r.PracticalExamCourseId == practicalExamCourseId && r.SubjectId == subjectId);
            return examCourse;
        }

        public void AddPracticalExamCourseSubject(PracticalExamCourseSubject practicalExamCourseSubject)
        {
            _context.PracticalExamCourseSubjects.Add(practicalExamCourseSubject);
            _context.SaveChanges();
        }

        public void RemovePracticalExamCourseSubject(PracticalExamCourseSubject practicalExamCourseSubject)
        {
            _context.PracticalExamCourseSubjects.Remove(practicalExamCourseSubject);
            _context.SaveChanges();
        }

        public bool CheckIfItHasSubjects(int courseId, int examid)
        {
            var data = _context.PracticalExamCourses.Include(r => r.PracticalExamCourseSubjects).FirstOrDefault(r => r.CourseId == courseId && r.PracticalExamId == examid);

            if (data?.PracticalExamCourseSubjects.Count() > 0)
                return true;
            return false;
        }

        public bool CheckIfItHasStudents(int practicalExamCourseId, int subjectId)
        {
            var PracticalExamCourse = _context.PracticalExamCourseSubjects.Include(r=>r.PracticalExamCourse.Course).Include(r => r.PracticalExamCourse).FirstOrDefault(r => r.PracticalExamCourseId == practicalExamCourseId && r.SubjectId == subjectId)?.PracticalExamCourse;

            var data = _context.PracticalEnrollmentExamStudentSubjects.Include(r=>r.PracticalEnrollmentExamStudent.EnrollStudentCourse.Course).Include(r=>r.PracticalEnrollmentExamStudent.PracticalEnrollmentExam)
                .FirstOrDefault(r => r.PracticalEnrollmentExamStudent.PracticalEnrollmentExam.PracticalExamId == PracticalExamCourse.PracticalExamId && r.SubjectId == subjectId && r.PracticalEnrollmentExamStudent.EnrollStudentCourse.Course.CourseId == PracticalExamCourse.CourseId);
            if (data != null)
                return true;
            return false;
        }
    }
}
