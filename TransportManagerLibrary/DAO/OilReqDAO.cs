using System;

namespace TransportManagerLibrary.DAO
{
    class OilReqDAO
    {
        //public  int ComCode_1{get;set;}
        public  string FuelSlipSLNo_2{get;set;}
        public  DateTime Date_3{get;set;}
        public  string TripNo{get;set;}
        public  string SupplierName_5{get;set;}
        public  decimal FuelQty_6{get;set;}
        public  decimal Rate_7{get;set;}
        public  int FuelCode_8 { get; set; }
        public  string Remarks_10 { get; set; }
        public  decimal Km { get; set; }
        public  bool Additional { get; set; }
        public  byte Status { get; set; }

        public  void ClearAll()
        {
            FuelSlipSLNo_2 = null;
            Date_3 = DateTime.Today;
            TripNo = null;
            SupplierName_5 = null;
            FuelQty_6 = 0;
            Rate_7 = 0;
            FuelCode_8 = 0;
            Remarks_10 = null;
        }
    }
}
