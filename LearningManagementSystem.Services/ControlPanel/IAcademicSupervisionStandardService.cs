using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface IAcademicSupervisionStandardService
    {
        IPagedList<AcademicSupervisionStandard> GetAcademicSupervisionStandards(string searchText, int? page, int languageId, int pagination);
        AcademicSupervisionStandard GetAcademicSupervisionStandardById(int id);
        AcademicSupervisionStandard GetAcademicSupervisionStandardById(int id, int languageId);
        void AddAcademicSupervisionStandard(AcademicSupervisionStandardViewModel AcademicSupervisionStandardViewModel);
        void EditAcademicSupervisionStandard(AcademicSupervisionStandardViewModel AcademicSupervisionStandardViewModel, AcademicSupervisionStandard AcademicSupervisionStandard);
        void DeleteAcademicSupervisionStandard(AcademicSupervisionStandard AcademicSupervisionStandard);
    }
}
