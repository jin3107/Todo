using MayNghien.Infrastructures.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Models.Data;
using Todo.Models.Entities;
using Task = Todo.Models.Entities.Task;

namespace Todo.Repositories.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task, ApplicationDbContext, ApplicationUser>
    {
    }
}
