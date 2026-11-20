
using Microsoft.EntityFrameworkCore;
using LifeManagementApp.Data;
using LifeManagementApp.Models;

namespace LifeManagementApp.Services
{
    public class TodoService
    {
        // Dependency injection for database context
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TodoService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task InitializeAsync()
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();
            // Checks if the database exists
            await context.Database.EnsureCreatedAsync();
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();
            // Gets all todos from the database and sorts them by priority
            return await context.Todos
                .OrderBy(t => t.Priority)
                .ToListAsync();
        }

        public async Task AddTodoAsync(Todo todo)
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();

            // Adds a new todo
            context.Todos.Add(todo);

            // Saves changes
            await context.SaveChangesAsync();
        }

        public async Task DeleteTodoAsync(Todo todo)
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();

            // Removes the selected todo
            context.Todos.Remove(todo);

            // Saves changes 
            await context.SaveChangesAsync();
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();

            // Updates the selected todo
            context.Todos.Update(todo);

            // Saves changes
            await context.SaveChangesAsync();
        }

        public async Task<List<Todo>> GetTodosByPriorityAsync()
            {
                await using var context =
                    await _contextFactory.CreateDbContextAsync();

                // Sorts todos
                return await context.Todos
                    .OrderBy(todo => todo.Priority)
                    .ThenBy(todo => todo.DueDate)
                    .ToListAsync();
            }

            public async Task<List<Todo>> GetTodosByDueDateAsync()
            {
                await using var context =
                    await _contextFactory.CreateDbContextAsync();

                // Sorts todos
                return await context.Todos
                    .OrderBy(todo => todo.DueDate)
                    .ThenBy(todo => todo.Priority)
                    .ToListAsync();
            }

        public async Task<List<Todo>> GetTopThreeTodosAsync()
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();

            // Sorts todos Takes only the first three todos
            return await context.Todos
                .Where(todo => !todo.IsCompleted)
                .OrderBy(todo => todo.Priority)
                .ThenBy(todo => todo.DueDate)
                .Take(3)
                .ToListAsync();
        }
    }
}
