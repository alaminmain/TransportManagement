using System;
using System.Data;

namespace TransportManagerLibrary.DAL
{
    public class SearchGateway:Gateway
    {
        CommonGateway commonGatewayObj=new CommonGateway();


        public DataTable searchItem(string searchLoad, string searchBy, string searchKey)
        {
           
            DataTable dt = new DataTable();
            switch (searchLoad)
            {
                    //Dealer
                case "Dealer":


                    if (searchBy == "1")
                    {

                        string sqlQurey = "SELECT Top 200 ComCode, CustId, CustName, DealerId, ContactPer, Add1, Add2, Add3, Phone, Mobile, Location,LocDistance,  Status FROM  Customer" +
" WHERE     (DealerId IS NULL) AND (CustId Like  '%@SeachKey%')";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                    }
                    else
                    {
                        string sqlQurey = "SELECT     ComCode, CustId, CustName, DealerId, ContactPer, Add1, Add2, Add3, Phone, Mobile, Location,LocDistance, Status FROM  Customer" +
" WHERE     (DealerId IS NULL) AND (CustName Like  '%@SeachKey%')";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }

                    break;

                case "AllCustomer":
                   

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT     ComCode, CustId, CustName,CustNameBangla,CustAddressBang, DealerId, ContactPer, Add1, Add2, Add3, Phone, Mobile, Location,LocDistance, Status"
+" FROM         Customer"+
" WHERE (CustId Like  '%@SeachKey%')";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }
                    else
                    {
                        string sqlQurey = "SELECT     ComCode, CustId, CustName,CustNameBangla,CustAddressBang, DealerId, ContactPer, Add1, Add2, Add3, Phone, Mobile, Location,LocDistance,  Status"
+" FROM         Customer"+
" WHERE  (CustName Like '%@SeachKey%')";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }
                    
                    break;

                    //Customer
                case "Customer":
                   

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT  CustId, CustName, DealerId, Designation, Addrress, Mobile FROM  Customer" +
" WHERE   (DealerId IS NULL) and  CustId like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }
                    else
                    {
                        string sqlQurey = "SELECT   CustId, CustName, DealerId, Designation, Addrress, Mobile FROM  Customer" +
" WHERE   (DealerId IS  NULL) and  CustName like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }

                    break;

                    //Location
                case "Location":
                   

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT    LocSLNO, LocName, LocDistance FROM         Location" +
" WHERE     LocSLNO like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }
                    else
                    {
                        string sqlQurey = "SELECT    LocSLNO, LocName, LocDistance FROM         Location" +
" WHERE     LocName like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }

                    break;

                    //Vehicle
                case "Vehicle":
                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT  VehicleID, VehicleNo, EngineNo, VehicleDesc,MobileNo,Capacity, KmPerLiter, fuelCode, status FROM  VehicleInfo" +
" WHERE  VehicleID like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }
                    else
                    {
                        string sqlQurey = " SELECT VehicleID, VehicleNo, EngineNo, VehicleDesc,MobileNo,Capacity, KmPerLiter, fuelCode, status FROM  VehicleInfo" +
" WHERE  VehicleNo like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }

                    break;

                    //Product
                case "Product":
                    

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT     CateCode, ProdSubCatCode, ProductCode, ProductName, ProductName1, ProductDescription, Reorder, UnitType, DistPrice, DealPrice, SalePrice, OperPrice,"
                      +" LandingCost, PriceInDollar, Discontinued"
+" FROM         Product" +
" WHERE     ProductCode like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }
                    else
                    {
                        string sqlQurey = "SELECT     CateCode, ProdSubCatCode, ProductCode, ProductName, ProductName1, ProductDescription, Reorder, UnitType, DistPrice, DealPrice, SalePrice, OperPrice,"
                      + " LandingCost, PriceInDollar, Discontinued"
+ " FROM         Product" +
" WHERE     ProductName like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }

                    break;

                    //Driver Search
                case "Driver":
                   

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT      EmpCode, Cardno, EmpName, ComCode, ShiftId, EmpType, MGR, GradeID, DeptID, DesignationID, AppointDate, JoiningDate, ConfirmDate, PBXExt, "
                     +" EducDegree, BirthDate, Sex, MeritalStatus, SpouseName, FatherName, MotherName, BloodGroup, Add1, Add2, Add3, PostCode, Country, Phone, Mobile, Fax, Email, "
                     +" PStatus,DrivingLicen"
                        +" FROM         Personal " +
                                          "WHERE     EmpCode like '%@SeachKey%' Order By EmpCode"; 
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey); 
                        
                    }
                    else
                    {
                        string sqlQurey = "SELECT      EmpCode, Cardno, EmpName, ComCode, ShiftId, EmpType, MGR, GradeID, DeptID, DesignationID, AppointDate, JoiningDate, ConfirmDate, PBXExt, "
                     + " EducDegree, BirthDate, Sex, MeritalStatus, SpouseName, FatherName, MotherName, BloodGroup, Add1, Add2, Add3, PostCode, Country, Phone, Mobile, Fax, Email, "
                     + " PStatus,DrivingLicen"
                        + " FROM         Personal " +
" WHERE     EmpName like '%@SeachKey%' Order By EmpCode";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey);
                    }

                    break;


                     //Vehicle Movement Search Form
            case "Transport":
                    

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT     Transport.TripNo, Transport.MovementNo, Transport.TransportDate, Transport.InvNo, Transport.DealerId, Customer.CustName AS DealerName, "
                     +" Customer_1.CustName, Transport.CustId, Transport.LocSLNO, Location.LocName, Transport.VehicleID, VehicleInfo.VehicleNo, Transport.EmpCode, "
                      +"Personal.EmpName, Transport.Remarks, Transport.TranStatus"
                       +" FROM         Transport INNER JOIN "
                      +" Customer ON Transport.DealerId = Customer.CustId INNER JOIN"
                      +" Customer AS Customer_1 ON Transport.CustId = Customer_1.CustId LEFT OUTER JOIN"
                      +" VehicleInfo ON Transport.VehicleID = VehicleInfo.VehicleID LEFT OUTER JOIN"
                      +" Location ON Transport.LocSLNO = Location.LocSLNO LEFT OUTER JOIN"
                      +" Personal ON Transport.EmpCode = Personal.EmpCode "+
" WHERE     TripNo like '%@SeachKey%' Order By Transport.TripNo DESC"; 
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey, searchKey); 
                        
                    }
                    else
                    {

                        string sqlQurey = "SELECT top 300    Transport.TripNo, Transport.MovementNo, Transport.TransportDate, Transport.InvNo, Transport.DealerId, Customer.CustName AS DealerName, "
                     + " Customer_1.CustName, Transport.CustId, Transport.LocSLNO, Location.LocName, Transport.VehicleID, VehicleInfo.VehicleNo, Transport.EmpCode, "
                      + "Personal.EmpName, Transport.Remarks, Transport.TranStatus"
                       + " FROM         Transport INNER JOIN "
                      + " Customer ON Transport.DealerId = Customer.CustId INNER JOIN"
                      + " Customer AS Customer_1 ON Transport.CustId = Customer_1.CustId LEFT OUTER JOIN"
                      + " VehicleInfo ON Transport.VehicleID = VehicleInfo.VehicleID LEFT OUTER JOIN"
                      + " Location ON Transport.LocSLNO = Location.LocSLNO LEFT OUTER JOIN"
                      + " Personal ON Transport.EmpCode = Personal.EmpCode  " +
" WHERE     Transport.MovementNo like '%@SeachKey%' Order By Transport.TransportDate DESC";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                    }

