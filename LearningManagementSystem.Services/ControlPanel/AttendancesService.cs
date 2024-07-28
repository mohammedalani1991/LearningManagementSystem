using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class AttendancesService : IAttendancesService
    {
        private readonly LearningManagementSystemContext _context;

        public AttendancesService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public CourseAttendance GetStudentAttendanceById(int id, DateTime dateTime)
        {
            return _context.CourseAttendances.FirstOrDefault(r => r.EnrollStudentCourseId == id && r.Status == (int)GeneralEnums.StatusEnum.Active && r.Date == dateTime);
        }

        public List<CourseAttendance> GetStudentAttendances(int courseId, int id)
        {
            return _context.CourseAttendances.Where(r => r.EnrollStudentCourseId == id && r.EnrollTeacherCourseId == courseId && r.Status == (int)GeneralEnums.StatusEnum.Active).OrderBy(r => r.Date).ToList();
        }

        public List<CourseAttendance> GetAttendances(int Id, DateTime dateTime)
        {
            var attendances = _context.CourseAttendances.Where(r => r.EnrollTeacherCourseId == Id && r.Status == (int)GeneralEnums.StatusEnum.Active && r.Date == dateTime);
            return attendances.ToList();
        }

        public IPagedList<EnrollStudentCourse> GetAttendances(int Id, string searchText, int page, int pagination, int languageId)
        {
            var attendances = _context.EnrollStudentCourses.Where(r => r.CourseId == Id && r.Status == (int)GeneralEnums.StatusEnum.Active)
                .Include(r => r.Student.Contact.ContactTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    attendances = attendances.Where(r => r.Student.Contact.FullName.Contains(searchText));
                else
                    attendances = attendances.Where(r => r.Student.Contact.ContactTranslations.Any(t => t.FullName.Contains(searchText) & t.LanguageId == languageId));
            }

            var pageSize = pagination;
            var pageNumber = page;
            var result = attendances;

            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Student.Contact.FullName = trans.FullName;
                }

            return output;
        }

        public void AddStudentAttendance(StudentAttendanceViewModel studentAttendanceViewModel)
        {
            var attendance = new CourseAttendance
            {
                EnrollTeacherCourseId = studentAttendanceViewModel.CourseId,
                EnrollStudentCourseId = studentAttendanceViewModel.EnrollStudentCourseId,
                AbsenceNote = studentAttendanceViewModel.AbsenceNote,
                CreatedBy = studentAttendanceViewModel.CreatedBy,
                CreatedOn = DateTime.Now,
                Date = studentAttendanceViewModel.Date,
                IsPresent = studentAttendanceViewModel.IsPresent,
                Status = studentAttendanceViewModel.Status,
            };

            _context.CourseAttendances.Add(attendance);
            _context.SaveChanges();
        }

        public void EditStudentAttendance(StudentAttendanceViewModel studentAttendanceViewModel, CourseAttendance attendance)
        {
            attendance.IsPresent = studentAttendanceViewModel.IsPresent;
            attendance.AbsenceNote = studentAttendanceViewModel.AbsenceNote ?? attendance.AbsenceNote;
            _context.Entry(attendance).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteStudentAttendance(int courseId, DateTime date)
        {
            var attendence = _context.CourseAttendances.Where(r=>r.EnrollTeacherCourseId == courseId && r.Date == date);
            _context.CourseAttendances.RemoveRange(attendence);
            _context.SaveChanges();
        }

        public bool checkIfDayIsValid(int courseId, DateTime date)
        {
            return true;

            var course = _context.EnrollTeacherCourses.Include(r => r.EnrollCourseTimes).FirstOrDefault(r => r.Id == courseId && r.Status == (int)GeneralEnums.StatusEnum.Active);
            GeneralEnums.WeekDaysEnum dayOfWeek = (GeneralEnums.WeekDaysEnum)Enum.Parse(typeof(GeneralEnums.WeekDaysEnum), date.DayOfWeek.ToString());
            int id = (int)dayOfWeek;
            if (course.EnrollCourseTimes.Select(r => r.DayId).Contains(id))
                return true;
            else
                return false;
        }


        public TeacherAttendance GetAttendanceById(int id)
        {
            return _context.TeacherAttendances.FirstOrDefault(r => r.Id == id);
        }

        public IPagedList<TeacherAttendance> GetTeacherAttendances(int courseId, int page, int pagination)
        {
            var attendances = _context.TeacherAttendances.Where(r => r.EnrollTeacherCourseId == courseId);
            return attendances.OrderByDescending(r => r.Date).ToPagedList(page, pagination);
        }

        public TeacherAttendance GetTeacherAttendance(int courseId, DateTime date)
        {
            var attendance = _context.TeacherAttendances.FirstOrDefault(r => r.EnrollTeacherCourseId == courseId && r.Date == date);
            return attendance;
        }

        public void AddAttendance(int courseId, DateTime date, string note, bool attended)
        {
            _context.TeacherAttendances.Add(new TeacherAttendance
            {
                Date = date,
                Attended = attended,
                EnrollTeacherCourseId = courseId,
                Note = note
            });
            _context.SaveChanges();
        }

        public void DeleteAttendance(TeacherAttendance attendance)
        {
            _context.TeacherAttendances.Remove(attendance); _context.SaveChanges();
        }  
       
    }
}
