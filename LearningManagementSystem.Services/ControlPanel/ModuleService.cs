using System;
using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class ModuleService : IModuleService
    {
        private readonly ISettingService _settingService;
        public ModuleService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<Module> GetModule(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English, int pagination=25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var modules = db.Modules.Include(r=>r.ModuleTranslations).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    modules = modules.Where(r => r.Name.Contains(searchText));
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = modules;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.ModuleTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;
                            item.Description = trans.Description;
                        }
                    }
                }
                return output;
            }
        }
        public Module GetModuleById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var Module = db.Modules.Find(id);
                return Module;
            }
        }
        public ModuleViewModel GetModuleById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.ModuleTranslations.Include(r => r.Module).FirstOrDefault(r => r.LanguageId == languageId && r.ModuleId == id);
                    if (aboutTran != null)
                    {
                        return new ModuleViewModel(aboutTran);
                    }
                }
                var Module = db.Modules.Find(id);
                return new ModuleViewModel(Module);
            }
        }

        public void AddModule(ModuleViewModel Modules)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var about = new Module()
                {
                    CreatedOn = DateTime.Now,
                    Status = Modules.Status,
                    Name = Modules.Name,
                    Description = Modules.Description,
                    SortOrder = Modules.SortOrder,
                    BaseUrl = Modules.BaseUrl,
                    Code = Modules.Code,                    
                    CreatedBy = Modules.CreatedBy,

                };
                db.Modules.Add(about);
                db.SaveChanges();

                Modules.Id = about.Id;

                if (Modules.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var ModuleTran = new ModuleTranslation()
                    {
                        Name = Modules.Name,
                        Description = Modules.Description,
                        LanguageId = Modules.LanguageId,
                        ModuleId = Modules.Id
                    };
                    db.ModuleTranslations.Add(ModuleTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditModule(ModuleViewModel moduleViewModel, Module module)
        {
            using (var db = new LearningManagementSystemContext())
            {
                module.Status = moduleViewModel.Status;
                module.SortOrder = moduleViewModel.SortOrder;
                module.BaseUrl = moduleViewModel.BaseUrl;
                module.Code = moduleViewModel.Code;
                if (moduleViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    module.Name = moduleViewModel.Name;
                    module.Description = moduleViewModel.Description;
                }

                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                if (moduleViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran = db.ModuleTranslations.FirstOrDefault(r =>
                        r.LanguageId == moduleViewModel.LanguageId &&
                        r.ModuleId == moduleViewModel.Id);
                    if (aboutTran != null)
                    {
                        aboutTran.Name = moduleViewModel.Name;
                        aboutTran.Description = moduleViewModel.Description;
                        db.Entry(aboutTran).State = EntityState.Modified;
                    }
                    else
                    {
                        var ModuleTran = new ModuleTranslation()
                        {
                            Name = moduleViewModel.Name,
                            Description = moduleViewModel.Description,
                            LanguageId = moduleViewModel.LanguageId,
                            ModuleId = moduleViewModel.Id
                        };
                        db.ModuleTranslations.Add(ModuleTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteModule(Module module)
        {
            using (var db = new LearningManagementSystemContext())
            {
                module.Status = (int)GeneralEnums.StatusEnum.Deleted;
                module.DeletedOn = DateTime.Now;
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
