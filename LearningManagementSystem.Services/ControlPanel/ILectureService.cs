using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ILectureService
    {
        IPagedList<Lecture> GetLectures(string searchText, int? page, int languageId, int pagination);
        Lecture GetLectureById(int id);
        Lecture GetLectureById_WithoutUsing(int id, LearningManagementSystemContext db);
        LectureViewModel GetLectureById(int id, int languageId);
        List<Lecture> GetLectureBySectionId(int SectionId, int languageId);
        List<Lecture> GetLectureBySectionId(int SectionId);
        List<Lecture> GetLectureBySectionId_WithoutUsing(int SectionId, LearningManagementSystemContext db);
        void AddLecture(LectureViewModel lecture);
        void AddLecture_WithoutUsing(LectureViewModel lectureViewModel, LearningManagementSystemContext db);
        void EditLecture(LectureViewModel lectureViewModel, Lecture lecture);
        void EditLecture_WithoutUsing(LectureViewModel lectureViewModel, Lecture lecture, LearningManagementSystemContext db);
        void DeleteLecture(Lecture lecture);
        void DeleteLecture_WithoutUsing(Lecture lecture, LearningManagementSystemContext db);
        void DeleteLectureBySectionID(int SectionID,bool WithoutUsing = false, LearningManagementSystemContext context = null);
    }
}
