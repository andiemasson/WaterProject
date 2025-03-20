using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft .AspNetCore.Http;
using WaterProject.API.Data;

namespace WaterProject.API.Controllers
    {
        [Route("[controller]")]
        [ApiController]
        
        public class WaterController : ControllerBase
        {
            private WaterProjectContext _context;
            
            public WaterController(WaterProjectContext temp) => _context = temp;

            [HttpGet("AllProjects")]
            public IEnumerable<Project> Get()
            {
                var something = _context.Projects.ToList();
                
                return something;
            }

            [HttpGet("FunctionalProjects")]
            public IEnumerable<Project> GetFunctionalProjects()
            {
                var something = _context.Projects.Where(p => p.ProjectFunctionalityStatus == "Functional").ToList();
                return something;
            }
        }
        
        
    }