using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class ConsoleHandler
    {
        private Dictionary<string, IQueryFactory> queryFactories = new Dictionary<string, IQueryFactory>
        {
            { "display", new DisplayQueryFactory() },
            { "add" , new AddQueryFactory()},
            { "update",new UpdateQueryFactory()},
            { "delete", new DeleteQueryFactory()}
        };
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
            string? command;
            while ((command = Console.ReadLine()) != null)
            {
                if (command == "exit")
                {
                    Environment.Exit(0);
                }
                else if (command == "print")
                {
                    DataHandler.MakeSnapshot(source);
                }
                else if (command == "report")
                {
                    Report(usual_reporters, Database.subjects);
                }
                else
                {
                    string[] query = command.Trim().Split(" ");
                    if (query[0] == "display" || query[0] == "update" || query[0] == "delete" || query[0] == "add")
                    {
                        QueryManager queryManager = new QueryManager(query);
                        queryManager.ExecuteQuery();
                    }
                    else
                        Console.WriteLine($"Command '{command}' doesn't exist");
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
