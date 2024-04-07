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
        public NewsGenerator(List<IReporter> reporters,List<IReportable> subjects)
        {
            this.reporters = reporters;
            this.subjects = subjects;
        }
        public IEnumerable<string> GenerateNextNews()
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
