using NetworkSourceSimulator;
using System.Text;

namespace PROJOBJ1
{
    public class ServerHandler:IDataSource
    {
        private NetworkSourceSimulator.NetworkSourceSimulator NetworkSource;
        public List<IEntity> objects { get; set; }
        public ServerHandler(string inPath, int minTime, int maxTime)
        {
            objects=new List<IEntity>();
            NetworkSource = new NetworkSourceSimulator.NetworkSourceSimulator(inPath, minTime, maxTime);
            NetworkSource.OnNewDataReady += NewMessageHandler;
            NetworkSource.OnIDUpdate += IdUpdateHandler;
            NetworkSource.OnContactInfoUpdate += ContactInfoUpdateHandler;
            NetworkSource.OnPositionUpdate += FlightPositionUpdateHandler;
        }
        public void Start()
        {
            Task.Run(NetworkSource.Run);
        }
        
        private void NewMessageHandler(object sender, NewDataReadyArgs args)
        {
            
            NetworkSourceSimulator.NetworkSourceSimulator server = (NetworkSourceSimulator.NetworkSourceSimulator)sender;
            Message msg = server.GetMessageAt(args.MessageIndex);
            (string code, Byte[] instanceData) = GetMessageInfo(msg);
            lock (objects)
            {
                IEntity instance = DataHandler.Factories[code].CreateInstance(instanceData);
                instance.addToDatabase();
                objects.Add(instance);
            }
            return;
        }

        private void IdUpdateHandler(object sender,IDUpdateArgs args)
        {
            Database.UpdateID(args.ObjectID,args.NewObjectID);
        }

        private void ContactInfoUpdateHandler(object sender, ContactInfoUpdateArgs args)
        {
            Database.UpdateContactInfo(args.ObjectID,args.EmailAddress,args.PhoneNumber);
        }

        private void FlightPositionUpdateHandler(object sender, PositionUpdateArgs args)
        {
            Database.UpdateFlightPosition(args.ObjectID,args.Latitude,args.Longitude,args.AMSL);
        }
        private (string, Byte[]) GetMessageInfo(Message msg)
        {
            string code;
            byte[] instanceData;
            using (MemoryStream memoryStream = new MemoryStream(msg.MessageBytes))
            {
                using (BinaryReader read = new BinaryReader(memoryStream))
                {
                    byte[] codeBytes = read.ReadBytes(3);
                    code = Encoding.ASCII.GetString(codeBytes);
                    code = CodeParser(code);
                    Int32 length = read.ReadInt32();
                    instanceData = read.ReadBytes(length);
                }
            }
            return (code, instanceData);
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
