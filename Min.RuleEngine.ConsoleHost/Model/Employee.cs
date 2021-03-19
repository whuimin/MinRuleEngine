using System;
using System.Text.Json.Serialization;

namespace Min.RuleEngine.ConsoleHost.Model
{
    internal class Employee
    {
        public DateTime FlexDate { get; set; }

        public string EmployeeNo { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Category { get; set; }

        public string Nationality { get; set; }

        public decimal FlexSalary { get; set; }

        public bool? Bool1 { get; set; }

        public decimal Salary { get; set; }

        [JsonIgnore]
        public int Age
        {
            get
            {
                var nowTime = DateTime.Now;
                var year = nowTime.Year - BirthDate.Year;
                if (nowTime.Month < BirthDate.Month || (nowTime.Month >= BirthDate.Month && nowTime.Day < BirthDate.Day))
                {
                    year--;
                }
                return year;
            }
        }

        [JsonIgnore]
        public int FlexAgeLastBirthday
        {
            get
            {
                var year = FlexDate.Year - BirthDate.Year;
                if (FlexDate.Month < BirthDate.Month || (FlexDate.Month >= BirthDate.Month && FlexDate.Day < BirthDate.Day))
                {
                    year--;
                }
                return year;
            }
        }
    }
}
