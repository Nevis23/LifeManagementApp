
using LifeManagementApp.Models;
using LifeManagementApp.ViewModels;

namespace LifeManagementApp;

public partial class TodoPage : ContentPage
{
    // store todoviewmodel
    private readonly TodoViewModel _viewModel;

    // receives the ViewModel
    public TodoPage(TodoViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        // connects to user interface
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // loads all todos
        await _viewModel.LoadTodosAsync();
    }

    private async void OnTodoCheckedChanged(
        object sender,
        CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox &&
            checkBox.BindingContext is Todo todo)
        {
            await _viewModel.ToggleTodoCompletedCommand.ExecuteAsync(todo);
        }
    }

    // returns to the dashboard
    private async void OnBackToDashboardClicked(
        object sender,
        EventArgs e)
        {
            await Navigation.PopAsync();
    }
}
