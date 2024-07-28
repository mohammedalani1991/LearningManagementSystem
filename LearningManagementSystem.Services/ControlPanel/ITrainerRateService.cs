using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ITrainerRateService
    {
        List<ManagementRateLineViewModel> GetManagmentRates(DateTime date, int enrollId, string user, int langId);
        void AddEditManagmentRates(ManagementRateViewModel managementRate, string user);

        List<AcademicSupervisionRateViewModel> GetAcademicSupervisionRates(DateTime date, int enrollId, string user,int langId);
        void AddEditAcademicSupervisionRates(List<AcademicSupervisionRateViewModel> aboutDics, string user);
    }
}
