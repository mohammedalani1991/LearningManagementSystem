using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using System;
using System.Linq;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Collections.Generic;


namespace LearningManagementSystem.Services.ControlPanel
{
    public class TemplateService : ITemplateService
    {
        private readonly ISettingService _settingService;
        private readonly LearningManagementSystemContext _context;

        public TemplateService(ISettingService settingService, LearningManagementSystemContext context)
        {
            _settingService = settingService;
            _context = context;
        }
        public TemplateHtml GetTemplateById(int id)
        {
            var template = _context.TemplateHtmls.Find(id);
            return template;
        }

        public List<TemplateHtml> GetCertificates()
        {
            var Template = _context.TemplateHtmls.Include(r=>r.Type).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.Type.Code == "Certificate").ToList();
            return Template;
        }

        public IPagedList<TemplateHtml> GetTemplates(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination = 25)
        {
            var templatees = _context.TemplateHtmls.Where(r =>
                r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                templatees = templatees.Where(r => r.Name.Contains(searchText));
            }
            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = templatees;
            var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            return output;
        }

        public void AddTemplate(TemplateViewModel templateViewModel)
        {
            var template = new TemplateHtml()
            {
                CreatedOn = DateTime.Now,
                Status = (int)GeneralEnums.StatusEnum.Active,
                Name = templateViewModel.Name,
                Code = templateViewModel.Code,
                CreatedBy = templateViewModel.CreatedBy,
                TypeId= templateViewModel.TypeId,
                RanderHtml= templateViewModel.RanderHtml,

            };
            _context.TemplateHtmls.Add(template);
            _context.SaveChanges();

        }


        public void EditTemplate(TemplateViewModel templateViewModel, TemplateHtml template)
        {
            template.Code = templateViewModel.Code;
            template.Name = templateViewModel.Name;
            template.RanderHtml = templateViewModel.RanderHtml;
            template.TypeId = templateViewModel.TypeId;
            template.Status = templateViewModel.Status;

            _context.Entry(template).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTemplate(TemplateHtml template)
        {
            template.Status = (int)GeneralEnums.StatusEnum.Deleted;
            template.DeletedOn = DateTime.Now;
            _context.Entry(template).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
