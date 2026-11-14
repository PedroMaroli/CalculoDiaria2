namespace CalculoDiaria;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Color.FromArgb("#F3F6FF"),
            BarTextColor = Colors.Black
        };
    }
}
