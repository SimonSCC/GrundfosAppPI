using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Pump
    {
        public int Speed { get; set; }
        public string ControlMode { get; set; }
        public string OperatingMode { get; set; }
        public int Setpoint { get; set; }
    }
}
