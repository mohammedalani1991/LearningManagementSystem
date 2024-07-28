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
    public class CourseResourceService : ICourseResourceService
    {
        private readonly ISettingService _settingService;
      
        public CourseResourceService(ISettingService settingService)
        {
            _settingService = settingService;
            
        }
        public IPagedList<CourseResource> GetCourseResources(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResources = db.CourseResources.Include(r => r.CourseResourceTranslations).Include(d=>d.Lecture).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    CourseResources = CourseResources.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        CourseResources = CourseResources.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        CourseResources = CourseResources.Where(r => r.CourseResourceTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = CourseResources;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CourseResourceTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }
        public CourseResource GetCourseResourceById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResource = db.CourseResources.Include(d => d.Lecture).FirstOrDefault(r=>r.Id == id);
                return CourseResource;
            }
        }
        public CourseResource GetCourseResourceById_WithoutUsing(int id, LearningManagementSystemContext db)
        {
                var CourseResource = db.CourseResources.Include(d => d.Lecture).FirstOrDefault(r => r.Id == id);
                return CourseResource;
        }
        public List<CourseResource> GetCourseResourceByLectureId(int LectureId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResources = db.CourseResources.Where(r => r.LectureId == LectureId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var CourseResource in CourseResources)
                    {
                        var trans = db.CourseResourceTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.CourseResourceId == CourseResource.Id);
                        if (trans != null)
                        {
                            CourseResource.Name = trans.Name;
                            CourseResource.Description = trans.Description;
                        }
                    }
                }
                return CourseResources;
            }
        }

        public List<CourseResource> GetCourseResourceByLectureId(int LectureId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResources = db.CourseResources.Include(r=>r.CourseResourceTranslations).Where(r => r.LectureId == LectureId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return CourseResources;
            }
        }

        public List<CourseResource> GetCourseResourceByLectureId_WithoutUsing(int LectureId, LearningManagementSystemContext db)
        {
                var CourseResources = db.CourseResources.Include(r => r.CourseResourceTranslations).Where(r => r.LectureId == LectureId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return CourseResources;
        }
        public CourseResourceViewModel GetCourseResourceById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CourseResourceTranslations.Include(r => r.CourseResource).ThenInclude(d=>d.Lecture).FirstOrDefault(r => r.LanguageId == languageId && r.CourseResourceId == id);
                    if (aboutTran != null)
                    {
                        return new CourseResourceViewModel(aboutTran);
                    }
                }
                var CourseResource = db.CourseResources.Include(r => r.Lecture).FirstOrDefault(d => d.Id == id && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return new CourseResourceViewModel(CourseResource);
            }
        }

        public void AddCourseResource(CourseResourceViewModel CourseResourceViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResource = new CourseResource()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = CourseResourceViewModel.Name,
                    Description = CourseResourceViewModel.Description,
                    Type = CourseResourceViewModel.Type,
                    Link = CourseResourceViewModel.Link,
                    LectureId = CourseResourceViewModel.LectureId,
                    CreatedBy = CourseResourceViewModel.CreatedBy,
                    CourseId = CourseResourceViewModel.CourseId,
                };
                db.CourseResources.Add(CourseResource);
                db.SaveChanges();

                CourseResource.Id = CourseResource.Id;

                if (CourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CourseResourceTran = new CourseResourceTranslation()
                    {
                        Name = CourseResourceViewModel.Name,
                        Description =CourseResourceViewModel.Description,
                        LanguageId = CourseResourceViewModel.LanguageId,
                        CourseResourceId = CourseResource.Id
                    };
                    db.CourseResourceTranslations.Add(CourseResourceTran);
                    db.SaveChanges();
                }
            }
        }
        public void AddCourseResource_WithoutUsing(CourseResourceViewModel CourseResourceViewModel, LearningManagementSystemContext db)
        {
           
                var CourseResource = new CourseResource()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = CourseResourceViewModel.Name,
                    Description = CourseResourceViewModel.Description,
                    Type = CourseResourceViewModel.Type,
                    Link = CourseResourceViewModel.Link,
                    LectureId = CourseResourceViewModel.LectureId,
                    CreatedBy = CourseResourceViewModel.CreatedBy,
                    CourseId = CourseResourceViewModel.CourseId,
                };
                db.CourseResources.Add(CourseResource);
                db.SaveChanges();

                CourseResource.Id = CourseResource.Id;

                if (CourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CourseResourceTran = new CourseResourceTranslation()
                    {
                        Name = CourseResourceViewModel.Name,
                        Description = CourseResourceViewModel.Description,
                        LanguageId = CourseResourceViewModel.LanguageId,
                        CourseResourceId = CourseResource.Id
                    };
                    db.CourseResourceTranslations.Add(CourseResourceTran);
                    db.SaveChanges();
                }
         
        }

        public void EditCourseResource(CourseResourceViewModel CourseResourceViewModel, CourseResource CourseResource)
        {
            using (var db = new LearningManagementSystemContext())
            {
                CourseResource.Type = CourseResourceViewModel.Type;
                CourseResource.Link = CourseResourceViewModel.Link;
                CourseResource.LectureId = CourseResourceViewModel.LectureId;
                CourseResource.CourseId = CourseResourceViewModel.CourseId;
                if (CourseResourceViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    CourseResource.Name = CourseResourceViewModel.Name;
                    CourseResource.Description = CourseResourceViewModel.Description;
                }

                db.Entry(CourseResource).State = EntityState.Modified;
                db.SaveChanges();
                if (CourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CourseResourceTranslation = db.CourseResourceTranslations.FirstOrDefault(r =>
                        r.LanguageId == CourseResourceViewModel.LanguageId &&
                        r.CourseResourceId == CourseResourceViewModel.Id);
                    if (CourseResourceTranslation != null)
                    {
                        CourseResourceTranslation.Name = CourseResourceViewModel.Name;
                        CourseResourceTranslation.Description = CourseResourceViewModel.Description;
                        db.Entry(CourseResourceTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var CourseResourceTran = new CourseResourceTranslation()
                        {
                            Name = CourseResourceViewModel.Name,
                            Description = CourseResourceViewModel.Description,
                            LanguageId = CourseResourceViewModel.LanguageId,
                            CourseResourceId = CourseResourceViewModel.Id
                        };
                        db.CourseResourceTranslations.Add(CourseResourceTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void EditCourseResource_WithoutUsing(CourseResourceViewModel CourseResourceViewModel, CourseResource CourseResource, LearningManagementSystemContext db)
        {
           
                CourseResource.Type = CourseResourceViewModel.Type;
                CourseResource.Link = CourseResourceViewModel.Link;
                CourseResource.LectureId = CourseResourceViewModel.LectureId;
                CourseResource.CourseId = CourseResourceViewModel.CourseId;
                if (CourseResourceViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    CourseResource.Name = CourseResourceViewModel.Name;
                    CourseResource.Description = CourseResourceViewModel.Description;
                }

                db.Entry(CourseResource).State = EntityState.Modified;
                db.SaveChanges();
                if (CourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CourseResourceTranslation = db.CourseResourceTranslations.FirstOrDefault(r =>
                        r.LanguageId == CourseResourceViewModel.LanguageId &&
                        r.CourseResourceId == CourseResourceViewModel.Id);
                    if (CourseResourceTranslation != null)
                    {
                        CourseResourceTranslation.Name = CourseResourceViewModel.Name;
                        CourseResourceTranslation.Description = CourseResourceViewModel.Description;
                        db.Entry(CourseResourceTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var CourseResourceTran = new CourseResourceTranslation()
                        {
                            Name = CourseResourceViewModel.Name,
                            Description = CourseResourceViewModel.Description,
                            LanguageId = CourseResourceViewModel.LanguageId,
                            CourseResourceId = CourseResourceViewModel.Id
                        };
                        db.CourseResourceTranslations.Add(CourseResourceTran);
                    }

                    db.SaveChanges();
                }

           
        }

        public void DeleteCourseResource(CourseResource CourseResource)
        {
            using (var db =new LearningManagementSystemContext())
            {
                CourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                CourseResource.DeletedOn = DateTime.Now;
                db.Entry(CourseResource).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteCourseResource_WithoutUsing(CourseResource CourseResource, LearningManagementSystemContext db)
        {
           
                CourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                CourseResource.DeletedOn = DateTime.Now;
                db.Entry(CourseResource).State = EntityState.Modified;
                db.SaveChanges();
        }
        public void DeleteCourseResourceByLectureId(int LectureId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResources = db.CourseResources.Where(e => e.LectureId == LectureId);
                foreach (var CourseResource in CourseResources)
                {
                    CourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CourseResource.DeletedOn = DateTime.Now;
                    CourseResource.LectureId = LectureId;
                    db.Entry(CourseResource).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public void DeleteCourseResourceByLectureId_WithoutUsing(int LectureId, LearningManagementSystemContext db)
        {
           
                var CourseResources = db.CourseResources.Where(e => e.LectureId == LectureId);
                foreach (var CourseResource in CourseResources)
                {
                    CourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CourseResource.DeletedOn = DateTime.Now;
                    CourseResource.LectureId = LectureId;
                    db.Entry(CourseResource).State = EntityState.Modified;
                }
                db.SaveChanges();
            
        }

        public void DeleteCourseResourceBySectionId(int SectionId, bool WithoutUsing = false, LearningManagementSystemContext context = null)
        {

            if (!WithoutUsing)
            {
                using (var db = new LearningManagementSystemContext())
                {
                    Actions(db);
                }
            }
            else
            {
                Actions(context);
            }

            void Actions(LearningManagementSystemContext db)
            {
                var LectureIDs = db.Lectures.Where(d => d.SectionId == SectionId && d.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.Id).ToArray();
                var CourseResources = db.CourseResources.Where(r => LectureIDs.Contains(r.LectureId) && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                foreach (var CourseResource in CourseResources)
                {
                    CourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CourseResource.DeletedOn = DateTime.Now;
                    db.Entry(CourseResource).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }


    }
}
