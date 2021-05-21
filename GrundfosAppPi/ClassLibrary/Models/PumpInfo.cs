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
        public int Speed { get; set; }

        public PumpInfo(string name, int frequence, double powerUsage, double flow, int speed)
        {
            Name = name;
            Frequence = frequence;
            PowerUsage = powerUsage;
            Flow = flow;
            Speed = speed;
        }

        public override string ToString()
        {
            return Name + " " + Frequence + " " + PowerUsage + " " + Flow + " " + Speed;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
