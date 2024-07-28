using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel.IServices
{
    public interface ICertificateAdoptionService
    {
        IPagedList<CertificateAdoption> GetCertificateAdoption(int? page, int languageId, int pagination);
        void AddCertificateAdoption(int semesterId, int courseId, string createdBy);
        void DeleteCertificateAdoption(int id);
        void EditCertificateAdoption(int id, bool show);
    }
}
