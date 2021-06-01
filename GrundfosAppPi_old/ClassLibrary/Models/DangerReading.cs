using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class DangerReading
    {
        public string PumpName { get; set; }
        public string Reading { get; set; }
        public string AttributeType { get; set; } //Speed, flow, frequence

        public DangerReading(string pumpName, string attributeType, string reading)
        {
            PumpName = pumpName;
            AttributeType = attributeType;
            Reading = reading;
        }

        public override string ToString()
        {
            return "The " + AttributeType + " from " + PumpName + " is reading " + Reading + " and it's about to explode 🤯💥💣";
        }
    }
}
