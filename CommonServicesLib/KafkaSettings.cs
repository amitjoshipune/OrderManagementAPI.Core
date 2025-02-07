using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesLib
{
    public  class KafkaSettings
    {
        public string BootstrapServers { get; set; }
        public string CartTopic { get; set; }
        public string OrderTopic { get; set; }
    }
}
