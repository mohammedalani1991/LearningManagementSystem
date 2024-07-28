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
    public class BranchService : IBranchService
    {
        private readonly ISettingService _settingService;
        public BranchService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IPagedList<Branch> GetBranches(string searchText, int? page, int languageId = (int)GeneralEnums.LanguageEnum.English,int pagination=25)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var branches = db.Branches.Include(r=>r.BranchTranslations).Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                //if (!string.IsNullOrWhiteSpace(searchText))
                //{
                //    branches = branches.Where(r => r.Name.Contains(searchText));
                //}
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                    {
                        branches = branches.Where(r => r.Name.Contains(searchText));
                    }
                    else
                    {
                        branches = branches.Where(r => r.BranchTranslations.Any(t => t.Name.Contains(searchText) & t.LanguageId == languageId));
                    }
                }
                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = branches;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.BranchTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                        {
                            item.Name = trans.Name;                            
                        }
                    }
                }
                return output;
            }
        }
        public Branch GetBranchById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var branch = db.Branches.Find(id);
                return branch;
            }
        }
        public BranchViewModel GetBranchById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var aboutTran =
                        db.BranchTranslations.Include(r => r.Branch).FirstOrDefault(r => r.LanguageId == languageId && r.BranchId == id);
                    if (aboutTran != null)
                    {
                        return new BranchViewModel(aboutTran);
                    }
                }
                var branch = db.Branches.Find(id);
                return new BranchViewModel(branch);
            }
        }

        public void AddBranch(BranchViewModel branchViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var branch = new Branch()
                {
                    CreatedOn = DateTime.Now,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    Name = branchViewModel.Name,
                    ColorId = branchViewModel.ColorId,
                    Code = branchViewModel.Code,                    
                    CreatedBy = branchViewModel.CreatedBy,
                };
                db.Branches.Add(branch);
                db.SaveChanges();

                branch.Id = branch.Id;

                if (branchViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var branchTran = new BranchTranslation()
                    {
                        Name = branchViewModel.Name,
                        LanguageId = branchViewModel.LanguageId,
                        BranchId = branch.Id
                    };
                    db.BranchTranslations.Add(branchTran);
                    db.SaveChanges();
                }
            }
        }


        public void EditBranch(BranchViewModel branchViewModel, Branch branch)
        {
            using (var db = new LearningManagementSystemContext())
            {                
                branch.ColorId = branchViewModel.ColorId;
                branch.Code = branchViewModel.Code;
                if (branchViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    branch.Name = branchViewModel.Name;
                }

                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                if (branchViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var branchTranslation = db.BranchTranslations.FirstOrDefault(r =>
                        r.LanguageId == branchViewModel.LanguageId &&
                        r.BranchId == branchViewModel.Id);
                    if (branchTranslation != null)
                    {
                        branchTranslation.Name = branchViewModel.Name;
                        db.Entry(branchTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var branchTran = new BranchTranslation()
                        {
                            Name = branchViewModel.Name,
                            LanguageId = branchViewModel.LanguageId,
                            BranchId = branchViewModel.Id
                        };
                        db.BranchTranslations.Add(branchTran);
                    }

                    db.SaveChanges();
                }

            }
        }

        public void DeleteBranch(Branch branch)
        {
            using (var db = new LearningManagementSystemContext())
            {
                branch.Status = (int)GeneralEnums.StatusEnum.Deleted;
                branch.DeletedOn = DateTime.Now;
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
