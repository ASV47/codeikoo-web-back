using AbstractionLayer;
using Academy.Interfaces.IServices.IAcademyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices
{
	public interface IServiceManager
	{
		IAuthenticationService AuthenticationService { get; }
		IContactMessageService ContactMessageService { get; }
		IJobService JobService { get; }	
		IJobApplicationService JobApplicationService { get; }	
		IInstructorApplicationService InstructorApplicationService { get; }
		ICourseService CourseService { get; }
		ICourseUnitService CourseUnitService { get; }
		IUnitLessonService UnitLessonService { get; }
		IImageSliderService ImageSliderService { get; }
		IStudentCourseCommentService StudentCourseCommentService { get; }

        #region Company
        ICompanyCourseService CompanyCourseService { get; }
		IArticleService ArticleService { get; }
		ICompanyJobApplicationService CompanyJobApplicationService { get; }
		ICompanyContactMessageService CompanyContactMessageService { get; }
		ISiteContactService SiteContactService { get; }
		IMissionService MissionService { get; }
		IExperienceService ExperienceService { get; }
		IExperienceCategoryService ExperienceCategoryService { get; }
		IExperienceItemService ExperienceItemService { get; }
		IServiceHandler serviceHandler { get; }
		ITechnologyService TechnologyService { get; }
		IClientService clientService { get; }
		IAboutUsService aboutUsService { get; }
		IAchievementService achievementService { get; }
		IFlexibilitySectionService flexibilitySectionService { get; }
		IFlexibilityItemService flexibilityItemService { get; }
		IWebSettingsService webSettingsService { get; }
		#endregion
	}
}
