using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class NewsGenerator
    {
        private List<IReportable> subjects;
        private List<IReporter> reporters;
        public static List<IReporter> usual_reporters = new List<IReporter>
        {
            new Television("Telewizja Abelowa"),
            new Television("Kanał TV-tensor"),
            new Radio("Radio Kwantyfikator"),
            new Radio("Radio Shmem"),
            new Newspaper("Gazeta Kategoryczna"),
            new Newspaper("Dziennik Politechniczny")
        };

    public NewsGenerator(List<IReporter> reporters,List<IReportable> subjects)
        {
            this.reporters = reporters;
            this.subjects = subjects;
        }
        public IEnumerable<string> GenerateNextNews()
        {
            lock (reporters)
            {
                lock (subjects)
                {
                    foreach (IReporter reporter in reporters)
                    {
                        foreach (IReportable subject in subjects)
                        {
                            yield return subject.acceptReport(reporter);
                        }
                    }
                }
            }

        }
    }
}
