namespace TransportManagerLibrary.DAO
{
    public  class DealerDAO
    {
         //@ComCode smallint,
        //   @CustId varchar(20),
        //   @CustName varchar(50),
        //   @DealerId varchar(20),
        //   @ContactPer varchar(50),
        //   @Addrress varchar(200),
        //   @Phone varchar(30),
        //   @Mobile varchar(30),
        //   @Location varchar(100),
        //   @Status bit,
        //   @User_Code varchar(10)
        public  int ComCode { get; set; }
        public  string CustId { get; set; }
        public  string CustName { get; set; }
        public  string CustNameBangla { get; set; }
        public  string CustAddressBang { get; set; }
        public  string DealerId { get; set; }
        public  string ContactPer { get; set; }
        public  string Add1 { get; set; }
        public  string Add2 { get; set; }
        public  string Add3 { get; set; }
        
        public  string Phone { get; set; }
        public  string Mobile { get; set; }
        public  string Location { get; set; }
        public  int locDistance { get; set; }
        public  int Status { get; set; }
        
        public  void ClearAll()
        {
            ComCode = 1;
            CustId = null;
            CustName = null;
            CustNameBangla = null;
            CustAddressBang = null;
            DealerId = null;
            ContactPer = null;
            Add1 = null;
            Add2 = null;
            Add3 = null;

            Phone = null;
            Mobile = null;
            Location = null;
            Status = 0;

        }

    }
}
