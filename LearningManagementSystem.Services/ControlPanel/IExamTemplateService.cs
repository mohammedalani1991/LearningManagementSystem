using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IExamTemplateService
    {
        IPagedList<ExamTemplate> GetExamTemplates(string searchText, int? page, int languageId, int pagination,int? CourseId,int? CategoryId);
        ExamTemplate GetExamTemplateById(int id);
        ExamTemplateViewModel GetExamTemplateById(int id, int languageId);
        void AddExamTemplate(ExamTemplateViewModel ExamTemplate);
        void EditExamTemplate(ExamTemplateViewModel ExamTemplateViewModel, ExamTemplate ExamTemplate);
        void DeleteExamTemplate(ExamTemplate ExamTemplate);
        List<ExamTemplate> GetdExamTemplatetByCourseId(int Courseid);
        List<ExamTemplateViewModel> GetdExamTemplatetByCourseId(int Courseid, int languageId);
        List<ExamTemplate> GetdExamTemplatetByCourseId_WithoutUsing(int Courseid, LearningManagementSystemContext db);
        public List<ExamTemplateViewModel> GetAllExamTemplatet(int languageId,int? CourseId, int? CategoryId);
    }
}
