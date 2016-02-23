using System;
using System.IO;
using System.IO.Compression;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //initializes target paths
            string startPath = @"C:\Users\Raf\Documents\Visual Studio 2013\Projects\ConsoleApplication7\ConsoleApplication7\temp";
            string zipPath = @"C:\Users\Raf\Documents\Visual Studio 2013\Projects\ConsoleApplication7\ConsoleApplication7\Result.zip";
            string extractPath = @"C:\Users\Raf\Documents\Visual Studio 2013\Projects\ConsoleApplication7\ConsoleApplication7\temp\Result";
            string choice;
            DirectoryInfo currentdir = new DirectoryInfo(startPath);
           
            //simple folder zip compression
            
            //simple zip extraction

            do
            {
                Console.WriteLine("Type a command (compress, extract, !specific compress, !specific extract,");
                Console.WriteLine("exit, start path, extract path, zip path, !help, comp type, !zip add ):");
                choice = Console.ReadLine();
                Console.WriteLine("\n");
                switch (choice)
                {
                    case "compress":
                        ZipFile.CreateFromDirectory(startPath, zipPath);
                        break;

                    case "extract":
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                        break;

                    case "specific compress":
                        Console.WriteLine("Specify a valid extension (example: .txt):");
                        choice = Console.ReadLine();
                        if (!File.Exists(zipPath))
                        {
                            System.IO.Directory.CreateDirectory(@"C:\Temporarydir");
                            ZipFile.CreateFromDirectory(@"C:\Temporarydir", zipPath);
                            System.IO.Directory.Delete(@"C:\Temporarydir");
                        }
                        using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
                        {
                            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                            {
                                foreach (FileInfo fileToCompress in currentdir.GetFiles())
                                {
                                    if (fileToCompress.FullName.EndsWith(choice, StringComparison.OrdinalIgnoreCase))
                                    {


                                        archive.CreateEntryFromFile(fileToCompress.FullName, fileToCompress.Name);


                                    }
                                }
                            }
                        }
                        break;

                    case "specific extract":
                        //extracts specific files by extension
                        Console.WriteLine("Specify a valid extension (example: .txt):");
                        choice = Console.ReadLine();
                        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                if (entry.FullName.EndsWith(choice, StringComparison.OrdinalIgnoreCase))
                                {
                                    entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));
                                }
                            }
                        }
                        break;

                    case "exit":
                        break;

                    case "start path":
                        startPath = Console.ReadLine();
                        break;       
        
                    case "zip path":
                        zipPath = Console.ReadLine();
                        break;

                    case "extract path":
                        extractPath = Console.ReadLine();
                        break;

                    case "zip add":
                        using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
                        {
                            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                            {
                                ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                                using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                                {
                                    writer.WriteLine("Information about this package.");
                                    writer.WriteLine("========================");
                                }
                            }
                        }
                        break;
                    case "zip delete":
                        Console.WriteLine("Enter file to Delete");
                        choice = Console.ReadLine();
                        using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open))
                        {
                            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                            {
                                ZipArchiveEntry readmeEntry = archive.GetEntry(choice); 
                                if (readmeEntry != null)
                                {
                                    readmeEntry.Delete();
                                }
                            }
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid Option \n");
                        break;
                }

            } while (choice != "exit");

          

         

        }
    }
}