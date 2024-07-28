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
    public class AssignmentService : IAssignmentService
    {
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        public AssignmentService(ISettingService settingService, ICourseService courseService)
        {
            _settingService = settingService;
            _courseService = courseService;
        }
        public IPagedList<Assignment> GetAssignments(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? CourseId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var assignments = db.Assignments.Include(r => r.AssignmentTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

        
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        assignments = assignments.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        assignments = assignments.Where(r => r.AssignmentTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                if (CourseId > 0)
                {
                    assignments = assignments.Where(r => r.CourseId == CourseId);
                }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = assignments;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.AssignmentTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }
                    }
                }
                foreach (var item in output)
                {

                    var trans = _courseService.GetCourseById(item.CourseId, languageId);
                    item.Course = new Course();
                    if (trans != null && trans.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        item.Course.CourseName = trans.CourseName;
                    }
                    else
                    {
                        item.Course.CourseName = "--";
                        item.Course.Id =0;
                    }
                }
                return output;
            }
        }
        public Assignment GetAssignmentById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var assignment = db.Assignments.Include(d => d.Course).FirstOrDefault(r=> r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Id == id);
                return assignment;
            }
        }
        public AssignmentViewModel GetAssignmentById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.AssignmentTranslations.Include(r => r.Assignment).ThenInclude(d=>d.Course).FirstOrDefault(r => r.Assignment.Course.Status != (int)GeneralEnums.StatusEnum.Deleted && r.LanguageId == languageId && r.AssignmentId == id);
                    if (aboutTran != null)
                    {
                        return new AssignmentViewModel(aboutTran);
                    }
                }
                var assignment = db.Assignments.Include(r => r.Course).FirstOrDefault(d => d.Status != (int)GeneralEnums.StatusEnum.Deleted && d.Id == id);
                if (assignment != null)
                    return new AssignmentViewModel(assignment);
                else
                    return null;
            }
        }

        public List<AssignmentViewModel> GetAssignmentByCourseId(int Courseid, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.AssignmentTranslations.Include(r => r.Assignment).ThenInclude(r => r.Course).Where(r => r.LanguageId == languageId && r.Assignment.CourseId == Courseid && r.Assignment.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new AssignmentViewModel(r));
                    if (aboutTran != null && aboutTran.Count() != 0)
                    {
                        return aboutTran.ToList();
                    }
                }
                var assignment = db.Assignments.Include(r => r.Course).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new AssignmentViewModel(r));
                return assignment.ToList();
            }
        }
        public List<Assignment> GetAssignmentByCourseId(int Courseid)
        {
            using (var db = new LearningManagementSystemContext())
            {
             
                var assignment = db.Assignments.Include(r => r.AssignmentTranslations).Include(r => r.Course).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return assignment.ToList();
            }
        }
        public List<Assignment> GetAssignmentByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db)
        {

                var assignment = db.Assignments.Include(r => r.AssignmentTranslations).Include(r => r.Course).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return assignment.ToList();
        }
        public void AddAssignment(AssignmentViewModel assignmentViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var assignment = new Assignment()
                {
                    CreatedOn = DateTime.Now,
                    Status = assignmentViewModel.Status,
                    Name = assignmentViewModel.Name,
                    Description = assignmentViewModel.Description,
                    ExpiryDate = assignmentViewModel.ExpiryDate,
                    SubmissionDate = assignmentViewModel.SubmissionDate,
                    CourseId = assignmentViewModel.CourseId,
                    CreatedBy = assignmentViewModel.CreatedBy,
                };
                db.Assignments.Add(assignment);
                db.SaveChanges();

                assignment.Id = assignment.Id;

                if (assignmentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var assignmentTran = new AssignmentTranslation()
                    {
                        Name = assignmentViewModel.Name,
                        Description =assignmentViewModel.Description,
                        LanguageId = assignmentViewModel.LanguageId,
                        AssignmentId = assignment.Id
                    };
                    db.AssignmentTranslations.Add(assignmentTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditAssignment(AssignmentViewModel assignmentViewModel, Assignment assignment)
        {
            using (var db = new LearningManagementSystemContext())
            {
                assignment.ExpiryDate = assignmentViewModel.ExpiryDate;
                assignment.SubmissionDate = assignmentViewModel.SubmissionDate;
                assignment.CourseId = assignmentViewModel.CourseId;
                assignment.Status = assignmentViewModel.Status;

                if (assignmentViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    assignment.Name = assignmentViewModel.Name;
                    assignment.Description = assignmentViewModel.Description;
                }

                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                if (assignmentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var assignmentTranslation = db.AssignmentTranslations.FirstOrDefault(r =>
                        r.LanguageId == assignmentViewModel.LanguageId &&
                        r.AssignmentId == assignmentViewModel.Id);
                    if (assignmentTranslation != null)
                    {
                        assignmentTranslation.Name = assignmentViewModel.Name;
                        assignmentTranslation.Description = assignmentViewModel.Description;
                        db.Entry(assignmentTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var assignmentTran = new AssignmentTranslation()
                        {
                            Name = assignmentViewModel.Name,
                            Description = assignmentViewModel.Description,
                            LanguageId = assignmentViewModel.LanguageId,
                            AssignmentId = assignmentViewModel.Id
                        };
                        db.AssignmentTranslations.Add(assignmentTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteAssignment(Assignment assignment)
        {
            using (var db = new LearningManagementSystemContext())
            {
                assignment.Status = (int)GeneralEnums.StatusEnum.Deleted;
                assignment.DeletedOn = DateTime.Now;
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}
