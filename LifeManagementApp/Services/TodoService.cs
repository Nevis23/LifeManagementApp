
using Microsoft.EntityFrameworkCore;
using LifeManagementApp.Data;
using LifeManagementApp.Models;

namespace LifeManagementApp.Services
{
    public class TodoService
    {
        // riippuvuuksien injektointi
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TodoService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task InitializeAsync()
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();
            //tarkistaa, onko tietokanta olemassa.
            await context.Database.EnsureCreatedAsync();
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();
                // hakee kaikki tehtävät tietokannasta
                return await context.Todos
                .OrderBy(t => t.Priority)
                .ToListAsync();
        }

        public async Task AddTodoAsync(Todo todo)
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();

            context.Todos.Add(todo);

            await context.SaveChangesAsync();
        }

        public async Task DeleteTodoAsync(Todo todo)
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();

            context.Todos.Remove(todo);

            await context.SaveChangesAsync();
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            await using var context =
                await _contextFactory.CreateDbContextAsync();

            context.Todos.Update(todo);

            await context.SaveChangesAsync();
        }

        public async Task<List<Todo>> GetTodosByPriorityAsync()
            {
                await using var context =
                    await _contextFactory.CreateDbContextAsync();

                return await context.Todos
                    .OrderBy(todo => todo.Priority)
                    .ThenBy(todo => todo.DueDate)
                    .ToListAsync();
            }

            public async Task<List<Todo>> GetTodosByDueDateAsync()
            {
                await using var context =
                    await _contextFactory.CreateDbContextAsync();

                return await context.Todos
                    .OrderBy(todo => todo.DueDate)
                    .ThenBy(todo => todo.Priority)
                    .ToListAsync();
            }
    }
}
