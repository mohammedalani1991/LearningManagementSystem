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
    public class EnrollCourseTimeService : IEnrollCourseTimeService
    {
        private readonly ISettingService _settingService;
        public EnrollCourseTimeService(ISettingService settingService, ILectureService lectureService, ICourseService courseService)
        {
            _settingService = settingService;
            
        }
    
        public EnrollCourseTime GetEnrollCourseTimeById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollCourseTime = db.EnrollCourseTimes.FirstOrDefault(r=>r.Id == id);
                return enrollCourseTime;
            }
        }
        public List<EnrollCourseTime> GetEnrollCourseTimeByEnrollTeacherCourseId(int EnrollTeacherCourseID)
        {
            using (var db = new LearningManagementSystemContext())
            {
       
                var enrollCourseTimes = db.EnrollCourseTimes.Where(d => d.EnrollCourseId == EnrollTeacherCourseID && d.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return enrollCourseTimes.ToList();
            }
        }

        public EnrollCourseTime AddEnrollCourseTime(EnrollCourseTimeViewModel enrollCourseTimeViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollCourseTime = new EnrollCourseTime()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    DayId = enrollCourseTimeViewModel.DayId,
                    FromTime = enrollCourseTimeViewModel.FromTime,
                    ToTime = enrollCourseTimeViewModel.ToTime,
                    EnrollCourseId = enrollCourseTimeViewModel.EnrollCourseId,
                    LearningMethodId = enrollCourseTimeViewModel.LearningMethodId,
                    CreatedBy = enrollCourseTimeViewModel.CreatedBy,
                };
                db.EnrollCourseTimes.Add(enrollCourseTime);
                db.SaveChanges();


                return enrollCourseTime;
            }
        }

        public EnrollCourseTime AddEnrollCourseTime_WithoutUsing(EnrollCourseTimeViewModel enrollCourseTimeViewModel, LearningManagementSystemContext db)
        {
          
                var enrollCourseTime = new EnrollCourseTime()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    DayId = enrollCourseTimeViewModel.DayId,
                    FromTime = enrollCourseTimeViewModel.FromTime,
                    ToTime = enrollCourseTimeViewModel.ToTime,
                    EnrollCourseId = enrollCourseTimeViewModel.EnrollCourseId,
                    LearningMethodId = enrollCourseTimeViewModel.LearningMethodId,
                    CreatedBy = enrollCourseTimeViewModel.CreatedBy,
                };
                db.EnrollCourseTimes.Add(enrollCourseTime);
                db.SaveChanges();


                return enrollCourseTime;
          
        }

        public void DeleteEnrollCourseTime(EnrollCourseTime enrollCourseTime)
        {
            using (var db = new LearningManagementSystemContext())
            {
                enrollCourseTime.Status = (int)GeneralEnums.StatusEnum.Deleted;
                enrollCourseTime.DeletedOn = DateTime.Now;
                db.Entry(enrollCourseTime).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteEnrollCourseTimeByEnrollTeacherCourseID(int EnrollTeacherCourseID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var EnrollCourseTimes = db.EnrollCourseTimes.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
                db.EnrollCourseTimes.RemoveRange(EnrollCourseTimes);
                db.SaveChanges();
            }

        }
        public void DeleteEnrollCourseTimeByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db)
        {
           
                var EnrollCourseTimes = db.EnrollCourseTimes.Where(e => e.EnrollCourseId == EnrollTeacherCourseID).ToList();
                db.EnrollCourseTimes.RemoveRange(EnrollCourseTimes);
                db.SaveChanges();
            

        }

    }
}
