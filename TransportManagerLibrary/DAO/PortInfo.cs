using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportManagerLibrary.DAO
{
    class PortInfo
    {
        public  string PortNam { get; set; }
        public  int BaudRate { get; set; }
        public  int Databits { get; set; }
        public  int ReadTimeOut { get; set; }
        public  int WriteTimeOut { get; set; }
    }
}
