namespace TransportManagerLibrary.DAO
{
    class ModemInfoDAO
    {
        public  string p_strPortName { get; set; }
        public  int p_uBaudRate { get; set; }
        public  int p_uDataBits { get; set; }
        public  int p_uReadTimeout { get; set; }
        public  int p_uWriteTimeout { get; set; }
    }
}
