<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="InternalRequisitionReport.aspx.cs" Inherits="TransportManagerUI.UI.InternalRequisitionReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelRequisitionReport" runat="server">
        <ContentTemplate>
            
            
            <asp:Panel ID="Panel1" runat="server" GroupingText="Internal Requisition Report" CssClass="entry-panel">
            
                        <table border="1" cellpadding="3px" cellspacing="3px">>
                           
                            <tr>
                                <td class="style12" align="left">
                                    &nbsp;</td>
                                <td class="style55">
                                    <asp:Label ID="Label5" runat="server" Text="From"></asp:Label>
                                </td>
                                <td class="style59">
                                    <asp:TextBox ID="txtFromDate" runat="server" style="margin-right: 16px" Width="132px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                        TargetControlID="txtFromDate" />
                                    <asp:Label ID="Label4" runat="server" Text="To "></asp:Label>
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="txtToDate" runat="server" Width="132px" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                        TargetControlID="txtToDate" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style13" align="left">
                                    <asp:RadioButton ID="rbtnDeptWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnDeptWise_CheckedChanged" Text="Dtpt.Wise" />
                                </td>
                                <td class="style56">
                                    &nbsp;</td>
                                <td class="style60">
                                    &nbsp;</td>
                                <td class="style11">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                                        Height="24px" onselectedindexchanged="ddlDepartment_SelectedIndexChanged" 
                                        Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style13" align="left">
                                    <asp:RadioButton ID="rbtnSubDeptWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnSubDeptWise_CheckedChanged" Text="SubDtpt.Wise" />
                                </td>
                                <td class="style56">
                                    &nbsp;</td>
                                <td class="style60">
                                    &nbsp;</td>
                                <td class="style11">
                                    <asp:DropDownList ID="ddlSubDepartment" runat="server" Height="24px" 
                                        Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style13" align="left">
                                    <asp:RadioButton ID="rbtnVehicleWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnVehicleWise_CheckedChanged" Text="Vehicle Wise" />
                                </td>
                                <td class="style56">
                                    &nbsp;</td>
                                <td class="style60">
                                    &nbsp;</td>
                                <td class="style11">
                                    
                                    <asp:DropDownList ID="ddlVehicleID" runat="server" Height="24px" Width="140px">
                                    </asp:DropDownList>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="style13" align="left">
                                </td>
                                <td class="style56">
                                    &nbsp;</td>
                                <td class="style60">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <td class="style11">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style12" align="left">
                                    &nbsp;</td>
                                <td class="style55">
                                    &nbsp;</td>
                                <td class="style59">
                                    <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click1" Text="Show" Width="65px"/>
                                    &nbsp;<asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" Text="Exit" width="65px"/>
                                </td>
                                <td class="style7">
                                    &nbsp;</td>
                            </tr>
                        </table>
        
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#' + '<%= ddlVehicleID.ClientID %>').select2({
             
            });
          
          
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('#' + '<%= ddlVehicleID.ClientID %>').select2({                   
                });
               
               
            }
        });
          </script>
</asp:Content>
