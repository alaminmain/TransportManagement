<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="AdminmnistrativReport.aspx.cs" Inherits="TransportManagerUI.UI.AdminmnistrativReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelAdminReport" runat="server">
        <ContentTemplate>
            
            <br />
            <asp:Panel ID="Panel1" runat="server" GroupingText="Administrative Report" CssClass="entry-panel">
            <table>
                <tr>
                    <td class="style34">
                        &nbsp;</td>
                    <td class="style20">
                       </td>
                </tr>
                <tr>
                    <td class="style34">
                        &nbsp;</td>
                    <td class="style20">
                        <table border="1" cellpadding="3px" cellspacing="3px">
                            <tr>
                                <td class="style30" align="left">
                                    &nbsp;</td>
                                <td class="style26">
                                    <asp:CheckBox ID="ckPaperRenewal" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="ckPaperRenewal_CheckedChanged" Text="Paper Renewal" />
                                </td>
                                <td class="style53">
                                    <asp:CheckBox ID="ckCase" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="ckCase_CheckedChanged" Text="Case" />
                                </td>
                                <td class="style7">
                                    <asp:CheckBox ID="ckAccidental" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="ckAccidental_CheckedChanged" Text="Accidental" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="style31" align="left">
                                    <asp:RadioButton ID="rbtnAll" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnAll_CheckedChanged" Text="All" />
                                </td>
                                <td class="style55" align="left">
                                    <asp:Label ID="Label4" runat="server" Text="From Date"></asp:Label>
                                </td>
                                <td class="style54" align="left">
                                    <asp:TextBox ID="txtFromDate" runat="server" Width="122px" style="margin-right: 16px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                                        Format="dd-MM-yyyy" TargetControlID="txtFromDate" />
                                    <asp:Label ID="Label5" runat="server" Text="To Date"></asp:Label>
                                </td>
                                <td class="style11">
                                    &nbsp;<asp:TextBox ID="txtToDate" runat="server"  Width="122px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" BehaviorID="txtToDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="style31" align="left">
                                    <asp:RadioButton ID="rbtnVehicleWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnVehicleWise_CheckedChanged" Text="VehicleWise" />
                                </td>
                                <td align="left" >
                                    <asp:Label ID="Label6" runat="server" Text="Vehicle No:"></asp:Label>
                                </td>
                                <td  align="left">
                                    <asp:DropDownList ID="ddlVehicleNo" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style31" align="left">
                                    <asp:RadioButton ID="rbtnDriverWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnDriverWise_CheckedChanged" Text="Driverwise" />
                                </td>
                                <td class="style55" align="left">
                                    <asp:Label ID="lblDriverName" runat="server" Text="Driver Name:"></asp:Label>
                                </td>
                                <td class="style54" align="left">
                                    <asp:DropDownList ID="ddlDriverID" runat="server" Width="200px" >
                                    </asp:DropDownList>
                                </td>
                                <td class="style11">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style31" align="left">
                                    &nbsp;</td>
                                <td class="style27">
                                </td>
                                <td class="style54" align="left">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <td class="style11">
                                </td>
                            </tr>
                            <tr>
                                <td class="style30" align="left">
                                    &nbsp;</td>
                                <td class="style26">
                                    &nbsp;</td>
                                <td class="style53">
                                    <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click1" Text="Show" Width="65px" />
                                    &nbsp;<asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" Text="Exit" Width="65px" />
                                </td>
                                <td class="style7">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style34">
                        &nbsp;</td>
                    <td class="style20">
                        &nbsp;</td>
                </tr>
            </table>
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

     <script type="text/javascript">

        $(document).ready(function () {
            $('#' + '<%= ddlVehicleNo.ClientID %>').select2({
             
            });
            $('#' + '<%= ddlDriverID.ClientID %>').select2();
          
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('#' + '<%= ddlVehicleNo.ClientID %>').select2({                   
                });
                $('#' + '<%= ddlDriverID.ClientID %>').select2();
               
            }
        });
          </script>
    
</asp:Content>
