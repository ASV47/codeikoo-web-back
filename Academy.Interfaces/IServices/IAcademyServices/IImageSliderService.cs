using Academy.Interfaces.DTOs.AcademyDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.IServices.IAcademyServices
{
    public interface IImageSliderService
    {
        Task<ImageSliderDto> AddAsync(CreateImageSliderDto dto);
        Task<List<ImageSliderDto>> GetAllAsync();
        Task<ImageSliderDto> GetByIdAsync(int id);
        Task<ImageSliderDto> UpdateAsync(int id, CreateImageSliderDto dto);
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
    }
}
