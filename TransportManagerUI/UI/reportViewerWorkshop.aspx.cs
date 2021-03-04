using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;

using System.Configuration;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Xsl;
using System.Globalization;

namespace TransportManagerUI.UI.Workshop
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        private ReportDocument salesrpt;
        private string FromDate;
        private string ToDate;
        private string ReportName;
        private string str, sqlstr;
        string serialrpt;
        string department;
        string SubDepartment;
        string SupplierID;
        string PONo;
        string CateID;
        string Vehicleid;
        string driverid;
        private string CloseDate;

       
        private DataSet ds;
        Stream oStream;


        private DataSet data()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TransportDBConnectionString"].ToString());
            con.Open();


            switch (ReportName)
            {
                case "PurchaseReceive":

                    sqlstr = @"SELECT        PR.PurRecNo, PR.PurOrderNo, PO.OrderDate, PR.RecDate, PR.InvNo, PR.InvDate, PR.Remarks, PR.TotFOB, PR.Freight, PR.Insurance, PR.Others, PR.Discount, PR.User_Code, PR.Entry_Date, PR.Update_Date, PRD.OrderSLNo, 
                         PRD.ProductCode, PRD.RecQty, PRD.PurPrice, PRD.CostPrice, IST.ProdSubCatName, IC.CateName, PR.Deduction, PRD.RecQty * PRD.PurPrice AS TotalPrice, IT.ProductName, IT.ProductBName, IT.UnitType, PO.SuplierID, 
                         SP.SupplierName, CI.cCompname, CI.cAdd1, CI.cAdd2, CI.cAdd3
                            FROM            PurRec AS PR INNER JOIN
                         PurRecProd AS PRD ON PR.PurRecNo = PRD.PurRecNo INNER JOIN
                         Item AS IT ON PRD.StoreCode = IT.StoreCode INNER JOIN
                         PurOrder AS PO ON PR.PurOrderNo = PO.PurOrderNo INNER JOIN
                         ItemSubCategory AS IST ON IT.ProdSubCatCode = IST.ProdSubCatCode AND IT.CateCode = IST.CateCode INNER JOIN
                         Item_Category AS IC ON IST.CateCode = IC.CateCode INNER JOIN
                         Supplier AS SP ON PO.SuplierID = SP.SupplierID INNER JOIN
                         CompInfo AS CI ON PR.ComCode = CI.ComCode
                           where PR.PurRecNo='" + serialrpt + "'";
                    SqlCommand cmd1 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    ds = new DataSet();
                    da1.Fill(ds, "PurRecTable");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLPurRecTable.xsd"));
                    con.Close();
                    return ds;

                case "PurchaseOrder":
                    sqlstr = @"select PO.ComCode,PO.PurOrderNo,PO.OrderDate,PO.SuplierID,PO.PINo,PO.PIIssDate,PO.PurOrderDesc,PO.Entry_Date,PO.User_Code,
                           PO.OrdStatus,PO.TotOrderQty,POP.OrderSLNo,POP.PurOrderNo,POP.OrdQty,POP.ProductCode,POP.PurPrice,vrPO.TAmount as TAmount,
                           IT.DealPrice,IT.ProductCode,IT.ProductName,IT.UnitType,SP.SupplierID,SP.SupplierName,SP.Add1,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3 from PurOrder PO 
                           inner join PurOrderProd POP on PO.PurOrderNo=POP.PurOrderNo
                           inner join Item IT on POP.ProductCode=IT.ProductCode and POP.StoreCode=IT.StoreCode 
                           inner join Supplier SP on PO.SuplierID=SP.SupplierID
                           inner join dbo.vrPurOrderQty vrPO on vrPO.PurOrderNo=PO.PurOrderNo
                           inner join CompInfo CI on PO.ComCode=CI.ComCode
                           where PO.PurOrderNo='" + serialrpt + "'";
                    SqlCommand cmd2 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    ds = new DataSet();
                    da2.Fill(ds, "PurOrderTeble");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLPurOrder.xsd"));
                    con.Close();
                    return ds;

                case "IssuStock":
                    sqlstr = @"SELECT     VI.ComCode, VI.IssVouchNo, VI.IssDate, VI.VehicleID, VI.ModelNo, VI.IssuStatus, VI.EmpCode AS DriverID,p.EmpName as TechnicianName,VD.Comment, VI.Remarks, VI.FromDate, VI.ToDate, 
                           VI.Entry_Date, VI.User_Code, ITS.ProdSubCatName, IT.ProdSubCatCode, VD.ProductCode, VD.IssQty, VD.PurPrice, IC.CateName, IT.ProductName,IT.ProductBName,VI.TechnitianID,
                           IT.UnitType, PR.EmpName AS DriverName,  VE.VehicleNo, CI.cCompname, CI.cAdd1, CI.cAdd2, CI.cAdd3
                      FROM  dbo.VMFIssue AS VI INNER JOIN
						    dbo.VMFIssueDetail AS VD ON VI.IssVouchNo = VD.IssVouchNo AND VI.ComCode = VD.ComCode INNER JOIN
						    dbo.Item AS IT ON VD.ProductCode = IT.ProductCode AND VD.StoreCode=IT.StoreCode INNER JOIN
						    dbo.Item_Category AS IC ON VI.CateCode = IC.CateCode INNER JOIN
						    dbo.Personal AS PR ON VI.EmpCode = PR.EmpCode INNER JOIN
						    dbo.VehicleInfo AS VE ON VI.VehicleID = VE.VehicleID INNER JOIN
						    dbo.Personal AS p ON VI.TechnitianID = p.EmpCode INNER JOIN
						    dbo.CompInfo AS CI ON VI.ComCode = CI.ComCode INNER JOIN
						    dbo.ItemSubCategory AS ITS ON VI.CateCode = ITS.CateCode AND VI.ProdSubCatCode = ITS.ProdSubCatCode
                            where VI.IssVouchNo='" + serialrpt + "'and VI.CateCode='" + department + "'";
                    SqlCommand cmd3 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    ds = new DataSet();
                    da3.Fill(ds, "StockIssue");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLStockIssue.xsd"));
                    con.Close();
                    return ds;


                case "InternalRequisition":
                    sqlstr = @"SELECT     SI.InternalReqNo,SI.ComCode, SI.IssVouchNo, SI.IssDate, SI.VehicleID, SI.ModelNo, SI.IssuStatus, SI.EmpCode AS DriverID,p.EmpName as TechnicianName,SD.Comment, SI.Remarks, SI.FromDate, SI.ToDate, 
                      SI.Entry_Date, SI.User_Code, ITS.ProdSubCatName, IT.ProdSubCatCode, SD.ProductCode, SD.IssQty, SD.PurPrice, SD.IssQty AS Expr1, IC.CateName, IT.ProductName,IT.ProductBName,
                      Case  when SI.IssuStatus='1' then''
							    when SI.IssuStatus>='2' then'gvjvgvj ‡`Iqv †h‡Z cv‡i '
							    --when SI.IssuStatus='3' then'gvjvgvj ‡`Iqv †nvK '
							    else '' end 'Notification1' ,
					  Case  when SI.IssuStatus='1' then''
							    --when SI.IssuStatus='2' then'gvjvgvj ‡`Iqv †h‡Z cv‡i '
							    when SI.IssuStatus>='3' then'gvjvgvj ‡`Iqv †nvK '
							    else '' end 'Notification2' ,
								IT.UnitType, PR.EmpName AS DriverName, p.EmpName AS TechnicianName, VI.VehicleNo, CI.cCompname, CI.cAdd1, CI.cAdd2, CI.cAdd3,SI.CateCode
                      FROM         dbo.StockIssue AS SI INNER JOIN
							dbo.StockIssueDetail AS SD ON SI.InternalReqNo = SD.InternalReqNo AND SI.ComCode = SD.ComCode INNER JOIN
							dbo.Item AS IT ON SD.ProductCode = IT.ProductCode AND SD.StoreCode=IT.StoreCode INNER JOIN
							dbo.Item_Category AS IC ON SI.CateCode = IC.CateCode INNER JOIN
							dbo.Personal AS PR ON SI.EmpCode = PR.EmpCode INNER JOIN
							dbo.VehicleInfo AS VI ON SI.VehicleID = VI.VehicleID INNER JOIN
							dbo.Personal AS p ON SI.TechnitianID = p.EmpCode INNER JOIN
							dbo.CompInfo AS CI ON SI.ComCode = CI.ComCode INNER JOIN
							dbo.ItemSubCategory AS ITS ON SI.CateCode = ITS.CateCode AND SI.ProdSubCatCode = ITS.ProdSubCatCode";


                    SqlCommand cmd7 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
                    ds = new DataSet();
                    da7.Fill(ds, "InternalRequisition");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLInternalRequisition.xsd"));
                    con.Close();
                    return ds;


                case "Requisition":
                    sqlstr = @"select PR.ReqNo,PR.ReqDate,PR.User_Code,PRD.ReqQty,CAST((PRD.ReqPrice) AS DECIMAL(18,2)) AS ReqPrice,PRD.PhyStock,PRD.RequireDay,PRD.UsedPerMonth,PRD.Comments,PRD.PrePurDate,PrePurQTY,IST.StoreCode,IT.ProductName,IT.ProductBName,IT.UnitType,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,IC.CateName,ITS.ProdSubCatName from PurRequisition PR inner join PurRequisitionDTL PRD on PR.ReqNo=PRD.ReqNo
                            inner join Item IT on PRD.ProductCode=IT.ProductCode and PR.CateCode=it.CateCode and PR.ProdSubCatCode=IT.ProdSubCatCode and PRD.StoreCode=IT.StoreCode
                            inner join ItemStock IST on PRD.ProductCode=IST.ProductCode and IT.StoreCode=IST.StoreCode
                            inner join CompInfo CI on PR.ComCode=CI.ComCode
                            inner join Item_Category IC on PR.CateCode=IC.CateCode
                            inner join ItemSubCategory ITS on IC.CateCode=ITS.CateCode and PR.ProdSubCatCode=ITS.ProdSubCatCode
                            where PR.ReqNo='" + serialrpt + "'and (PR.Status='1' OR PR.Status='2')";
                    SqlCommand cmd4 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                    ds = new DataSet();
                    da4.Fill(ds, "Requisition");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLRequisition.xsd"));
                    con.Close();
                    return ds;



                case "AdministrativWork":
                    sqlstr = @"select AW.AdministrativNo, AW.IssDate,AW.AttendDate, AW.Remarks,AW.ModelNo,AWD.ItemID,AWD.ExpDate,CAST((AWD.Amount) AS DECIMAL(18,2)) AS Amount,AWD.Comments,PR.EmpName,VI.VehicleNo,ci.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,AW.CateID,AW.EmpCode,AIC.CateName,ADI.ItemName,ADI.Dhara,AW.SergeantName,AW.Area,AW.Discount
						from AdministrativWork AW inner join AdministrativWorkDTL AWD on AW.AdministrativNo=AWD.AdministrativNo and AW.ComCode=AWD.ComCode
                           inner join Personal PR on AW.EmpCode=PR.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join CompInfo CI on AW.ComCode=CI.ComCode
                           inner join AdminItemCate AIC on AW.CateID=AIC.CateID
                           inner join AdminItem ADI on AWD.ItemID=ADI.ItemID
                           where aw.AdministrativNo='" + serialrpt + "'";
                    SqlCommand cmd5 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                    ds = new DataSet();
                    da5.Fill(ds, "AdministrativWork");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLAdministrativWork.xsd"));
                    con.Close();
                    return ds;


                case "RequisitionStatement":
                    //                sqlstr = @"select PR.ReqNo,PR.ReqDate,PR.User_Code,PRD.ReqQty,CAST((PRD.ReqPrice) AS DECIMAL(18,2)) AS ReqPrice,PRD.PhyStock,PRD.RequireDay,PRD.UsedPerMonth,PRD.Comments,PRD.PrePurDate,PrePurQTY,IST.StoreCode,IT.ProductName,IT.ProductBName,IT.UnitType,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,IC.CateName,ITS.ProdSubCatName from PurRequisition PR inner join PurRequisitionDTL PRD on PR.ReqNo=PRD.ReqNo
                    //                            inner join Item IT on PRD.ProductCode=IT.ProductCode and PR.CateCode=it.CateCode and PR.ProdSubCatCode=IT.ProdSubCatCode and PRD.StoreCode=IT.StoreCode
                    //                            inner join ItemStock IST on PRD.ProductCode=IST.ProductCode and IT.StoreCode=IST.StoreCode
                    //                            inner join CompInfo CI on PR.ComCode=CI.ComCode
                    //                            inner join Item_Category IC on PR.CateCode=IC.CateCode
                    //                            inner join ItemSubCategory ITS on IC.CateCode=ITS.CateCode and PR.ProdSubCatCode=ITS.ProdSubCatCode
                    //                           where PR.Status='1' and PR.ReqDate between CONVERT(smalldatetime,'"+ FromDate+"', 103) and CONVERT(smalldatetime,'"+ ToDate+"', 103)  order by PR.ReqDate desc";


                    //SqlCommand cmd6 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da6 = new SqlDataAdapter("usp_ReportRequisitionStatement", con);
                    da6.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter p1 = new SqlParameter("@FromDate", SqlDbType.VarChar, 10);
                    p1.Value = FromDate.ToString();
                    da6.SelectCommand.Parameters.Add(p1);
                    SqlParameter p2 = new SqlParameter("@ToDate", SqlDbType.VarChar, 10);
                    p2.Value = ToDate.ToString();
                    da6.SelectCommand.Parameters.Add(p2);

                    ds = new DataSet();
                    da6.Fill(ds, "RequisitionStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLRequisitionStatement.xsd"));

                    con.Close();

                    return ds;

                case "SupplierStutas":
                    sqlstr = @"select Supplier.SupplierID,Supplier.SupplierName,Supplier.ContactName,Supplier.Phone,(Supplier.Add1+', '+Supplier.Add2+', '+Supplier.Add3)as Address,Supplier.Mobile,Supplier.Email,CompInfo.cCompname,CompInfo.cAdd1,CompInfo.cAdd2,CompInfo.cAdd3 from Supplier inner join CompInfo on Supplier.ComCode=CompInfo.ComCode where Status='True' ";
                    SqlCommand cmd8 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da8 = new SqlDataAdapter(cmd8);
                    ds = new DataSet();
                    da8.Fill(ds, "SupplierStutas");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLSupplierStutas.xsd"));
                    con.Close();
                    return ds;

                case "Bill":
                    //                sqlstr = @"select PR.ReqNo,PR.ReqDate,PR.User_Code,PRD.ReqQty,CAST((PRD.ReqPrice) AS DECIMAL(18,2)) AS ReqPrice,PRD.PhyStock,PRD.RequireDay,PRD.UsedPerMonth,PRD.Comments,PRD.PrePurDate,PrePurQTY,IST.StoreCode,IT.ProductName,IT.ProductBName,IT.UnitType,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,IC.CateName,ITS.ProdSubCatName from PurRequisition PR inner join PurRequisitionDTL PRD on PR.ReqNo=PRD.ReqNo
                    //                            inner join Item IT on PRD.ProductCode=IT.ProductCode and PR.CateCode=it.CateCode and PR.ProdSubCatCode=IT.ProdSubCatCode and PRD.StoreCode=IT.StoreCode
                    //                            inner join ItemStock IST on PRD.ProductCode=IST.ProductCode and IT.StoreCode=IST.StoreCode
                    //                            inner join CompInfo CI on PR.ComCode=CI.ComCode
                    //                            inner join Item_Category IC on PR.CateCode=IC.CateCode
                    //                            inner join ItemSubCategory ITS on IC.CateCode=ITS.CateCode and PR.ProdSubCatCode=ITS.ProdSubCatCode
                    //                           where PR.Status='1' and PR.ReqDate between CONVERT(smalldatetime,'"+ FromDate+"', 103) and CONVERT(smalldatetime,'"+ ToDate+"', 103)  order by PR.ReqDate desc";


                    //SqlCommand cmd6 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da9 = new SqlDataAdapter("usp_ReportRequisitionStatement", con);
                    da9.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter p3 = new SqlParameter("@FromDate", SqlDbType.VarChar, 10);
                    p3.Value = FromDate.ToString();
                    da9.SelectCommand.Parameters.Add(p3);
                    SqlParameter p4 = new SqlParameter("@ToDate", SqlDbType.VarChar, 10);
                    p4.Value = ToDate.ToString();
                    da9.SelectCommand.Parameters.Add(p4);

                    ds = new DataSet();
                    da9.Fill(ds, "RequisitionStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLRequisitionStatement.xsd"));

                    con.Close();

                    return ds;

                case "InternalRequisition2":
                    sqlstr = @"SELECT     SI.InternalReqNo,SI.ComCode, SI.IssVouchNo, SI.IssDate, SI.VehicleID, SI.ModelNo, SI.IssuStatus, SI.EmpCode AS DriverID,SD.Comment, SI.Remarks, SI.FromDate, SI.ToDate, 
                           SI.Entry_Date, SI.User_Code, ITS.ProdSubCatName,SI.ProdSubCatCode, SD.ProductCode, SD.IssQty, SD.PurPrice, IC.CateName, IT.ProductName,IT.ProductBName,SI.CateCode,IT.UnitType,
                           Case  when SI.IssuStatus='1' then''
							    when SI.IssuStatus>='2' then'gvjvgvj ‡`Iqv †h‡Z cv‡i '
							    --when SI.IssuStatus='3' then'gvjvgvj ‡`Iqv †nvK '
							    else '' end 'Notification1' ,
							    
							Case  when SI.IssuStatus='1' then''
							    --when SI.IssuStatus='2' then'gvjvgvj ‡`Iqv †h‡Z cv‡i '
							    when SI.IssuStatus>='3' then'gvjvgvj ‡`Iqv †nvK '
							    else '' end 'Notification2' ,
                           IT.UnitType AS DriverName,  CI.cCompname, CI.cAdd1, CI.cAdd2, CI.cAdd3
                      FROM         dbo.StockIssue AS SI INNER JOIN
                            dbo.StockIssueDetail AS SD ON SI.InternalReqNo = SD.InternalReqNo AND SI.ComCode = SD.ComCode INNER JOIN
                            dbo.Item AS IT ON SD.ProductCode = IT.ProductCode AND SD.StoreCode=IT.StoreCode INNER JOIN
                            dbo.Item_Category AS IC ON SI.CateCode = IC.CateCode INNER JOIN
                            dbo.CompInfo AS CI ON SI.ComCode = CI.ComCode INNER JOIN
                            dbo.ItemSubCategory AS ITS ON IT.CateCode = ITS.CateCode AND IT.ProdSubCatCode = ITS.ProdSubCatCode";
                    SqlCommand cmd10 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                    ds = new DataSet();
                    da10.Fill(ds, "InternalRequisition2");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLInternalRequisition2.xsd"));
                    con.Close();
                    return ds;


                case "IssuStock2":
                    sqlstr = @"SELECT     VI.ComCode, VI.IssVouchNo, VI.IssDate, VI.VehicleID, VI.ModelNo, VI.IssuStatus, VI.EmpCode AS DriverID,p.EmpName as TechnicianName,VD.Comment, VI.Remarks, VI.FromDate, VI.ToDate, 
                           VI.Entry_Date, VI.User_Code, ITS.ProdSubCatName, IT.ProdSubCatCode, VD.ProductCode, VD.IssQty, VD.PurPrice, IC.CateName, IT.ProductName,IT.ProductBName,VI.TechnitianID,
                           IT.UnitType, PR.EmpName AS DriverName,  VE.VehicleNo, CI.cCompname, CI.cAdd1, CI.cAdd2, CI.cAdd3
                      FROM  dbo.VMFIssue AS VI INNER JOIN
						    dbo.VMFIssueDetail AS VD ON VI.IssVouchNo = VD.IssVouchNo AND VI.ComCode = VD.ComCode INNER JOIN
						    dbo.Item AS IT ON VD.ProductCode = IT.ProductCode AND VD.StoreCode=IT.StoreCode INNER JOIN
						    dbo.Item_Category AS IC ON VI.CateCode = IC.CateCode INNER JOIN
						    dbo.Personal AS PR ON VI.EmpCode = PR.EmpCode INNER JOIN
						    dbo.VehicleInfo AS VE ON VI.VehicleID = VE.VehicleID INNER JOIN
						    dbo.Personal AS p ON VI.TechnitianID = p.EmpCode INNER JOIN
						    dbo.CompInfo AS CI ON VI.ComCode = CI.ComCode INNER JOIN
						    dbo.ItemSubCategory AS ITS ON VI.CateCode = ITS.CateCode AND VI.ProdSubCatCode = ITS.ProdSubCatCode
                            where VI.IssVouchNo='" + serialrpt + "'and VI.CateCode='" + department + "'";
                    SqlCommand cmd11 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                    ds = new DataSet();
                    da11.Fill(ds, "StockIssue");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLStockIssue.xsd"));
                    con.Close();
                    return ds;

                case "RequisitionStatement2":
                    sqlstr = @"select PR.ComCode,PR.ReqNo,PR.CateCode,PR.ProdSubCatCode,PR.ReqDate,VPR.ToTalRecQty,VPR.TotRecAmount,IC.CateName,ITC.ProdSubCatName,C.cCompname,C.cAdd1,C.cAdd2,C.cAdd3 from PurRequisition PR inner join vrPurRequisition VPR on PR.ReqNo=VPR.ReqNo and PR.ComCode=VPR.ComCode
                            inner join Item_Category IC on PR.CateCode=IC.CateCode
                            inner join ItemSubCategory ITC on PR.CateCode=ITC.CateCode and PR.ProdSubCatCode=ITC.ProdSubCatCode
                            inner join CompInfo C on PR.ComCode=C.ComCode
                            where PR.ReqDate between CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd12 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
                    ds = new DataSet();
                    da12.Fill(ds, "RequisitionStatement2");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLRequisitionStatement2.xsd"));
                    con.Close();
                    return ds;

                case "RequisitionDepartmentWise":
                    sqlstr = @"select PR.ComCode,PR.ReqNo,PR.CateCode,PR.ProdSubCatCode,PR.ReqDate,VPR.ToTalRecQty,VPR.TotRecAmount,IC.CateName,ITC.ProdSubCatName,C.cCompname,C.cAdd1,C.cAdd2,C.cAdd3 from PurRequisition PR inner join vrPurRequisition VPR on PR.ReqNo=VPR.ReqNo and PR.ComCode=VPR.ComCode
                            inner join Item_Category IC on PR.CateCode=IC.CateCode
                            inner join ItemSubCategory ITC on PR.CateCode=ITC.CateCode and PR.ProdSubCatCode=ITC.ProdSubCatCode
                            inner join CompInfo C on PR.ComCode=C.ComCode
                            where PR.ReqDate between CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd13 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da13 = new SqlDataAdapter(cmd13);
                    ds = new DataSet();
                    da13.Fill(ds, "RequisitionStatement2");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLRequisitionStatement2.xsd"));
                    con.Close();
                    return ds;

                case "RequisitionSubDepartmentWise":
                    sqlstr = @"select PR.ComCode,PR.ReqNo,PR.CateCode,PR.ProdSubCatCode,PR.ReqDate,VPR.ToTalRecQty,VPR.TotRecAmount,IC.CateName,ITC.ProdSubCatName,C.cCompname,C.cAdd1,C.cAdd2,C.cAdd3 from PurRequisition PR inner join vrPurRequisition VPR on PR.ReqNo=VPR.ReqNo and PR.ComCode=VPR.ComCode
                            inner join Item_Category IC on PR.CateCode=IC.CateCode
                            inner join ItemSubCategory ITC on PR.CateCode=ITC.CateCode and PR.ProdSubCatCode=ITC.ProdSubCatCode
                            inner join CompInfo C on PR.ComCode=C.ComCode
                            where PR.ReqDate between CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd14 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da14 = new SqlDataAdapter(cmd14);
                    ds = new DataSet();
                    da14.Fill(ds, "RequisitionStatement2");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLRequisitionStatement2.xsd"));
                    con.Close();
                    return ds;

                case "PurchaseOrderStatement":
                    sqlstr = @"select PO.ComCode,PO.PurOrderNo,PO.OrderDate,PO.PINo,PO.PIIssDate,PO.OrdStatus,PO.SuplierID,PO.TotOrderQty,VPO.TQty,VPO.TAmount,s.SupplierID,s.SupplierName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3 
                        from PurOrder PO inner join vrPurOrderQty VPO on PO.ComCode=VPO.ComCode and PO.PurOrderNo=VPO.PurOrderNo
                           inner join Supplier s on PO.SuplierID=s.SupplierID and PO.ComCode=s.ComCode
                           inner join CompInfo CI on PO.ComCode=CI.ComCode
                           where PO.OrderDate between  CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd15 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da15 = new SqlDataAdapter(cmd15);
                    ds = new DataSet();
                    da15.Fill(ds, "PurchaseOrderStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//PurchaseOrderStatement.xsd"));
                    con.Close();
                    return ds;


                case "PurchaseOrderSupplierWise":
                    sqlstr = @"select PO.ComCode,PO.PurOrderNo,PO.OrderDate,PO.PINo,PO.PIIssDate,PO.OrdStatus,PO.SuplierID,PO.TotOrderQty,VPO.TQty,VPO.TAmount,s.SupplierID,s.SupplierName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3 
                        from PurOrder PO inner join vrPurOrderQty VPO on PO.ComCode=VPO.ComCode and PO.PurOrderNo=VPO.PurOrderNo
                           inner join Supplier s on PO.SuplierID=s.SupplierID and PO.ComCode=s.ComCode
                           inner join CompInfo CI on PO.ComCode=CI.ComCode
                           where PO.OrderDate between  CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd16 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da16 = new SqlDataAdapter(cmd16);
                    ds = new DataSet();
                    da16.Fill(ds, "PurchaseOrderStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//PurchaseOrderStatement.xsd"));
                    con.Close();
                    return ds;

                case "PurchaseReceiveStatement":
                    sqlstr = @"select R.ComCode,R.PurRecNo,R.RecDate,R.PurOrderNo,PR.OrderDate ,R.InvNo,CAST((R.Discount) AS DECIMAL(18,2)) AS Discount,CAST((R.Deduction) AS DECIMAL(18,2)) AS Deduction ,R.InvDate,VR.ToTalRecQty,VR.TotRecAmount,PR.SuplierID,S.SupplierName,C.cCompname,C.cAdd1,C.cAdd2,C.cAdd3 
                        from PurRec R inner join vrPurRecQty VR on R.PurRecNo=VR.PurRecNo And R.ComCode=VR.ComCode
                           inner join PurOrder PR on R.PurOrderNo=PR.PurOrderNo and R.ComCode=PR.ComCode 
                           inner join CompInfo C on R.ComCode=C.ComCode
                           inner join Supplier S on PR.SuplierID=S.SupplierID
                           where R.RecDate between CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd17 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da17 = new SqlDataAdapter(cmd17);
                    ds = new DataSet();
                    da17.Fill(ds, "PurchaseReceiveStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLPurchaseReceiveStatement.xsd"));
                    con.Close();
                    return ds;

                case "PurchaseReceivePOWise":
                    sqlstr = @"select R.ComCode,R.PurRecNo,R.RecDate,R.PurOrderNo,PR.OrderDate ,R.InvNo,CAST((R.Discount) AS DECIMAL(18,2)) AS Discount,CAST((R.Deduction) AS DECIMAL(18,2)) AS Deduction ,R.InvDate,VR.ToTalRecQty,VR.TotRecAmount,PR.SuplierID,S.SupplierName,C.cCompname,C.cAdd1,C.cAdd2,C.cAdd3 
                        from PurRec R inner join vrPurRecQty VR on R.PurRecNo=VR.PurRecNo And R.ComCode=VR.ComCode
                           inner join PurOrder PR on R.PurOrderNo=PR.PurOrderNo and R.ComCode=PR.ComCode 
                           inner join CompInfo C on R.ComCode=C.ComCode
                           inner join Supplier S on PR.SuplierID=S.SupplierID
                           where R.RecDate between CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd18 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da18 = new SqlDataAdapter(cmd18);
                    ds = new DataSet();
                    da18.Fill(ds, "PurchaseReceiveStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLPurchaseReceiveStatement.xsd"));
                    con.Close();
                    return ds;

                case "PurchaseReceiveSupplierWise":
                    sqlstr = @"select R.ComCode,R.PurRecNo,R.RecDate,R.PurOrderNo,PR.OrderDate ,R.InvNo,CAST((R.Discount) AS DECIMAL(18,2)) AS Discount,CAST((R.Deduction) AS DECIMAL(18,2)) AS Deduction ,R.InvDate,VR.ToTalRecQty,VR.TotRecAmount,PR.SuplierID,S.SupplierName,C.cCompname,C.cAdd1,C.cAdd2,C.cAdd3 
                        from PurRec R inner join vrPurRecQty VR on R.PurRecNo=VR.PurRecNo And R.ComCode=VR.ComCode
                           inner join PurOrder PR on R.PurOrderNo=PR.PurOrderNo and R.ComCode=PR.ComCode 
                           inner join CompInfo C on R.ComCode=C.ComCode
                           inner join Supplier S on PR.SuplierID=S.SupplierID
                           where R.RecDate between CONVERT(smalldatetime,'" + FromDate + "',103)AND CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd19 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da19 = new SqlDataAdapter(cmd19);
                    ds = new DataSet();
                    da19.Fill(ds, "PurchaseReceiveStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLPurchaseReceiveStatement.xsd"));
                    con.Close();
                    return ds;

                case "AdministrativWorkPaper":
                    sqlstr = @"select AW.AdministrativNo, AW.IssDate,AW.AttendDate, AW.Remarks,AW.ModelNo,AWD.ItemID,AWD.ExpDate,CAST((AWD.Amount) AS DECIMAL(18,2)) AS Amount,AWD.Comments,VI.VehicleNo,ci.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,AW.CateID,AW.EmpCode,AIC.CateName,ADI.ItemName,ADI.Dhara
						from AdministrativWork AW inner join AdministrativWorkDTL AWD on AW.AdministrativNo=AWD.AdministrativNo and AW.ComCode=AWD.ComCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join CompInfo CI on AW.ComCode=CI.ComCode
                           inner join AdminItemCate AIC on AW.CateID=AIC.CateID
                           inner join AdminItem ADI on AWD.ItemID=ADI.ItemID
                           where aw.AdministrativNo='" + serialrpt + "'";
                    SqlCommand cmd20 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da20 = new SqlDataAdapter(cmd20);
                    ds = new DataSet();
                    da20.Fill(ds, "AdministrativWorkPaper");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLAdministrativWorkPaper.xsd"));
                    con.Close();
                    return ds;

                case "AdministrativWorkCase":
                    sqlstr = @"select AW.AdministrativNo, AW.IssDate,AW.AttendDate, AW.Remarks,AW.ModelNo,AWD.ItemID,AWD.ExpDate,CAST((AWD.Amount) AS DECIMAL(18,2)) AS Amount,AWD.Comments,PR.EmpName,VI.VehicleNo,ci.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,AW.CateID,AW.EmpCode,AIC.CateName,ADI.ItemName,ADI.Dhara,AW.SergeantName,AW.Area,AW.Discount
						from AdministrativWork AW inner join AdministrativWorkDTL AWD on AW.AdministrativNo=AWD.AdministrativNo and AW.ComCode=AWD.ComCode
                           inner join Personal PR on AW.EmpCode=PR.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join CompInfo CI on AW.ComCode=CI.ComCode
                           inner join AdminItemCate AIC on AW.CateID=AIC.CateID
                           inner join AdminItem ADI on AWD.ItemID=ADI.ItemID
                           where aw.AdministrativNo='" + serialrpt + "'";
                    SqlCommand cmd21 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da21 = new SqlDataAdapter(cmd21);
                    ds = new DataSet();
                    da21.Fill(ds, "AdministrativWork");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLAdministrativWork.xsd"));
                    con.Close();
                    return ds;

                case "AdministrativCaseStatement":
                    sqlstr = @"select distinct(AW.AdministrativNo),AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,AW.Area,AW.Discount,AWD.ExpDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join AdministrativWorkDTL AWD on AW.AdministrativNo=AWD.AdministrativNo
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.AdministrativNo)asc";
                    SqlCommand cmd22 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da22 = new SqlDataAdapter(cmd22);
                    ds = new DataSet();
                    da22.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;

                case "AdministrativPaperStatement":
                    sqlstr = @"select AW.AdministrativNo,AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.IssDate)asc";
                    SqlCommand cmd23 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da23 = new SqlDataAdapter(cmd23);
                    ds = new DataSet();
                    da23.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;

                case "AdministrativAccidentalStatement":
                    sqlstr = @"select distinct(AW.AdministrativNo),AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,AW.Area,AW.Discount,AWD.ExpDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join AdministrativWorkDTL AWD on AW.AdministrativNo=AWD.AdministrativNo
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.IssDate)asc";
                    SqlCommand cmd24 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da24 = new SqlDataAdapter(cmd24);
                    ds = new DataSet();
                    da24.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;

                case "AccidentalStatementVehicleWise":
                    sqlstr = @"select AW.AdministrativNo,AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.IssDate)asc";
                    SqlCommand cmd25 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da25 = new SqlDataAdapter(cmd25);
                    ds = new DataSet();
                    da25.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;

                case "AccidentalStatementDriverWise":
                    sqlstr = @"select AW.AdministrativNo,AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.IssDate)asc";
                    SqlCommand cmd26 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da26 = new SqlDataAdapter(cmd26);
                    ds = new DataSet();
                    da26.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;


                case "CaseStatementVehicleWise":
                    sqlstr = @"select AW.AdministrativNo,AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.IssDate)asc";
                    SqlCommand cmd27 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da27 = new SqlDataAdapter(cmd27);
                    ds = new DataSet();
                    da27.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;

                case "CaseStatementDriverWise":
                    sqlstr = @"select AW.AdministrativNo,AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.IssDate)asc";
                    SqlCommand cmd28 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da28 = new SqlDataAdapter(cmd28);
                    ds = new DataSet();
                    da28.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;

                case "PaperStatementVehicleWise":
                    sqlstr = @"select AW.AdministrativNo,AW.IssDate,AW.EmpCode,AW.CateID,AW.ModelNo,AW.VehicleID,AW.AttendDate,P.EmpName,VI.VehicleNo,VAW.TotAmount,P.EmpName,VI.VehicleNo,CIF.cCompname,CIF.cAdd1,CIF.cAdd2,CIF.cAdd3 from AdministrativWork AW inner join AdminItemCate AITC on AW.CateID=AITC.CateID
                           inner join Personal P on AW.ComCode=p.ComCode and AW.EmpCode=P.EmpCode
                           inner join VehicleInfo VI on AW.VehicleID=VI.VehicleID
                           inner join vrAdministrativWorkAMT VAW on AW.ComCode=VAW.ComCode and AW.AdministrativNo=VAW.AdministrativNo
                           inner join CompInfo CIF on AW.ComCode=CIF.ComCode 
                           where AW.CateID='" + CateID + "' and AW.IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)order by (AW.IssDate)asc";
                    SqlCommand cmd29 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da29 = new SqlDataAdapter(cmd29);
                    ds = new DataSet();
                    da29.Fill(ds, "AdministrativCaseStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//AdministrativCaseStatement.xsd"));
                    con.Close();
                    return ds;


                case "VMFStatementDatail":
                    sqlstr = @"SELECT     VI.ComCode, VI.IssVouchNo, VI.IssDate, VI.VehicleID, VI.ModelNo, VI.IssuStatus, VI.EmpCode AS DriverID,p.EmpName as TechnicianName,VD.Comment, VI.Remarks, VI.FromDate, VI.ToDate,VI.ServiceStatus,
                           VI.Entry_Date, VI.User_Code, ITS.ProdSubCatName, VI.ProdSubCatCode, VD.ProductCode, VD.IssQty, VD.PurPrice, IC.CateName, IT.ProductName,IT.ProductBName,VI.TechnitianID,VD.StoreCode,VI.CateCode,
                           IT.UnitType, PR.EmpName AS DriverName,  VE.VehicleNo, CI.cCompname, CI.cAdd1, CI.cAdd2, CI.cAdd3
                      FROM  dbo.VMFIssue AS VI INNER JOIN
						    dbo.VMFIssueDetail AS VD ON VI.IssVouchNo = VD.IssVouchNo AND VI.ComCode = VD.ComCode INNER JOIN
						    dbo.Item AS IT ON VD.ProductCode = IT.ProductCode AND VD.StoreCode=IT.StoreCode INNER JOIN
						    dbo.Item_Category AS IC ON VI.CateCode = IC.CateCode INNER JOIN
						    dbo.Personal AS PR ON VI.EmpCode = PR.EmpCode INNER JOIN
						    dbo.VehicleInfo AS VE ON VI.VehicleID = VE.VehicleID INNER JOIN
						    dbo.Personal AS p ON VI.TechnitianID = p.EmpCode INNER JOIN
						    dbo.CompInfo AS CI ON VI.ComCode = CI.ComCode INNER JOIN
						    dbo.ItemSubCategory AS ITS ON VI.CateCode = ITS.CateCode AND VI.ProdSubCatCode = ITS.ProdSubCatCode
						    where VI.IssuStatus>=2 and IssDate between CONVERT(smalldatetime,'" + FromDate + "',103) and CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd30 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da30 = new SqlDataAdapter(cmd30);
                    ds = new DataSet();
                    da30.Fill(ds, "VMFStatementDatail");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLVMFStatementDatail.xsd"));
                    con.Close();
                    return ds;



                case "VMFStatementSummery":
                    sqlstr = @"select VI.IssVouchNo,VI.IssDate,VI.VehicleID,VI.CateCode,VI.ModelNo,VI.FromDate,VI.ToDate,VI.ProdSubCatCode,VI.Remarks,VI.EmpCode,VI.TechnitianID,VI.IssuStatus,VI.ServiceStatus,VVI.ToTalIssQty,VVI.TotIssAmount,VIF.VehicleNo,IC.CateName,IST.ProdSubCatName,P.EmpName as DriverName,PR.EmpName as TechnicianName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3
                            from VMFIssue VI inner join vrVMFQty VVI on VI.ComCode=VVI.ComCode and VI.IssVouchNo=VVI.IssVouchNo
                            inner join VehicleInfo VIF on VI.VehicleID=VIF.VehicleID 
                            inner join Personal P on VI.EmpCode=P.EmpCode
                            inner join Personal PR on VI.TechnitianID=PR.EmpCode
                            inner join Item_Category IC on VI.CateCode=IC.CateCode
                            inner join ItemSubCategory IST on VI.ProdSubCatCode=IST.ProdSubCatCode and VI.CateCode=IST.CateCode
                            inner join CompInfo CI on VI.ComCode=CI.ComCode
                            where IssuStatus>=3 and IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd31 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da31 = new SqlDataAdapter(cmd31);
                    ds = new DataSet();
                    da31.Fill(ds, "VMFStatementSummery");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLVMFStatementSummery.xsd"));
                    con.Close();
                    return ds;


                case "VMFStatementVehicleWise":
                    sqlstr = @"select VI.IssVouchNo,VI.IssDate,VI.VehicleID,VI.CateCode,VI.ModelNo,VI.FromDate,VI.ToDate,VI.ProdSubCatCode,VI.Remarks,VI.EmpCode,VI.TechnitianID,VI.IssuStatus,VI.ServiceStatus,VVI.ToTalIssQty,VVI.TotIssAmount,VIF.VehicleNo,IC.CateName,IST.ProdSubCatName,P.EmpName as DriverName,PR.EmpName as TechnicianName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3
                            from VMFIssue VI inner join vrVMFQty VVI on VI.ComCode=VVI.ComCode and VI.IssVouchNo=VVI.IssVouchNo
                            inner join VehicleInfo VIF on VI.VehicleID=VIF.VehicleID 
                            inner join Personal P on VI.EmpCode=P.EmpCode
                            inner join Personal PR on VI.TechnitianID=PR.EmpCode
                            inner join Item_Category IC on VI.CateCode=IC.CateCode
                            inner join ItemSubCategory IST on VI.ProdSubCatCode=IST.ProdSubCatCode and VI.CateCode=IST.CateCode
                            inner join CompInfo CI on VI.ComCode=CI.ComCode
                            where IssuStatus>=3 and IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd32 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da32 = new SqlDataAdapter(cmd32);
                    ds = new DataSet();
                    da32.Fill(ds, "VMFStatementSummery");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLVMFStatementSummery.xsd"));
                    con.Close();
                    return ds;


                case "VMFStatementSubdeptWise":
                    sqlstr = @"select VI.IssVouchNo,VI.IssDate,VI.VehicleID,VI.CateCode,VI.ModelNo,VI.FromDate,VI.ToDate,VI.ProdSubCatCode,VI.Remarks,VI.EmpCode,VI.TechnitianID,VI.IssuStatus,VI.ServiceStatus,VVI.ToTalIssQty,VVI.TotIssAmount,VIF.VehicleNo,IC.CateName,IST.ProdSubCatName,P.EmpName as DriverName,PR.EmpName as TechnicianName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3
                            from VMFIssue VI inner join vrVMFQty VVI on VI.ComCode=VVI.ComCode and VI.IssVouchNo=VVI.IssVouchNo
                            inner join VehicleInfo VIF on VI.VehicleID=VIF.VehicleID 
                            inner join Personal P on VI.EmpCode=P.EmpCode
                            inner join Personal PR on VI.TechnitianID=PR.EmpCode
                            inner join Item_Category IC on VI.CateCode=IC.CateCode
                            inner join ItemSubCategory IST on VI.ProdSubCatCode=IST.ProdSubCatCode and VI.CateCode=IST.CateCode
                            inner join CompInfo CI on VI.ComCode=CI.ComCode
                            where IssuStatus>=3 and IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd33 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da33 = new SqlDataAdapter(cmd33);
                    ds = new DataSet();
                    da33.Fill(ds, "VMFStatementSummery");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLVMFStatementSummery.xsd"));
                    con.Close();
                    return ds;



                case "InternalREQStatementDeptWise":
                    sqlstr = @"select SI.IssVouchNo,SI.InternalReqNo,SI.IssDate,SI.VehicleID,SI.CateCode,SI.ModelNo,SI.FromDate,SI.ToDate,SI.ProdSubCatCode,SI.Remarks,SI.EmpCode,SI.TechnitianID,SI.IssuStatus,SI.ServiceStatus,VSI.ToTalIssQty,VSI.TotIssAmount,IC.CateName,IST.ProdSubCatName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3
                            from StockIssue SI inner join vrStockIssueQty VSI on SI.ComCode=VSI.ComCode and SI.InternalReqNo=VSI.InternalReqNo
                            inner join Item_Category IC on SI.CateCode=IC.CateCode
                            inner join ItemSubCategory IST on SI.ProdSubCatCode=IST.ProdSubCatCode and SI.CateCode=IST.CateCode
                            inner join CompInfo CI on SI.ComCode=CI.ComCode
                            where SI.IssuStatus>=3 and IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd34 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da34 = new SqlDataAdapter(cmd34);
                    ds = new DataSet();
                    da34.Fill(ds, "InternalREQStatementDeptWise");
                    //.WriteXmlSchema(Server.MapPath("~//XML//XMLInternalREQStatementDeptWise.xsd"));
                    con.Close();
                    return ds;


                case "InternalREQStatementSubDeptWise":
                    sqlstr = @"select SI.IssVouchNo,SI.InternalReqNo,SI.IssDate,SI.VehicleID,SI.CateCode,SI.ModelNo,SI.FromDate,SI.ToDate,SI.ProdSubCatCode,SI.Remarks,SI.EmpCode,SI.TechnitianID,SI.IssuStatus,SI.ServiceStatus,VSI.ToTalIssQty,VSI.TotIssAmount,IC.CateName,IST.ProdSubCatName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3
                            from StockIssue SI inner join vrStockIssueQty VSI on SI.ComCode=VSI.ComCode and SI.InternalReqNo=VSI.InternalReqNo
                            inner join Item_Category IC on SI.CateCode=IC.CateCode
                            inner join ItemSubCategory IST on SI.ProdSubCatCode=IST.ProdSubCatCode and SI.CateCode=IST.CateCode
                            inner join CompInfo CI on SI.ComCode=CI.ComCode
                            where SI.IssuStatus>=3 and IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd35 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da35 = new SqlDataAdapter(cmd35);
                    ds = new DataSet();
                    da35.Fill(ds, "InternalREQStatementDeptWise");
                    //.WriteXmlSchema(Server.MapPath("~//XML//XMLInternalREQStatementDeptWise.xsd"));
                    con.Close();
                    return ds;


                case "BillDeptWise":
                    sqlstr = @"select SI.IssVouchNo,SI.InternalReqNo,SI.IssDate,SI.VehicleID,SI.CateCode,SI.ModelNo,SI.FromDate,SI.ToDate,SI.ProdSubCatCode,SI.Remarks,SI.EmpCode,SI.TechnitianID,SI.IssuStatus,SI.ServiceStatus,VSI.ToTalIssQty,VSI.TotIssAmount,IC.CateName,IST.ProdSubCatName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3
                            from StockIssue SI inner join vrStockIssueQty VSI on SI.ComCode=VSI.ComCode and SI.InternalReqNo=VSI.InternalReqNo
                            inner join Item_Category IC on SI.CateCode=IC.CateCode
                            inner join ItemSubCategory IST on SI.ProdSubCatCode=IST.ProdSubCatCode and SI.CateCode=IST.CateCode
                            inner join CompInfo CI on SI.ComCode=CI.ComCode
                            where SI.IssuStatus>=3 and IssDate between CONVERT(smalldatetime,'" + FromDate + "',103)and CONVERT(smalldatetime,'" + ToDate + "',103)";
                    SqlCommand cmd36 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da36 = new SqlDataAdapter(cmd36);
                    ds = new DataSet();
                    da36.Fill(ds, "InternalREQStatementDeptWise");
                    //.WriteXmlSchema(Server.MapPath("~//XML//XMLInternalREQStatementDeptWise.xsd"));
                    con.Close();
                    return ds;

                case "ReturnOrder":
                    sqlstr = @"select RO.ReturnNo,RO.RetDate,RO.InternalReqNo,RO.RetAmount,RO.User_Code,RO.RetRemarks,RO.CateCode,RO.ProdSubCatCode,Rop.ProductCode,Rop.StoreCode,Rop.RetQty,Rop.UnitPrice,Rop.Comment,IT.ProductName,IT.ProductBName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,CI.cPhone,CI.cFax,IC.CateName,IST.ProdSubCatName from ReturnOrd RO inner join ReturnProd Rop on RO.ComCode=Rop.ComCode and RO.ReturnNo=Rop.ReturnNo
                            inner join  Item IT on Rop.ProductCode=IT.ProductCode and Rop.StoreCode=IT.StoreCode
                            inner join  CompInfo CI on RO.ComCode=CI.ComCode
                            inner join Item_Category IC on RO.CateCode=IC.CateCode
                            inner join ItemSubCategory IST on RO.ProdSubCatCode=IST.ProdSubCatCode and RO.CateCode=IST.CateCode";
                    SqlCommand cmd37 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da37 = new SqlDataAdapter(cmd37);
                    ds = new DataSet();
                    da37.Fill(ds, "ReturnOrder");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//ReturnOrder.xsd"));
                    con.Close();
                    return ds;

                case "ReturnOrderStatement":
                    sqlstr = @"select RO.ReturnNo,RO.RetDate,RO.InternalReqNo,RO.RetAmount,RO.RetRemarks,VRQ.TotalQty,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,CI.cPhone,CI.cFax
                            from ReturnOrd RO inner join ReturnProd ROP on RO.ReturnNo=ROP.ReturnNo and RO.ComCode=ROP.ComCode
                            inner join varRetuQty VRQ on RO.ReturnNo=VRQ.ReturnNo and RO.ComCode=VRQ.ComCode
                            inner join CompInfo CI on RO.ComCode=CI.ComCode";
                    SqlCommand cmd38 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da38 = new SqlDataAdapter(cmd38);
                    ds = new DataSet();
                    da38.Fill(ds, "ReturnOrderStatement");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//ReturnOrderStatement.xsd"));
                    con.Close();
                    return ds;



                case "DailyStock":
                    sqlstr = @"select DS.ComCode,DS.Productcode,DS.StoreCode,DS.CloseDate,DS.OpenPhy,DS.OpenStock,DS.ProcFlag,DS.RecQty,DS.SalesQty,DS.RetQty,P.ProductName,PC.CateName,CI.cCompname,CI.cAdd1,CI.cAdd2,CI.cAdd3,CI.cPhone,CI.cFax,P.ProductDiscription,PS.ProdSubCatCode,PC.CateCode
                                from DailyStock DS inner join Item P on DS.Productcode=P.ProductCode and DS.StoreCode=P.StoreCode
                                inner join ItemSubCategory PS on P.ProdSubCatCode=PS.ProdSubCatCode and P.CateCode=PS.CateCode
                                inner join Item_Category PC on PS.CateCode=PC.CateCode
                                inner join CompInfo CI on DS.ComCode=CI.ComCode";

                    SqlCommand cmd39 = new SqlCommand(sqlstr, con);
                    SqlDataAdapter da39 = new SqlDataAdapter(cmd39);
                    ds = new DataSet();
                    da39.Fill(ds, "DailyStock");
                    //ds.WriteXmlSchema(Server.MapPath("~//XML//XMLDailyStock.xsd"));
                    con.Close();
                    return ds;

            }

            return ds;

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            ReportName = Request.QueryString["RN"].ToString();
            this.ReportGenerate();



        }
        private void ReportGenerate()
        {
            switch (ReportName)
            {
                case "PurchaseReceive":
                    //FromDate = "12-09-2014";
                    //ToDate = "13-09-2014";
                    serialrpt = Request.QueryString["RID"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurchaseRec.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());


                    break;
                case "PurchaseOrder":
                    serialrpt = Request.QueryString["RID"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurchaseOrder.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                  
                   
                   
                    break;
                case "IssuStock":
                    serialrpt = Request.QueryString["RID"].ToString();
                    department = Request.QueryString["Dpt"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//StockIssu.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                  
                   
                   
                   
                    
                    break;

                case "InternalRequisition":
                    serialrpt = Request.QueryString["RID"].ToString();
                    department = Request.QueryString["Dpt"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//InternalRequisition.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("dept", department);
                    salesrpt.SetParameterValue("RID", serialrpt);
                  
                   
                   
                   
                    
                    //try
                    //{
                    //    salesrpt.PrintOptions.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[0].ToString();
                    //    salesrpt.PrintToPrinter(1, false, 0, 0);
                    //}
                    //catch (Exception ex)
                    //{
                    //    lblErrorMsg.Text = ex.Message;
                    //}


                    //DataSet SetDataSouece = new DataSet();

                    //    //dsReportSource.WriteXmlSchema(MapPath("XMLFiles\\Reports\\Barcodegenerate.xsd"));
                    //StockIssu StockIssurpt = new StockIssu();
                    //StockIssu.SetDataSouece(ds.Tables[0].);
                    //    StockIssurpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "StockIssurp");












                    break;



                case "Requisition":
                    serialrpt = Request.QueryString["RID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurRequisition.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());

                  
                   
                   
                   
                    
                    //try
                    //{
                    //    salesrpt.PrintOptions.PrinterName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[0].ToString();
                    //    salesrpt.PrintToPrinter(1, false, 0, 0);
                    //}
                    //catch (Exception ex)
                    //{
                    //    lblErrorMsg.Text = ex.Message;
                    //}


                    //DataSet SetDataSouece = new DataSet();

                    //    //dsReportSource.WriteXmlSchema(MapPath("XMLFiles\\Reports\\Barcodegenerate.xsd"));
                    //StockIssu StockIssurpt = new StockIssu();
                    //StockIssu.SetDataSouece(ds.Tables[0].);
                    //    StockIssurpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "StockIssurp");


                    break;

                case "AdministrativWork":
                    serialrpt = Request.QueryString["RID"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AdministrativWork.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                                
                   
                   
                    












                    break;


                case "RequisitionStatement":
                    //string dept = "01";
                    String Fdate = Request.QueryString["FromDate"].ToString();
                    String Tdate = Request.QueryString["ToDate"].ToString();
                    FromDate = Fdate.ToString().Trim();
                    ToDate = Tdate.ToString().Trim();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//RequisitionStatement.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    //salesrpt.SetParameterValue("dept",dept);
                  
                   
                   
                   
                    








                    break;

                case "SupplierStutas":


                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//SupplierStutas.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                
                   
                   
                   
                    








                    break;

                case "Bill":
                    department = Request.QueryString["Dept"].ToString();
                    string Fstdate = Request.QueryString["FromDate"].ToString();
                    string Toodate = Request.QueryString["ToDate"].ToString();
                    FromDate = Fstdate.ToString().Trim();
                    ToDate = Toodate.ToString().Trim();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//Bill.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("dept", department);
                 
                   
                   
                   
                    








                    break;

                case "InternalRequisition2":
                    serialrpt = Request.QueryString["RID"].ToString();
                    department = Request.QueryString["Dpt"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//InternalRequisition2.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("dept", department);
                    salesrpt.SetParameterValue("RID", serialrpt);
                  
                   
                   
                   
                    









                    break;

                case "Bill2":

                    department = Request.QueryString["Dept"].ToString();
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//InternalRequisition2.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    //salesrpt.SetParameterValue("Fdate",Convert.ToDateTime(FromDate));
                    //salesrpt.SetParameterValue("Tdate", Convert.ToDateTime(ToDate));
                    salesrpt.SetParameterValue("dept", department);
                    //CrystalReportViewer1.SelectionFormula = "Date({InternalRequisition2.IssDate})>=#" + FromDate + "# And ({InternalRequisition2.IssDate})<=#" + ToDate + "#";

                  
                   
                   
                   
                    










                    break;


                case "IssuStock2":
                    serialrpt = Request.QueryString["RID"].ToString();
                    department = Request.QueryString["Dpt"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//StockIssu2.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                  
                   
                   
                   
                    









                    break;

                case "RequisitionStatement2":
                    //string dept = "01";
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    //FromDate = Fdate.ToString().Trim();
                    //ToDate = Tdate.ToString().Trim();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//RequisitionStatement2.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());


                    //CrystalDecisions.Shared.ParameterRangeValue rangvalue = new CrystalDecisions.Shared.ParameterRangeValue();
                    //string Fdate1=string.Format("{dd-MMM-yyyy}", FromDate);
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //string todate1 = string.Format("{0:dd-MMM-yyyy}", ToDate);
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));




                  
                   
                   
                   
                    








                    break;

                case "RequisitionDepartmentWise":
                    //string dept = "01";
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    department = Request.QueryString["DeptId"].ToString();
                    //FromDate = Fdate.ToString().Trim();
                    //ToDate = Tdate.ToString().Trim();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//RequisitionDepartmentWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());

                    salesrpt.SetParameterValue("Dpt", department);
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                  
                   
                   
                   
                    








                    break;

                case "RequisitionSubDepartmentWise":

                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    department = Request.QueryString["DeptId"].ToString();
                    SubDepartment = Request.QueryString["SubDeptId"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//RequisitionSubDepartmentWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());

                    salesrpt.SetParameterValue("Dpt", department);
                    salesrpt.SetParameterValue("SubDpt", SubDepartment);
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                  
                   
                   
                   
                    








                    break;

                case "PurchaseOrderStatement":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurchaseOrderStatement.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                   
                   
                   
                   
                    







                    break;

                case "PurchaseOrderSupplierWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    SupplierID = Request.QueryString["SupplierId"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurchaseOrderSupplierWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Supplier", SupplierID);
                 
                   
                   
                   
                    







                    break;

                case "PurchaseReceiveStatement":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurchaseReceiveStatement.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));

                 
                   
                   
                   
                    







                    break;

                case "PurchaseReceivePOWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    PONo = Request.QueryString["PONo"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurchaseReceivePOWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("PoNo", PONo);

                  
                   
                   
                   
                    







                    break;

                case "PurchaseReceiveSupplierWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    SupplierID = Request.QueryString["SupplierID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PurchaseReceiveSupplierWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Supplierid", SupplierID);

                  
                   
                   
                   
                    



                    break;

                case "AdministrativWorkPaper":
                    serialrpt = Request.QueryString["RID"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AdministrativWorkPaper.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                 
                   
                   
                   
                    







                    break;

                case "AdministrativWorkCase":
                    serialrpt = Request.QueryString["RID"].ToString();
                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AdministrativWorkCase.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                   
                   
                   
                    







                    break;


                case "AdministrativCaseStatement":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AdministrativCaseStatement.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("Supplierid", SupplierID);

                   
                   
                   
                    







                    break;


                case "AdministrativPaperStatement":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AdministrativPaperStatement.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("Supplierid", SupplierID);

                  
                   
                   
                   
                    







                    break;

                case "AdministrativAccidentalStatement":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AdministrativAccidentalStatement.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("Supplierid", SupplierID);

                  
                   
                   
                   
                    







                    break;

                case "AccidentalStatementVehicleWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();
                    Vehicleid = Request.QueryString["VehicleID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AccidentalStatementVehicleWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Vehicle", Vehicleid);

                   
                   
                   
                   
                    







                    break;

                case "AccidentalStatementDriverWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();
                    driverid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//AccidentalStatementDriverWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Driver", driverid);

                   
                   
                   
                   
                    







                    break;


                case "CaseStatementDriverWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();
                    driverid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//CaseStatementDriverWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Driver", driverid);

                   
                   
                   
                   
                    







                    break;

                case "CaseStatementVehicleWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();
                    Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//CaseStatementVehicleWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Vehicle", Vehicleid);

                   
                   
                   
                   
                    







                    break;




                case "PaperStatementVehicleWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    CateID = Request.QueryString["CateID"].ToString();
                    Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//PaperStatementVehicleWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Vehicle", Vehicleid);

                   
                   
                   
                   
                    







                    break;

                case "VMFStatementDatail":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    //CateID = Request.QueryString["CateID"].ToString();
                    // Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//VMFStatementDatail.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("Vehicle", Vehicleid);

                  
                   
                   
                   
                    







                    break;


                case "VMFStatementSummery":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    //CateID = Request.QueryString["CateID"].ToString();
                    // Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//VMFStatementSummery.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("Vehicle", Vehicleid);

                   
                   
                   
                   
                    







                    break;


                case "VMFStatementVehicleWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    Vehicleid = Request.QueryString["VehicleID"].ToString();
                    //CateID = Request.QueryString["CateID"].ToString();
                    // Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//VMFStatementVehicleWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Vehicle", Vehicleid);

                   
                   
                   
                   
                    







                    break;

                case "VMFStatementSubdeptWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    SubDepartment = Request.QueryString["SubDeptNo"].ToString();
                    //CateID = Request.QueryString["CateID"].ToString();
                    // Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//VMFStatementSubdeptWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("Subdept", SubDepartment);

                  
                   
                   
                    







                    break;


                case "InternalREQStatementDeptWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    //SubDepartment = Request.QueryString["SubDeptNo"].ToString();
                    CateID = Request.QueryString["Dpt"].ToString();
                    // Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//InternalREQStatementDeptWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("dept", CateID);

                  
                   
                   
                    







                    break;

                case "InternalREQStatementSubDeptWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    SubDepartment = Request.QueryString["SubDept"].ToString();
                    CateID = Request.QueryString["Dpt"].ToString();
                    // Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//InternalREQStatementSubDeptWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("dept", CateID);
                    salesrpt.SetParameterValue("Subdept", SubDepartment);

                   
                   
                   
                   
                    







                    break;



                case "BillDeptWise":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();
                    //SubDepartment = Request.QueryString["SubDeptNo"].ToString();
                    CateID = Request.QueryString["Dept"].ToString();
                    // Vehicleid = Request.QueryString["DriverID"].ToString();

                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//BillDeptWise.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("dept", CateID);

                  
                   
                   
                   
                    







                    break;


                case "ReturnOrder":

                    serialrpt = Request.QueryString["RID"].ToString();


                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//ReturnOrder.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());

                    salesrpt.SetParameterValue("RetNo", serialrpt);

                   
                   
                   
                    







                    break;

                case "ReturnOrderStatement":
                    FromDate = Request.QueryString["FromDate"].ToString();
                    ToDate = Request.QueryString["ToDate"].ToString();


                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//ReturnOrderStatement.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("FromDate", DateTime.ParseExact(FromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("dept", CateID);

                  
                   
                   
                    







                    break;

                case "DailyStock":
                    CloseDate = Request.QueryString["ClosingDate"].ToString();
                    //ToDate = Request.QueryString["ToDate"].ToString();
                    //serialrpt = Request.QueryString["CustoID"].ToString();


                    salesrpt = new ReportDocument();
                    str = Server.MapPath("~//report//DailyStock.rpt");
                    salesrpt.Load(str);
                    salesrpt.SetDataSource(data());
                    salesrpt.SetParameterValue("CloseDate", DateTime.ParseExact(CloseDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("ToDate", DateTime.ParseExact(ToDate, "dd-MM-yyyy", CultureInfo.InvariantCulture));
                    //salesrpt.SetParameterValue("CustID", serialrpt);
                    //alesrpt.SetParameterValue("Subdept", SubDepartment);

                 
                   
                    







                    break;


            }
            byte[] byteArray = null;
            oStream = salesrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();


        }
    }
}