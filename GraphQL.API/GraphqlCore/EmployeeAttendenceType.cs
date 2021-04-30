
using GraphQL.API.Infrastructure.DBContext;
using GraphQL.Types;

namespace GraphQL.API.GraphqlCore
{
    public class EmployeeAttendenceType : ObjectGraphType<Employee>
    {
        public EmployeeAttendenceType()
        {
            Field(x => x.Id).Description(" id.");

            Field(x => x.EmployeeId).Description("Employee id.");
            Field(x => x.EmployeeName).Description("Employee name.");
            Field(x => x.AttendenceDate).Description("Attendence Date");

          
        }
    }
}