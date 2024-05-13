using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoRental.CrossCutting.MessageBus
{
    public static class JsonUtils
    {
        public static T Deserialize<T>(byte[] body) where T : class
        {
            var message = Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<T>(message);
        }
        public static byte[] ToByte(object obj)
        {
            if (obj == null)
                return null;

            var message = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(message);

        }
    }
}
