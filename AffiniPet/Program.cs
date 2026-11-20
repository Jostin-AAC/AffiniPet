// *************************************************
// Proyecto: AffiniPet (PPE03 - Configuración Inicial)
// Integrantes: Jostin Aldas, Paul Olmos, Galo Castillo
// Fecha de realización: 20/11/2026
// Fecha de entrega: 25/11/2026
// Resultados:
// * Estructura base de la solución Windows Forms en .NET 8 configurada
//   y vinculada al repositorio de control de versiones en GitHub.
// **************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AffiniPet
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
