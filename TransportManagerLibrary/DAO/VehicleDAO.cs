namespace TransportManagerLibrary.DAO
{
    class VehicleDAO
    {
        //VehicleID, VehicleNo, EngineNo, VehicleDesc
        public  string VehicleId { get; set; }
        public  string VehicleNo { get; set; }
        public  string EngineNo { get; set; }
        public  string VehicleDesc { get; set; }
        public  string MobileNo { get; set; }
        public  string Capacity { get; set; }
        public  decimal KmPerLitre { get; set; }
        public  int FuelType { get; set; }
        public  int Status { get; set; }
    }
}
