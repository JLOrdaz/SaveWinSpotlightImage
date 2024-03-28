// See https://aka.ms/new-console-template for more information
string rutaDestino;
string ruta = @"%UserProfile%\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
string rutaFull = Environment.ExpandEnvironmentVariables(ruta);

string[] archivos = System.IO.Directory.GetFiles(rutaFull);
string destino;
int contarNuevo = 0;
int contarExistentes = 0;

if (args.Length > 0)
{
    Console.WriteLine("Starting...");

    rutaDestino = args[0];
    ConfigureFolders();

    Console.WriteLine("Searching for files");

    foreach (var item in archivos)
    {
        Console.Write(".");
        FileInfo fileInfo = new(item);
        if (fileInfo.Length > 100000)
        {
            var image = System.Drawing.Image.FromFile(fileInfo.FullName);
            destino = Path.Combine(rutaDestino, image.Size.Width > image.Size.Height ? "landscape" : "portrait", fileInfo.Name + ".jpg");

            if (File.Exists(destino))
                contarExistentes++;
            else
                contarNuevo++;

            File.Copy(fileInfo.FullName, destino, true);
        }
    };
    Console.WriteLine();
    Console.WriteLine($"Found \n{contarNuevo} new files.. \n{contarExistentes} existing files..");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Please indicate as a parameter the destination route");
    Console.ForegroundColor = ConsoleColor.White;
}

void ConfigureFolders()
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
            Console.WriteLine("Creating folder portrait");
            Directory.CreateDirectory(Path.Combine(rutaDestino, "portrait"));
        }
    }
}