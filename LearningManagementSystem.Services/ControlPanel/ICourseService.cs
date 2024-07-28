using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICourseService
    {
        List<Course> GetCoursesList(int languageId);
        List<Course> GetCoursesList(int semesterId, int languageId);
        IPagedList<Course> GetCourses(string searchText, int? page, int languageId, int pagination);

        IPagedList<CourseViewModel> GetCourses(bool showInHome, int? page, int languageId, int pagination=25);
        Task<IPagedList<CourseViewModel>> GetCoursesForHomePgae(bool showInHome, int? page, int languageId, int pagination = 25);
        IPagedList<CourseViewModel> GetActiveCoursesForGuest(bool? showInHome, string search, int? categoryId, int? trainer, int? page, int languageId, int pagination = 25);
        Course GetCourseById(int id);
        CourseViewModel GetCourseById(int id, int languageId);

        void AddCourse(CourseViewModel course);
        void EditCourse(CourseViewModel courseViewModel, Course course);
        void DeleteCourse(Course course);
        List<Course> GetCourseByIdAsList(int id, int languageId);
        List<Course> GetCoursesByName(string CourseName, int languageId);
        Course GetCourseById_WithoutUsing(int id, LearningManagementSystemContext db);
        Course GetCourseByEnrollTeacherCourseId(int id);
        CourseViewModel GetCourseByEnrollTeacherCourseId(int id, int languageId);
    }
}
