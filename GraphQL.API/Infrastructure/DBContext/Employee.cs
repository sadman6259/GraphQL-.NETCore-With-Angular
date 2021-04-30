using System;
using System.Collections.Generic;

namespace GraphQL.API.Infrastructure.DBContext
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string AttendenceDate { get; set; }

    }
}
