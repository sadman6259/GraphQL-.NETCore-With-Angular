using GraphQL.API.Infrastructure.Repositories;
using GraphQL.Types;


namespace GraphQL.API.GraphqlCore
{
    public class TechEventQuery : ObjectGraphType<object>
    {
        public TechEventQuery(ITechEventRepository repository)
        {
            Name = "TechEventQuery";

            Field<TechEventInfoType>(
               "event",
               arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "eventId" }),
               resolve: context => repository.GetTechEventById(context.GetArgument<int>("eventId"))
            );

            Field<ListGraphType<TechEventInfoType>>(
             "events",
             resolve: context => repository.GetTechEvents()
          );
            Field<ListGraphType<ParticipantType>>(
          "participants",
          resolve: context => repository.GetParticipants()
       );
            Field<ListGraphType<EmployeeAttendenceType>>(
       "employeesattendence",
       arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "attendenceDate" }),

       resolve: context => repository.GetEmployeesAttendence(context.GetArgument<string>("attendenceDate"))
    );
        }
    }
}