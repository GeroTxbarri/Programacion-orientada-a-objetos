using vehiculos;

Auto fiat     = new Auto(45);
Bicicleta bici = new Bicicleta();
Camion camion  = new Camion();

bici.Mover(20);
Console.WriteLine($"Posición bici tras 20s: {bici.Posicion()} metros");   // 200

bici.Mover(10);
Console.WriteLine($"Posición bici tras 10s más: {bici.Posicion()} metros"); // 300

Carrera carrera1 = new Carrera(new Bicicleta(), "Bicicleta", new Auto(45), "Ford");
carrera1.Competir(60);


