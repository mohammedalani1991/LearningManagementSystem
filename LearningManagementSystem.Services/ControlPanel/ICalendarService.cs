using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICalendarService
    {
        IPagedList<Calendar> GetCalendars(string searchText, int? page, int languageId, int pagination);
        Calendar GetCalendarById(int id);
        CalendarViewModel GetCalendarById(int id, int languageId);

        void AddCalendar(CalendarViewModel calendar);
        void EditCalendar(CalendarViewModel calendarViewModel, Calendar calendar);
        void DeleteCalendar(Calendar calendar);
        Task<List<Calendar>> GetCalendarsForGuest(string searchText, DateTime startDate, DateTime endDate, int languageId, int? TypeID);
        Task<List<PrivateCalendarViewModel>> GetCalendarsForStudent(int? contactId, int? studentId, string searchText, DateTime startDate, DateTime endDate, int languageId, int? TypeID);
        Task<List<PrivateCalendarViewModel>> GetCalendarsForTrainer(int contactId ,string searchText, DateTime startDate, DateTime endDate, int languageId, int? TypeID);
    }
}
