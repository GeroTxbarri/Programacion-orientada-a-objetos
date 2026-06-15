
Mazo mazo = new Mazo();
mazo.barajar();

Mano jugador1 = new Mano();
Mano jugador2 = new Mano();

// Repartir 3 cartas a cada jugador
for (int i = 0; i < 3; i++)
{
    jugador1.recibirCarta(mazo.robarCarta());
    jugador2.recibirCarta(mazo.robarCarta());
}

Console.WriteLine("\n-Jugador 1 -");
jugador1.mostrarMano();

Console.WriteLine("\n-Jugador 2 -");
jugador2.mostrarMano();

Console.WriteLine($"\nCartas restantes en el mazo: {mazo.cuantasCartasQuedan()}");
