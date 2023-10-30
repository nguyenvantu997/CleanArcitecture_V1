using Application.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _dbContext;
        public RepositoryManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        ITodoItemRepository IRepositoryManager.TodoItemRepository => new TodoItemRepository(_dbContext);

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
