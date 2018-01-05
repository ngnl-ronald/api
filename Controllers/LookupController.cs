using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LookupController : Controller
    {
        Entities.DataContext dbContext;

        public LookupController(Entities.DataContext dbContext) => this.dbContext = dbContext;

        [HttpGet("{lookup_table}")]
        public async Task<IEnumerable<Lookup>> Get(string lookup_table)
        {
            return await dbContext.Lookups.FromSql("SELECT * FROM get_lookup({0})", lookup_table).ToArrayAsync();
        }
    }
}
