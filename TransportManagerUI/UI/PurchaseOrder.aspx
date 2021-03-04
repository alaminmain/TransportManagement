<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.PurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "PurchaseOrder.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
   
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
                                        <asp:BoundField DataField="PurOrderNo" HeaderText="PurOrderNo">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                                        <asp:BoundField DataField="ReqNo" HeaderText="ReqNo">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" />
                <asp:BoundField DataField="PurOrderDesc" HeaderText="Remarks" />
               
               
               
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
                    BorderColor="Silver" GroupingText="Purchase Order" CssClass="entry-panel">
                    <asp:Panel ID="Panel6" runat="server" GroupingText="" ForeColor="Black">
                     <table>
                         
                              <tr>
                                  <td align="right">PO No </td>
                                  <td align="left">
                                      <asp:Label ID="lblPONO" runat="server" CssClass="IdStyle"></asp:Label>
                                      &nbsp;&nbsp;
                                      <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="#009933"></asp:Label>
                                  </td>
                              </tr>
                          
                <tr>
                    <td align="right">
                      Select Requisition
                    </td>
                    <td class="style4" align="left">
                        <asp:TextBox ID="txtReqNo" runat="server" BackColor="#66CCFF" ReadOnly="True"></asp:TextBox>
                         <asp:ImageButton ID="btnRequistionSearch" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png"
                     CssClass="ImageButtonSytle" OnClick="btnRequistionSearch_Click" />
                        <asp:HiddenField ID="hfShowReqGrid" runat="server" />
                 <ajaxToolkit:ModalPopupExtender ID="hfShowReqGrid_ModalPopupExtender" runat="server" BehaviorID="hfShowReqGrid_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowReqGrid" BackgroundCssClass="modalBackground" PopupControlID="pnRequsition">
                 </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style15" align="right">
                        Requisition Date
                    </td>
                    <td class="style4" align="left">
                        <asp:TextBox ID="txtReqDate" runat="server" BackColor="#66CCFF"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <td class="style38" align="right">
                            Purchase OrderDate
                        </td>
                        <td class="style36" align="left">
                            <asp:TextBox ID="txtPurOrderDate" runat="server" TabIndex="6"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtPurOrderDate_CalendarExtender" runat="server" BehaviorID="txtPurOrderDate_CalendarExtender" TargetControlID="txtPurOrderDate" Format="dd/MM/yyyy"  />
                            
                        </td>
                </tr>
                          
                              <tr>
                                  <td class="style37" align="right">Description </td>
                                  <td align="left" class="style35">
                                      <asp:TextBox ID="txtPurOrderDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                                  </td>
                              </tr>
                         
                         <tr>
                             <td class="style38" align="right">
                            <asp:Label ID="Label6" runat="server" Text="Suplier Name:"></asp:Label>
                        </td>
                        <td class="style36" align="left">
                            <asp:DropDownList ID="ddlSupplierID" runat="server" Height="30px" Width="200px" 
                                TabIndex="7">
                            </asp:DropDownList>
                        </td>
                         </tr>
                
               
                <tr>
                    <td class="style15" align="right">
                        &nbsp;</td>
                    <td class="style4" align="left">
                        &nbsp;</td>
                </tr>


            </table>
            <br />
            <br />
                        <asp:Panel ID="Panel4" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black" >
         
            <asp:GridView ID="gvPurchaseEdit" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Center" 
            Width="70%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ReqSLNo" HeaderText="Order Serial" Visible="False" />
                        <asp:BoundField DataField="ProductCode" HeaderText="Product Code" >
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="ProductName" HeaderText="ProductName" >
                        <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="StoreCode" HeaderText="Store Code" >
                        <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="ReqQty" HeaderText="Requisition Qty" >
                        <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="OrderQty" HeaderText="Previous Order" >
                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField >
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label5" runat="server" Text="Order QTY"></asp:Label>
                            </HeaderTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label3" runat="server" Text="Order QTY"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtOrderQTY" style="text-align:center" runat="server" 
                            Text='<%# Eval("OrdQty") %>' Height="22px" Width="85px" AutoPostBack="True" 
                            ontextchanged="txtOrderQTY_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label4" runat="server" Text="Unit Price"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPurPrice" style="text-align:right" runat="server" 
                            Text='<%# Eval("PurPrice") %>' Height="22px" Width="85px" AutoPostBack="True" 
                                    ontextchanged="txtPurPrice_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label15" runat="server" Text="Total AMT"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtTotalAMT" style="text-align:right" runat="server" 
                            Text='<%# Eval("TotalAMT") %>' Height="22px" Width="85px" BackColor="#66CCFF" 
                            ReadOnly="True"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="ckAll" runat="server" AutoPostBack="True" Checked="true"
                            oncheckedchanged="ckSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ck" runat="server" Checked="true"
                             />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
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

               
                    <asp:Panel ID="pnRequsition" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="3px"
                        ScrollBars="Auto">
                        <asp:UpdatePanel ID="upReqsition" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtReqSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnReqSearch" runat="server" Text="Search" 
                                                formnovalidate OnClick="btnReqSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvRequisition" runat="server" AllowPaging="True" EmptyDataText="No Data To Show"
                                    GridLines="Horizontal" CellPadding="4" ForeColor="#333333" 
                                    AutoGenerateColumns="False" 
                                    OnPageIndexChanging="gvRequisition_PageIndexChanging" OnSelectedIndexChanged="gvRequisition_SelectedIndexChanged"
                                    >
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="ReqNo" HeaderText="ReqNo">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                <asp:BoundField DataField="ReqDate" HeaderText="Date" />
                <asp:BoundField DataField="CateName" HeaderText="CateName" />
                <asp:BoundField DataField="ProdSubCatName" HeaderText="ProdSubCatName" />
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
                                            <asp:Button ID="btnReqOk" runat="server" Text="Ok" Width="60px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnReqCancel" runat="server" Text="Cancel" />
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
    
</asp:Content>