namespace TransportManagerLibrary.DAO
{
    class ProductsDAO
    {
        // CateCode, ProdSubCatCode, ProductCode, ProductName, ProductName1, ProductDescription, 
        //Reorder, UnitType, DistPrice, DealPrice, SalePrice, OperPrice
        //              +" LandingCost, PriceInDollar, Discontinued, Entry_date, Update_Date, User_Code
        
        public  string CateCode { get; set; }
        public  string ProdSubCatCode { get; set; }
        public  string Productcode { get; set; }

        public  string ProductName { get; set; }
        public  string ShortName { get; set; }
        public  string ProductDescription { get; set; }
        public  string Reorder { get; set; }
        public  string UnitType { get; set; }
        public  string DistPrice { get; set; }
        public  string DealPrice { get; set; }
        public  string SalePrice { get; set; }
        public  string OperPrice { get; set; }
        public  string LandingCost { get; set; }
        public  string PriceInDollar { get; set; }
        public  string Discontinued { get; set; }

    }
}
