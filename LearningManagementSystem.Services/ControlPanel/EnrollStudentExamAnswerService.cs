using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class EnrollStudentExamAnswerService : IEnrollStudentExamAnswerService
    {
        public void AddEnrollStudentAnswerExam(List<EnrollStudentExamAnswerViewModel> enrollStudentExamAnswerViewModelList, LearningManagementSystemContext db)
        {
           
                foreach (var item in enrollStudentExamAnswerViewModelList)
                {
                    var enrollStudentExamAnswer = new EnrollStudentExamAnswer()
                    {
                        CreatedOn = item.CreatedOn,
                        Status = item.Status,
                        CreatedBy = item.CreatedBy,
                        EnrollStudentExamId = item.EnrollStudentExamId,
                        Mark = item.Mark,
                        EnrollCourseExamQuestionId  = item.EnrollCourseExamQuestionId,
                        Answer = item.Answer,
                        IsCorrect=item.IsCorrect
                    };

                    db.EnrollStudentExamAnswers.Add(enrollStudentExamAnswer);
                    db.SaveChanges();

                    if (item.EnrollStudentExamAnswerOptions != null && item.EnrollStudentExamAnswerOptions.Count > 0)
                    {
                        foreach (var op in item.EnrollStudentExamAnswerOptions)
                        {
                            db.EnrollStudentExamAnswerOptions.Add(new EnrollStudentExamAnswerOption()
                            {
                                EnrollStudentExamAnswerId = enrollStudentExamAnswer.Id,
                                QusetionOptionId = op.QusetionOptionId,
                                CreatedBy = op.CreatedBy,
                                CreatedOn = op.CreatedOn,
                                Status = op.Status
                            });
                            db.SaveChanges();
                        }
                    
                    }
                }

        }
        public void EditEnrollStudentExamAnswer(List<EnrollStudentExamAnswerViewModel> enrollStudentExamAnswerViewModelList, LearningManagementSystemContext db)
        {

            foreach (var item in enrollStudentExamAnswerViewModelList)
            {
                var CurrentenrollStudentExamAnswer = GetEnrollStudentExamAnswer(item.EnrollCourseExamQuestionId, item.EnrollStudentExamId, db);
                CurrentenrollStudentExamAnswer.Mark = item.Mark;
                CurrentenrollStudentExamAnswer.IsCorrect = item.IsCorrect;
                db.Entry(CurrentenrollStudentExamAnswer).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        public List<EnrollStudentExamAnswer> GetListEnrollStudentExamAnswer(int EnrollStudentExamID)
        {

            using (var db = new LearningManagementSystemContext())
            {
                var EnrollStudentExamAnswer = db.EnrollStudentExamAnswers.Include(s => s.EnrollStudentExamAnswerOptions).Where(r =>  r.EnrollStudentExamId == EnrollStudentExamID && r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();
                return EnrollStudentExamAnswer;
            }

        }

            public EnrollStudentExamAnswer GetEnrollStudentExamAnswer(int EnrollCourseExamQuestionId ,int EnrollStudentExamID, LearningManagementSystemContext db)
        {
            

                var EnrollStudentExamAnswer = db.EnrollStudentExamAnswers.FirstOrDefault(r => r.EnrollCourseExamQuestionId == EnrollCourseExamQuestionId && r.EnrollStudentExamId== EnrollStudentExamID && r.Status != (int)GeneralEnums.StatusEnum.Deleted);
                return EnrollStudentExamAnswer;
           
        }

    }
}
