namespace GestaoAluguerAutomoveis;

internal class Programa
{
    private static readonly Frota _frota = new();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== GESTÃO DE FROTA PARA ALUGUER ===\n");

        while (true)
        {
            Console.Clear();
            ExibirMenu();
            string escolha = Console.ReadLine()?.Trim() ?? string.Empty;
            if (ProcessarEscolha(escolha))
                break;
        }

        Console.WriteLine("\nEncerrando a aplicação. Obrigado pela preferência!");
    }

    static void ExibirMenu()
    {
        Console.WriteLine("=============== MENU PRINCIPAL ===============");
        Console.WriteLine("1 - Registar automóvel novo");
        Console.WriteLine("2 - Exibir frota completa");
        Console.WriteLine("3 - Atualizar quilometragem");
        Console.WriteLine("4 - Consultar automóvel por placa");
        Console.WriteLine("0 - Sair");
        Console.Write("Selecione uma opção: ");
    }

    static bool ProcessarEscolha(string escolha)
    {
        switch (escolha)
        {
            case "1": RegistrarAutomovel(); break;
            case "2": ExibirFrota(); break;
            case "3": AtualizarQuilometragem(); break;
            case "4": ConsultarAutomovel(); break;
            case "0": return true;
            default:
                Console.WriteLine("Opção inválida. Prima qualquer tecla para continuar...");
                Console.ReadKey();
                break;
        }

        return false;
    }

    static void RegistrarAutomovel()
    {
        Console.Clear();
        Console.WriteLine("--- REGISTAR NOVO AUTOMÓVEL ---");

        string placa = LerTexto("Placa: ");
        if (_frota.ContemVeiculo(placa))
        {
            Console.WriteLine("Já existe um automóvel com esta placa.");
            Pausar();
            return;
        }

        string marca = LerTexto("Marca: ");
        string modelo = LerTexto("Modelo: ");
        int ano = LerInteiro("Ano de fabrico: ");
        double quilometragemInicial = LerDouble("Quilometragem inicial: ");

        var automovel = new Automovel(placa, marca, modelo, ano, quilometragemInicial);
        _frota.Adicionar(automovel);

        Console.WriteLine($"Automóvel {marca} {modelo} ({placa}) registado com sucesso!");
        Pausar();
    }

    static void ExibirFrota()
    {
        Console.Clear();
        Console.WriteLine("--- FROTA REGISTADA ---");

        if (!_frota.TemVeiculos())
        {
            Console.WriteLine("Não há automóveis registados no sistema.");
            Pausar();
            return;
        }

        foreach (var automovel in _frota.Listar())
            automovel.MostrarResumo();

        Pausar();
    }

    static void AtualizarQuilometragem()
    {
        Console.Clear();
        Console.WriteLine("--- ATUALIZAR QUILOMETRAGEM ---");

        if (!_frota.TemVeiculos())
        {
            Console.WriteLine("A frota está vazia.");
            Pausar();
            return;
        }

        string placa = LerTexto("Placa do automóvel: ");
        var automovel = _frota.BuscarPorPlaca(placa);

        if (automovel == null)
        {
            Console.WriteLine("Automóvel não encontrado.");
            Pausar();
            return;
        }

        Console.WriteLine($"Quilometragem atual de {automovel.Marca} {automovel.Modelo}: {automovel.Quilometragem} km");
        double novaQuilometragem = LerDouble("Quilometragem após aluguer: ");

        automovel.AtualizarQuilometragem(novaQuilometragem);
        Pausar();
    }

    static void ConsultarAutomovel()
    {
        Console.Clear();
        Console.WriteLine("--- CONSULTAR AUTOMÓVEL ---");

        if (!_frota.TemVeiculos())
        {
            Console.WriteLine("Não há automóveis na frota.");
            Pausar();
            return;
        }

        string placa = LerTexto("Placa do automóvel: ");
        var automovel = _frota.BuscarPorPlaca(placa);

        if (automovel == null)
        {
            Console.WriteLine("Automóvel não localizado.");
            Pausar();
            return;
        }

        automovel.MostrarResumo();
        Pausar();
    }

    static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (int.TryParse(input, out int valor))
                return valor;

            Console.WriteLine("Entrada inválida. Introduza um número inteiro.");
        }
    }

    static double LerDouble(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (double.TryParse(input, out double valor))
                return valor;

            Console.WriteLine("Entrada inválida. Introduza um valor numérico.");
        }
    }

    static void Pausar()
    {
        Console.WriteLine("\nPrima qualquer tecla para continuar...");
        Console.ReadKey();
    }
}

internal class Frota
{
    private readonly List<Automovel> _automoveis = new();

    public void Adicionar(Automovel automovel) => _automoveis.Add(automovel);

    public bool TemVeiculos() => _automoveis.Count > 0;

    public bool ContemVeiculo(string placa) => _automoveis.Exists(x => x.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Automovel> Listar() => _automoveis;

    public Automovel? BuscarPorPlaca(string placa) =>
        _automoveis.Find(x => x.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
}