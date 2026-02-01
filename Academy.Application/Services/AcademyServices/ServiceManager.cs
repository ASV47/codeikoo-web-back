using AbstractionLayer;
using Academy.Application.Repositories;
using Academy.Infrastructure.Entities.AcademyEntities;
using Academy.Interfaces.Interfaces;
using Academy.Interfaces.IServices;
using Academy.Interfaces.IServices.IAcademyServices;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.AcademyServices
{
    public class ServiceManager(UserManager<ApplicationUser> userManager, IConfiguration configuration,
		IUnitOfWork unitOfWork, IMapper mapper, IEmailSender _emailSender, IFileStorageService storageService, ILocalizationService localizationService) : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration, _emailSender));
        private readonly Lazy<IContactMessageService> _LazyContactMessageService = new Lazy<IContactMessageService>(() => new ContactMessageService(unitOfWork, mapper));
        private readonly Lazy<IJobService> _LazyJobService = new Lazy<IJobService>(() => new JobService(unitOfWork, mapper, localizationService));
        private readonly Lazy<IJobApplicationService> _LazyJobApplicationService = new Lazy<IJobApplicationService>(() => new JobApplicationService(unitOfWork, mapper, storageService));
        private readonly Lazy<IInstructorApplicationService> _LazyInstructorApplicationService = new Lazy<IInstructorApplicationService>(() => new InstructorApplicationService(unitOfWork, mapper, storageService));
        private readonly Lazy<ICourseService> _LazyCourseService = new(() => new CourseService(unitOfWork, mapper, storageService, localizationService));
        private readonly Lazy<ICourseUnitService> _LazyCourseUnitService = new(() => new CourseUnitService(unitOfWork, mapper, localizationService));
        private readonly Lazy<IUnitLessonService> _LazyUnitLessonService = new(() => new UnitLessonService(unitOfWork, mapper, localizationService));
        private readonly Lazy<IImageSliderService> _LazyImageSliderService = new(() => new ImageSliderService(unitOfWork, mapper, storageService));
        private readonly Lazy<IStudentCourseCommentService> _LazyStudentCourseCommentService = new(() => new StudentCourseCommentService(unitOfWork, mapper));

		#region Company
		private readonly Lazy<ICompanyCourseService> _LazyCompanyCourseService = new Lazy<ICompanyCourseService>(() => new CompanyCourseService(unitOfWork, mapper));
		private readonly Lazy<IArticleService> _LazyArticleService = new Lazy<IArticleService>(() => new ArticleService(unitOfWork, mapper, storageService));
		private readonly Lazy<ICompanyJobApplicationService> _LazyCompanyJobApplicationService = new Lazy<ICompanyJobApplicationService>(() => new CompanyJobApplicationService(unitOfWork, mapper));
		private readonly Lazy<ICompanyContactMessageService> _LazyCompanyContactMessageService = new Lazy<ICompanyContactMessageService>(() => new CompanyContactMessageService(unitOfWork, mapper));
		private readonly Lazy<ISiteContactService> _LazySiteContactService = new Lazy<ISiteContactService>(() => new SiteContactService(unitOfWork, mapper));
		private readonly Lazy<IMissionService> _LazyMissionService = new Lazy<IMissionService>(() => new MissionService(unitOfWork, mapper));
		private readonly Lazy<IExperienceService> _LazyExperienceService = new Lazy<IExperienceService>(() => new ExperienceService(unitOfWork, mapper));
		private readonly Lazy<IExperienceCategoryService> _LazyExperienceCategoryService = new Lazy<IExperienceCategoryService>(() => new ExperienceCategoryService(unitOfWork, mapper));
		private readonly Lazy<IExperienceItemService> _LazyExperienceItemService = new Lazy<IExperienceItemService>(() => new ExperienceItemService(unitOfWork, mapper));
		private readonly Lazy<IServiceHandler> _LazyServiceHandler = new Lazy<IServiceHandler>(() => new ServiceHandler(unitOfWork, mapper));
		private readonly Lazy<ITechnologyService> _lazyTechnologyService = new Lazy<ITechnologyService>(() => new TechnologyHandler(unitOfWork, mapper));
		private readonly Lazy<IClientService> _LazyClientService = new Lazy<IClientService>(() => new ClientService(unitOfWork, mapper, storageService));
		private readonly Lazy<IAboutUsService> _LazyAboutUsService = new Lazy<IAboutUsService>(() => new AboutUsService(unitOfWork, mapper));
		private readonly Lazy<IAchievementService> _LazyAchievementService = new Lazy<IAchievementService>(() => new AchievementService(unitOfWork, mapper));
		private readonly Lazy<IFlexibilitySectionService> _LazyFlexibilitySectionService = new Lazy<IFlexibilitySectionService>(() => new FlexibilitySectionService(unitOfWork, mapper));
		private readonly Lazy<IFlexibilityItemService> _LazyFlexibilityItemService = new Lazy<IFlexibilityItemService>(() => new FlexibilityItemService(unitOfWork, mapper, storageService));
		private readonly Lazy<IWebSettingsService> _LazyWebSettingsService = new Lazy<IWebSettingsService>(() => new WebSettingsService(unitOfWork, mapper));
		#endregion

		public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
        public IContactMessageService ContactMessageService => _LazyContactMessageService.Value;
        public IJobService JobService => _LazyJobService.Value;
        public IJobApplicationService JobApplicationService => _LazyJobApplicationService.Value;
        public IInstructorApplicationService InstructorApplicationService => _LazyInstructorApplicationService.Value;
        public ICourseService CourseService => _LazyCourseService.Value;
        public ICourseUnitService CourseUnitService => _LazyCourseUnitService.Value;
        public IUnitLessonService UnitLessonService => _LazyUnitLessonService.Value;
        public IImageSliderService ImageSliderService => _LazyImageSliderService.Value;
        public IStudentCourseCommentService StudentCourseCommentService => _LazyStudentCourseCommentService.Value;


        #region Company
        public ICompanyCourseService CompanyCourseService => _LazyCompanyCourseService.Value;
		public IArticleService ArticleService => _LazyArticleService.Value;
		public ICompanyJobApplicationService CompanyJobApplicationService => _LazyCompanyJobApplicationService.Value;
		public ICompanyContactMessageService CompanyContactMessageService => _LazyCompanyContactMessageService.Value;
		public ISiteContactService SiteContactService => _LazySiteContactService.Value;
		public IMissionService MissionService => _LazyMissionService.Value;
		public IExperienceService ExperienceService => _LazyExperienceService.Value;
		public IExperienceCategoryService ExperienceCategoryService => _LazyExperienceCategoryService.Value;
		public IExperienceItemService ExperienceItemService => _LazyExperienceItemService.Value;
		public IServiceHandler serviceHandler => _LazyServiceHandler.Value;
		public ITechnologyService TechnologyService => _lazyTechnologyService.Value;
		public IClientService clientService => _LazyClientService.Value;
		public IAboutUsService aboutUsService => _LazyAboutUsService.Value;
		public IAchievementService achievementService => _LazyAchievementService.Value;
		public IFlexibilitySectionService flexibilitySectionService => _LazyFlexibilitySectionService.Value;
		public IFlexibilityItemService flexibilityItemService => _LazyFlexibilityItemService.Value;
		public IWebSettingsService webSettingsService => _LazyWebSettingsService.Value;


        #endregion

    }
}
