using SampleRestApi.Database.Models;
using SampleRestApi.Enums;
using System;
using System.Linq.Dynamic.Core;
using SampleRestApi.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace SampleRestApi.Services;

public interface IService<T, K> where T : class
{
    Task<IEnumerable<T>> GetModelsAsync();
    Task<T> GetModelByIdAsync(K modelId);

    private IEnumerable<T> GetSortedModels(IEnumerable<T> models, string attribute, OrderBy orderBy)
        => models.OrderBy(attribute, orderBy);

    async Task<IEnumerable<T>> GetModelsAsync(string attribute, OrderBy orderBy)
    {
        var models = await GetModelsAsync();
        return GetSortedModels(models, attribute, orderBy);
    }

    private IEnumerable<T> GetPaddingModels(IEnumerable<T> models, int? pageSize, int pageNumber, int? limit)
    {
        if (pageSize is int || limit is int)
        {
            if (limit is int _limit && _limit > 0)
                pageSize = pageSize is int _pageSize ? Math.Min(_pageSize, _limit) : _limit;

            return models.Skip((pageNumber - 1) * pageSize!.Value).Take(pageSize!.Value);
        }

        return models;
    }

    async Task<IEnumerable<T>> GetModelsAsync(int? pageSize, int pageNumber, int? limit)
    {
        var models = await GetModelsAsync();
        return GetPaddingModels(models, pageSize, pageNumber, limit);
    }

    async Task<IEnumerable<T>> GetModelsAsync(string attribute, OrderBy orderBy, int? pageSize, int pageNumber, int? limit)
    {
        var models = await GetModelsAsync();
        return GetPaddingModels(GetSortedModels(models, attribute, orderBy), pageSize, pageNumber, limit);
    }


    Task CreateModelAsync(T model);
    Task UpdateModelAsync(T model);
    Task DeleteModelAsync(K modelId);
}
