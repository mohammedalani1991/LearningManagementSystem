using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.ControlPanel.IServices;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static iTextSharp.text.pdf.AcroFields;

namespace LearningManagementSystem.Services.ControlPanel.Services
{
    public class CourseMarksService : ICourseMarksService
    {
        private readonly LearningManagementSystemContext _context;

        public CourseMarksService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public CourseMark GetCourseMarkById(int id)
        {
            return _context.CourseMarks.FirstOrDefault(r => r.Id == id);

        }

        public CourseMark GetCourseMarkById(int id , int languageId)
        {
            var mark = _context.CourseMarks.Include(r=>r.CourseMarkTranslations).FirstOrDefault(r=>r.Id == id);
            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = mark.CourseMarkTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                    mark.Title = trans.Title;
            }
            return mark;
        }

        public List<CourseMark> GetCourseMarks(int courseId, int languageId)
        {
            var marks = _context.CourseMarks.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.CourseId == courseId).Include(r => r.CourseMarkTranslations).AsQueryable();

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in marks)
                {
                    var trans = item.CourseMarkTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Title = trans.Title;
                }

            return marks.ToList();
        }

        public void AddCourseMark(CourseMarkViewModel courseMarkViewModel)
        {
            var courseMark = new CourseMark()
            {
                CourseId = courseMarkViewModel.CourseId,
                CreatedBy = courseMarkViewModel.CreatedBy,
                CreatedOn = DateTime.Now,
                Status = courseMarkViewModel.Status,
                Title = courseMarkViewModel.Title,
                Value = courseMarkViewModel.Value,
                ValueTo = courseMarkViewModel.ValueTo,
            };

            _context.CourseMarks.Add(courseMark);
            _context.SaveChanges();

            if (courseMarkViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var courseTran = new CourseMarkTranslation()
                {
                    Title = courseMarkViewModel.Title,
                    LanguageId = courseMarkViewModel.LanguageId,
                    CourseMarkId = courseMark.Id
                };
                _context.CourseMarkTranslations.Add(courseTran);
                _context.SaveChanges();
            }
        }


        public void EditCourseMark(CourseMarkViewModel courseMarkViewModel, CourseMark courseMark)
        {
            courseMark.Status = courseMarkViewModel.Status;
            courseMark.Value = courseMarkViewModel.Value;
            courseMark.ValueTo = courseMarkViewModel.ValueTo;

            if (courseMarkViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                courseMark.Title = courseMarkViewModel.Title;

            _context.Entry(courseMark).State = EntityState.Modified;
            _context.SaveChanges();

            if (courseMarkViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var courseTranslation = _context.CourseMarkTranslations.FirstOrDefault(r =>
                    r.LanguageId == courseMarkViewModel.LanguageId &&
                    r.CourseMarkId == courseMark.Id);
                if (courseTranslation != null)
                {
                    courseTranslation.Title = courseMarkViewModel.Title;
                    _context.Entry(courseTranslation).State = EntityState.Modified;
                }
                else
                {
                    var courseTran = new CourseMarkTranslation()
                    {
                        Title = courseMarkViewModel.Title,
                        LanguageId = courseMarkViewModel.LanguageId,
                        CourseMarkId = courseMark.Id
                    };
                    _context.CourseMarkTranslations.Add(courseTran);
                }

                _context.SaveChanges();

            }
        }
        public void DeleteCourseMark(CourseMark course)
        {
            course.Status = (int)GeneralEnums.StatusEnum.Deleted;
            course.DeletedOn = DateTime.Now;
            _context.Entry(course).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
