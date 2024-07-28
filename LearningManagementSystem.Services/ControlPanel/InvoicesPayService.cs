using System.Linq;
using LearningManagementSystem.Core;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using DataEntity.Models.EfModels;
using X.PagedList;
using System.Threading.Tasks;
using DataEntity.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using LearningManagementSystem.Services.Helpers;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class InvoicesPayService : IInvoicesPayService
    {
        private readonly ISettingService _settingService;
        public InvoicesPayService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IPagedList<InvoicesPay>> GetInvoicesPay(string searchText, int? page, int languageId, int? CourseId = 0, int? ProcessStatusID = 0)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var InvoicesPaysList = db.InvoicesPays.Where(r =>
                    r.Status != (int)GeneralEnums.StatusEnum.Deleted).Include(r => r.EnrollTeacherCourse.Teacher.Contact.ContactTranslations).Include(r => r.Contact).AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchText))
                    InvoicesPaysList = InvoicesPaysList.Where(r => r.Contact.FullName.Contains(searchText) || r.ReceiptNo.Contains(searchText));

                if (ProcessStatusID > 0)
                {
                    InvoicesPaysList = InvoicesPaysList.Where(r => r.ProcessStatus == ProcessStatusID);
                }
                if (CourseId > 0)
                {
                    InvoicesPaysList = InvoicesPaysList.Where(r => r.EnrollTeacherCourse.CourseId == CourseId);
                }

                var result = InvoicesPaysList;

                var pageSize = int.Parse(_settingService.GetOrCreate(Constants.SystemSettings.ControlPanelPageSize, "10").Value);
                var pageNumber = (page ?? 1);
                var output = await result.OrderByDescending(r => r.Id).ToPagedListAsync(pageNumber, pageSize);

                foreach (var item in output)
                {
                    var Contacts = await db.Contacts.FirstOrDefaultAsync(r => r.Id == item.ContactId);
                    var Courses = await db.Courses.FirstOrDefaultAsync(r => r.Id == item.EnrollTeacherCourse.CourseId);

                    item.Contact.FullName = Contacts.FullName;
                    item.EnrollTeacherCourse.CourseName = Courses.CourseName;
                }

                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var ContactTranslations = await db.ContactTranslations.FirstOrDefaultAsync(r => r.ContactId == item.ContactId && r.LanguageId == languageId);
                        if (ContactTranslations != null)
                            item.Contact.FullName = ContactTranslations.FullName;


                        var CourseTranslations = await db.CourseTranslations.FirstOrDefaultAsync(r => r.CourseId == item.EnrollTeacherCourse.CourseId && r.LanguageId == languageId);
                        if (CourseTranslations != null)
                            item.EnrollTeacherCourse.CourseName = CourseTranslations.CourseName;

                        var teacher = item.EnrollTeacherCourse.Teacher.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (CourseTranslations != null)
                            item.EnrollTeacherCourse.Teacher.Contact.FullName = teacher.FullName;

                    }
                }

                return output;
            }

        }

        public async Task<int> AddInvoicesPay(InvoicesPayViewModel InvoicesPayViewModel, bool accepted = false)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var GeInvoicesPay = await db.InvoicesPays.Where(r => r.ContactId == InvoicesPayViewModel.ContactId && (r.ProcessStatus == (int)GeneralEnums.InvoicesPayStatusEnum.Pending) && r.EnrollTeacherCourseId == InvoicesPayViewModel.EnrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefaultAsync();
                if (GeInvoicesPay != null) { return -1; }

                var InvoicesPay = new InvoicesPay()
                {
                    ContactId = InvoicesPayViewModel.ContactId,
                    EnrollTeacherCourseId = InvoicesPayViewModel.EnrollTeacherCourseId,
                    ProcessStatus = accepted ? (int)GeneralEnums.InvoicesPayStatusEnum.Accepted : (int)GeneralEnums.InvoicesPayStatusEnum.Pending,
                    ReceiptNo = InvoicesPayViewModel.ReceiptNo,
                    AttachmentUrl = InvoicesPayViewModel.AttachmentUrl,
                    Notes = InvoicesPayViewModel.Notes,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    CreatedOn = DateTime.Now,
                    CreatedBy = InvoicesPayViewModel.CreatedBy,
                    CurrencyRate = InvoicesPayViewModel.CurrencyRate,
                    CustomerCurrencyCode = InvoicesPayViewModel.CustomerCurrencyCode,
                };
                await db.InvoicesPays.AddAsync(InvoicesPay);
                await db.SaveChangesAsync();

                return InvoicesPay.Id;
            }
        }

        public async Task<InvoicesPayViewModel> GetInvoicesPayById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {

                var InvoicesPay = await db.InvoicesPays.FirstOrDefaultAsync(d => d.Id == id);
                return new InvoicesPayViewModel(InvoicesPay);
            }
        }

        public async Task<InvoicesPay> ChangeInvoicesPay(InvoicesPayViewModel InvoicesPayViewModel, LearningManagementSystemContext db)
        {

            var GeInvoicesPay = await db.InvoicesPays.Where(r => r.Id == InvoicesPayViewModel.Id && r.ProcessStatus == (int)GeneralEnums.InvoicesPayStatusEnum.Pending && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefaultAsync();
            if (GeInvoicesPay != null)
            {
                var InvoicesPay = await db.InvoicesPays.FirstOrDefaultAsync(r => r.Id == InvoicesPayViewModel.Id);
                InvoicesPay.ProcessStatus = InvoicesPayViewModel.ProcessStatus;
                InvoicesPay.ProcessDate = DateTime.Now;

                db.Entry(InvoicesPay).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return InvoicesPay;
            }
            else
            {
                return null;
            }



        }



    }
}
