using MayNghien.Infrastructures.Models.Requests;
using MayNghien.Infrastructures.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DTOs.Requests;
using Todo.DTOs.Responses;

namespace Todo.Services.Interfaces
{
    public interface ITaskService
    {
        Task<AppResponse<TaskResponse>> GetByIdAsync(Guid id);
        Task<AppResponse<TaskResponse>> CreateAsync(TaskRequest request);
        Task<AppResponse<TaskResponse>> UpdateAsync(TaskRequest request);
        Task<AppResponse<string>> DeleteAsync(Guid id);
        Task<AppResponse<SearchResponse<TaskResponse>>> SearchAsync(SearchRequest request);
    }
}
