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

namespace LearningManagementSystem.Services.ControlPanel
{
    public class CalendarService : ICalendarService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;
        private readonly IStudentService _studentService;
        private readonly ITrainerService _trainerService;

        public CalendarService(ISettingService settingService, LearningManagementSystemContext context, IStudentService studentService, ITrainerService trainerService)
        {
            _settingService = settingService;
            _context = context;
            _studentService = studentService;
            _trainerService = trainerService;
        }
        public IPagedList<Calendar> GetCalendars(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var calendars = db.Calendars.Include(r => r.CalendarTranslations).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);


                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        calendars = calendars.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        calendars = calendars.Where(r => r.CalendarTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = calendars;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.CalendarTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                        }
                    }
                }
                return output;
            }
        }
        public Calendar GetCalendarById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var calendar = db.Calendars.Find(id);
                return calendar;
            }
        }
        public CalendarViewModel GetCalendarById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.CalendarTranslations.Include(r => r.Calendar).FirstOrDefault(r => r.LanguageId == languageId && r.CalendarId == id);
                    if (aboutTran != null)
                    {
                        return new CalendarViewModel(aboutTran);
                    }
                }
                var calendar = db.Calendars.Find(id);
                return new CalendarViewModel(calendar);
            }
        }

        public void AddCalendar(CalendarViewModel calendarViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var calendar = new Calendar()
                {
                    CreatedOn = DateTime.Now,
                    Status = calendarViewModel.Status,
                    Name = calendarViewModel.Name,
                    Description = calendarViewModel.Description,
                    StartDate = calendarViewModel.StartDate,
                    EndDate = calendarViewModel.EndDate,
                    Type = calendarViewModel.Type,
                    CreatedBy = calendarViewModel.CreatedBy,
                };
                db.Calendars.Add(calendar);
                db.SaveChanges();

                calendar.Id = calendar.Id;

                if (calendarViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var calendarTran = new CalendarTranslation()
                    {
                        Name = calendarViewModel.Name,
                        Description = calendarViewModel.Description,
                        LanguageId = calendarViewModel.LanguageId,
                        CalendarId = calendar.Id
                    };
                    db.CalendarTranslations.Add(calendarTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditCalendar(CalendarViewModel calendarViewModel, Calendar calendar)
        {
            using (var db = new LearningManagementSystemContext())
            {
                calendar.StartDate = calendarViewModel.StartDate;
                calendar.EndDate = calendarViewModel.EndDate;
                calendar.Type = calendarViewModel.Type;
                calendar.Status = calendarViewModel.Status;

                if (calendarViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    calendar.Name = calendarViewModel.Name;
                    calendar.Description = calendarViewModel.Description;
                }

                db.Entry(calendar).State = EntityState.Modified;
                db.SaveChanges();
                if (calendarViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var calendarTranslation = db.CalendarTranslations.FirstOrDefault(r =>
                        r.LanguageId == calendarViewModel.LanguageId &&
                        r.CalendarId == calendarViewModel.Id);
                    if (calendarTranslation != null)
                    {
                        calendarTranslation.Name = calendarViewModel.Name;
                        calendarTranslation.Description = calendarViewModel.Description;
                        db.Entry(calendarTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var calendarTran = new CalendarTranslation()
                        {
                            Name = calendarViewModel.Name,
                            Description = calendarViewModel.Description,
                            LanguageId = calendarViewModel.LanguageId,
                            CalendarId = calendarViewModel.Id
                        };
                        db.CalendarTranslations.Add(calendarTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteCalendar(Calendar calendar)
        {
            using (var db = new LearningManagementSystemContext())
            {
                calendar.Status = (int)GeneralEnums.StatusEnum.Deleted;
                calendar.DeletedOn = DateTime.Now;
                db.Entry(calendar).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public async Task<List<Calendar>> GetCalendarsForGuest(string searchText, DateTime startDate, DateTime endDate, int languageId, int? TypeID)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var calendars = db.Calendars.Include(r => r.CalendarTranslations).Where(r =>
                    r.Status == (int)GeneralEnums.StatusEnum.Active);

                if (!string.IsNullOrWhiteSpace(searchText))
                {

                    if (languageId != CultureHelper.GetDefaultLanguageId())
                    {
                        calendars = calendars.Where(r => r.CalendarTranslations.Any(t => t.Name.Contains(searchText)));
                    }
                    else
                    {
                        calendars = calendars.Where(r => r.Name.Contains(searchText));
                    }
                }


                if (startDate != null && startDate != DateTime.MinValue && endDate != null && endDate != DateTime.MinValue)
                {
                    calendars = calendars.Where(r => (r.StartDate >= startDate && r.StartDate <= endDate) || (startDate >= r.StartDate && endDate <= r.EndDate) || (r.EndDate >= startDate && r.EndDate <= endDate));
                }
                if (TypeID > 0)
                {
                    calendars = calendars.Where(r => r.Type == TypeID);
                }

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in calendars)
                    {
                        var trans = item.CalendarTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                        }
                    }
                }
                return await calendars.ToListAsync();
            }
        }

        public async Task<List<PrivateCalendarViewModel>> GetCalendarsForStudent(int? contactId, int? studentId, string searchText, DateTime startDate, DateTime endDate, int languageId, int? TypeID)
        {
            var id = 0;
            if (contactId != null)
                id = _studentService.GetStudentByContactId(contactId ?? 0)?.Id??0;
            else
                id = studentId??0;

            var list = new List<PrivateCalendarViewModel>();
            var calendars = _context.Calendars.Include(r => r.CalendarTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);
            var studentCourses = _context.EnrollStudentCourses.Where(r => r.StudentId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.Course.EnrollTeacherCourseTranlations).Include(r => r.Course.EnrollCourseTimes).Include(r => r.Course.EnrollCourseExams);


            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in calendars)
                {
                    var trans = item.CalendarTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Name = trans.Name;
                }
                foreach (var item in studentCourses)
                {
                    var trans = item.Course.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Course.CourseName = trans.CourseName;
                }
            }
            foreach (var item in calendars)
                list.Add(new PrivateCalendarViewModel()
                {
                    Description = item.Description,
                    Name = item.Name,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Type = item.Type,
                });

            foreach (var item in studentCourses)
            {
                if (item.Course.WorkEndDate != null)
                    foreach (var item1 in item.Course.EnrollCourseTimes)
                    {
                        var day = LookupHelper.GetWeekDaysEnum(item1.DayId);
                        var length = item.Course.WorkEndDate.Value - item.Course.WorkStartDate;
                        for (int i = 0; i <= length.Days; i++)
                            if (item.Course.WorkStartDate.AddDays(i).DayOfWeek.ToString() == day)
                                list.Add(new PrivateCalendarViewModel()
                                {
                                    Description = "",
                                    Name = item.Course.CourseName,
                                    StartDate = item.Course.WorkStartDate.AddDays(i) + item1.FromTime.Value,
                                    EndDate = item.Course.WorkStartDate.AddDays(i) + item1.ToTime.Value,
                                });
                    }

                foreach (var item1 in item.Course.EnrollCourseExams)
                    if (item1.PublishDate != null && item1.PublishEndDate != null)
                        list.Add(new PrivateCalendarViewModel()
                        {
                            Description = item1.Description,
                            Name = item1.Name,
                            StartDate = item1.PublishDate.Value,
                            EndDate = item1.PublishEndDate.Value,
                        });
            }

            if (!string.IsNullOrWhiteSpace(searchText))
                list = list.Where(r => r.Name.Contains(searchText)).ToList();
            if (startDate != null && startDate != DateTime.MinValue && endDate != null && endDate != DateTime.MinValue)
                list = list.Where(r => (r.StartDate >= startDate && r.StartDate <= endDate) || (startDate >= r.StartDate && endDate <= r.EndDate) || (r.EndDate >= startDate && r.EndDate <= endDate)).ToList();
            if (TypeID > 0)
                list = list.Where(r => r.Type == TypeID).ToList();

            return await list.ToListAsync();
        }

        public async Task<List<PrivateCalendarViewModel>> GetCalendarsForTrainer(int contactId, string searchText, DateTime startDate, DateTime endDate, int languageId, int? TypeID)
        {
            var trainerDetails = _trainerService.GetTrainerByContactId(contactId);

            var list = new List<PrivateCalendarViewModel>();
            var calendars = _context.Calendars.Include(r => r.CalendarTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);
            var teacherCourses = _context.EnrollTeacherCourses.Where(r => r.TeacherId == trainerDetails.Id && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.EnrollTeacherCourseTranlations).Include(r => r.EnrollCourseTimes).Include(r => r.EnrollCourseExams);


            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in calendars)
                {
                    var trans = item.CalendarTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.Name = trans.Name;
                }
                foreach (var item in teacherCourses)
                {
                    var trans = item.EnrollTeacherCourseTranlations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                        item.CourseName = trans.CourseName;
                }
            }
            foreach (var item in calendars)
                list.Add(new PrivateCalendarViewModel()
                {
                    Description = item.Description,
                    Name = item.Name,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Type = item.Type,
                });

            foreach (var item in teacherCourses)
            {
                if (item.WorkEndDate != null)
                    foreach (var item1 in item.EnrollCourseTimes)
                    {
                        var day = LookupHelper.GetWeekDaysEnum(item1.DayId);
                        var length = item.WorkEndDate.Value - item.WorkStartDate;
                        for (int i = 0; i <= length.Days; i++)
                            if (item.WorkStartDate.AddDays(i).DayOfWeek.ToString() == day)
                                list.Add(new PrivateCalendarViewModel()
                                {
                                    Description = "",
                                    Name = item.CourseName,
                                    StartDate = item.WorkStartDate.AddDays(i) + item1.FromTime.Value,
                                    EndDate = item.WorkStartDate.AddDays(i) + item1.ToTime.Value,
                                });
                    }

                foreach (var item1 in item.EnrollCourseExams)
                    if (item1.PublishDate != null && item1.PublishEndDate != null)
                        list.Add(new PrivateCalendarViewModel()
                        {
                            Description = item1.Description,
                            Name = item1.Name,
                            StartDate = item1.PublishDate.Value,
                            EndDate = item1.PublishEndDate.Value,
                        });
            }

            if (!string.IsNullOrWhiteSpace(searchText))
                list = list.Where(r => r.Name.Contains(searchText)).ToList();
            if (startDate != null && startDate != DateTime.MinValue && endDate != null && endDate != DateTime.MinValue)
                list = list.Where(r => (r.StartDate >= startDate && r.StartDate <= endDate) || (startDate >= r.StartDate && endDate <= r.EndDate) || (r.EndDate >= startDate && r.EndDate <= endDate)).ToList();
            if (TypeID > 0)
                list = list.Where(r => r.Type == TypeID).ToList();

            return await list.ToListAsync();
        }
    }
}
