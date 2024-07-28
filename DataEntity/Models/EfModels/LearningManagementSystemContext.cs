using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class LearningManagementSystemContext : DbContext
    {
        public LearningManagementSystemContext()
        {
        }

        public LearningManagementSystemContext(DbContextOptions<LearningManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutDic> AboutDics { get; set; }
        public virtual DbSet<AboutDicTranslation> AboutDicTranslations { get; set; }
        public virtual DbSet<AcademicSupervisionRate> AcademicSupervisionRates { get; set; }
        public virtual DbSet<AcademicSupervisionStandard> AcademicSupervisionStandards { get; set; }
        public virtual DbSet<AcademicSupervisionStandardTranslation> AcademicSupervisionStandardTranslations { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentTranslation> AssignmentTranslations { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchTranslation> BranchTranslations { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<CalendarTranslation> CalendarTranslations { get; set; }
        public virtual DbSet<CertificateAdoption> CertificateAdoptions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityTranslation> CityTranslations { get; set; }
        public virtual DbSet<CmsCatery> CmsCateries { get; set; }
        public virtual DbSet<CmsCateryTranslation> CmsCateryTranslations { get; set; }
        public virtual DbSet<CmsPage> CmsPages { get; set; }
        public virtual DbSet<CmsPageTranslation> CmsPageTranslations { get; set; }
        public virtual DbSet<CmsProject> CmsProjects { get; set; }
        public virtual DbSet<CmsProjectCategory> CmsProjectCategories { get; set; }
        public virtual DbSet<CmsProjectCategoryTranslation> CmsProjectCategoryTranslations { get; set; }
        public virtual DbSet<CmsProjectCost> CmsProjectCosts { get; set; }
        public virtual DbSet<CmsProjectCostTranslation> CmsProjectCostTranslations { get; set; }
        public virtual DbSet<CmsProjectDonor> CmsProjectDonors { get; set; }
        public virtual DbSet<CmsProjectResource> CmsProjectResources { get; set; }
        public virtual DbSet<CmsProjectTranslation> CmsProjectTranslations { get; set; }
        public virtual DbSet<CmsSlider> CmsSliders { get; set; }
        public virtual DbSet<CmsSliderTranslation> CmsSliderTranslations { get; set; }
        public virtual DbSet<CmsWhatPeopleSay> CmsWhatPeopleSays { get; set; }
        public virtual DbSet<CmsWhatPeopleSayTranslation> CmsWhatPeopleSayTranslations { get; set; }
        public virtual DbSet<CommunicationChannel> CommunicationChannels { get; set; }
        public virtual DbSet<CommunicationChannelTranslation> CommunicationChannelTranslations { get; set; }
        public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactTranslation> ContactTranslations { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CountryTranslation> CountryTranslations { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseAttendance> CourseAttendances { get; set; }
        public virtual DbSet<CourseCategory> CourseCategories { get; set; }
        public virtual DbSet<CourseCategoryTranslation> CourseCategoryTranslations { get; set; }
        public virtual DbSet<CourseCertification> CourseCertifications { get; set; }
        public virtual DbSet<CourseMark> CourseMarks { get; set; }
        public virtual DbSet<CourseMarkTranslation> CourseMarkTranslations { get; set; }
        public virtual DbSet<CoursePackage> CoursePackages { get; set; }
        public virtual DbSet<CoursePackagesRelation> CoursePackagesRelations { get; set; }
        public virtual DbSet<CoursePakagesTranslation> CoursePakagesTranslations { get; set; }
        public virtual DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
        public virtual DbSet<CourseResource> CourseResources { get; set; }
        public virtual DbSet<CourseResourceTranslation> CourseResourceTranslations { get; set; }
        public virtual DbSet<CourseTranslation> CourseTranslations { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CurrencyTranslation> CurrencyTranslations { get; set; }
        public virtual DbSet<DataBaseScript> DataBaseScripts { get; set; }
        public virtual DbSet<DetailsLookup> DetailsLookups { get; set; }
        public virtual DbSet<DetailsLookupTranslation> DetailsLookupTranslations { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeTranslation> EmployeeTranslations { get; set; }
        public virtual DbSet<EnrollAssignment> EnrollAssignments { get; set; }
        public virtual DbSet<EnrollAssignmentTranslation> EnrollAssignmentTranslations { get; set; }
        public virtual DbSet<EnrollCourseAllowUserRate> EnrollCourseAllowUserRates { get; set; }
        public virtual DbSet<EnrollCourseAssigment> EnrollCourseAssigments { get; set; }
        public virtual DbSet<EnrollCourseAssigmentQuestion> EnrollCourseAssigmentQuestions { get; set; }
        public virtual DbSet<EnrollCourseAssigmentQuestionOption> EnrollCourseAssigmentQuestionOptions { get; set; }
        public virtual DbSet<EnrollCourseAssigmentQuestionOptionTranslation> EnrollCourseAssigmentQuestionOptionTranslations { get; set; }
        public virtual DbSet<EnrollCourseAssigmentQuestionTranslation> EnrollCourseAssigmentQuestionTranslations { get; set; }
        public virtual DbSet<EnrollCourseAssigmentTranslation> EnrollCourseAssigmentTranslations { get; set; }
        public virtual DbSet<EnrollCourseExam> EnrollCourseExams { get; set; }
        public virtual DbSet<EnrollCourseExamQuestion> EnrollCourseExamQuestions { get; set; }
        public virtual DbSet<EnrollCourseExamTranslation> EnrollCourseExamTranslations { get; set; }
        public virtual DbSet<EnrollCourseQuiz> EnrollCourseQuizzes { get; set; }
        public virtual DbSet<EnrollCourseQuizPointe> EnrollCourseQuizPointes { get; set; }
        public virtual DbSet<EnrollCourseResource> EnrollCourseResources { get; set; }
        public virtual DbSet<EnrollCourseResourceTranslation> EnrollCourseResourceTranslations { get; set; }
        public virtual DbSet<EnrollCourseTime> EnrollCourseTimes { get; set; }
        public virtual DbSet<EnrollCourseTimeCustomization> EnrollCourseTimeCustomizations { get; set; }
        public virtual DbSet<EnrollLecture> EnrollLectures { get; set; }
        public virtual DbSet<EnrollLectureTranslation> EnrollLectureTranslations { get; set; }
        public virtual DbSet<EnrollSectionOfCourse> EnrollSectionOfCourses { get; set; }
        public virtual DbSet<EnrollSectionOfCourseTranslation> EnrollSectionOfCourseTranslations { get; set; }
        public virtual DbSet<EnrollStudentAlert> EnrollStudentAlerts { get; set; }
        public virtual DbSet<EnrollStudentAssigment> EnrollStudentAssigments { get; set; }
        public virtual DbSet<EnrollStudentAssigmentAnswer> EnrollStudentAssigmentAnswers { get; set; }
        public virtual DbSet<EnrollStudentAssigmentAnswerOption> EnrollStudentAssigmentAnswerOptions { get; set; }
        public virtual DbSet<EnrollStudentCourse> EnrollStudentCourses { get; set; }
        public virtual DbSet<EnrollStudentCourseAttachment> EnrollStudentCourseAttachments { get; set; }
        public virtual DbSet<EnrollStudentExam> EnrollStudentExams { get; set; }
        public virtual DbSet<EnrollStudentExamAnswer> EnrollStudentExamAnswers { get; set; }
        public virtual DbSet<EnrollStudentExamAnswerOption> EnrollStudentExamAnswerOptions { get; set; }
        public virtual DbSet<EnrollTeacherCourse> EnrollTeacherCourses { get; set; }
        public virtual DbSet<EnrollTeacherCourseTranlation> EnrollTeacherCourseTranlations { get; set; }
        public virtual DbSet<ExamQuestion> ExamQuestions { get; set; }
        public virtual DbSet<ExamTemplate> ExamTemplates { get; set; }
        public virtual DbSet<ExamTemplateTranslation> ExamTemplateTranslations { get; set; }
        public virtual DbSet<Expulsion> Expulsions { get; set; }
        public virtual DbSet<Generalization> Generalizations { get; set; }
        public virtual DbSet<GeneralizationEmployee> GeneralizationEmployees { get; set; }
        public virtual DbSet<GeneralizationTranslation> GeneralizationTranslations { get; set; }
        public virtual DbSet<InvoicesPay> InvoicesPays { get; set; }
        public virtual DbSet<ItemFile> ItemFiles { get; set; }
        public virtual DbSet<Lecture> Lectures { get; set; }
        public virtual DbSet<LectureTranslation> LectureTranslations { get; set; }
        public virtual DbSet<ManagementRate> ManagementRates { get; set; }
        public virtual DbSet<ManagementRateLine> ManagementRateLines { get; set; }
        public virtual DbSet<ManagementStandard> ManagementStandards { get; set; }
        public virtual DbSet<ManagementStandardTranslation> ManagementStandardTranslations { get; set; }
        public virtual DbSet<MarkAdoptionForExam> MarkAdoptionForExams { get; set; }
        public virtual DbSet<MarkAdoptionForPracticalExam> MarkAdoptionForPracticalExams { get; set; }
        public virtual DbSet<MasterLookup> MasterLookups { get; set; }
        public virtual DbSet<MasterLookupTranslation> MasterLookupTranslations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModuleTranslation> ModuleTranslations { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionTranslation> PermissionTranslations { get; set; }
        public virtual DbSet<PracticalEnrollmentExam> PracticalEnrollmentExams { get; set; }
        public virtual DbSet<PracticalEnrollmentExamStudent> PracticalEnrollmentExamStudents { get; set; }
        public virtual DbSet<PracticalEnrollmentExamStudentSubject> PracticalEnrollmentExamStudentSubjects { get; set; }
        public virtual DbSet<PracticalEnrollmentExamStudentSubjectRating> PracticalEnrollmentExamStudentSubjectRatings { get; set; }
        public virtual DbSet<PracticalEnrollmentExamTrainer> PracticalEnrollmentExamTrainers { get; set; }
        public virtual DbSet<PracticalExam> PracticalExams { get; set; }
        public virtual DbSet<PracticalExamCourse> PracticalExamCourses { get; set; }
        public virtual DbSet<PracticalExamCourseSubject> PracticalExamCourseSubjects { get; set; }
        public virtual DbSet<PracticalExamQuestion> PracticalExamQuestions { get; set; }
        public virtual DbSet<PracticalExamTranslation> PracticalExamTranslations { get; set; }
        public virtual DbSet<PracticalQuestion> PracticalQuestions { get; set; }
        public virtual DbSet<PracticalQuestionTranslation> PracticalQuestionTranslations { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionOption> QuestionOptions { get; set; }
        public virtual DbSet<QuestionOptionTranslation> QuestionOptionTranslations { get; set; }
        public virtual DbSet<QuestionTranslation> QuestionTranslations { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SectionOfCourse> SectionOfCourses { get; set; }
        public virtual DbSet<SectionOfCourseQuiz> SectionOfCourseQuizzes { get; set; }
        public virtual DbSet<SectionOfCourseTranslation> SectionOfCourseTranslations { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<SemesterTranslation> SemesterTranslations { get; set; }
        public virtual DbSet<SenangPay> SenangPays { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }
        public virtual DbSet<StudentBalanceHistory> StudentBalanceHistories { get; set; }
        public virtual DbSet<StudentExpulsionHistory> StudentExpulsionHistories { get; set; }
        public virtual DbSet<StudentNote> StudentNotes { get; set; }
        public virtual DbSet<StudentNotesTranslation> StudentNotesTranslations { get; set; }
        public virtual DbSet<StudentSubscription> StudentSubscriptions { get; set; }
        public virtual DbSet<StudentTranslation> StudentTranslations { get; set; }
        public virtual DbSet<SubCommunicationChannel> SubCommunicationChannels { get; set; }
        public virtual DbSet<SubCommunicationChannelTranslation> SubCommunicationChannelTranslations { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectTranslation> SubjectTranslations { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<SuperAdmin> SuperAdmins { get; set; }
        public virtual DbSet<SuperAdminSetting> SuperAdminSettings { get; set; }
        public virtual DbSet<SystemFile> SystemFiles { get; set; }
        public virtual DbSet<SystemFileTranslation> SystemFileTranslations { get; set; }
        public virtual DbSet<SystemGroup> SystemGroups { get; set; }
        public virtual DbSet<SystemGroupTranslation> SystemGroupTranslations { get; set; }
        public virtual DbSet<SystemGroupUser> SystemGroupUsers { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<SystemSettingTranslation> SystemSettingTranslations { get; set; }
        public virtual DbSet<TeacherAttendance> TeacherAttendances { get; set; }
        public virtual DbSet<TempTable1> TempTable1s { get; set; }
        public virtual DbSet<TemplateHtml> TemplateHtmls { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<TrainerRateMeasure> TrainerRateMeasures { get; set; }
        public virtual DbSet<TrainerRateMeasureTranslation> TrainerRateMeasureTranslations { get; set; }
        public virtual DbSet<TrainerTranslation> TrainerTranslations { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserProfileTranslation> UserProfileTranslations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                                                                                                 .AddJsonFile("appsettings.json")
                                                                                                                 .AddEnvironmentVariables();
                var configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AboutDic>(entity =>
            {
                entity.ToTable("AboutDic");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.GroupName).HasMaxLength(300);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<AboutDicTranslation>(entity =>
            {
                entity.ToTable("AboutDicTranslation");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.AboutDic)
                    .WithMany(p => p.AboutDicTranslations)
                    .HasForeignKey(d => d.AboutDicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AboutDicTranslation_AboutDicTranslation");
            });

            modelBuilder.Entity<AcademicSupervisionRate>(entity =>
            {
                entity.ToTable("AcademicSupervisionRate");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.AcademicSupervisionRates)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_AcademicSupervisionRate_EnrollTeacherCourse");

                entity.HasOne(d => d.Standard)
                    .WithMany(p => p.AcademicSupervisionRates)
                    .HasForeignKey(d => d.StandardId)
                    .HasConstraintName("FK_AcademicSupervisionRate_AcademicSupervisionStandard");
            });

            modelBuilder.Entity<AcademicSupervisionStandard>(entity =>
            {
                entity.ToTable("AcademicSupervisionStandard");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AcademicSupervisionStandardTranslation>(entity =>
            {
                entity.ToTable("AcademicSupervisionStandardTranslation");

                entity.Property(e => e.Standard).IsRequired();

                entity.HasOne(d => d.AcademicSupervisionStandard)
                    .WithMany(p => p.AcademicSupervisionStandardTranslations)
                    .HasForeignKey(d => d.AcademicSupervisionStandardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcademicSupervisionStandardTranslation_AcademicSupervisionStandard");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignment");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_Course");
            });

            modelBuilder.Entity<AssignmentTranslation>(entity =>
            {
                entity.ToTable("AssignmentTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Assignment)
                    .WithMany(p => p.AssignmentTranslations)
                    .HasForeignKey(d => d.AssignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssignmentTranslation_Assignment");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.AbsenceNote).HasMaxLength(500);

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_Contact");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<BranchTranslation>(entity =>
            {
                entity.ToTable("BranchTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchTranslations)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchTranslation_Branch");
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.ToTable("Calendar");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CalendarTranslation>(entity =>
            {
                entity.ToTable("CalendarTranslation");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.CalendarTranslations)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CalendarTranslation_Calendar");
            });

            modelBuilder.Entity<CertificateAdoption>(entity =>
            {
                entity.ToTable("CertificateAdoption");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CertificateAdoptions)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CertificateAdoption_Course");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.CertificateAdoptions)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_CertificateAdoption_Semester");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<CityTranslation>(entity =>
            {
                entity.ToTable("CityTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CityTranslations)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CityTranslation_City");
            });

            modelBuilder.Entity<CmsCatery>(entity =>
            {
                entity.ToTable("CmsCatery");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_CmsCatery_CmsCatery");
            });

            modelBuilder.Entity<CmsCateryTranslation>(entity =>
            {
                entity.ToTable("CmsCateryTranslation");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Catery)
                    .WithMany(p => p.CmsCateryTranslations)
                    .HasForeignKey(d => d.CateryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CmsCateryTranslation_CmsCatery");
            });

            modelBuilder.Entity<CmsPage>(entity =>
            {
                entity.ToTable("CmsPage");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.HasOne(d => d.Catery)
                    .WithMany(p => p.CmsPages)
                    .HasForeignKey(d => d.CateryId)
                    .HasConstraintName("FK_CmsPage_CmsCatery");
            });

            modelBuilder.Entity<CmsPageTranslation>(entity =>
            {
                entity.ToTable("CmsPageTranslation");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.CmsPageTranslations)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CmsPageTranslation_CmsPage");
            });

            modelBuilder.Entity<CmsProject>(entity =>
            {
                entity.ToTable("CmsProject");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.HasOne(d => d.ProjectCategory)
                    .WithMany(p => p.CmsProjects)
                    .HasForeignKey(d => d.ProjectCategoryId)
                    .HasConstraintName("FK_CmsProject_CmsProjectCategory");
            });

            modelBuilder.Entity<CmsProjectCategory>(entity =>
            {
                entity.ToTable("CmsProjectCategory");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CmsProjectCategoryTranslation>(entity =>
            {
                entity.ToTable("CmsProjectCategoryTranslation");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.ProjectCategory)
                    .WithMany(p => p.CmsProjectCategoryTranslations)
                    .HasForeignKey(d => d.ProjectCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CmsProjectCategoryTranslation_CmsProjectCategory");
            });

            modelBuilder.Entity<CmsProjectCost>(entity =>
            {
                entity.ToTable("CmsProjectCost");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CmsProjectCosts)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_CmsProjectCost_CmsProject");
            });

            modelBuilder.Entity<CmsProjectCostTranslation>(entity =>
            {
                entity.ToTable("CmsProjectCostTranslation");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.ProjectCost)
                    .WithMany(p => p.CmsProjectCostTranslations)
                    .HasForeignKey(d => d.ProjectCostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CmsProjectCostTranslation_CmsProjectCost");
            });

            modelBuilder.Entity<CmsProjectDonor>(entity =>
            {
                entity.ToTable("CmsProjectDonor");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.ProjectCost)
                    .WithMany(p => p.CmsProjectDonors)
                    .HasForeignKey(d => d.ProjectCostId)
                    .HasConstraintName("FK_CmsProjectDonor_CmsProjectCost");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CmsProjectDonors)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_CmsProjectDonor_CmsProject");
            });

            modelBuilder.Entity<CmsProjectResource>(entity =>
            {
                entity.ToTable("CmsProjectResource");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CmsProjectResources)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_CmsProjectResource_CmsProject");
            });

            modelBuilder.Entity<CmsProjectTranslation>(entity =>
            {
                entity.ToTable("CmsProjectTranslation");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CmsProjectTranslations)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CmsProjectTranslation_CmsProject");
            });

            modelBuilder.Entity<CmsSlider>(entity =>
            {
                entity.ToTable("CmsSlider");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ReadMoreLink).IsUnicode(false);
            });

            modelBuilder.Entity<CmsSliderTranslation>(entity =>
            {
                entity.ToTable("CmsSliderTranslation");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Slider)
                    .WithMany(p => p.CmsSliderTranslations)
                    .HasForeignKey(d => d.SliderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CmsSliderTranslation_CmsSlider");
            });

            modelBuilder.Entity<CmsWhatPeopleSay>(entity =>
            {
                entity.ToTable("CmsWhatPeopleSay");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.PersonName).IsRequired();
            });

            modelBuilder.Entity<CmsWhatPeopleSayTranslation>(entity =>
            {
                entity.ToTable("CmsWhatPeopleSayTranslation");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.PersonName).IsRequired();

                entity.HasOne(d => d.People)
                    .WithMany(p => p.CmsWhatPeopleSayTranslations)
                    .HasForeignKey(d => d.PeopleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CmsWhatPeopleSayTranslation_CmsWhatPeopleSay");
            });

            modelBuilder.Entity<CommunicationChannel>(entity =>
            {
                entity.ToTable("CommunicationChannel");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note).HasMaxLength(500);
            });

            modelBuilder.Entity<CommunicationChannelTranslation>(entity =>
            {
                entity.ToTable("CommunicationChannelTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.CommunicationChannel)
                    .WithMany(p => p.CommunicationChannelTranslations)
                    .HasForeignKey(d => d.CommunicationChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommunicationChannelTranslation_CommunicationChannel");
            });

            modelBuilder.Entity<CommunicationLog>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.TypeText).HasMaxLength(300);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.CommunicationLogs)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommunicationLogs_Contact");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.IdNumber).HasMaxLength(200);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(100);

                entity.Property(e => e.PhoneNumberCode).HasMaxLength(256);

                entity.Property(e => e.SecondName).HasMaxLength(100);

                entity.Property(e => e.ThirdName).HasMaxLength(100);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Contact_Branch");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Contact_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Contact_Country");
            });

            modelBuilder.Entity<ContactTranslation>(entity =>
            {
                entity.ToTable("ContactTranslation");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.SecondName).HasMaxLength(100);

                entity.Property(e => e.ThirdName).HasMaxLength(100);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactTranslations)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactTranslation_Contact");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("ContactType");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactTypes)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactType_Contact");
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Subject).IsRequired();
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryCode).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<CountryTranslation>(entity =>
            {
                entity.ToTable("CountryTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryTranslations)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryTranslation_Country");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.AssignmentMark).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CoursePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ListeningExamMark).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.SuccessMark).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Course_CourseCategory");
            });

            modelBuilder.Entity<CourseAttendance>(entity =>
            {
                entity.ToTable("CourseAttendance");

                entity.Property(e => e.AbsenceNote)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.CourseAttendances)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .HasConstraintName("FK_CourseAttendance_EnrollStudentCourse");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.CourseAttendances)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_CourseAttendance_EnrollTeacherCourse");
            });

            modelBuilder.Entity<CourseCategory>(entity =>
            {
                entity.ToTable("CourseCategory");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_CourseCategory_CourseCategory");
            });

            modelBuilder.Entity<CourseCategoryTranslation>(entity =>
            {
                entity.ToTable("CourseCategoryTranslation");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CourseCategoryTranslations)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseCategoryTranslation_CourseCategory");
            });

            modelBuilder.Entity<CourseCertification>(entity =>
            {
                entity.ToTable("CourseCertification");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseCertifications)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseCertification_Course");

                entity.HasOne(d => d.TemplateHtml)
                    .WithMany(p => p.CourseCertifications)
                    .HasForeignKey(d => d.TemplateHtmlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseCertification_TemplateHtml");
            });

            modelBuilder.Entity<CourseMark>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.Property(e => e.Value).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.ValueTo).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseMarks)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CourseMarks_Course");
            });

            modelBuilder.Entity<CourseMarkTranslation>(entity =>
            {
                entity.ToTable("CourseMarkTranslation");

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.CourseMark)
                    .WithMany(p => p.CourseMarkTranslations)
                    .HasForeignKey(d => d.CourseMarkId)
                    .HasConstraintName("FK_CourseMarkTranslation_CourseMarks");
            });

            modelBuilder.Entity<CoursePackage>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CoursePackagesRelation>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CoursePackagesRelations)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoursePackagesRelations_Course");

                entity.HasOne(d => d.CoursePackages)
                    .WithMany(p => p.CoursePackagesRelations)
                    .HasForeignKey(d => d.CoursePackagesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoursePackagesRelations_CoursePackages");
            });

            modelBuilder.Entity<CoursePakagesTranslation>(entity =>
            {
                entity.ToTable("CoursePakagesTranslation");

                entity.Property(e => e.PackageName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.CoursePackages)
                    .WithMany(p => p.CoursePakagesTranslations)
                    .HasForeignKey(d => d.CoursePackagesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoursePakagesTranslation_CoursePackages");
            });

            modelBuilder.Entity<CoursePrerequisite>(entity =>
            {
                entity.ToTable("CoursePrerequisite");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CoursePrerequisiteCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrerequisiteCourse_Course");

                entity.HasOne(d => d.PrerequisiteCourse)
                    .WithMany(p => p.CoursePrerequisitePrerequisiteCourses)
                    .HasForeignKey(d => d.PrerequisiteCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrerequisiteCourse_Course1");
            });

            modelBuilder.Entity<CourseResource>(entity =>
            {
                entity.ToTable("CourseResource");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseResources)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseResource_Course");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.CourseResources)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseResource_Lecture");
            });

            modelBuilder.Entity<CourseResourceTranslation>(entity =>
            {
                entity.ToTable("CourseResourceTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.CourseResource)
                    .WithMany(p => p.CourseResourceTranslations)
                    .HasForeignKey(d => d.CourseResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseResourceTranslation_CourseResource");
            });

            modelBuilder.Entity<CourseTranslation>(entity =>
            {
                entity.ToTable("CourseTranslation");

                entity.Property(e => e.CourseName).HasMaxLength(200);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseTranslations)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseTranslation_Course");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Code).HasMaxLength(500);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Icon).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<CurrencyTranslation>(entity =>
            {
                entity.ToTable("CurrencyTranslation");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.CurrencyTranslations)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrencyTranslation_Currency");
            });

            modelBuilder.Entity<DataBaseScript>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<DetailsLookup>(entity =>
            {
                entity.ToTable("DetailsLookup");

                entity.Property(e => e.Code).HasMaxLength(300);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Value).HasMaxLength(200);

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DetailsLookups)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailsLookup_MasterLookup");
            });

            modelBuilder.Entity<DetailsLookupTranslation>(entity =>
            {
                entity.ToTable("DetailsLookupTranslation");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.DetailsLookup)
                    .WithMany(p => p.DetailsLookupTranslations)
                    .HasForeignKey(d => d.DetailsLookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailsLookupTranslation_DetailsLookup");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("Email");

                entity.Property(e => e.Body).HasMaxLength(1000);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CycleLink)
                    .HasMaxLength(1000)
                    .HasColumnName("cycleLink");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email1)
                    .HasMaxLength(300)
                    .HasColumnName("Email");

                entity.Property(e => e.ImageLink).HasMaxLength(1000);

                entity.Property(e => e.Subject).HasMaxLength(256);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Email_Contact");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.StartWorkDate).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Contact");
            });

            modelBuilder.Entity<EmployeeTranslation>(entity =>
            {
                entity.ToTable("EmployeeTranslation");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTranslations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTranslation_Employee");
            });

            modelBuilder.Entity<EnrollAssignment>(entity =>
            {
                entity.ToTable("EnrollAssignment");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollCourse)
                    .WithMany(p => p.EnrollAssignments)
                    .HasForeignKey(d => d.EnrollCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollAssignment_EnrollCourseExam");
            });

            modelBuilder.Entity<EnrollAssignmentTranslation>(entity =>
            {
                entity.ToTable("EnrollAssignmentTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.EnrollAssignment)
                    .WithMany(p => p.EnrollAssignmentTranslations)
                    .HasForeignKey(d => d.EnrollAssignmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollAssignmentTranslation_EnrollAssignment");
            });

            modelBuilder.Entity<EnrollCourseAllowUserRate>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.EnrollCourseAllowUserRates)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseAllowUserRates_Contact");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.EnrollCourseAllowUserRates)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_EnrollCourseAllowUserRates_EnrollTeacherCourse");
            });

            modelBuilder.Entity<EnrollCourseAssigment>(entity =>
            {
                entity.ToTable("EnrollCourseAssigment");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.EnrollCourseAssigments)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseAssigment_EnrollTeacherCourse");
            });

            modelBuilder.Entity<EnrollCourseAssigmentQuestion>(entity =>
            {
                entity.ToTable("EnrollCourseAssigmentQuestion");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.QuestionName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollCourseAssigment)
                    .WithMany(p => p.EnrollCourseAssigmentQuestions)
                    .HasForeignKey(d => d.EnrollCourseAssigmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseAssigmentQuestion_EnrollCourseAssigment");
            });

            modelBuilder.Entity<EnrollCourseAssigmentQuestionOption>(entity =>
            {
                entity.ToTable("EnrollCourseAssigmentQuestionOption");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.EnrollCourseAssigmentQuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_EnrollCourseAssigmentQuestionOption_EnrollCourseAssigmentQuestion");
            });

            modelBuilder.Entity<EnrollCourseAssigmentQuestionOptionTranslation>(entity =>
            {
                entity.ToTable("EnrollCourseAssigmentQuestionOptionTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.EnrollCourseAssigmentQuestionOptionTranslations)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_EnrollCourseAssigmentQuestionOptionTranslation_EnrollCourseAssigmentQuestionOption");
            });

            modelBuilder.Entity<EnrollCourseAssigmentQuestionTranslation>(entity =>
            {
                entity.ToTable("EnrollCourseAssigmentQuestionTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.EnrollCourseAssigmentQuestionTranslations)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseAssigmentQuestionTranslation_EnrollCourseAssigmentQuestion");
            });

            modelBuilder.Entity<EnrollCourseAssigmentTranslation>(entity =>
            {
                entity.ToTable("EnrollCourseAssigmentTranslation");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.EnrollCourseAssigment)
                    .WithMany(p => p.EnrollCourseAssigmentTranslations)
                    .HasForeignKey(d => d.EnrollCourseAssigmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseAssigmentTranslation_EnrollCourseAssigment");
            });

            modelBuilder.Entity<EnrollCourseExam>(entity =>
            {
                entity.ToTable("EnrollCourseExam");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EnrollLectureId).HasColumnName("EnrollLectureID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.TestTypeId).HasColumnName("TestTypeID");

                entity.HasOne(d => d.EnrollLecture)
                    .WithMany(p => p.EnrollCourseExams)
                    .HasForeignKey(d => d.EnrollLectureId)
                    .HasConstraintName("FK_EnrollCourseExam_EnrollLecture");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.EnrollCourseExams)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseExam_EnrollTeacherCourse");

                entity.HasOne(d => d.ExamTemplate)
                    .WithMany(p => p.EnrollCourseExams)
                    .HasForeignKey(d => d.ExamTemplateId)
                    .HasConstraintName("FK_EnrollCourseExam_ExamTemplate");
            });

            modelBuilder.Entity<EnrollCourseExamQuestion>(entity =>
            {
                entity.ToTable("EnrollCourseExamQuestion");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollCourseExam)
                    .WithMany(p => p.EnrollCourseExamQuestions)
                    .HasForeignKey(d => d.EnrollCourseExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseExamQuestion_EnrollCourseExam");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.EnrollCourseExamQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseExamQuestion_ExamQuestion");
            });

            modelBuilder.Entity<EnrollCourseExamTranslation>(entity =>
            {
                entity.ToTable("EnrollCourseExamTranslation");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.EnrollCourseExam)
                    .WithMany(p => p.EnrollCourseExamTranslations)
                    .HasForeignKey(d => d.EnrollCourseExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseExamTranslation_EnrollCourseExam");
            });

            modelBuilder.Entity<EnrollCourseQuiz>(entity =>
            {
                entity.ToTable("EnrollCourseQuiz");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.EnrollCourseQuizzes)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_EnrollCourseQuiz_EnrollTeacherCourse");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.EnrollCourseQuizzes)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_EnrollCourseQuiz_Lecture");
            });

            modelBuilder.Entity<EnrollCourseQuizPointe>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.QuestionOne).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.QuestionThree).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.QuestionTwo).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.EnrollCourseQuiz)
                    .WithMany(p => p.EnrollCourseQuizPointes)
                    .HasForeignKey(d => d.EnrollCourseQuizId)
                    .HasConstraintName("FK_EnrollCourseQuizPointes_EnrollCourseQuiz");

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.EnrollCourseQuizPointes)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .HasConstraintName("FK_EnrollCourseQuizPointes_EnrollStudentCourse");
            });

            modelBuilder.Entity<EnrollCourseResource>(entity =>
            {
                entity.ToTable("EnrollCourseResource");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollCourse)
                    .WithMany(p => p.EnrollCourseResources)
                    .HasForeignKey(d => d.EnrollCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseResource_EnrollTeacherCourse");

                entity.HasOne(d => d.EnrollLecture)
                    .WithMany(p => p.EnrollCourseResources)
                    .HasForeignKey(d => d.EnrollLectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseResource_EnrollLecture");
            });

            modelBuilder.Entity<EnrollCourseResourceTranslation>(entity =>
            {
                entity.ToTable("EnrollCourseResourceTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollCourseResource)
                    .WithMany(p => p.EnrollCourseResourceTranslations)
                    .HasForeignKey(d => d.EnrollCourseResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseResourceTranslation_EnrollCourseResource");
            });

            modelBuilder.Entity<EnrollCourseTime>(entity =>
            {
                entity.ToTable("EnrollCourseTime");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollCourse)
                    .WithMany(p => p.EnrollCourseTimes)
                    .HasForeignKey(d => d.EnrollCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseTime_EnrollTeacherCourse");
            });

            modelBuilder.Entity<EnrollCourseTimeCustomization>(entity =>
            {
                entity.ToTable("EnrollCourseTimeCustomization");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollCourse)
                    .WithMany(p => p.EnrollCourseTimeCustomizations)
                    .HasForeignKey(d => d.EnrollCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollCourseTimeCustomization_EnrollTeacherCourse");
            });

            modelBuilder.Entity<EnrollLecture>(entity =>
            {
                entity.ToTable("EnrollLecture");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.LectureName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollCourse)
                    .WithMany(p => p.EnrollLectures)
                    .HasForeignKey(d => d.EnrollCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollLecture_EnrollTeacherCourse");

                entity.HasOne(d => d.EnrollSection)
                    .WithMany(p => p.EnrollLectures)
                    .HasForeignKey(d => d.EnrollSectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollLecture_EnrollSectionOfCourse");
            });

            modelBuilder.Entity<EnrollLectureTranslation>(entity =>
            {
                entity.ToTable("EnrollLectureTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.LectureName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollLecture)
                    .WithMany(p => p.EnrollLectureTranslations)
                    .HasForeignKey(d => d.EnrollLectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollLectureTranslation_EnrollLecture");
            });

            modelBuilder.Entity<EnrollSectionOfCourse>(entity =>
            {
                entity.ToTable("EnrollSectionOfCourse");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollCourse)
                    .WithMany(p => p.EnrollSectionOfCourses)
                    .HasForeignKey(d => d.EnrollCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollSectionOfCourse_EnrollTeacherCourse");
            });

            modelBuilder.Entity<EnrollSectionOfCourseTranslation>(entity =>
            {
                entity.ToTable("EnrollSectionOfCourseTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.EnrollSection)
                    .WithMany(p => p.EnrollSectionOfCourseTranslations)
                    .HasForeignKey(d => d.EnrollSectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollSectionOfCourseTranslation_EnrollSectionOfCourse");
            });

            modelBuilder.Entity<EnrollStudentAlert>(entity =>
            {
                entity.ToTable("EnrollStudentAlert");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.EnrollStudentAlerts)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .HasConstraintName("FK_EnrollStudentAlert_EnrollStudentCourse");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.EnrollStudentAlerts)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_EnrollStudentAlert_EnrollTeacherCourse");
            });

            modelBuilder.Entity<EnrollStudentAssigment>(entity =>
            {
                entity.ToTable("EnrollStudentAssigment");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollCourseAssigment)
                    .WithMany(p => p.EnrollStudentAssigments)
                    .HasForeignKey(d => d.EnrollCourseAssigmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentAssigment_EnrollCourseAssigment");

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.EnrollStudentAssigments)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentAssigment_EnrollStudentCourse");
            });

            modelBuilder.Entity<EnrollStudentAssigmentAnswer>(entity =>
            {
                entity.ToTable("EnrollStudentAssigmentAnswer");

                entity.Property(e => e.Answer).HasMaxLength(500);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollCourseAssigmentQuestion)
                    .WithMany(p => p.EnrollStudentAssigmentAnswers)
                    .HasForeignKey(d => d.EnrollCourseAssigmentQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentAssigmentAnswer_EnrollCourseAssigmentQuestion");

                entity.HasOne(d => d.EnrollStudentAssigment)
                    .WithMany(p => p.EnrollStudentAssigmentAnswers)
                    .HasForeignKey(d => d.EnrollStudentAssigmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentAssigmentAnswer_EnrollStudentAssigment");
            });

            modelBuilder.Entity<EnrollStudentAssigmentAnswerOption>(entity =>
            {
                entity.ToTable("EnrollStudentAssigmentAnswerOption");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollStudentAssigmentAnswer)
                    .WithMany(p => p.EnrollStudentAssigmentAnswerOptions)
                    .HasForeignKey(d => d.EnrollStudentAssigmentAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentAssigmentAnswerOption_EnrollStudentAssigmentAnswer");
            });

            modelBuilder.Entity<EnrollStudentCourse>(entity =>
            {
                entity.ToTable("EnrollStudentCourse");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CustomerCurrencyCode).HasMaxLength(250);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpelledDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.EnrollStudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentCourse_EnrollTeacherCourse");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.EnrollStudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentCourse_Student");
            });

            modelBuilder.Entity<EnrollStudentCourseAttachment>(entity =>
            {
                entity.ToTable("EnrollStudentCourseAttachment");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileAttached).HasMaxLength(500);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.EnrollStudentCourseAttachments)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .HasConstraintName("FK_EnrollStudentCourseAttachment_EnrollStudentCourse");
            });

            modelBuilder.Entity<EnrollStudentExam>(entity =>
            {
                entity.ToTable("EnrollStudentExam");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollCourseExam)
                    .WithMany(p => p.EnrollStudentExams)
                    .HasForeignKey(d => d.EnrollCourseExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentExam_EnrollCourseExam");

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.EnrollStudentExams)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentExam_Student");
            });

            modelBuilder.Entity<EnrollStudentExamAnswer>(entity =>
            {
                entity.ToTable("EnrollStudentExamAnswer");

                entity.Property(e => e.Answer).HasMaxLength(500);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollCourseExamQuestion)
                    .WithMany(p => p.EnrollStudentExamAnswers)
                    .HasForeignKey(d => d.EnrollCourseExamQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentExamQuestion_ExamQuestion");

                entity.HasOne(d => d.EnrollStudentExam)
                    .WithMany(p => p.EnrollStudentExamAnswers)
                    .HasForeignKey(d => d.EnrollStudentExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentExamQuestion_EnrollStudentExam");
            });

            modelBuilder.Entity<EnrollStudentExamAnswerOption>(entity =>
            {
                entity.ToTable("EnrollStudentExamAnswerOption");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollStudentExamAnswer)
                    .WithMany(p => p.EnrollStudentExamAnswerOptions)
                    .HasForeignKey(d => d.EnrollStudentExamAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentExamAnswerOption_EnrollStudentExamQuestion");

                entity.HasOne(d => d.QusetionOption)
                    .WithMany(p => p.EnrollStudentExamAnswerOptions)
                    .HasForeignKey(d => d.QusetionOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollStudentExamAnswerOption_QuestionOption");
            });

            modelBuilder.Entity<EnrollTeacherCourse>(entity =>
            {
                entity.ToTable("EnrollTeacherCourse");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.PublicationDate).HasColumnType("date");

                entity.Property(e => e.PublicationEndDate).HasColumnType("date");

                entity.Property(e => e.SectionName).HasMaxLength(200);

                entity.Property(e => e.WorkEndDate).HasColumnType("date");

                entity.Property(e => e.WorkStartDate).HasColumnType("date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.EnrollTeacherCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollTeacherCourse_Course");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.EnrollTeacherCourses)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_EnrollTeacherCourse_Semester");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.EnrollTeacherCourses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollTeacherCourse_Trainer");
            });

            modelBuilder.Entity<EnrollTeacherCourseTranlation>(entity =>
            {
                entity.ToTable("EnrollTeacherCourseTranlation");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SectionName).HasMaxLength(200);

                entity.HasOne(d => d.EnrollCourse)
                    .WithMany(p => p.EnrollTeacherCourseTranlations)
                    .HasForeignKey(d => d.EnrollCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollTeacherCourseTranlation_EnrollTeacherCourse");
            });

            modelBuilder.Entity<ExamQuestion>(entity =>
            {
                entity.ToTable("ExamQuestion");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ExamQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamQuestion_Question");

                entity.HasOne(d => d.Template)
                    .WithMany(p => p.ExamQuestions)
                    .HasForeignKey(d => d.TemplateId)
                    .HasConstraintName("FK_ExamQuestion_ExamTemplate");
            });

            modelBuilder.Entity<ExamTemplate>(entity =>
            {
                entity.ToTable("ExamTemplate");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ExamTemplates)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ExamTemplate_CourseCategory");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ExamTemplates)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_ExamTemplate_Course");
            });

            modelBuilder.Entity<ExamTemplateTranslation>(entity =>
            {
                entity.ToTable("ExamTemplateTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamTemplateTranslations)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamTemplateTranslation_ExamTemplate");
            });

            modelBuilder.Entity<Expulsion>(entity =>
            {
                entity.ToTable("Expulsion");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpelledFrom).HasColumnType("datetime");

                entity.Property(e => e.ExpelledTo).HasColumnType("datetime");

                entity.Property(e => e.ExpulsionEnd).HasColumnType("datetime");

                entity.Property(e => e.ExpulsionStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<Generalization>(entity =>
            {
                entity.ToTable("Generalization");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Generalizations)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Generalization_Branch");
            });

            modelBuilder.Entity<GeneralizationEmployee>(entity =>
            {
                entity.ToTable("GeneralizationEmployee");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.GeneralizationEmployees)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_GeneralizationEmployee_Contact");

                entity.HasOne(d => d.Generalization)
                    .WithMany(p => p.GeneralizationEmployees)
                    .HasForeignKey(d => d.GeneralizationId)
                    .HasConstraintName("FK_GeneralizationEmployee_Generalization");
            });

            modelBuilder.Entity<GeneralizationTranslation>(entity =>
            {
                entity.ToTable("GeneralizationTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.Generalization)
                    .WithMany(p => p.GeneralizationTranslations)
                    .HasForeignKey(d => d.GeneralizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneralizationTranslation_Generalization");
            });

            modelBuilder.Entity<InvoicesPay>(entity =>
            {
                entity.ToTable("InvoicesPay");

                entity.Property(e => e.AttachmentUrl).IsRequired();

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CustomerCurrencyCode).HasMaxLength(250);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ProcessDate).HasColumnType("datetime");

                entity.Property(e => e.ReceiptNo)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.InvoicesPays)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoicesPay_Contact");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.InvoicesPays)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoicesPay_EnrollTeacherCourse");
            });

            modelBuilder.Entity<ItemFile>(entity =>
            {
                entity.ToTable("ItemFile");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.ItemFiles)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemFile_SystemFile");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.ToTable("Lecture");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.LectureName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lecture_SectionOfCourse");
            });

            modelBuilder.Entity<LectureTranslation>(entity =>
            {
                entity.ToTable("LectureTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.LectureName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.LectureTranslations)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LectureTranslation_Lecture");
            });

            modelBuilder.Entity<ManagementRate>(entity =>
            {
                entity.ToTable("ManagementRate");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Percent).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.ManagementRates)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_ManagementRate_EnrollTeacherCourse");
            });

            modelBuilder.Entity<ManagementRateLine>(entity =>
            {
                entity.ToTable("ManagementRateLine");

                entity.HasOne(d => d.ManagementRate)
                    .WithMany(p => p.ManagementRateLines)
                    .HasForeignKey(d => d.ManagementRateId)
                    .HasConstraintName("FK_ManagementRateLine_ManagementRate");

                entity.HasOne(d => d.Standard)
                    .WithMany(p => p.ManagementRateLines)
                    .HasForeignKey(d => d.StandardId)
                    .HasConstraintName("FK_ManagementRateLine_Standard");
            });

            modelBuilder.Entity<ManagementStandard>(entity =>
            {
                entity.ToTable("ManagementStandard");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ManagementStandardTranslation>(entity =>
            {
                entity.ToTable("ManagementStandardTranslation");

                entity.Property(e => e.Standard).IsRequired();

                entity.HasOne(d => d.ManagementStandard)
                    .WithMany(p => p.ManagementStandardTranslations)
                    .HasForeignKey(d => d.ManagementStandardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagementStandardTranslation_ManagementStandard");
            });

            modelBuilder.Entity<MarkAdoptionForExam>(entity =>
            {
                entity.ToTable("MarkAdoptionForExam");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.MarkAdoptionForExams)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_MarkAdoptionForExam_EnrollTeacherCourse");

                entity.HasOne(d => d.ExamTemplate)
                    .WithMany(p => p.MarkAdoptionForExams)
                    .HasForeignKey(d => d.ExamTemplateId)
                    .HasConstraintName("FK_MarkAdoptionForExam_ExamTemplate");
            });

            modelBuilder.Entity<MarkAdoptionForPracticalExam>(entity =>
            {
                entity.ToTable("MarkAdoptionForPracticalExam");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.MarkAdoptionForPracticalExams)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_MarkAdoptionForPracticalExam_EnrollTeacherCourse");

                entity.HasOne(d => d.PracticalExam)
                    .WithMany(p => p.MarkAdoptionForPracticalExams)
                    .HasForeignKey(d => d.PracticalExamId)
                    .HasConstraintName("FK_MarkAdoptionForPracticalExam_PracticalExam");
            });

            modelBuilder.Entity<MasterLookup>(entity =>
            {
                entity.ToTable("MasterLookup");

                entity.Property(e => e.Code).HasMaxLength(200);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<MasterLookupTranslation>(entity =>
            {
                entity.ToTable("MasterLookupTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.MasterLookup)
                    .WithMany(p => p.MasterLookupTranslations)
                    .HasForeignKey(d => d.MasterLookupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MasterLookupTranslation_MasterLookup");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ExtraMobile).HasMaxLength(50);

                entity.Property(e => e.Message1).HasColumnName("Message");

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.Source).HasMaxLength(250);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Messages_Branch");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.BaseUrl).HasMaxLength(500);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ModuleTranslation>(entity =>
            {
                entity.ToTable("ModuleTranslation");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleTranslations)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleTranslation_Modules");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Notification_Contact");

                entity.HasOne(d => d.Generalization)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.GeneralizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_Generalization");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.PageName).HasMaxLength(300);

                entity.Property(e => e.PageUrl).IsRequired();

                entity.Property(e => e.PermissionKey)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.ModuleId)
                    .HasConstraintName("FK_Permission_Modules");

                entity.HasOne(d => d.SuperAdmin)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.SuperAdminId)
                    .HasConstraintName("FK_Permission_SuperAdmin");
            });

            modelBuilder.Entity<PermissionTranslation>(entity =>
            {
                entity.ToTable("PermissionTranslation");

                entity.Property(e => e.PageName).HasMaxLength(300);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionTranslations)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionTranslation_Permission");
            });

            modelBuilder.Entity<PracticalEnrollmentExam>(entity =>
            {
                entity.ToTable("PracticalEnrollmentExam");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.PracticalEnrollmentExams)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_PracticalEnrollmentExam_EnrollTeacherCourse");

                entity.HasOne(d => d.PracticalExam)
                    .WithMany(p => p.PracticalEnrollmentExams)
                    .HasForeignKey(d => d.PracticalExamId)
                    .HasConstraintName("FK_PracticalEnrollmentExam_PracticalExam");
            });

            modelBuilder.Entity<PracticalEnrollmentExamStudent>(entity =>
            {
                entity.ToTable("PracticalEnrollmentExamStudent");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Mark).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.MarkAfterConversion).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.PracticalEnrollmentExamStudents)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .HasConstraintName("FK_PracticalEnrollmentExamStudent_EnrollStudentCourse");

                entity.HasOne(d => d.PracticalEnrollmentExam)
                    .WithMany(p => p.PracticalEnrollmentExamStudents)
                    .HasForeignKey(d => d.PracticalEnrollmentExamId)
                    .HasConstraintName("FK_PracticalEnrollmentExamStudent_PracticalEnrollmentExam");
            });

            modelBuilder.Entity<PracticalEnrollmentExamStudentSubject>(entity =>
            {
                entity.ToTable("PracticalEnrollmentExamStudentSubject");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DisountMarkTotal).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Mark).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.PracticalEnrollmentExamStudent)
                    .WithMany(p => p.PracticalEnrollmentExamStudentSubjects)
                    .HasForeignKey(d => d.PracticalEnrollmentExamStudentId)
                    .HasConstraintName("FK_PracticalEnrollmentExamStudentSubject_PracticalEnrollmentExamStudent");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.PracticalEnrollmentExamStudentSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_PracticalEnrollmentExamStudentSubject_Subject");
            });

            modelBuilder.Entity<PracticalEnrollmentExamStudentSubjectRating>(entity =>
            {
                entity.ToTable("PracticalEnrollmentExamStudentSubjectRating");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Mark).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.PracticalEnrollmentExamStudentSubject)
                    .WithMany(p => p.PracticalEnrollmentExamStudentSubjectRatings)
                    .HasForeignKey(d => d.PracticalEnrollmentExamStudentSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalEnrollmentExamStudentSubjectRating_PracticalEnrollmentExamStudentSubject");

                entity.HasOne(d => d.PracticalQuestion)
                    .WithMany(p => p.PracticalEnrollmentExamStudentSubjectRatings)
                    .HasForeignKey(d => d.PracticalQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalEnrollmentExamStudentSubjectRating_PracticalQuestion");
            });

            modelBuilder.Entity<PracticalEnrollmentExamTrainer>(entity =>
            {
                entity.ToTable("PracticalEnrollmentExamTrainer");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.PracticalEnrollmentExam)
                    .WithMany(p => p.PracticalEnrollmentExamTrainers)
                    .HasForeignKey(d => d.PracticalEnrollmentExamId)
                    .HasConstraintName("FK_PracticalEnrollmentExamTrainer_PracticalEnrollmentExam");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.PracticalEnrollmentExamTrainers)
                    .HasForeignKey(d => d.TrainerId)
                    .HasConstraintName("FK_PracticalEnrollmentExamTrainer_Trainer");
            });

            modelBuilder.Entity<PracticalExam>(entity =>
            {
                entity.ToTable("PracticalExam");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Mark).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.MarkAfterConversion).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<PracticalExamCourse>(entity =>
            {
                entity.ToTable("PracticalExamCourse");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.PracticalExamCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalExamCourse_Course");

                entity.HasOne(d => d.PracticalExam)
                    .WithMany(p => p.PracticalExamCourses)
                    .HasForeignKey(d => d.PracticalExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalExamCourse_PracticalExam");
            });

            modelBuilder.Entity<PracticalExamCourseSubject>(entity =>
            {
                entity.ToTable("PracticalExamCourseSubject");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.PracticalExamCourse)
                    .WithMany(p => p.PracticalExamCourseSubjects)
                    .HasForeignKey(d => d.PracticalExamCourseId)
                    .HasConstraintName("FK_PracticalExamCourseSubject_PracticalExamCourse");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.PracticalExamCourseSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_PracticalExamCourseSubject_Subject");
            });

            modelBuilder.Entity<PracticalExamQuestion>(entity =>
            {
                entity.ToTable("PracticalExamQuestion");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.PracticalExam)
                    .WithMany(p => p.PracticalExamQuestions)
                    .HasForeignKey(d => d.PracticalExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalExamQuestion_PracticalExam");

                entity.HasOne(d => d.PracticalQuestion)
                    .WithMany(p => p.PracticalExamQuestions)
                    .HasForeignKey(d => d.PracticalQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalExamQuestion_PracticalQuestion");
            });

            modelBuilder.Entity<PracticalExamTranslation>(entity =>
            {
                entity.ToTable("PracticalExamTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.PracticalExam)
                    .WithMany(p => p.PracticalExamTranslations)
                    .HasForeignKey(d => d.PracticalExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalExamTranslation_PracticalExam");
            });

            modelBuilder.Entity<PracticalQuestion>(entity =>
            {
                entity.ToTable("PracticalQuestion");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1024);

                entity.Property(e => e.Mark).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<PracticalQuestionTranslation>(entity =>
            {
                entity.ToTable("PracticalQuestionTranslation");

                entity.Property(e => e.Description).HasMaxLength(1024);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.PracticalQuestion)
                    .WithMany(p => p.PracticalQuestionTranslations)
                    .HasForeignKey(d => d.PracticalQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PracticalQuestionTranslation_PracticalQuestion");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Question_Trainer");
            });

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.ToTable("QuestionOption");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_QuestionOption_Question");
            });

            modelBuilder.Entity<QuestionOptionTranslation>(entity =>
            {
                entity.ToTable("QuestionOptionTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.QuestionOptionTranslations)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_QuestionOptionTranslation_QuestionOption");
            });

            modelBuilder.Entity<QuestionTranslation>(entity =>
            {
                entity.ToTable("QuestionTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionTranslations)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_QuestionTranslation_Question");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermissions_Permission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermissions_RolePermissions");
            });

            modelBuilder.Entity<SectionOfCourse>(entity =>
            {
                entity.ToTable("SectionOfCourse");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.SectionOfCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectionOfCourse_Course");
            });

            modelBuilder.Entity<SectionOfCourseQuiz>(entity =>
            {
                entity.ToTable("SectionOfCourseQuiz");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.SectionOfCourseQuizzes)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_SectionOfCourseQuiz_Lecture");

                entity.HasOne(d => d.SectionOfCourse)
                    .WithMany(p => p.SectionOfCourseQuizzes)
                    .HasForeignKey(d => d.SectionOfCourseId)
                    .HasConstraintName("FK_SectionOfCourseQuiz_SectionOfCourse");
            });

            modelBuilder.Entity<SectionOfCourseTranslation>(entity =>
            {
                entity.ToTable("SectionOfCourseTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionOfCourseTranslations)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SectionOfCourseTranslation_SectionOfCourse");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("Semester");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.PublicationDate).HasColumnType("date");

                entity.Property(e => e.PublicationEndDate).HasColumnType("date");

                entity.Property(e => e.WorkEndDate).HasColumnType("date");

                entity.Property(e => e.WorkStartDate).HasColumnType("date");
            });

            modelBuilder.Entity<SemesterTranslation>(entity =>
            {
                entity.ToTable("SemesterTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.SemesterTranslations)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SemesterTranslation_Semester");
            });

            modelBuilder.Entity<SenangPay>(entity =>
            {
                entity.ToTable("SenangPay");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ApplicationUserId).HasMaxLength(450);

                entity.Property(e => e.Country).HasMaxLength(450);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(18, 8)");

                entity.Property(e => e.CustomerCurrencyCode).HasMaxLength(250);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Details).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(450);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(450);

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.SenangPays)
                    .HasForeignKey(d => d.ApplicationUserId);

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.SenangPays)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK__SenangPay__Enrol__30E33A54");

                entity.HasOne(d => d.ProjectCost)
                    .WithMany(p => p.SenangPays)
                    .HasForeignKey(d => d.ProjectCostId)
                    .HasConstraintName("FK_SenangPay_CmsProjectCost");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.SenangPays)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_SenangPay_CmsProject");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("source");

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Nationality).HasMaxLength(255);

                entity.Property(e => e.NewId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("NewID");

                entity.Property(e => e.Number).HasMaxLength(255);

                entity.Property(e => e.State).HasMaxLength(255);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Balance).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.BirthPlace).HasMaxLength(200);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.ExtraMobile).HasMaxLength(100);

                entity.Property(e => e.MedicalDescription).HasMaxLength(250);

                entity.Property(e => e.Work)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.WorkPlace).HasMaxLength(200);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Contact");
            });

            modelBuilder.Entity<StudentAttendance>(entity =>
            {
                entity.ToTable("StudentAttendance");

                entity.Property(e => e.AbsenceNote)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.StudentAttendances)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .HasConstraintName("FK_StudentAttendance_EnrollTeacherCourse");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAttendances)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentAttendance_Student");
            });

            modelBuilder.Entity<StudentBalanceHistory>(entity =>
            {
                entity.ToTable("StudentBalanceHistory");

                entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Balance).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.StudentBalanceHistories)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .HasConstraintName("FK_StudentBalanceHistory_EnrollStudentCourse");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentBalanceHistories)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentBalanceHistory_Student");
            });

            modelBuilder.Entity<StudentExpulsionHistory>(entity =>
            {
                entity.ToTable("StudentExpulsionHistory");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollStudentCourse)
                    .WithMany(p => p.StudentExpulsionHistories)
                    .HasForeignKey(d => d.EnrollStudentCourseId)
                    .HasConstraintName("FK_StudentExpulsionHistory_EnrollStudentCourse");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentExpulsionHistories)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentExpulsionHistory_Student");
            });

            modelBuilder.Entity<StudentNote>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentNotes)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentNotes_Student");
            });

            modelBuilder.Entity<StudentNotesTranslation>(entity =>
            {
                entity.ToTable("StudentNotesTranslation");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.StudentNote)
                    .WithMany(p => p.StudentNotesTranslations)
                    .HasForeignKey(d => d.StudentNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentNotesTranslation_StudentNotes");
            });

            modelBuilder.Entity<StudentSubscription>(entity =>
            {
                entity.ToTable("StudentSubscription");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(9, 2)");

                entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.StudentSubscriptions)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSubscription_EnrollTeacherCourse");
            });

            modelBuilder.Entity<StudentTranslation>(entity =>
            {
                entity.ToTable("StudentTranslation");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BirthPlace).HasMaxLength(200);

                entity.Property(e => e.Country).HasMaxLength(200);

                entity.Property(e => e.Work).HasMaxLength(200);

                entity.Property(e => e.WorkPlace).HasMaxLength(200);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentTranslations)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTranslation_Student");
            });

            modelBuilder.Entity<SubCommunicationChannel>(entity =>
            {
                entity.ToTable("SubCommunicationChannel");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.CommunicationChannel)
                    .WithMany(p => p.SubCommunicationChannels)
                    .HasForeignKey(d => d.CommunicationChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCommunicationChannel_CommunicationChannel");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_SubCommunicationChannel_SubCommunicationChannel");
            });

            modelBuilder.Entity<SubCommunicationChannelTranslation>(entity =>
            {
                entity.ToTable("SubCommunicationChannelTranslation");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.SubCommunicationChannel)
                    .WithMany(p => p.SubCommunicationChannelTranslations)
                    .HasForeignKey(d => d.SubCommunicationChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCommunicationChannelTranslation_SubCommunicationChannel");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<SubjectTranslation>(entity =>
            {
                entity.ToTable("SubjectTranslation");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectTranslations)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectTranslation_Subject");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<SuperAdmin>(entity =>
            {
                entity.ToTable("SuperAdmin");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<SuperAdminSetting>(entity =>
            {
                entity.Property(e => e.ImageUrl).HasMaxLength(512);

                entity.Property(e => e.ImageUrlAr).HasMaxLength(512);

                entity.Property(e => e.NameArabic).HasMaxLength(512);

                entity.Property(e => e.NameEnglish).HasMaxLength(512);

                entity.Property(e => e.SecondarySiteColor).HasMaxLength(256);

                entity.Property(e => e.SiteColor).HasMaxLength(256);
            });

            modelBuilder.Entity<SystemFile>(entity =>
            {
                entity.ToTable("SystemFile");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.DisplayName).HasMaxLength(200);

                entity.Property(e => e.FileUrl).IsRequired();

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SystemFileTranslation>(entity =>
            {
                entity.ToTable("SystemFileTranslation");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.SystemFile)
                    .WithMany(p => p.SystemFileTranslations)
                    .HasForeignKey(d => d.SystemFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemFileTranslation_SystemFile");
            });

            modelBuilder.Entity<SystemGroup>(entity =>
            {
                entity.ToTable("SystemGroup");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<SystemGroupTranslation>(entity =>
            {
                entity.ToTable("SystemGroupTranslation");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.SystemGroup)
                    .WithMany(p => p.SystemGroupTranslations)
                    .HasForeignKey(d => d.SystemGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemGroupTranslation_SystemGroup");
            });

            modelBuilder.Entity<SystemGroupUser>(entity =>
            {
                entity.HasOne(d => d.SystemGroup)
                    .WithMany(p => p.SystemGroupUsers)
                    .HasForeignKey(d => d.SystemGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemGroupUsers_SystemGroup");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.SystemGroupUsers)
                    .HasForeignKey(d => d.UserProfileId)
                    .HasConstraintName("FK_SystemGroupUsers_UserProfile");
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.ToTable("SystemLog");

                entity.Property(e => e.Component)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.ToTable("SystemSetting");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.SystemSettings)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_SystemSetting_Branch");

                entity.HasOne(d => d.SuperAdmin)
                    .WithMany(p => p.SystemSettings)
                    .HasForeignKey(d => d.SuperAdminId)
                    .HasConstraintName("FK_SystemSetting_SuperAdmin");
            });

            modelBuilder.Entity<SystemSettingTranslation>(entity =>
            {
                entity.ToTable("SystemSettingTranslation");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.SystemSettingTranslations)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemSettingTranslation_SystemSetting");
            });

            modelBuilder.Entity<TeacherAttendance>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(512);

                entity.HasOne(d => d.EnrollTeacherCourse)
                    .WithMany(p => p.TeacherAttendances)
                    .HasForeignKey(d => d.EnrollTeacherCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherAttendances_EnrollTeacherCourse");
            });

            modelBuilder.Entity<TempTable1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Temp_Table1");

                entity.Property(e => e.Rownum).HasColumnName("rownum");
            });

            modelBuilder.Entity<TemplateHtml>(entity =>
            {
                entity.ToTable("TemplateHtml");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TemplateHtmls)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TemplateHtml_DetailsLookup");
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("Trainer");

                entity.Property(e => e.CreatedBy).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).HasMaxLength(300);

                entity.Property(e => e.Signature).HasMaxLength(512);

                entity.Property(e => e.StartWorkDate).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Trainers)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trainer_Contact");
            });

            modelBuilder.Entity<TrainerRateMeasure>(entity =>
            {
                entity.ToTable("TrainerRateMeasure");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.FromRange).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Measure).HasMaxLength(200);

                entity.Property(e => e.ToRange).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<TrainerRateMeasureTranslation>(entity =>
            {
                entity.ToTable("TrainerRateMeasureTranslation");

                entity.Property(e => e.Measure)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.TrainerRateMeasure)
                    .WithMany(p => p.TrainerRateMeasureTranslations)
                    .HasForeignKey(d => d.TrainerRateMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrainerRateMeasureTranslation_TrainerRateMeasure");
            });

            modelBuilder.Entity<TrainerTranslation>(entity =>
            {
                entity.ToTable("TrainerTranslation");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.TrainerTranslations)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrainerTranslation_Trainer");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.StartWorkDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_UserProfile_Contact");
            });

            modelBuilder.Entity<UserProfileTranslation>(entity =>
            {
                entity.ToTable("UserProfileTranslation");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.UserProfileTranslations)
                    .HasForeignKey(d => d.UserProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfileTranslation_UserProfile");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
