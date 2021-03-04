<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TripInfoOther.aspx.cs" Inherits="TransportManagerUI.UI.TripInfoOther" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "TripInfoOther.aspx/PageLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: Error
        });

    });

    //    function Error(request, status, error) {
    //        alert('Not Loggeed In');
    //        var url = "NotLoggedIn.htm";
    //        $(location).attr('href', url);

    //    }

  </script>
    <style type="text/css">
       
       
        .auto-style1 {
            text-align: left;
        }
       
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
   
    <asp:Panel ID="Panel3" runat="server">
         <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
         <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel7" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" formnovalidate   />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate   />
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click" />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel8" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
             <br />
      <asp:Panel ID="Panel8" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="1px" >
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" 
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     onselectedindexchanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                 <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="TripNo" HeaderText="TripNo" />
                <asp:BoundField DataField="TripDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="TripDate" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                <asp:BoundField DataField="CustName" HeaderText="DealerName" />
                
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
         <table>
            <tr>
            <td>
                <asp:Button ID="btnSearchOk" runat="server" Text="Ok" Width="60px" />
            </td>
            <td>
            <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
                  </ContentTemplate>
             <Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnSearch" />
           
            </Triggers>
    </asp:UpdatePanel>
    </asp:Panel>
    
     
        
    </asp:Panel>

         <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" ForeColor="Black" 
             BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Other Trip" 
             CssClass="entry-panel" BackColor="#D3DCBE">
        <table cellpadding="3px" cellspacing="1">
            <tr>
        <td align="right" class="style14">
        Trip No
        </td>
         <td align="left" class="style1">
             <asp:Label ID="lblTripNo" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
            <tr>
            <td align="right" class="style16">
            Trip Date
            </td>
             <td align="left" class="style1">
                 <asp:TextBox ID="txtTripDate" runat="server" Width="120px" ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="txtTripDate_CalendarExtender" 
                     runat="server" BehaviorID="txtTCDate_CalendarExtender" 
                     TargetControlID="txtTripDate" Format="dd/MMM/yyyy" />
            </td>
           
            </tr>
            <tr>
                <td align="right">
                 Ghat
                </td><td align="left" class="style1">
                <asp:DropDownList ID="ddlGhatList" runat="server" AutoPostBack="True" Height="24px" Width="173px">
             
                 </asp:DropDownList>
            </td>
            </tr>
            <tr>
             <td class="style15" align="right">
            Transport By
            </td>
            <td align="left" class="style1">
                <asp:DropDownList ID="ddlTransportBy" runat="server" AutoPostBack="true" 
                    Height="24px" Width="172px" 
                    onselectedindexchanged="ddlTransportBy_SelectedIndexChanged">
                <asp:ListItem Value="1" Text="Own"></asp:ListItem>
                <asp:ListItem Value="2" Text="Hired"></asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
             <tr>
             <td class="style15" align="right">
                 Transport Agent
            </td>
            <td align="left" class="style1">
                <asp:DropDownList ID="ddlAgent" runat="server" AutoPostBack="true" 
                    Height="24px" Width="169px">
              
                   
              
                </asp:DropDownList>
            </td>
            </tr>
            
            <tr>
            <td class="style15" align="right">
                Vehicle
            </td>
             <td align="left" class="style1">
           
             
                  <asp:ImageButton ID="btnVehicle" runat="server" ImageUrl="~/Images/1488717192_Search.png" 
                   CssClass="ImageButtonSytle" onclick="btnVehicle_Click" />
               
                   <asp:Label ID="lblCapacity" runat="server" Font-Bold="True" ForeColor="Blue" 
                      Height="20px"></asp:Label>&nbsp;
                   <ajaxToolkit:ModalPopupExtender ID="hfVehicleSearch_ModalPopupExtender" 
                      runat="server" BehaviorID="hfVehicleSearch_ModalPopupExtender" 
                      CancelControlID="btnVehicleCancel" DynamicServicePath="" 
                      OkControlID="btnVehicleOk" PopupControlID="plVehicleSearch" 
                      TargetControlID="hfVehicleSearch" BackgroundCssClass="modalBackground" >
                  </ajaxToolkit:ModalPopupExtender>
                        
        
                  <asp:Button ID="btnAddHiredVehicle" runat="server" Text="Add Hired Vehicle" 
                      onclick="btnAddHiredVehicle_Click" />
                        
        
            </td>
           
             
            </tr>
          
            <tr>
            <td align="right">
                Driver
            </td>
             <td align="left" class="style1">

             <asp:Label ID="lblDriverCode" runat="server" Font-Bold="True" 
                     CssClass="lblInformation1" required></asp:Label>*
             <asp:ImageButton ID="btnDriver" runat="server" ImageUrl="~/Images/1488717192_Search.png" 
                     CssClass="ImageButtonSytle" onclick="btnDriver_Click" />
                     <asp:Label ID="lblDriverName" runat="server" Font-Bold="True" 
                     CssClass="lblInformation2"></asp:Label> &nbsp
              <asp:Button ID="btnAddHDriver" runat="server"  Text="Add Hired Driver" OnClick="btnAddHDriver_Click" />
                 </td>
           
            </tr>
              <tr valign="middle">
            <td align="right" nowrap="nowrap">
                Dealer&nbsp;
            </td>
             <td align="left"  colspan="2"  nowrap="nowrap">
                     <asp:Label ID="lblDealerCode" runat="server" 
                     CssClass="lblInformation1" required  ></asp:Label>*


                 <asp:ImageButton ID="btnShowDealer" runat="server" CssClass="ImageButtonSytle" 
                     ImageUrl="~/Images/1488717192_Search.png" onclick="btnShowDealer_Click" />
                 <asp:Label ID="lblDealerName" runat="server" CssClass="lblInformation2"></asp:Label>
                
                    

                 <br />
            </td>
           
            </tr>
            <tr>
            <td align="right">Total KM
            </td>
            <td colspan="3" align="left" class="style1">
                <asp:TextBox ID="txtTotalKm" runat="server">0</asp:TextBox>
