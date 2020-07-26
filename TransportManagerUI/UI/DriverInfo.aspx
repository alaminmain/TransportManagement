<%@ Page Title="Driver Info" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="DriverInfo.aspx.cs" Inherits="TransportManagerUI.UI.DriverInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "DriverInfo.aspx/PageLoad",
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
                    <asp:Panel ID="Panel7" runat="server" HorizontalAlign="Left">
                        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate  />
                        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate  />
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate  />


                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
                        <asp:HiddenField ID="hfShowList" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel8" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="Panel8" runat="server" BorderWidth="2px" BackColor="White" BorderStyle="Double">
                        <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" formnovalidate />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True"
                                    EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4"
                                    ForeColor="#333333" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvlistofBasicData_PageIndexChanging"
                                    OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" />
                                        <asp:BoundField DataField="EmpName" HeaderText="EmpName" />
                                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
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

                <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Employee Info" CssClass="entry-panel">

                    <asp:Panel ID="Panel6" runat="server">

                        <table cellpadding="3px">
                            <tr>
                                <td align="right">&nbsp;Emp. Code
                                </td>
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblEmpCode" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">&nbsp;Name
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="306px" required></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td align="right">Father Name
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFatherName" runat="server" Width="306px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Mother Name
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMotherName" runat="server" Width="306px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Blood Group
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" AutoPostBack="False">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">A+</asp:ListItem>
                                        <asp:ListItem Value="2">A-</asp:ListItem>
                                        <asp:ListItem Value="3">B+</asp:ListItem>
                                        <asp:ListItem Value="4">B-</asp:ListItem>
                                        <asp:ListItem Value="5">AB+</asp:ListItem>
                                        <asp:ListItem Value="6">AB-</asp:ListItem>
                                        <asp:ListItem Value="7">O+</asp:ListItem>
                                        <asp:ListItem Value="8">O-</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                           
                            <tr>
                                <td align="right">Address 1
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress1" runat="server" Width="306px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Address 2
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress2" runat="server" Width="306px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Address 3
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress3" runat="server" Width="306px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Post Code
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Designation
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDesignation" runat="server" AutoPostBack="false" Width="120px">
                                        <asp:ListItem Value="1" Text="Driver"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Mechanic"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Hired Driver"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="right">Mobile Phone
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">NID
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtNID" runat="server" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                            </tr>
                            <tr>
                                <td align="right">Driving Licence
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDrivingLisense" runat="server" Width="200px" required></asp:TextBox>
                                </td>
                                <td>
                            </tr>
                            <tr>
                                <td align="right">Driving Capacity
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDrivingCapacity" runat="server" ></asp:TextBox>
                                </td>
                                <td>
                            </tr>

                              <tr>

                                <td align="right">Ghat
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlGhat" runat="server" Width="200px">
                                      
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>

                                <td align="right">Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Width="120px">
                                        <asp:ListItem Value="0" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="On Trip"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>
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
