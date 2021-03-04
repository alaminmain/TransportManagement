<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PendingTCList.aspx.cs" Inherits="TransportManagerUI.UI.PendingTCList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <style type="text/css">
     .FixedHeader {
            position:relative;
            font-weight: bold;
        }   
        </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="PlToolBox" runat="server" >
       
    <asp:UpdatePanel  ID="uplMembership" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" >
        <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh"  formnovalidate 
                onclick="btnRefresh_Click"   />
        
   <asp:Button ID="btnTC" runat="server" Text="New TC" OnClick="btnTC_Click"  />
           <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1" BorderColor="Black" GroupingText="List of TC" Height="500px">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
                 <div>
                     
                 
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
                
                     </div>
                 <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
        <asp:GridView ID="gvlistofBasicData" runat="server" 
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
            Font-Size="Small" RowStyle-CssClass="GvGrid" ShowFooter="True" width="100%" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="TCNo" HeaderText="TC No" >
                <ItemStyle Wrap="False" width="140px"/>
                </asp:BoundField>
                <asp:BoundField DataField="TCDate"  
                    HeaderText="Date Time" >
                <ItemStyle Wrap="True" Width="90px"/>
                </asp:BoundField>
              
                
                 <asp:BoundField DataField="paymentMode" HeaderText="Delivery Mode">
                  <ItemStyle Wrap="True" Width="40px"/>
                </asp:BoundField>
                <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" ItemStyle-Wrap="True">
                <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CustName" HeaderText="Retailer Name" 
                    ItemStyle-Wrap="True">
                <ItemStyle Wrap="True" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="RetailerAddress" HeaderText="Delivery Location" 
                    ItemStyle-Wrap="True" >
                <ItemStyle Wrap="True" Width="200px" />
                </asp:BoundField>
               <%-- <asp:BoundField DataField="Mobile" HeaderText="Retailer Phone" />--%>
                <asp:BoundField DataField="ProductName" HeaderText="Material Type" >
               
                <ItemStyle Width="120px" />
               
                </asp:BoundField>
                <asp:BoundField DataField="InvNo" HeaderText="DO NO" />
                <asp:BoundField DataField="OrderQty" HeaderText="Qty" DataFormatString="{0:0}"  />
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
