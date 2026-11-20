namespace LifeManagementApp;

public partial class App : Application
{
    private readonly HomePage _homePage;

    // constructor that receives the homePage
    public App(HomePage homePage)
    {
        InitializeComponent();
        // Saves the HomePage
        _homePage = homePage;
    }

    // creates the mainwindow
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(_homePage));
    }
}