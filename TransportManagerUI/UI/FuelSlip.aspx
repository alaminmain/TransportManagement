<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="FuelSlip.aspx.cs" Inherits="TransportManagerUI.UI.FuelSlip" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "FuelSlip.aspx/PageLoad",
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
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    
    <asp:Panel ID="Panel4" runat="server">
        <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
    <asp:Panel ID="plTopControl" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel7" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" formnovalidate  />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click"  formnovalidate  />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click" />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="plBasicData" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="plBasicData" runat="server" BackColor="White" BorderColor="Black" BorderWidth="1" BorderStyle="Double">
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
                <asp:BoundField DataField="FuelSlipNo" HeaderText="FuelSlipNo" />
                <asp:BoundField DataField="TripDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="TripDate" />
                <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
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
  
    
        
  
    
        <asp:Panel ID="Panel2" runat="server" GroupingText="Fuel Slip" 
        CssClass="entry-panel" ForeColor="Black">
        <table cellpadding="3px" align="left">
          
            <tr>
            <td align="right" class="style1">
                Trip No
            </td>
             <td align="left">
                 <asp:ImageButton ID="btnTripSearch" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png" OnClick="btnTripSearch_Click" 
                     CssClass="ImageButtonSytle" />
                
            </td>
            </tr>
              <tr>
        <td align="right" >
            Fuel Slip No
        </td>
         <td align="left">
             <asp:Label ID="lblFuelSlipNo" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
            <tr>
            <td colspan="2">
                <asp:HiddenField ID="hfStatus" runat="server" />
                 <asp:HiddenField ID="hfShowTrip" runat="server" />
                 <ajaxToolkit:ModalPopupExtender ID="hfShowTrip_ModalPopupExtender" runat="server" BehaviorID="hfShowTrip_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowTrip" BackgroundCssClass="modalBackground" PopupControlID="plTrip">
                 </ajaxToolkit:ModalPopupExtender>
            </td>
             
            </tr>
            
          
            <tr>
            <td align="right" class="style1">
                Supplier
            </td>
             <td align="left">
                 <asp:TextBox ID="txtSupplier" runat="server" Width="202px" required>Madina Filling Station</asp:TextBox>
            </td>
            </tr>
           
            <tr>
            <td align="right">
            Adjusted Fuel Qty
            </td>
             <td align="left">
               <asp:TextBox ID="txtAdjustedFuelQty" runat="server" AutoPostBack="True" ViewStateMode="Enabled" OnTextChanged="txtAdjustedFuelQty_TextChanged"></asp:TextBox>
                 <ajaxToolkit:FilteredTextBoxExtender ID="txtAdjustedFuelQty_FilteredTextBoxExtender" runat="server" BehaviorID="txtAdjustedFuelQty_FilteredTextBoxExtender" TargetControlID="txtAdjustedFuelQty" ValidChars="1234567890." />
            </td>
            </tr>
            <tr>
            <td align="right">
            Total Fuel Quantity
            </td>
             <td align="left">
               <asp:TextBox ID="txtfuelQty" runat="server" ReadOnly="True" ></asp:TextBox>
            </td>
            </tr

            
           
            
   
            
            </table>
            <table>
                <tr>
                <td>
                
                    <asp:DetailsView ID="dvTrip" runat="server" AutoGenerateRows="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="Horizontal" >
                    
                        <AlternatingRowStyle BackColor="White" />
                        <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                        <EditRowStyle BackColor="#2461BF" HorizontalAlign="Left" />
                        <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                        <Fields>
                            <asp:BoundField DataField="TripNo" HeaderText="Trip No" />
                            <asp:BoundField DataField="TripDate" DataFormatString="{0:dd/MMM/yyyy}" 
                                HeaderText="Trip Date" />
                            <asp:BoundField DataField="EmpName" HeaderText="Driver Name" />
                            <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                            <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                            <asp:BoundField DataField="CapacityBal" HeaderText="Capacity Bal" 
                                DataFormatString="{0:dd/MMM/yyyy}" />
                            <asp:BoundField DataField="KmPerLiter" DataFormatString="{0:N}" HeaderText="KM PER Ltr" />
                            <asp:BoundField DataField="Totalkm" HeaderText="Total km" />
                            <asp:BoundField DataField="Additionalkm" HeaderText="Additionalkm" />
                        </Fields>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                    
                    </asp:DetailsView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    <asp:Panel ID="plTrip" runat="server" BorderStyle="Dotted" BorderWidth="2px" 
                   BackColor="White" ScrollBars="Auto" GroupingText="Select Trip">
                  <asp:UpdatePanel ID="upTrip" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <table>
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtSearchTrip" runat="server"/>
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnSearchdTrip" runat="server" Text="Search" CssClass="Button" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvTrip" runat="server" AllowPaging="True" 
                       CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                AutoGenerateColumns="False" OnPageIndexChanging="gvTrip_PageIndexChanging" 
                onselectedindexchanged="gvTrip_SelectedIndexChanged" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="TripNo" HeaderText="TripNo" />
                
                <asp:BoundField DataField="TripDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="TripDate" />
                <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />

                <asp:BoundField DataField="Totalkm" HeaderText="Totalkm" />

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
                <asp:Button ID="btnTripOk" runat="server" Text="Ok" Width="60px" />
            </td>
            <td class="auto-style2">
            <asp:Button ID="btnTripCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
                   </ContentTemplate>
<Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnSearchdTrip" />
           
            </Triggers>
</asp:UpdatePanel>
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
