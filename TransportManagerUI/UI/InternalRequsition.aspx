<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="InternalRequsition.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.InternalRequsition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "InternalRequsition.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
    });
    <style type="text/css">
        .style1
        {
            width: 10px;
        }
    </style>
    <style type="text/css">
         .myFont{
  font-size:4px;
}
        .style1
        {
            width: 208px;
        }
    </style>
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
                        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate />
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
                                <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" 
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="InternalReqNo" HeaderText="InternalReqNo">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IssVouchNo" HeaderText="IssVouchNo">
                    <ItemStyle HorizontalAlign="Center" width="160px" />
                    </asp:BoundField>
                <asp:BoundField DataField="IssDate" HeaderText="IssDate" 
                     />
                      <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" 
                     />
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
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" BorderStyle="Outset" BorderWidth="3px"
                    BorderColor="Silver" GroupingText="Internal Requisition" 
                    CssClass="entry-panel">
                    <asp:Panel ID="Panel6" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black">
                        <table cellpadding="5px">
                            <tr>
                                <td><table>
                            <tr>
                                <td class="text-right" align="right">
                                    Requisition No.</td>
                                <td align="left"  >
                                    <asp:Label runat="server" ID="lblIRNo" CssClass="IdStyle"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="#006600" Style="text-align: center"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="text-right" align="right">
                                    VMF Id
                                </td>
                                <td align="left"  >
                                    <asp:Label runat="server" ID="txtIssVoucherNo" CssClass="IdStyle"></asp:Label>&nbsp;&nbsp;
                                    <asp:ImageButton ID="btnVMF" runat="server" CssClass="ImageButtonSytle" 
                                        ImageUrl="~/Images/1488717192_Search.png" OnClick="btnVMF_Click" />
                                    <asp:HiddenField ID="hfShowVFMGrid" runat="server" />
                                    &nbsp;<ajaxToolkit:ModalPopupExtender ID="hfShowVFMGrid_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" BehaviorID="hfShowVFMGrid_ModalPopupExtender" DynamicServicePath="" PopupControlID="pnVMF" TargetControlID="hfShowVFMGrid">
                                    </ajaxToolkit:ModalPopupExtender>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="text-right" align="right">
                                    IR Date
                                </td>
                                <td  align="left">
                                    <asp:TextBox ID="txtIssDate" runat="server" TabIndex="1"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="txtIssDate_CalendarExtender" runat="server" BehaviorID="txtIssDate_CalendarExtender"
                                        TargetControlID="txtIssDate" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                            
                            <tr>
                    <td class="style15" align="right">
                        Department
                    </td>
                    <td class="style4" align="left">
                        <asp:DropDownList ID="ddlDepartment" runat="server" Height="24px" Width="141px" 
                            onselectedindexchanged="ddlDepartment_SelectedIndexChanged" TabIndex="1" AutoPostBack="True">
                        </asp:DropDownList>
                        </td>
                </tr>
                            <tr>
                                <td class="text-right" align="right">
                                    Sub Department
                                </td>
                                <td  align="left">
                                    <asp:DropDownList ID="ddlSubDepartment" runat="server" AutoPostBack="True" Height="24px"
                                        OnSelectedIndexChanged="ddlSubDepartment_SelectedIndexChanged" Width="140px"
                                        TabIndex="6">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                        <td class="style106" align="right">
                                            <p style="font-family: SutonnyMJ; font-size: medium; font-style: normal; font-weight: normal">gšÍe¨ </p> 
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtRemark" runat="server" Font-Names="SutonnyMJ" 
                                                TextMode="MultiLine" Width="300px" Height="60px" Font-Size="Medium"></asp:TextBox>
                                        </td>
                                    </tr>
                            <tr>
                            <td></td>
                                <td align="left" >
                                    <asp:CheckBox ID="ckWithoutParts" runat="server" Text="Service" AutoPostBack="True"
                                        OnCheckedChanged="ckWithoutParts_CheckedChanged" TabIndex="11"  />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td align="left" >
                                    &nbsp;</td>
                            </tr>
                            
                        </table></td>
                                <td>
                                    <asp:Panel ID="Panel4" runat="server">
                                        <asp:DetailsView ID="dvVMF" runat="server" CellPadding="4" ForeColor="#333333" 
                                            GridLines="Horizontal" Height="50px" Caption="VMF Info" 
                                            AutoGenerateRows="False">
                                            <AlternatingRowStyle BackColor="White" />
                                            <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FieldHeaderStyle HorizontalAlign="Left" BackColor="#DEE8F5" Font-Bold="True" />
                                            <Fields>
                                                <asp:BoundField DataField="IssVouchNo" HeaderText="Voucher No" />
                                                <asp:BoundField DataField="IssDate"  HeaderText="Voucher Date" />
                                                <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                                                <asp:BoundField DataField="ModelNo" HeaderText="Model No" />
                                                <asp:BoundField DataField="EmpName" HeaderText="Driver" />
                                                <asp:BoundField DataField="TechnicianName" HeaderText="Technician" />
                                                <asp:BoundField DataField="FromDate" HeaderText="FromDate" />
                                                <asp:BoundField DataField="ToDate" HeaderText="ToDate" />
                                            </Fields>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                        </asp:DetailsView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        
                        
                        <br />
                        <table border="1">
                            <tr>
                                <td class="auto-style12">
                                    Product Name
                                </td>
                                <td class="auto-style11" align="center">
                                    Issue Qty
                                </td>
                                <td  align="center">
                                    Unit Price
                                </td>
                                <td  align="center">
                                    Remarks
                                </td>
                                <td class="">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlProductCode" runat="server" Height="24px" TabIndex="14"
                                        Width="200px">
                                    </asp:DropDownList>
                                   
                                </td>
                                <td  align="center">
                                    <asp:TextBox ID="txtIssuQty" runat="server" AutoPostBack="True"
                                        Style="margin-left: 0px; font-weight: 700;" Width="60px" 
                                        ontextchanged="txtIssuQty_TextChanged"></asp:TextBox>
                                </td>
                                <td  align="center">
                                    <asp:TextBox ID="txtPrice" runat="server" Style="margin-left: 0px" 
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td  align="center">
                                    <asp:TextBox ID="txtComment" runat="server" Style="margin-left: 0px" 
                                        Width="160px"></asp:TextBox>
                                </td>
                                <td >
                                    <asp:Button ID="btnAddToList" runat="server" OnClick="btnAddToList_Click" Text="Add To List"
                                        Width="96px" />
                                </td>
                            </tr>
                        </table>

                          <asp:GridView ID="gvEditVMF" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Center" 
                        ShowFooter="True">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" />
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                            <asp:BoundField DataField="ProductBName" HeaderText="Product Name (Bangla)">
                                <ItemStyle Font-Names="SutonnyMJ" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StoreCode" HeaderText="StoreCode">
                                <ItemStyle Width="120px" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label26" runat="server" Text="ProductQty"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIssuQTY" runat="server"
                                        Style="text-align: center;" Text='<%# Eval("IssQty") %>' Columns="11" TabIndex="13"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label27" runat="server" Text="Unit Price"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPrice" runat="server" BackColor="#66CCFF" ReadOnly="True" Style="text-align: right;"
                                        Text='<%# Eval("PurPrice") %>' Width="85px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label29" runat="server" Text="Total Price"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotalAMT" runat="server" BackColor="#66CCFF" ReadOnly="True" Style="text-align: right;"
                                        Text='<%# Eval("TotalAMT") %>' Width="85px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="Label32" runat="server" Text="Remarks"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" runat="server" Height="22px" Width="95px" Text='<%# Eval("Comment") %>'
                                        TabIndex="14"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ckAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ck" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="ck_CheckedChanged"
                                         />
                                </ItemTemplate>
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
                    <table >
                                    <tr>
                                        <td>
                                            Total Qty
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtTotalQTY" runat="server" Width="92px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Total Amount
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalAMT" runat="server" Width="92px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                    </asp:Panel>
                  
                    
                </asp:Panel>

                <asp:Panel ID="pnVMF" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="3px"
                        ScrollBars="Auto">
                        <asp:UpdatePanel ID="upVMF" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtVFMSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnVMFSearch" runat="server" Text="Search"
                                                formnovalidate OnClick="btnVMFSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvVFM" runat="server" AllowPaging="True" EmptyDataText="No Data To Show"
                                    GridLines="Horizontal" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" OnPageIndexChanging="gvVFM_PageIndexChanging" OnSelectedIndexChanged="gvVFM_SelectedIndexChanged"
                                    >
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="IssVouchNo" HeaderText="VMF No">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                <asp:BoundField DataField="IssDate" HeaderText="IssDate" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" >
               
                                        <ItemStyle Font-Names="SutonnyMJ" Font-Size="Medium" />
                                        </asp:BoundField>
               
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
                                            <asp:Button ID="btnVMFOk" runat="server" Text="Ok" Width="60px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnVMFCancel" runat="server" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
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
    <script type="text/javascript">

        $(document).ready(function () {
           
            $('#' + '<%= ddlProductCode.ClientID %>').select2();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
              
                $('#' + '<%= ddlProductCode.ClientID %>').select2();
            }
        });
          </script>
</asp:Content>
