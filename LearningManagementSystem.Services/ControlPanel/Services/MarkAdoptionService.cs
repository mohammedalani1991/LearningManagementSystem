using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.ControlPanel.IServices;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningManagementSystem.Services.ControlPanel.Services
{
    public class MarkAdoptionService : IMarkAdoptionService
    {
        private readonly LearningManagementSystemContext _context;

        public MarkAdoptionService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public List<MarkAdoptionForExam> GetMarkAdoptionForExam(int semesterId, int courseId)
        {
            var enrollTecherCourses = _context.EnrollTeacherCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active
                && r.CourseId == courseId && r.SemesterId == semesterId).Include(r => r.EnrollCourseExams.Where(r => r.ExamTemplateId != null && r.Status == (int)GeneralEnums.StatusEnum.Active)).ToList();

            var markAdoptionForExams = new List<MarkAdoptionForExam>();

            foreach (var item in enrollTecherCourses)
                foreach (var item1 in item.EnrollCourseExams)
                    markAdoptionForExams.Add(new MarkAdoptionForExam()
                    {
                        EnrollTeacherCourseId = item.Id,
                        ExamTemplateId = item1.ExamTemplateId,
                        Value = _context.MarkAdoptionForExams.FirstOrDefault(r => r.ExamTemplateId == item1.ExamTemplateId && r.EnrollTeacherCourseId == item.Id)?.Value ?? false,
                    });

            return markAdoptionForExams.GroupBy(r => r.ExamTemplateId).Select(r => r.FirstOrDefault()).ToList();
        }

        public void AdoptMarkAdoptionForExam(int semesterId, int courseId, int examTemplateId, bool adopted)
        {
            var enrollTecherCourses = _context.EnrollTeacherCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active
                            && r.CourseId == courseId && r.SemesterId == semesterId).Include(r => r.EnrollCourseExams.Where(r => r.ExamTemplateId == examTemplateId && r.Status == (int)GeneralEnums.StatusEnum.Active)).ToList();

            var markAdoptionForExams = new List<MarkAdoptionForExam>();

            foreach (var item in enrollTecherCourses)
                foreach (var item1 in item.EnrollCourseExams)
                {
                    markAdoptionForExams.Add(new MarkAdoptionForExam()
                    {
                        EnrollTeacherCourseId = item.Id,
                        ExamTemplateId = item1.ExamTemplateId,
                        Value = adopted,
                    });
                    item1.MarkAdoption = adopted;
                    _context.Entry(item1).State = EntityState.Modified;
                }

            _context.MarkAdoptionForExams.AddRange(markAdoptionForExams);
            _context.SaveChanges();
        }

        public List<MarkAdoptionForPracticalExam> GetMarkAdoptionForPracticalExam(int semesterId, int courseId)
        {
            var enrollTecherCourses = _context.EnrollTeacherCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active
                           && r.CourseId == courseId && r.SemesterId == semesterId).Include(r => r.PracticalEnrollmentExams.Where(r => r.PracticalExamId != null && r.Status == (int)GeneralEnums.StatusEnum.Active)).ToList();

            var markAdoptionForPracticalExam = new List<MarkAdoptionForPracticalExam>();

            foreach (var item in enrollTecherCourses)
                foreach (var item1 in item.PracticalEnrollmentExams)
                    markAdoptionForPracticalExam.Add(new MarkAdoptionForPracticalExam()
                    {
                        EnrollTeacherCourseId = item.Id,
                        PracticalExamId = item1.PracticalExamId,
                        Value = _context.MarkAdoptionForPracticalExams.FirstOrDefault(r => r.PracticalExamId == item1.PracticalExamId && r.EnrollTeacherCourseId == item.Id)?.Value ?? false,
                    });

            return markAdoptionForPracticalExam.GroupBy(r => r.PracticalExamId).Select(r => r.FirstOrDefault()).ToList();
        }

        public void AdoptPracticalExam(int semesterId, int courseId, int practicalExamId, bool adopted)
        {
            var enrollTecherCourses = _context.EnrollTeacherCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active
                           && r.CourseId == courseId && r.SemesterId == semesterId).Include(r => r.PracticalEnrollmentExams.Where(r => r.PracticalExamId == practicalExamId && r.Status == (int)GeneralEnums.StatusEnum.Active)).ToList();

            var markAdoptionForPracticalExam = new List<MarkAdoptionForPracticalExam>();

            foreach (var item in enrollTecherCourses)
                foreach (var item1 in item.PracticalEnrollmentExams)
                {
                    markAdoptionForPracticalExam.Add(new MarkAdoptionForPracticalExam()
                    {
                        EnrollTeacherCourseId = item.Id,
                        PracticalExamId = item1.PracticalExamId,
                        Value = adopted,
                    });
                    item1.MarkAdoption = adopted;
                    _context.Entry(item1).State = EntityState.Modified;
                }

            _context.MarkAdoptionForPracticalExams.AddRange(markAdoptionForPracticalExam);
            _context.SaveChanges();
        }
    }
}
