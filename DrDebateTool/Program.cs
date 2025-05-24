public partial class Program
{
    public static void Execute(string filepath, string outpath="")
    {
        if (!File.Exists(filepath))
        {
            Console.WriteLine($"File {filepath} does not exist!");
            return;
        }

        if (filepath.EndsWith(".dat"))
        {
            Console.WriteLine($"Extracting {Path.GetFileName(filepath)}...");
            DrDebateBin.DebateBin.Extract(filepath, outpath);
            Console.WriteLine($"Extracted  {Path.GetFileName(filepath)}!");
            return;
        }

        Console.WriteLine($"Repacking {Path.GetFileName(filepath)}...");
        DrDebateBin.DebateBin.Repack(filepath, outpath);
        Console.WriteLine($"Repacked  {Path.GetFileName(filepath)}!");
    }
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Execute(args[0], args.Length > 1 ? args[1] : "");
            return;
        }

        while (true)
        {
            Console.WriteLine("Drag and drop the .dat /.txt file to extract / repack");
            Execute(Console.ReadLine().Replace("\"", ""));
        }
    }
}