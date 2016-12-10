using System;
using System.IO;

namespace MTC.Host.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            string _path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!_path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                _path += Path.DirectorySeparatorChar;

            string archivo = Path.Combine(_path, "MTC.Host.exe");
            if (!File.Exists(archivo))
            {
                Console.WriteLine(string.Format(Properties.Resources.msgArchivoNoExiste, archivo));
                Console.ReadKey();
            }
            else
                try
                {
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                    psi.FileName = archivo;
                    psi.UseShellExecute = true;
                    psi.WorkingDirectory = _path;
                    psi.Arguments = "-w";
                    System.Diagnostics.Process.Start(psi);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
        }
    }
}
