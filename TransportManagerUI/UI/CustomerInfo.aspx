﻿<%@ Page Title="Customer Info" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="CustomerInfo.aspx.cs" Inherits="TransportManagerUI.UI.CustomerInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "CustomerInfo.aspx/PageLoad",
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
        <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate/>
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate 
                onclick="btnReport_Click"/>


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" formnovalidate/>
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderStyle="Double" 
            BorderWidth="1px">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search"  />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" formnovalidate
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" PageSize="15" 
                     EmptyDataText="No Data To Show" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging"   class="display" 
                     OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged" OnPreRender="gvlistofBasicData_PreRender" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="CustId" HeaderText="CustId" />
                <asp:BoundField DataField="CustName" HeaderText="CustName" />
                <asp:BoundField DataField="CustAddressBang" HeaderText="Address" >
                <ItemStyle Font-Names="SutonnyMJ" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                <asp:BoundField DataField="LocDistance" HeaderText="Distance" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Left" BackColor="#EFF3FB" />
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
            <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel"  />
            </td>
            </tr>
            </table>
                  </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    
     
        
    </asp:Panel>
 
       <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" ForeColor="Black" GroupingText="Customer Info">
        
               <asp:Panel ID="Panel6" runat="server" CssClass="entry-panel">

                   <table cellpadding="3px">
                   <tr>
        <td align="right">
        Customer Id
        </td>
         <td align="left">
             <asp:Label ID="lblCustomerId" runat="server" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
                   <tr>
        <td align="right">
            Dealer
        </td>
         <td align="left">
             <asp:Label ID="lblDealerCode" runat="server" Font-Bold="True" 
                 CssClass="lblInformation1" required ></asp:Label>&nbsp
             <asp:ImageButton ID="btnShowDealer" runat="server" 
                 ImageUrl="~/Images/1488717192_Search.png" OnClick="btnShowDealer_Click" 
                 CssClass="ImageButtonSytle" ToolTip="Search Dealer" formnovalidate />
             &nbsp<asp:Label ID="lblDealerName" runat="server" Font-Bold="True" 
                 CssClass="lblInformation2"></asp:Label>
             <asp:HiddenField ID="hfShowDealer" runat="server" />
             <ajaxToolkit:ModalPopupExtender ID="hfShowDealer_ModalPopupExtender" BackgroundCssClass="modalBackground" runat="server" BehaviorID="hfShowDealer_ModalPopupExtender" CancelControlID="btnDealerCancel" DynamicServicePath="" OkControlID="btnDealerOk" PopupControlID="Panel4" TargetControlID="hfShowDealer">
             </ajaxToolkit:ModalPopupExtender>
        </td>
        </tr>
            
            <tr>
            <td align="right">
                &nbsp;Name
            </td>
             <td align="left">
                 <asp:TextBox ID="txtCustomerName" runat="server" Width="306px" required ></asp:TextBox>
                 
            </td>
            </tr>
            <tr>
            <td align="right" >
                Name (Bangla)
            </td>
             <td align="left">
                 <asp:TextBox ID="txtCustomerNameBangla" runat="server" Width="306px" 
                     Font-Names="SutonnyMJ" ></asp:TextBox>
                 
            </td>
            </tr>
            
            <tr>
            <td align="right">
                Address (Bangla)
            </td>
             <td align="left">
                <asp:TextBox ID="txtAddressBangla" runat="server" TextMode="MultiLine" 
                     Width="306px" Font-Names="SutonnyMJ" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            
                <td align="right">
                    Address1
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtAddres1" runat="server" Width="306px"></asp:TextBox>
                     
                </td>
                </tr>
            <tr>
                <td align="right">Address2
            </td>
            <td align="left">
                <asp:TextBox ID="txtAddress2" runat="server" Width="306px" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            
                <td align="right">
                    Address3
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtAddress3" runat="server" Width="306px"></asp:TextBox>
                     
                </td>
                
            </tr>
                       <tr>
            
                <td align="right">
                    Location
                </td>
                 <td align="left">
                     <asp:DropDownList ID="ddlLocation" runat="server" Width="200px"></asp:DropDownList>
                     
                </td>
                
            </tr>
                       <tr>
                <td align="right" >Distance
            </td>
            <td align="left">
                <asp:TextBox ID="txtLocDistance" runat="server" required >0</asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="right" >Contact Person  
            </td>
            <td align="left">
                <asp:TextBox ID="txtContactPerson" runat="server" Width="306px" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            
                <td align="right">
                   Sales Person Phone
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                     
                </td>
                </tr>
            <tr>
                <td align="right" >Mobile 
            </td>
            <td align="left">
                <asp:TextBox ID="txtMobile" runat="server" ></asp:TextBox>
            </td>
            </tr>

          
            <tr>
            
                <td align="right">
                    Status
                </td>
                 <td align="left">
                     <asp:DropDownList ID="ddlStatus" runat="server">
                     <asp:ListItem Value="0" Text="Active"></asp:ListItem>
                     <asp:ListItem Value="1" Text="InActive"></asp:ListItem>
                     </asp:DropDownList>
                     
                </td>
                </tr>
            
            
        </table>
               </asp:Panel>
          <%-- Dealer Panel Start--%>
            <asp:Panel ID="Panel4" runat="server" BorderStyle="Dotted" BorderWidth="2px" 
                   BackColor="White" ScrollBars="Auto" GroupingText="Select Dealer">
                  <asp:UpdatePanel ID="upDearler" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <table>
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtDealerSearch" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnDearlerSearch" runat="server" Text="Search" 
                                    CssClass="Button" onclick="btnDearlerSearch_Click" formnovalidate 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvDealer" runat="server" AllowPaging="True" CellPadding="4" 
                ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
                EmptyDataText="No Data To Show" 
                onpageindexchanging="gvDealer_PageIndexChanging" OnSelectedIndexChanged="gvDealer_SelectedIndexChanged">
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
                <asp:Button ID="btnDealerOk" runat="server" Text="Ok" Width="60px" />
            </td>
            <td>
            <asp:Button ID="btnDealerCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
                   </ContentTemplate>
<Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnDearlerSearch" />
           
            </Triggers>
</asp:UpdatePanel>
                  </asp:Panel>
        
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

          <%--    $(document).ready(function () {
                                                         
                                                      $('#' + '<%= ddlLocation.ClientID %>').select2();
                                                      Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                                                      function EndRequestHandler(sender, args) {
                                                          $('#' + '<%= ddlLocation.ClientID %>').select2();
                      
                    }
                   });--%>
          </script>
   </asp:Panel>

</asp:Content>
