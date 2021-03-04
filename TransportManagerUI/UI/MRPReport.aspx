<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="MRPReport.aspx.cs" Inherits="TransportManagerUI.UI.MRPReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRequisitionReport" runat="server">
        <ContentTemplate>
            
            
            <asp:Panel ID="Panel1" runat="server" GroupingText="MRP Report" CssClass="entry-panel">
            
                        <table >
                        
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rbtnAll" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="rbtnAll_CheckedChanged" Text="All" />
                            </td>
                            <td class="style55">
                                <asp:Label ID="Label3" runat="server" Text="From"></asp:Label>
                            </td>
                            <td class="style20">
                                <asp:TextBox ID="txtFromDate" runat="server" style="margin-right: 16px" Width="132px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                    TargetControlID="txtFromDate" />
                                <asp:Label ID="Label4" runat="server" Text="To "></asp:Label>
                            </td>
                            <td class="style17">
                                <asp:TextBox ID="txtToDate" runat="server" Width="132px" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                    TargetControlID="txtToDate" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rbtnPoWise" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="rbtnPoWise_CheckedChanged" Text="POwise" />
                            </td>
                            <td class="style55">
                                &nbsp;</td>
                            <td class="style20">
                                <asp:Label ID="Label5" runat="server" Text="PO Wise"></asp:Label>
                            </td>
                            <td class="style17">
                                <asp:DropDownList ID="ddlPO" runat="server" Height="24px" Width="140px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:RadioButton ID="rbtnSupplierWise" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="rbtnSupplierWise_CheckedChanged" Text="Supplier wise" />
                            </td>
                            <td class="style55">
                                &nbsp;</td>
                            <td class="style20">
                                <asp:Label ID="Label8" runat="server" Text="Supplier Wise"></asp:Label>
                            </td>
                            <td class="style17">
                                <asp:DropDownList ID="ddlSupplier" runat="server" Height="24px" Width="140px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;</td>
                            <td class="style55">
                                &nbsp;</td>
                            <td class="style20">
                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td class="style17">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;</td>
                            <td class="style55">
                                &nbsp;</td>
                            <td class="style20">
                                <asp:Button ID="btnShow" runat="server"  onclick="btnShow_Click1" 
                                    Text="Show" Width="65px" />
                                &nbsp;<asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" 
                                    Text="Exit" Width="65px" />
                            </td>
                            <td class="style17">
                                &nbsp;</td>
                        </tr>
                    </table>
        
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
