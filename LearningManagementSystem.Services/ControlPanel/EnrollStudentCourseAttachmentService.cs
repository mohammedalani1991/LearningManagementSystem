using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollStudentCourseAttachmentService : IEnrollStudentCourseAttachmentService
    {
        public void AddEnrollStudentCourseAttachment(EnrollStudentCourseAttachmentViewModel enrollStudentCourseAttachmentViewModel)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollStudentCourseAttachment = new EnrollStudentCourseAttachment()
                {
                    CreatedBy = enrollStudentCourseAttachmentViewModel.CreatedBy,
                     CreatedOn = enrollStudentCourseAttachmentViewModel.CreatedOn,
                     EnrollStudentCourseId = enrollStudentCourseAttachmentViewModel.EnrollStudentCourseId,
                     FileAttached = enrollStudentCourseAttachmentViewModel.FileAttached,
                     Notes = enrollStudentCourseAttachmentViewModel.Notes,
                     Status = enrollStudentCourseAttachmentViewModel.Status
                };

                db.EnrollStudentCourseAttachments.Add(enrollStudentCourseAttachment);
                db.SaveChanges();
            }
        }

        public EnrollStudentCourseAttachmentViewModel GetEnrollStudentCourseAttachmentByEnrollStudentCourseId(int enrollStudentCourseId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var enrollStudentCourseAttachment = db.EnrollStudentCourseAttachments.FirstOrDefault(s => s.EnrollStudentCourseId == enrollStudentCourseId);

                if(enrollStudentCourseAttachment == null)
                {
                    return new EnrollStudentCourseAttachmentViewModel();
                }

                return new EnrollStudentCourseAttachmentViewModel(enrollStudentCourseAttachment);
            }
        }
    }
}
