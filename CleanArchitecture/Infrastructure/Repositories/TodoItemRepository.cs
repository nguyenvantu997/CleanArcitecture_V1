using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TodoItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
