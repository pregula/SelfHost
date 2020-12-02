using System.Collections.Generic;

namespace SelfHost.Infrastructure.DTO
{
    public class ResultDto
    {
        public IEnumerable<CompanyDetailsDto> Result { get; set; }
    }
}