<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TransportContact123.aspx.cs" Inherits="TransportManagerUI.UI.TransportContact" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "TransportContact.aspx/PageLoad",
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
       

       
        .auto-style1 {
            height: 35px;
        }
       

       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel8" runat="server">
<asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
    
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" />
   <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" onclick="btnReport_Click" 
                 />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" BackgroundCssClass="modalBackground" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel6"  CancelControlID="btnSearchCancel">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel6" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="2px" >
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" 
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" 
                     EmptyDataText="No Data To Show" GridLines="None" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="TCNo" HeaderText="TCNo" />
                <asp:BoundField DataField="TCDate" DataFormatString="{0:d}" 
                    HeaderText="TCDate" />
                <asp:BoundField DataField="CustName" HeaderText="CustName" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
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
    
  <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" BackColor="#CCCCCC" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Transport Contact">
        <table cellpadding="3px" cellspacing="1">
            <tr>
        <td align="right" >
        Tc No
        </td>
         <td align="left">
             <asp:Label ID="lblTcNo" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
            <tr>
            <td align="right" >
            TC Date</td>
             <td align="left" class="auto-style2">
                 <asp:TextBox ID="txtTCDate" runat="server" Width="120px" ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="txtTCDate_CalendarExtender" 
                     runat="server" BehaviorID="txtTCDate_CalendarExtender" 
                     TargetControlID="txtTCDate" Format="dd/MMM/yyyy" />
            </td>
            </tr>
            
            <tr valign="middle">
            <td align="right" nowrap="nowrap">
                Dealer&nbsp;
            </td>
             <td align="left"  colspan="2" class="auto-style1" nowrap="nowrap">
                 <asp:Label ID="lblDealerCode" runat="server" Font-Bold="True" 
                     CssClass="lblInformation1"></asp:Label>
               
              &nbsp;<asp:ImageButton ID="btnDealerShow" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png" OnClick="btnDealerShow_Click" 
                     CssClass="ImageButtonSytle" />&nbsp;
                     <asp:Label ID="lblDealerName" runat="server" Font-Bold="True" 
                     CssClass="lblInformation2"></asp:Label>
                    &nbsp;
                
                    

                 <br />
            </td>
           
            </tr>
            <tr valign="middle">
            <td align="right" valign="middle" class="auto-style1">Customer</td>
                 <td align="left" valign="middle" class="auto-style1" >
                     
                     <asp:Label ID="lblCustomerCode" runat="server" 
                         CssClass="lblInformation1"></asp:Label>&nbsp;
                     
                     <asp:ImageButton ID="btnSelectCustomer" runat="server" 
                         ImageUrl="~/Images/1488717192_Search.png" OnClick="btnSelectCustomer_Click" 
                         CssClass="ImageButtonSytle"/>
                      
                         &nbsp;<asp:Label ID="lblCustomerName" runat="server" 
                         CssClass="lblInformation2"></asp:Label>
                     

                 </td>
                  
                     
                
               
            </tr>
            <tr>
                <td align="right">
                 Payment Mode
                </td><td align="left" class="auto-style2">
                <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" Width="173px" Height="24px">
                <asp:ListItem Value="1" Text="CNF"></asp:ListItem>
                <asp:ListItem Value="2" Text="FOB"></asp:ListItem>
                 </asp:DropDownList>
            </td>
            </tr>
           
              <tr>
                <td align="right">
                 Ghat
                </td><td align="left" class="auto-style2">
                <asp:DropDownList ID="ddlGhatList" runat="server" AutoPostBack="True" Height="24px" Width="173px">
             
                 </asp:DropDownList>
            </td>
            </tr>

            <tr>
            <td align="right" class="auto-style5" >Remarks
            </td>
            <td align="left" class="auto-style2">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="268px"></asp:TextBox>
            </td>
          
            </tr>
             <tr>
                   <td>Status</td>
            <td align="left" >
             <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True">
                <asp:ListItem Value="0" Text="Open"></asp:ListItem>
               <asp:ListItem Value="1" Text="Confirm"></asp:ListItem>
               <asp:ListItem Value="2" Text="Cancel"></asp:ListItem>
                  <asp:ListItem Value="3" Text="Close"></asp:ListItem>
                 </asp:DropDownList>
            </td>
              </tr> 
            <tr> 
                <td colspan="" align="left" class="auto-style5" >
                <asp:Button ID="btnSearchOrder" runat="server" BackColor="#0099FF" ForeColor="Black" onclick="btnSearchOrder_Click" Text="Select DO" />
                </td>
                <td>
                    <asp:HiddenField ID="hidSearchdDealer" runat="server" />
                    <ajaxToolkit:ModalPopupExtender ID="hidSearchdDealer_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" BehaviorID="hidSearchdDealer_ModalPopupExtender" CancelControlID="btnDealerCancel" DynamicServicePath="" OkControlID="btnDealerOk" PopupControlID="Panel7" TargetControlID="hidSearchdDealer">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:HiddenField ID="hidSearchdCustomer" runat="server" />
                    <ajaxToolkit:ModalPopupExtender ID="hidSearchdCustomer_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" BehaviorID="hidSearchdDealer0_ModalPopupExtender" CancelControlID="btnCustomerCancel" DynamicServicePath="" OkControlID="btnOk" PopupControlID="Panel9" TargetControlID="hidSearchdCustomer">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:HiddenField ID="hidSelectDO" runat="server" />
                    <ajaxToolkit:ModalPopupExtender ID="hidSelectDO_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" BehaviorID="hlSearchDo_ModalPopupExtender" CancelControlID="btnDOCancel" DynamicServicePath=""  PopupControlID="Panel5" TargetControlID="hidSelectDO">
                    </ajaxToolkit:ModalPopupExtender>
                </td>
                
                
            </tr>
            
        </table>
        </asp:Panel>
        
    <asp:Panel ID="Panel5" runat="server" GroupingText="Search DO" BackColor="White" BorderWidth="2px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table style="width: 255px" >
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtSearchDo" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnSearchDo" runat="server" Text="Search" CssClass="Button" OnClick="btnSearchDo_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvListofDO" runat="server" AllowPaging="True" 
              CellPadding="4" ForeColor="#333333" GridLines="None" 
                AutoGenerateColumns="False" onpageindexchanging="gvListofDO_PageIndexChanging" 
               >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <EditItemTemplate>
                        <asp:CheckBox ID="cbSelect" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="InvNo" HeaderText="DO No" />
                <asp:BoundField DataField="InvDate" DataFormatString="{0:d}" 
                    HeaderText="Date" />
                <asp:BoundField DataField="CustId" HeaderText="Dealer Id" />
                <asp:BoundField DataField="CustName" HeaderText="Dealer Name" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="InvAmount" HeaderText="Amount" 
                    DataFormatString="{0:F}" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
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
                <asp:Button ID="btnDoOk" runat="server" Text="Ok" onclick="btnDoOk_Click" />
            </td>
            <td>
            <asp:Button ID="btnDOCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
        </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSearchDo" />
           
            </Triggers>
        </asp:UpdatePanel>
            </asp:Panel>
       

    
    <asp:Panel ID="Panel7" runat="server" GroupingText="Search Dealer" BackColor="White" BorderWidth="2px">
         <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table style="width: 255px" >
                            <tr>
                            <td class="auto-style3">
                                <asp:TextBox ID="txtDealerSearch" runat="server" Width="158px" />
                            </td>
                            <td class="auto-style4">
                                <asp:Button ID="btnDealerSearch" runat="server" Text="Search" CssClass="Button" onclick="btnDealerSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvDealerSearch" runat="server" AllowPaging="True" CellPadding="4" 
                ForeColor="#333333" GridLines="None" 
                OnPageIndexChanging="gvDealerSearch_PageIndexChanging" 
                OnSelectedIndexChanged="gvDealerSearch_SelectedIndexChanged" 
                AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="CustId" HeaderText="Id" />
                <asp:BoundField DataField="CustName" HeaderText="Name" />
                <asp:BoundField DataField="CustAddressBang" HeaderText="Address" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
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
                <asp:Button ID="btnDealerOk" runat="server" Text="Ok" />
            </td>
            <td>
            <asp:Button ID="btnDealerCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
            </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
   
           
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
     <asp:Panel ID="Panel9" runat="server" 
                             GroupingText="Search Customer" 
                           BackColor="White">
     <asp:UpdatePanel ID="upCustomer" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
     
     <table>
         <tr>
             <td><asp:TextBox ID="txtSearchCustomer" runat="server"></asp:TextBox>
                 <ajaxToolkit:TextBoxWatermarkExtender ID="txtSearchCustomer_TextBoxWatermarkExtender" runat="server" BehaviorID="txtSearchCustomer_TextBoxWatermarkExtender" TargetControlID="txtSearchCustomer" WatermarkText="Search" />
             </td>
              <td><asp:Button ID="btnCustomerSearch" runat="server" Text="Search" onclick="btnCustomerSearch_Click" 
                      /></td>
         </tr>
         
         </table>
        
             <asp:GridView ID="gvCustomer" runat="server" AllowPaging="True" 
                      CellPadding="4" ForeColor="#333333" GridLines="None" 
             OnPageIndexChanging="gvCustomer_PageIndexChanging" 
             OnSelectedIndexChanged="gvCustomer_SelectedIndexChanged" AutoGenerateColumns="False" 
                     >
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                      <asp:BoundField DataField="CustId" HeaderText="CustId" />
                <asp:BoundField DataField="CustName" HeaderText="CustName" />
                <asp:BoundField DataField="CustAddressBang" HeaderText="Address" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
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
                 </asp:GridView></td>

        
         <table>
         <tr>
             <td><asp:Button ID="btnOk" runat="server" Text="Ok" /> </td>
             <td>
                <asp:Button ID="btnCustomerCancel" runat="server" Text="Cancel" />
             </td>
         </tr>
         
         
     </table>
    </ContentTemplate>
     <Triggers>
     <asp:AsyncPostBackTrigger ControlID="btnCustomerSearch"/>
     </Triggers></asp:UpdatePanel>
                    </asp:Panel>


           <asp:Panel ID="Panel4" runat="server" CssClass="entry-panel">
            <%--<asp:UpdatePanel ID="upProductList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                     <table>
                   
            <tr>

            <td colspan="3">

                &nbsp;<asp:GridView ID="gvListofDOProduct" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" AutoGenerateColumns="False" OnRowCommand="gvListofDOProduct_RowCommand" OnRowCancelingEdit="gvListofDOProduct_RowCancelingEdit" OnRowEditing="gvListofDOProduct_RowEditing" OnRowDeleting="gvListofDOProduct_RowDeleting" OnRowUpdating="gvListofDOProduct_RowUpdating" >
                 
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:CommandField ShowDeleteButton="True" />
                     <asp:BoundField DataField="InvNo" HeaderText="Do No" ReadOnly="True" />
                     <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" ReadOnly="True" />
                     <asp:BoundField DataField="ProductName" HeaderText="ProductName" 
                         ReadOnly="True" >
                     <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:TemplateField HeaderText="OrderQty">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OrderQty") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <FooterTemplate>
                             <asp:Label ID="lblTotalqty" runat="server" class="totalQty"></asp:Label>
                         </FooterTemplate>
                         <ItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OrderQty") %>' onchange='calculate()' class="calculate"></asp:TextBox>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" ReadOnly="True" 
                         DataFormatString="{0:f}" >
                     <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
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

                
            
           
            </td>
               
                
            </tr>
                                </table>

                <%--</ContentTemplate>
                <Triggers>
    <asp:AsyncPostBackTrigger ControlID="gvListofDOProduct" EventName="RowCommand" />
</Triggers>
            </asp:UpdatePanel>--%>
                          </asp:Panel>
     
   
</ContentTemplate>
<Triggers>
 <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMembership" >
            <ProgressTemplate>
            <div class="UpdateProgress">
                <img src="../Images/ajax-loader.gif" alt="Processing..."/>
            </div>
            </ProgressTemplate>
    </asp:UpdateProgress>
        </asp:Panel>
        <script type="text/javascript">
            function calculate() {
                var lblTotal = 0.00;
                //var passed = false;
                //var id = 0;

                $(".calculate").each(function (index, value) {
                    var val = value.value;

                    val = val.replace(",", ".");
                    txtTotal = MathRound(parseFloat(txtTotal) + parseFloat(val));

                });


                document.getElementById("cphMainContent_gvListofDOProduct_lblTotalqty").value = txtTotal.toFixed(2);
                CalculateNetIncome();
            }

            function MathRound(number) {
                return Math.round(number * 100) / 100;
            }
        </script>
</asp:Content>
