using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ICmsPageService
    {
        IPagedList<CmsPage> GetCmsPages(string searchText, int? page, int languageId, int pagination);
        IPagedList<CmsPage> GetActiveCmsPages(string searchText,int categoryId, DateTime? fromDate,DateTime? toDate,int? page, int languageId, int pagination);
        List<CmsPage> GetCmsPageForHome(int languageId);

        CmsPage GetCmsPageById(int id);
        CmsPage GetActiveCmsPageById(int id, int languageId);
        CmsPageViewModel GetCmsPageById(int id, int languageId);

        void AddCmsPage(CmsPageViewModel CmsPage);
        void EditCmsPage(CmsPageViewModel calendarViewModel, CmsPage CmsPage);
        void DeleteCmsPage(CmsPage CmsPage);
    }
}
