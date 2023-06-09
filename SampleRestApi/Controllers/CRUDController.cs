using Microsoft.AspNetCore.Mvc;
using SampleRestApi.Database.Models;
using SampleRestApi.Enums;
using SampleRestApi.Extensions;
using SampleRestApi.Services;
using System.Collections.Generic;
using System;

namespace SampleRestApi.Controllers;

public abstract class CRUDController<T, K> : ApiController where T : class
{
    protected readonly IService<T, K> service;
    public CRUDController(IService<T, K> service)
        => this.service = service;


    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetModelByIdAsync(K id)
        => await CheckResponseAsync(async () => Ok(await service.GetModelByIdAsync(id)));

    [HttpGet]
    public virtual async Task<IActionResult> GetModelsAsync(string? attribute, string? order, int? pageSize, int? pageNumber, int? limit)
        => await CheckResponseAsync(async () =>
        {
            bool isSorting = order.TryParseToOrderBy(out OrderBy? orderBy) && !string.IsNullOrEmpty(attribute) ;
            bool isPadding = ((pageSize is int _pageSize && _pageSize > 0) || (limit is int _limit && _limit > 0))
                && pageNumber is int _pageNumber && _pageNumber > 0;

            IEnumerable<T> models;

            if (isSorting && isPadding)
                models = await service.GetModelsAsync(attribute!, orderBy!.Value, pageSize, pageNumber!.Value, limit);
            else if (isSorting)
                models = await service.GetModelsAsync(attribute!, orderBy!.Value);
            else if (isPadding)
                models = await service.GetModelsAsync(pageSize, pageNumber!.Value, limit);
            else
                models = await service.GetModelsAsync();

            return Ok(models);
        });

    [HttpPost]
    public virtual async Task<IActionResult> CreateModelAsync([FromBody] T model)
        => await CheckResponseAsync(async () =>
        {
            await service.CreateModelAsync(model);
            return CreatedAtAction(null, null, model);
        });

    [HttpPut]
    public virtual async Task<IActionResult> UpdateModelAsync([FromBody] T model)
        => await CheckResponseAsync(async () =>
        {
            await service.UpdateModelAsync(model);
            return NoContent();
        });

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteModelAsync(K id)
        => await CheckResponseAsync(async () =>
        {
            await service.DeleteModelAsync(id);
            return NoContent();
        });


    [NonAction]
    protected async Task<IActionResult> CheckResponseAsync(Func<Task<IActionResult>> actionResult)
    {
        try
        {
            return await actionResult();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
