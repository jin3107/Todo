﻿using Azure.Core;
using LinqKit;
using MayNghien.Infrastructures.Models.Requests;
using MayNghien.Infrastructures.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.DTOs.Requests;
using Todo.DTOs.Responses;
using Todo.Repositories.Interfaces;
using Todo.Services.Interfaces;
using Todo.Services.Mapping;
using Task = Todo.Models.Entities.Task;
using static MayNghien.Infrastructures.Helpers.SearchHelper;

namespace Todo.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<AppResponse<TaskResponse>> CreateAsync(TaskRequest request)
        {
            var result = new AppResponse<TaskResponse>();
            try
            {
                var newTask = TaskMapper.ToEntity(request);
                newTask.Id = Guid.NewGuid();
                newTask.Title = request.Title;
                newTask.Description = request.Description;
                newTask.DueDate = request.DueDate;
                newTask.Priority = request.Priority;
                newTask.CreatedOn = DateTime.UtcNow;
                newTask.IsCompleted = false;
                newTask.CompletedOn = null;
                await _taskRepository.AddAsync(newTask);

                var response = TaskMapper.ToResponse(newTask);
                result.BuildResult(response, "Task created successfully.");
            }
            catch (Exception ex)
            {
                result.BuildError(ex.Message + " " + ex.StackTrace);
            }
            return result;
        }

        public async Task<AppResponse<string>> DeleteAsync(Guid id)
        {
            var result = new AppResponse<string>();
            try
            {
                var task = await _taskRepository.GetAsync(id);
                if (task == null || task.IsDeleted == true)
                    return result.BuildError("Task not found or deleted.");
                task.IsDeleted = true;
                await _taskRepository.EditAsync(task);
                result.BuildResult("Task deleted successfully.");
            }
            catch (Exception ex)
            {
                result.BuildError(ex.Message + " " + ex.StackTrace);
            }
            return result;
        }

        public async Task<AppResponse<TaskResponse>> GetByIdAsync(Guid id)
        {
            var result = new AppResponse<TaskResponse>();
            try
            {
                var task = await _taskRepository.FindByAsync(p => p.Id == id).FirstOrDefaultAsync();
                if (task == null || task.IsDeleted == true)
                    return result.BuildError("Task not found or deleted.");

                var response = TaskMapper.ToResponse(task);
                result.BuildResult(response);
            }
            catch (Exception ex)
            {
                result.BuildError(ex.Message + " " + ex.StackTrace);
            }
            return result;
        }

        public async Task<AppResponse<SearchResponse<TaskResponse>>> SearchAsync(SearchRequest request)
        {
            var result = new AppResponse<SearchResponse<TaskResponse>>();
            try
            {
                var query = BuildFilterExpression(request.Filters!);
                var numOfRecords = await _taskRepository.CountRecordsAsync(query);
                var tasks = _taskRepository.FindByPredicate(query).AsQueryable();

                if (request.SortBy != null)
                    tasks = _taskRepository.AddSort(tasks, request.SortBy);
                else
                    tasks = tasks.OrderBy(x => x.Title);

                int pageIndex = request.PageIndex ?? 1;
                int pageSize = request.PageSize ?? 10;
                int startIndex = (pageIndex - 1) * pageSize;
                var classList = await tasks.Skip(startIndex).Take(pageSize).ToListAsync();
                var dtoList = classList.Select(TaskMapper.ToResponse).ToList();
                var searchResponse = new SearchResponse<TaskResponse>
                {
                    TotalPages = CalculateNumOfPages(numOfRecords, pageSize),
                    TotalRows = numOfRecords,
                    CurrentPage = pageIndex,
                    Data = dtoList,
                    RowsPerPage = pageSize,
                };
                result.Data = searchResponse;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.BuildError(ex.Message + " " + ex.StackTrace);
            }
            return result;
        }

        private ExpressionStarter<Task> BuildFilterExpression(List<Filter> filters)
        {
            try
            {
                var predicate = PredicateBuilder.New<Task>(true);
                if (filters != null)
                {
                    foreach (var filter in filters)
                    {
                        switch (filter.FieldName)
                        {
                            case "Title":
                                if (!string.IsNullOrEmpty(filter.Value))
                                    predicate = predicate.And(x => x.Title.Contains(filter.Value));
                                break;

                            default: break;
                        }
                    }
                }

                predicate = predicate.And(x => x.IsDeleted == false);
                return predicate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        public async Task<AppResponse<TaskResponse>> UpdateAsync(TaskRequest request)
        {
            var result = new AppResponse<TaskResponse>();
            try
            {
                var task = await _taskRepository.GetAsync(request.Id);
                if (task == null || task.IsDeleted == true)
                    return result.BuildError("Task not found or deleted.");

                task.Title = request.Title;
                task.Description = request.Description;
                task.DueDate = request.DueDate;
                task.ModifiedOn = DateTime.UtcNow;
                task.IsCompleted = request.IsCompleted;
                task.Priority = request.Priority;
                task.CompletedOn = request.IsCompleted ? request.CompletedOn ?? DateTime.UtcNow : null;
                await _taskRepository.EditAsync(task);
                var response = TaskMapper.ToResponse(task);
                result.BuildResult(response, "Task updated successfully.");
            }
            catch (Exception ex)
            {
                result.BuildError(ex.Message + " " + ex.StackTrace);
            }
            return result;
        }
    }
}
