using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.General;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class SectionOfCourseQuizService : ISectionOfCourseQuizService
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ISettingService _settingService;

        public SectionOfCourseQuizService(LearningManagementSystemContext context, ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        public SectionOfCourseQuiz GetQuizById(int id)
        {
            return _context.SectionOfCourseQuizzes.FirstOrDefault(x => x.Id == id);
        }

        public IPagedList<SectionOfCourseQuiz> GetQuizzes(int id, int? page, int languageId, int pagination = 25)
        {
            var sectionoffcourses = _context.SectionOfCourseQuizzes.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.SectionOfCourseId == id).Include(r => r.Lecture.LectureTranslations);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = sectionoffcourses;
            var output = result.OrderBy(r => r.Order).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in output)
                {
                    var trans = item.Lecture.LectureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        item.Lecture.LectureName = trans.LectureName;
                        item.Lecture.Description = trans.Description;
                    }
                }

            return output;
        }

        public void RefetchQuizzes(int id, string createdBy)
        {
            var sectionOfCourse = _context.SectionOfCourses.Include(r => r.Lectures).FirstOrDefault(r => r.Id == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            if (Boolean.Parse(_settingService.GetOrCreate("HardDelete_Quiz_After_Refetch_ByAdmin", "False").Value))
            {
                var courseQuizQuiz = _context.SectionOfCourseQuizzes.Where(r => r.SectionOfCourseId == id);
                foreach (var item in courseQuizQuiz)
                {
                    item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    _context.Entry(item).State = EntityState.Modified;
                }

                _context.SaveChanges();
            }

            foreach (var item in sectionOfCourse.Lectures.OrderBy(r=>r.Order))
            {
                if (item.Status != (int)GeneralEnums.StatusEnum.Deleted)
                {
                    var sectionOfCourseQuiz = _context.SectionOfCourseQuizzes.FirstOrDefault(r => r.LectureId == item.Id && r.SectionOfCourseId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                    if (sectionOfCourseQuiz == null)
                        _context.SectionOfCourseQuizzes.Add(new SectionOfCourseQuiz()
                        {
                            CreatedBy = createdBy,
                            CreatedOn = DateTime.Now,
                            Status = (int)GeneralEnums.StatusEnum.Active,
                            SectionOfCourseId = id,
                            LectureId = item.Id,
                            QuestionOne = true,
                            QuestionTwo = true,
                            QuestionThree = true,
                            Order = item.Order,
                        });
                    else
                    {
                        sectionOfCourseQuiz.Order = item.Order;
                        _context.Entry(sectionOfCourseQuiz).State = EntityState.Modified;
                    }
                }
                else
                {
                    var sectionOfCourseQuiz = _context.SectionOfCourseQuizzes.FirstOrDefault(r => r.LectureId == item.Id && r.SectionOfCourseId == id);
                    if (sectionOfCourseQuiz != null)
                        if (Boolean.Parse(_settingService.GetOrCreate("Delete_Quiz_After_Refetch", "True").Value))
                        {
                            sectionOfCourseQuiz.Status = (int)GeneralEnums.StatusEnum.Deleted;
                            _context.Entry(sectionOfCourseQuiz).State = EntityState.Modified;
                        }
                }
            }
            _context.SaveChanges();
        }

        public void RefetchCourseQuizzes(int id, string createdBy)
        {
            var sectionsOfCourse = _context.SectionOfCourses.Where(r => r.CourseId == id).Include(r => r.Lectures);
            foreach (var sectionOfCourse in sectionsOfCourse)
                foreach (var item in sectionOfCourse.Lectures.OrderBy(r => r.Order))
                {
                    if (sectionOfCourse.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        var sectionOfCourseQuiz = _context.SectionOfCourseQuizzes.FirstOrDefault(r => r.LectureId == item.Id && r.SectionOfCourseId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                        if (sectionOfCourseQuiz == null)
                            _context.SectionOfCourseQuizzes.Add(new SectionOfCourseQuiz()
                            {
                                CreatedBy = createdBy,
                                CreatedOn = DateTime.Now,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                SectionOfCourseId = id,
                                LectureId = item.Id,
                                QuestionOne = true,
                                QuestionTwo = true,
                                QuestionThree = true,
                                Order = item.Order,
                            });
                        else
                        {
                            sectionOfCourseQuiz.Order = item.Order;
                            _context.Entry(sectionOfCourseQuiz).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        var sectionOfCourseQuiz = _context.SectionOfCourseQuizzes.FirstOrDefault(r => r.LectureId == item.Id && r.SectionOfCourseId == sectionOfCourse.Id);
                        if (sectionOfCourseQuiz != null)
                            if (Boolean.Parse(_settingService.GetOrCreate("Delete_Quiz_After_Refetch", "True").Value))
                            {
                                sectionOfCourseQuiz.Status = (int)GeneralEnums.StatusEnum.Deleted;
                                _context.Entry(sectionOfCourseQuiz).State = EntityState.Modified;
                            }
                    }
                }
            _context.SaveChanges();
        }

        public void EditQuiz(SectionOfCourseQuiz quiz, int num, bool isTrue)
        {
            if (num == 1)
                quiz.QuestionOne = isTrue;
            if (num == 2)
                quiz.QuestionTwo = isTrue;
            if (num == 3)
                quiz.QuestionThree = isTrue;

            _context.Entry(quiz).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteQuizzesBySectionID(int id)
        {
            var quizzes = _context.SectionOfCourseQuizzes.Where(r => r.SectionOfCourseId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
            foreach (var item in quizzes)
            {
                item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}
