using Microsoft.AspNetCore.Mvc;
using SampleRestApi.Enums;
using SampleRestApi.Services;
using System.Collections.Generic;
using System;
using SampleRestApi.Database.Models;
using SampleRestApi.Extensions;

namespace SampleRestApi.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class ApiController : ControllerBase
{

}
