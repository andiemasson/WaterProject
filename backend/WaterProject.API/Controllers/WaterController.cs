using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft .AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
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
            public IActionResult GetProjects(int pageSize = 10, int pageNum = 1, [FromQuery] List<string>? projectTypes = null)
            {
                var query = _context.Projects.AsQueryable();
                
                if (projectTypes != null && projectTypes.Any())
                {
                    query = query.Where(p => projectTypes.Contains(p.ProjectType));
                }
                
                var something = query
                    .OrderByDescending(p => p.ProjectId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                    
                var totalNumProjects = query.Count();
                                    
                var someObject = new
                {
                    Projects = something,
                    TotalNumProjects = totalNumProjects
                };
                
                return Ok(someObject);
                
                /*string? favProjType = Request.Cookies["FavoriteProjectType"];
                Console.WriteLine("---------COOKIE---------\n" + favProjType);

                HttpContext.Response.Cookies.Append("FavoriteProjectType", "Borehole Well and Hand Pump", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddMinutes(1),
                });*/
            }

            [HttpGet("GetProjectTypes")]
            public IActionResult GetProjectTypes()
            {
                var projectTypes = _context.Projects
                    .Select(p => p.ProjectType)
                    .Distinct()
                    .ToList();
                
                return Ok(projectTypes);
            }

            [HttpPost("AddProject")]
            public IActionResult AddProject([FromBody] Project newProject)
            {
                _context.Projects.Add(newProject);
                _context.SaveChanges();
                return Ok(newProject);
            }

            [HttpPut("UpdateProject/{projectId}")]
            public IActionResult UpdateProject(int projectId, [FromBody] Project updatedProject)
            {
                var existingProject = _context.Projects.Find(projectId);

                existingProject.ProjectName = updatedProject.ProjectName;
                existingProject.ProjectType = updatedProject.ProjectType;
                existingProject.ProjectRegionalProgram = updatedProject.ProjectRegionalProgram;
                existingProject.ProjectImpact = updatedProject.ProjectImpact;
                existingProject.ProjectPhase = updatedProject.ProjectPhase;
                existingProject.ProjectFunctionalityStatus = updatedProject.ProjectFunctionalityStatus;

                _context.Projects.Update(existingProject);
                _context.SaveChanges();

                return Ok(existingProject);
            }

            [HttpDelete("DeleteProject/{projectId}")]
            public IActionResult DeleteProject(int projectId)
            {
                var project = _context.Projects.Find(projectId);

                if (project == null)
                {
                    return NotFound(new {message = "Project not found"});
                }

                _context.Projects.Remove(project);
                _context.SaveChanges();

                return NoContent();
            }
        }
        
        
    }