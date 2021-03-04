<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="AdministrativWorkApproved.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.AdministrativWorkApproved" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "AdministrativWorkApproved.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelAdminApproved" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Panel1" runat="server" CssClass="entry-panel" ToolTip="ADMINISTRATIV APPROVED" GroupingText="ADMINISTRATIV APPROVED">
        
        
       <table >
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnApproved" runat="server" onclick="btnApproved_Click" 
                        Text="Approved" Width="70px" />
                    &nbsp;&nbsp;<asp:Button ID="btnCancle" runat="server" 
                        onclick="btnCancle_Click" Text="Exit" Width="65px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table>
            
            <tr>
                <td class="style71">
                </td>
                <td class="style148">
                    &nbsp;</td>
                <td class="style154">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblMsg1" runat="server" ForeColor="Green"></asp:Label>
                </td>
                <td class="style132">
                  
                </td>
                <td class="style2">
                    &nbsp;</td>
            </tr>
        </table>
        
        <asp:GridView ID="gvAdminApproved" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
            Width="80%" ShowFooter="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ckALL" runat="server" AutoPostBack="True" 
                            oncheckedchanged="ckALL_CheckedChanged" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="30px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="ckAdminApproved" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" >
                <ItemStyle HorizontalAlign="Left" Width="120px" />
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="IssDate" HeaderText="Issue Date" >
                <ItemStyle HorizontalAlign="Left" Width="100px" />
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="AdministrativNo" HeaderText="AdministrativNo" >
                <ItemStyle HorizontalAlign="Left" Width="170px"/>
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ModelNo" HeaderText="Model No" >
                <ItemStyle HorizontalAlign="Left" Width="100px"/>
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="EmpName" HeaderText="Driver Name" >
                <ItemStyle HorizontalAlign="Left" Width="150px" />
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CateName" HeaderText="Type" >
                <ItemStyle HorizontalAlign="Left" Width="100px" />
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TotAmount" HeaderText="Total Amount" >
                <ItemStyle HorizontalAlign="Right" Width="100px"/>
                <HeaderStyle HorizontalAlign="Right" />
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
       
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
