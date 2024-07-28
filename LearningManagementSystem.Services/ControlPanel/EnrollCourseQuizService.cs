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
    public class EnrollCourseQuizService : IEnrollCourseQuizService
    {
        private readonly LearningManagementSystemContext _context;
        private readonly ISettingService _settingService;

        public EnrollCourseQuizService(LearningManagementSystemContext context, ISettingService settingService)
        {
            _context = context;
            _settingService = settingService;
        }

        public EnrollCourseQuiz GetQuizById(int id)
        {
            return _context.EnrollCourseQuizzes.FirstOrDefault(x => x.Id == id);
        }

        public List<EnrollCourseQuizPointe> GetQuizPointsByQuizId(int id)
        {
            return _context.EnrollCourseQuizPointes.Where(x => x.EnrollCourseQuizId == id).Include(r => r.EnrollStudentCourse).ToList();
        }

        public IPagedList<EnrollCourseQuiz> GetQuizzes(int id, int? page, int languageId, int pagination = 25)
        {
            var enrollCourseQuiz = _context.EnrollCourseQuizzes.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted && r.EnrollTeacherCourseId == id).Include(r => r.Lecture.LectureTranslations).Where(r=>r.Lecture.Status != (int)GeneralEnums.StatusEnum.Deleted);

            var pageSize = pagination;
            var pageNumber = (page ?? 1);
            var result = enrollCourseQuiz;
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
            var courseId = _context.EnrollTeacherCourses.FirstOrDefault(r => r.Id == id)?.CourseId;
            var enrollCourseQuiz = _context.SectionOfCourses.Where(r => r.CourseId == courseId).Include(r => r.SectionOfCourseQuizzes); // Core


            if (Boolean.Parse(_settingService.GetOrCreate("HardDelete_Quiz_After_Refetch_ByAdmin", "False").Value))
            {
                var EnrollCourseQuizQuiz = _context.EnrollCourseQuizzes.Where(r => r.EnrollTeacherCourseId == id);
                foreach (var item in EnrollCourseQuizQuiz)
                {
                    item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    _context.Entry(item).State = EntityState.Modified;
                }

                _context.SaveChanges();
            }


            foreach (var quiz in enrollCourseQuiz) // core
                foreach (var item in quiz.SectionOfCourseQuizzes.OrderBy(r => r.Order))
                {
                    if (item.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        var enrollCourseQuizQuiz = _context.EnrollCourseQuizzes.FirstOrDefault(r => r.LectureId == item.LectureId && r.EnrollTeacherCourseId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                        if (enrollCourseQuizQuiz == null)
                            _context.EnrollCourseQuizzes.Add(new EnrollCourseQuiz()
                            {
                                CreatedBy = createdBy,
                                CreatedOn = DateTime.Now,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                EnrollTeacherCourseId = id,
                                LectureId = item.LectureId,
                                QuestionOne = item.QuestionOne,
                                QuestionTwo = item.QuestionTwo,
                                QuestionThree = item.QuestionThree,
                                Order = item.Order,
                            });
                        else
                        {
                            enrollCourseQuizQuiz.Order = item.Order;
                            _context.Entry(enrollCourseQuizQuiz).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        var EnrollCourseQuizQuiz = _context.EnrollCourseQuizzes.FirstOrDefault(r => r.LectureId == item.LectureId && r.EnrollTeacherCourseId == id);
                        if (EnrollCourseQuizQuiz != null)
                            if (Boolean.Parse(_settingService.GetOrCreate("Delete_Quiz_After_Refetch", "True").Value))
                            {
                                EnrollCourseQuizQuiz.Status = (int)GeneralEnums.StatusEnum.Deleted;
                                _context.Entry(EnrollCourseQuizQuiz).State = EntityState.Modified;
                            }
                    }
                }
            _context.SaveChanges();
        }

        public void RefetchQuizzes_WithoutUsing(int id, string createdBy, LearningManagementSystemContext db)
        {
            var courseId = db.EnrollTeacherCourses.FirstOrDefault(r => r.Id == id)?.CourseId;
            var enrollCourseQuiz = db.SectionOfCourses.Where(r => r.CourseId == courseId).Include(r => r.SectionOfCourseQuizzes);

            if (Boolean.Parse(_settingService.GetOrCreate("HardDelete_Quiz_After_Refetch_ByAdmin", "False").Value))
            {
                var EnrollCourseQuizQuiz = db.EnrollCourseQuizzes.Where(r => r.EnrollTeacherCourseId == id);
                foreach (var item in EnrollCourseQuizQuiz)
                {
                    item.Status = (int)GeneralEnums.StatusEnum.Deleted;
                    db.Entry(item).State = EntityState.Modified;
                }

                db.SaveChanges();
            }
            foreach (var quiz in enrollCourseQuiz)
                foreach (var item in quiz.SectionOfCourseQuizzes.OrderBy(r => r.Order))
                {
                    if (item.Status != (int)GeneralEnums.StatusEnum.Deleted)
                    {
                        var enrollCourseQuizQuiz = db.EnrollCourseQuizzes.FirstOrDefault(r => r.LectureId == item.LectureId && r.EnrollTeacherCourseId == id && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                        if (enrollCourseQuizQuiz == null)
                            db.EnrollCourseQuizzes.Add(new EnrollCourseQuiz()
                            {
                                CreatedBy = createdBy,
                                CreatedOn = DateTime.Now,
                                Status = (int)GeneralEnums.StatusEnum.Active,
                                EnrollTeacherCourseId = id,
                                LectureId = item.LectureId,
                                QuestionOne = item.QuestionOne,
                                QuestionTwo = item.QuestionTwo,
                                QuestionThree = item.QuestionThree,
                                Order = item.Order,
                            });
                        else
                        {
                            enrollCourseQuizQuiz.Order = item.Order;
                            _context.Entry(enrollCourseQuizQuiz).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        var EnrollCourseQuizQuiz = db.EnrollCourseQuizzes.FirstOrDefault(r => r.LectureId == item.LectureId && r.EnrollTeacherCourseId == id);
                        if (EnrollCourseQuizQuiz != null)
                            if (Boolean.Parse(_settingService.GetOrCreate("Delete_Quiz_After_Refetch", "True").Value))
                            {
                                EnrollCourseQuizQuiz.Status = (int)GeneralEnums.StatusEnum.Deleted;
                                db.Entry(EnrollCourseQuizQuiz).State = EntityState.Modified;
                            }
                    }
                }
            db.SaveChanges();
        }

        public void EditQuiz(EnrollCourseQuiz quiz, int num, bool isTrue)
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

        public List<EnrollCourseQuiz> GetStudentQuizMarks(int enrollStudentCourseId, int enrollTeacherCourseId , int languageId)
        {
            var quizzes = _context.EnrollCourseQuizzes.Include(r => r.EnrollCourseQuizPointes.Where(s => s.EnrollStudentCourseId == enrollStudentCourseId))
                .Where(r => r.EnrollTeacherCourseId == enrollTeacherCourseId && r.EnrollCourseQuizPointes.Any() && r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                .Include(r => r.Lecture.LectureTranslations).OrderBy(r => r.Order);
            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in quizzes)
                {
                    var trans = item.Lecture.LectureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        item.Lecture.LectureName = trans.LectureName;
                        item.Lecture.Description = trans.Description;
                    }
                }

            return quizzes.ToList();
        }

        public List<EnrollCourseQuiz> GetAllStudentQuizMarks(int enrollStudentCourseId, int enrollTeacherCourseId, int languageId)
        {
            var quizzes = _context.EnrollCourseQuizzes.Include(r => r.EnrollCourseQuizPointes.Where(s => s.EnrollStudentCourseId == enrollStudentCourseId))
                .Where(r => r.EnrollTeacherCourseId == enrollTeacherCourseId && r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                .Include(r => r.Lecture.LectureTranslations).Where(r=>r.Lecture.Status != (int)GeneralEnums.StatusEnum.Deleted).OrderBy(r => r.Order);

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in quizzes)
                {
                    var trans = item.Lecture.LectureTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (trans != null)
                    {
                        item.Lecture.LectureName = trans.LectureName;
                        item.Lecture.Description = trans.Description;
                    }
                }
            return quizzes.ToList();
        }

        public void AddQuizPoint(decimal? value, int quizId, int enrollStudentCourseId, int num, string createdBy)
        {
            var point = _context.EnrollCourseQuizPointes.FirstOrDefault(r => r.EnrollCourseQuizId == quizId && r.EnrollStudentCourseId == enrollStudentCourseId);
            if (point != null)
            {
                if (num == 1)
                    point.QuestionOne = value;
                else if (num == 2)
                    point.QuestionTwo = value;
                else point.QuestionThree = value;

                _context.Entry(point).State = EntityState.Modified;
            }
            else
                _context.EnrollCourseQuizPointes.Add(new EnrollCourseQuizPointe()
                {
                    CreatedBy = createdBy,
                    CreatedOn = DateTime.Now,
                    EnrollCourseQuizId = quizId,
                    EnrollStudentCourseId = enrollStudentCourseId,
                    Status = (int)GeneralEnums.StatusEnum.Active,
                    QuestionOne = num == 1 ? value : -1,
                    QuestionTwo = num == 2 ? value : -1,
                    QuestionThree = num == 3 ? value : -1
                });

            _context.SaveChanges();
        }

        public int GetFailuresCount(int id)
        {
            var QuizLowPoint = decimal.Parse(_settingService.GetOrCreate("Quiz_Low_Point", "7").Value);
            var count1 = _context.EnrollCourseQuizPointes.Where(r => r.EnrollStudentCourseId == id && r.QuestionOne < QuizLowPoint && r.QuestionOne >= 0)
                .Include(r => r.EnrollCourseQuiz).Where(r => r.EnrollCourseQuiz.Status != (int)GeneralEnums.StatusEnum.Deleted).Count();
            var count2 = _context.EnrollCourseQuizPointes.Where(r => r.EnrollStudentCourseId == id && r.QuestionTwo < QuizLowPoint && r.QuestionTwo >= 0)
               .Include(r => r.EnrollCourseQuiz).Where(r => r.EnrollCourseQuiz.Status != (int)GeneralEnums.StatusEnum.Deleted).Count();
            var count3 = _context.EnrollCourseQuizPointes.Where(r => r.EnrollStudentCourseId == id && r.QuestionThree < QuizLowPoint && r.QuestionThree >= 0)
               .Include(r => r.EnrollCourseQuiz).Where(r => r.EnrollCourseQuiz.Status != (int)GeneralEnums.StatusEnum.Deleted).Count();

            return count1 + count2 + count3;
        }
    }
}
