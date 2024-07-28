using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISubjectService
    {
        IPagedList<Subject> GetSubjects(int? type,int? examTypeId, string searchText, int? page, int languageId, int pagination);
        Subject GetSubjectById(int id);
        Subject GetSubjectById(int id, int languageId);
        void AddSubject(SubjectViewModel subjectViewModel);
        void EditSubject(SubjectViewModel subjectViewModel, Subject subject);
        void DeleteSubject(Subject subject);
    }
}
