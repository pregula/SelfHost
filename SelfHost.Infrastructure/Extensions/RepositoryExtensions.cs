using SelfHost.Domain;
using SelfHost.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace SelfHost.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Company> GetOrFailAsync(this ICompanyRepository repository, long id)
        {
            var company = await repository.GetAsync(id);
            if (company == null)
            {
                throw new Exception($"Company with id: '{id}' does not exist!");
            }

            return company;
        }
    }
}