namespace CalculoDiaria;

public partial class SobrePage : ContentPage
{
    public SobrePage()
    {
        InitializeComponent();
    }

    private async void VoltarClicado(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
