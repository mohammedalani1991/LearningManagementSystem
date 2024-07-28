using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollCourseResourceService : IEnrollCourseResourceService
    {
        private readonly ISettingService _settingService;

        public EnrollCourseResourceService(ISettingService settingService)
        {
            _settingService = settingService;
          
        }

        public EnrollCourseResource AddEnrollCourseResourceForAdmin(CourseResource CourseResource, int EnrollCourseId,int EnrollLectureId, LearningManagementSystemContext db)
        {
          
                var enrollCourseResource = new EnrollCourseResource()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = CourseResource.Name,
                    Link = CourseResource.Link,
                    Type = CourseResource.Type,
                    Description = CourseResource.Description,
                    EnrollCourseId = EnrollCourseId,
                    EnrollLectureId = EnrollLectureId,
                    CreatedBy = CourseResource.CreatedBy,
                };
                db.EnrollCourseResources.Add(enrollCourseResource);
                db.SaveChanges();

                enrollCourseResource.Id = enrollCourseResource.Id;


                if (CourseResource.CourseResourceTranslations != null)
                {
                    foreach (var CourseResourceTranslations in CourseResource.CourseResourceTranslations)
                    {
                        var EnrollCourseResourceTranslation = new EnrollCourseResourceTranslation()
                        {
                            Name = CourseResourceTranslations.Name,
                            Description = CourseResourceTranslations.Description,
                            LanguageId = CourseResourceTranslations.LanguageId,
                            EnrollCourseResourceId = enrollCourseResource.Id
                        };
                        db.EnrollCourseResourceTranslations.Add(EnrollCourseResourceTranslation);
                        db.SaveChanges();
                    }
                }
                
                return enrollCourseResource;
          
        }

        public EnrollCourseResource AddEnrollCourseResource(EnrollCourseResourceViewModel enrollCourseResourceViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollCourseResource = new EnrollCourseResource()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = enrollCourseResourceViewModel.Name,
                    Link = enrollCourseResourceViewModel.Link,
                    Type = enrollCourseResourceViewModel.Type,
                    Description = enrollCourseResourceViewModel.Description,
                    EnrollCourseId = enrollCourseResourceViewModel.EnrollCourseId,
                    EnrollLectureId = enrollCourseResourceViewModel.EnrollLectureId,
                    CreatedBy = enrollCourseResourceViewModel.CreatedBy,
                };
                db.EnrollCourseResources.Add(enrollCourseResource);
                db.SaveChanges();

                enrollCourseResource.Id = enrollCourseResource.Id;

                if (enrollCourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var EnrollCourseResourceTranslation = new EnrollCourseResourceTranslation()
                    {
                        Name = enrollCourseResourceViewModel.Name,
                        Description =enrollCourseResourceViewModel.Description,
                        LanguageId = enrollCourseResourceViewModel.LanguageId,
                        EnrollCourseResourceId = enrollCourseResource.Id
                    };
                    db.EnrollCourseResourceTranslations.Add(EnrollCourseResourceTranslation);
                    db.SaveChanges();
                }
                return enrollCourseResource;
            }
        }
        public EnrollCourseResource AddEnrollCourseResource_WithoutUsing(EnrollCourseResourceViewModel enrollCourseResourceViewModel, LearningManagementSystemContext db)
        {
          
                var enrollCourseResource = new EnrollCourseResource()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = enrollCourseResourceViewModel.Name,
                    Link = enrollCourseResourceViewModel.Link,
                    Type = enrollCourseResourceViewModel.Type,
                    Description = enrollCourseResourceViewModel.Description,
                    EnrollCourseId = enrollCourseResourceViewModel.EnrollCourseId,
                    EnrollLectureId = enrollCourseResourceViewModel.EnrollLectureId,
                    CreatedBy = enrollCourseResourceViewModel.CreatedBy,
                };
                db.EnrollCourseResources.Add(enrollCourseResource);
                db.SaveChanges();

                enrollCourseResource.Id = enrollCourseResource.Id;

                if (enrollCourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var EnrollCourseResourceTranslation = new EnrollCourseResourceTranslation()
                    {
                        Name = enrollCourseResourceViewModel.Name,
                        Description = enrollCourseResourceViewModel.Description,
                        LanguageId = enrollCourseResourceViewModel.LanguageId,
                        EnrollCourseResourceId = enrollCourseResource.Id
                    };
                    db.EnrollCourseResourceTranslations.Add(EnrollCourseResourceTranslation);
                    db.SaveChanges();
                }
                return enrollCourseResource;
           
        }
        public void EditEnrollCourseResource(EnrollCourseResourceViewModel EnrollCourseResourceViewModel, EnrollCourseResource EnrollCourseResource)
        {
            using (var db = new LearningManagementSystemContext())
            {
                EnrollCourseResource.Type = EnrollCourseResourceViewModel.Type;
                EnrollCourseResource.Link = EnrollCourseResourceViewModel.Link;
                EnrollCourseResource.EnrollLectureId = EnrollCourseResourceViewModel.EnrollLectureId;
                EnrollCourseResource.EnrollCourseId = EnrollCourseResourceViewModel.EnrollCourseId;
                if (EnrollCourseResourceViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    EnrollCourseResource.Name = EnrollCourseResourceViewModel.Name;
                    EnrollCourseResource.Description = EnrollCourseResourceViewModel.Description;
                }

                db.Entry(EnrollCourseResource).State = EntityState.Modified;
                db.SaveChanges();
                if (EnrollCourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var EnrollCourseResourceTranslations = db.EnrollCourseResourceTranslations.FirstOrDefault(r =>
                        r.LanguageId == EnrollCourseResourceViewModel.LanguageId &&
                        r.EnrollCourseResourceId == EnrollCourseResourceViewModel.Id);
                    if (EnrollCourseResourceTranslations != null)
                    {
                        EnrollCourseResourceTranslations.Name = EnrollCourseResourceViewModel.Name;
                        EnrollCourseResourceTranslations.Description = EnrollCourseResourceViewModel.Description;
                        db.Entry(EnrollCourseResourceTranslations).State = EntityState.Modified;
                    }
                    else
                    {
                        var CourseResourceTran = new EnrollCourseResourceTranslation()
                        {
                            Name = EnrollCourseResourceViewModel.Name,
                            Description = EnrollCourseResourceViewModel.Description,
                            LanguageId = EnrollCourseResourceViewModel.LanguageId,
                            EnrollCourseResourceId = EnrollCourseResourceViewModel.Id
                        };
                        db.EnrollCourseResourceTranslations.Add(CourseResourceTran);
                    }

                    db.SaveChanges();
                }

            }
        }
        public void EditEnrollCourseResource_WithoutUsing(EnrollCourseResourceViewModel EnrollCourseResourceViewModel, EnrollCourseResource EnrollCourseResource, LearningManagementSystemContext db)
        {
            
                EnrollCourseResource.Type = EnrollCourseResourceViewModel.Type;
                EnrollCourseResource.Link = EnrollCourseResourceViewModel.Link;
                EnrollCourseResource.EnrollLectureId = EnrollCourseResourceViewModel.EnrollLectureId;
                EnrollCourseResource.EnrollCourseId = EnrollCourseResourceViewModel.EnrollCourseId;
                if (EnrollCourseResourceViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    EnrollCourseResource.Name = EnrollCourseResourceViewModel.Name;
                    EnrollCourseResource.Description = EnrollCourseResourceViewModel.Description;
                }

                db.Entry(EnrollCourseResource).State = EntityState.Modified;
                db.SaveChanges();
                if (EnrollCourseResourceViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var EnrollCourseResourceTranslations = db.EnrollCourseResourceTranslations.FirstOrDefault(r =>
                        r.LanguageId == EnrollCourseResourceViewModel.LanguageId &&
                        r.EnrollCourseResourceId == EnrollCourseResourceViewModel.Id);
                    if (EnrollCourseResourceTranslations != null)
                    {
                        EnrollCourseResourceTranslations.Name = EnrollCourseResourceViewModel.Name;
                        EnrollCourseResourceTranslations.Description = EnrollCourseResourceViewModel.Description;
                        db.Entry(EnrollCourseResourceTranslations).State = EntityState.Modified;
                    }
                    else
                    {
                        var CourseResourceTran = new EnrollCourseResourceTranslation()
                        {
                            Name = EnrollCourseResourceViewModel.Name,
                            Description = EnrollCourseResourceViewModel.Description,
                            LanguageId = EnrollCourseResourceViewModel.LanguageId,
                            EnrollCourseResourceId = EnrollCourseResourceViewModel.Id
                        };
                        db.EnrollCourseResourceTranslations.Add(CourseResourceTran);
                    }

                    db.SaveChanges();
                }

           
        }
        public EnrollCourseResource GetEnrollCourseResourceById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResource = db.EnrollCourseResources.Include(d => d.EnrollLecture).FirstOrDefault(r => r.Id == id);
                return CourseResource;
            }
        }
        public EnrollCourseResource GetEnrollCourseResourceById_WithoutUsing(int id,LearningManagementSystemContext db)
        {
         
                var CourseResource = db.EnrollCourseResources.Include(d => d.EnrollLecture).FirstOrDefault(r => r.Id == id);
                return CourseResource;
            
        }
        public List<EnrollCourseResource> GetEnrollCourseResourceByLectureId(int EnrollLectureId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResources = db.EnrollCourseResources.Where(r => r.EnrollLectureId == EnrollLectureId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return CourseResources;
            }
        }
        public List<EnrollCourseResource> GetEnrollCourseResourceByLectureId_WithoutUsing(int EnrollLectureId, LearningManagementSystemContext db)
        {
           
                var CourseResources = db.EnrollCourseResources.Where(r => r.EnrollLectureId == EnrollLectureId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return CourseResources;
            
        }
        public List<EnrollCourseResource> GetEnrollCourseResourceByLectureId(int LectureId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResources = db.EnrollCourseResources.Where(r => r.EnrollLectureId == LectureId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var CourseResource in CourseResources)
                    {
                        var trans = db.EnrollCourseResourceTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollCourseResourceId == CourseResource.Id);
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
        
        public List<EnrollCourseResource> GetEnrollCourseResource(int enrollLectureId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollCourseResources = db.EnrollCourseResources.Where(s => s.Status == (int)GeneralEnums.StatusEnum.Active && s.EnrollLectureId == enrollLectureId).ToList();

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var EnrollSection in enrollCourseResources)
                    {
                        var trans = db.EnrollCourseResourceTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollCourseResourceId == EnrollSection.Id);
                        if (trans != null)
                        {
                            EnrollSection.Name = trans.Name;
                            EnrollSection.Description = trans.Description;
                        }
                    }


                }
                return enrollCourseResources;
            }
        }

        public void DeleteEnrollCourseResource(EnrollCourseResource enrollCourseResource)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollCourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollCourseResource.DeletedOn = DateTime.Now;
                db.Entry(enrollCourseResource).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteEnrollCourseResource_WithoutUsing(EnrollCourseResource enrollCourseResource, LearningManagementSystemContext db)
        {
           
                enrollCourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollCourseResource.DeletedOn = DateTime.Now;
                db.Entry(enrollCourseResource).State = EntityState.Modified;
                db.SaveChanges();
            
        }
        public void DeleteEnrollCourseResourceByEnrollTeacherCourseID(int EnrollTeacherCourseID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseResources = db.EnrollCourseResources.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
                foreach (var EnrollCourseResource in EnrollCourseResources)
                {
                    var EnrollCourseResourceTranslations = db.EnrollCourseResourceTranslations.Where(e => e.EnrollCourseResourceId == EnrollCourseResource.Id).ToList();
                    db.EnrollCourseResourceTranslations.RemoveRange(EnrollCourseResourceTranslations);
                }
                db.EnrollCourseResources.RemoveRange(EnrollCourseResources);
                db.SaveChanges();
            }

        }
        public void DeleteEnrollCourseResourceByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db)
        {
           
                var EnrollCourseResources = db.EnrollCourseResources.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
                foreach (var EnrollCourseResource in EnrollCourseResources)
                {
                    var EnrollCourseResourceTranslations = db.EnrollCourseResourceTranslations.Where(e => e.EnrollCourseResourceId == EnrollCourseResource.Id).ToList();
                    db.EnrollCourseResourceTranslations.RemoveRange(EnrollCourseResourceTranslations);
                }
                db.EnrollCourseResources.RemoveRange(EnrollCourseResources);
                db.SaveChanges();
            

        }
        public void DeleteEnrollCourseResourceByLectureId(int EnrollLectureId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CourseResources = db.EnrollCourseResources.Where(e => e.EnrollLectureId == EnrollLectureId);
                foreach (var CourseResource in CourseResources)
                {
                    CourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CourseResource.DeletedOn = DateTime.Now;
                    CourseResource.EnrollLectureId = EnrollLectureId;
                    db.Entry(CourseResource).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public void DeleteEnrollCourseResourceByLectureId_WithoutUsing(int EnrollLectureId, LearningManagementSystemContext db)
        {
         
                var CourseResources = db.EnrollCourseResources.Where(e => e.EnrollLectureId == EnrollLectureId);
                foreach (var CourseResource in CourseResources)
                {
                    CourseResource.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    CourseResource.DeletedOn = DateTime.Now;
                    CourseResource.EnrollLectureId = EnrollLectureId;
                    db.Entry(CourseResource).State = EntityState.Modified;
                }
                db.SaveChanges();
          
        }
        public void DeleteEnrollCourseResourceBySectionId(int SectionId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var LectureIDs = db.Lectures.Where(d => d.SectionId == SectionId && d.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.Id).ToArray();
                var CourseResources = db.EnrollCourseResources.Where(r => LectureIDs.Contains(r.EnrollLectureId) && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                foreach (var CourseResource in CourseResources)
                    DeleteEnrollCourseResource(CourseResource);
            }
        }
        public void DeleteEnrollCourseResourceBySectionId_WithoutUsing(int SectionId, LearningManagementSystemContext db)
        {
          
                var LectureIDs = db.Lectures.Where(d => d.SectionId == SectionId && d.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.Id).ToArray();
                var CourseResources = db.EnrollCourseResources.Where(r => LectureIDs.Contains(r.EnrollLectureId) && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                foreach (var CourseResource in CourseResources)
                    DeleteEnrollCourseResource(CourseResource);
          
        }

    }
}
