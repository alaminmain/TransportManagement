<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="DailyStock.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.DailyStock" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelDailyStock" runat="server">
        <ContentTemplate>
           <asp:Panel runat="server" CssClass="entry-panel" GroupText="Daily Stock">
           
            <table>
                <tr>
                    <td class="style9">
                        <asp:Label ID="Label1" runat="server" Text="Closing Date :"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:TextBox ID="txtClosingDate" runat="server" style="margin-left: 0px" 
                            TabIndex="2"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtClosingDate_CalendarExtender" runat="server" 
                            Format="dd-MM-yyyy" TargetControlID="txtClosingDate" />
                    </td>
                    <td>
                        <table >
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rbtnForTheDate" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnForTheDate_CheckedChanged" Text="For The Date" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rbtnMonthToDate" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnMonthToDate_CheckedChanged" Text="Month To Date" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style9">
                        &nbsp;</td>
                    <td class="style8">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="green"></asp:Label>
                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style9">
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
           <table >
                <tr>
                    <td class="style5">
                        &nbsp;</td>
                    <td class="style26">
                        <asp:Button ID="btnProcess" runat="server" onclick="btnProcess_Click" 
                            Text="Process" ToolTip=" " Width="150px" />
                        &nbsp;<asp:Button ID="btnPrint" runat="server" onclick="btnPrint_Click" Text="Print" 
                            Width="65px" />
                        &nbsp;<asp:Button ID="btnCancle" runat="server" onclick="btnCancle_Click" 
                            Text="Exit" Width="65px" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
