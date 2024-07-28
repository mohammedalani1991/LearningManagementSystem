using LearningManagementSystem.Data;
using LearningManagementSystem.Services.ControlPanel;
using LearningManagementSystem.Services.General;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;
using DataEntity.Models.EfModels;
using LearningManagementSystem.Services.Global.Service;
using LearningManagementSystem.Services.Global.IService;
using LearningManagementSystem.Services.BankOfQuestion;
using LearningManagementSystem.Services.Controllers;
using LearningManagementSystem.Services.Reports.ISerives;
using LearningManagementSystem.Services.Reports.Service;
using LearningManagementSystem.Services.ControlPanel.IServices;
using LearningManagementSystem.Services.ControlPanel.Services;
using Microsoft.Extensions.Options;

namespace LearningManagementSystem
{
    public class Startup
    {
        private const string DefaultCultureName = "en-US";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>
                (
                    options => options.SignIn.RequireConfirmedAccount = true
                )
                .AddEntityFrameworkStores<ApplicationDbContext>()

                .AddDefaultTokenProviders();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                          options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.Configure<PasswordHasherOptions>(options =>
                options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
            );
            services.AddDbContext<LearningManagementSystemContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IAboutDicService, AboutDicService>();
            services.AddScoped<ISystemLogService, SystemLogService>();
            services.AddScoped<ISystemSettingService, SystemSettingService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<IModuleService, ModuleService>(); services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<ICmsCateryService, CmsCateryService>();
            services.AddScoped<ICmsSliderService, CmsSliderService>();
            services.AddScoped<ICmsPageService, CmsPageService>();
            services.AddScoped<ICmsProjectService, CmsProjectService>();
            services.AddScoped<ICmsCategoryProjectService, CmsCategoryProjectService>();
            services.AddScoped<ICmsWhatPeopleSayService, CmsWhatPeopleSayService>();
            services.AddScoped<ISemesterService, SemesterService>();
            services.AddScoped<IRolesPermissionService, RolesPermissionService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISystemGroupService, SystemGroupService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<ISenangPayService, SenangPayService>();

            services.AddScoped<IContactsService, ContactsService>();
            services.AddScoped<IEmailsService, EmailsService>();
            services.AddScoped<ICommunicationLogService, CommunicationLogService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IGeneralizationService, GeneralizationService>();
            services.AddScoped<ICommunicationChannelService, CommunicationChannelService>();
            services.AddScoped<ISubCommunicationChannelService, SubCommunicationChannelService>();
            services.AddScoped<IGlobalSmsService, GlobalSmsService>();

            services.AddScoped<IQuestionOptionService, QuestionOptionService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IEnrollStudentCourseService, EnrollStudentCourseService>();
            services.AddScoped<IEnrollStudentExamService, EnrollStudentExamService>();
            services.AddScoped<IEnrollStudentExamAnswerService, EnrollStudentExamAnswerService>();


            services.AddScoped<ICourseCategoryService, CourseCategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<ISectionOfCourseService, SectionOfCourseService>();
            services.AddScoped<ICourseResourceService, CourseResourceService>();
            services.AddScoped<ICoursePrerequisiteService, CoursePrerequisiteService>();
            services.AddScoped<IExamTemplateService, ExamTemplateService>();
            services.AddScoped<IExamQuestionService, ExamQuestionService>();
            services.AddScoped<IEnrollTeacherCourseService, EnrollTeacherCourseService>();
            services.AddScoped<IEnrollSectionOfCourseService, EnrollSectionOfCourseService>();
            services.AddScoped<IEnrollCourseTimeService, EnrollCourseTimeService>();
            services.AddScoped<IEnrollLectureService, EnrollLectureService>();
            services.AddScoped<IEnrollCourseResourceService, EnrollCourseResourceService>();
            services.AddScoped<IEnrollAssignmentService, EnrollAssignmentService>();
            services.AddScoped<IEnrollCourseExamService, EnrollCourseExamService>();
            services.AddScoped<IEnrollCourseExamQuestionService, EnrollCourseExamQuestionService>();
            services.AddScoped<ISubscribersService, SubscribersService>();
            services.AddScoped<IEnrollStudentCourseAttachmentService, EnrollStudentCourseAttachmentService>();
            services.AddScoped<IInvoicesPayService, InvoicesPayService>();
            services.AddScoped<IEnrollCourseAssignmentService, EnrollCourseAssignmentService>();
            services.AddScoped<IEnrollStudentAssigmentService, EnrollStudentAssigmentService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IAttendancesService, AttendancesService>();
            services.AddScoped<ICoursePackagesService, CoursePackagesService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IPracticalQuestionService, PracticalQuestionService>();
            services.AddScoped<IPracticalExamService, PracticalExamService>();
            services.AddScoped<IPracticalEnrollmentExamService, PracticalEnrollmentExamService>();
            services.AddScoped<IPracticalEnrollmentExamStudentService, PracticalEnrollmentExamStudentService>();
            services.AddScoped<ISectionOfCourseQuizService, SectionOfCourseQuizService>();
            services.AddScoped<IEnrollCourseQuizService, EnrollCourseQuizService>();
            services.AddScoped<IManagementStandardService, ManagementStandardService>();
            services.AddScoped<IAcademicSupervisionStandardService, AcademicSupervisionStandardService>();
            services.AddScoped<ITrainerRateMeasureService, TrainerRateMeasureService>();
            services.AddScoped<IEnrollCourseAllowUserRateService, EnrollCourseAllowUserRateService>();
            services.AddScoped<ITrainerRateService, TrainerRateService>();
            services.AddScoped<IEnrollStudentAlertService, EnrollStudentAlertService>();
            services.AddScoped<IExpulsionService, ExpulsionService>();
            services.AddScoped<IBalanceHistoryService, BalanceHistoryService>();
            services.AddScoped<IStudentReportService, StudentReportService>();
            services.AddScoped<IReportsServies, ReportsServies>();
            services.AddScoped<ICourseMarksService, CourseMarksService>();
            services.AddScoped<IMarkAdoptionService, MarkAdoptionService>();
            services.AddScoped<ICertificateAdoptionService, CertificateAdoptionService>();
            services.AddScoped<ITrainerReportService, TrainerReportService>();

            services.AddMvc()
              // localization options are going to have their resources (language dictionary) stored in Resources folder.
              .AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
              .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization();

            services.AddAntiforgery();

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                // the list of supported cultures.
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo(DefaultCultureName),
                    new CultureInfo("ar")
                };

                // set the localization default culture as english
                opts.DefaultRequestCulture = new RequestCulture(DefaultCultureName);

                // supported cultures are the supportedCultures variable we defined above.
                // formatiting dates, numbers, etc.
                opts.SupportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo(DefaultCultureName)
                };

                // UI strings that we have localized 
                opts.SupportedUICultures = supportedCultures;
            });
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(DefaultCultureName);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(DefaultCultureName);

            var ci = new CultureInfo(DefaultCultureName);
            System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat = ci.DateTimeFormat;


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/HandleError", "?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("ControlPanel",
                    "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}").RequireAuthorization();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute("Error", "{*url}", new { controller = "Home", action = "HandleError", code = 404 });
            });


            // Add this middleware to handle the custom 404 error page
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
                {
                    context.Response.Redirect("/Home/HandleError/404");
                }
            });
        }
    }
}
