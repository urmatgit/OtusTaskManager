using TaskboardService.DataAccess.Entity;

namespace TaskboardService.Api.Models
{
    public static class ModelMapping
    {
        public static TaskboardDto ToDto(this Taskboard entity) 
        {
            return new TaskboardDto()
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Title = entity.Title,
                Status = entity.Status,
                SortOrder = entity.SortOrder,
                Columns = entity.Columns.Select(c => new TaskboardColumnDto()
                {
                    Id = c.Id,
                    Title = c.Title,
                    SortOrder = c.SortOrder,
                    VipLimit = c.VipLimit,
                }).OrderBy(t => t.SortOrder).ToList()
            };
        }

        public static Taskboard ToEntity(this TaskboardDto dto, Taskboard entity) 
        {
            entity.Status = dto.Status;
            entity.SortOrder = dto.SortOrder;
            entity.Title = dto.Title;
            return entity;
        }

        public static TaskboardColumn ToEntity(this TaskboardColumnDto dto, TaskboardColumn entity) 
        {
            entity.Title = dto.Title;
            entity.SortOrder = dto.SortOrder;
            entity.VipLimit = dto.VipLimit;
            return entity;
        }
    }
}