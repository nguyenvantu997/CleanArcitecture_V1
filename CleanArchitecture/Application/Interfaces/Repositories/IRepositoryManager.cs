using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        ITodoItemRepository TodoItemRepository { get; }
        Task SaveChangeAsync();
    }
}
