using System;
using System.IO;
using System.Threading;
namespace OLEManager
{
    public class Kassa
    {
        private Resonance.M304ManagerApplication m304;
        public Kassa()
        {
            m304 = new Resonance.M304ManagerApplication { ThrowExceptionsOnError = true };
            m304.InitAuto();
        }
        public void Print(string[] text)
        {
            try
            {
                m304.OpenTextDocument();
                foreach (string s in text)
                {
                    m304.FreeTextLine(s);
                }
                m304.CloseTextDocument();
                Console.WriteLine("Документ надруковано");
            }
            catch
            {
                Console.WriteLine("Помилка друку");
            }
        }
    }


    public class DocumentWorker
    {
        private const int CheckIntervalSeconds = 5;
        private readonly string _documentPath;
        private bool _isRunning = false;
        private Thread _thread;
        private Kassa p;
        public DocumentWorker(string documentPath, Kassa p)
        {
            _documentPath = documentPath;
            this.p = p;
        }

        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;
            _thread = new Thread(RunWorker);
            _thread.Start();
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;
            _thread.Join();
        }

        private void RunWorker()
        {
            while (_isRunning)
            {
                try
                {

                    var maxLength = 40;
                    // читаємо файл
                    string[] lines = File.ReadAllLines(_documentPath);

                    // очищуємо файл
                    File.WriteAllText(_documentPath, string.Empty);

                    // створюємо новий файл для запису
                    using (StreamWriter writer = new StreamWriter(_documentPath, true))
                    {
                        // обробляємо кожен рядок
                        foreach (string line in lines)
                        {
                            // якщо довжина рядка перевищує максимальну довжину
                            if (line.Length > maxLength)
                            {
                                // розбиваємо рядок на частини
                                int start = 0;
                                while (start < line.Length)
                                {
                                    // знаходимо кінцеву позицію частини
                                    int end = Math.Min(start + maxLength, line.Length);

                                    // записуємо частину в файл
                                    writer.WriteLine(line.Substring(start, end - start));

                                    // рухаємося до наступної частини
                                    start = end;
                                }
                            }
                            else
                            {
                                // записуємо рядок в файл
                                writer.WriteLine(line);
                            }
                        }
                    }
                    string content = File.ReadAllText(_documentPath);
                    if (content != "")
                    {
                        string[] st = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        p.Print(st);
                    }
                    File.WriteAllText(_documentPath, string.Empty);

                    Thread.Sleep(CheckIntervalSeconds * 1000); // 5 секунд
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка при роботі з документом: {ex.Message}");
                }
            }
        }

        public class main
        {
            public static void Main(string[] args)
            {
                Kassa kassa = new Kassa();
                var worker = new DocumentWorker("C:\\Users\\kosver\\source\\repos\\Avantage7Questions\\Avantage7Questions\\wwwroot\\File1.txt", kassa);
                worker.Start();
            }
        }
    }
}