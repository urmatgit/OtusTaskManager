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
                LastOpened = entity.LastOpened,
                Columns = entity.Columns.Select(col => new TaskboardColumnDto()
                {
                    Id = col.Id,
                    TaskboardId = col.TaskboardId,
                    Title = col.Title,
                    SortOrder = col.SortOrder,
                    WipLimit = col.WipLimit,
                    Color = col.Color,
                    Tasks = col.Tasks.Select(t => new TaskItemDto()
                    {
                        Id = t.Id,
                        TaskboardId = t.TaskboardId,
                        TaskboardColumnId = t.TaskboardColumnId,
                        Title = t.Title,
                        Description = t.Description,
                        Priority = t.Priority,
                        Progress = t.Progress,
                        ExecutionDate = t.ExecutionDate,
                        FactExecutionDate = t.FactExecutionDate,
                        HeaderColor = t.HeaderColor
                    }).OrderBy(ti => ti.SortOrder).ToList()
                }).OrderBy(col => col.SortOrder).ToList()
            };
        }

        public static Taskboard ToEntity(this TaskboardDto dto, Taskboard entity) 
        {
            entity.Status = dto.Status;
            entity.SortOrder = dto.SortOrder;
            entity.Title = dto.Title;
            entity.LastOpened = dto.LastOpened;
            return entity;
        }

        public static TaskboardColumn ToEntity(this TaskboardColumnDto dto, TaskboardColumn entity) 
        {
            entity.Title = dto.Title;
            entity.SortOrder = dto.SortOrder;
            entity.WipLimit = dto.WipLimit;
            entity.SortOrder = dto.SortOrder;
            entity.Color = dto.Color;
            return entity;
        }

        public static TaskItem ToEntity(this TaskItemDto dto, TaskItem entity) 
        {
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.SortOrder = dto.SortOrder;
            entity.Priority = dto.Priority;
            entity.ExecutionDate = dto.ExecutionDate;
            entity.FactExecutionDate = dto.FactExecutionDate;
            entity.Progress = dto.Progress;
            entity.HeaderColor = dto.HeaderColor;

            return entity;
        }
    }
}