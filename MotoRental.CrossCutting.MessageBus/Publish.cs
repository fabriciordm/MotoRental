using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.CrossCutting.MessageBus
{
    public abstract class Publish<T> where T : class
    {
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string RoutingKey { get; set; }


      
        public Dictionary<string, object> Headers { get; set; }
        public Dictionary<string, object> Arguments { get; set; }

        public T Data { get; private set; }
        public byte[] Body { get { return ConverterData(); } }


        protected void AddData(T data)
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
