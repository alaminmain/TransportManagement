<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="VehicleMovmentEntry.aspx.cs" Inherits="TransportManagerUI.UI.VehicleMovmentEntry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "VehicleMovmentEntry.aspx/PageLoad",
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
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" />
        <asp:Button ID="btnShowList" runat="server" Text="List" 
                onclick="btnShowList_Click" />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click" />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
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
                     EmptyDataText="No Data To Show" GridLines="None" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     onselectedindexchanged="gvlistofBasicData_SelectedIndexChanged" >
                     
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                 <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="MoveRegNo" HeaderText="MoveRegNo" />
                <asp:BoundField DataField="MoveDate" DataFormatString="{0:dd/MMM/yyyy}" 
                     HeaderText="MoveDate" />
                <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                 <asp:BoundField DataField="VehicleStatus" HeaderText="VehicleStatus" />
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
  
    
        <asp:Panel ID="Panel2" runat="server" GroupingText="Workshop Movement" 
        CssClass="entry-panel" ForeColor="Black">
        <table cellpadding="3px" align="left">
          <tr>
            <td align="right" class="style1">
                Movement Id</td>
             <td align="left">
                  <asp:Label ID="lblMovmentId" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
                
            </td>
            </tr>
            <tr>
            <td align="right" class="style1">
                Vehicle</td>
             <td align="left">
                 <asp:ImageButton ID="btnSearchVehicle" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png"
                     CssClass="ImageButtonSytle" OnClick="btnSearchVehicle_Click" />
                
            </td>
            </tr>
            <tr>
            <td align="right">
                Driver
            </td>
             <td align="left" class="style1">

             <asp:Label ID="lblDriverCode" runat="server" Font-Bold="True" 
                     CssClass="lblInformation1" required></asp:Label>
             <asp:ImageButton ID="btnDriver" runat="server" ImageUrl="~/Images/1488717192_Search.png" 
                     CssClass="ImageButtonSytle" onclick="btnDriver_Click" />
                     <asp:Label ID="lblDriverName" runat="server" Font-Bold="True" 
                     CssClass="lblInformation2"></asp:Label>
              
            </td>
           
            </tr>
            <tr>
            <td colspan="2">
                 <asp:HiddenField ID="hfShowTrip" runat="server" />
                 <ajaxToolkit:ModalPopupExtender ID="hfShowTrip_ModalPopupExtender" runat="server" BehaviorID="hfShowTrip_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowTrip" BackgroundCssClass="modalBackground" PopupControlID="plVehicleSearch">
                 </ajaxToolkit:ModalPopupExtender>
                 <asp:HiddenField ID="hfDriverSearch" runat="server" />
                  
                  <ajaxToolkit:ModalPopupExtender ID="hfDriverSearch_ModalPopupExtender" 
                     runat="server" BehaviorID="hfDriverSearch_ModalPopupExtender" DynamicServicePath="" 
                     TargetControlID="hfDriverSearch" BackgroundCssClass="modalBackground" PopupControlID="plDriver" OkControlID="btnDriverOk" CancelControlID="btnDriverCancel">
                 </ajaxToolkit:ModalPopupExtender>
            </td>
             
            </tr>
            
          
            <tr>
            <td align="right" class="style1">
                Current Status
            </td>
             <td align="left">
                 <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" Enabled="False">
                    
                     <asp:ListItem Text="Workshop" Value="2"></asp:ListItem>
                     
                 </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td align="right">
                Date
            </td>
             <td align="left">
               <asp:TextBox ID="txtStatusUpdateDate" runat="server" ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="txtStatusUpdateDate_CalendarExtender" runat="server" BehaviorID="txtStatusUpdateDate_CalendarExtender" TargetControlID="txtStatusUpdateDate" Format="dd/MMM/yyyy" />
                 &nbsp;
                 <asp:TextBox ID="txtTime" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="txtTime_MaskedEditExtender" runat="server" 
                     Mask="99:99:99"
             MessageValidatorTip="true"
             OnFocusCssClass="MaskedEditFocus"
             OnInvalidCssClass="MaskedEditError"
             MaskType="Time"
             AcceptAMPM="True"
            ErrorTooltipEnabled="True" TargetControlID="txtTime" />
            
               
            
            </td>
            </tr>
            <tr>
            <td align="right">
                Remarks
            </td>
             <td align="left">
               <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Font-Names="SutonnyMJ" Width="400px" Font-Size="Medium" ></asp:TextBox>
                 
            </td>
            </tr>
            
           
            
   
            
            </table>
            <table align="left">
                <tr>
                <td>
                
                    <asp:DetailsView ID="dvVehicle" runat="server" AutoGenerateRows="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" >
                    
                        <AlternatingRowStyle BackColor="White" />
                        <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                        <Fields>
                            
                            <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />
                           
                            <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                            <asp:BoundField DataField="ChesisNo" HeaderText="ChesisNo" />
                            <asp:BoundField DataField="ModelNo" HeaderText="ModelNo" />
                            <asp:BoundField DataField="EngineNo" HeaderText="EngineNo" 
                                 />
                            <asp:BoundField DataField="KmPerLiter" HeaderText="KmPerLiter" />
                        </Fields>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                    
                    </asp:DetailsView>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                <td>
                
                    
                <asp:GridView ID="gvVehicleStatus" runat="server" AutoGenerateColumns="False" 
                        Caption="Current Status" CaptionAlign="Top" CellPadding="4" ForeColor="#333333" 
                        GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="NoofVehicles" HeaderText="No of Vehicles" />
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
              CellPadding="4" ForeColor="#333333" GridLines="None" 
                AutoGenerateColumns="False" 
                onselectedindexchanged="gvVehicleList_SelectedIndexChanged" OnPageIndexChanging="gvVehicleList_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
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
              CellPadding="4" ForeColor="#333333" GridLines="None" 
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
