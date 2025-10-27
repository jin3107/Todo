using MayNghien.Infrastructures.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Data;
using Todo.Models.Entities;
using Todo.Repositories.Interfaces;
using Task = Todo.Models.Entities.Task;

namespace Todo.Repositories.Implementations
{
    public class TaskRepository : GenericRepository<Task, ApplicationDbContext, ApplicationUser>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext unitOfWork) : base(unitOfWork)
        {
        }
    }
}
