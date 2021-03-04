<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="casewithdrawn.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.casewithdrawn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "casewithdrawn.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
<asp:UpdatePanel ID="UpdatePanelCaseWIdraw" runat="server">
        <ContentTemplate>
           <asp:Panel runat="server" CssClass="entry-panel" GroupingText="Case Withdrawn">
            <table >
               
               
                <tr>
                
                    <td >
                        <table>
                        <tr>
                    
                    <td>
                        Case Widraw Date
                    </td>
                  
                    <td>
                        <asp:TextBox ID="txtWidrawDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtIssDate0_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txtWidrawDate" />
                    </td>
                    <td>
                        <asp:Button ID="btnWidraw" runat="server" onclick="btnApproved_Click" Text="Widraw" Width="70px" />
                    </td>
                            <tr>
                                <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                     />
                            </td>
                            </tr>
                            
                  
                </tr>
                            <tr>
                    <td colspan="4">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="lblMsg1" runat="server" ForeColor="Green"></asp:Label></td>
                    
                </tr>
                        </table>
                   
                
            </table>
            <asp:GridView ID="gvCaseWidraw" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                Width="80%" ShowFooter="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckALL" runat="server" AutoPostBack="True" 
                                oncheckedchanged="ckALL_CheckedChanged" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:CheckBox ID="ckAdminApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="AdministrativNo" HeaderText="Admin ID">
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IssDate" HeaderText="Issue Date">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No">
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpName" HeaderText="Driver Name">
                    <ItemStyle HorizontalAlign="Left" Width="175px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CateName" HeaderText="Type">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotAmount" HeaderText="Total Amount">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                    <HeaderStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscount" runat="server" Width="65px"></asp:TextBox>
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
          
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
