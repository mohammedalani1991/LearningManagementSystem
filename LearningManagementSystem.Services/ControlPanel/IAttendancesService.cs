using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IAttendancesService
    {
        CourseAttendance GetStudentAttendanceById(int id, DateTime dateTime);
        List<CourseAttendance> GetStudentAttendances(int courseId, int id);
        List<CourseAttendance> GetAttendances(int Id, DateTime dateTime);
        IPagedList<EnrollStudentCourse> GetAttendances(int Id, string searchText, int page, int pagination, int languageId);
        void AddStudentAttendance(StudentAttendanceViewModel studentAttendanceViewModel);
        void EditStudentAttendance(StudentAttendanceViewModel studentAttendanceViewModel, CourseAttendance attendance);
        void DeleteStudentAttendance(int courseId, DateTime date);
        bool checkIfDayIsValid(int courseId, DateTime date);

        TeacherAttendance GetAttendanceById(int id);
        IPagedList<TeacherAttendance> GetTeacherAttendances(int courseId, int page, int pagination);
        TeacherAttendance GetTeacherAttendance(int courseId, DateTime date);
        void AddAttendance(int courseId, DateTime date, string note, bool attended);
        void DeleteAttendance(TeacherAttendance attendance);
    }
}
