public class Carta
{
    
    private readonly int _numero;
    private readonly string _palo;

    public Carta(int numero, string palo)
    {
        _numero = numero;
        _palo   = palo;
    }
    public override string ToString()
    {
    
        return $"{_numero.ToString()} de {_palo}";
    }
}
    