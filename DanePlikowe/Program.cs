namespace Operations
{
    using System.Diagnostics;

    class FileMethods
    {
        public static void ReverseText(string path)
        {

            string filePath = path;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        for (int i = line.Length - 1; i >= 0; i--)
                        {
                            Console.Write(line[i]);
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
            }
        }

        public static void ReadText(string path) { 

        
            string FilePath = path;

            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Plik o podanej ścieżce nie istnieje.");
                return;
            }

            try
            {
                using (FileStream file = File.OpenRead(FilePath))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Wystąpił błąd podczas odczytywania pliku:");
                Console.WriteLine(e.Message);
            }
        }

        public static void DoCopy(string SourceFile, string TargetFile) { 
            try
            {
                Copy(SourceFile, TargetFile);
                Console.WriteLine("Plik skopiowany pomyślnie!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd podczas kopiowania pliku: " + e.Message);
            }
        }

        static void Copy(string SourceFile, string TargetFile)
        {
            using (FileStream source = new FileStream(SourceFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream target = new FileStream(TargetFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] bufor = new byte[1024 * 1024]; // Bufor 1 MB
                    int iloscBajtowOdczytanych;

                    while ((iloscBajtowOdczytanych = source.Read(bufor, 0, bufor.Length)) > 0)
                    {
                        target.Write(bufor, 0, iloscBajtowOdczytanych);
                    }
                }
            }
        }
    }

    class BinaryMethods
    {
        public static void Menu() {

            int choice;

            do
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Pobierz dane i zapisz do pliku binarnego");
                Console.WriteLine("2. Odczytaj dane z pliku binarnego i wyświetl na ekranie");
                Console.WriteLine("0. Wyjdź");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        WriteDataToBinaryFile();
                        break;
                    case 2:
                        ReadDataFromBinaryFile();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                        break;
                }
            }

            while (choice != 0);
   
        }

        static void WriteDataToBinaryFile()
        {
            Console.Write("Podaj imię: ");
            string name = Console.ReadLine();

            Console.Write("Podaj wiek: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Podaj adres: ");
            string address = Console.ReadLine();

            string path = @"C:\Users\Jarek\source\repos\DanePlikowe\DanePlikowe\data.bin";

            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                bw.Write(name);
                bw.Write(age);
                bw.Write(address);
            }
            
            Console.WriteLine("Dane zapisane do pliku data.bin.");
        }

        static void ReadDataFromBinaryFile()
        {
            string path = @"C:\Users\Jarek\source\repos\DanePlikowe\DanePlikowe\data.bin";

            if (!File.Exists(path))
            {
                Console.WriteLine("Plik data.bin nie istnieje.");
                return;
            }

            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                string name = br.ReadString();
                int age = br.ReadInt32();
                string address = br.ReadString();

                Console.WriteLine($"Imię: {name}");
                Console.WriteLine($"Wiek: {age}");
                Console.WriteLine($"Adres: {address}");


            }

        }
    }

    class PerformanceCounter
    {
        public static void TimeCounter(Action<string, string> action, string arg1, string arg2)
        {
            Stopwatch stoper = new Stopwatch();
            stoper.Start();

            action(arg1, arg2); 
            stoper.Stop();

            TimeSpan czas = stoper.Elapsed;
            Console.WriteLine("Czas wykonania operacji: {0} ms", czas.TotalMilliseconds);
        }
    }

}
namespace DanePlikowe
{
    using Operations;
    class Program
    {
        static void Main()
        {
            string source = @"C:\Users\Jarek\source\repos\DanePlikowe\DanePlikowe\plik.txt";
            string target = @"C:\Users\Jarek\source\repos\DanePlikowe\DanePlikowe\plik_kopia.txt";
            FileMethods.ReadText(source); //Zadanie 3
            FileMethods.ReverseText(source); //Zadanie 4
            BinaryMethods.Menu(); //Zadanie 5
            FileMethods.DoCopy(source, target); //Zadanie 6
            source = @"C:\Users\Jarek\source\repos\DanePlikowe\DanePlikowe\testfile.txt";
            target = @"C:\Users\Jarek\source\repos\DanePlikowe\DanePlikowe\testfile_kopia.txt";
            PerformanceCounter.TimeCounter(FileMethods.DoCopy, source, target); //Zadanie 7

            Environment.Exit(0);

        }
    }
}