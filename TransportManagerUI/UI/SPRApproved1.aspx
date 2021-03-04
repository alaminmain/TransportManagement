<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="SPRApproved1.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.SPRApproved1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "SPRApproved1.aspx/PageLoad",
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
                        <asp:UpdatePanel ID="upListofbasicData" runat="server">
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
                                    OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged" 
                                    OnPageIndexChanging="gvlistofBasicData_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="ReqNo" HeaderText="ReqNo">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                <asp:BoundField DataField="ReqDate" HeaderText="Date" />
                <asp:BoundField DataField="Remark" HeaderText="Remarks" />
               
               
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
                    BorderColor="Silver" GroupingText="Purchase Requisition Approve By Department Head" CssClass="entry-panel">
                    <asp:Panel ID="Panel6" runat="server" GroupingText="" ForeColor="Black" CssClass="entry-panel">
                     <table >
                <tr>
                    <td align="right">
                       Requisition No
                    </td>
                    <td class="style4" align="left">
                        <asp:TextBox ID="txtReqNo" runat="server" BackColor="#66CCFF" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style15" align="right">
                        Requisition Date
                    </td>
                    <td class="style4" align="left">
                        <asp:TextBox ID="txtReqDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" Format="dd/MM/yyyy"  TargetControlID="txtReqDate" runat="server" />
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
                    <td class="style15" align="right">
                        Sub Department
                    </td>
                    <td class="style4" align="left">
                        <asp:DropDownList ID="ddlSubDepartment" runat="server" Height="24px" 
                            Width="141px" TabIndex="2" AutoPostBack="True" 
                            onselectedindexchanged="ddlSubDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                    </td>
                </tr>
                         <tr>
                             <td align="right">Remarks</td>
                             <td align="left">
<asp:TextBox ID="txtRemark" runat="server" Height="20px" TabIndex="21" 
                            Width="269px"></asp:TextBox>
                             </td>
                         </tr>
                          <tr>
                                <td align="right" class="style106">
                                    Status
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlStatus" runat="server" Height="24px" Width="200px" 
                                        Enabled="False">
                                        <asp:ListItem Text="Open" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Approved By Department Head" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Approved By Factory Head" Value="2"></asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </td>
                                </tr>
                <%--<tr>
                    <td class="style36" align="right">
                        Requisition Type
                        </td>
                    <td class="style37" align="left">
                        <asp:CheckBox ID="ckStorePurchase" runat="server" AutoPostBack="True" 
                             Text="Load Store Product" 
                            TabIndex="3" OnCheckedChanged="ckStorePurchase_CheckedChanged" />
                        
                      
                    </td>
                    
                </tr>--%>
                <tr>
                <td>&nbsp;</td>
                <td align="left">
                    <asp:Button ID="btnReorderLevel" runat="server" onclick="btnReorderLevel_Click" 
                        Text="Load Reorder Level" />
                </td>
                </tr>
                <tr>
                    <td class="style15" align="right">
                        &nbsp;</td>
                    <td class="style4" align="left">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="#009933" 
                            style="font-weight: 700"></asp:Label>
                    </td>
                </tr>


            </table>
            <br />
            <table style="width:100%; border:1px solid green;">
                <tr>
                    <td class="style33">
                        &nbsp;</td>
                    <td class="style34">
                        Product Name
                    </td>
                    <td class="style40">
                        Present Requisition
                    </td>
                    <td class="style41">
                        Require Day
                    </td>
                    <td class="style42">
                        Estimated Value
                    </td>
                    <td class="style43">
                        Per Month Used
                    </td>
                    <td class="style44">
                       Remarks
                    </td>
                    <td class="style45">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style33">
                        &nbsp;</td>
                    <td class="style34">
                        <table style="width: 86%; margin-right: 11px;">
                            <tr>
                                <td class="style7">
                                    <asp:DropDownList ID="ddlProductCode" runat="server" 
                                        TabIndex="8">
                                    </asp:DropDownList>
                                </td>
                                
                            </tr>
                        </table>
                    </td>
                    <td class="style40">
                        <asp:TextBox ID="txtReqQTY" runat="server" Height="19px" Width="85px" 
                            TabIndex="9"></asp:TextBox>
                    </td>
                    <td class="style41">
                        <asp:TextBox ID="txtReqDay" runat="server" Height="19px" Width="85px" 
                            TabIndex="10"></asp:TextBox>
                    </td>
                    <td class="style42">
                        <asp:TextBox ID="txtReqPrice" runat="server" Height="19px" Width="85px" 
                            TabIndex="11"></asp:TextBox>
                    </td>
                    <td class="style43">
                        <asp:TextBox ID="txtPerMonthUse" runat="server" Height="19px" Width="85px" 
                            TabIndex="12"></asp:TextBox>
                    </td>
                    <td class="style44">
                        <asp:TextBox ID="txtComments" runat="server" Height="19px" Width="85px" 
                            TabIndex="13"></asp:TextBox>
                    </td>
                    <td class="style45">
                        <asp:Button ID="btnAddtoList" runat="server" onclick="btnAddtoList_Click" 
                            Text="Add to list" Width="82px" TabIndex="14" />
                    </td>
                </tr>
            </table>
            <br />
          <asp:Panel ID="Panel4" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black" >
         
            <br />
            <asp:GridView ID="gvPurRequisition" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                    <asp:BoundField DataField="ProductName" HeaderText="Goods Purticulars" />
                    <asp:BoundField DataField="ProductBName" HeaderText="Bengali Name" 
                        Visible="False" />
                    <asp:BoundField DataField="StoreCode" HeaderText="Store Code" />
                    <asp:BoundField DataField="PhyStock" HeaderText="Balance" />
                    <asp:BoundField DataField="UnitType" HeaderText="UOM" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Label5" runat="server" Text="Present REQ"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPresentReq" runat="server" Height="22px" Width="85px" Text='<%# Eval("ReqQty") %>'
                                TabIndex="15">0</asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Label6" runat="server" Text="Require Day"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtReqDay" runat="server" Height="22px" Width="85px" Text='<%# Eval("RequireDay") %>'
                                TabIndex="16">0</asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Label7" runat="server" Text="Estimated Value"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtEstmitedValue" runat="server" Height="22px" Width="85px" Text='<%# Eval("ReqPrice") %>' 
                                ontextchanged="txtEstmitedValue_TextChanged" AutoPostBack="True" 
                                TabIndex="17">0</asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Label8" runat="server" Text="Total Value"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtTotalValue" runat="server" Height="22px" Width="85px" Text='<%# Eval("TotalAMT") %>'
                                TabIndex="18">0</asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Label9" runat="server" Text="Per Month Use"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPerMonthUse" runat="server" Height="21px" Width="85px" Text='<%# Eval("UsedPerMonth") %>'
                                TabIndex="19">0</asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="Label10" runat="server" Text="Remarks"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Height="22px" Width="85px" Text='<%# Eval("Comments") %>'
                                TabIndex="20"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ckALL" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="ckALL_CheckedChanged" 
                                 />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ck" runat="server" Checked="true"  />
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
                        </asp:Panel>
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