<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PendingMovementInfo.aspx.cs" Inherits="TransportManagerUI.UI.PendingMovementInfo" %>
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
        
   <asp:Button ID="btnMovement" runat="server" Text="New Movement" OnClick="btnMovement_Click"   />
           <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" 
                   />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1" BorderColor="Black" GroupingText="List of Movement" Height="500px">
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
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" Font-Size="Small" RowStyle-CssClass="GvGrid" ShowFooter="True" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="TripNo" HeaderText="TripNo" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="MovementNo" HeaderText="MovementNo" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="TransportDate"
                    HeaderText="Date" />
                
                 
                <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" >
                <ItemStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="CustName" HeaderText="Retailer Name" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="RetailerAddress" HeaderText="Delivery Location" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="LocDistance" HeaderText="Distance (KM)" DataFormatString="{0:00}" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" >
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EmpName" HeaderText="Driver" >
              
                <ItemStyle Width="100px" />
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
