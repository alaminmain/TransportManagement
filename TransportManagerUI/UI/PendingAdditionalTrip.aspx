﻿<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PendingAdditionalTrip.aspx.cs" Inherits="TransportManagerUI.UI.PendingAdditionalTrip" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
  
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "PendingTripInfo.aspx/PageLoad",
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

    <style type="text/css">
        .auto-style3 {
            display: inline-block;
            background-color: White;
            font-weight: bold;
            height: 20px;
            color: Blue;
            width: 306px;
            border-width: 1px;
            border-style: solid;
            padding-left: 2px;
            padding-right: 2px;
            padding-top: 0px;
            padding-bottom: 0px;
        }

        .style1 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="PlToolBox" runat="server" ToolTip="ToolBox">

        <asp:UpdatePanel ID="uplMembership" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC"
                    BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
                    <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" formnovalidate
                            OnClick="btnRefresh_Click" />

                        <asp:Button ID="txtNewTrip" runat="server" Text="New Additional Trip" 
                            OnClick="txtNewTrip_Click" />

                        <asp:Button ID="btnReport" runat="server" Text="Report"
                            OnClick="btnReport_Click"  />

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click" />

                    </asp:Panel>
                    <br />
                    <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1"
                        BorderColor="Black" ScrollBars="Horizontal" 
                        GroupingText="Additional Trip Info">
                        <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>

                                <div id="DivRoot" align="left">
                                    <div style="overflow: hidden;" id="DivHeaderRow">

                                    </div>

                                    <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                        <asp:GridView ID="gvlistofBasicData" runat="server"
                                            EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4"
                                            ForeColor="#333333" AutoGenerateColumns="False"
                                            OnPageIndexChanging="gvlistofBasicData_PageIndexChanging" 
                                            Font-Size="Small" AllowSorting="True" RowStyle-CssClass="GvGrid" 
                                            ShowFooter="True" Width="100%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>

                                                <asp:BoundField DataField="TripNo" HeaderText="Trip No" />

                                                <asp:BoundField DataField="TripDate" 
                                                    HeaderText="Date" />

                                                <asp:BoundField DataField="TripTime" 
                                                    HeaderText="Time" >

                                                <ItemStyle Width="100px" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                                                <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Driver" />
                                                <asp:BoundField DataField="CustName" HeaderText="Dealer Name" />
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
                                          </div>

    <div id="DivFooterRow" style="overflow:hidden">

    </div>
</div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>



                </asp:Panel>


            </ContentTemplate>

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
