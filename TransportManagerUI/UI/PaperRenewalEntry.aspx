<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PaperRenewalEntry.aspx.cs" Inherits="TransportManagerUI.UI.PaperRenewalEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "PaperRenewalEntry.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
  
    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel5" runat="server">
        <asp:UpdatePanel ID="uplMembership" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" BorderColor="#3399FF"
                    Direction="LeftToRight" ScrollBars="Auto">
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left">
                        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate style="width: 39px" />
                        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click"
                            formnovalidate />
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate OnClick="btnReport_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            formnovalidate />
                        <asp:HiddenField ID="hfShowList" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server"
                            BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList"
                            PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="3px"
                        ScrollBars="Auto">
                        <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                formnovalidate />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" EmptyDataText="No Data To Show"
                                    GridLines="Horizontal" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged" OnPageIndexChanging="gvlistofBasicData_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="AdministrativNo" HeaderText="Admin ID"></asp:BoundField>
                                        <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                                        <asp:BoundField DataField="IssDate" HeaderText="IssDate" />
                                        <asp:BoundField DataField="CateName" HeaderText="Type" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSearchOk" runat="server" Text="Ok" Width="60px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server"  CssClass="entry-panel">
                    <asp:Panel ID="Panel6" runat="server" GroupingText="Paper Renewal Entry" CssClass="entry-panel" ForeColor="Black">
                        <table style="border: 1px solid green;">
                             <tr>
                                <td align="right" >
                                </td>
                                <td align="left" >
                                    <asp:DropDownList ID="ddlWorkType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWorkType_SelectedIndexChanged" Visible="False">
                                        
                                        <asp:ListItem Value="02" Selected="True">Paper Renewal</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >Paper Renwal ID
                                </td>
                                <td align="left" >
                                    <asp:TextBox ID="txtAdminNo" runat="server" CssClass="IdStyle"></asp:TextBox>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblMsg2" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >Issue Date
                                </td>
                                <td align="left" >
                                    <asp:TextBox ID="txtIssDate" runat="server" Width="80px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtIssDate0_CalendarExtender" runat="server"
                                        Format="dd/MM/yyyy" TargetControlID="txtIssDate" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >Vehicle No
                                </td>
                                <td align="left" >
                                    <asp:DropDownList ID="ddlVehicleID" runat="server" Width="200px" 
                                        AutoPostBack="True" onselectedindexchanged="ddlVehicleID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;</td>
                            </tr>
                            
                          
                            
                            <tr>
                                <td align="right">
                        Remark
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                            </tr>
                            <tr>
                                <td align="right" class="style106">
                                    Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Height="24px" Width="200px">
                                        <asp:ListItem Text="Open" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                     
                                       
                                        <asp:ListItem Text="Cancle" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </table>
                        
                        <table style=" border:1px solid green;">
                <tr>
                    <td class="style151">
                        <asp:Label ID="lblItemId" runat="server" Text="Paper Renewal Type"></asp:Label>
                    </td>
                    <td class="style142">
                        <asp:Label ID="lblDateType" runat="server" Text="Expired Date"></asp:Label>
                    </td>
                    <td>
                        Amount
                    </td>
                    <td class="style138">
                        Remarks
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style151">
                        <asp:DropDownList ID="ddlItemType" runat="server" Height="24px" 
                            style="margin-left: 0px" Width="121px">
                        </asp:DropDownList>
                    </td>
                    <td class="style142">
                        <asp:TextBox ID="txtExpareDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtExpareDate_CalendarExtender" runat="server" 
                            Format="dd/MM/yyyy" TargetControlID="txtExpareDate" />
                    </td>
                    <td class="style149">
                        <asp:TextBox ID="txtAmount" runat="server" Width="123px"></asp:TextBox>
                    </td>
                    <td class="style138">
                        <asp:TextBox ID="txtComments" runat="server"  Width="123px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAddtoList" runat="server" 
                             Text="Add To List" Width="83px" OnClick="btnAddtoList_Click" />
                    </td>
                </tr>
            </table>
                    </asp:Panel>
                    <asp:GridView ID="gvAdministration" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                        Width="80%" ShowFooter="True" 
                        OnRowDataBound="gvAdministration_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                        <Columns>
                <asp:BoundField DataField="ItemID" HeaderText="Event ID">
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ItemName" HeaderText="Event Name" >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                   
                    <asp:BoundField DataField="ExpDate" HeaderText="Expiry Date" >
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" >
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Comments" HeaderText="Remarks" >
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                            <%--<asp:TemplateField HeaderText="RenewalDate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRenewalDate" runat="server" placeholder="dd/MM/YYYY"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckAll" runat="server" AutoPostBack="True" checked="true"
                                oncheckedchanged="ckAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ck" runat="server" AutoPostBack="True" OnCheckedChanged="ck_CheckedChanged" checked="true"
                                />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
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
                   
                                <table>
                                    <tr>
                                        <td>
                                            Total Amount
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalAMT" runat="server" Width="160px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                           
                </asp:Panel>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMembership">
            <ProgressTemplate>
                <div class="UpdateProgress">
                    <img src="../../Images/ajax-loader.gif" alt="Processing..." />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
</asp:Content>
