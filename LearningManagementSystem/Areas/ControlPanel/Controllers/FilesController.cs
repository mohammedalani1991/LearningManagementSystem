using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Filters;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class FilesController : Controller
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ILogService _logService;
        private readonly ISettingService _settingService;

        public FilesController(LearningManagementSystemContext context,
            ILogService logService, ISettingService settingService)
        {
            _context = context;
            _logService = logService;
            _settingService = settingService;
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteFile(DeleteFileViewModel item)
        {
            try
            {
                var itemFile = await _context.ItemFiles.FindAsync(item.Id);
                itemFile.Status = (int)GeneralEnums.StatusEnum.Deleted;
                _context.Update(itemFile);
                await _context.SaveChangesAsync();
                return Redirect(item.ReturnUrl);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Deleting File (Post)");
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        /// <summary>
        /// To use formData.append('files', files);
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadFiles(List<IFormFile> files)
        {
            try
            {
                var urls = SystemFilesHelper.AddFile(files, _context, User.Identity?.Name ?? string.Empty);

                return Json(urls);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Uploading file (Post)");
                return Json(null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadInvoiceFiles(List<IFormFile> files)
        {
            try
            {
                var urls = SystemFilesHelper.AddInvoiceFile(files, _context, User.Identity?.Name ?? string.Empty);

                return Json(urls);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Uploading file (Post)");
                return Json(null);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadStudentExamAttachments(List<IFormFile> files)
        {
            try
            {
                var urls = SystemFilesHelper.AddStudentExamAttachments(files, _context, User.Identity?.Name ?? string.Empty);

                return Json(urls);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Uploading file (Post)");
                return Json(null);
            }
        }

        public IActionResult UploadListOfFiles(List<IFormFile> files, int file)
        {
            try
            {
                var ids = SystemFilesHelper.AddFiles(files, _context, User.Identity?.Name ?? string.Empty, file);

                return Json(ids);
            }
            catch (Exception ex)
            {
                _logService.LogException(User.Identity?.Name ?? string.Empty, ex, "Error While Uploading file (Post)");
                return Json(null);
            }
        }
    }
}