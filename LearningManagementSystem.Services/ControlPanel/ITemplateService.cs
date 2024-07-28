using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ITemplateService
    {
        TemplateHtml GetTemplateById(int id);
        List<TemplateHtml> GetCertificates();
        IPagedList<TemplateHtml> GetTemplates(string searchText, int? page, int languageId, int pagination);
        void AddTemplate(TemplateViewModel template);
        void EditTemplate(TemplateViewModel templateViewModel, TemplateHtml template);
        void DeleteTemplate(TemplateHtml template);
    }
}
