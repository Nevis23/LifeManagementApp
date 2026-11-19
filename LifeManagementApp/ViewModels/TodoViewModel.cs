
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;
using System.Collections.ObjectModel;

namespace LifeManagementApp.ViewModels
{
   
    public partial class TodoViewModel : ObservableObject
    {
        private readonly TodoService _todoService;

        public ObservableCollection<Todo> Todos { get; set; }
            = new ObservableCollection<Todo>();

        [ObservableProperty]
        private string newTodoTitle = string.Empty;

        public TodoViewModel(TodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task LoadTodosAsync()
        {
            await _todoService.InitializeAsync();

            var todos = await _todoService.GetTodosAsync();

            Todos.Clear();

            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
        }

        [RelayCommand]
        private async Task AddTodoAsync()
        {
            if (string.IsNullOrWhiteSpace(NewTodoTitle))
                return;

            Todo newTodo = new Todo
            {
                Title = NewTodoTitle,
                Description = "",
                Priority = SelectedPriority switch
                {
                    "High" => 1,
                    "Medium" => 2,
                    "Low" => 3,
                    _ => 2
                },

                DueDate = NewTodoDueDate,
                IsCompleted = false
            };

            await _todoService.AddTodoAsync(newTodo);

            Todos.Add(newTodo);

            NewTodoTitle = string.Empty;
        }

        [RelayCommand]
        private async Task DeleteTodoAsync(Todo todo)
        {
            await _todoService.DeleteTodoAsync(todo);

            Todos.Remove(todo);
        }

        [RelayCommand]
        private async Task ToggleTodoCompletedAsync(Todo todo)
        {
            await _todoService.UpdateTodoAsync(todo);
        }

        [RelayCommand]
        private async Task SortByPriorityAsync()
        {
            var todos = await _todoService.GetTodosByPriorityAsync();

            Todos.Clear();

            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
        }

        [RelayCommand]
        private async Task SortByDueDateAsync()
        {
            var todos = await _todoService.GetTodosByDueDateAsync();

            Todos.Clear();

            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }
        }

        [ObservableProperty]
        private DateTime newTodoDueDate = DateTime.Today;

        [ObservableProperty]
        private string selectedPriority = "Medium";

        public List<string> PriorityOptions { get; } =
            new List<string>
            {
        "High",
        "Medium",
        "Low"
            };

        [ObservableProperty]
        private Todo selectedTodo;

        [ObservableProperty]
        private string editTodoTitle = string.Empty;

        [ObservableProperty]
        private int editTodoPriority = 2;

        [ObservableProperty]
        private DateTime editTodoDueDate = DateTime.Today;

        [ObservableProperty]
        private string editTodoPriorityText = "Medium";
    }
}
