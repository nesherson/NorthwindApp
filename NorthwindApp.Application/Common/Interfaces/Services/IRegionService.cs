using NorthwindApp.Common;
using NorthwindApp.Domain;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IRegionService
{
    Task<Result<RegionResponse>> CreateRegionAsync(RegionCreateRequest request);
    Task<Result<RegionResponse>> UpdateRegionAsync(RegionUpdateRequest request);
    Task<Result> DeleteRegionAsync(int id);
    Task<Result<RegionResponse>>  GetRegionAsync(int id);
    Task<Result<List<RegionResponse>>>  GetPaginatedRegionsAsync(string? sortColumn = null, string sortOrder = "desc", int pageIndex = 1, int pageSize= 25);
}