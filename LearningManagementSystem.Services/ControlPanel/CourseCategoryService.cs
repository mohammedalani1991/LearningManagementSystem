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
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ISettingService _settingService;
        public CourseCategoryService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<CourseCategory> GetCourseCategorys(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursecategorys = db.CourseCategories.Include(r => r.CourseCategoryTranslations).Include(d => d.Parent).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    coursecategorys = coursecategorys.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        coursecategorys = coursecategorys.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        coursecategorys = coursecategorys.Where(r => r.CourseCategoryTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = coursecategorys;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
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
        public CourseCategory GetCourseCategoryById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursecategory = db.CourseCategories.Include(r => r.Parent).FirstOrDefault(x => x.Id == id && x.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return coursecategory;
            }
        }
        public CourseCategoryViewModel GetCourseCategoryById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CourseCategoryTranslations.Include(r => r.Category).ThenInclude(p => p.Parent).FirstOrDefault(r => r.LanguageId == languageId && r.CategoryId == id && r.Category.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (aboutTran != null)
                    {
                        return new CourseCategoryViewModel(aboutTran);
                    }
                }
                var coursecategory = db.CourseCategories.Include(r => r.Parent).FirstOrDefault(x => x.Id == id && x.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (coursecategory != null)
                    return new CourseCategoryViewModel(coursecategory);
                else
                    return null;

            }
        }

        public List<Course> GetCourseByCategoryId(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var courses = db.Courses.Where(x => x.CategoryId == id && x.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r=>r.CourseTranslations);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in courses)
                    {
                        var tran = item.CourseTranslations.FirstOrDefault(r=>r.LanguageId == languageId);
                        if (tran != null)
                            item.CourseName = tran.CourseName;
                    }

                return courses.ToList();
            }
        }

        public void AddCourseCategory(CourseCategoryViewModel coursecategoryViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursecategory = new CourseCategory()
                {
                    CreatedOn = DateTime.Now,
                    Status = coursecategoryViewModel.Status,
                    Name = coursecategoryViewModel.Name,
                    Description = coursecategoryViewModel.Description,
                    ImageUrl = coursecategoryViewModel.ImageUrl,
                    ParentId = (coursecategoryViewModel.ParentId == 0) ? (int?)null : coursecategoryViewModel.ParentId,
                    ShowInHomePage = coursecategoryViewModel.ShowInHomePage,
                    CreatedBy = coursecategoryViewModel.CreatedBy,
                };
                db.CourseCategories.Add(coursecategory);
                db.SaveChanges();

                coursecategory.Id = coursecategory.Id;

                if (coursecategoryViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var coursecategoryTran = new CourseCategoryTranslation()
                    {
                        Name = coursecategoryViewModel.Name,
                        LanguageId = coursecategoryViewModel.LanguageId,
                        Description = coursecategoryViewModel.Description,
                        CategoryId = coursecategory.Id
                    };
                    db.CourseCategoryTranslations.Add(coursecategoryTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditCourseCategory(CourseCategoryViewModel coursecategoryViewModel, CourseCategory coursecategory)
        {
            using (var db = new LearningManagementSystemContext())
            {

                coursecategory.ImageUrl = coursecategoryViewModel.ImageUrl;
                coursecategory.ParentId = (coursecategoryViewModel.ParentId == 0) ? (int?)null : coursecategoryViewModel.ParentId;
                coursecategory.ShowInHomePage = coursecategoryViewModel.ShowInHomePage;
                coursecategory.Status = coursecategoryViewModel.Status;

                if (coursecategoryViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    coursecategory.Name = coursecategoryViewModel.Name;
                    coursecategory.Description = coursecategoryViewModel.Description;
                }

                db.Entry(coursecategory).State = EntityState.Modified;
                db.SaveChanges();
                if (coursecategoryViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var coursecategoryTranslation = db.CourseCategoryTranslations.FirstOrDefault(r =>
                        r.LanguageId == coursecategoryViewModel.LanguageId &&
                        r.CategoryId == coursecategoryViewModel.Id);
                    if (coursecategoryTranslation != null)
                    {
                        coursecategoryTranslation.Name = coursecategoryViewModel.Name;
                        coursecategoryTranslation.Description = coursecategoryViewModel.Description;
                        db.Entry(coursecategoryTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var coursecategoryTran = new CourseCategoryTranslation()
                        {
                            Name = coursecategoryViewModel.Name,
                            LanguageId = coursecategoryViewModel.LanguageId,
                            Description = coursecategoryViewModel.Description,
                            CategoryId = coursecategoryViewModel.Id
                        };
                        db.CourseCategoryTranslations.Add(coursecategoryTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCourseCategory(CourseCategory coursecategory)
        {
            using (var db = new LearningManagementSystemContext())
            {
                coursecategory.Status = (int)GeneralEnums.StatusEnum.Deleted;
                coursecategory.DeletedOn = DateTime.Now;
                db.Entry(coursecategory).State = EntityState.Modified;
                db.SaveChanges();
                if (coursecategory.Status == (int)GeneralEnums.StatusEnum.Deleted)
                {
                    DeletedParentIDCourseCategory(coursecategory.Id);
                }

            }
        }
        public void DeletedParentIDCourseCategory(int DeletedCourseCategoryID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursecategoryList = db.CourseCategories.Where(x => x.ParentId == DeletedCourseCategoryID).ToList();
                foreach (var coursecategory in coursecategoryList)
                {
                    coursecategory.ParentId = null;
                    db.Entry(coursecategory).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public List<CourseCategory> GetAllCourseCategorys(int languageId = (int)GeneralEnums.LanguageEnum.English)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursecategorys = db.CourseCategories.Include(r => r.CourseCategoryTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                var result = coursecategorys;
                var output = result.OrderByDescending(r => r.Id).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
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


        public List<CourseCategory> GetActiveCourseCategorysForGuest(bool? checkParent, int? parentId, bool? showInHome, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var coursecategorys = db.CourseCategories.Include(r => r.CourseCategoryTranslations).Where(r =>
                      r.Status == (int)GeneralEnums.StatusEnum.Active);


                if (showInHome.HasValue && showInHome.Value)
                {
                    coursecategorys = coursecategorys.Where(r => r.ShowInHomePage == true);
                }

                if (checkParent.HasValue && checkParent.Value)
                {
                    coursecategorys = coursecategorys.Where(r => r.ParentId == parentId);

                }



                var result = coursecategorys;
                var output = result.OrderByDescending(r => r.Id).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CourseCategoryTranslations.FirstOrDefault(r => r.LanguageId == languageId);
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
    }
}
