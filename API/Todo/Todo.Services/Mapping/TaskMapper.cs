using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = Todo.Models.Entities.Task;
using Todo.DTOs.Requests;
using Todo.DTOs.Responses;

namespace Todo.Services.Mapping
{
    public static class TaskMapper
    {
        public static TaskRequest ToRequest(Task entity)
        {
            return new TaskRequest
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DueDate = entity.DueDate,
                IsCompleted = entity.IsCompleted,
                Priority = entity.Priority,
                CompletedOn = entity.CompletedOn,
            };
        }

        public static Task ToEntity(TaskRequest request)
        {
            return new Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = request.IsCompleted,
                Priority = request.Priority,
                CompletedOn = request.CompletedOn,
            };
        }

        public static TaskResponse ToResponse(Task entity)
        {
            return new TaskResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DueDate = entity.DueDate,
                IsCompleted = entity.IsCompleted,
                Priority = entity.Priority,
                CompletedOn = entity.CompletedOn,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }
    }
}