<asp:HiddenField ID="hfDriverSearch" runat="server" />
            </td>
            
            </tr>
            <tr>
            <td align="right">Remarks
            </td>
            <td colspan="3" align="left" class="style1">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            
            </tr>
            
            <tr>
                   <td>Status</td>
            <td align="left" >
             <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True">
                <asp:ListItem Value="0" Text="Open"></asp:ListItem>
                <asp:ListItem Value="1" Text="On Trip"></asp:ListItem>
                 <asp:ListItem Value="2" Text="Billed"></asp:ListItem>
               <asp:ListItem Value="3" Text="Cancel"></asp:ListItem>
                 
                 </asp:DropDownList>
                                   
            </td>
              </tr>
            <tr>

                <td>
                  </td>
                <td>
                    
                   
                  <asp:HiddenField ID="hfVehicleSearch" runat="server" />
                    <asp:HiddenField ID="hfTotalCapacity" runat="server" />

                  <ajaxToolkit:ModalPopupExtender ID="hfDriverSearch_ModalPopupExtender" 
                     runat="server" BehaviorID="hfDriverSearch_ModalPopupExtender" DynamicServicePath="" 
                     TargetControlID="hfDriverSearch" BackgroundCssClass="modalBackground" PopupControlID="plDriver" OkControlID="btnDriverOk" CancelControlID="btnDriverCancel">
                 </ajaxToolkit:ModalPopupExtender>
                    
                    <asp:HiddenField ID="hfDealer1" runat="server" />
                   
                    <ajaxToolkit:ModalPopupExtender ID="hfDealer1_ModalPopupExtender" 
                        runat="server" BehaviorID="hfDealer1_ModalPopupExtender" DynamicServicePath="" 
                        PopupControlID="holycow" TargetControlID="hfDealer1" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                   
                 <asp:HiddenField ID="hfInsertHVehicle" runat="server" />
                    

                    <asp:HiddenField ID="hfInsertDriver" runat="server" />
                    <ajaxToolkit:ModalPopupExtender ID="hfInsertDriver_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" BehaviorID="hfInsertDriver_ModalPopupExtender" DynamicServicePath="" PopupControlID="pnHDriver" TargetControlID="hfInsertDriver">
                    </ajaxToolkit:ModalPopupExtender>
                    

                    <ajaxToolkit:ModalPopupExtender ID="hfInsertHVehicle_ModalPopupExtender" 
                        runat="server" BehaviorID="hfInsertHVehicle_ModalPopupExtender" 
                        DynamicServicePath="" PopupControlID="plHiredVehicle" 
                        TargetControlID="hfInsertHVehicle" BackgroundCssClass="modalBackground">
                    </ajaxToolkit:ModalPopupExtender>
                    

                </td>
                
            </tr>
            
        </table>
            

        </asp:Panel>

    <asp:Panel ID="plVehicleSearch" runat="server" GroupingText="Select Vehicle" 
             BackColor="White" BorderWidth="2px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table style="width: 255px" >
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtVehicleSearch" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnVehicleSearch" runat="server" Text="Search" 
                                    CssClass="Button" onclick="btnVehicleSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvVehicleList" runat="server" AllowPaging="True" 
              CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                AutoGenerateColumns="False" 
                onselectedindexchanged="gvVehicleList_SelectedIndexChanged" 
                OnPageIndexChanging="gvVehicleList_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                <asp:BoundField DataField="VehicleType" HeaderText="VehicleType" />
                <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                <asp:BoundField DataField="KmPerLiter" HeaderText="KmPerLiter" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
            <table>
            <tr>
            <td>
                <asp:Button ID="btnVehicleOk" runat="server" Text="Ok" />
            </td>
            <td>
            <asp:Button ID="btnVehicleCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
        </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnVehicleSearch" />
           
            </Triggers>
        </asp:UpdatePanel>
            </asp:Panel>

    <asp:Panel ID="plDriver" runat="server" GroupingText="Select Driver" 
             BackColor="White" BorderWidth="2px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table style="width: 255px" >
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtSearchDriver" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnSearchDriver" runat="server" Text="Search" CssClass="Button" onclick="btnSearchDriver_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvDriverList" runat="server" AllowPaging="True" 
              CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                AutoGenerateColumns="False" 
                onpageindexchanging="gvDriverList_PageIndexChanging" 
                onselectedindexchanged="gvDriverList_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" />
                <asp:BoundField DataField="EmpName" HeaderText="EmpName" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
            <table>
            <tr>
            <td>
                <asp:Button ID="btnDriverOk" runat="server" Text="Ok" />
            </td>
            <td>
            <asp:Button ID="btnDriverCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
        </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSearchDriver" />
           
            </Triggers>
        </asp:UpdatePanel>
            </asp:Panel>

    <asp:Panel ID="holycow" runat="server" GroupingText="Search Dealer" BackColor="White" BorderWidth="2px">
         <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table style="width: 255px" >
                            <tr>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtDealerSearch" runat="server" Width="158px" />
                            </td>
                            <td class="auto-style4">
                                <asp:Button ID="btnDealerSearch" runat="server" Text="Search" CssClass="Button" onclick="btnDealerSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvDealerSearch" runat="server" AllowPaging="True" CellPadding="4" 
                ForeColor="#333333" GridLines="Horizontal" 
                OnPageIndexChanging="gvDealerSearch_PageIndexChanging" 
                OnSelectedIndexChanged="gvDealerSearch_SelectedIndexChanged" 
                AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="CustId" HeaderText="Id" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CustName" HeaderText="Name" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CustAddressBang" HeaderText="Address" >
                <ItemStyle HorizontalAlign="Left" Font-Names="SutonnyMJ" />
                </asp:BoundField>
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" >
               
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LocDistance" HeaderText="Distance" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
           <table>
            <tr>
            <td>
                <asp:Button ID="btnDealerOk" runat="server" Text="Ok" />
            </td>
            <td>
            <asp:Button ID="btnDealerCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
            </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnDealerSearch" />
   
           
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:Panel ID="plHiredVehicle" runat="server" ScrollBars="Horizontal" 
        ForeColor="Black" BorderStyle="Outset" BorderWidth="1px"
        GroupingText="Add Hired Vehicle" BackColor="White">
         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div style="background-position: left; text-align: left;">
             <asp:Button ID="btnHiredVehicle" runat="server" Text="Add" formnovalidate 
                 onclick="btnHiredVehicle_Click"  />
             <asp:Button ID="btnHiredVehicleCancel" runat="server" Text="Close" 
                 formnovalidate onclick="btnHiredVehicleCancel_Click"/>
             &nbsp; <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </div>
       
               <asp:Panel ID="Panel6" runat="server" GroupingText="" BackColor="#66CCFF">

                   <table cellpadding="3px">
                    <tr>
        <td align="right">
       Vehicle Type
        </td>
         <td colspan="3" align="left">
             <asp:DropDownList ID="ddlVehicleType" runat="server" Width="200px">
                 <asp:ListItem Text="Open Truck" Value="Open Truck"></asp:ListItem>
                 <asp:ListItem Text="Covered Van" Value="Covered Van"></asp:ListItem>
                 <asp:ListItem Text="Dump Truck" Value="Dump Truck"></asp:ListItem>
                 <asp:ListItem Text="Vessel" Value="Vessel"></asp:ListItem>
                  <asp:ListItem Text="Bulk Carrier" Value="Bulk Carrier"></asp:ListItem>
                 <asp:ListItem Text="Trailer" Value="Trailer"></asp:ListItem>
                 <asp:ListItem Text="Ready Mix" Value="Ready Mix"></asp:ListItem>
             </asp:DropDownList>
        </td>
        </tr>
                   <tr>
        <td align="right">
        Vehicle Id
        </td>
         <td colspan="3" align="left">
             <asp:Label ID="lblVehicleId" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
        <tr>
        <td align="right">
        Ghat
        </td>
         <td colspan="3" align="left">
             <asp:DropDownList ID="ddlHGhat" runat="server" Width="200px">
             </asp:DropDownList>
        </td>
        </tr>
            <tr>
        <td align="right">
        Vehicle No
        </td>
         <td colspan="3" align="left">
             <asp:TextBox ID="txtVehicleNo" runat="server" Width="200px"></asp:TextBox>
        </td>
        </tr>
        <tr>
            <td align="right">
            Model No
            </td>
             <td align="left">
                 <asp:TextBox ID="txtModelNo" runat="server" Width="200px" ></asp:TextBox>
                 
            </td>
            </tr>
          
            
           <tr>
            
            
                <td align="right">Capacity
            </td>
            <td align="left">
                <asp:TextBox ID="txtCapacity" runat="server" Width="200px"></asp:TextBox>
            </td>
            </tr>

            <tr>
                <td align="right">Capacity Unit
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlCapacityUnit" runat="server" Width="200px">
                <asp:ListItem Value="0" Text="MT"></asp:ListItem>
                <asp:ListItem Value="1" Text="BAG"></asp:ListItem>
                </asp:DropDownList> 
            </td>

            </tr>
            <tr>
            
                <td align="right">
                    Mobile No
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtMobileNo" runat="server" Width="200px"></asp:TextBox>
                     
                </td>
                </tr>
            
            
            <tr>
            
                <td align="right">
                    K.M Per Litre
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtKmPerLitre" runat="server" Width="200px">0</asp:TextBox>
                     
                     <ajaxToolkit:FilteredTextBoxExtender ID="txtKmPerLitre_FilteredTextBoxExtender" 
                         runat="server" BehaviorID="txtKmPerLitre_FilteredTextBoxExtender" 
                         TargetControlID="txtKmPerLitre" ValidChars=".0123456789" />
                     
                </td>
                </tr>
            
            <tr>
                <td align="right">Fuel Type
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlFuelType" runat="server" Width="200px">
                </asp:DropDownList> 
            </td>

            </tr>
            <%--<tr>
            
                <td align="right">
                    &nbsp;Hired
                </td>
                 <td align="left">
                     <asp:CheckBox ID="chkIsHired" runat="server" />
                     
                </td>
                </tr>--%>
             
            <tr>
                <td align="right">
                    Status
                </td>
                 <td align="left">
                     <asp:DropDownList ID="ddlHiredVehicleStatus" runat="server" Width="200px">
                     <asp:ListItem Value="0" Text="Pull"></asp:ListItem>
                     <asp:ListItem Value="1" Text="On Trip"></asp:ListItem>
                     <asp:ListItem Value="2" Text="Workshop"></asp:ListItem>
                     <asp:ListItem Value="3" Text="Not In Service"></asp:ListItem>
                     </asp:DropDownList>
                     
                </td>
            </tr>

           
            
        </table>
               </asp:Panel>
               </ContentTemplate>
  
        </asp:UpdatePanel>
        </asp:Panel>

    <asp:Panel ID="pnHDriver" runat="server" ScrollBars="Horizontal" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Hired Driver" BackColor="#FFFFCC">
        <asp:UpdatePanel ID="upAddDriver" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div style="background-position: left; text-align: left;">
             <asp:Button ID="btnAddDriver" runat="server" Text="Add" formnovalidate OnClick="btnAddDriver_Click" 
                  />
             <asp:Button ID="btnCancelAddDriver" runat="server" Text="Close" 
                 formnovalidate OnClick="btnCancelAddDriver_Click" />
             &nbsp; 
             <asp:Label ID="lblMessageDriver" runat="server"></asp:Label>
        </div>
                    <asp:Panel ID="Panel9" runat="server">

                        <table cellpadding="3px">
                            <tr>
                                <td align="right">Hired Driver Code</td>
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblEmpCode" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Name
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="306px" ></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td align="right">Father Name
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFatherName" runat="server" Width="306px"></asp:TextBox>
                                </td>
                            </tr>

                            

                            <tr>
                                <td align="right">Mobile Phone
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">NID
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtNID" runat="server" Width="200px"></asp:TextBox>
                                </td>
                              
                            </tr>
                            <tr>
                                <td align="right">Driving Licence
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDrivingLisense" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                
                            </tr>
                            

                              
                            <tr>

                                <td align="right">Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDriverStatus" runat="server" Width="120px">
                                        <asp:ListItem Value="0" Text="Active" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="On Trip"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                            </tr>

                        </table>
                    </asp:Panel>


            </ContentTemplate>
  
        </asp:UpdatePanel>
                </asp:Panel>

    <asp:Panel ID="Panel4" runat="server" CssClass="entry-panel">
            <%--<asp:UpdatePanel ID="upProductList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                     <table>
                   
            <tr>

            <td colspan="3">

                <asp:Panel ID="Panel5" runat="server" BorderWidth="1" BorderColor="#FFFFCC" 
        BackColor="#D3DCBE" ForeColor="Black" GroupingText="Add Product">
            <table cellpadding="5px" >
            <tr>
            <th>
            Product Name
            </th>
            <th>
            Quantity
            </th>
            <th>
            Unit Price
            </th>
            </tr>
            <tr style="border-style: groove">
            <td>
          
                <asp:DropDownList ID="ddlproductName" runat="server" AutoPostBack="True" Font-Size="14px" Height="24px">
              
                </asp:DropDownList>
              
                </td>
                 <td class="auto-style9">
                    <asp:TextBox ID="txtQuantity" runat="server" Height="18px" Width="60px"></asp:TextBox>*
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtQuantity_FilteredTextBoxExtender" 
                        runat="server" BehaviorID="txtQuantity_FilteredTextBoxExtender" 
                        TargetControlID="txtQuantity" ValidChars="0123456789." />
                </td>
                <td class="auto-style9">
                    <asp:TextBox ID="txtRate" runat="server" Height="18px" Width="72px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtRate_FilteredTextBoxExtender" 
                        runat="server" BehaviorID="txtRate_FilteredTextBoxExtender" 
                        TargetControlID="txtRate" ValidChars="0123456789." />
                </td>
               
                <td class="auto-style9">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
            <td colspan="4">
                &nbsp;<asp:GridView ID="grdListOfProduct" runat="server" AutoGenerateColumns="False" 
                    onrowdeleting="grdListOfProduct_RowDeleting" CellPadding="4" 
                    ForeColor="#333333" GridLines="Horizontal" 
                   
                    EmptyDataText="No Data To Show" ShowFooter="True" >
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:CommandField ShowDeleteButton="True" />
                     <asp:BoundField DataField="ProductCode" HeaderText="ProductCode"  />
                     <asp:BoundField DataField="ProductName" HeaderText="ProductName" ItemStyle-Width="200px"
                         ReadOnly="True" >
                     <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:TemplateField HeaderText="OrderQty">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OrderQty","{0:0}") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <FooterTemplate>
                             <asp:Label ID="lblTotalqty" runat="server"></asp:Label>
                         </FooterTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label1" runat="server" Text='<%# Bind("OrderQty", "{0:0}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="Rent" HeaderText="Rent" ReadOnly="True" 
                         DataFormatString="{0:0.00}" >
                     <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
                     <asp:TemplateField HeaderText="TotalAmount">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TotalAmount","{0:0.00}") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label2" runat="server" Text='<%# Bind("TotalAmount","{0:0.00}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                    
                 </Columns>
                 
                 <EditRowStyle BackColor="#2461BF" />
                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                 <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                 <RowStyle BackColor="#EFF3FB" />
                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                 <SortedAscendingCellStyle BackColor="#F5F7FB" />
                 <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                 <SortedDescendingCellStyle BackColor="#E9EBEF" />
                 <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
            </td>
    
            </tr>
            
                       
            
            </table>
               
            </asp:Panel>

                
            
           
            </td>
               
                
            </tr>
                                </table>

                <%--</ContentTemplate>
                <Triggers>
    <asp:AsyncPostBackTrigger ControlID="gvListofDOProduct" EventName="RowCommand" />
</Triggers>
            </asp:UpdatePanel>--%>
                          </asp:Panel>
    </ContentTemplate>
<Triggers>
 <asp:PostBackTrigger ControlID="btnSave" />
           
            </Triggers>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMembership" >
            <ProgressTemplate>
            <div class="UpdateProgress">
                <img src="../Images/ajax-loader.gif" alt="Processing..."/>
            </div>
            </ProgressTemplate>
    </asp:UpdateProgress>
   </asp:Panel>

</asp:Content>
