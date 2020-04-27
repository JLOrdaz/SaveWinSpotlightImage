using System;
using System.IO;
using System.Reflection;

namespace SaveWSpotlightImage
{
    class Program
    {
        static string rutaDestino;
        static string ruta = @"%UserProfile%\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
        static string rutaFull = Environment.ExpandEnvironmentVariables(ruta);
        static void Main(string[] args)
        {
            string[] archivos = System.IO.Directory.GetFiles(rutaFull);
            string destino;
            int contarNuevo = 0;
            int contarExistentes = 0;
            if (args.Length > 0)
            {
                rutaDestino = args[0];
                Console.WriteLine("Starting...");
                ConfigureFolders();

                Console.WriteLine("Searching for files");

                foreach (var item in archivos)
                {
                    Console.Write(".");
                    FileInfo fileInfo = new FileInfo(item);
                    if (fileInfo.Length > 100000)
                    {
                        var image = System.Drawing.Image.FromFile(fileInfo.FullName);

                        if (image.Size.Width > image.Size.Height)
                        {
                            destino = Path.Combine(rutaDestino, "landscape", fileInfo.Name + ".jpg");
                        }
                        else
                        {
                            destino = Path.Combine(rutaDestino, "portrait", fileInfo.Name + ".jpg");
                        }


                        if (File.Exists(destino))
                            contarExistentes++;
                        else
                            contarNuevo++;

                        File.Copy(fileInfo.FullName, destino, true);
                    }
                };
                Console.WriteLine();
                Console.WriteLine(value: $"Found \n{contarNuevo} new files.. \n{contarExistentes} existing files..");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please indicate as a parameter the destination route");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        private static void ConfigureFolders()
        {
            if (!Directory.Exists(rutaDestino))
            {
                Console.WriteLine("Creating destination folder");
                Directory.CreateDirectory(rutaDestino);

                if (!Directory.Exists(Path.Combine(rutaDestino, "landscape")))
                {
                    Console.WriteLine("Creating folder landscape");
                    Directory.CreateDirectory(Path.Combine(rutaDestino, "landscape"));
                }
                if (!Directory.Exists(Path.Combine(rutaDestino, "portrait")))
                {
                    Console.WriteLine("Creating folder landscape");
                    Directory.CreateDirectory(Path.Combine(rutaDestino, "portrait"));
                }
            }

        }
    }
}