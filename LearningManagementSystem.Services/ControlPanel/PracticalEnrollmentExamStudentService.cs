using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core;
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
    public class PracticalEnrollmentExamStudentService : IPracticalEnrollmentExamStudentService
    {
        private readonly LearningManagementSystemContext _context;
        private readonly IPracticalEnrollmentExamService _practicalEnrollmentExamService;
        private readonly ISettingService _settingService;

        public PracticalEnrollmentExamStudentService(LearningManagementSystemContext context
            , IPracticalEnrollmentExamService practicalEnrollmentExamService, ISettingService settingService)
        {
            _context = context;
            _practicalEnrollmentExamService = practicalEnrollmentExamService;
            _settingService = settingService;
        }

        public PracticalEnrollmentExam GetPracticalEnrollmentExamById(int id)
        {
            var studentExam = _context.PracticalEnrollmentExams.FirstOrDefault(r => r.Id == id);
            return studentExam;
        }

        public PracticalEnrollmentExamStudent GetOrCreate(int practicalEnrollmentExamId, int enrollStudentCourseId, string createdBy)
        {
            var studentExam = _context.PracticalEnrollmentExamStudents.FirstOrDefault(r => r.PracticalEnrollmentExamId == practicalEnrollmentExamId && r.EnrollStudentCourseId == enrollStudentCourseId);

            if (studentExam == null)
            {
                studentExam = new PracticalEnrollmentExamStudent()
                {
                    EnrollStudentCourseId = enrollStudentCourseId,
                    PracticalEnrollmentExamId = practicalEnrollmentExamId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = createdBy,

                };
                _context.PracticalEnrollmentExamStudents.Add(studentExam);
                _context.SaveChanges();
            }
            return studentExam;
        }
        public List<PracticalEnrollmentExamStudentSubject> GetStudentSubjects(int id, int languageId)
        {
            var subjects = _context.PracticalEnrollmentExamStudentSubjects.Where(r => r.PracticalEnrollmentExamStudentId == id).Include(r => r.Subject.SubjectTranslations).Include(r => r.PracticalEnrollmentExamStudentSubjectRatings);
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in subjects)
                {
                    var trans = item.Subject.SubjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Subject.Title = trans.Title;
                }
            return subjects.ToList();
        }

        public PracticalEnrollmentExamStudentSubject GetStudentSubject(int practicalEnrollmentExamStudentId, int subjectId)
        {
            var subject = _context.PracticalEnrollmentExamStudentSubjects.Include(r => r.PracticalEnrollmentExamStudentSubjectRatings).FirstOrDefault(r => r.PracticalEnrollmentExamStudentId == practicalEnrollmentExamStudentId && r.SubjectId == subjectId);
            return subject;
        }

        public void AddPracticalEnrollmentExamStudentSubject(PracticalEnrollmentExamStudentSubject practicalEnrollmentExamStudentSubject)
        {
            _context.PracticalEnrollmentExamStudentSubjects.Add(practicalEnrollmentExamStudentSubject);
            _context.SaveChanges();
        }

        public void RemovePracticalEnrollmentExamStudentSubject(PracticalEnrollmentExamStudentSubject practicalEnrollmentExamStudentSubject)
        {
            _context.PracticalEnrollmentExamStudentSubjectRatings.RemoveRange(practicalEnrollmentExamStudentSubject.PracticalEnrollmentExamStudentSubjectRatings);
            _context.PracticalEnrollmentExamStudentSubjects.Remove(practicalEnrollmentExamStudentSubject);
            _context.SaveChanges();
        }

        public IPagedList<Subject> GetSubjectsForStudents(int practicalEnrollmentExamId, int courseId, int? typeId, string searchText, int? page, int languageId, int pagination)
        {
            var examId = _context.PracticalEnrollmentExams.FirstOrDefault(r => r.Id == practicalEnrollmentExamId)?.PracticalExamId;
            var Subjects = _context.PracticalExamCourseSubjects.Include(r => r.PracticalExamCourse).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.PracticalExamCourse.PracticalExamId == examId && r.PracticalExamCourse.CourseId == courseId)
                .Include(r => r.Subject.SubjectTranslations).Where(r => r.Subject.Status != (int)GeneralEnums.StatusEnum.Deleted).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    Subjects = Subjects.Where(r => r.Subject.Title.Contains(searchText));
                else
                    Subjects = Subjects.Where(r => r.Subject.SubjectTranslations.Any(t => t.Title.Contains(searchText) & t.LanguageId == languageId));
            }
            if (typeId != null && typeId > 0)
                Subjects = Subjects.Where(r => r.Subject.TypeId == typeId);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = Subjects;
            var output = result.Select(r => r.Subject).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.SubjectTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Title = trans.Title;
                }

            return output;
        }

        public int GetSubjectCount(int id, int courseId)
        {
            var examId = _context.PracticalEnrollmentExams.FirstOrDefault(r => r.Id == id)?.PracticalExamId;

            var count = _context.PracticalExamCourseSubjects.Include(r => r.PracticalExamCourse).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.PracticalExamCourse.PracticalExamId == examId && r.PracticalExamCourse.CourseId == courseId).Count();
            return count;
        }

        public bool DidExamStart(int id)
        {
            var exam = _context.PracticalEnrollmentExamStudents.Include(r => r.PracticalEnrollmentExam).FirstOrDefault(r => r.Id == id)?.PracticalEnrollmentExam;
            if (exam != null && exam.StartDate <= DateTime.Now)
                return true;
            return false;
        }

        public bool DidExamNotEnd(int id)
        {
            var exam = _context.PracticalEnrollmentExamStudents.Include(r => r.PracticalEnrollmentExam).FirstOrDefault(r => r.Id == id)?.PracticalEnrollmentExam;
            if (exam != null && exam.EndDate >= DateTime.Now)
                return true;
            return false;
        }

        public bool IsExamSubmited(int id)
        {
            var student = _context.PracticalEnrollmentExamStudents.FirstOrDefault(r => r.Id == id);
            var exam = _context.PracticalEnrollmentExamStudentSubjects.Include(r => r.PracticalEnrollmentExamStudentSubjectRatings).FirstOrDefault(r => r.PracticalEnrollmentExamStudentId == student.Id);
            if (exam != null && exam.PracticalEnrollmentExamStudentSubjectRatings.Any())
                return true;
            return false;
        }

        public bool IsExamSubmited(int practicalEnrollmentExamId, int courseId)
        {
            var student = _context.PracticalEnrollmentExamStudents.FirstOrDefault(r => r.PracticalEnrollmentExamId == practicalEnrollmentExamId && r.EnrollStudentCourseId == courseId);
            var exam = _context.PracticalEnrollmentExamStudentSubjects.Include(r => r.PracticalEnrollmentExamStudentSubjectRatings).FirstOrDefault(r => r.PracticalEnrollmentExamStudentId == student.Id);
            if (exam != null && exam.PracticalEnrollmentExamStudentSubjectRatings.Any())
                return true;
            return false;
        }

        public void ReCalculateMark(int id)
        {
            var student = _context.PracticalEnrollmentExamStudents.Include(r => r.PracticalEnrollmentExam.EnrollTeacherCourse).FirstOrDefault(r => r.Id == id);
            var mark = _practicalEnrollmentExamService.GetMark(student.PracticalEnrollmentExam.PracticalExamId ?? 0, student.PracticalEnrollmentExam.EnrollTeacherCourse.CourseId);
            if (mark <= 0)
                mark = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.Exam_Subject_Points, "20").Value);

            var exams = _context.PracticalEnrollmentExamStudentSubjects.Include(r => r.PracticalEnrollmentExamStudentSubjectRatings)
                .ThenInclude(r => r.PracticalQuestion).Where(r => r.PracticalEnrollmentExamStudentId == student.Id);
            if (exams != null)
            {
                foreach (var exam in exams)
                {
                    decimal eMark = mark;
                    foreach (var item in exam.PracticalEnrollmentExamStudentSubjectRatings)
                    {
                        //eMark = eMark - (item.NoOfErrors * item.PracticalQuestion.Mark);
                    }
                }
            }
        }

        public int GetSubjectCoumtForStudent(int practicalEnrollmentExamId, int enrollStudentCourseId, string createdBy)
        {
            var studentExam = GetOrCreate(practicalEnrollmentExamId, enrollStudentCourseId, createdBy);
            var count = _context.PracticalEnrollmentExamStudentSubjects.Where(r => r.PracticalEnrollmentExamStudentId == studentExam.Id).Count();
            return count;
        }


        public List<PracticalQuestion> GetQuestions(int examId, int languageId)
        {
            var questions = _context.PracticalExamQuestions.Where(r => r.PracticalExamId == examId).Include(r => r.PracticalQuestion.PracticalQuestionTranslations);
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in questions)
                {
                    var trans = item.PracticalQuestion.PracticalQuestionTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.PracticalQuestion.Name = trans.Name;
                }

            return questions.Select(r => r.PracticalQuestion).ToList();
        }

        public void DeletePracticalEnrollmentExam(PracticalEnrollmentExam practicalEnrollmentExam)
        {
            practicalEnrollmentExam.Status = (int)GeneralEnums.StatusEnum.Deleted;
            practicalEnrollmentExam.DeletedOn = DateTime.Now;
            _context.Entry(practicalEnrollmentExam).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void EditMarkAdoption(PracticalEnrollmentExam practicalEnrollmentExam, bool adopt)
        {
            practicalEnrollmentExam.MarkAdoption = adopt;
            _context.Entry(practicalEnrollmentExam).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddExam(int practicalEnrollmentExamStudentId, List<ExamObjectViewModel> examObjects, decimal subjectMark)
        {
            foreach (var item in examObjects)
            {
                var examSubject = _context.PracticalEnrollmentExamStudentSubjects.FirstOrDefault(r => r.Id == item.PracticalSubjectId);
                examSubject.Mark = item.Mark;

                _context.Entry(examSubject).State = EntityState.Modified;
                var rate = _context.PracticalEnrollmentExamStudentSubjectRatings.FirstOrDefault(r => r.PracticalQuestionId == item.QuastionId && r.PracticalEnrollmentExamStudentSubjectId == examSubject.Id);
                if (rate != null)
                {
                    rate.NoOfErrors = item.NoOfErrors;
                    _context.Entry(rate).State = EntityState.Modified;
                }
                else
                {
                    _context.PracticalEnrollmentExamStudentSubjectRatings.Add(new PracticalEnrollmentExamStudentSubjectRating()
                    {
                        CreatedOn = DateTime.Now,
                        CreatedBy = item.CreatedBy,
                        NoOfErrors = item.NoOfErrors,
                        PracticalQuestionId = item.QuastionId,
                        PracticalEnrollmentExamStudentSubjectId = examSubject.Id,
                        Mark = subjectMark,
                    });
                }
            }
            _context.SaveChanges();
        }

        public void EditMark(int practicalEnrollmentExamStudentId, decimal mark, decimal markAfterConversion)
        {
            var exam = _context.PracticalEnrollmentExamStudents.FirstOrDefault(r => r.Id == practicalEnrollmentExamStudentId);
            exam.Mark = mark;
            exam.MarkAfterConversion = markAfterConversion;
            _context.Entry(exam).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
