public class Mazo
{
    private string[] Palos = { "Espadas", "Bastos", "Oros", "Copas" };
    private int[] Numeros = { 1, 2, 3, 4, 5, 6, 7, 10, 11, 12 };

    private List<Carta> _cartas;
    private Random rand;

    public Mazo()
    {
        rand    = new Random();
        _cartas = new List<Carta>();

        foreach (string palo in Palos)
            foreach (int numero in Numeros)
                _cartas.Add(new Carta(numero, palo));
    }

    public void barajar()
    {
        for (int i = _cartas.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (_cartas[i], _cartas[j]) = (_cartas[j], _cartas[i]);
        }
        Console.WriteLine("mazo mezclado.");
    }

    public Carta robarCarta()
    {
        if (_cartas.Count == 0)
        {
            Console.WriteLine("Error: el mazo está vacío.");
            return null!;
        }

        Carta carta = _cartas[_cartas.Count - 1];
        _cartas.RemoveAt(_cartas.Count - 1);
        return carta;
    }

    public int cuantasCartasQuedan()
    {
        return _cartas.Count;
    }
}
