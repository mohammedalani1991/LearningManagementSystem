using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class TrainerService : ITrainerService
    {
        private readonly LearningManagementSystemContext _context;

        public TrainerService(LearningManagementSystemContext context)
        {
            _context = context;
        }

        public Trainer GetTrainerById(int id)
        {
            var r = _context.Trainers.Find(id);
            return r;
        }

        public Trainer GetTrainerByCourseExamId(int courseExamId)
        {
            var course = _context.EnrollCourseExams.Include(r => r.EnrollTeacherCourse).FirstOrDefault(r => r.Id == courseExamId);
            if (course != null)
            {
                var r = _context.Trainers.Find(course.EnrollTeacherCourse.TeacherId);
                return r;
            }
            return null;
        }

        public TrainerViewModel GetTrainerById(int id, int languageId)
        {
            var trainers = _context.Trainers.Include(c => c.Contact).Include(c => c.TrainerTranslations).Include(c => c.Contact.ContactTranslations).FirstOrDefault(r => r.Id == id);

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = trainers.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                var transTr = trainers.TrainerTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                {
                    trainers.Contact.FullName = trans.FullName;
                    trainers.Contact.FirstName = trans.FirstName;
                    trainers.Contact.SecondName = trans.SecondName;
                    trainers.Contact.ThirdName = trans.ThirdName;
                    trainers.Contact.LastName = trans.LastName;

                }
                if (transTr != null)
                {
                    trainers.Description = transTr.Description;

                }
            }

            return new TrainerViewModel(trainers);
        }

        public TrainerViewModel GetTrainerByContactId(int ContactId)
        {
            var trainers = _context.Trainers.Include(c => c.Contact).FirstOrDefault(r => r.ContactId == ContactId && r.Status != (int)GeneralEnums.StatusEnum.Deleted);

            return new TrainerViewModel(trainers);
        }

        public List<TrainerViewModel> GetTrainers(int languageId)
        {
            var trainers = _context.Trainers.Include(c => c.TrainerTranslations).Include(c => c.Contact).Include(c => c.Contact.ContactTranslations).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted).ToList();

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var trainer in trainers)
                {
                    var trans = trainer.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var transTr = trainer.TrainerTranslations.FirstOrDefault(r => r.LanguageId == languageId);

                    if (trans != null)
                    {
                        trainer.Contact.FullName = trans.FullName;
                        trainer.Contact.FirstName = trans.FirstName;
                        trainer.Contact.SecondName = trans.SecondName;
                        trainer.Contact.ThirdName = trans.ThirdName;
                        trainer.Contact.LastName = trans.LastName;

                    }

                    if (transTr != null)
                    {
                        trainer.Description = transTr.Description;
                    }
                }
            }

            return trainers.Select(s => new TrainerViewModel(s)).ToList();
        }


        public Trainer GetActiveTrainerById(int id, int languageId)
        {
            var trainer = _context.Trainers.Include(c => c.TrainerTranslations).Include(c => c.Contact.ContactTranslations).FirstOrDefault(r => r.Id == id && r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                var trans = trainer.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                var transTr = trainer.TrainerTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                if (trans != null)
                {
                    trainer.Contact.FullName = trans.FullName;
                    trainer.Contact.FirstName = trans.FirstName;
                    trainer.Contact.SecondName = trans.SecondName;
                    trainer.Contact.ThirdName = trans.ThirdName;
                    trainer.Contact.LastName = trans.LastName;
                }
                if (transTr != null)
                {
                    trainer.Description = transTr.Description;
                }
            }

            return trainer;
        }


        public IPagedList<Trainer> GetTrainers(string searchText, int? page, int languageId, int pagination)
        {
            var trainers = _context.Trainers.Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted)
                .Include(c => c.TrainerTranslations).Include(c => c.Contact.ContactTranslations).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
                trainers = trainers.Where(r => (r.Contact.FullName.Replace(" ", "").Contains(searchText.Replace(" ", "")) ||
                r.Contact.ContactTranslations.FirstOrDefault().FullName.Replace(" ", "").Contains(searchText.Replace(" ", ""))));

            int pageSize = pagination;
            var pageNumber = page ?? 1;

            var result = trainers.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in result)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var transTr = item.TrainerTranslations.FirstOrDefault(r => r.LanguageId == languageId);

                    if (trans != null)
                    {
                        item.Contact.FullName = trans.FullName;
                        item.Contact.FirstName = trans.FirstName;
                        item.Contact.LastName = trans.LastName;
                        item.Contact.ThirdName = trans.ThirdName;
                        item.Contact.SecondName = trans.SecondName;
                    }

                    if (transTr != null)
                    {
                        item.Description = transTr.Description;
                    }
                }
            }
            return result;
        }


        public IPagedList<Trainer> GetActiveTrainers(string searchText, int? specialty, int? page, int languageId, int pagination)
        {
            var trainers = _context.Trainers.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active)
                .Include(c => c.TrainerTranslations).Include(c => c.Contact.ContactTranslations).AsQueryable();

            if (specialty != null && specialty != 0)
                trainers = trainers.Where(a => a.GeneralSpecialtyId == specialty);

            if (!string.IsNullOrWhiteSpace(searchText))
                if (languageId == CultureHelper.GetDefaultLanguageId())
                    trainers = trainers.Where(r => r.Contact.FullName.Contains(searchText));
                else
                    trainers = trainers.Where(r => r.Contact.ContactTranslations.FirstOrDefault(s=>s.LanguageId == languageId).FullName.Contains(searchText));


            int pageSize = pagination;
            var pageNumber = page ?? 1;

            var result = trainers.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in result)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var transTr = item.TrainerTranslations.FirstOrDefault(r => r.LanguageId == languageId);

                    if (trans != null)
                    {
                        item.Contact.FullName = trans.FullName;
                        item.Contact.FirstName = trans.FirstName;
                        item.Contact.LastName = trans.LastName;
                        item.Contact.ThirdName = trans.ThirdName;
                        item.Contact.SecondName = trans.SecondName;
                    }

                    if (transTr != null)
                    {
                        item.Description = transTr.Description;
                    }
                }
            }
            return result;
        }


        public List<Trainer> GetActiveTrainers(bool? EnableAndShowInpages, int languageId)
        {
            var trainers = _context.Trainers.Include(c => c.TrainerTranslations).Include(c => c.Contact).Include(c => c.Contact.ContactTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (EnableAndShowInpages != null)
            {
                trainers = trainers.Where(r => r.ShowInPages == true);
            }

            var result = trainers.ToList();

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in result)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var transTr = item.TrainerTranslations.FirstOrDefault(r => r.LanguageId == languageId);

                    if (trans != null)
                    {
                        item.Contact.FullName = trans.FullName;
                        item.Contact.FirstName = trans.FirstName;
                        item.Contact.LastName = trans.LastName;
                        item.Contact.ThirdName = trans.ThirdName;
                        item.Contact.SecondName = trans.SecondName;
                    }

                    if (transTr != null)
                    {
                        item.Description = transTr.Description;
                    }
                }
            }
            return result;
        }

        public async Task<List<Trainer>> GetActiveTrainersForHonePage(bool? EnableAndShowInpages, int languageId)
        {
            var trainers = _context.Trainers.Include(c => c.TrainerTranslations).Include(c => c.Contact).Include(c => c.Contact.ContactTranslations).Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active);

            if (EnableAndShowInpages != null)
            {
                trainers = trainers.Where(r => r.ShowInPages == true);
            }

            var result = await trainers.Take(4).ToListAsync();

            if (languageId != CultureHelper.GetDefaultLanguageId())
            {
                foreach (var item in result)
                {
                    var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    var transTr = item.TrainerTranslations.FirstOrDefault(r => r.LanguageId == languageId);

                    if (trans != null)
                    {
                        item.Contact.FullName = trans.FullName;
                        item.Contact.FirstName = trans.FirstName;
                        item.Contact.LastName = trans.LastName;
                        item.Contact.ThirdName = trans.ThirdName;
                        item.Contact.SecondName = trans.SecondName;
                    }

                    if (transTr != null)
                    {
                        item.Description = transTr.Description;
                    }
                }
            }
            return result;
        }
        public int AddTrainer(TrainerViewModel trainerViewModel)
        {
            var reg = new Trainer()
            {
                CreatedBy = trainerViewModel.CreatedBy,
                CreatedOn = DateTime.Now,
                Status = trainerViewModel.Status,
                ContactId = trainerViewModel.ContactId,
                GeneralSpecialtyId = trainerViewModel.GeneralSpecialtyId,
                StartWorkDate = trainerViewModel.StartWorkDate.Value,
                WorkHouers = trainerViewModel.WorkHouers,
                IsUser = trainerViewModel.IsUser,
                IsFullTimeWorker = trainerViewModel.IsFullTimeWorker,
                ImageUrl = trainerViewModel.ImageUrl,
                ShowInPages = trainerViewModel.ShowInPages,
                Signature = trainerViewModel.Signature,
            };

            _context.Trainers.Add(reg);
            _context.SaveChanges();

            if (trainerViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var TrainerTran = new TrainerTranslation()
                {
                    Description = trainerViewModel.Description,
                    LanguageId = trainerViewModel.LanguageId,
                    TrainerId = reg.Id
                };

                _context.TrainerTranslations.Add(TrainerTran);
                _context.SaveChanges();
            }
            return reg.Id;
        }

        public void EditTrainer(TrainerViewModel trainerViewModel, Trainer trainer)
        {
            trainer.Status = trainerViewModel.Status;
            trainer.GeneralSpecialtyId = trainerViewModel.GeneralSpecialtyId;
            trainer.StartWorkDate = trainerViewModel.StartWorkDate.Value;
            trainer.WorkHouers = trainerViewModel.WorkHouers;
            trainer.IsFullTimeWorker = trainerViewModel.IsFullTimeWorker;
            trainer.ShowInPages = trainerViewModel.ShowInPages;
            trainer.ImageUrl = trainerViewModel.ImageUrl;
            trainer.Signature = trainerViewModel.Signature;

            if(trainerViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
            {
                trainer.Description = trainerViewModel.Description;
            }

            _context.Entry(trainer).State = EntityState.Modified;
            _context.SaveChanges();

            if (trainerViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var detailsTran = _context.TrainerTranslations.FirstOrDefault(r =>
                    r.LanguageId == trainerViewModel.LanguageId &&
                    r.TrainerId == trainerViewModel.Id);
                if (detailsTran != null)
                {
                    detailsTran.Description = trainerViewModel.Description;
                    _context.Entry(detailsTran).State = EntityState.Modified;
                }
                else
                {
                    var cityTran = new TrainerTranslation()
                    {
                        Description = trainerViewModel.Description,
                        LanguageId = trainerViewModel.LanguageId,
                        TrainerId = trainerViewModel.Id
                    };
                    _context.TrainerTranslations.Add(cityTran);
                }

                _context.SaveChanges();
            }

        }

        public void UpdateTrainer(TrainerViewModel trainerViewModel)
        {
            var trainer = _context.Trainers.Find(trainerViewModel.Id);
            trainer.UserProfileId = trainerViewModel.UserProfileId;
            _context.Entry(trainer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTrainer(Trainer trainer)
        {
            trainer.Status = (int)GeneralEnums.StatusEnum.Deleted;
            trainer.DeletedOn = DateTime.Now;
            _context.Entry(trainer).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
