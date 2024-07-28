using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICourseCategoryService
    {
        IPagedList<CourseCategory> GetCourseCategorys(string searchText, int? page, int languageId, int pagination);
        List<CourseCategory> GetAllCourseCategorys( int languageId);
        List<CourseCategory> GetActiveCourseCategorysForGuest(bool? checkParent, int? parentId, bool? showInHome, int languageId);


        CourseCategory GetCourseCategoryById(int id);
        CourseCategoryViewModel GetCourseCategoryById(int id, int languageId);
        List<Course> GetCourseByCategoryId(int id, int languageId);

        void AddCourseCategory(CourseCategoryViewModel coursecategorys);
        void EditCourseCategory(CourseCategoryViewModel CourseCategoryViewModel, CourseCategory CourseCategory);
        void DeleteCourseCategory(CourseCategory CourseCategory);
    }
}
