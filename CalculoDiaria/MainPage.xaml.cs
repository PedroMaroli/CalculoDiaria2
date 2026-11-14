using System.Globalization;

namespace CalculoDiaria;

public partial class MainPage : ContentPage
{
    private readonly CultureInfo _brasil = new("pt-BR");
    private int _adultos = 1;
    private int _criancas;
    private int _suiteSelecionada = 1;
    private readonly string[] _nomes = ["Standard", "Luxo", "Premium"];
    private readonly decimal[] _precos = [220m, 340m, 490m];

    public MainPage()
    {
        InitializeComponent();
        CheckInPicker.MinimumDate = DateTime.Today;
        CheckInPicker.Date = DateTime.Today;
        CheckOutPicker.MinimumDate = DateTime.Today.AddDays(1);
        CheckOutPicker.Date = DateTime.Today.AddDays(1);
        AtualizarTela();
    }

    private void AtualizarTela()
    {
        AdultosLabel.Text = _adultos.ToString();
        CriancasLabel.Text = _criancas.ToString();
        Border[] cartoes = [StandardCard, LuxoCard, PremiumCard];
        Label[] marcadores = [StandardCheck, LuxoCheck, PremiumCheck];
        for (int i = 0; i < cartoes.Length; i++)
        {
            bool selecionado = i == _suiteSelecionada;
            cartoes[i].Stroke = Color.FromArgb(selecionado ? "#5844D2" : "#DFE4F4");
            cartoes[i].BackgroundColor = Color.FromArgb(selecionado ? "#F8F7FF" : "#FFFFFF");
            cartoes[i].StrokeThickness = selecionado ? 2.5 : 1.5;
            marcadores[i].Text = selecionado ? "●" : "○";
            marcadores[i].TextColor = Color.FromArgb(selecionado ? "#5844D2" : "#A8ACBF");
        }
        int noites = Math.Max(0, (CheckOutPicker.Date - CheckInPicker.Date).Days);
        PeriodoLabel.Text = $"{noites} {(noites == 1 ? "diária selecionada" : "diárias selecionadas")}";
        NoitesLabel.Text = $"{noites} {(noites == 1 ? "noite" : "noites")}";
        TotalLabel.Text = (_precos[_suiteSelecionada] * noites).ToString("C", _brasil);
    }

    private void SelecionarStandard(object sender, TappedEventArgs e) { _suiteSelecionada = 0; AtualizarTela(); }
    private void SelecionarLuxo(object sender, TappedEventArgs e) { _suiteSelecionada = 1; AtualizarTela(); }
    private void SelecionarPremium(object sender, TappedEventArgs e) { _suiteSelecionada = 2; AtualizarTela(); }
    private void DiminuirAdultos(object sender, EventArgs e) { if (_adultos > 1) _adultos--; AtualizarTela(); }
    private void AumentarAdultos(object sender, EventArgs e) { if (_adultos < 20) _adultos++; AtualizarTela(); }
    private void DiminuirCriancas(object sender, EventArgs e) { if (_criancas > 0) _criancas--; AtualizarTela(); }
    private void AumentarCriancas(object sender, EventArgs e) { if (_criancas < 20) _criancas++; AtualizarTela(); }

    private void CheckInAlterado(object sender, DateChangedEventArgs e)
    {
        CheckOutPicker.MinimumDate = e.NewDate.AddDays(1);
        if (CheckOutPicker.Date <= e.NewDate)
            CheckOutPicker.Date = e.NewDate.AddDays(1);
        AtualizarTela();
    }

    private void CheckOutAlterado(object sender, DateChangedEventArgs e) => AtualizarTela();

    private async void AvancarClicado(object sender, EventArgs e)
    {
        if (CheckOutPicker.Date <= CheckInPicker.Date)
        {
            await DisplayAlert("Confira as datas", "O check-out precisa ser depois do check-in.", "OK");
            return;
        }
        if (_adultos + _criancas > 8)
        {
            await DisplayAlert("Quantidade de hóspedes", "Para esta simulação, selecione até 8 hóspedes.", "OK");
            return;
        }
        await Navigation.PushAsync(new ResumoPage(_adultos, _criancas, _nomes[_suiteSelecionada], _precos[_suiteSelecionada], CheckInPicker.Date, CheckOutPicker.Date));
    }

    private async void AbrirSobreClicado(object sender, EventArgs e) => await Navigation.PushAsync(new SobrePage());
}
