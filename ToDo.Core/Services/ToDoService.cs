using Microsoft.EntityFrameworkCore;
using ToDo.Core.Contexts;
using ToDo.Domain.Entities.ToDo;

namespace ToDo.Core.Services
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ToDoItem> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<ToDoItem>> GetCompletedAsync(CancellationToken cancellationToken = default);
        Task<List<ToDoItem>> GetPendingAsync(CancellationToken cancellationToken = default);
        Task<int> CreateAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default);
        Task<bool> CompleteAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default);
    }

    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _context;

        public ToDoService(ToDoDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ToDoItems
                                 .OrderByDescending(x => x.CreatedAt)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<ToDoItem> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.ToDoItems
                                 .Where(x => x.Id == id)
                                 .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<ToDoItem>> GetCompletedAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ToDoItems
                                 .Where(x => x.IsCompleted)
                                 .OrderByDescending(x => x.CompletedAt)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<List<ToDoItem>> GetPendingAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ToDoItems
                                 .Where(x => !x.IsCompleted)
                                 .OrderByDescending(x => x.CreatedAt)
                                 .ToListAsync(cancellationToken);
        }

        public async Task<int> CreateAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default)
        {
            toDoItem.CreatedAt = DateTime.UtcNow;
            toDoItem.UpdatedAt = DateTime.UtcNow;

            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync(cancellationToken);
            return toDoItem.Id;
        }

        public async Task<bool> UpdateAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default)
        {
            toDoItem.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default)
        {
            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> CompleteAsync(ToDoItem toDoItem, CancellationToken cancellationToken = default)
        {
            _context.ToDoItems.Update(toDoItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
