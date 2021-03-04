<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="VMFReport.aspx.cs" Inherits="TransportManagerUI.UI.VMFReport" %>
 <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelVehicleMaintananceReport" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" GroupingText="VMF Reports" CssClass="entry-panel">
            <table >
                
                <tr>
                    <td class="style25">
                        <table >
                            <tr>
                                <td>
                                Select Date
                                </td>
                                <td>
                                From<br /> &nbsp;<asp:TextBox ID="txtFromDate" runat="server" Width="132px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                        TargetControlID="txtFromDate" />
                                </td>
                                <td>To<br />  <asp:TextBox ID="txtToDate" runat="server" style="margin-left: 16px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MM-yyyy" 
                                                    TargetControlID="txtToDate" /></td>
                            </tr>
                            
                            <tr>
                                <td class="auto-style2" align="left">
                                    <asp:RadioButton ID="rbtnAll" runat="server" AutoPostBack="True" Text="ALL" 
                                        oncheckedchanged="rbtnAll_CheckedChanged" />
                                </td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td class="style23">
                                    <asp:RadioButton ID="rbtnAllDetail" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnAllDetail_CheckedChanged" Text="Details" />
                                    <br />
                                    <asp:RadioButton ID="rbtnAllSummery" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnAllSummery_CheckedChanged" Text="Summery" />
                                </td>
                            </tr>
                           
                            <tr>
                                <td class="auto-style2" align="left">
                                    <asp:RadioButton ID="rbtnVehicleWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnVehicleWise_CheckedChanged" Text="Vehicle Wise" />
                                </td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td class="style23">
                                    <asp:DropDownList ID="ddlVehicleID" runat="server" Height="24px" Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2" align="left">
                                    <asp:RadioButton ID="rbtnSubDeptWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnSubDeptWise_CheckedChanged" Text="SubDept. Wise" />
                                </td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td class="style23">
                                    <asp:DropDownList ID="ddlSubDepartment" runat="server" AutoPostBack="True" 
                                        Height="24px" Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2" align="left">
                                    <asp:RadioButton ID="rbtnProductWise" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="rbtnProductWise_CheckedChanged" />
                                </td>
                                <td class="style10">
                                    <asp:Label ID="Label7" runat="server" Text="Product Wise" Visible="False"></asp:Label>
                                </td>
                                <td class="style23">
                                    <asp:DropDownList ID="ddlProductCode" runat="server" Height="24px" 
                                        Width="140px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2" align="left">
                                </td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td class="style23">
                                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2" align="left">
                                    &nbsp;</td>
                                <td class="style6">
                                    &nbsp;</td>
                                <td class="style22">
                                    <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click1" Text="Show" Width="65px" />
                                    &nbsp;<asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" Text="Exit" Width="65px"  />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
            </table>
                </asp:Panel>
             <script type="text/javascript">

                $(document).ready(function () {
                    $('#' + '<%= ddlVehicleID.ClientID %>').select2();
                    
                    $('#' + '<%= ddlProductCode.ClientID %>').select2();
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    function EndRequestHandler(sender, args) {
                        $('#' + '<%= ddlVehicleID.ClientID %>').select2();
                         
                    $('#' + '<%= ddlProductCode.ClientID %>').select2();
                    }
                });
    </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
