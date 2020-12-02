using System;
using System.Collections.Generic;

namespace SelfHost.Domain
{
    public class Company : BaseObject
    {
        public string Name { get; protected set; }
        public int EstablishmentYear { get; protected set; }
        public virtual IEnumerable<Employee> Employees => _employees;
        private ISet<Employee> _employees = new HashSet<Employee>();

        protected Company()
        {
        }

        public Company(string name, int establishmentYear)
        {
            SetName(name);
            SetEstablishmentYear(establishmentYear);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Company can not have an empty name.");
            }

            Name = name;
        }

        public void SetEstablishmentYear(int establishmentYear)
        {
            if (establishmentYear <= 0)
            {
                throw new Exception("Company can not have an zero or under zero establishment year.");
            }

            EstablishmentYear = establishmentYear;
        }

        public void AddEmployees(IEnumerable<Employee> employees)
        {
            _employees.UnionWith(employees);
        }

        public void DeleteEmployees(IEnumerable<Employee> employees)
        {
            foreach (var employee in employees)
            {
                _employees.Remove(employee);
            }
        }
    }
}