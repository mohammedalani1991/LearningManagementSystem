using System;
using System.Collections.Generic;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IStudentService
    {
        void AddStudent(StudentViewModel studentViewModel);
        void EditStudent(StudentViewModel studentViewModel, Student student);
        IPagedList<Student> GetStudents( FilterViewModel filter, int? companyId, int? page, int languageId, int pagination,int? employeeId);
        StudentViewModel GetStudentById(int id, int languageId);
        Student GetStudentModalById(int id, int languageId);
        public Student GetStudentById(int id);
        IPagedList<Student> GetStudentsForAssign(string searchText, int? EnrollTeacherCourseID,int? page, int languageId, int pagination);
        void DeleteStudent(int id);
        void EditStudentAttendance(StudentAttendanceViewModel studentAttendanceViewModel, StudentAttendance student);
        Student GetStudentByContactId(int ContactId);
        List<EnrollStudentCourse> GetStudentCertificates(int studentId, int languageId);

        void ChangeBalance(Student student ,decimal amount);
        string GetStudentAttendence(int studentId);
        decimal GetPaymentAmount(int studentId);
        int GetCourseCount(int studentId);
    }
}
