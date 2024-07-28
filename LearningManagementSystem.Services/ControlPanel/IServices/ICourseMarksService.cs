using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningManagementSystem.Services.ControlPanel.IServices
{
    public interface ICourseMarksService
    {
        CourseMark GetCourseMarkById(int id); 
        CourseMark GetCourseMarkById(int id, int languageId);
        List<CourseMark> GetCourseMarks(int courseId ,int languageId);
        void AddCourseMark(CourseMarkViewModel courseMarkViewModel);
        void EditCourseMark(CourseMarkViewModel courseMarkViewModel, CourseMark courseMark);
        void DeleteCourseMark(CourseMark course);
    }
}
