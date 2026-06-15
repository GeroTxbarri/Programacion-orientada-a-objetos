public class Mano
{
    private List<Carta> _cartas;

    public Mano()
    {
        _cartas = new List<Carta>();
    }

    public void recibirCarta(Carta carta)
    {
        _cartas.Add(carta);
    }

    public void mostrarMano()
    {
        foreach (Carta carta in _cartas)
            Console.WriteLine($"  - {carta}");
    }

    // Devuelve la cantidad de cartas en la mano
    public int cantidadDeCartas()
    {
        return _cartas.Count;
    }
}
