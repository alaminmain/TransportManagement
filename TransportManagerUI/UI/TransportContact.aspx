<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TransportContact.aspx.cs" Inherits="TransportManagerUI.UI.TransportContact" %>
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


   
     
  </script>
    <style type="text/css">
       

       
        .auto-style1 {
            height: 35px;
        }
       

       
        .auto-style2 {
            text-align: left;
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
        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate   />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate   />
   <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" onclick="btnReport_Click" 
                 />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"   />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" BackgroundCssClass="modalBackground" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel6"  CancelControlID="btnSearchCancel">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
        <br />
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
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="TCNo" HeaderText="TCNo" />
                <asp:BoundField DataField="TCDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="TCDate" />
                <asp:BoundField DataField="CustName" HeaderText="DealerName" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
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
    
<asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" BackColor="#CCCCCC" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Transport Contact">
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
                    TargetControlID="txtTCDate" Format="dd/MMM/yyyy" /> Time:
                <asp:TextBox ID="txtTCTime" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="txtTCTime_MaskedEditExtender" runat="server" 
                     Mask="99:99:99"
             MessageValidatorTip="true"
             OnFocusCssClass="MaskedEditFocus"
             OnInvalidCssClass="MaskedEditError"
             MaskType="Time"
             AcceptAMPM="True"
            ErrorTooltipEnabled="True" TargetControlID="txtTCTime" />
            
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" 
                    ControlExtender="txtTCTime_MaskedEditExtender" ControlToValidate="txtTCTime"></ajaxToolkit:MaskedEditValidator>
            
        </td>
        </tr>
            
        <tr valign="middle">
        <td align="right" nowrap="nowrap" class="auto-style1">
            Dealer
        </td>
            <td align="left"  colspan="2" class="auto-style1" nowrap="nowrap">
                <asp:Label ID="lblDealerCode" runat="server" Font-Bold="True" 
                    CssClass="lblInformation1" required></asp:Label>*
               
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
        <td align="right" valign="top" class="auto-style1">Customer</td>
                <td valign="middle" class="auto-style2" >
                     
                    <asp:Label ID="lblCustomerCode" runat="server" 
                        CssClass="lblInformation1" required></asp:Label>*&nbsp;
                     
                    <asp:ImageButton ID="btnSelectCustomer" runat="server" 
                        ImageUrl="~/Images/1488717192_Search.png" OnClick="btnSelectCustomer_Click" 
                        CssClass="ImageButtonSytle"/>
                      
                        &nbsp;<asp:Label ID="lblCustomerName" runat="server" 
                        CssClass="lblInformation2"></asp:Label>
                     

                    <br />
                    <br />
                    Location:<asp:Label ID="lblLocation" runat="server" BackColor="White" BorderColor="#33CCCC" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#FF3300" Visible="True"></asp:Label>
                   <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Distance (KM):--%>
                    Rent: <asp:Label ID="lblRent" runat="server" BackColor="White" BorderColor="#33CCCC" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#FF3300" Width="100px" Visible="True"></asp:Label>
                     

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
            <asp:ListItem Value="2" Text="On Trip"></asp:ListItem>
            <asp:ListItem Value="3" Text="Cancel"></asp:ListItem>
                
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
                <asp:HiddenField ID="hfState" runat="server" />
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
            CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
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
            <asp:BoundField DataField="InvDate" DataFormatString="{0:dd/MMM/yyyy}" 
                HeaderText="Date" />
            <asp:BoundField DataField="CustId" HeaderText="Dealer Id" />
            <asp:BoundField DataField="CustName" HeaderText="Dealer Name" >
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
            <asp:BoundField DataField="TotalQty" HeaderText="TotalQty" />
            <asp:BoundField DataField="BalQty" DataFormatString="{0:0}" HeaderText="BalQty" />
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
            ForeColor="#333333" GridLines="Horizontal" 
            OnPageIndexChanging="gvDealerSearch_PageIndexChanging" 
            OnSelectedIndexChanged="gvDealerSearch_SelectedIndexChanged" 
            AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
                
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="CustId" HeaderText="Id" />
            <asp:BoundField DataField="CustName" HeaderText="Name" />
            <asp:BoundField DataField="CustAddressBang" HeaderText="Address" >
            <ItemStyle Font-Names="SutonnyMJ" />
            </asp:BoundField>
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
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
            <td><asp:TextBox ID="txtSearchCustomer" runat="server" placeholder="Search"></asp:TextBox>
               
            </td>
            <td><asp:Button ID="btnCustomerSearch" runat="server" Text="Search" onclick="btnCustomerSearch_Click" 
                    /></td>
        </tr>
         
        </table>
        
            <asp:GridView ID="gvCustomer" runat="server" AllowPaging="True" 
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
            OnPageIndexChanging="gvCustomer_PageIndexChanging" 
            OnSelectedIndexChanged="gvCustomer_SelectedIndexChanged" AutoGenerateColumns="False" 
                    >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                    <asp:BoundField DataField="CustId" HeaderText="CustId" />
            <asp:BoundField DataField="CustName" HeaderText="CustName" />
            <asp:BoundField DataField="CustAddressBang" HeaderText="Address" >
                    <ItemStyle Font-Names="SutonnyMJ" />
                    </asp:BoundField>
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                    <asp:BoundField DataField="LocDistance" HeaderText="Distance" />
                    <asp:BoundField DataField="LocName" HeaderText="Location" />
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

            &nbsp;<asp:GridView ID="gvListofDOProduct" runat="server" CellPadding="4" 
                ForeColor="#333333" GridLines="Horizontal" ShowFooter="True" 
                AutoGenerateColumns="False"  
                OnRowCancelingEdit="gvListofDOProduct_RowCancelingEdit" 
                OnRowEditing="gvListofDOProduct_RowEditing" 
                OnRowDeleting="gvListofDOProduct_RowDeleting" 
                OnRowUpdating="gvListofDOProduct_RowUpdating" >
                 
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField DataField="InvNo" HeaderText="DoNo" ReadOnly="True" />
                    <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" ReadOnly="True" >
                    
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" 
                        ReadOnly="True" ItemStyle-Width="200px">
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="DOQty">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DOQty") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDoQty" runat="server" Text='<%# Bind("DOQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OrderQty">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OrderQty","{0:0}") %>'  onchange="calculate()" ></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate >
                            <asp:TextBox ID="txtTotalQty" runat="server" style="text-align: right" ></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" 
                                Text='<%# Bind("OrderQty", "{0:0}") %>' style="text-align: right" onchange="calculate()" class="calculate" ></asp:TextBox>
                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rent">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRent" runat="server" 
                                Text='<%# Bind("Rent", "{0:0.00}")  %>' style="text-align: right"></asp:TextBox>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" 
                                Text='<%# Eval("Rent", "{0:0.00}") %>'></asp:Label>
                        </EditItemTemplate>
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

    <script type="text/javascript">
        function CheckNumber() {

            var gv = document.getElementById("cphMainContent_gvlistofBasicData");
            alert(gv);
            var rowCount = $("cphMainContent_gvlistofBasicData tr").length - 1; //minus the header tand footer row
            var prefixID = "cphMainContent_gvListofDOProduct"; // view source to get the actual prefixed id
            var tbID = "_Text1"; //id of TextBox to compare
            var lblID = "_lblDoQty"; //id of Label to compare


            var theLabel;
            var theTextBox;
            //set the value of i to 2 as starting index since the first row id shows something like this
            //GridView1_ctl02_Label1
            for (var i = 2; i < rowCount - 1; i++) {
                alert('2');

                theLabel = document.getElementById(prefixID + lblID + i);
                theTextBox = document.getElementById(prefixID + tbID + i);


                if (parseInt(theLabel.innerHTML) < parseInt(theTextBox.value)) {
                    alert("Label value is less than or equal to the value of TextBox");
                    theTextBox.focus();
                }
                else
                    calculate();

            }
        }

        function calculate() {
            var txtTotal = 0.00;
            //var passed = false;
            //var id = 0;
            //if(FindTextBoxValueInGrid==

            $(".calculate").each(function (index, value) {
                var val = value.value;

                val = val.replace(",", ".");
                txtTotal = MathRound(parseFloat(txtTotal) + parseFloat(val));

            });


            document.getElementById("cphMainContent_gvListofDOProduct_txtTotalQty").value = txtTotal.toFixed(2);

            //CalculateNetIncome();
        }

        function MathRound(number) {
            return Math.round(number * 100) / 100;
        }

    </script>
        </asp:Panel>
    
</asp:Content>
