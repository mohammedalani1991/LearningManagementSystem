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
    public class LectureService : ILectureService
    {
        private readonly ISettingService _settingService;
        private readonly ICourseResourceService _courseResourceService;
        public LectureService(ISettingService settingService, ICourseResourceService courseResourceService)
        {
            _settingService = settingService;
            _courseResourceService = courseResourceService;
        }
        public IPagedList<Lecture> GetLectures(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lectures = db.Lectures.Include(r => r.LectureTranslations).Include(d=>d.Section).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    lectures = lectures.Where(r => r.LectureName.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        lectures = lectures.Where(r => r.LectureName.Contains(searchText));
                    }
                    else
                    {
                        lectures = lectures.Where(r => r.LectureTranslations.Any(t => t.LectureName.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = lectures;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.LectureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.LectureName = trans.LectureName;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }
        public Lecture GetLectureById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lecture = db.Lectures.Include(d => d.Section).FirstOrDefault(r=>r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return lecture;
            }
        }
        public Lecture GetLectureById_WithoutUsing(int id, LearningManagementSystemContext db)
        {
                var lecture = db.Lectures.Include(d => d.Section).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return lecture;
        }
        public List<Lecture> GetLectureBySectionId(int SectionId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lectures = db.Lectures.Include(e=>e.CourseResources).Where(r => r.SectionId == SectionId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var lecture in lectures)
                    {
                        var trans = db.LectureTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.LectureId == lecture.Id);
                        if (trans != null)
                        {
                            lecture.LectureName = trans.LectureName;
                            lecture.Description = trans.Description;
                            lecture.CourseResources=_courseResourceService.GetCourseResourceByLectureId(lecture.Id, languageId);
                        }
                    }
                }
                return lectures.OrderBy(r=>r.Order).ToList();
            }
        }

        public List<Lecture> GetLectureBySectionId(int SectionId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lectures = db.Lectures.Include(e => e.LectureTranslations).Include(e => e.CourseResources).Where(r => r.SectionId == SectionId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return lectures;
            }
        }

        public List<Lecture> GetLectureBySectionId_WithoutUsing(int SectionId,  LearningManagementSystemContext db)
        {
           
            var lectures = db.Lectures.Include(e => e.LectureTranslations).Include(e => e.CourseResources).Where(r => r.SectionId == SectionId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).OrderBy(r=>r.Order).ToList();
            return lectures;
        }

        public LectureViewModel GetLectureById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.LectureTranslations.Include(r => r.Lecture).ThenInclude(d=>d.Section).FirstOrDefault(r => r.LanguageId == languageId && r.LectureId == id);
                    if (aboutTran != null)
                    {
                        return new LectureViewModel(aboutTran);
                    }
                }
                var lecture = db.Lectures.Include(r => r.Section).FirstOrDefault(d => d.Id == id);
                return new LectureViewModel(lecture);
            }
        }

        public void AddLecture(LectureViewModel lectureViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lecture = new Lecture()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    LectureName = lectureViewModel.LectureName,
                    Description = lectureViewModel.Description,
                    SectionId = lectureViewModel.SectionId,
                    CreatedBy = lectureViewModel.CreatedBy,
                    Order = lectureViewModel.Order = lectureViewModel.Order,
                };
                db.Lectures.Add(lecture);
                db.SaveChanges();

                lecture.Id = lecture.Id;

                if (lectureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var lectureTran = new LectureTranslation()
                    {
                        LectureName = lectureViewModel.LectureName,
                        Description =lectureViewModel.Description,
                        LanguageId = lectureViewModel.LanguageId,
                        LectureId = lecture.Id
                    };
                    db.LectureTranslations.Add(lectureTran);
                    db.SaveChanges();
                }
            }
        }

        public void AddLecture_WithoutUsing(LectureViewModel lectureViewModel, LearningManagementSystemContext db)
        {
           
                var lecture = new Lecture()
                {
                    CreatedOn = DateTime.Now,
                    Status = lectureViewModel.Status,
                    LectureName = lectureViewModel.LectureName,
                    Description = lectureViewModel.Description,
                    SectionId = lectureViewModel.SectionId,
                    CreatedBy = lectureViewModel.CreatedBy,
                    Order = lectureViewModel.Order,
                };
                db.Lectures.Add(lecture);
                db.SaveChanges();

                lecture.Id = lecture.Id;

                if (lectureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var lectureTran = new LectureTranslation()
                    {
                        LectureName = lectureViewModel.LectureName,
                        Description = lectureViewModel.Description,
                        LanguageId = lectureViewModel.LanguageId,
                        LectureId = lecture.Id
                    };
                    db.LectureTranslations.Add(lectureTran);
                    db.SaveChanges();
                }
           
        }

        public void EditLecture(LectureViewModel lectureViewModel, Lecture lecture)
        {
            using (var db = new LearningManagementSystemContext())
            {
                
                lecture.SectionId = lectureViewModel.SectionId;
                lecture.Status = lectureViewModel.Status;
                lecture.Order = lectureViewModel.Order;

                if (lectureViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    lecture.LectureName = lectureViewModel.LectureName;
                    lecture.Description = lectureViewModel.Description;
                }

                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
                if (lectureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var lectureTranslation = db.LectureTranslations.FirstOrDefault(r =>
                        r.LanguageId == lectureViewModel.LanguageId &&
                        r.LectureId == lectureViewModel.Id);
                    if (lectureTranslation != null)
                    {
                        lectureTranslation.LectureName = lectureViewModel.LectureName;
                        lectureTranslation.Description = lectureViewModel.Description;
                        db.Entry(lectureTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var lectureTran = new LectureTranslation()
                        {
                            LectureName = lectureViewModel.LectureName,
                            Description = lectureViewModel.Description,
                            LanguageId = lectureViewModel.LanguageId,
                            LectureId = lectureViewModel.Id
                        };
                        db.LectureTranslations.Add(lectureTran);
                    }

                    db.SaveChanges();
                }

            }
        }
        public void EditLecture_WithoutUsing(LectureViewModel lectureViewModel, Lecture lecture, LearningManagementSystemContext db)
        {
           
                lecture.SectionId = lectureViewModel.SectionId;
                lecture.Status = lectureViewModel.Status;
                lecture.Order = lectureViewModel.Order;

                if (lectureViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    lecture.LectureName = lectureViewModel.LectureName;
                    lecture.Description = lectureViewModel.Description;
                }

                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
                if (lectureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var lectureTranslation = db.LectureTranslations.FirstOrDefault(r =>
                        r.LanguageId == lectureViewModel.LanguageId &&
                        r.LectureId == lectureViewModel.Id);
                    if (lectureTranslation != null)
                    {
                        lectureTranslation.LectureName = lectureViewModel.LectureName;
                        lectureTranslation.Description = lectureViewModel.Description;
                        db.Entry(lectureTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var lectureTran = new LectureTranslation()
                        {
                            LectureName = lectureViewModel.LectureName,
                            Description = lectureViewModel.Description,
                            LanguageId = lectureViewModel.LanguageId,
                            LectureId = lectureViewModel.Id
                        };
                        db.LectureTranslations.Add(lectureTran);
                    }

                    db.SaveChanges();
                }

            
        }
        public void DeleteLecture(Lecture lecture)
        {
            using (var db = new LearningManagementSystemContext())
            {
                lecture.Status = (int)GeneralEnums.StatusEnum.Deleted;
                lecture.DeletedOn = DateTime.Now;
                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteLecture_WithoutUsing(Lecture lecture, LearningManagementSystemContext db)
        {
           
                lecture.Status = (int)GeneralEnums.StatusEnum.Deleted;
                lecture.DeletedOn = DateTime.Now;
                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
        }
        public void DeleteLectureBySectionID(int SectionID, bool WithoutUsing = false, LearningManagementSystemContext context = null)
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
                var lectures = db.Lectures.Where(e => e.SectionId == SectionID);
                foreach (var Lecture in lectures)
                {
                    Lecture.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    Lecture.DeletedOn = DateTime.Now;
                    Lecture.SectionId = SectionID;
                    db.Entry(Lecture).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        

    }
}
