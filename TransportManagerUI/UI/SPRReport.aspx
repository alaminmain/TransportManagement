<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="SPRReport.aspx.cs" Inherits="TransportManagerUI.UI.SPRReport" %>
 <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 102px;
        }
        .auto-style4 {
            width: 241px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRequisitionReport" runat="server">
        <ContentTemplate>
            
            
            <asp:Panel ID="Panel1" runat="server" GroupingText="SPR Report" CssClass="entry-panel">
            <table style="width:100%;" border="1" cellpadding="3px" cellspacing="2px">
                
                <tr>
                    <td class="style19">
                        <table border="1" cellpadding="2px" cellspacing="2px">
                            
                            <tr>
                                <td align="left">
                                    <asp:RadioButton ID="rbtnAll" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnAll_CheckedChanged" Text="All" />
                                </td>
                                <td class="auto-style3">
                                    <asp:Label ID="Label3" runat="server" Text="From"></asp:Label>
                                </td>
                                <td class="auto-style4">
                                    <asp:TextBox ID="txtFromDate" runat="server" style="margin-right: 16px" Width="132px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                        TargetControlID="txtFromDate" />
                                    <asp:Label ID="Label4" runat="server" Text="To "></asp:Label>
                                </td>
                                <td class="style20">
                                    <asp:TextBox ID="txtToDate" runat="server" Width="132px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                        TargetControlID="txtToDate" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:RadioButton ID="rbtnDeptWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnDeptWise_CheckedChanged" Text="Dept.Wise" />
                                </td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                <td class="auto-style4">
                                    <asp:Label ID="Label5" runat="server" Text="Department Name:"></asp:Label>
                                </td>
                                <td class="style20">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                                        Height="24px" onselectedindexchanged="ddlDepartment_SelectedIndexChanged" 
                                        Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:RadioButton ID="rbtnSubDeptWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnSubDeptWise_CheckedChanged" Text="SubDept.Wise" />
                                </td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                <td class="auto-style4">
                                    <asp:Label ID="Label6" runat="server" Text="Sub Department Name:"></asp:Label>
                                </td>
                                <td class="style25">
                                    <asp:DropDownList ID="ddlSubDepartment" runat="server" Height="24px" 
                                        Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                <td class="auto-style4">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <td class="style20">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;</td>
                                <td class="auto-style3">
                                    &nbsp;</td>
                                <td class="auto-style4">
                                    <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click" Text="Show" Width="65px" />
                                    &nbsp;<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Exit" Width="65px" />
                                </td>
                                <td class="style20">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                
            </table>
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
