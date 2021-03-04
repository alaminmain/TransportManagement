<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="DO.aspx.cs" Inherits="TransportManagerUI.UI.DO" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "DO.aspx/PageLoad",
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
            
height: 20px;color: Blue;
width:306px;
            border-width:1px;
            border-style: solid;
           
            padding-left:2px;
            padding-right:2px;
            padding-top:0px;
            padding-bottom:0px;
           
        }
       
        </style>
    
</asp:Content>

 <asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
     <asp:Panel ID="PlToolBox" runat="server" ToolTip="ToolBox">
       
    <asp:UpdatePanel  ID="uplMembership" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" formnovalidate   />
        <asp:Button ID="btnShowList" runat="server" Text="List" 
                onclick="btnShowList_Click" formnovalidate   />
   <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
        <br />
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
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     onselectedindexchanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="InvNo" HeaderText="DO No" />
                <asp:BoundField DataField="InvDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="Date" />
                <asp:BoundField DataField="CustId" HeaderText="Dealer Id" />
                <asp:BoundField DataField="CustName" HeaderText="Dealer Name" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalQty" DataFormatString="{0:G}" HeaderText="TotalQty" />
                <asp:BoundField DataField="InvAmount" HeaderText="Amount" 
                    DataFormatString="{0:N}" >
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
                <asp:Button ID="btnSearchOk" runat="server" Text="Ok" Width="60px" OnClick="btnSearchOk_Click" style="height: 26px" />
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

        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="DO" BackColor="#CCCCCC">
        <table cellpadding="2PX" cellspacing="1">
                <tr>
        <td  align="right">
            DO No
        </td>
         <td colspan="3"  align="left">
             <asp:Label ID="lblInvoiceNo" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
       
        </td>
        </tr>
        <tr>
            <td align="right">
                SAP DO No</td>
             <td align="left" colspan="3">
                 <asp:TextBox ID="txtManualDoNo" runat="server" Width="120px" ></asp:TextBox>
                
            </td>
            
            </tr>
            <tr>
            <td align="right">
                DO Date</td>
             <td align="left" colspan="3">
                 <asp:TextBox ID="lblInvoiceDate" runat="server" Width="120px" ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="lblInvoiceDate_CalendarExtender" 
                     runat="server" BehaviorID="lblInvoiceDate_CalendarExtender" 
                     TargetControlID="lblInvoiceDate" Format="dd/MMM/yyyy" ClearTime="True" />
            </td>
            
            </tr>
          
            <tr>
                <td align="right" valign="middle">Dealer</td>
            <td colspan="3" align="left" valign="middle">
        
                  <asp:Label ID="lblCustomerCode" runat="server" 
                    CssClass="auto-style3" Height="20px" Width="80px" formnovalidate  ></asp:Label>*
                  &nbsp;  <asp:ImageButton ID="btnCustomer" runat="server" ImageUrl="~/Images/1488717192_Search.png" 
                    onclick="btnCustomer_Click" CssClass="ImageButtonSytle" />
                &nbsp;<asp:Label ID="lblCustomerName" runat="server" CssClass="auto-style3" ></asp:Label>
                        
                    
                     
                  
               
            </td>
            </tr>
            <tr>
                <td  align="right">
            Delivery Mode
            </td>
             <td align="left" colspan="3">
                <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" 
                     Height="24px" Width="150px">
                <asp:ListItem Value="1" Text="CNF"></asp:ListItem>
                <asp:ListItem Value="2" Text="FOB"></asp:ListItem>
                 </asp:DropDownList>
            </td>
            </tr>
             <tr>
                <td  align="right">
            Ghat
            </td>
             <td align="left" colspan="3">
                <asp:DropDownList ID="ddlGhatList" runat="server" AutoPostBack="True" 
                     Height="24px" Width="150px">
               

                 </asp:DropDownList>
               
            </td>
            </tr>
            <tr>
            <td  align="right">Remarks
            </td>
            <td align="left"  colspan="3">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                <asp:HiddenField ID="hidCustomerData" runat="server" />
                <ajaxToolkit:ModalPopupExtender ID="hidCustomerData_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" BehaviorID="ImageButton2_ModalPopupExtender" CancelControlID="btnCustomerCancel" DynamicServicePath="" OkControlID="btnOk" PopupControlID="Panel6" TargetControlID="hidCustomerData">
                </ajaxToolkit:ModalPopupExtender>
            </td>
            
            </tr>
            <tr>
                <td  align="right">
                    Status
            </td>
             <td align="left" colspan="3">
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" Height="24px" Width="141px">
               <asp:ListItem Value="0" Text="Open"></asp:ListItem>
               <asp:ListItem Value="1" Text="Delivered"></asp:ListItem>
               <asp:ListItem Value="2" Text="Cancel"></asp:ListItem>
                  <asp:ListItem Value="3" Text="Close"></asp:ListItem>

                 </asp:DropDownList>
               
            </td>
            </tr>
            
        </table>
        </asp:Panel>
            
        <asp:Panel ID="Panel3" runat="server" BorderWidth="1" BorderColor="#FFFFCC" 
        BackColor="#CCCCCC" ForeColor="Black">
            <table cellpadding="5px" >
            <tr>
            <th>
            Product Name
            </th>
            <th>
            Quantity
            </th>
            <th>
            Unit Price
            </th>
            </tr>
            <tr style="border-style: groove">
            <td>
          
                <asp:DropDownList ID="ddlproductName" runat="server" AutoPostBack="True" Font-Size="14px" OnSelectedIndexChanged="ddlproductName_SelectedIndexChanged" Height="24px">
              
                </asp:DropDownList>
              
                </td>
                 <td class="auto-style9">
                    <asp:TextBox ID="txtQuantity" runat="server" Height="18px" Width="60px"></asp:TextBox>*
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtQuantity_FilteredTextBoxExtender" 
                        runat="server" BehaviorID="txtQuantity_FilteredTextBoxExtender" 
                        TargetControlID="txtQuantity" ValidChars="0123456789." />
                </td>
                <td class="auto-style9">
                    <asp:TextBox ID="txtRate" runat="server" Height="18px" Width="72px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtRate_FilteredTextBoxExtender" 
                        runat="server" BehaviorID="txtRate_FilteredTextBoxExtender" 
                        TargetControlID="txtRate" ValidChars="0123456789." />
                </td>
               
                <td class="auto-style9">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
            <td colspan="4">
                &nbsp;<asp:GridView ID="grdListOfProduct" runat="server" AutoGenerateColumns="False" 
                    onrowdeleting="grdListOfProduct_RowDeleting" CellPadding="4" 
                    ForeColor="#333333" GridLines="Horizontal" 
                    OnRowDataBound="grdListOfProduct_RowDataBound" 
                    EmptyDataText="No Data To Show" ShowFooter="True" >
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:CommandField ShowDeleteButton="True" />
                     <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" />
                     <asp:BoundField DataField="ProductName" HeaderText="ProductName" 
                         ReadOnly="True" SortExpression="ProductName" >
                     <ItemStyle HorizontalAlign="Left" Width="200px" />
                     </asp:BoundField>
                     <asp:TemplateField HeaderText="Quantity">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label1" runat="server" Text='<%# Bind("Quantity", "{0:F2}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="UnitPrice" DataFormatString="{0:F}" HeaderText="UnitPrice">
                     <ItemStyle HorizontalAlign="Right" />
                     </asp:BoundField>
                     <asp:TemplateField HeaderText="TotalPrice">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TotalPrice") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label2" runat="server" Text='<%# Bind("TotalPrice", "{0:F2}") %>'></asp:Label>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Right" />
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
                <br />
            </td>
    
            </tr>
            
            <tr>
                    <td>
                    
 <asp:Panel ID="Panel6" runat="server" ForeColor="Black" BorderStyle="Outset" 
                            BorderWidth="3px" BorderColor="Silver" GroupingText="Search Dealer" 
                            ScrollBars="Auto" BackColor="White">
     <asp:UpdatePanel ID="upCustomer" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
     
     <table>
         <tr>
             <td><asp:TextBox ID="txtSearchCustomer" runat="server"></asp:TextBox>
                 <ajaxToolkit:TextBoxWatermarkExtender ID="txtSearchCustomer_TextBoxWatermarkExtender" runat="server" BehaviorID="txtSearchCustomer_TextBoxWatermarkExtender" TargetControlID="txtSearchCustomer" WatermarkText="Search"  />
             </td>
              <td><asp:Button ID="btnCustomerSearch" runat="server" Text="Search" 
                      onclick="btnCustomerSearch_Click" /></td>
         </tr>
         <tr>
             <td colspan="2">
             <asp:GridView ID="gvCustomer" runat="server" AllowPaging="True" 
                     onpageindexchanging="gvCustomer_PageIndexChanging" 
                     onselectedindexchanged="gvCustomer_SelectedIndexChanged" CellPadding="4" 
                     ForeColor="#333333" GridLines="Horizontal" AutoGenerateColumns="False" 
                     >
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                      <asp:BoundField DataField="CustId" HeaderText="Id" />
                        <asp:BoundField DataField="CustName" HeaderText="Dealer Name" >
                        <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                        <asp:BoundField DataField="CustAddressBang" HeaderText="Address" >
                        <ItemStyle HorizontalAlign="Left" Font-Names="SutonnyMJ" />
                     </asp:BoundField>
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" >

                     <ItemStyle HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="LocDistance" HeaderText="Distance" />

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
                 </asp:GridView></td>

         </tr>
         <tr>
             <td><asp:Button ID="btnOk" runat="server" Text="Ok" /></td>
             <td>
                 <asp:Button ID="btnCustomerCancel" runat="server" Text="Cancel" />
             </td>
         </tr>
         
         
     </table>
    </ContentTemplate>
     <Triggers>
     <asp:AsyncPostBackTrigger ControlID="btnCustomerSearch"/>
     </Triggers>

     </asp:UpdatePanel>
                    </asp:Panel>
                     
     
                    </td>
                </tr>
            
            
            </table>
               
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

</asp:Content>
