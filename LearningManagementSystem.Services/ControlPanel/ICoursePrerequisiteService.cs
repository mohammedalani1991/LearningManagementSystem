using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICoursePrerequisiteService
    {
        Task<IPagedList<Course>> GetCoursePrerequisites(string searchText, int? page, int languageId, int pagination,int? CourseId);
        CoursePrerequisite GetCoursePrerequisiteById(int id);

        List<CoursePrerequisite> GetCoursePrerequisiteByCourseId(int Courseid);
        List<CoursePrerequisite> GetCoursePrerequisiteByCourseId(int Courseid, int languageId);
        void AddCoursePrerequisite(CoursePrerequisiteViewModel coursePrerequisite);
        void EditCoursePrerequisite(CoursePrerequisiteViewModel coursePrerequisiteViewModel, CoursePrerequisite coursePrerequisite);
        void DeleteCoursePrerequisite(CoursePrerequisite coursePrerequisite);
        void DeleteCoursePrerequisiteByCourseId(int CourseId);
        void DeleteCoursePrerequisiteByPrerequisiteCourseId(int PrerequisiteCourseId);
        void AddCoursePrerequisite_WithoutUsing(CoursePrerequisiteViewModel coursePrerequisiteViewModel, LearningManagementSystemContext db);
        List<CoursePrerequisite> GetCoursePrerequisiteByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db);
        void DeleteCoursePrerequisite_WithoutUsing(CoursePrerequisite coursePrerequisite, LearningManagementSystemContext db);
        void DeleteCoursePrerequisiteByCourseId_WithoutUsing(int CourseId, LearningManagementSystemContext db);
        CoursePrerequisite GetCoursePrerequisiteById_WithoutUsing(int id, LearningManagementSystemContext db);
        void EditCoursePrerequisite_WithoutUsing(CoursePrerequisiteViewModel coursePrerequisiteViewModel, CoursePrerequisite coursePrerequisite, LearningManagementSystemContext db);
        List<CoursePrerequisiteViewModel> GetViewModeCoursePrerequisiteByCourseId(int Courseid, int languageId);
        void DeleteCoursePrerequisites(List<CoursePrerequisite> CoursePrerequisites);
    }
}
