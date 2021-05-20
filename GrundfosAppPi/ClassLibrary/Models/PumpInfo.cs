using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary.Models
{
    public class PumpInfo
    {
        public string Name { get; set; }
        public int Frequence { get; set; }
        public double PowerUsage { get; set; }
        public double Flow { get; set; }

        public PumpInfo(string name, int frequence, double powerUsage, double flow)
        {
            Name = name;
            Frequence = frequence;
            PowerUsage = powerUsage;
            Flow = flow;
        }

        public override string ToString()
        {
            return Name + " " + Frequence + " " + PowerUsage + " " + Flow;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