                    break;
                    //Account Searhc
            case "Accounts":
                    

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT     AccountCode, AccountDesc, Remarks, AccCategory FROM         ChartofAccount" +
" WHERE  AccountCode like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                    }
                    else
                    {
                        string sqlQurey = "SELECT     AccountCode, AccountDesc, Remarks, AccCategory FROM         ChartofAccount " +
" WHERE  AccountDesc like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                    }

                    break;

                    //Voucher Search
            case "Voucher":
                    

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT     ComCode, VoucherNo, VoucherDate, TripNo, Income, Advance, TotExpense, Narration, Km,Fuel, KmPerLitre, FuelRate,AdditionalKM, ReturnDate, VouchStatus FROM         Voucher" +
" WHERE     VoucherNo like '%@SeachKey%' Order By VoucherNo DESC";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                    }
                    else
                    {
//                        string sqlQurey = "SELECT  ComCode, VoucherNo, VoucherDate, TripNo, Income, Advance, TotExpense, Narration, Km,Fuel,KmPerLitre, FuelRate,AdditionalKM, ReturnDate, VouchStatus FROM  Voucher" +
//" WHERE     TripNo like '%" + searchKey + "%' Order By VoucherNo DESC";


                        string sqlQurey = "SELECT top 200    ComCode, VoucherNo, VoucherDate, TripNo, Income, Advance, TotExpense, Narration, Km,Fuel,KmPerLitre, FuelRate,AdditionalKM, ReturnDate, VouchStatus FROM  Voucher" +
" WHERE     TripNo like '%@SeachKey%' Order By VoucherNo DESC";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                    }

