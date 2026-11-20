using LifeManagementApp.ViewModels;

namespace LifeManagementApp;

public partial class HomePage : ContentPage
{
    // stores page for navigation
    private readonly TodoPage _todoPage;
    private readonly HomeViewModel _viewModel;

    // receives the page and ViewModel
    public HomePage(
        TodoPage todoPage,
        HomeViewModel viewModel)
    {
        InitializeComponent();

        _todoPage = todoPage;
        _viewModel = viewModel;

        // connects viewmodel
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadImportantTodosAsync();
        await _viewModel.LoadWeatherAsync();
    }

    private async void OnOpenTodosClicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PushAsync(_todoPage);
    }
}