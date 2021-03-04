<%@ Page Title="Dealer Info" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="DealerInfo.aspx.cs" Inherits="TransportManagerUI.UI.DealerInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "DealerInfo.aspx/PageLoad",
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
     

    
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    
    <asp:Panel ID="Panel5" runat="server">
        <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" ScrollBars="Auto">
        <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate/>
   <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" formnovalidate/>
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground" >
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderColor="Black" 
            BorderStyle="Double" BorderWidth="1px">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" formnovalidate
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
   
       <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Dealer Info" CssClass="entry-panel">
        
               <asp:Panel ID="Panel6" runat="server">

                   <table cellpadding="3px">
                   <tr>
        <td align="right">
        Dealer Id
        </td>
         <td align="left">
             <asp:Label ID="lblCustomerId" runat="server" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
                   
            
            <tr>
            <td align="right">
            Name
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
                <asp:TextBox ID="txtAddressBangla" runat="server" Width="306px" 
                     Font-Names="SutonnyMJ" ></asp:TextBox>
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
                <td align="right" >Address2
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
                <td align="right" >Distance
            </td>
            <td align="left">
                <asp:TextBox ID="txtLocDistance" runat="server" required>0</asp:TextBox>
            </td>
            </tr>
                       <tr>
            
                <td align="right">
                    Location
                </td>
                 <td align="left">
                     
                     <asp:DropDownList ID="ddlLocation" runat="server"></asp:DropDownList>
                    
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
                     <asp:ListItem Value="1" Text="Active"></asp:ListItem>
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
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMembership" >
            <ProgressTemplate>
            <div class="UpdateProgress">
                <img src="../Images/ajax-loader.gif" alt="Processing..."/>
            </div>
            </ProgressTemplate>
    </asp:UpdateProgress>

         <script type="text/javascript">

<%--              $(document).ready(function () {
                                                         
                    $('#' + '<%= ddlLocation.ClientID %>').select2({
                        width: 'resolve'
                    });
                                                      Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                                                      function EndRequestHandler(sender, args) {
                                                          $('#' + '<%= ddlLocation.ClientID %>').select2({
                                                              width: 'resolve'
                                                          });
                      
                    }
                   });--%>
          </script>
   </asp:Panel>

</asp:Content>
