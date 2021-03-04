<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="MRR.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.MRR" %>
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
                                    OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged" 
                                    OnPageIndexChanging="gvlistofBasicData_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        <asp:BoundField DataField="PurRecNo" HeaderText="PurRecNo">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                                        <asp:BoundField DataField="RecDate" HeaderText="RecDate">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                <asp:BoundField DataField="InvNo" HeaderText="InvNo" />
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
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" BorderStyle="Outset" BorderWidth="3px"
                    BorderColor="Silver" GroupingText="Purchase Order Received" CssClass="entry-panel">
                    <asp:Panel ID="Panel6" runat="server" GroupingText="" ForeColor="Black" CssClass="entry-panel">
                     <table>
                         
                              <tr>
                                  <td align="right">PO Receive No </td>
                                  <td align="left">
                                      <asp:Label ID="lblPORecNo" runat="server" CssClass="IdStyle"></asp:Label>
                                      &nbsp;&nbsp;
                                      <asp:Label ID="lblMessage" runat="server" ForeColor="#009933" Font-Bold="True"></asp:Label>
                                  </td>
                              </tr>
                          
                <tr>
                    <td align="right">
                      Select Purchase Order
                    </td>
                    <td class="style4" align="left">
                        <asp:TextBox ID="txtPurOrderNo" runat="server" BackColor="#66CCFF" ReadOnly="True"></asp:TextBox>
                         <asp:ImageButton ID="btnPurchaseOrder" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png"
                     CssClass="ImageButtonSytle" OnClick="btnPurchaseOrder_Click"  />
                        <asp:HiddenField ID="hfShowPurGrid" runat="server" />
                 <ajaxToolkit:ModalPopupExtender ID="hfShowPurGrid_ModalPopupExtender" runat="server" BehaviorID="hfShowPurGrid_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowPurGrid" BackgroundCssClass="modalBackground" PopupControlID="pnPurchase">
                 </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style15" align="right">
                        Purchase Order Date
                    </td>
                    <td class="style4" align="left">
                        <asp:TextBox ID="txtPODate" runat="server" BackColor="#66CCFF"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <td  align="right">
                            Receive Date
                        </td>
                        <td  align="left">
                            <asp:TextBox ID="txtRecDate" runat="server" TabIndex="6"></asp:TextBox>
                           
                            <ajaxToolkit:CalendarExtender ID="txtRecDate_CalendarExtender" runat="server" BehaviorID="txtRecDate_CalendarExtender" TargetControlID="txtRecDate" Format="dd/MM/yyyy" />
                           
                        </td>
                </tr>
                         <tr>
                     <td  align="right">
                            Invoice No
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInvNo" runat="server" ></asp:TextBox>
                           
                        </td>
                </tr>
                         <tr>
                     <td  align="right">
                            Invoice Date
                        </td>
                        <td  align="left">
                            <asp:TextBox ID="txtInvDate" runat="server" TabIndex="6"></asp:TextBox>
                           
                            <ajaxToolkit:CalendarExtender ID="txtInvDate_CalendarExtender" runat="server" BehaviorID="txtInvDate_CalendarExtender" TargetControlID="txtInvDate" Format="dd/MM/yyyy" />
                           
                        </td>
                </tr> 
                              <tr>
                                  <td align="right">Remarks </td>
                                  <td align="left" class="style35">
                                      <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                  </td>
                              </tr>

            </table>
            <br />
            <br />
          <asp:Panel ID="Panel4" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black" >
         
            <br />
                        <asp:GridView ID="gvPurRecEdit" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Center" 
                Width="90%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ProductCode" HeaderText="Product Code" >
                        <ItemStyle width="90px"></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" >
                        <ItemStyle width="90px"></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="StoreCode" HeaderText="Store Code" >
                        <ItemStyle width="90px" HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="OrdQty" HeaderText="Order Qty" >
                         <ItemStyle width="90px" HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField >
                        <asp:BoundField DataField="ReceivedQty" HeaderText="Previous Received" >
                        <ItemStyle width="90px" HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField >
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label12" runat="server" Text="Receive QTY"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtRecQTY" runat="server" style="text-align:center;" Text='<%# Eval("RecQty") %>' 
                                Height="20px" Width="95px" AutoPostBack="True" 
                                ontextchanged="txtRecQTY_TextChanged"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label13" runat="server" Text="Recive Price"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrice" runat="server" style="text-align:right;" Text='<%# Eval("PurPrice") %>'
                            Height="20px" Width="95px" AutoPostBack="True" 
                                 BackColor="#66CCFF" ReadOnly="True"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label14" runat="server" Text="Cost"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtCost" runat="server" style="text-align:right;"
                            Height="20px" Width="95px" AutoPostBack="True" 
                                ontextchanged="txtCost_TextChanged" Text='<%# Eval("CostPrice") %>'>
                                0</asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="Label15" runat="server" Text="Total Price"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtTotalPrice" runat="server" style="text-align:right;"
                            Height="20px" Width="95px" Text='<%# Eval("TotalPrice") %>' 
                                BackColor="#66CCFF" ReadOnly="True"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="ckAll" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="ckAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ck" runat="server" AutoPostBack="True" OnCheckedChanged="ck_CheckedChanged" 
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

                        <table>
                <tr>
                    <td >
                       Sub Total
                    </td>
                    <td >
                        <asp:TextBox ID="txtSubTotal" runat="server" BackColor="#66CCFF" 
                            ReadOnly="True" TabIndex="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        Discount</td>
                    <td>
                        <asp:TextBox ID="txtDisParcent" runat="server" AutoPostBack="True" 
                            ontextchanged="txtDisParcent_TextChanged" TabIndex="11" Width="47px">0</asp:TextBox>
                        &nbsp;%
                        &nbsp;<asp:TextBox ID="txtDiscount" runat="server" AutoPostBack="True" 
                            BackColor="#66CCFF" ontextchanged="txtDiscount_TextChanged" ReadOnly="True" 
                            TabIndex="10" Width="57px">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        Deduction</td>
                    <td class="style38">
                        <asp:TextBox ID="txtDeduction" runat="server" AutoPostBack="True" 
                            ontextchanged="txtDeduction_TextChanged" TabIndex="12">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td >
                        Total Qty</td>
                    <td>
                        <asp:TextBox ID="txtTotalQTY" runat="server" BackColor="#66CCFF" 
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Total Amount</td>
                    <td>
                        <asp:TextBox ID="txtTotalAMT" runat="server" BackColor="#66CCFF" 
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
               
                
            </table>
                        </asp:Panel>
                    </asp:Panel>
                    
                </asp:Panel>

               
                    <asp:Panel ID="pnPurchase" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="3px"
                        ScrollBars="Auto">
                        <asp:UpdatePanel ID="upPurchase" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtPurchaseSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPurSearch" runat="server" Text="Search" 
                                                formnovalidate OnClick="btnPurSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="gvPurchase" runat="server" AllowPaging="True" EmptyDataText="No Data To Show"
                                    GridLines="Horizontal" CellPadding="4" ForeColor="#333333" 
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvPurchase_PageIndexChanging" OnSelectedIndexChanged="gvPurchase_SelectedIndexChanged" 
                                    >
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True" />
                                        
                <asp:BoundField DataField="PurOrderNo" HeaderText="PurOrderNo">

                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                                        <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" />
                                        <asp:BoundField DataField="ReqNo" HeaderText="ReqNo">
                    <ItemStyle HorizontalAlign="Center" width="120px" />
                    </asp:BoundField>
                
                <asp:BoundField DataField="PurOrderDesc" HeaderText="PurOrderDesc" />
               
               
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
                                            <asp:Button ID="btnPurOk" runat="server" Text="Ok" Width="60px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPurCancel" runat="server" Text="Cancel" />
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