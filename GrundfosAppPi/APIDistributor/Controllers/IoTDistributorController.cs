using APIDistributor.Services;
using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDistributor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IoTDistributorController : ControllerBase
    {
       readonly ReceiverLogic _receiverLogic;

        public IoTDistributorController(ReceiverLogic receiverLogic)
        {
            _receiverLogic = receiverLogic;
        }

        [HttpGet]
        public PumpInfo GetLatestReadings()
        {
            return _receiverLogic.LastReadValues;
        }    

    }
}
