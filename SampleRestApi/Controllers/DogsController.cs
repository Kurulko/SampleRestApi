using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleRestApi.Database.Models;
using SampleRestApi.Database;
using System.Linq;
using SampleRestApi.Services;
using SampleRestApi.Extensions;

namespace SampleRestApi.Controllers;

public class DogsController : CRUDController<Dog, long>
{
    public DogsController(IDogService dogService) : base(dogService) { }
}





