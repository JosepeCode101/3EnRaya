using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3EnRaya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] tablero = new char[3, 3];

            InicializarTablero(ref tablero);

            string jugador1 = "";
            string jugador2 = "";

            Console.WriteLine("¡Bienvenido al 3 en Raya!");
            Console.WriteLine("______________________________________");
            Console.WriteLine();
            //Console.WriteLine("Jugador 1. Introduce tu nombre:");
            
            while(jugador1.Length < 2)
            {
                Console.WriteLine("Jugador 1. Juegas con X. Introduce tu nombre:");
                jugador1 = Console.ReadLine();
                Console.WriteLine();
                if (jugador1.Length < 2)
                {
                    Console.WriteLine("Introduce un nombre más largo");
                }
            }
            while (jugador2.Length < 2)
            {
                Console.WriteLine("Jugador 2. Juegas con O. Introduce tu nombre:");
                jugador2 = Console.ReadLine();
                Console.WriteLine();
                if (jugador2.Length < 2)
                {
                    Console.WriteLine("Introduce un nombre más largo");
                }
            }

            bool turnoJugador1 = true;

            while (!HayGanador(tablero))
            {
                Console.WriteLine("¡Bienvenido al 3 en Raya!");
                Console.WriteLine("_________________________");
                Console.WriteLine();
                Console.Clear();

                Console.WriteLine(TableroVisual(tablero));

                string nombreJugador = "";
                if (turnoJugador1)
                {
                    nombreJugador = jugador1;
                }
                else
                {
                    nombreJugador = jugador2;
                }

                Console.WriteLine($"{nombreJugador}, introduce la coordenada donde quieres jugar");
                Console.WriteLine("Ejempo: A1");

                string coordenada = "";
                while (!CoordenadaValida(coordenada) || EstaYaOcupada(tablero, coordenada))
                {
                    Console.WriteLine($"{nombreJugador}, introduce la coordenada donde quieres jugar");
                    Console.WriteLine("Ejempo: A1");

                    coordenada = Console.ReadLine().ToUpper();
                    if (!CoordenadaValida(coordenada))
                    {
                        Console.WriteLine("La coordenada no es válida");
                        Console.WriteLine();
                    }
                    if (EstaYaOcupada(tablero, coordenada))
                    {
                        Console.WriteLine("La coordenada está ocupada, introduce otra coordenada");
                        Console.WriteLine();
                    }
                }
                char caracterUsado = ' ';
                if (turnoJugador1)
                {
                    caracterUsado = 'X';
                }
                else
                {
                    caracterUsado = 'O';
                }
                EscribirCoordenada(ref tablero, coordenada, caracterUsado);
                turnoJugador1 = !turnoJugador1;
                if (HayGanador(tablero))
                {
                    break;
                }
            }

            if (HayGanador(tablero))
            {
                Console.WriteLine("Fin del Juego");
                if (Ganador(tablero) == 'X')
                {
                    Console.WriteLine($"{jugador1} H A    G A N A D O");
                }
                else
                {
                    Console.WriteLine($"{jugador2} H A   G A N A D O");
                }
            }

            Console.ReadKey();
        }

        static void InicializarTablero (ref char[,] tablero)
        {
            for (int i1 = 0; i1< 3; i1++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    tablero[i1, i2] = ' ';
                }
            }
        }
        static string TableroVisual(char[,] tablero)
        {
            //Console.WriteLine();
            string tv = "";
            tv = "   A    B    C" + Environment.NewLine;
            tv +=  "  ┌───┬───┬───┐" + Environment.NewLine;
            tv += $"1 │ {tablero[0,0]} │ {tablero[0,1]} │ {tablero[0,2]} │" + Environment.NewLine; 
            tv += "  ├───┼───┼───┤" + Environment.NewLine;
            tv += $"2 │ {tablero[1,0]} │ {tablero[1,1]} │ {tablero[1,2]} │" + Environment.NewLine;
            tv += "  ├───┼───┼───┤" + Environment.NewLine;
            tv += $"3 │ {tablero[2, 0]} │ {tablero[2, 1]} │ {tablero[1, 2]} │" + Environment.NewLine;
            tv += "  └───┴───┴───┘" + Environment.NewLine;

            return tv;
        }
        
        static char Ganador(char[,] tablero)
        {

            // Ganador horizontal
            if (tablero[0,0] == tablero[0,1] && tablero[0,1] == tablero[0, 2] && tablero[0,0] != ' ')
            {
                return tablero[0,0];
            }
            if (tablero[1, 0] == tablero[1, 1] && tablero[1, 1] == tablero[1, 2] && tablero[1, 0] != ' ')
            {
                return tablero[1,0];
            }
            if (tablero[2, 0] == tablero[2, 1] && tablero[2, 1] == tablero[2, 2] && tablero[2, 0] != ' ')
            {
                return tablero[2,0];
            }
            // Ganador vertical
            if (tablero[0, 0] == tablero[1, 0] && tablero[1, 0] == tablero[2, 0] && tablero[0, 0] != ' ')
            {
                return tablero[0, 0];
            }
            if (tablero[0, 1] == tablero[1, 1] && tablero[1, 1] == tablero[2, 1] && tablero[0, 1] != ' ')
            {
                return tablero[0, 1];
            }
            if (tablero[0, 2] == tablero[1, 2] && tablero[1, 2] == tablero[2, 2] && tablero[0, 2] != ' ')
            {
                return tablero[0, 2];
            }
            // Ganador diagonal
            if (tablero[0, 0] == tablero[1, 1] && tablero[1, 1] == tablero[2, 2] && tablero[0, 0] != ' ')
            {
                return tablero[0, 0];
            }
            if (tablero[0, 2] == tablero[1, 1] && tablero[1, 1] == tablero[2, 0] && tablero[0, 2] != ' ')
            {
                return tablero[0, 2];
            }

            return ' ';
        }

        static bool HayGanador(char[,] tablero)
        {
            return Ganador(tablero) != ' ';
        }

        static bool EstaOcupada(char[,] tablero, int x, int y)
        {
            if (x < 0 || x > 2)
            {
                throw new ArgumentException("El valor de x debe ser 0, 1 o 2", "x");
            }
            if (y < 0 || y < 2)
            {
                throw new ArgumentException("El valor de x debe ser 0, 1 o 2", "y");
            }

            return tablero[x, y] != ' ';
        }

        static bool EstaYaOcupada(char[,] tablero, string coordenada)
        {
            switch (coordenada)
            {
                case "A1":
                    return EstaOcupada(tablero, 0, 0);
                case "A2":
                    return EstaOcupada(tablero, 0, 1);
                case "A3":
                    return EstaOcupada(tablero, 0, 2);
                case "B1":
                    return EstaOcupada(tablero, 1, 0);
                case "B2":
                    return EstaOcupada(tablero, 1, 1);
                case "B3":
                    return EstaOcupada(tablero, 1, 2);
                case "C1":
                    return EstaOcupada(tablero, 2, 0);
                case "C2":
                    return EstaOcupada(tablero, 2, 1);
                case "C3":
                    return EstaOcupada(tablero, 2, 1);
            }
            return false;
        }

        static void EscribirCoordenada (ref char[,] tablero, string coordenada, char letra)
        {
            coordenada = coordenada.ToUpper();

            switch (coordenada)
            {
                case "A1":
                    tablero[0, 0] = letra;
                    return;
                case "A2":
                    tablero[0, 1] = letra;
                    return;
                case "A3":
                    tablero[0, 2] = letra;
                    return;
                case "B1":
                    tablero[1, 0] = letra;
                    return;
                case "B2":
                    tablero[1, 1] = letra;
                    return;
                case "B3":
                    tablero[1, 2] = letra;
                    return;
                case "C1":
                    tablero[2, 0] = letra;
                    return;
                case "C2":
                    tablero[2, 1] = letra;
                    return;
                case "C3":
                    tablero[2, 2] = letra;
                    return;
            }


        }
        
        static bool CoordenadaValida(string coordenada)
        {
            switch (coordenada)
            {
                case "A1":
                case "A2":
                case "A3":
                case "B1":
                case "B2":
                case "B3":
                case "C1":
                case "C2":
                case "C3":
                    return true;
                default:
                    return false;
            }
            return false;
        }
    
    }
}
