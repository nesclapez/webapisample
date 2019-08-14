using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public dynamic Get()
        {
            return new
            {
                DicoProperty = new Dictionary<string, string>()
                {
                    { "MyFirstKey", "MyFirstValue" },
                    { "MySecondKey", "MySecondValue" }
                },
                DynamicPropery = new
                {
                    MyFirstPropery = "MyFirstValue",
                    MySecondProperty = "MySecondValue",
                },
                StringArrayPropery = new []
                {
                    "MyFirstValue", 
                    "MySecondValue"
                },
                DateProperty = DateTime.Now
            };
        }
    }
}
