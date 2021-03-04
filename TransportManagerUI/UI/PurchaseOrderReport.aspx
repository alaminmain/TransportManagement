<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReport.aspx.cs" Inherits="TransportManagerUI.UI.PurchaseOrderReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRequisitionReport" runat="server">
        <ContentTemplate>
            
            
            <asp:Panel ID="Panel1" runat="server" GroupingText="Purchase Order Report" CssClass="entry-panel">
            
                        <table  border="1" cellpadding="3px" cellspacing="3px">
                            
                            <tr>
                                <td class="style14" align="left">
                                    <asp:RadioButton ID="rbtnAll" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnAll_CheckedChanged" Text="All" />
                                </td>
                                <td class="style64">
                                    <asp:Label ID="Label3" runat="server" Text="From"></asp:Label>
                                </td>
                                <td class="style65">
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="132px"  style="margin-right: 16px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                        TargetControlID="txtFromDate" />
                                    <asp:Label ID="Label4" runat="server" Text="To "></asp:Label>
                                </td>
                                <td class="style58">
                                    <asp:TextBox ID="txtToDate" runat="server" Width="132px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                        TargetControlID="txtToDate" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style14" align="left">
                                    <asp:RadioButton ID="rbtnSupplierWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnSupplierWise_CheckedChanged" Text="Supplierwise" />
                                </td>
                                <td class="style64">
                                    &nbsp;</td>
                                <td class="style65">
                                    <asp:Label ID="Label5" runat="server" Text="Supplier Wise"></asp:Label>
                                </td>
                                <td class="style58">
                                    <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" 
                                        Height="24px" Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style14" align="left">
                                    &nbsp;</td>
                                <td class="style64">
                                    &nbsp;</td>
                                <td class="style65">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <td class="style58">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style14" align="left">
                                    &nbsp;</td>
                                <td class="style64">
                                    &nbsp;</td>
                                <td class="style65">
                                    <asp:Button ID="btnShow" runat="server"  onclick="btnShow_Click1" 
                                        Text="Show" Width="65px" />
                                    &nbsp;<asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" 
                                        Text="Exit" Width="65px" />
                                </td>
                                <td class="style58">
                                    &nbsp;</td>
                            </tr>
                        </table>
        
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
