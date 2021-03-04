<%@ Page Title="Movement Info" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="MovmentInfo.aspx.cs" Inherits="TransportManagerUI.UI.MovmentInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "MovmentInfo.aspx/PageLoad",
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
   
    <asp:Panel ID="Panel3" runat="server">
         <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" formnovalidate  />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate  />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />               
     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel8" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel8" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="1px" >
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
                     OnPageIndexChanging="gvlistofBasicData_PageIndexChanging" 
                     OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="MovementNo" HeaderText="MovementNo" />
                <asp:BoundField DataField="TransportDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="TransportDate" />
                <asp:BoundField DataField="TripNo" HeaderText="TripNo" />
                <asp:BoundField DataField="TCNo" HeaderText="TCNo" />
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
             <Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnSearch" />
           
            </Triggers>
    </asp:UpdatePanel>
    </asp:Panel>
    
     
        
    </asp:Panel>

     <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" CssClass="entry-panel"
            GroupingText="Movement Info" ForeColor="Black">
         <table>
             <tr>
                 <td valign="top">
       <table cellpadding="3px" cellspacing="1" align="left" >
            <tr>
        <td align="right">
            Mov. No
        </td>
         <td align="left">
             <asp:Label ID="lblMovement" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
            <tr>
            <td align="right">
                Date
            </td>
             <td align="left" >
                 <asp:TextBox ID="txtMovmentDate" runat="server" Width="120px" ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="txtMovmentDate_CalendarExtender" 
                     runat="server" BehaviorID="txtMovmentDate_CalendarExtender" 
                     TargetControlID="txtMovmentDate" Format="dd/MMM/yyyy" />
            </td>

            </tr>
             <tr>
                <td align="right">
                 Ghat
                </td><td align="left" >
                <asp:DropDownList ID="ddlGhatList" runat="server" AutoPostBack="True" Height="24px" Width="173px">
             
                 </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td align="right">
                Trip No
            </td>
             <td align="left" >
                 
                 <asp:ImageButton ID="btnTripSearch" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png" onclick="btnTripSearch_Click" 
                     CssClass="ImageButtonSytle" />
                 
                 &nbsp;<asp:Label ID="lblTripInfo" runat="server" Font-Bold="True" ForeColor="#0066FF"></asp:Label>
                 <asp:HiddenField ID="hfTrip" runat="server" />
                 <ajaxToolkit:ModalPopupExtender ID="hfTrip_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" BehaviorID="hfTrip_ModalPopupExtender" DynamicServicePath="" PopupControlID="Panel5" TargetControlID="hfTrip">
                 </ajaxToolkit:ModalPopupExtender>
               
            </td>
          
            </tr>
            <tr>
            <td align="right" class="style7">
                Dealer
            </td>
             <td align="left">
               <asp:Label ID="lblCustomerCode" runat="server" 
                     Height="20px" Width="80px" CssClass="lblInformation1" required></asp:Label>
                 <asp:ImageButton ID="btnDealerSearch" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png" 
                     onclick="btnDealerSearch_Click" 
                     CssClass="ImageButtonSytle" Enabled="False" Visible="False" />
                       &nbsp;<asp:Label ID="lblCustomerName" runat="server" 
                     CssClass="lblInformation2" ></asp:Label>
                 <asp:HiddenField ID="hfDealer" runat="server" />
                
                 <ajaxToolkit:ModalPopupExtender ID="hfDealer_ModalPopupExtender" runat="server" 
                     BehaviorID="hfDealer_ModalPopupExtender" DynamicServicePath="" 
                     TargetControlID="hfDealer" PopupControlID="Panel6" BackgroundCssClass="modalBackground" CancelControlID="btnDealerCancel" OkControlID="btnDealerOk">
                 </ajaxToolkit:ModalPopupExtender>
            </td>
            
            </tr>
            <tr>
            <td align="right">
                TC No
            </td>
             <td class="style1" align="left">
                 <asp:ImageButton ID="btnTcNoSearch" runat="server" 
                     ImageUrl="~/Images/1488717192_Search.png" onclick="btnTcNoSearch_Click" 
                     CssClass="ImageButtonSytle"/> &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:ImageButton ID="btnRefresh" runat="server" 
                                    ImageUrl="~/Images/1488717327_Synchronize.png" Height="22px" 
                                    onclick="btnRefresh_Click" ToolTip="Reload TC" Width="22px" />
                                    <br />
                 &nbsp;<asp:Label ID="lblTCInfo" runat="server" Font-Bold="True" ForeColor="#0066FF"></asp:Label>
                 <asp:HiddenField ID="hfTC" runat="server" />
                
                 <asp:HiddenField ID="hfCustomerId" runat="server" />
                
                 <ajaxToolkit:ModalPopupExtender ID="hfTC_ModalPopupExtender" runat="server" 
                     BehaviorID="hfTC_ModalPopupExtender" DynamicServicePath="" 
                     TargetControlID="hfTC" PopupControlID="Panel7" BackgroundCssClass="modalBackground" CancelControlID="btnTCCancel" OkControlID="btnTCok">
                 </ajaxToolkit:ModalPopupExtender>
            </td>
            
            <tr>
            <td align="right" class="style7">
                Customer
            </td>
             <td align="left">
               <asp:Label ID="lblCustomerCode1" runat="server" 
                     Height="20px" Width="80px" CssClass="lblInformation1" required></asp:Label>
                
                       &nbsp;<asp:Label ID="lblCustomerName1" runat="server" 
                     CssClass="lblInformation2" ></asp:Label>
                
                
                 
            </td>

            </tr>
            <tr>
            <td align="right">Remarks
            </td>
            <td align="left" colspan="2">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="300px" 
                    style="font-size: small; font-family: SutonnyMJ"></asp:TextBox>
            </td>
            
            </tr>
            <tr>
                   <td align="right">Status</td>
            <td align="left" >
             <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True">
                <asp:ListItem Value="0" Text="Open"></asp:ListItem>
               <asp:ListItem Value="1" Text="Confirm"></asp:ListItem>
               <asp:ListItem Value="2" Text="Cancel"></asp:ListItem>
                  <asp:ListItem Value="3" Text="Close"></asp:ListItem>
                 </asp:DropDownList>
                  
               
            </td>
                
              </tr>

                
                         
                   
            <%--<tr>
            <td class="style7">Current Capacity</td>
            <td align="left"><asp:Label ID="lblCurrentCapacity" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            
                &nbsp;Remaining Capacity <asp:Label ID="lblCapacity" runat="server" Font-Bold="True" ForeColor="#000066"></asp:Label>
            </td>
            <td></td>
            <td>
                
                </td>
            
            </tr>--%>
            
            
        </table>
                 </td>
                 <td valign="top" >
           <table >
                <tr>
                <td>
                <asp:Panel ID="PlSms" runat="server" BorderColor="#3399FF" BorderStyle="Solid">


                <table>
                <tr>
                <td align="right">S&amp;M Mobile No
                   </td>
            <td align="left" >
            <asp:Label ID="lblsnmMobileNo" runat="server" Width="120px" ></asp:Label>
               
            </td>
                
              </tr>

                <tr>
                   <td align="right" >Dealer Mobile No</td>
            <td align="left" >
                <asp:Label ID="lblDealerMobile" runat="server" Width="120px" ></asp:Label>
               
            </td>
                
              
              </tr>

                <tr>
                   <td align="right" >Customer Mobile no</td>
            <td align="left" >
            <asp:Label ID="lblCustomerMobile" runat="server" Width="120px" ></asp:Label>
            </td>
               
               
              </tr>
              <tr>
              <td >
              <asp:Button ID="btnSendSms" runat="server" Text="Send Sms" 
                      onclick="btnSendSms_Click" />
              </td>
                </tr>
                    
                </table>
                 </asp:Panel>
                         <br />
                    <asp:DetailsView ID="dvTrip" runat="server" AutoGenerateRows="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="Horizontal" Width="300px" >
                    
                        <AlternatingRowStyle BackColor="White" />
                        <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                        <Fields>
                            <asp:BoundField DataField="TripNo" HeaderText="Trip No" />
                            <asp:BoundField DataField="TripDate" DataFormatString="{0:dd/MMM/yyyy}" 
                                HeaderText="Trip Date" />
                            <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
                            <asp:BoundField DataField="EmpCode" HeaderText="DriverCode" />
                            <asp:BoundField DataField="EmpName" HeaderText="Driver Name" />
                            <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                            <asp:BoundField DataField="CapacityBal" HeaderText="Capacity Bal" 
                                DataFormatString="{0:dd/MMM/yyyy}" />
                            <asp:BoundField DataField="Totalkm" HeaderText="Total km" 
                                DataFormatString="{0:f2}" />
                            <asp:TemplateField HeaderText="KM Per Liter">
                                <ItemTemplate>
                                    <asp:Label ID="lblKMPerLiter" runat="server" Text='<%# Bind("KmPerLiter") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("KmPerLiter") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("KmPerLiter") %>'></asp:TextBox>
                                </InsertItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FuelRate">
                                <ItemTemplate>
                                    <asp:Label ID="lblFuelRate" runat="server" 
                                        Text='<%# Bind("FuelRate", "{0:0.00}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("FuelRate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("FuelRate", "{0:0.00}") %>'></asp:TextBox>
                                </InsertItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Left" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                    
                    </asp:DetailsView>
                    </td>
                </tr>
            </table>

                 </td>
             </tr>
         </table>
        
               </asp:Panel>

              <asp:Panel ID="Panel5" runat="server" BorderStyle="Dotted" BorderWidth="2px" 
                   BackColor="White" ScrollBars="Auto" GroupingText="Select Trip">
                  <asp:UpdatePanel ID="upTrip" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <table>
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtSearchTrip" runat="server"/>
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnSearchdTrip" runat="server" Text="Search" CssClass="Button" OnClick="btnSearchdTrip_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvTrip" runat="server" AllowPaging="True" 
                      onselectedindexchanged="gvTrip_SelectedIndexChanged" CellPadding="4" 
                ForeColor="#333333" GridLines="Horizontal" 
                OnPageIndexChanging="gvTrip_PageIndexChanging" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                  <asp:BoundField DataField="TripNo" HeaderText="TripNo" />
                <asp:BoundField DataField="TripDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="TripDate" />
                <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />
                <asp:BoundField DataField="Totalkm" HeaderText="Totalkm" />
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
                <asp:Button ID="btnTripOk" runat="server" Text="Ok" />
            </td>
            <td class="auto-style2">
            <asp:Button ID="btnTripCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
                   </ContentTemplate>
<Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnSearchdTrip" />
           
            </Triggers>
</asp:UpdatePanel>
                  </asp:Panel>

              <asp:Panel ID="Panel6" runat="server" BorderStyle="Dotted" BorderWidth="2px" 
                   BackColor="White" ScrollBars="Auto" GroupingText="Select Dealer">
                  <asp:UpdatePanel ID="upDearler" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <table>
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtDealerSearch" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnDearlerSearch" runat="server" Text="Search" CssClass="Button" OnClick="btnDearlerSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvDealer" runat="server" AllowPaging="True" CellPadding="4" 
                ForeColor="#333333" GridLines="Horizontal" 
                OnSelectedIndexChanged="gvDealer_SelectedIndexChanged" 
                AutoGenerateColumns="False" OnPageIndexChanging="gvDealer_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                 <asp:BoundField DataField="CustId" HeaderText="Id" />
                <asp:BoundField DataField="CustName" HeaderText="Name" />
                <asp:BoundField DataField="CustAddressBang" HeaderText="Address" />
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
                <asp:Button ID="btnDealerOk" runat="server" Text="Ok" OnClick="btnDealerOk_Click" />
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

              <asp:Panel ID="Panel7" runat="server" BorderStyle="Outset" BorderWidth="2px" ScrollBars="Auto" GroupingText="Select TC" BackColor="White">
                  <asp:UpdatePanel ID="upTC" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <table>
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtTCSearch" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnTCSearch" runat="server" Text="Search" CssClass="Button" OnClick="btnTCSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvTC" runat="server" AllowPaging="True" CellPadding="4" 
                ForeColor="#333333" GridLines="Horizontal" 
                OnSelectedIndexChanged="gvTC_SelectedIndexChanged" AutoGenerateColumns="False" 
                OnPageIndexChanging="gvTC_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                 <asp:BoundField DataField="TCNo" HeaderText="TCNo" />
                <asp:BoundField DataField="TCDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="TCDate" />
                <asp:BoundField DataField="CustName" HeaderText="Dealer" />
                <asp:BoundField DataField="CustId" HeaderText="CustId" />
                <asp:BoundField DataField="custName1" HeaderText="Customer" />
             
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
                <asp:Button ID="btnTCok" runat="server" Text="Ok" OnClick="btnTCok_Click" />
            </td>
            <td>
            <asp:Button ID="btnTCCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
            </ContentTemplate>
<Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnTCSearch" />
           
            </Triggers>
</asp:UpdatePanel>
                  </asp:Panel>

    <asp:Panel ID="Panel9" runat="server" CssClass="entry-panel">
          
                <asp:GridView ID="gvListofDOProduct" runat="server" CellPadding="4" 
                    ForeColor="#333333" GridLines="Horizontal" ShowFooter="True" AutoGenerateColumns="False" >
                 
                 <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" ReadOnly="True" />
                     <asp:BoundField DataField="ProductName" HeaderText="ProductName" 
                         ReadOnly="True" ItemStyle-Width="200px" >
                     <ItemStyle Width="200px" />
                     </asp:BoundField>
                     <asp:TemplateField HeaderText="OrderQty">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("OrderQty") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label1" runat="server" Text='<%# Bind("OrderQty", "{0:0}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="Rent" HeaderText="Rent" ReadOnly="True" 
                         DataFormatString="{0:0.00}" />
                     <asp:TemplateField HeaderText="Total">
                         <EditItemTemplate>
                             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Total") %>'></asp:TextBox>
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:Label ID="Label2" runat="server" Text='<%# Bind("TotalPrice", "{0:0.00}") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
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
                 
                </asp:GridView></asp:Panel>

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
