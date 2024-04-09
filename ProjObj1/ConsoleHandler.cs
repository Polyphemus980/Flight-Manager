using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class ConsoleHandler
    {
        public static List<IReporter> usual_reporters = new List<IReporter>
        {
            new Television("Telewizja Abelowa"),
            new Television("Kanał TV-tensor"),
            new Radio("Radio Kwantyfikator"),
            new Radio("Radio Shmem"),
            new Newspaper("Gazeta Kategoryczna"),
            new Newspaper("Dziennik Politechniczny")
        };
        public static void ConsoleReact(IDataSource source)
        {
            string? Command = "";
            while ((Command = Console.ReadLine()) != null)
            {
                if (Command == "exit")
                {
                    Environment.Exit(0);
                }
                if (Command == "print")
                {
                    DataHandler.MakeSnapshot(source);
                }
                if (Command == "report")
                {
                    Report(usual_reporters, Database.subjects);
                }
            }
        }

        public static void Report(List<IReporter> reporters,List<IReportable> subjects)
        {
            NewsGenerator generator = new NewsGenerator(usual_reporters, Database.subjects);
            string? report;
            while ((report = generator.GenerateNextNews()) != null)
            {
                Console.WriteLine(report);
            }
        }
    }
}
