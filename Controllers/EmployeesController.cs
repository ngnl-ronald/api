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
        public IEnumerable<employee> Get()
        {
            return dbContext.employees;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public employee Get(int id)
        {
            return dbContext.employees.Where(t => t.id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public employee Post([FromBody]employee value)
        {
            dbContext.employees.Add(value);
            dbContext.SaveChanges();
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public employee Put(int id, [FromBody]employee value)
        {
            var entity = dbContext.employees.Where(t => t.id == id).FirstOrDefault();
            entity.given_name = value.given_name;
            entity.surname = value.surname;
            dbContext.SaveChanges();
            return entity;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public employee Delete(int id)
        {
            var entity = dbContext.employees.Where(t => t.id == id).FirstOrDefault();
            dbContext.employees.Remove(entity);
            dbContext.SaveChanges();
            return entity;
        }
    }
}
