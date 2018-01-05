using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Enums;
using WebApi.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi
{
    
    
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        Entities.DataContext dbContext;

        public EmployeesController(Entities.DataContext dbContext) => this.dbContext = dbContext;
        // GET: api/values

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return dbContext.Employees;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return dbContext.Employees.Where(t => t.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public Employee Post([FromBody]Employee value)
        {
            dbContext.Employees.Add(value);
            dbContext.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Employee Put(int id, [FromBody]Employee value)
        {
            var entity = dbContext.Employees.Where(t => t.Id == id).FirstOrDefault();
            entity.GivenName = value.GivenName;
            entity.Surname = value.Surname;
            dbContext.SaveChanges();
            return entity;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Employee Delete(int id)
        {
            var entity = dbContext.Employees.Where(t => t.Id == id).FirstOrDefault();
            dbContext.Employees.Remove(entity);
            dbContext.SaveChanges();
            return entity;
        }
    }
}
