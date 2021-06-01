using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Data;
using ClassLibrary.Models;


namespace APIDistributor.Services
{
    public class ReceiverLogic
    {
        public PumpInfo LastReadValues;
        readonly IOTReceiver _receiver;

        public ReceiverLogic(IOTReceiver receiver)
        {
            _receiver = receiver;
            //_receiver.MessageReceived += (o, e) => SetLastValues();
            _receiver.MessageReceived += MessageReceivedEventHandler;
        }

        void MessageReceivedEventHandler(object sender, EventArgs e)
        {
            SetLastValues();
        }


        public void SetLastValues()
        {
            LastReadValues = _receiver.ConsumeMessage();
        }


    }
}
