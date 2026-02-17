//Nombre de los integrantes: 
//Luis Paulo Espinoza Sandoval - 1202925
//Cristopher Abdel De la Cruz Galvez - 1064625
//Ruben Dario Paredes Flores - 1152225


public class Usuario
{
    public int pisoActual;
    public bool estaDentro;

    public Usuario(int pA)
    {
        pisoActual = pA;
        estaDentro = false;
    }
}
public class Elevador
{
    public int piso;
    public bool llamado;
    public bool lleno;

    public Elevador(int p)
    {
        this.piso = p;
        this.llamado = false;
    }

    public int estadoMovimiento(int pActual, int destino)
    {
        int tipoMovimiento;
        if (pActual > destino)
        {
            tipoMovimiento = -1;
        }
        else
        {
            tipoMovimiento = 1;
        }
        return tipoMovimiento;
    }
    public void Moviendo(Elevador e, Usuario u, int v)
    {
        for (int i = e.piso; i != u.pisoActual; i += v)
        {
            Console.WriteLine("El elevador esta en el piso " + e.piso);
            e.piso += v;
            Thread.Sleep(100);
        }
    }

    Random random = new Random();
    public bool estaLleno()
    {
        int valor = random.Next(0, 5);
        return (valor == 0);
    }
}



class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenid@! Estas en el piso 1");
        Console.WriteLine("Quieres llamar al elevador? S/N");
        int maxPisos = 10;
        Random random = new Random();
        Usuario usuario = new Usuario(1);
        Elevador elevador = new Elevador(maxPisos);
        string iniciar;
        bool activo = false;
        iniciar = Console.ReadLine();
        if (iniciar == "S" || iniciar == "s")
        {
            activo = true;
            elevador.llamado = true;
        }
        while (activo)
        {
            if (elevador.llamado == true)
            {
                Console.WriteLine("Estas en el piso " + usuario.pisoActual);
                elevador.Moviendo(elevador, usuario, elevador.estadoMovimiento(elevador.piso, usuario.pisoActual));
                Console.WriteLine("El elevador ha llegado al piso " + elevador.piso);
                if (elevador.estaLleno())
                {
                    Console.WriteLine("El elevador esta lleno, por favor espere al siguiente.");
                    elevador.llamado = false;
                    do
                    {
                        elevador.piso = random.Next(1, 11);
                    }
                    while (elevador.piso == usuario.pisoActual);
                    Console.WriteLine("Quiere llamar al elevador de nuevo? S/ N");
                    string reintentar;
                    reintentar = Console.ReadLine();
                    if (reintentar == "S" || reintentar == "s")
                    {
                        elevador.llamado = true;
                        continue;
                    }
                    else
                    {
                        activo = false;
                    }
                }
                else
                {
                    Console.WriteLine("Subiendo al elevador...");
                    usuario.estaDentro = true;
                    Console.WriteLine("A que piso desea ir? El edificio tiene " + maxPisos + " pisos.");
                    int pisoDestino = 1;
                    do
                    {
                        pisoDestino = Convert.ToInt32(Console.ReadLine());
                        if (pisoDestino > maxPisos || pisoDestino < 0)
                        {
                            Console.WriteLine("Este edificio solo tiene " + maxPisos + " pisos y no cuenta con subterraneo. Por favor, intente de nuevo.");
                        }
                    }
                    while (pisoDestino > maxPisos || pisoDestino < 0);
                    bool pisoCorrecto = true;
                    usuario.pisoActual = pisoDestino;
                    while (pisoCorrecto)
                    {
                        elevador.Moviendo(elevador, usuario, elevador.estadoMovimiento(elevador.piso, pisoDestino));
                        Console.WriteLine("Has llegado al piso " + usuario.pisoActual);
                        Console.WriteLine("Quieres bajarte y quedarte en este piso o ir a otro piso? Quedarse = S / Otro piso = N");
                        string respuesta = Console.ReadLine();
                        if (respuesta == "S" || respuesta == "s")
                        {
                            usuario.estaDentro = false;
                            elevador.llamado = false;
                            activo = false; 
                            pisoCorrecto = false; 
                            break;
                        }
                        else
                        {
                            Console.WriteLine("A que piso desea ir? El edificio tiene " + maxPisos + " pisos.");
                            int nuevoDestino = Convert.ToInt16(Console.ReadLine());
                            if (nuevoDestino > maxPisos || nuevoDestino < 0)
                            {
                                Console.WriteLine("Este edificio solo tiene " + maxPisos + " pisos y no cuenta con subterraneo. Por favor, intente de nuevo.");
                                continue;
                            }
                            pisoDestino = nuevoDestino;
                            usuario.pisoActual = pisoDestino;
                        }
                    }
                }
            }
        }
        Console.WriteLine("Talvez el piso correcto fue los amigos que hicimos en el camino. Gracias por usar el programa!");
    }
}