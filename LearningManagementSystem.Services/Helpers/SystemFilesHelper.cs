using System;
using System.Collections.Generic;
using System.IO;
using LearningManagementSystem.Core.SystemEnums;
using DataEntity.Models.EfModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services.Helpers
{
    public class SystemFilesHelper
    {
        public static List<string> AddFile(List<IFormFile> files, LearningManagementSystemContext context, string username)
        {
            var fileUrls = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var sysFile = new SystemFile
                    {
                        CreatedBy = username,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        TypeId = (int)GeneralEnums.FileEnum.Image,
                        FileUrl = string.Empty,
                        ModifiedBy = username,
                        ModifiedOn = DateTime.Now,
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/'))
                    };
                    context.SystemFiles.Add(sysFile);
                    context.SaveChanges();
                    
                    var extention = Path.GetExtension(formFile.FileName.Trim('"').Trim('/'));
                    var filePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Document\\{(int)GeneralEnums.FileEnum.Image}\\{sysFile.DisplayName}-{sysFile.Id}{extention}";

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    var url = $"/Document/{(int)GeneralEnums.FileEnum.Image}/{sysFile.DisplayName}-{sysFile.Id}{extention}";

                    sysFile.FileUrl = url;
                    context.Entry(sysFile).State = EntityState.Modified;
                    context.SaveChanges();

                    var sysFileTrans = new SystemFileTranslation()
                    {
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        IsDefault = true,
                        LanguageId = CultureHelper.GetDefaultLanguageId(),
                        SystemFileId = sysFile.Id
                    };
                    context.SystemFileTranslations.Add(sysFileTrans);
                    context.SaveChanges();
                    fileUrls.Add(url);
                }
            }

            return fileUrls;
        }
        public static List<string> AddInvoiceFile(List<IFormFile> files, LearningManagementSystemContext context, string username)
        {
            var fileUrls = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var sysFile = new SystemFile
                    {
                        CreatedBy = username,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        TypeId = (int)GeneralEnums.FileEnum.InvoiceStudent,
                        FileUrl = string.Empty,
                        ModifiedBy = username,
                        ModifiedOn = DateTime.Now,
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/'))
                    };
                    context.SystemFiles.Add(sysFile);
                    context.SaveChanges();
                    var NewGuid = Guid.NewGuid();
                    var extention = Path.GetExtension(formFile.FileName.Trim('"').Trim('/'));
                    var filePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Document\\{(int)GeneralEnums.FileEnum.InvoiceStudent}\\{NewGuid}{extention}";

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    var url = $"/Document/{(int)GeneralEnums.FileEnum.InvoiceStudent}/{NewGuid}{extention}";

                    sysFile.FileUrl = url;
                    context.Entry(sysFile).State = EntityState.Modified;
                    context.SaveChanges();

                    var sysFileTrans = new SystemFileTranslation()
                    {
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        IsDefault = true,
                        LanguageId = CultureHelper.GetDefaultLanguageId(),
                        SystemFileId = sysFile.Id
                    };
                    context.SystemFileTranslations.Add(sysFileTrans);
                    context.SaveChanges();
                    fileUrls.Add(url);
                }
            }

            return fileUrls;
        }
        public static List<string> AddStudentExamAttachments(List<IFormFile> files, LearningManagementSystemContext context, string username)
        {
            var fileUrls = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var sysFile = new SystemFile
                    {
                        CreatedBy = username,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        TypeId = (int)GeneralEnums.FileEnum.StudentExamAttachments,
                        FileUrl = string.Empty,
                        ModifiedBy = username,
                        ModifiedOn = DateTime.Now,
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/'))
                    };
                    context.SystemFiles.Add(sysFile);
                    context.SaveChanges();
                    var NewGuid = Guid.NewGuid();
                    var extention = Path.GetExtension(formFile.FileName.Trim('"').Trim('/'));
                    var filePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Document\\{(int)GeneralEnums.FileEnum.StudentExamAttachments}\\{NewGuid}{extention}";

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    var url = $"/Document/{(int)GeneralEnums.FileEnum.StudentExamAttachments}/{NewGuid}{extention}";

                    sysFile.FileUrl = url;
                    context.Entry(sysFile).State = EntityState.Modified;
                    context.SaveChanges();

                    var sysFileTrans = new SystemFileTranslation()
                    {
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        IsDefault = true,
                        LanguageId = CultureHelper.GetDefaultLanguageId(),
                        SystemFileId = sysFile.Id
                    };
                    context.SystemFileTranslations.Add(sysFileTrans);
                    context.SaveChanges();
                    fileUrls.Add(url);
                }
            }

            return fileUrls;
        }
        public static List<string> AddFiles(List<IFormFile> files, LearningManagementSystemContext context, string username, int file)
        {
            var fileUrls = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var sysFile = new SystemFile
                    {
                        CreatedBy = username,
                        CreatedOn = DateTime.Now,
                        Status = (int)GeneralEnums.StatusEnum.Active,
                        TypeId = file,
                        FileUrl = string.Empty,
                        ModifiedBy = username,
                        ModifiedOn = DateTime.Now,
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/'))
                    };
                    context.SystemFiles.Add(sysFile);
                    context.SaveChanges();

                    var extention = Path.GetExtension(formFile.FileName.Trim('"').Trim('/'));
                    var filePath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Document\\{file}\\{sysFile.Id}{extention}";

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    var url = $"/Document/{file}/{sysFile.Id}{extention}";

                    sysFile.FileUrl = url;
                    context.Entry(sysFile).State = EntityState.Modified;
                    context.SaveChanges();
                    fileUrls.Add(url);

                    var sysFileTrans = new SystemFileTranslation()
                    {
                        Description = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        DisplayName = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        AltText = Path.GetFileNameWithoutExtension(formFile.FileName.Trim('"').Trim('/')),
                        IsDefault = true,
                        LanguageId = CultureHelper.GetDefaultLanguageId(),
                        SystemFileId = sysFile.Id
                    };
                    context.SystemFileTranslations.Add(sysFileTrans);
                    context.SaveChanges();
                }
            }

            return fileUrls;
        }

    }
}
