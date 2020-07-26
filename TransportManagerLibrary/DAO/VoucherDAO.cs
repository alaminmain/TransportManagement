namespace TransportManagerLibrary.DAO
{
    class VoucherDAO
    {
        //ComCode, VoucherNo, VoucherDate, TripNo, Income, Advance, TotExpense, Narration, Km, KmPerLitre, FuelRate, ReturnDate, VouchStatus, Entry_date, 
                      
        public  string VoucherNo { get; set;}
        public  string VoucherDate { get; set; }
        public  string TripNo { get; set; }
        public  decimal Income { get; set; }
        public  decimal Advance { get; set; }
        public  decimal TotExpense { get; set; }
        public  decimal Km { get; set; }
        public  decimal Fuel { get; set; }
        public  decimal KmPerLitre { get; set; }
        public  decimal FuelRate { get; set; }
        public  decimal AdditionalKM { get; set; }
        public  string ReturnDate { get; set; }
        public  string Narration { get; set; }
        public  string VoucherStatus { get; set; }
    }
}
