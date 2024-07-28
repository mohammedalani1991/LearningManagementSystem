using DataEntity.Models.EfModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.ControlPanel.IServices
{
    public interface IMarkAdoptionService
    {
        List<MarkAdoptionForExam> GetMarkAdoptionForExam(int semesterId, int courseId);
        void AdoptMarkAdoptionForExam(int semesterId, int courseId, int examTemplateId, bool adopted);
        List<MarkAdoptionForPracticalExam> GetMarkAdoptionForPracticalExam(int semesterId, int courseId);
        void AdoptPracticalExam(int semesterId, int courseId, int practicalExamId, bool adopted);
    }
}
