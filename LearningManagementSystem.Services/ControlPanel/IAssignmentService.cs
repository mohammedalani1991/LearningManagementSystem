using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IAssignmentService
    {
        IPagedList<Assignment> GetAssignments(string searchText, int? page, int languageId, int pagination, int? CourseId);
        Assignment GetAssignmentById(int id);

        AssignmentViewModel GetAssignmentById(int id, int languageId);

        void AddAssignment(AssignmentViewModel assignment);
        void EditAssignment(AssignmentViewModel assignmentViewModel, Assignment assignment);
        void DeleteAssignment(Assignment assignment);
        List<Assignment> GetAssignmentByCourseId(int Courseid);
        List<AssignmentViewModel> GetAssignmentByCourseId(int Courseid, int languageId);
        List<Assignment> GetAssignmentByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db);
    }
}
