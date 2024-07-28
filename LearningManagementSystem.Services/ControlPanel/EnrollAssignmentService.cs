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
    public class EnrollAssignmentService : IEnrollAssignmentService
    {
        private readonly ISettingService _settingService;

        public EnrollAssignmentService(ISettingService settingService)
        {
            _settingService = settingService;

        }

        public EnrollAssignment AddEnrollAssignmentForAdmin(Assignment Assignment, int EnrollCourseId, LearningManagementSystemContext db)
        {

            var enrollAssignment = new EnrollAssignment()
            {
                CreatedOn = DateTime.Now,
                Status = (int)GeneralEnums.StatusEnum.Active,
                Name = Assignment.Name,
                Description = Assignment.Description,
                EnrollCourseId = EnrollCourseId,
                CreatedBy = Assignment.CreatedBy,
                SubmissionDate = Assignment.SubmissionDate,
                ExpiryDate = Assignment.ExpiryDate
            };
            db.EnrollAssignments.Add(enrollAssignment);
            db.SaveChanges();

            enrollAssignment.Id = enrollAssignment.Id;


            if (Assignment.AssignmentTranslations != null)
            {
                foreach (var AssignmentTranslations in Assignment.AssignmentTranslations)
                {
                    var enrollAssignmentTran = new EnrollAssignmentTranslation()
                    {
                        Name = AssignmentTranslations.Name,
                        Description = AssignmentTranslations.Description,
                        LanguageId = AssignmentTranslations.LanguageId,
                        EnrollAssignmentId = enrollAssignment.Id
                    };
                    db.EnrollAssignmentTranslations.Add(enrollAssignmentTran);
                    db.SaveChanges();
                }
            }

            return enrollAssignment;

        }
        public EnrollAssignment AddEnrollAssignment(EnrollAssignmentViewModel enrollAssignmentViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollAssignment = new EnrollAssignment()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = enrollAssignmentViewModel.Name,
                    Description = enrollAssignmentViewModel.Description,
                    EnrollCourseId = enrollAssignmentViewModel.EnrollCourseId,
                    CreatedBy = enrollAssignmentViewModel.CreatedBy,
                    SubmissionDate = enrollAssignmentViewModel.SubmissionDate,
                    ExpiryDate = enrollAssignmentViewModel.ExpiryDate
                };
                db.EnrollAssignments.Add(enrollAssignment);
                db.SaveChanges();

                enrollAssignment.Id = enrollAssignment.Id;

                if (enrollAssignmentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var enrollAssignmentTran = new EnrollAssignmentTranslation()
                    {
                        Name = enrollAssignmentViewModel.Name,
                        Description = enrollAssignmentViewModel.Description,
                        LanguageId = enrollAssignmentViewModel.LanguageId,
                        EnrollAssignmentId = enrollAssignment.Id
                    };
                    db.EnrollAssignmentTranslations.Add(enrollAssignmentTran);
                    db.SaveChanges();
                }
                return enrollAssignment;
            }
        }
        public List<EnrollStudentAssigment> GetEnrollAssignmentByEnrollTeacherCourseId(int EnrollTeacherCourseId, int studentId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollAssignments = db.EnrollCourseAssigments.Where(r => r.EnrollTeacherCourseId == EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
               
                var enrollStudentCourse = db.EnrollStudentCourses.FirstOrDefault(s => s.StudentId == studentId && s.CourseId == EnrollTeacherCourseId && s.Status == (int)GeneralEnums.StatusEnum.Active);

                if (enrollStudentCourse != null)
                {
                    foreach (var item in enrollAssignments)
                    {
                        var enrollStudentAssigments = db.EnrollStudentAssigments.Where(r => r.EnrollCourseAssigmentId == item.Id && r.EnrollStudentCourseId == enrollStudentCourse.Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                        if (!enrollStudentAssigments.Select(r => r.EnrollCourseAssigmentId).Contains(item.Id))
                        {
                            var enrollStudentAssigment = new EnrollStudentAssigment()
                            {
                                CreatedOn = DateTime.Now,
                                EnrollCourseAssigmentId = item.Id,
                                EnrollStudentCourseId = enrollStudentCourse.Id,
                                CreatedBy = item.CreatedBy,
                                Status = item.Status
                            };
                            db.EnrollStudentAssigments.Add(enrollStudentAssigment);
                        }
                        foreach (var item1 in enrollStudentAssigments)
                            if (item.Id == item1.EnrollCourseAssigmentId)
                            {
                                item1.Status = item.Status;
                                db.Entry(item1).State = EntityState.Modified;
                            }
                        db.SaveChanges();
                    }

                    var studentAssigments = db.EnrollStudentAssigments.Where(r => r.EnrollStudentCourseId == enrollStudentCourse.Id && r.Status == (int)GeneralEnums.StatusEnum.Active)
                        .Include(r => r.EnrollCourseAssigment.EnrollCourseAssigmentTranslations).Include(r => r.EnrollStudentAssigmentAnswers).AsQueryable();

                    if (enrollAssignments.Count() != studentAssigments.Count())
                    {
                        foreach (var item in studentAssigments)
                        {
                            item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                            db.Entry(item).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                        GetEnrollAssignmentByEnrollTeacherCourseId(EnrollTeacherCourseId, studentId, languageId);
                    }

                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        foreach (var item in studentAssigments)
                        {
                            var trans = item.EnrollCourseAssigment.EnrollCourseAssigmentTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                            if (trans != null)
                            {
                                item.EnrollCourseAssigment.Name = trans.Name;
                                item.EnrollCourseAssigment.Description = trans.Description;
                            }
                        }
                    }
                    return studentAssigments.OrderByDescending(r => r.EnrollCourseAssigment.PublishEndDate).ToList();
                }
                else
                    return new List<EnrollStudentAssigment>();
            }

        }

        public void DeleteEnrollAssignment(EnrollAssignment enrollAssignment)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollAssignment.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollAssignment.DeletedOn = DateTime.Now;
                db.Entry(enrollAssignment).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteEnrollAssignmentByEnrollTeacherCourseID(int EnrollTeacherCourseID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollAssignments = db.EnrollAssignments.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
                foreach (var EnrollAssignment in EnrollAssignments)
                {
                    var EnrollAssignmentTranslations = db.EnrollAssignmentTranslations.Where(e => e.EnrollAssignmentId == EnrollAssignment.Id).ToList();
                    db.EnrollAssignmentTranslations.RemoveRange(EnrollAssignmentTranslations);
                }
                db.EnrollAssignments.RemoveRange(EnrollAssignments);
                db.SaveChanges();
            }

        }

        public void DeleteEnrollAssignmentByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db)
        {

            var EnrollAssignments = db.EnrollAssignments.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
            foreach (var EnrollAssignment in EnrollAssignments)
            {
                var EnrollAssignmentTranslations = db.EnrollAssignmentTranslations.Where(e => e.EnrollAssignmentId == EnrollAssignment.Id).ToList();
                db.EnrollAssignmentTranslations.RemoveRange(EnrollAssignmentTranslations);
            }
            db.EnrollAssignments.RemoveRange(EnrollAssignments);
            db.SaveChanges();


        }

    }
}
