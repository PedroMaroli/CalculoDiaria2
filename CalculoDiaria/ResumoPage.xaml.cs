using System.Globalization;

namespace CalculoDiaria;

public partial class ResumoPage : ContentPage
{
    public ResumoPage(int adultos, int criancas, string suite, decimal valorDiaria, DateTime entrada, DateTime saida)
    {
        InitializeComponent();
        var brasil = new CultureInfo("pt-BR");
        int diarias = (saida - entrada).Days;
        SuiteResumo.Text = $"Suíte {suite}";
        DiariasResumo.Text = $"{diarias} {(diarias == 1 ? "noite" : "noites")} de hospedagem";
        AdultosResumo.Text = adultos.ToString();
        CriancasResumo.Text = criancas.ToString();
        EntradaResumo.Text = entrada.ToString("dd/MM/yyyy");
        SaidaResumo.Text = saida.ToString("dd/MM/yyyy");
        ValorDiariaResumo.Text = valorDiaria.ToString("C", brasil);
        QuantidadeResumo.Text = diarias.ToString();
        TotalResumo.Text = (valorDiaria * diarias).ToString("C", brasil);
    }

    private async void VoltarClicado(object sender, EventArgs e) => await Navigation.PopAsync();
}
