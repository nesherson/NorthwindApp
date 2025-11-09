using NorthwindApp.Common;
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
    }

    public Task<Result<RegionResponse>> GetRegionAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<RegionResponse>>> GetPaginatedRegionsAsync(string? sortColumn = null, string sortOrder = "desc", int pageIndex = 1,
        int pageSize = 25)
    {
        throw new NotImplementedException();
    }
}