<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TripStatusReport.aspx.cs" Inherits="TransportManagerUI.UI.TripStatusReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel2" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left">
        &nbsp;
       <asp:ImageButton ID="btnRefresh" runat="server" 
                                    ImageUrl="~/Images/1488717327_Synchronize.png" Height="22px" 
                                    onclick="btnRefresh_Click" ToolTip="Refresh" Width="22px" />
       
       <asp:Button ID="btnPullReceived" runat="server" Text="Pull Received" OnClick="btnPullReceived_Click" />
        <asp:Button ID="btnAdvanceLoad" runat="server" Text="Advance Load" 
           onclick="btnAdvanceLoad_Click" />
        <asp:Button ID="btnWorkShopReceived" runat="server" Text="Workshop Received" OnClick="btnWorkShopReceived_Click" />
     </asp:Panel>
      
    <asp:Panel ID="Panel1" runat="server" CssClass="entry-panel">
        <table cellpadding="3px">
            <tr>
                <td valign="top">
                    <table border="1" style="color: #000000; border-style: groove">
                        <tr>
                            <td>
                                Vehicle Status Reports
                            </td>
                            
                        </tr>
                        <tr>
                        <td> Select Ghat: 
                            <asp:DropDownList ID="ddlGhat" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlGhat_SelectedIndexChanged" AppendDataBoundItems="true">
                                <asp:ListItem Text="All" Value="" />
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                
                                <asp:GridView ID="gvVehicleStatus" runat="server" AutoGenerateColumns="False" 
                                    Caption="Current Status" CaptionAlign="Top" CellPadding="4" ForeColor="#333333" 
                                    GridLines="Horizontal" 
                                    onselectedindexchanged="gvVehicleStatus_SelectedIndexChanged" 
                                    DataKeyNames="VehicleStatus" ShowFooter="True" Width="282px">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Status">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" CommandName="Select" runat="server" Text='<%# Bind("Status")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No of Vehicles">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("NoofVehicles") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NoofVehicles") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="VehicleStatus" HeaderText="VehicleStatus" 
                                            Visible="False" />
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
                                </td>
                        </tr>
                        <tr>
                            <td>
                               
                                &nbsp;</td>
                            
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table style="color: #000000; border-style: groove; background-color: #CCFFFF; padding-left: 3px;">
                        
                        
                        
                        <tr>
                        <td align="left">
                         <asp:LinkButton ID="btnReport" runat="server" onclick="btnReport_Click">Show Report</asp:LinkButton>
                        </td>
                           
                        </tr>

                        <tr>
                        <td>
                        <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                            <asp:GridView ID="gvVehicleList" runat="server" 
              CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                              ShowFooter="True"  
              >
            <AlternatingRowStyle BackColor="White" />
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
        
                        
                        </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
</asp:Content>
