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
        public double PowerUsage { get; set; }


        private int frequence;
        public int Frequence
        {
            get { return frequence; }
            set
            {
                if (value <= 10 || value >= 55)
                {
                    FrequenceStatus = StatusCode.Warning;

                    if (value <= 5 || value >= 60)
                    {
                        FrequenceStatus = StatusCode.Danger;
                    }
                }
                else
                {
                    FrequenceStatus = StatusCode.Good;
                }

                frequence = value;
            }
        }
        public StatusCode FrequenceStatus { get; set; }


        private double flow;
        public double Flow
        {
            get { return flow; }
            set
            {
                if (value <= 4 || value >= 12)
                {
                    FlowStatus = StatusCode.Warning;

                    if (value <= 0 || value >= 16)
                    {
                        FlowStatus = StatusCode.Danger;
                    }
                }
                else
                {
                    FlowStatus = StatusCode.Good;
                }

                flow = value;
            }
        }
        public StatusCode FlowStatus { get; set; }


        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
                if (value <= 150 || value >= 1850)
                {
                    SpeedStatus = StatusCode.Warning;

                    if (value <= 100 || value >= 1900)
                    {
                        SpeedStatus = StatusCode.Danger;
                    }
                }
                else
                {
                    SpeedStatus = StatusCode.Good;
                }

                speed = value;
            }
        }
        public StatusCode SpeedStatus { get; set; }


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

        public DangerReading DoesContainDangerValues()
        {
            if (SpeedStatus == StatusCode.Danger)
            {
                return new DangerReading(Name, "Speed", Speed.ToString());
            }
            if (FlowStatus == StatusCode.Danger)
            {
                return new DangerReading(Name, "Flow", Flow.ToString());
            }
            if (FrequenceStatus == StatusCode.Danger)
            {
                return new DangerReading(Name, "Frequence", Frequence.ToString());
            }

            return null;
        }
    }


    public enum StatusCode
    {
        Good,
        Warning,
        Danger,
    }
}
