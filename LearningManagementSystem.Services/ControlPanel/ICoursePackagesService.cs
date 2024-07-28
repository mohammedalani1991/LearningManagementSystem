using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICoursePackagesService
    {
        Task<IPagedList<CoursePackageViewModel>> GetCoursePackages(bool admin ,string searchText, int? page, int languageId, int pagination, int? TeacherId = 0, int? CourseId=0);
        CoursePackage GetCoursePackageById(int id);
        CoursePackage GetCoursePackageById(int id, int languageId);
        List<CoursePackagesRelation> GetCoursePackagesRelationByPackageId(int PackageId, int languageId);
        void DeleteCoursePackageRelation(int CoursePackageId, LearningManagementSystemContext db);
        void DeleteCoursePackages(int CoursePackageId, LearningManagementSystemContext db);
        CoursePackage AddCoursePackage(CoursePackageViewModel CoursePackageViewModel, LearningManagementSystemContext db);
        CoursePackage EditCoursePackage(CoursePackageViewModel CoursePackageViewModel, CoursePackage CoursePackage, LearningManagementSystemContext db);
        CoursePackagesRelation AddCoursePackagesRelation(CoursePackagesRelationsViewModel CoursePackagesRelationsViewModel, LearningManagementSystemContext db);
        List<EnrollTeacherCourse> GetCoursePackagesRelationByPackageIdAndTeacherId(int PackageId, int TeacherId, int languageId);
        void DeleteCoursePackageTranslations(int CoursePackageId, LearningManagementSystemContext db);
    }
}
