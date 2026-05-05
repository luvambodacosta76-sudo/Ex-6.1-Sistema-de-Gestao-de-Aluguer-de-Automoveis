namespace GestaoAluguerAutomoveis;

public class Automovel
{
    public string Placa { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Ano { get; set; }
    public double Quilometragem { get; private set; }

    public Automovel()
    {
        Placa = "N/A";
        Marca = "N/A";
        Modelo = "N/A";
        Ano = 0;
        Quilometragem = 0;
    }

    public Automovel(string placa, string marca, string modelo, int ano, double quilometragem)
    {
        Placa = placa;
        Marca = marca;
        Modelo = modelo;
        Ano = ano;
        Quilometragem = quilometragem;
    }

    public void AtualizarQuilometragem(double novaLeitura)
    {
        if (novaLeitura < Quilometragem)
        {
            Console.WriteLine("A quilometragem não pode ser inferior à atual.");
            return;
        }

        Quilometragem = novaLeitura;
        Console.WriteLine($"Quilometragem atualizada para {Quilometragem} km.");
    }

    public void MostrarResumo()
    {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Placa:      {Placa}");
        Console.WriteLine($"Marca:      {Marca}");
        Console.WriteLine($"Modelo:     {Modelo}");
        Console.WriteLine($"Ano:        {Ano}");
        Console.WriteLine($"Km atual:   {Quilometragem} km");
        Console.WriteLine("------------------------------------------------");
    }
}