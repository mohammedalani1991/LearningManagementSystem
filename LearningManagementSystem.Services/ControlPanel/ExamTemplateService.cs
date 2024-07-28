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
    public class ExamTemplateService : IExamTemplateService
    {
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        private readonly ICourseCategoryService _courseCategoryService;
        public ExamTemplateService(ISettingService settingService, ICourseService courseService, ICourseCategoryService courseCategoryService)
        {
            _settingService = settingService;
            _courseService = courseService;
            _courseCategoryService = courseCategoryService;
        }
        public IPagedList<ExamTemplate> GetExamTemplates(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25,int? CourseId=0,int? CategoryId=0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamTemplates = db.ExamTemplates.Include(r => r.ExamTemplateTranslations).Include(d=>d.Course).Include(d => d.Category).Where(r =>
                        r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    ExamTemplates = ExamTemplates.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        ExamTemplates = ExamTemplates.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        ExamTemplates = ExamTemplates.Where(r => r.ExamTemplateTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                if (CourseId > 0)
                {
                    ExamTemplates = ExamTemplates.Where(r => r.CourseId == CourseId);
                }
                if (CategoryId > 0)
                {
                    ExamTemplates = ExamTemplates.Where(r => r.CategoryId == CategoryId);
                }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = ExamTemplates;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);


                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var transExamTemplates = item.ExamTemplateTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transExamTemplates != null)
                        {
                            item.Name = transExamTemplates.Name;
                            item.Description = transExamTemplates.Description;


                        }
                    }
                }

                foreach (var item in output)
                {
                    if (item.CourseId != null)
                    {
                        var transCourse = _courseService.GetCourseById(item.CourseId.Value, languageId);

                        if (transCourse != null && transCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            item.Course.CourseName = transCourse.CourseName;
                        }
                        else
                        {
                            item.Course = new Course();
                            item.Course.CourseName = "--";
                            item.Course.Id = 0;
                        }
                    }
                    else
                    {
                        item.Course = new Course();
                        item.Course.CourseName = "--";
                        item.Course.Id = 0;
                    }

                    if (item.CategoryId != null)
                    {
                        var transCourseCategory = _courseCategoryService.GetCourseCategoryById(item.CategoryId.Value, languageId);
                        if (transCourseCategory != null && transCourseCategory.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            item.Category.Name = transCourseCategory.Name;
                        }
                        else
                        {
                            item.Category = new CourseCategory();
                            item.Category.Name = "--";
                            item.Category.Id = 0;
                        }
                    }
                    else
                    {
                        item.Category = new CourseCategory();
                        item.Category.Name = "--";
                        item.Category.Id = 0;
                    }
                }

                return output;
            }
        }



        public ExamTemplate GetExamTemplateById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamTemplate = db.ExamTemplates.Include(e=>e.Course).Include(r => r.Category).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.Id == id);
                return ExamTemplate;
            }
        }
        public ExamTemplateViewModel GetExamTemplateById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.ExamTemplateTranslations.Include(r => r.Exam).ThenInclude(e=>e.Course).ThenInclude(d => d.Category).FirstOrDefault(r => r.Exam.Status != (int)GeneralEnums.StatusEnum.Deleted && r.LanguageId == languageId && r.ExamId == id);
                    if (aboutTran != null)
                    {
                        return new ExamTemplateViewModel(aboutTran);
                    }
                }
                var ExamTemplate = db.ExamTemplates.Include(e=>e.Course).Include(r => r.Category).FirstOrDefault(x => x.Status != (int)GeneralEnums.StatusEnum.Deleted && x.Id == id);
                if (ExamTemplate != null)
                    return new ExamTemplateViewModel(ExamTemplate);
                else
                    return null;
            }
        }

        public List<ExamTemplate> GetdExamTemplatetByCourseId(int Courseid)
        {
            using (var db = new LearningManagementSystemContext())
            {
      
                var examTemplate = db.ExamTemplates.Include(r=>r.ExamTemplateTranslations).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return examTemplate.ToList();
            }
        }
        public List<ExamTemplate> GetdExamTemplatetByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db)
        {

                var examTemplate = db.ExamTemplates.Include(r => r.ExamTemplateTranslations).Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return examTemplate.ToList();
           
        }
        public List<ExamTemplateViewModel> GetdExamTemplatetByCourseId(int Courseid, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.ExamTemplateTranslations.Include(r => r.Exam).Where(r => r.LanguageId == languageId && r.Exam.CourseId == Courseid && r.Exam.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new ExamTemplateViewModel(r));
                    if (aboutTran != null && aboutTran.Count() != 0)
                    {
                        return aboutTran.ToList();
                    }
                }
                var examTemplate = db.ExamTemplates.Where(d => d.CourseId == Courseid && d.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new ExamTemplateViewModel(r));
                return examTemplate.ToList();
            }
        }

        public List<ExamTemplateViewModel> GetAllExamTemplatet(int languageId,int? CourseId=0,int? CategoryId=0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.ExamTemplateTranslations.Include(r => r.Exam).ThenInclude(d => d.Course).ThenInclude(d => d.Category).Where(r => (CourseId > 0 && r.Exam.Course.Id == CourseId) || (CategoryId > 0 && r.Exam.Category.Id == CategoryId) &&  r.LanguageId == languageId &&  r.Exam.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => new ExamTemplateViewModel(r));

                    if (aboutTran != null && aboutTran.Count() != 0)
                    {
                        return aboutTran.ToList();
                    }
                }
                var examTemplate = db.ExamTemplates.Include(d => d.Course).Include(d=>d.Category).Where(d => (CourseId > 0 && d.CourseId == CourseId) || (CategoryId > 0 && d.CategoryId == CategoryId) && d.Status != (int)GeneralEnums.StatusEnum.Deleted ).Select(r => new ExamTemplateViewModel(r));
         
                return examTemplate.ToList();
            }
        }
        public void AddExamTemplate(ExamTemplateViewModel ExamTemplateViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var ExamTemplate = new ExamTemplate()
                {
                    CreatedOn = DateTime.Now,
                    CreatedBy = ExamTemplateViewModel.CreatedBy,
                    Status = ExamTemplateViewModel.Status,
                    Name = ExamTemplateViewModel.Name,
                    Description = ExamTemplateViewModel.Description,
                    CategoryId = (ExamTemplateViewModel.CategoryId == 0) ? null : ExamTemplateViewModel.CategoryId,
                    CourseId = (ExamTemplateViewModel.CourseId == 0) ? null : ExamTemplateViewModel.CourseId,
                    Duration = ExamTemplateViewModel.Duration,
                    Shuffle = ExamTemplateViewModel.Shuffle,

                };
                db.ExamTemplates.Add(ExamTemplate);
                db.SaveChanges();

                ExamTemplate.Id = ExamTemplate.Id;

                if (ExamTemplateViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var ExamTemplateTran = new ExamTemplateTranslation()
                    {
                        Name = ExamTemplateViewModel.Name,
                        Description = ExamTemplateViewModel.Description,
                        LanguageId = ExamTemplateViewModel.LanguageId,
                        ExamId = ExamTemplate.Id
                    };
                    db.ExamTemplateTranslations.Add(ExamTemplateTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditExamTemplate(ExamTemplateViewModel ExamTemplateViewModel, ExamTemplate ExamTemplate)
        {
            using (var db = new LearningManagementSystemContext())
            {

                ExamTemplate.Duration = ExamTemplateViewModel.Duration;
                ExamTemplate.CategoryId = (ExamTemplateViewModel.CategoryId == 0) ? null : ExamTemplateViewModel.CategoryId;
                ExamTemplate.CourseId = (ExamTemplateViewModel.CourseId == 0) ? null : ExamTemplateViewModel.CourseId;
                ExamTemplate.Status = ExamTemplateViewModel.Status;
                ExamTemplate.Shuffle = ExamTemplateViewModel.Shuffle;

                if (ExamTemplateViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    ExamTemplate.Name = ExamTemplateViewModel.Name;
                    ExamTemplate.Description = ExamTemplateViewModel.Description;
                }

                db.Entry(ExamTemplate).State = EntityState.Modified;
                db.SaveChanges();
                if (ExamTemplateViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var ExamTemplateTranslation = db.ExamTemplateTranslations.FirstOrDefault(r =>
                        r.LanguageId == ExamTemplateViewModel.LanguageId &&
                        r.ExamId == ExamTemplateViewModel.Id);
                    if (ExamTemplateTranslation != null)
                    {
                        ExamTemplateTranslation.Name = ExamTemplateViewModel.Name;
                        ExamTemplateTranslation.Description = ExamTemplateViewModel.Description;
                        db.Entry(ExamTemplateTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var ExamTemplateTran = new ExamTemplateTranslation()
                        {
                            Name = ExamTemplateViewModel.Name,
                            Description = ExamTemplateViewModel.Description,
                            LanguageId = ExamTemplateViewModel.LanguageId,
                            ExamId = ExamTemplateViewModel.Id
                        };
                        db.ExamTemplateTranslations.Add(ExamTemplateTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteExamTemplate(ExamTemplate ExamTemplate)
        {
            using (var db = new LearningManagementSystemContext())
            {
                ExamTemplate.Status = (int)GeneralEnums.StatusEnum.Deleted;
                ExamTemplate.DeletedOn = DateTime.Now;
                db.Entry(ExamTemplate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


    }
}
