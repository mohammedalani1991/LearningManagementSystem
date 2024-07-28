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
using System.Threading.Tasks;
using static iTextSharp.text.pdf.AcroFields;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CoursePackagesService : ICoursePackagesService
    {
        private readonly ISettingService _settingService;
        private readonly ICourseService _courseService;
        private readonly IContactsService _contactsService;
        public CoursePackagesService(ISettingService settingService, ICourseService courseService, IContactsService contactsService)
        {
            _settingService = settingService;
            _courseService = courseService;
            _contactsService = contactsService;
        }
        public async Task<IPagedList<CoursePackageViewModel>> GetCoursePackages(bool admin, string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25, int? TeacherId = 0, int? CourseId = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var CoursePackage = db.CoursePackages.Include(r => r.CoursePakagesTranslations).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                    CoursePackage = CoursePackage.Where(r => r.PackageName.Contains(searchText) || r.CoursePakagesTranslations.Any(t => t.PackageName.Contains(searchText) & t.LanguageId == languageId));

                if (CourseId > 0)
                {
                    var CoursePackagesRelationsCoursePackagesIds = db.CoursePackagesRelations.Where(r => r.CourseId == CourseId).Select(r => r.CoursePackagesId);

                    CoursePackage = CoursePackage.Where(r => CoursePackagesRelationsCoursePackagesIds.Contains(r.Id));
                }

                if (TeacherId > 0)
                {
                    CoursePackage = CoursePackage.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);
                }

                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = await CoursePackage.ToListAsync();


                var output = result.OrderByDescending(r => r.CreatedOn).ToPagedList(pageNumber, pageSize);

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CoursePakagesTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.PackageName = trans.PackageName;
                            item.Notes = trans.Notes;
                        }

                    }
                }
                var ViewModel = output.Select(r => new CoursePackageViewModel(r));
                foreach (var CoursePackageData in ViewModel)
                {
                    var EnrollTeacherCourses = new List<int>();
                    if (TeacherId > 0)
                    {
                        EnrollTeacherCourses = db.EnrollTeacherCourses.Where(r => r.TeacherId == TeacherId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.Id).ToList();
                    }
                    var DataCoursePackagesRelations = db.CoursePackagesRelations.Include(r => r.Course.Course).Include(r => r.CoursePackages).ToList();
                    if (admin)
                        DataCoursePackagesRelations = DataCoursePackagesRelations.Where(r => r.CoursePackagesId == CoursePackageData.Id && (EnrollTeacherCourses.Contains(r.CourseId) || EnrollTeacherCourses.Count == 0)).ToList();
                    else
                        DataCoursePackagesRelations = DataCoursePackagesRelations.Where(r => r.CoursePackagesId == CoursePackageData.Id && (EnrollTeacherCourses.Contains(r.CourseId))).ToList();

                    foreach (var EnrollTeacherCoursesData in DataCoursePackagesRelations)
                    {
                        if (!string.IsNullOrEmpty(EnrollTeacherCoursesData.Course.SectionName))
                            EnrollTeacherCoursesData.Course.CourseName = EnrollTeacherCoursesData.Course.Course.CourseName + " / " + EnrollTeacherCoursesData.Course.SectionName;

                        if (languageId != CultureHelper.GetDefaultLanguageId())
                        {
                            var course = db.EnrollTeacherCourses.Include(r => r.EnrollTeacherCourseTranlations).FirstOrDefault(r => r.Id == EnrollTeacherCoursesData.CourseId);
                            var trans = db.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId && r.CourseId == course.CourseId);
                            var trans1 = course.EnrollTeacherCourseTranlations.FirstOrDefault(s => s.LanguageId == languageId);
                            if (trans != null && trans1 != null)
                            {
                                if (!string.IsNullOrEmpty(trans1.SectionName))
                                    EnrollTeacherCoursesData.Course.CourseName = trans.CourseName + " / " + trans1.SectionName;
                                else if (!string.IsNullOrEmpty(EnrollTeacherCoursesData.Course.SectionName))
                                    EnrollTeacherCoursesData.Course.CourseName = trans.CourseName + " / " + EnrollTeacherCoursesData.Course.SectionName;
                                else
                                    EnrollTeacherCoursesData.Course.CourseName = trans.CourseName;

                            }
                        }
                    }

                    if (DataCoursePackagesRelations.Count > 0)
                    {
                        var transTeacher = db.Trainers.FirstOrDefault(r => r.Id == DataCoursePackagesRelations[0].Course.TeacherId);
                        var transContact = _contactsService.GetContact(transTeacher.ContactId, languageId);

                        if (transContact != null && transContact.Status != (int)GeneralEnums.StatusEnum.Deleted)
                        {
                            CoursePackageData.TrainerName = transContact.FullName;

                        }
                        else
                        {
                            CoursePackageData.TrainerName = "--";
                        }
                    }
                    CoursePackageData.CoursePackagesRelations = DataCoursePackagesRelations;


                }
                return ViewModel;
            }

        }
        public CoursePackage GetCoursePackageById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CoursePackage = db.CoursePackages.Include(d => d.CoursePakagesTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return CoursePackage;
            }
        }
        public CoursePackage GetCoursePackageById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CoursePackage = db.CoursePackages.Include(d => d.CoursePakagesTranslations).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var trans = CoursePackage.CoursePakagesTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        CoursePackage.PackageName = trans.PackageName;
                        CoursePackage.Notes = trans.Notes;
                    }

                }
                return CoursePackage;
            }
        }
        public List<CoursePackagesRelation> GetCoursePackagesRelationByPackageId(int PackageId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CoursePackagesRelations = db.CoursePackagesRelations.Where(r => r.CoursePackagesId == PackageId).Include(r => r.Course.EnrollTeacherCourseTranlations).Include(r => r.Course.Course.CourseTranslations).Include(r => r.CoursePackages).ToList();
                foreach (var EnrollTeacherCoursesData in CoursePackagesRelations)
                {
                    if (!string.IsNullOrEmpty(EnrollTeacherCoursesData.Course.SectionName))
                        EnrollTeacherCoursesData.Course.CourseName = EnrollTeacherCoursesData.Course.Course.CourseName + " / " + EnrollTeacherCoursesData.Course.SectionName;
                }

                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in CoursePackagesRelations)
                    {
                        var trans = item.Course.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        var trans1 = item.Course.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null && trans1 != null)
                        {
                            if (!string.IsNullOrEmpty(trans1.SectionName))
                                item.Course.CourseName = trans.CourseName + " / " + trans1.SectionName;
                            else if (!string.IsNullOrEmpty(item.Course.SectionName))
                                item.Course.CourseName = trans.CourseName + " / " + item.Course.SectionName;
                            else
                                item.Course.CourseName = trans.CourseName;
                        }
                    }
                return CoursePackagesRelations;
            }
        }
        public List<EnrollTeacherCourse> GetCoursePackagesRelationByPackageIdAndTeacherId(int PackageId, int TeacherId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var CoursePackagesRelations = db.CoursePackagesRelations.Include(r => r.Course).Include(r => r.CoursePackages).Where(r => r.CoursePackagesId == PackageId).Select(r => r.CourseId).ToList();

                var EnrollTeacherCourses = db.EnrollTeacherCourses.Include(r => r.EnrollTeacherCourseTranlations).Where(r => r.TeacherId == TeacherId && CoursePackagesRelations.Contains(r.Id) && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

                foreach (var EnrollTeacherCoursesData in EnrollTeacherCourses)
                {

                    if (!string.IsNullOrEmpty(EnrollTeacherCoursesData.SectionName))
                        EnrollTeacherCoursesData.SectionName = EnrollTeacherCoursesData.SectionName;

                    if (!string.IsNullOrEmpty(EnrollTeacherCoursesData.CourseName))
                        EnrollTeacherCoursesData.CourseName = EnrollTeacherCoursesData.CourseName;

                    if (!string.IsNullOrEmpty(EnrollTeacherCoursesData.SectionName))
                        EnrollTeacherCoursesData.CourseName = EnrollTeacherCoursesData.CourseName + "/ " + EnrollTeacherCoursesData.SectionName;

                }
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var EnrollTeacherCoursesData in EnrollTeacherCourses)
                    {
                        var trans = EnrollTeacherCoursesData.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            if (!string.IsNullOrEmpty(trans.SectionName))
                                EnrollTeacherCoursesData.SectionName = trans.SectionName;
                            else
                                EnrollTeacherCoursesData.SectionName = EnrollTeacherCoursesData.SectionName;

                            if (!string.IsNullOrEmpty(trans.CourseName))
                                EnrollTeacherCoursesData.CourseName = trans.CourseName;
                            else
                                EnrollTeacherCoursesData.CourseName = EnrollTeacherCoursesData.CourseName;

                            if (!string.IsNullOrEmpty(trans.SectionName))
                                EnrollTeacherCoursesData.CourseName = trans.CourseName + "/ " + trans.SectionName;
                            else
                                EnrollTeacherCoursesData.CourseName = EnrollTeacherCoursesData.CourseName + "/ " + EnrollTeacherCoursesData.SectionName;



                        }
                    }

                }
                return EnrollTeacherCourses;
            }
        }


        public CoursePackage AddCoursePackage(CoursePackageViewModel CoursePackageViewModel, LearningManagementSystemContext db)
        {

            var GetCoursePackage = db.CoursePackages.Include(r=>r.CoursePakagesTranslations).FirstOrDefault(r => r.PackageName == CoursePackageViewModel.PackageName && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            var CoursePackage = new CoursePackage();

            if (GetCoursePackage == null)
            {
                CoursePackage = new CoursePackage()
                {
                    CreatedOn = DateTime.Now,
                    Status = CoursePackageViewModel.Status,
                    PackageName = CoursePackageViewModel.PackageName,
                    Notes = CoursePackageViewModel.Notes,
                    CreatedBy = CoursePackageViewModel.CreatedBy,
                };

                db.CoursePackages.Add(CoursePackage);
                db.SaveChanges();

                CoursePackage.Id = CoursePackage.Id;

                if (CoursePackageViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var CoursePackagesTran = new CoursePakagesTranslation()
                    {
                        CoursePackagesId = CoursePackage.Id,
                        PackageName = CoursePackageViewModel.PackageName,
                        Notes = CoursePackageViewModel.Notes,
                        LanguageId = CoursePackageViewModel.LanguageId,

                    };
                    db.CoursePakagesTranslations.Add(CoursePackagesTran);
                    db.SaveChanges();
                }
            }
            else
            {
                if (CoursePackageViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    GetCoursePackage.CoursePakagesTranslations.FirstOrDefault(r=>r.LanguageId == CoursePackageViewModel.LanguageId).PackageName = CoursePackageViewModel.PackageName;
                    db.Entry(GetCoursePackage).State = EntityState.Modified;
                    db.SaveChanges();
                }
                CoursePackage = GetCoursePackage;
            }

            return CoursePackage;

        }
        public CoursePackage EditCoursePackage(CoursePackageViewModel CoursePackageViewModel, CoursePackage CoursePackage, LearningManagementSystemContext db)
        {

            var GetCoursePackage = db.CoursePackages.FirstOrDefault(r => r.PackageName == CoursePackageViewModel.PackageName && r.Id != CoursePackage.Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            if (GetCoursePackage == null)
            {

                CoursePackage.Status = CoursePackageViewModel.Status;
                if (CoursePackageViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    CoursePackage.PackageName = CoursePackageViewModel.PackageName;
                    CoursePackage.Notes = CoursePackageViewModel.Notes;
                }

                db.Entry(CoursePackage).State = EntityState.Modified;
                db.SaveChanges();


                if (CoursePackageViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var coursePakagesTranslation = db.CoursePakagesTranslations.FirstOrDefault(r =>
                r.LanguageId == CoursePackageViewModel.LanguageId &&
                r.CoursePackagesId == CoursePackageViewModel.Id);

                    if (coursePakagesTranslation != null)
                    {

                        coursePakagesTranslation.PackageName = CoursePackageViewModel.PackageName;
                        coursePakagesTranslation.Notes = CoursePackageViewModel.Notes;
                        db.Entry(coursePakagesTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var CoursePackagesTran = new CoursePakagesTranslation()
                        {
                            CoursePackagesId = CoursePackage.Id,
                            PackageName = CoursePackageViewModel.PackageName,
                            Notes = CoursePackageViewModel.Notes,
                            LanguageId = CoursePackageViewModel.LanguageId,

                        };
                        db.CoursePakagesTranslations.Add(CoursePackagesTran);

                    }
                    db.SaveChanges();

                }
            }
            return CoursePackage;

        }

        public CoursePackagesRelation AddCoursePackagesRelation(CoursePackagesRelationsViewModel CoursePackagesRelationsViewModel, LearningManagementSystemContext db)
        {

            var GetCoursePackagesRelations = db.CoursePackagesRelations.FirstOrDefault(r => r.CourseId == CoursePackagesRelationsViewModel.CourseId && r.CoursePackagesId == CoursePackagesRelationsViewModel.CoursePackagesId);
            var CoursePackagesRelation = new CoursePackagesRelation();

            if (GetCoursePackagesRelations == null)
            {
                CoursePackagesRelation = new CoursePackagesRelation()
                {
                    CourseId = CoursePackagesRelationsViewModel.CourseId,
                    CoursePackagesId = CoursePackagesRelationsViewModel.CoursePackagesId,
                };

                db.CoursePackagesRelations.Add(CoursePackagesRelation);
                db.SaveChanges();
            }

            return CoursePackagesRelation;
        }



        public void DeleteCoursePackage(CoursePackage CoursePackage)
        {
            using (var db = new LearningManagementSystemContext())
            {
                CoursePackage.Status = (int)GeneralEnums.StatusEnum.Deleted;
                CoursePackage.DeletedOn = DateTime.Now;
                db.Entry(CoursePackage).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        public void DeleteCoursePackageRelation(int CoursePackageId, LearningManagementSystemContext db)
        {
            var CoursePackagesRelations = db.CoursePackagesRelations.Where(e => e.CoursePackagesId == CoursePackageId).ToList();
            db.CoursePackagesRelations.RemoveRange(CoursePackagesRelations);
            db.SaveChanges();
        }

        public void DeleteCoursePackageTranslations(int CoursePackageId, LearningManagementSystemContext db)
        {
            var CoursePackageTranslations = db.CoursePakagesTranslations.Where(e => e.CoursePackagesId == CoursePackageId).ToList();
            db.CoursePakagesTranslations.RemoveRange(CoursePackageTranslations);
            db.SaveChanges();
        }

        public void DeleteCoursePackages(int CoursePackageId, LearningManagementSystemContext db)
        {

            var CoursePackages = db.CoursePackages.Where(e => e.Id == CoursePackageId).ToList();
            db.CoursePackages.RemoveRange(CoursePackages);
            db.SaveChanges();

        }

    }
}
