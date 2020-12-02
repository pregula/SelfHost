using AutoMapper;
using SelfHost.Domain;
using SelfHost.Domain.Repositories;
using SelfHost.Infrastructure.Abstractions.Interfaces;
using SelfHost.Infrastructure.Commands.Company;
using SelfHost.Infrastructure.DTO;
using SelfHost.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfHost.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<long> CreateAsync(CreateCompany dto)
        {
            var company = new Company(dto.Name, (int)dto.EstablishmentYear);
            return await _companyRepository.AddAsync(company);
        }

        public async Task AddEmployeesAsync(long companyId, IEnumerable<CreateCompanyEmployee> dtos)
        {
            var company = await _companyRepository.GetOrFailAsync(companyId);
            IEnumerable<Employee> employees = dtos.Select(d => new Employee(d.FristName, d.LastName, d.DateOfBirth, d.JobTitle.ToEnum()));
            company.AddEmployees(employees);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task<ResultDto> SearchAsync(Search dto)
        {
            IEnumerable<string> employeeJobTitles = Enumerable.Empty<string>();
            if (dto.EmployeeJobTitles != null)
            {
                employeeJobTitles = dto.EmployeeJobTitles;
            }

            var jobTitleEnums = employeeJobTitles.Select(ejt => ejt.ToEnum());
            var companies = await _companyRepository.BrowseAsync(dto.EmployeeDateOfBirthFrom, dto.EmployeeDateOfBirthTo, jobTitleEnums, dto.Keyword);
            return new ResultDto() { Result = _mapper.Map<IEnumerable<CompanyDetailsDto>>(companies) };
        }

        public async Task UpdateAsync(long id, UpdateCompany dto)
        {
            var company = await _companyRepository.GetOrFailAsync(id);
            company.SetName(dto.Name);
            company.SetEstablishmentYear((int)dto.EstablishmentYear);
            var employeesToDelete = company.Employees.ToList();
            company.DeleteEmployees(employeesToDelete);
            IEnumerable<Employee> employees = Enumerable.Empty<Employee>();
            if (dto.Employees != null)
            {
                employees = dto.Employees.Select(d => new Employee(d.FristName, d.LastName, d.DateOfBirth, d.JobTitle.ToEnum()));
            }
            company.AddEmployees(employees);
            await _companyRepository.UpdateAsync(company);
        }

        public async Task DeleteAsync(long id)
        {
            var company = await _companyRepository.GetOrFailAsync(id);
            await _companyRepository.DeleteAsync(company);
        }
    }
}