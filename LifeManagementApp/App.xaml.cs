
namespace LifeManagementApp;

public partial class App : Application
{
    private readonly TodoPage _todoPage;

    public App(TodoPage todoPage)
    {
        InitializeComponent();

        _todoPage = todoPage;
    }

    protected override Window CreateWindow(
        IActivationState? activationState)
    {
        return new Window(_todoPage);
    }
}
