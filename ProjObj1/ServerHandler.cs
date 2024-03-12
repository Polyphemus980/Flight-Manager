using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PROJOBJ1
{
    public class ServerHandler
    {
        public List<IEntity> Objects;
        
        public ServerHandler() 
        {
            Objects=new List<IEntity>();
        }
        public void EventHandler(object sender, NewDataReadyArgs args)
        {
            NetworkSourceSimulator.NetworkSourceSimulator server = (NetworkSourceSimulator.NetworkSourceSimulator) sender;
            Message msg = server.GetMessageAt(args.MessageIndex);
            (string Code, Byte[] InstanceData)= GetMessageInfo(msg);
            lock (Objects)
            {
                Objects.Add(DataHandler.Factories[Code].CreateInstance(InstanceData));
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
                    Code=CodeParser(Code);
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
