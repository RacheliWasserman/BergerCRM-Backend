using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CRM.BLL.Common.Pagination;
using CRM.BLL.Common;
using CRM.BLL.Modules.Units.DTOs;
using CRM.BLL.Modules.Units.Interfaces;
using CRM.DAL.Modules.Projects.Repositories;

namespace CRM.BLL.Modules.Units.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepo;

        public UnitService(IUnitRepository unitRepo)
        {
            _unitRepo = unitRepo;
        }

        public async Task<PagedResult<UnitListDto>> GetPagedAsync(PageRequest pageRequest, object filter)
        {
            // filter object expected to be a dynamic anonymous from controller; keep minimal
            int? projectId = null;
            int? buildingId = null;
            int? floorId = null;
            int? unitStatusId = null;
            int? unitTypeId = null;
            string? search = null;

            if (filter is System.Text.Json.JsonElement je && je.ValueKind == System.Text.Json.JsonValueKind.Object)
            {
                if (je.TryGetProperty("projectId", out var pj) && pj.ValueKind == System.Text.Json.JsonValueKind.Number) projectId = pj.GetInt32();
                if (je.TryGetProperty("buildingId", out var bj) && bj.ValueKind == System.Text.Json.JsonValueKind.Number) buildingId = bj.GetInt32();
                if (je.TryGetProperty("floorId", out var fj) && fj.ValueKind == System.Text.Json.JsonValueKind.Number) floorId = fj.GetInt32();
                if (je.TryGetProperty("unitStatusId", out var usj) && usj.ValueKind == System.Text.Json.JsonValueKind.Number) unitStatusId = usj.GetInt32();
                if (je.TryGetProperty("unitTypeId", out var utj) && utj.ValueKind == System.Text.Json.JsonValueKind.Number) unitTypeId = utj.GetInt32();
                if (je.TryGetProperty("search", out var sj) && sj.ValueKind == System.Text.Json.JsonValueKind.String) search = sj.GetString();
            }

            var data = await _unitRepo.GetPagedAsync(pageRequest.Page, pageRequest.PageSize, projectId, buildingId, floorId, unitStatusId, unitTypeId, search);

            var items = data.Items.Select(u => new UnitListDto
            {
                Id = u.Id,
                UnitNumber = u.UnitNumber,
                ProjectId = u.Floor?.Building?.Project?.Id ?? 0,
                ProjectName = u.Floor?.Building?.Project?.Name,
                BuildingId = u.Floor?.Building?.Id ?? 0,
                BuildingName = u.Floor?.Building?.Name,
                FloorId = u.FloorId,
                FloorDisplay = u.Floor != null ? (u.Floor.Name ?? ("Floor " + u.Floor.FloorNumber)) : null,
                UnitTypeId = u.UnitTypeId,
                UnitTypeName = u.UnitType?.Name,
                UnitStatusId = u.UnitStatusId,
                UnitStatusName = u.UnitStatus?.Name,
                CurrentPrice = u.CurrentPrice,
                TotalArea = u.TotalArea,
                BuiltArea = u.BuiltArea
            }).ToList();

            return new PagedResult<UnitListDto> { Items = items, TotalCount = data.TotalCount, Page = pageRequest.Page, PageSize = pageRequest.PageSize };
        }

        public async Task<ServiceResult<UnitDetailsDto>> GetByIdAsync(int id)
        {
            var u = await _unitRepo.GetByIdWithDetailsAsync(id);
            if (u == null) return ServiceResult<UnitDetailsDto>.NotFound();

            var dto = new UnitDetailsDto
            {
                Id = u.Id,
                UnitNumber = u.UnitNumber,
                ProjectId = u.Floor?.Building?.Project?.Id ?? 0,
                ProjectName = u.Floor?.Building?.Project?.Name,
                BuildingId = u.Floor?.Building?.Id ?? 0,
                BuildingName = u.Floor?.Building?.Name,
                FloorId = u.FloorId,
                FloorNumber = u.Floor?.FloorNumber ?? 0,
                FloorName = u.Floor?.Name,
                UnitTypeId = u.UnitTypeId,
                UnitTypeName = u.UnitType?.Name,
                UnitStatusId = u.UnitStatusId,
                UnitStatusName = u.UnitStatus?.Name,
                CurrentPrice = u.CurrentPrice,
                TotalArea = u.TotalArea,
                BuiltArea = u.BuiltArea,
                BalconyArea = u.BalconyArea,
                GardenArea = u.GardenArea,
                RoofArea = u.RoofArea,
                PricePerSquareMeter = u.PricePerSquareMeter,
                NumberOfRooms = u.NumberOfRooms,
                NumberOfBedrooms = u.NumberOfBedrooms,
                NumberOfBathrooms = u.NumberOfBathrooms,
                MarketingDescription = u.MarketingDescription,
                InternalNotes = u.InternalNotes
            };

            return ServiceResult<UnitDetailsDto>.Success(dto);
        }

        public async Task<List<UnitLookupDto>> GetLookupAsync(int? projectId = null, int? unitStatusId = null)
        {
            var list = await _unitRepo.GetLookupAsync(projectId, unitStatusId);
            return list.Select(u => new UnitLookupDto
            {
                Id = u.Id,
                Display = u.UnitNumber + (u.Floor?.Building?.Project != null ? (" - " + u.Floor.Building.Project.Name) : ""),
                ProjectId = u.Floor?.Building?.Project?.Id ?? 0,
                UnitStatusId = u.UnitStatusId,
                UnitStatusName = u.UnitStatus?.Name
            }).ToList();
        }
    }
}
