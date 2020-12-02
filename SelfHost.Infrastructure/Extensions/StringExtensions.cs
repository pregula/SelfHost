using SelfHost.Domain;
using System;

namespace SelfHost.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static JobTitleEnum ToEnum(this string str)
        {
            return (JobTitleEnum)Enum.Parse(typeof(JobTitleEnum), str);
        }
    }
}