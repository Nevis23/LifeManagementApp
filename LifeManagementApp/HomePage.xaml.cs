using LifeManagementApp.ViewModels;

namespace LifeManagementApp;

public partial class HomePage : ContentPage
{
    private readonly TodoPage _todoPage;
    private readonly HomeViewModel _viewModel;

    public HomePage(
        TodoPage todoPage,
        HomeViewModel viewModel)
    {
        InitializeComponent();

        _todoPage = todoPage;
        _viewModel = viewModel;

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