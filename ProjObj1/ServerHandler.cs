using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class ServerHandler
    {
        private NetworkSourceSimulator.NetworkSourceSimulator NetworkSource;
        public List<IEntity> objects;
        public Visitor visitor;
        public ServerHandler(string inPath, int minTime, int maxTime)
        {

            objects=new List<IEntity>();
            visitor=new Visitor();
            NetworkSource = new NetworkSourceSimulator.NetworkSourceSimulator(inPath, minTime, maxTime);
            NetworkSource.OnNewDataReady += EventHandler;
        }
        public void StartServer()
        {
            Task.Run(NetworkSource.Run);
            Task.Run(()=>ConsoleReact());
        }
        
        public void ConsoleReact()
        {
            string Command = "";
            while ((Command = Console.ReadLine()) != null)
            {
                if (Command == "exit")
                {
                    Environment.Exit(0);
                }
                if (Command == "print")
                {
                    MakeSnapshot();
                }
            }
        }
        public void MakeSnapshot()
        {

            lock (objects) 
            {
                DataHandler.SaveToPath(SnapshotName(), objects);
            }
        }
        public static string SnapshotName()
        {
            DateTime CurrentTime = DateTime.Now;
            string SnapshotName = "snapshot_" + CurrentTime.Hour + "_" + CurrentTime.Minute + "_" + CurrentTime.Second + ".json";
            return SnapshotName;
        }
        public void EventHandler(object sender, NewDataReadyArgs args)
        {
            NetworkSourceSimulator.NetworkSourceSimulator server = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
            Message msg = server.GetMessageAt(args.MessageIndex);
            (string Code, Byte[] instanceData) = GetMessageInfo(msg);
            lock (objects)
            {
                IEntity instance = DataHandler.Factories[Code].CreateInstance(instanceData);
                instance.accept(visitor);
                objects.Add(instance);
            }
            return;
        }
        private (string, Byte[]) GetMessageInfo(Message msg)
        {
            Int32 Length;
            string Code;
            byte[] InstanceData;
            using (MemoryStream memoryStream = new MemoryStream(msg.MessageBytes))
            {
                using (BinaryReader read = new BinaryReader(memoryStream))
                {
                    byte[] CodeBytes = read.ReadBytes(3);
                    Code = Encoding.ASCII.GetString(CodeBytes);
                    Code = CodeParser(Code);
                    Length = read.ReadInt32();
                    InstanceData = read.ReadBytes(Length);
                }
            }
            return (Code, InstanceData);
        }
        private string CodeParser(string Code)
        {
            switch (Code)
            {
                case "NCR": return "C";
                case "NPA": return "P";
                case "NCA": return "CA";
                case "NCP": return "CP";
                case "NPP": return "PP";
                case "NAI": return "AI";
                case "NFL": return "FL";
                default: return "";
            }
        }
    }
}
