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
        private int subject_index;
        private int report_index;
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
            subject_index = report_index = 0;
        }
        public string? GenerateNextNews()
        {
            if (subject_index >= subjects.Count || report_index >= reporters.Count) 
            {
                return null;
            }
            string report= subjects[subject_index].acceptReport(reporters[report_index]);
            if (subject_index == subjects.Count - 1)
            {
                subject_index = 0;
                report_index++;
            }
            else
            {
                subject_index++;
            }
            return report;
        }
    }
}
