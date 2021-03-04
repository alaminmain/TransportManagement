<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="MovementReports.aspx.cs" Inherits="TransportManagerUI.UI.MovementReports" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
        .auto-style2 {
            height: 25px;
        }
    </style>
    <script type="text/javascript">
        function NewWindow() {
            document.forms[0].target = "_blank";
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" BackColor="#CCCCCC">
        <table>
            <tr>
                <td>
                    <table border="1" style="color: #000000; border-style: groove">
                        <tr>
                            <td>
                                Movement Reports
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:RadioButtonList ID="rblReports" runat="server" ForeColor="Black" OnSelectedIndexChanged="rblReports_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Text="Movement Statement" Value="MovementStatementRpt" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Statement Details" Value="MoveStatementDetail" ></asp:ListItem>
                                    <asp:ListItem Text="Vehiclewise Statement" Value="MoveStatementVehicle"></asp:ListItem>
                                    <asp:ListItem Text="Dealerwise Statement" Value="MoveDetailDealer"></asp:ListItem>
                                    
                                    <asp:ListItem Text="Productwise Statement" Value="MoveProductWise"></asp:ListItem>
                                    
                                    
                                    <asp:ListItem Text="Dealerwise Summery" Value="MoveDealerwiseSummery"></asp:ListItem>
                                                                    
                                   
                                </asp:RadioButtonList>
                                </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table style="color: #000000; border-style: groove; background-color: #CCFFFF; padding-left: 3px;">
                        <tr>
                            <td style="color: black;" class="auto-style2">From Date</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" BehaviorID="txtFromDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd/MMM/yyyy" />
                            </td>
                            <td class="auto-style2">To Date</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" BehaviorID="txtToDate_CalendarExtender" TargetControlID="txtToDate" Format="dd/MMM/yyyy" FirstDayOfWeek="Saturday" />
                            </td>
                        </tr>
                        <tr>
                            
                            <td colspan="4">
                               <asp:Panel ID="POtherOptions" GroupingText="Search" runat="server">
                                   <table>
                                       <tr>
                                           <td>
                                               <asp:Label ID="lblCode" runat="server" CssClass="lblInformation1"></asp:Label></td>
                                           <td>
                                               <asp:ImageButton ID="btnSearchInfo" runat="server" ImageUrl="~/Images/1488717192_Search.png" CssClass="ImageButtonSytle" OnClick="btnSearchInfo_Click" />

                                           </td>
                                            <td>
                                               <asp:Label ID="lblName" runat="server" CssClass="lblInformation2"></asp:Label></td>
                                           <td>&nbsp;</td>
                                       </tr>
                                   </table>
                               </asp:Panel> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click" OnClientClick="NewWindow();"/>
                            </td>
                             <td>
                                <asp:Button ID="btnShowStatment" runat="server" Text="Show Report(Onscreen)" OnClick="btnShowStatment_Click" />
                            </td>
                            <td> <asp:HiddenField ID="hfShowList" runat="server" />
                                 <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7"  CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1" BorderColor="Black">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" 
                     EmptyDataText="No Data To Show" GridLines="None" CellPadding="4" 
                     ForeColor="#333333" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     onselectedindexchanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
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
         <table>
            <tr>
            <td>
                <asp:Button ID="btnSearchOk" runat="server" Text="Ok" Width="60px"  style="height: 26px" />
            </td>
            <td>
            <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
                  </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

