using AbstractionLayer;
using AutoMapper;
using CoreLayer.Entities;
using Academy.Interfaces.Interfaces;
using SharedLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class CompanyCourseService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICompanyCourseService
	{
		public async Task<CompanyCourseDTO> CreateCourseAsync(CreateCompanyCourseDTO dto)
		{
			var course = _mapper.Map<CreateCompanyCourseDTO, CompanyCourse>(dto);
			var repo = _unitOfWork.GetRepository<CompanyCourse, int>();
			await repo.AddAsync(course);
			await _unitOfWork.SaveChangesAsync();
			return _mapper.Map<CompanyCourse, CompanyCourseDTO>(course);
		}

		public async Task DeleteCourseAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<CompanyCourse, int>();
			var course = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Course not found.");
			repo.Delete(course);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task EnrollInCourseAsync(int courseId, CourseEnrollmentDTO dto)
		{
			var courseRepo = _unitOfWork.GetRepository<CompanyCourse, int>();
			var course = await courseRepo.GetByIdAsync(courseId)
				?? throw new KeyNotFoundException("Course not found");

			var enrollment = _mapper.Map<CourseEnrollment>(dto);
			enrollment.CourseId = courseId;

			var enrollmentRepo = _unitOfWork.GetRepository<CourseEnrollment, int>();
			await enrollmentRepo.AddAsync(enrollment);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<CompanyCourseDTO>> GetAllCoursesAsync()
		{
			var Repo = _unitOfWork.GetRepository<CompanyCourse, int>();
			var Courses = await Repo.GetAllAsync();
			var MappedCourses = _mapper.Map<IEnumerable<CompanyCourse>, IEnumerable<CompanyCourseDTO>>(Courses);
			return MappedCourses;
		}

		public async Task<CompanyCourseDTO> GetCourseById(int id)
		{
			var Course = await _unitOfWork.GetRepository<CompanyCourse, int>().GetByIdAsync(id);
			var MappedCourse = _mapper.Map<CompanyCourse, CompanyCourseDTO>(Course);
			return MappedCourse;
		}

		public async Task<CompanyCourseDTO> UpdateCourseAsync(int id, CreateCompanyCourseDTO dto)
		{
			var repo = _unitOfWork.GetRepository<CompanyCourse, int>();
			var existingCourse = await repo.GetByIdAsync(id)
				?? throw new KeyNotFoundException("Course not found.");
			_mapper.Map<CreateCompanyCourseDTO, CompanyCourse>(dto, existingCourse);
			repo.Update(existingCourse);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<CompanyCourse, CompanyCourseDTO>(existingCourse);
		}
	}
}
