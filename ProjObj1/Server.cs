using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class Server
    {
        private NetworkSourceSimulator.NetworkSourceSimulator NetworkSource;
        private ServerHandler Handler;
        public Server(string inPath, int minTime, int maxTime)
        {
            Handler=new ServerHandler();
            NetworkSource = new NetworkSourceSimulator.NetworkSourceSimulator(inPath, minTime, maxTime);
            NetworkSource.OnNewDataReady += Handler.EventHandler;
        }
        public void StartServer()
        {
            Task.Run(NetworkSource.Run);
        }
        public void MakeSnapshot()
        {
            lock (Handler.Objects)
            {
                DataHandler.SaveToPath(SnapshotName(), Handler.Objects);
            }
        }
        public static string SnapshotName()
        {
            DateTime CurrentTime = DateTime.Now;
            string SnapshotName = "snapshot_" + CurrentTime.Hour + "_" + CurrentTime.Minute + "_" + CurrentTime.Second + ".json";
            return SnapshotName;
        }
    }
}
