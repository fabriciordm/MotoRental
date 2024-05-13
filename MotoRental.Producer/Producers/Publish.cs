using MotoRental.CrossCutting.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.Producer.Producers
{
    public  class Publish  
    {
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string RoutingKey { get; set; }



        public Dictionary<string, object> Headers { get; set; }
        public Dictionary<string, object> Arguments { get; set; }

        public string Data { get; private set; }
        public byte[] Body { get { return ConverterData(); } }


        protected void AddData(string data)
        {
            Data = data;
        }

        private byte[] ConverterData()
        {
            if (Data == null)
                return null;

            return JsonUtils.ToByte(Data);
        }
    }
}


