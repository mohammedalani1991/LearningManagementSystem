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
    public class EnrollLectureService : IEnrollLectureService
    {
        private readonly ISettingService _settingService;
        private readonly IEnrollCourseResourceService _enrollCourseResourceService;
        public EnrollLectureService(ISettingService settingService, IEnrollCourseResourceService enrollCourseResourceService)
        {
            _settingService = settingService;
            _enrollCourseResourceService = enrollCourseResourceService;
        }

        public List<EnrollLecture> GetEnrollLectureByEnrollTeacherCoursId(int EnrollTeacherCoursId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var lectures = db.EnrollLectures.Include(e => e.EnrollSection).Where(r => r.EnrollCourseId == EnrollTeacherCoursId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var lecture in lectures)
                    {
                        var trans = db.EnrollLectureTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollLectureId == lecture.Id);
                        var transSectionOfCourse = db.SectionOfCourseTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.SectionId == lecture.EnrollSectionId);
                        if (trans != null)
                        {
                            lecture.LectureName = trans.LectureName;
                            lecture.Description = trans.Description;
                        }
                        if (transSectionOfCourse != null)
                        {
                            lecture.EnrollSection.SectionName = transSectionOfCourse.SectionName;
                        }
                    }


                }
                return lectures;
            }
        }

        public List<EnrollLecture> GetEnrollLectureBySectionId(int SectionId)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var lectures = db.EnrollLectures.Include(e => e.EnrollCourseResources).Where(r => r.EnrollSectionId == SectionId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return lectures;
            }
        }
        public List<EnrollLecture> GetEnrollLectureBySectionId_WithoutUsing(int SectionId, LearningManagementSystemContext db)
        {
           
                var lectures = db.EnrollLectures.Include(e => e.EnrollCourseResources).Where(r => r.EnrollSectionId == SectionId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return lectures;
            
        }

        public List<EnrollLecture> GetEnrollLectureBySectionId(int SectionId, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lectures = db.EnrollLectures.Include(e => e.EnrollCourseResources).Where(r => r.EnrollSectionId == SectionId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var lecture in lectures)
                    {
                        var trans = db.EnrollLectureTranslations.FirstOrDefault(r => r.LanguageId == languageId & r.EnrollLectureId == lecture.Id);
                        if (trans != null)
                        {
                            lecture.LectureName = trans.LectureName;
                            lecture.Description = trans.Description;
                            lecture.EnrollCourseResources = _enrollCourseResourceService.GetEnrollCourseResourceByLectureId(lecture.Id, languageId);
                        }
                    }
                }
                return lectures.OrderBy(r=>r.Order).ToList();
            }
        }
        public EnrollLecture AddEnrollLectureForAdmin(Lecture Lecture,int EnrollCourseId,int EnrollSectionId, LearningManagementSystemContext db)
        {
           
                var enrollLecture = new EnrollLecture()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    LectureName = Lecture.LectureName,
                    Description = Lecture.Description,
                    EnrollSectionId = EnrollSectionId,
                    EnrollCourseId = EnrollCourseId,
                    CreatedBy = Lecture.CreatedBy,
                    Order = Lecture.Order,
                };
                db.EnrollLectures.Add(enrollLecture);
                db.SaveChanges();

                enrollLecture.Id = enrollLecture.Id;


                if (Lecture.LectureTranslations != null)
                {
                    foreach (var LectureTranslations in Lecture.LectureTranslations)
                    {
                        var enrollLectureTran = new EnrollLectureTranslation()
                        {
                            LectureName = LectureTranslations.LectureName,
                            Description = LectureTranslations.Description,
                            LanguageId = LectureTranslations.LanguageId,
                            EnrollLectureId = enrollLecture.Id
                        };
                        db.EnrollLectureTranslations.Add(enrollLectureTran);
                        db.SaveChanges();
                    }
                }
                
                return enrollLecture;
           
        }
        public EnrollLecture AddEnrollLecture(EnrollLectureViewModel enrollLectureViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollLecture = new EnrollLecture()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    LectureName = enrollLectureViewModel.LectureName,
                    Description = enrollLectureViewModel.Description,
                    EnrollSectionId = enrollLectureViewModel.EnrollSectionId,
                    EnrollCourseId = enrollLectureViewModel.EnrollCourseId,
                    CreatedBy = enrollLectureViewModel.CreatedBy,
                    Order = enrollLectureViewModel.Order,
                };
                db.EnrollLectures.Add(enrollLecture);
                db.SaveChanges();

                enrollLecture.Id = enrollLecture.Id;

                if (enrollLectureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var enrollLectureTran = new EnrollLectureTranslation()
                    {
                        LectureName = enrollLectureViewModel.LectureName,
                        Description =enrollLectureViewModel.Description,
                        LanguageId = enrollLectureViewModel.LanguageId,
                        EnrollLectureId = enrollLecture.Id
                    };
                    db.EnrollLectureTranslations.Add(enrollLectureTran);
                    db.SaveChanges();
                }
                return enrollLecture;
            }
        }
        public EnrollLecture AddEnrollLecture_WithoutUsing(EnrollLectureViewModel enrollLectureViewModel, LearningManagementSystemContext db)
        {
           
                var enrollLecture = new EnrollLecture()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    LectureName = enrollLectureViewModel.LectureName,
                    Description = enrollLectureViewModel.Description,
                    EnrollSectionId = enrollLectureViewModel.EnrollSectionId,
                    EnrollCourseId = enrollLectureViewModel.EnrollCourseId,
                    CreatedBy = enrollLectureViewModel.CreatedBy,
                    Order = enrollLectureViewModel.Order,
                };
                db.EnrollLectures.Add(enrollLecture);
                db.SaveChanges();

                enrollLecture.Id = enrollLecture.Id;

                if (enrollLectureViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var enrollLectureTran = new EnrollLectureTranslation()
                    {
                        LectureName = enrollLectureViewModel.LectureName,
                        Description = enrollLectureViewModel.Description,
                        LanguageId = enrollLectureViewModel.LanguageId,
                        EnrollLectureId = enrollLecture.Id
                    };
                    db.EnrollLectureTranslations.Add(enrollLectureTran);
                    db.SaveChanges();
                }
                return enrollLecture;
           
        }
        public EnrollLecture GetEnrollLectureById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lecture = db.EnrollLectures.Include(d => d.EnrollSection).FirstOrDefault(r => r.Id == id);
                return lecture;
            }
        }
        public EnrollLecture GetEnrollLectureById_WithoutUsing(int id, LearningManagementSystemContext db)
        {
           
                var lecture = db.EnrollLectures.Include(d => d.EnrollSection).FirstOrDefault(r => r.Id == id);
                return lecture;
          
        }

        public void EditEnrollLecture(EnrollLectureViewModel lectureViewModel, EnrollLecture lecture)
        {
            using (var db = new LearningManagementSystemContext())
            {

                lecture.EnrollSectionId = lectureViewModel.EnrollSectionId;
                lecture.EnrollCourseId = lectureViewModel.EnrollCourseId;
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
                    var lectureTranslation = db.EnrollLectureTranslations.FirstOrDefault(r =>
                        r.LanguageId == lectureViewModel.LanguageId &&
                        r.EnrollLectureId == lectureViewModel.Id);
                    if (lectureTranslation != null)
                    {
                        lectureTranslation.LectureName = lectureViewModel.LectureName;
                        lectureTranslation.Description = lectureViewModel.Description;
                        db.Entry(lectureTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var lectureTran = new EnrollLectureTranslation()
                        {
                            LectureName = lectureViewModel.LectureName,
                            Description = lectureViewModel.Description,
                            LanguageId = lectureViewModel.LanguageId,
                            EnrollLectureId = lectureViewModel.Id
                        };
                        db.EnrollLectureTranslations.Add(lectureTran);
                    }

                    db.SaveChanges();
                }

            }
        }
        public void EditEnrollLecture_WithoutUsing(EnrollLectureViewModel lectureViewModel, EnrollLecture lecture, LearningManagementSystemContext db)
        {
           
                lecture.EnrollSectionId = lectureViewModel.EnrollSectionId;
                lecture.EnrollCourseId = lectureViewModel.EnrollCourseId;
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
                    var lectureTranslation = db.EnrollLectureTranslations.FirstOrDefault(r =>
                        r.LanguageId == lectureViewModel.LanguageId &&
                        r.EnrollLectureId == lectureViewModel.Id);
                    if (lectureTranslation != null)
                    {
                        lectureTranslation.LectureName = lectureViewModel.LectureName;
                        lectureTranslation.Description = lectureViewModel.Description;
                        db.Entry(lectureTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var lectureTran = new EnrollLectureTranslation()
                        {
                            LectureName = lectureViewModel.LectureName,
                            Description = lectureViewModel.Description,
                            LanguageId = lectureViewModel.LanguageId,
                            EnrollLectureId = lectureViewModel.Id
                        };
                        db.EnrollLectureTranslations.Add(lectureTran);
                    }

                    db.SaveChanges();
                }

         
        }
        public void DeleteEnrollLecture(EnrollLecture enrollLecture)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollLecture.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollLecture.DeletedOn = DateTime.Now;
                db.Entry(enrollLecture).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void DeleteEnrollLecture_WithoutUsing(EnrollLecture enrollLecture, LearningManagementSystemContext db)
        {
            
                enrollLecture.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollLecture.DeletedOn = DateTime.Now;
                db.Entry(enrollLecture).State = EntityState.Modified;
                db.SaveChanges();
            
        }

        public void DeleteEnrollLectureByEnrollTeacherCourseID(int TeacherCourseID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollLectures = db.EnrollLectures.Where(e => e.EnrollCourseId == TeacherCourseID).ToList();
                foreach (var EnrollLecture in EnrollLectures)
                {
                    var EnrollLectureTranslations = db.EnrollLectureTranslations.Where(e => e.EnrollLectureId == EnrollLecture.Id).ToList();
                    db.EnrollLectureTranslations.RemoveRange(EnrollLectureTranslations);

                }
                db.EnrollLectures.RemoveRange(EnrollLectures);
                db.SaveChanges();
            }

        }
        public void DeleteEnrollLectureByEnrollTeacherCourseID_WithoutUsing(int TeacherCourseID, LearningManagementSystemContext db)
        {
          
                var EnrollLectures = db.EnrollLectures.Where(e => e.EnrollCourseId == TeacherCourseID).ToList();
                foreach (var EnrollLecture in EnrollLectures)
                {
                    var EnrollLectureTranslations = db.EnrollLectureTranslations.Where(e => e.EnrollLectureId == EnrollLecture.Id).ToList();
                    db.EnrollLectureTranslations.RemoveRange(EnrollLectureTranslations);

                }
                db.EnrollLectures.RemoveRange(EnrollLectures);
                db.SaveChanges();
           

        }
        public void DeleteEnrollLLectureBySectionID(int SectionID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var lectures = db.EnrollLectures.Where(e => e.EnrollSectionId == SectionID);
                foreach (var Lecture in lectures)
                {
                    Lecture.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    Lecture.DeletedOn = DateTime.Now;
                    Lecture.EnrollSectionId = SectionID;
                    db.Entry(Lecture).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public void DeleteEnrollLLectureBySectionID_WithoutUsing(int SectionID, LearningManagementSystemContext db)
        {
           
                var lectures = db.EnrollLectures.Where(e => e.EnrollSectionId == SectionID);
                foreach (var Lecture in lectures)
                {
                    Lecture.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    Lecture.DeletedOn = DateTime.Now;
                    Lecture.EnrollSectionId = SectionID;
                    db.Entry(Lecture).State = EntityState.Modified;
                }
                db.SaveChanges();
            
        }

    }
}
