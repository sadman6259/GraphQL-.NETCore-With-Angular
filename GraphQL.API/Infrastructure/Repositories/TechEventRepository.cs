using GraphQL.API.Infrastructure.DBContext;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
//using GraphQL.API.Domain;


namespace GraphQL.API.Infrastructure.Repositories
{
    /// <summary>  
    /// TechEventRepository.  
    /// </summary>  
    [EnableCors("AllowMyOrigin")]
    public class TechEventRepository: ITechEventRepository
    {

        /// <summary>  
        /// The _context.  
        /// </summary>  
        private readonly TechEventDBContext _context;

        public TechEventRepository(TechEventDBContext context)
        {
            this._context = context;
        }
        [EnableCors("AllowMyOrigin")]
        public async Task<List<Participant>> GetParticipantInfoByEventId(int id)
        {
            return await (from ep in this._context.EventParticipants
                          join p in this._context.Participant on ep.ParticipantId equals p.ParticipantId
                          where ep.EventId == id
                          select p).ToListAsync();
        }
        [EnableCors("AllowMyOrigin")]
        public async Task<TechEventInfo> GetTechEventById(int id)
        {
            return await Task.FromResult(_context.TechEventInfo.FirstOrDefault(i => i.EventId == id));
        }
        [EnableCors("AllowMyOrigin")]
        public async Task<TechEventInfo[]> GetTechEvents()
        {
            return _context.TechEventInfo.ToArray();
        }
        [EnableCors("AllowMyOrigin")]
        public async Task<Participant[]> GetParticipants()
        {
            return _context.Participant.ToArray();

        }
        [EnableCors("AllowMyOrigin")]
        //public async Task<Employee[]> GetEmployeesAttendence()
        //{
        //    var list = _context.Employee.FromSql($"call EmployeeAttendenceSP()").ToArray();
        //    return list;
        //}
        public async Task<List<Employee>> GetEmployeesAttendence(string Date)
        {
            var date = new SqlParameter("AttendenceDate", Date);

        
            var empinfo = _context.Employee
                .FromSql("EXECUTE dbo.EmployeeAttendenceSP @AttendenceDate={0}", Date)
                .ToList();
            return empinfo;



        }
    }
}