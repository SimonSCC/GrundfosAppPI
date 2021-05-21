using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Pump
    {
        Random rand = new Random();


        //Static values
        public string Name { get; set; }
        public string ControlMode { get; set; }
        public string OperatingMode { get; set; }
        public int Setpoint { get; set; }


        //Dynamic values
        public bool TurnedOn { get; set; } = true;


        private double flow;
        public double Flow
        {
            get
            {
                //over 16 er farlig
                //0 er farlig

                flow = rand.NextDouble();
                flow += rand.Next(0, 17);
                flow = Math.Round(flow, 2);

                return flow;
            }
        }


        private double powerUsage;
        public double PowerUsage
        {
            get
            {
                //No danger

                powerUsage += 0.01;
                powerUsage = Math.Round(powerUsage, 2);

                return powerUsage;
            }
        }


        private int speed;
        public int Speed
        {
            get
            {
                //under 100rpm
                //over 1900rpm

                speed = rand.Next(0, 2000);

                return speed;
            }
        }


        private int frequence;
        public int Frequence
        {
            get
            {
                //under 5 er farlig, 
                //over 60 er farlig
                frequence = rand.Next(0, 65);

                return frequence;
            }
        }


        public Pump(string name)
        {
            Name = name;
        }

        public PumpInfo GetValues()
        {
            return new PumpInfo(Name, Frequence, PowerUsage, Flow, Speed);
        }
    }
}
