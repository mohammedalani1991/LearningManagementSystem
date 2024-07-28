using System;
using System.Collections.Generic;
using System.Linq;
using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using LearningManagementSystem.Core.SystemEnums;
using LearningManagementSystem.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
    public class StudentService : IStudentService
    {
        private readonly IContactsService _contactsService;
        private readonly LearningManagementSystemContext _context;

        public StudentService(IContactsService contactsService, LearningManagementSystemContext context)
        {
            _contactsService = contactsService;
            _context = context;
        }

        public async void AddStudent(StudentViewModel studentViewModel)
        {
            studentViewModel.Address = string.IsNullOrWhiteSpace(studentViewModel.Address) ? "" : studentViewModel.Address;
            var student = new Student()
            {
                CreatedBy = studentViewModel.CreatedBy,
                CreatedOn = DateTime.Now,
                Status = studentViewModel.Status,
                Address = studentViewModel.Address ?? "",
                BirthDate = studentViewModel.BirthDate,
                BirthPlace = studentViewModel.BirthPlace,
                Country = studentViewModel.Country ?? "",
                Email = studentViewModel.Email,
                EducationalLevelId = studentViewModel.EducationalLevelId??0,
                ExtraMobile = studentViewModel.ExtraMobile,
                Work = studentViewModel.Work ?? "",
                WorkPlace = studentViewModel.WorkPlace,
                ContactId = studentViewModel.ContactId,
                IsExternalStudy = studentViewModel.IsExternalStudy,
                IsFastSubscription = studentViewModel.isFastSubscription,
                IsMedicalPast = studentViewModel.IsMedicalPast,
                MedicalDescription = studentViewModel.Note,
                CollegeId = studentViewModel.CollegeId ?? 0,
                SpecialtyId = studentViewModel.SpecialtyId ?? 0,

                TrainingConsultantId = studentViewModel.TrainingConsultantId,
            };

            _context.Students.Add(student);

            var contact = _context.Contacts.Find(studentViewModel.ContactId);
            if (!string.IsNullOrEmpty(studentViewModel.Email))
                contact.Email = studentViewModel.Email;
            if (!string.IsNullOrEmpty(studentViewModel.Mobile))
                contact.Mobile = studentViewModel.Mobile;
            if (!string.IsNullOrEmpty(studentViewModel.PhoneNumberCode))
                contact.PhoneNumberCode = studentViewModel.PhoneNumberCode;

            _context.Entry(contact).State = EntityState.Modified;
            _context.SaveChanges();

            if (studentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
            {
                var studentTran = new StudentTranslation()
                {
                    LanguageId = studentViewModel.LanguageId.Value,
                    Address = studentViewModel.Address,
                    BirthPlace = studentViewModel.BirthPlace,
                    Country = studentViewModel.Country,
                    StudentId = student.Id,
                    Work = studentViewModel.Work,
                    WorkPlace = studentViewModel.WorkPlace
                };

                _context.StudentTranslations.Add(studentTran);
                _context.SaveChanges();
            }
        }

        public void EditStudent(StudentViewModel studentViewModel, Student student)
        {
            using (var db = new LearningManagementSystemContext())
            {
                student.BirthDate = studentViewModel.BirthDate;
                student.Email = studentViewModel.Email;
                student.EducationalLevelId = studentViewModel.EducationalLevelId.Value;
                student.ExtraMobile = studentViewModel.ExtraMobile;

                if (studentViewModel.LanguageId == CultureHelper.GetDefaultLanguageId())
                {
                    student.BirthPlace = studentViewModel.BirthPlace;
                }

                db.Entry(student).State = EntityState.Modified;
                db.Entry(student.Contact).State = EntityState.Modified;
                db.SaveChanges();

                if (studentViewModel.LanguageId != CultureHelper.GetDefaultLanguageId())
                {
                    var studentTranslation = db.StudentTranslations.Include(r => r.Student.Contact).FirstOrDefault(r =>
                        r.LanguageId == studentViewModel.LanguageId &&
                        r.StudentId == student.Id);
                    if (studentTranslation != null)
                    {
                        studentTranslation.BirthPlace = studentViewModel.BirthPlace;

                        db.Entry(studentTranslation).State = EntityState.Modified;
                    }
                    else
                    {
                        var studentTran = new StudentTranslation()
                        {
                            LanguageId = studentViewModel.LanguageId.Value,
                            BirthPlace = studentViewModel.BirthPlace,
                            StudentId = student.Id,
                        };

                        db.StudentTranslations.Add(studentTran);
                    }
                }

                db.SaveChanges();
            }
        }

        public IPagedList<Student> GetStudents(FilterViewModel filter, int? companyId, int? page, int languageId, int pagination, int? employeeId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var students = db.Students.Include(a => a.Contact.ContactTranslations)
                        .Include(a => a.StudentTranslations).Where(r => r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (employeeId != null)
                    students = students.Where(r => r.TrainingConsultantId == employeeId && r.TrainingConsultantId != 0);


                if (!string.IsNullOrWhiteSpace(filter.SearchText))
                    students = students.Where(r => r.Email.Contains(filter.SearchText) || r.Address.Contains(filter.SearchText)
                    || r.Country.Contains(filter.SearchText));

                if (filter.Gender > 0)
                {
                    students = students.Where(a => a.Contact.GenderId == filter.Gender);
                }

                if (filter.FromDate != null)
                {
                    students = students.Where(a => a.CreatedOn >= filter.FromDate);
                }

                if (filter.ToDate != null)
                {
                    students = students.Where(a => a.CreatedOn <= filter.ToDate);
                }

                var result = students;
                int pageSize = pagination;
                var pageNumber = page ?? 1;

                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    foreach (var item in output)
                    {
                        var trans = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        var trans1 = item.StudentTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (trans != null)
                            item.Contact.FullName = trans.FullName;
                        if (trans1 != null)
                            item.Address = trans1.Address;
                    }
                }
                return output;
            }
        }

        public StudentViewModel GetStudentById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var studentTran =
                        db.StudentTranslations.Include(r => r.Student).FirstOrDefault(r => r.LanguageId == languageId && r.StudentId == id);

                    if (studentTran != null)
                    {
                        var contactTran = db.ContactTranslations.Include(r => r.Contact).FirstOrDefault(c => c.LanguageId == languageId && c.ContactId == studentTran.Student.ContactId);
                        if (contactTran != null)
                        {
                            return new StudentViewModel(studentTran, contactTran);
                        }
                    }
                }

                var student = db.Students.Include(s => s.Contact).First(s => s.Id == id);

                return new StudentViewModel(student, student.Contact);
            }
        }

        public Student GetStudentModalById(int id, int languageId)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var student = db.Students.Include(c => c.Contact.ContactTranslations).Include(r => r.EnrollStudentCourses).FirstOrDefault(c => c.Id == id);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                {
                    var tran = student.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (tran != null)
                        student.Contact.FullName = tran.FullName;
                }

                return student;
            }
        }

        public Student GetStudentById(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                return db.Students.Include(c => c.Contact).First(c => c.Id == id);
            }
        }

        public IPagedList<Student> GetStudentsForAssign(string searchText, int? EnrollTeacherCourseID, int? page, int languageId, int pagination)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var Students = db.Students.Include(s => s.Contact).ThenInclude(s => s.ContactTranslations).Where(r =>
                      r.Status != (int)GeneralEnums.StatusEnum.Deleted);

                if (EnrollTeacherCourseID != null && EnrollTeacherCourseID > 0)
                {
                    List<int> StudentIds = db.EnrollStudentCourses.Where(r => r.CourseId == EnrollTeacherCourseID && r.Status != (int)GeneralEnums.StatusEnum.Deleted).Select(r => r.StudentId).ToList();
                    if (StudentIds.Count > 0)
                        Students = Students.Where(r => !StudentIds.Contains(r.Id));
                }

                if (!string.IsNullOrWhiteSpace(searchText))
                    if (languageId == CultureHelper.GetDefaultLanguageId())
                        Students = Students.Where(r => r.Contact.FullName.Contains(searchText));
                    else
                        Students = Students.Where(r => r.Contact.ContactTranslations.Any(t => t.FullName.Contains(searchText) & t.LanguageId == languageId));


                var pageSize = pagination;
                var pageNumber = (page ?? 1);
                var result = Students;
                var output = result.OrderByDescending(r => r.Id).ToPagedList(pageNumber, pageSize);
                if (languageId != CultureHelper.GetDefaultLanguageId())
                    foreach (var item in output)
                    {
                        var transContact = item.Contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                        if (transContact != null)
                            item.Contact.FullName = transContact.FullName;
                    }

                return output;
            }
        }

        public void DeleteStudent(int id)
        {
            using (var db = new LearningManagementSystemContext())
            {
                var Student = db.Students.Find(id);
                Student.Status = (int)GeneralEnums.StatusEnum.Deleted;
                db.Entry(Student).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        public void EditStudentAttendance(StudentAttendanceViewModel studentAttendanceViewModel, StudentAttendance student)
        {
            student.IsPresent = studentAttendanceViewModel.IsPresent;
            student.AbsenceNote = studentAttendanceViewModel.AbsenceNote ?? student.AbsenceNote;

            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Student GetStudentByContactId(int ContactId)
        {
            var Students = _context.Students.Where(r => r.ContactId == ContactId && r.Status != (int)GeneralEnums.StatusEnum.Deleted).FirstOrDefault();
            return Students;
        }

        public List<EnrollStudentCourse> GetStudentCertificates(int studentId, int languageId)
        {
            var courses = _context.EnrollStudentCourses.Where(r => r.StudentId == studentId && r.IsPass == true && r.Status == (int)GeneralEnums.StatusEnum.Active)
                .Include(r => r.Course.Course.CourseTranslations).Include(r => r.Course.Course.CourseCertifications).Where(r => r.Course.CertificateAdoption == true).AsQueryable();

            foreach (var item in courses)
                foreach (var item1 in item.Course.Course.CourseCertifications)
                {
                    var templateHtmls = _context.TemplateHtmls.FirstOrDefault(r => r.Id == item1.TemplateHtmlId && r.Status == (int)GeneralEnums.StatusEnum.Active);
                    if (templateHtmls != null)
                        item1.CreatedBy = templateHtmls.Name;
                }

            if (languageId != CultureHelper.GetDefaultLanguageId())
                foreach (var item in courses)
                {
                    var transcourse = item.Course.Course.CourseTranslations.FirstOrDefault(r => r.LanguageId == languageId);
                    if (transcourse != null)
                        item.Course.Course.CourseName = transcourse.CourseName;

                }

            return courses.ToList();
        }

        public void ChangeBalance(Student student, decimal amount)
        {
            student.Balance = (student.Balance ?? 0) + amount;
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public string GetStudentAttendence(int studentId)
        {
            var studentCourse = _context.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.StudentId == studentId).Include(r => r.CourseAttendances);
            int num1 = 0;
            int num2 = 0;
            foreach (var item in studentCourse)
                foreach (var attendence in item.CourseAttendances)
                    if (attendence.IsPresent == true)
                        num1++;
                    else num2++;

            return $"{num1} / {num2 + num1}";
        }

        public decimal GetPaymentAmount(int studentId)
        {
            var studentCourse = _context.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.StudentId == studentId).Include(r => r.Course.Course);
            decimal amount = 0;
            foreach (var item in studentCourse)
                amount = amount + item.Course.Course.CoursePrice ?? 0;

            return amount;
        }

        public int GetCourseCount(int studentId)
        {
            return _context.EnrollStudentCourses.Where(r => r.Status == (int)GeneralEnums.StatusEnum.Active && r.StudentId == studentId).Count();
        }
    }
}
