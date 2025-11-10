using NorthwindApp.Application.Common.Constants;
using NorthwindApp.Common;
using NorthwindApp.Common.Collections;
using NorthwindApp.Domain;
using NorthwindApp.Infrastructure;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.Application;

public class RegionService : IRegionService
{
    private readonly NorthwindAppDbContext _dbContext;

    public RegionService(NorthwindAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<RegionResponse>> CreateRegionAsync(RegionCreateRequest request)
    {
        var newRegion = new Region
        {
            Description = request.Description
        };
        
        _dbContext.Regions.Add(newRegion);
        await _dbContext.SaveChangesAsync();
        
        return new RegionResponse(newRegion.Id,  newRegion.Description, newRegion.DateCreated);
    }

    public async Task<Result<RegionResponse>> UpdateRegionAsync(RegionUpdateRequest request)
    {
        var regionToUpdate = await _dbContext.Regions
            .FindAsync(request.Id);
        
        if (regionToUpdate is null)
            return Result.Failure<RegionResponse>(RegionErrors.RegionNotFound(request.Id));
        
        regionToUpdate.Description = request.Description;
        
        _dbContext.Regions.Update(regionToUpdate);
        await _dbContext.SaveChangesAsync();
        
        return new RegionResponse(regionToUpdate.Id, regionToUpdate.Description, regionToUpdate.DateCreated, regionToUpdate.DateModified);
    }

    public async Task<Result> DeleteRegionAsync(int id)
    {
        var regionToDelete = await _dbContext.Regions
            .FindAsync(id);
        
        if (regionToDelete is null)
            return Result.Failure<RegionResponse>(RegionErrors.RegionNotFound(id));
        
        _dbContext.Regions.Remove(regionToDelete);
        await _dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<RegionResponse>> GetRegionAsync(int id)
    {
        var region = await _dbContext.Regions
            .FindAsync(id);
        
        if (region is null)
            return Result.Failure<RegionResponse>(RegionErrors.RegionNotFound(id));
        
        return new RegionResponse(region.Id, region.Description, region.DateCreated, region.DateModified);
    }

    public async Task<Result<PaginatedList<RegionResponse>>> GetPaginatedRegionsAsync(string? sortProperty = null, string sortOrder = "desc", int pageIndex = 1,
        int pageSize = 25)
    {
        var projection = _dbContext.Regions
            .AsQueryable()
            .OrderByDynamic(sortProperty ?? SortingConstants.DateCreated, sortOrder)
            .Select(x => new RegionResponse(x.Id, x.Description, x.DateCreated, x.DateModified));

        var result = await PaginatedList<RegionResponse>.CreateAsync(projection, pageIndex, pageSize);

        return result;
    }
}

