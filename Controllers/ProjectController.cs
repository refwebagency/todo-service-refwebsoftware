using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;

namespace TodoService.Controllers
{
    [Route("todo/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        public ProjectController(){}

        [HttpPost]
        public ActionResult testCom()
        {
            Console.WriteLine("Request Post ok pour todo");

            return Ok("Request Post ok pour todo");
        }
        
    }
}