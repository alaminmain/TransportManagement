﻿<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="LocationInfo.aspx.cs" Inherits="TransportManagerUI.UI.LocationInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "LocationInfo.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    //    function Error(request, status, error) {
    //        alert('Not Loggeed In');
    //        var url = "NotLoggedIn.htm";
    //        $(location).attr('href', url);

    //    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel5" runat="server">
        <asp:UpdatePanel ID="uplMembership" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC"
                    BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left">
                        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate />
                        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate />
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate />


                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" formnovalidate />
                        <asp:HiddenField ID="hfShowList" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="3px" ScrollBars="Auto">
                        <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" formnovalidate/>
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True"
                                    EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4"
                                    ForeColor="#333333" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged"
                                    OnPageIndexChanging="gvlistofBasicData_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="LocSLNO" HeaderText="SLNO" />
                                        <asp:BoundField DataField="LocName" HeaderText="Location" />
                                        <asp:BoundField DataField="District" HeaderText="District" />
                                        <asp:BoundField DataField="LocDistance" HeaderText="Distance" />
                                        <asp:BoundField DataField="LocRent" HeaderText="Rent" />
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
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Location & Rent" CssClass="entry-panel">

                    <asp:Panel ID="Panel6" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black">

                        <table cellpadding="3px">
                            <tr>
                                <td align="right">Code
                                </td>
                                <td colspan="3" align="left">
                                    <asp:Label ID="lblLocSlNo" runat="server" Font-Bold="True"
                                        CssClass="IdStyle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Location Name
                                </td>
                                <td colspan="3" align="left">
                                    <asp:TextBox ID="txtLocationName" runat="server" required Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="right">District
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDistict" runat="server"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td align="right">Distance
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDistance" runat="server" Text="0"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td align="right">Rent
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRent" runat="server" required Text="0.00"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;</td>
                            </tr>

                        </table>
                    </asp:Panel>



                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />

            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMembership">
            <ProgressTemplate>
                <div class="UpdateProgress">
                    <img src="../Images/ajax-loader.gif" alt="Processing..." />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
</asp:Content>
