using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class PumpInfo
    {
        public int Frequence { get; set; }
        public double PowerUsage { get; set; }
        public double Flow { get; set; }

        public PumpInfo(int frequence, double powerUsage, double flow)
        {
            Frequence = frequence;
            PowerUsage = powerUsage;
            Flow = flow;
        }

        public override string ToString()
        {
            return Frequence + " " + PowerUsage + " " + Flow;
        }
    }
}
