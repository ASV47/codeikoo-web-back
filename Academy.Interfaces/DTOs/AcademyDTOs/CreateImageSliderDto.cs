using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Interfaces.DTOs.AcademyDTOs
{
    public class CreateImageSliderDto
    {
        public IFormFile? Image { get; set; }
    }
}
