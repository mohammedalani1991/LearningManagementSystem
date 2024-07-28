using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.ControlPanel
{
   public interface IEnrollStudentCourseAttachmentService
    {
        void AddEnrollStudentCourseAttachment(EnrollStudentCourseAttachmentViewModel enrollStudentCourseAttachmentViewModel);
        EnrollStudentCourseAttachmentViewModel GetEnrollStudentCourseAttachmentByEnrollStudentCourseId(int enrollStudentCourseId);

    }
}
