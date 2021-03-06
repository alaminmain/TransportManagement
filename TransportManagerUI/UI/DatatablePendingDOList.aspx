﻿<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="DatatablePendingDOList.aspx.cs" Inherits="TransportManagerUI.UI.DatatablePendingDOList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
  
    <script type="text/javascript">
   
//     $(document).ready(function () {

//         $.ajax({
//             type: "POST",
//             url: "PendingDOList.aspx/PageLoad",
//             contentType: "application/json; charset=utf-8",
//             dataType: "json",
//             error: Error
//         });

//     });
     $(document).ready(function () {
         var oTable = $("#cphMainContent_gvlistofBasicData").DataTable({
            
             "sScrollY": (0.5 * $(window).height()),
             "bPaginate": false,
             "bJQueryUI": true,
             "bScrollCollapse": true

         });

         if ($.browser.webkit) {
             setTimeout(function () {
                 oTable.fnAdjustColumnSizing();
             }, 10);
         }
         //setTimeout(function () {
         //    oTable.fnAdjustColumnSizing();
         //}, 10);
        
     });
    
    

     //    function Error(request, status, error) {
     //        alert('Not Loggeed In');
     //        var url = "NotLoggedIn.htm";
     //        $(location).attr('href', url);

     //    }

  </script>
    
    <style type="text/css">
     .dataTables_scroll
{
    overflow:auto;
}
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
    <asp:Panel ID="PlToolBox" runat="server">
       
    <asp:UpdatePanel  ID="uplMembership" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" >
        <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh"  formnovalidate 
                onclick="btnRefresh_Click"   />
        
   <asp:Button ID="btnNewDO" runat="server" Text="New DO"  OnClientClick="http://localhost:3214/UI/TransportContact.aspx.target ='_blank';" OnClick="btnNewDO_Click" />
           
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />

     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1" 
            BorderColor="Black"  GroupingText="Pending DO">
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

     <%--   <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

   <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">--%>
      <div style ="width:1070px;">
        <asp:GridView ID="gvlistofBasicData" runat="server" 
                     EmptyDataText="No Data To Show" AutoGenerateColumns="False" 
                     class="display" 
              BorderStyle="Inset" BorderWidth="1px" CellPadding="4" Width="40%"               
            ShowFooter="True" onprerender="gvlistofBasicData_PreRender" ViewStateMode="Disabled" GridLines="Horizontal">
            <Columns>
               
                <asp:TemplateField HeaderText="DO No">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("InvNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("InvNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="DONo" HeaderText="SAP DO NO" >
               
                </asp:BoundField>
                <asp:BoundField DataField="InvDate" DataFormatString="{0:dd.MM.yy}" 
                    HeaderText="Date" >
               
                </asp:BoundField>
                <asp:BoundField DataField="StoreName" HeaderText="Delivery from" />
                <asp:BoundField DataField="paymentMode" HeaderText="Delivery Mode" />
                <asp:BoundField DataField="CustName" HeaderText="Dealer Name" >
               
                </asp:BoundField>
                <asp:BoundField DataField="Address" HeaderText="Delivery Location" >
               
                </asp:BoundField>
                <asp:BoundField DataField="ProductName" HeaderText="Material Name" >
               
                </asp:BoundField>
                <asp:BoundField DataField="OrderQty" HeaderText="Order Qty" DataFormatString="{0:0}" />
                <asp:BoundField DataField="SOQty" HeaderText="SO Qty" DataFormatString="{0:0}" />
                <asp:BoundField DataField="PendingQty" HeaderText="Pending Qty" DataFormatString="{0:0}" />
                <asp:BoundField DataField="UnitPrice" HeaderText="DO Price" DataFormatString="{0:0.00}" />
            </Columns>
        </asp:GridView>
</div>
<%--        </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
</div>--%>
         
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
