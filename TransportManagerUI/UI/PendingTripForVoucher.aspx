<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PendingTripForVoucher.aspx.cs" Inherits="TransportManagerUI.UI.PendingTripForVoucher" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <meta http-equiv="refresh" content="30">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "PendingTripForVoucher.aspx/PageLoad",
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
       
        .auto-style3 {
            display: inline-block;
            background-color: White;
            font-weight: bold;
            
height: 20px;color: Blue;
width:306px;
            border-width:1px;
            border-style: solid;
           
            padding-left:2px;
            padding-right:2px;
            padding-top:0px;
            padding-bottom:0px;
           
        }
       
        .style1
        {
            height: 25px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="PlToolBox" runat="server" ToolTip="ToolBox">
       
    <asp:UpdatePanel  ID="uplMembership" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh"  formnovalidate 
                onclick="btnRefresh_Click"   />
        
   <asp:Button ID="txtNewVoucher" runat="server" Text="New Voucher" onclick="txtNewVoucher_Click" 
                />
           
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />

     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1" 
            BorderColor="Black" ScrollBars="Horizontal" 
            GroupingText="Trip Info (Not Billed)">
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
                                <td>Pending By:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPendingType" runat="server" Width="200px" Enabled="True" AutoPostBack="True" OnSelectedIndexChanged="ddlPendingType_SelectedIndexChanged">
                                         <asp:ListItem Text="Trip" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Vehiclewise" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Driverwise" Value="2"></asp:ListItem>
                     
                 </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                  <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">

        <!-- Pending Trip List-->
        <asp:GridView ID="gvlistofBasicData" runat="server" 
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" Width="100%" 
                     AllowPaging="False" Font-Size="Small" AllowSorting="False" 
            RowStyle-CssClass="GvGrid" ShowFooter="True" ShowHeader="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
               
               <asp:BoundField DataField="TripNo" HeaderText="Trip No" />
                 
                <asp:BoundField DataField="TripDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="Date" />
              
                
               
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                <asp:BoundField DataField="CapacityBal" HeaderText="Load Capacity" />
                <asp:BoundField DataField="EmpName" HeaderText="Driver" />
                  <asp:BoundField DataField="CustName" HeaderText="Dealer Name" />
               
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

        <!-- Vehiclewise Pending Trip List-->
        <asp:GridView ID="gvlistofBasicData2" runat="server" 
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" Font-Size="Small" 
            RowStyle-CssClass="GvGrid" ShowFooter="True">
            <AlternatingRowStyle BackColor="White" />
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

        <!-- Driverwise Pending Trip List-->
        
            </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
</div>
         
                  </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    
     
        
    </asp:Panel>

                
        </ContentTemplate>

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
