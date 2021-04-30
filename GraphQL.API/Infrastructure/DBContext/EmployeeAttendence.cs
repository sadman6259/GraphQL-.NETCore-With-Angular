using System;
using System.Collections.Generic;

namespace GraphQL.API.Infrastructure.DBContext
{
    public partial class EmployeeAttendence
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? AttendenceDate { get; set; }
    }
}