                    break;

            case "OilReq":
                    
                    OilReqGateway oilReqObj=new OilReqGateway();
                       
                   //dt = oilReqObj.get_Oid_Req_Detail();

                    if (searchBy == "1")
                    {
                        string sqlQurey = "SELECT  ComCode, FuelSlipSLNo, Date, TripSLNo, SupplierName, FuelQty, Rate, FuelCode,Remarks,km,AdditionalFuel,Status "
+ "FROM         OilRequsition" +
 " WHERE     FuelSlipSLNo like '%@SeachKey%' Order By FuelSlipSLNo DESC";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                       
                    }
                    else
                    {

                        /////////////////////////////////////////////////////////

//                        string sqlQurey = "SELECT  ComCode, FuelSlipSLNo, Date, TripSLNo, SupplierName, FuelQty, Rate, FuelCode,Remarks,km,AdditionalFuel,Status "
//+ " FROM         OilRequsition" +
// " WHERE     TripSLNo like '%" + searchKey + "%' Order By FuelSlipSLNo DESC";


                        string sqlQurey = "SELECT top 200 ComCode, FuelSlipSLNo, Date, TripSLNo, SupplierName, FuelQty, Rate, FuelCode,Remarks,km,AdditionalFuel,Status "
+ " FROM         OilRequsition" +
 " WHERE     TripSLNo like '%@SeachKey%' Order By Date DESC";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                        
                    }
                    break;
                    //User
            case "User":
                    {
                        string sqlQurey = "SELECT   ComCode, CustId, UserName, UserPwd, User_Perm "
                                          + "FROM         UserInfo" +
                                          " WHERE     UserName like '%@SeachKey%'";
                        dt = commonGatewayObj.Get_Item_BySearch(sqlQurey,searchKey);
                    }
                    break;
                default:
                    throw new ArgumentException
                    (
                    "GetDataReader was given an incorrect Request for data"
                    );    

            }
//Return Data table

            return dt;
        }

        public DataTable LoadCustomerByDealer(string searchLoad, string searchBy, string searchKey, string condition)
        {
            DataTable dt = new DataTable();
            
            
                if (searchBy == "1")
                {
                    string sqlQurey =
                        "SELECT  ComCode, CustId, CustName, DealerId, ContactPer, Add1, Add2, Add3, Phone, Mobile, Location,[LocDistance], Status FROM  Customer" +
" WHERE     (DealerId ='"+ condition +"')  AND (CustId Like  '%" + searchKey + "%')";
                    dt = commonGatewayObj.Get_Item_BySearch(sqlQurey);
                }
                else
                {
                    string sqlQurey =
                        "SELECT     ComCode, CustId, CustName, DealerId, ContactPer, Add1, Add2, Add3, Phone, Mobile, Location,[LocDistance], Status FROM  Customer" +
                        " WHERE     (DealerId='"+ condition +"') AND (CustName Like  '%" + searchKey + "%')";
                    dt = commonGatewayObj.Get_Item_BySearch(sqlQurey);
                }

            
            return dt;
        }
    }
}
