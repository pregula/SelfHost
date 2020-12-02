using System;

namespace SelfHost.Domain
{
    public class Employee : BaseObject
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public JobTitleEnum JobTitle { get; protected set; }
        public long CompanyId { get; set; }
        public virtual Company Company { get; protected set; }

        protected Employee()
        {
        }

        public Employee(string firstName, string lastName, DateTime dateOfBirth, JobTitleEnum jobTitle)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            DateOfBirth = dateOfBirth;
            JobTitle = jobTitle;
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new Exception("Employee can not have an empty first name.");
            }

            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("Employee can not have an empty last name.");
            }

            LastName = lastName;
        }
    }
}