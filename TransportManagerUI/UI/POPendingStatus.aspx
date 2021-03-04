<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="POPendingStatus.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.POPendingStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "POPendingStatus.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
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
        
   <asp:Button ID="btnNewDO" runat="server" Text="New PO" onclick="btnNewDO_Click"  />
           
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />

     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1" 
            BorderColor="Black"  GroupingText="SPR Pending Status">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
     <tr>
     <td >
                        Department
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlDepartment" runat="server" Height="24px" Width="140px" 
                            onselectedindexchanged="ddlDepartment_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                     <td >
                        SubDepartment
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubDepartment" runat="server" Height="24px" 
                            Width="140px">
                        </asp:DropDownList>
                    </td>
         <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                     />
                            </td>
     </tr>
                           
                        </table>

                   <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
    
        <asp:GridView ID="gvlistofBasicData" runat="server" RowStyle-CssClass="GvGrid"
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     
            Width="100%" Font-Size="Small" 
                     
            ShowFooter="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>               
               <asp:BoundField DataField="ProductCode" HeaderText="Product Code">
                <ItemStyle HorizontalAlign="Center" width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductName" HeaderText="Product Name">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ProductBName" HeaderText="Product Name (Bangla)">
                <ItemStyle Font-Names="SutonnyMJ" HorizontalAlign="left" />
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="StoreCode" HeaderText="Store Code">
                <ItemStyle HorizontalAlign="Center" width="175px" />
                </asp:BoundField>
                <asp:BoundField DataField="OrdQty" HeaderText="Order QTY">
                <ItemStyle HorizontalAlign="Center" width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="ReceivedQty" HeaderText="Received QTY" >
                <ItemStyle HorizontalAlign="Center" width="100px" />
                </asp:BoundField>
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

