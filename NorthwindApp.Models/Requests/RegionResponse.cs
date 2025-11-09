namespace NorthwindApp.Models;

public record RegionResponse(int Id, string Description, DateTime DateCreated, DateTime? DateModified = null);