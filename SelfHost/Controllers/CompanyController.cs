using Microsoft.AspNetCore.Mvc;
using SelfHost.Framework;
using SelfHost.Infrastructure.Abstractions.Interfaces;
using SelfHost.Infrastructure.Commands.Company;
using System.Linq;
using System.Threading.Tasks;

namespace SelfHost.Controllers
{
    [Route("company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [AuthFilter]
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateCompany command)
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult(Ok(ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage)));
            }
            var companyId = await _companyService.CreateAsync(command);
            await _companyService.AddEmployeesAsync(companyId, command.Employees);
            return Created($"{companyId}", null);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] Search command)
        {
            return Ok(await _companyService.SearchAsync(command));
        }

        [AuthFilter]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateCompany command)
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult(Ok(ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage)));
            }
            await _companyService.UpdateAsync(id, command);
            return NoContent();
        }

        [AuthFilter]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }
    }
}