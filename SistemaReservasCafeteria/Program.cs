using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservasCafeteria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Definir y mostrar el menú
            List<Combo> menu = DefinirMenu();
            MostrarMenu(menu);
            {
                Console.WriteLine("¡Bienvenido al Sistema de Reservas de la Cafetería!");

                // Iniciar las reservas
                string[,] reservas = InicializarReservas();

                while (true)
                {
                    Console.WriteLine("\n--- Menú ---");
                    Console.WriteLine("Heraldy Cristopher Cusquisiban");
                    Console.WriteLine(DateTime.Now.AddHours(0));
                    Console.WriteLine("1. Mostrar Menú de Combos");
                    Console.WriteLine("2. Mostrar Estado de Reservas");
                    Console.WriteLine("3. Registrar Reserva");
                    Console.WriteLine("4. Cancelar Reserva");
                    Console.WriteLine("5. Listar Reservas por Turno");
                    Console.WriteLine("6. Calcular Ingresos");
                    Console.WriteLine("7. Buscar Reserva por Nombre del Estudiante");
                    Console.WriteLine("8. Salir");

                    Console.WriteLine("Seleccione una opción:");
                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            MostrarMenu(menu);
                            break;
                        case "2":
                            MostrarReservas(reservas);
                            break;
                        case "3":
                            RegistrarReserva(reservas, menu);
                            break;
                        case "4":
                            CancelarReserva(reservas);
                            break;
                        case "5":
                            ListarReservasPorTurno(reservas);
                            break;
                        case "6":
                            CalcularIngresos(reservas, menu);
                            break;
                        case "7":
                            BuscarReservaPorNombre(reservas);
                            break;
                        case "8":
                            Console.WriteLine("¡Gracias por usar el sistema!");
                            return;
                        default:
                            Console.WriteLine("Opción inválida. Intente de nuevo.");
                            break;
                    }
                }
            }
        }

        struct Combo
        {
            public string Nombre;
            public double Precio;
        }

        static List<Combo> DefinirMenu()
        {
            List<Combo> menu = new List<Combo>();

            // Agregar combos al menú
            menu.Add(new Combo { Nombre = "Café + Pan", Precio = 2.50 });
            menu.Add(new Combo { Nombre = "Jugo + Sándwich", Precio = 4.00 });
            menu.Add(new Combo { Nombre = "Té + Galletas", Precio = 3.00 });

            return menu;
        }

        static void MostrarMenu(List<Combo> menu)
        {
            Console.WriteLine("Menú de Combos:");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i].Nombre} - Precio: {menu[i].Precio:C}");
            }
        }
        // Función para iniciar la matriz de reservas
        static string[,] InicializarReservas()
        {
            string[,] reservas = new string[2, 20]; // 2 turnos, 20 reservas por turno
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    reservas[i, j] = "Libre"; // Iniciar todas las reservas como "Libre"
                }
            }
            return reservas;
        }

        static void MostrarReservas(string[,] reservas)
        {
            Console.WriteLine("\nEstado de las Reservas:");
            Console.WriteLine("Turno Mañana:");
            for (int j = 0; j < 20; j++)
            {
                Console.WriteLine($"Reserva {j + 1}: {reservas[0, j]}");
            }
            Console.WriteLine("Turno Tarde:");
            for (int j = 0; j < 20; j++)
            {
                Console.WriteLine($"Reserva {j + 1}: {reservas[1, j]}");
            }
        }
        // Función para registrar una reserva
        static void RegistrarReserva(string[,] reservas, List<Combo> menu)
        {
            Console.WriteLine("\nRegistrar Reserva:");
            Console.WriteLine("Ingrese el turno (1: Mañana, 2: Tarde):");
            int turno = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Ingrese el número de reserva (1-20):");
            int numeroReserva = int.Parse(Console.ReadLine()) - 1;

            if (turno < 0 || turno > 1 || numeroReserva < 0 || numeroReserva > 19)
            {
                Console.WriteLine("Número de turno o reserva inválido.");
                return;
            }

            if (reservas[turno, numeroReserva] == "Libre")
            {
                Console.WriteLine("Ingrese el nombre del estudiante:");
                string nombreEstudiante = Console.ReadLine();

                MostrarMenu(menu);
                Console.WriteLine("Ingrese el número del combo que desea reservar:");
                int numeroCombo = int.Parse(Console.ReadLine()) - 1;

                if (numeroCombo < 0 || numeroCombo >= menu.Count)
                {
                    Console.WriteLine("Número de combo inválido.");
                    return;
                }

                reservas[turno, numeroReserva] = $"{nombreEstudiante} - {menu[numeroCombo].Nombre}";
                Console.WriteLine("Reserva registrada con éxito.");
            }
            else
            {
                Console.WriteLine("Esta reserva ya está ocupada.");
            }
        }

        // Función para cancelar una reserva
        static void CancelarReserva(string[,] reservas)
        {
            Console.WriteLine("\nCancelar Reserva:");
            Console.WriteLine("Ingrese el turno (1: Mañana, 2: Tarde):");
            int turno = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Ingrese el número de reserva (1-20):");
            int numeroReserva = int.Parse(Console.ReadLine()) - 1;

            if (turno < 0 || turno > 1 || numeroReserva < 0 || numeroReserva > 19)
            {
                Console.WriteLine("Número de turno o reserva inválido.");
                return;
            }

            if (reservas[turno, numeroReserva] != "Libre")
            {
                reservas[turno, numeroReserva] = "Libre";
                Console.WriteLine("Reserva cancelada con éxito.");
            }
            else
            {
                Console.WriteLine("Esta reserva ya está libre.");
            }
        }
        // Función para listar las reservas por turno
        static void ListarReservasPorTurno(string[,] reservas)
        {
            Console.WriteLine("\nListar Reservas por Turno:");
            Console.WriteLine("Ingrese el turno (1: Mañana, 2: Tarde):");
            int turno = int.Parse(Console.ReadLine()) - 1;

            if (turno < 0 || turno > 1)
            {
                Console.WriteLine("Número de turno inválido.");
                return;
            }

            Console.WriteLine($"\nReservas para el turno {(turno == 0 ? "Mañana" : "Tarde")}:");
            for (int j = 0; j < 20; j++)
            {
                Console.WriteLine($"Reserva {j + 1}: {reservas[turno, j]}");
            }
        }

        // Función para calcular los ingresos por turno y totales
        static void CalcularIngresos(string[,] reservas, List<Combo> menu)
        {
            double ingresosManana = 0;
            double ingresosTarde = 0;

            for (int j = 0; j < 20; j++)
            {
                if (reservas[0, j] != "Libre") // Mañana
                {
                    // Extraer el nombre del combo reservado
                    string reserva = reservas[0, j];
                    string nombreCombo = reserva.Substring(reserva.IndexOf('-') + 2); // Obtener el nombre del combo

                    // Buscar el precio del combo en el menú
                    foreach (var combo in menu)
                    {
                        if (combo.Nombre == nombreCombo)
                        {
                            ingresosManana += combo.Precio;
                            break;
                        }
                    }
                }

                if (reservas[1, j] != "Libre") // Tarde
                {
                    // Extraer el nombre del combo reservado
                    string reserva = reservas[1, j];
                    string nombreCombo = reserva.Substring(reserva.IndexOf('-') + 2); // Obtener el nombre del combo

                    // Buscar el precio del combo en el menú
                    foreach (var combo in menu)
                    {
                        if (combo.Nombre == nombreCombo)
                        {
                            ingresosTarde += combo.Precio;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("\nCálculo de Ingresos:");
            Console.WriteLine($"Ingresos Turno Mañana: {ingresosManana:C}");
            Console.WriteLine($"Ingresos Turno Tarde: {ingresosTarde:C}");
            Console.WriteLine($"Ingresos Totales: {ingresosManana + ingresosTarde:C}");
        }

        // Función para buscar reserva por nombre del estudiante
        static void BuscarReservaPorNombre(string[,] reservas)
        {
            Console.WriteLine("\nBuscar Reserva por Nombre del Estudiante:");
            Console.WriteLine("Ingrese el nombre del estudiante:");
            string nombreEstudiante = Console.ReadLine();

            bool encontrado = false;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (reservas[i, j] != "Libre" && reservas[i, j].Contains(nombreEstudiante))
                    {
                        Console.WriteLine($"Reserva encontrada en el turno {(i == 0 ? "Mañana" : "Tarde")}, Reserva {j + 1}: {reservas[i, j]}");
                        encontrado = true;
                    }
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("No se encontró ninguna reserva para ese estudiante.");
            }
        }
    }
}
