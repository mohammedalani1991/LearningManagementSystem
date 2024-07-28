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
    public class PracticalEnrollmentExamService : IPracticalEnrollmentExamService
    {
        private readonly LearningManagementSystemContext _context;

        public PracticalEnrollmentExamService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public int GetMark(int practicalExamId, int courseId)
        {
            var mark = _context.PracticalExamCourses.FirstOrDefault(r => r.PracticalExamId == practicalExamId && r.CourseId == courseId);
            return mark?.SubjectMark ?? 0;
        }

        public PracticalEnrollmentExam GetPracticalEnrollmentExamById(int id)
        {
            var exam = _context.PracticalEnrollmentExams.Include(r => r.PracticalExam).Include(r => r.EnrollTeacherCourse).FirstOrDefault(r => r.Id == id);
            return exam;
        }

        public IPagedList<PracticalEnrollmentExam> GetPracticalEnrollmentExam(int? typeId, int enrollTeacherCourseId, int page, string searchText, int languageId, int pagination)
        {
            var PracticalExams = _context.PracticalEnrollmentExams.Where(r => r.EnrollTeacherCourseId == enrollTeacherCourseId && r.Status == (int)GeneralEnums.StatusEnum.Active)
                .Include(r => r.PracticalExam.PracticalExamTranslations).Include(r => r.PracticalEnrollmentExamStudents.Where(s => s.PracticalEnrollmentExamStudentSubjects.Count() > 0 && s.EnrollStudentCourse.Status == (int)GeneralEnums.StatusEnum.Active)).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    PracticalExams = PracticalExams.Where(r => r.PracticalExam.Name.Contains(searchText));
                else
                    PracticalExams = PracticalExams.Where(r => r.PracticalExam.PracticalExamTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
            }
            if (typeId != null && typeId > 0)
                PracticalExams = PracticalExams.Where(r => r.TypeId == typeId);

            var pageSize = pagination;
            var pageNumber = page;
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

        public IPagedList<PracticalEnrollmentExam> GetSupportPracticalEnrollmentExam(int trainerId, int? typeId, int enrollTeacherCourseId, int page, string searchText, int languageId, int pagination)
        {
            var PracticalExams = _context.PracticalEnrollmentExamTrainers.Where(r=>r.TrainerId == trainerId).Include(r=>r.PracticalEnrollmentExam.PracticalExam.PracticalExamTranslations)
                .Include(r => r.PracticalEnrollmentExam.PracticalEnrollmentExamStudents.Where(s => s.PracticalEnrollmentExamStudentSubjects.Count() > 0 && s.EnrollStudentCourse.Status == (int)GeneralEnums.StatusEnum.Active))
                .Where(r=>r.PracticalEnrollmentExam.EnrollTeacherCourseId == enrollTeacherCourseId)
                .Select(r=>r.PracticalEnrollmentExam);

            if (!string.IsNullOrWhiteSpace(searchText))
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    PracticalExams = PracticalExams.Where(r => r.PracticalExam.Name.Contains(searchText));
                else
                    PracticalExams = PracticalExams.Where(r => r.PracticalExam.PracticalExamTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));

            if (typeId != null && typeId > 0)
                PracticalExams = PracticalExams.Where(r => r.TypeId == typeId);

            var pageSize = pagination;
            var pageNumber = page;
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

        public List<PracticalEnrollmentExam> GetPracticalEnrollmentExam(int enrollTeacherCourseId, int languageId)
        {
            var PracticalExams = _context.PracticalEnrollmentExams.Where(r => r.EnrollTeacherCourseId == enrollTeacherCourseId && r.Status == (int)GeneralEnums.StatusEnum.Active)
                .Include(r => r.PracticalExam.PracticalExamTranslations).AsQueryable();


            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in PracticalExams)
                {
                    var trans = item.PracticalExam.PracticalExamTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.PracticalExam.Name = trans.Name;
                }

            return PracticalExams.ToList();
        }

        public int? GetTypeId(int examId)
        {
            var type = _context.PracticalExams.Find(examId);
            return type != null ? type.TypeId : 0;
        }

        public void AddPracticalEnrollmentExam(PracticalEnrollmentExamViewModel practicalEnrollmentExamViewModel)
        {
            var exam = new PracticalEnrollmentExam()
            {
                CreatedBy = practicalEnrollmentExamViewModel.CreatedBy,
                CreatedOn = practicalEnrollmentExamViewModel.CreatedOn,
                EnrollTeacherCourseId = practicalEnrollmentExamViewModel.EnrollTeacherCourseId,
                PracticalExamId = practicalEnrollmentExamViewModel.PracticalExamId,
                Status = practicalEnrollmentExamViewModel.Status,
                TypeId = practicalEnrollmentExamViewModel.TypeId,
                StartDate = practicalEnrollmentExamViewModel.StartDate,
                EndDate = practicalEnrollmentExamViewModel.EndDate,
            };
            _context.PracticalEnrollmentExams.Add(exam);
            _context.SaveChanges();
        }

        public void AddPracticalEnrollmentExam_WithoutUsing(PracticalEnrollmentExamViewModel practicalEnrollmentExamViewModel, LearningManagementSystemContext db)
        {
            var exam = new PracticalEnrollmentExam()
            {
                CreatedBy = practicalEnrollmentExamViewModel.CreatedBy,
                CreatedOn = practicalEnrollmentExamViewModel.CreatedOn,
                EnrollTeacherCourseId = practicalEnrollmentExamViewModel.EnrollTeacherCourseId,
                PracticalExamId = practicalEnrollmentExamViewModel.PracticalExamId,
                Status = practicalEnrollmentExamViewModel.Status,
                TypeId = practicalEnrollmentExamViewModel.TypeId,
                StartDate = practicalEnrollmentExamViewModel.StartDate,
                EndDate = practicalEnrollmentExamViewModel.EndDate,
            };
            db.PracticalEnrollmentExams.Add(exam);
            db.SaveChanges();
        }

        public void EditPracticalEnrollmentExam(PracticalEnrollmentExamViewModel practicalEnrollmentExamViewModel, PracticalEnrollmentExam practicalEnrollmentExam)
        {
            practicalEnrollmentExam.StartDate = practicalEnrollmentExamViewModel.StartDate;
            practicalEnrollmentExam.EndDate = practicalEnrollmentExamViewModel.EndDate;
            practicalEnrollmentExam.Status = practicalEnrollmentExamViewModel.Status;
            _context.Entry(practicalEnrollmentExam).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<PracticalEnrollmentExamTrainer> GetPracticalEnrollmentExamTrainers(int examId)
        {
            var list = _context.PracticalEnrollmentExamTrainers.Where(r => r.PracticalEnrollmentExamId == examId).ToList();

            return list;
        }

        public PracticalEnrollmentExamTrainer GetPracticalEnrollmentExamTrainers(int practicalEnrollmentExamId, int TrainerId)
        {
            return _context.PracticalEnrollmentExamTrainers.FirstOrDefault(r => r.PracticalEnrollmentExamId == practicalEnrollmentExamId && r.TrainerId == TrainerId);
        }
        public void AddPracticalEnrollmentExamTrainer(PracticalEnrollmentExamTrainer practicalEnrollmentExamTrainer)
        {
            _context.PracticalEnrollmentExamTrainers.Add(practicalEnrollmentExamTrainer);
            _context.SaveChanges();
        }

        public void RemovePracticalEnrollmentExamTrainer(PracticalEnrollmentExamTrainer practicalEnrollmentExamTrainer)
        {
            _context.PracticalEnrollmentExamTrainers.Remove(practicalEnrollmentExamTrainer);
            _context.SaveChanges();
        }
    }
}
