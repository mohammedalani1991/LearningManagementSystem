using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IEnrollAssignmentService
    {
        EnrollAssignment AddEnrollAssignmentForAdmin(Assignment Assignment, int EnrollCourseId, LearningManagementSystemContext db);
        EnrollAssignment AddEnrollAssignment(EnrollAssignmentViewModel EnrollAssignment);
        void DeleteEnrollAssignment(EnrollAssignment EnrollAssignment);
        void DeleteEnrollAssignmentByEnrollTeacherCourseID(int EnrollTeacherCourseID);
        List<EnrollStudentAssigment> GetEnrollAssignmentByEnrollTeacherCourseId(int EnrollTeacherCourseId, int studentId, int languageId);
        void DeleteEnrollAssignmentByEnrollTeacherCourseID_WithoutUsing(int EnrollTeacherCourseID, LearningManagementSystemContext db);
    }
}
