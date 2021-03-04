<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TripAdvInfo.aspx.cs" Inherits="TransportManagerUI.UI.TripAdvInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "TripAdvInfo.aspx/PageLoad",
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
   
    <asp:Panel ID="Panel3" runat="server">
         <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
         <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel7" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" formnovalidate   />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate   />
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click" />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel8" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
             <br />
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
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     onselectedindexchanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="TripAdvNo" HeaderText="TripAdvNo" />
                <asp:BoundField DataField="TripAdvDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="TripAdvDate" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                <asp:BoundField DataField="EmpName" HeaderText="Driver" />
                
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
             <Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnSearch" />
           
            </Triggers>
    </asp:UpdatePanel>
    </asp:Panel>
    
     
        
    </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" ForeColor="Black" 
             BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Trip Advance Load" 
             CssClass="entry-panel">
        <table cellpadding="3px" cellspacing="1">
            <tr>
        <td align="right" class="style14">
            Trip Advance No
        </td>
         <td align="left" class="style1">
             <asp:Label ID="lblTripNo" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
            <tr>
            <td align="right" class="style16">
                Trip Advance Date
            </td>
             <td align="left" class="style1">
                 <asp:TextBox ID="txtTripDate" runat="server" Width="120px" ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="txtTripDate_CalendarExtender" 
                     runat="server" BehaviorID="txtTCDate_CalendarExtender" 
                     TargetControlID="txtTripDate" Format="dd/MMM/yyyy" />
            </td>
           
            </tr>
            <tr>
                <td align="right">
                 Ghat
                </td><td align="left" class="style1">
                <asp:DropDownList ID="ddlGhatList" runat="server" AutoPostBack="True" Height="24px" Width="173px">
             
                 </asp:DropDownList>
            </td>
            </tr>
            <tr>
             <td class="style15" align="right">
            Transport By
            </td>
            <td align="left" class="style1">
                <asp:DropDownList ID="ddlTransportBy" runat="server" AutoPostBack="true" 
                    Height="24px" Width="172px">
                <asp:ListItem Value="1" Text="Own"></asp:ListItem>
                <asp:ListItem Value="2" Text="Hired"></asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
             <tr>
             <td class="style15" align="right">
                 Transport Agent
            </td>
            <td align="left" class="style1">
                <asp:DropDownList ID="ddlAgent" runat="server" AutoPostBack="true" 
                    Height="24px" Width="169px">
              
                   
              
                </asp:DropDownList>
            </td>
            </tr>
            
            <tr>
            <td class="style15" align="right">
                Vehicle
            </td>
             <td align="left" class="style1">
           
             
                  <asp:ImageButton ID="btnVehicle" runat="server" ImageUrl="~/Images/1488717192_Search.png" 
                   CssClass="ImageButtonSytle" onclick="btnVehicle_Click" />
               
                   <asp:Label ID="lblCapacity" runat="server" Font-Bold="True" ForeColor="Blue" 
                      Height="20px"></asp:Label>&nbsp;
                   <ajaxToolkit:ModalPopupExtender ID="hfVehicleSearch_ModalPopupExtender" 
                      runat="server" BehaviorID="hfVehicleSearch_ModalPopupExtender" 
                      CancelControlID="btnVehicleCancel" DynamicServicePath="" 
                      OkControlID="btnVehicleOk" PopupControlID="plVehicleSearch" 
                      TargetControlID="hfVehicleSearch" BackgroundCssClass="modalBackground" >
                  </ajaxToolkit:ModalPopupExtender>
                        
        
            </td>
           
             
            </tr>
          
            <tr>
            <td align="right">
                Driver
            </td>
             <td align="left" class="style1">

             <asp:Label ID="lblDriverCode" runat="server" Font-Bold="True" 
                     CssClass="lblInformation1" required></asp:Label>*
             <asp:ImageButton ID="btnDriver" runat="server" ImageUrl="~/Images/1488717192_Search.png" 
                     CssClass="ImageButtonSytle" onclick="btnDriver_Click" />
                     <asp:Label ID="lblDriverName" runat="server" Font-Bold="True" 
                     CssClass="lblInformation2"></asp:Label>
              
            </td>
           
            </tr>
             
            <tr>
            <td align="right">Remarks
            </td>
            <td colspan="3" align="left" class="style1">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            
            </tr>
            
            <tr>
                   <td>Status</td>
            <td align="left" >
             <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True">
               <asp:ListItem Value="0" Text="Open"></asp:ListItem>
                <asp:ListItem Value="1" Text="On Trip"></asp:ListItem>
                 <asp:ListItem Value="2" Text="Billed"></asp:ListItem>
               <asp:ListItem Value="3" Text="Cancel"></asp:ListItem>
                 
                 </asp:DropDownList>
                                   
            </td>
              </tr>
            <tr>

                <td>
                    &nbsp;</td>
                <td>
<asp:HiddenField ID="hfDriverSearch" runat="server" />
                  <asp:HiddenField ID="hfVehicleSearch" runat="server" />
                    <asp:HiddenField ID="hfTotalCapacity" runat="server" />
                  <ajaxToolkit:ModalPopupExtender ID="hfDriverSearch_ModalPopupExtender" 
                     runat="server" BehaviorID="hfDriverSearch_ModalPopupExtender" DynamicServicePath="" 
                     TargetControlID="hfDriverSearch" BackgroundCssClass="modalBackground" PopupControlID="plDriver" OkControlID="btnDriverOk" CancelControlID="btnDriverCancel">
                 </ajaxToolkit:ModalPopupExtender>

                </td>
                
            </tr>
            
        </table>
            

        </asp:Panel>

        <asp:Panel ID="plVehicleSearch" runat="server" GroupingText="Select Vehicle" 
             BackColor="White" BorderWidth="2px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table style="width: 255px" >
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtVehicleSearch" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnVehicleSearch" runat="server" Text="Search" 
                                    CssClass="Button" onclick="btnVehicleSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvVehicleList" runat="server" AllowPaging="True" 
              CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                AutoGenerateColumns="False" 
                onselectedindexchanged="gvVehicleList_SelectedIndexChanged" 
                OnPageIndexChanging="gvVehicleList_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" />
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
                 <asp:BoundField DataField="VehicleType" HeaderText="VehicleType" />
                <asp:BoundField DataField="Capacity" HeaderText="Capacity" />
                <asp:BoundField DataField="KmPerLiter" HeaderText="KmPerLiter" />
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
                <asp:Button ID="btnVehicleOk" runat="server" Text="Ok" />
            </td>
            <td>
            <asp:Button ID="btnVehicleCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
        </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnVehicleSearch" />
           
            </Triggers>
        </asp:UpdatePanel>
            </asp:Panel>

            <asp:Panel ID="plDriver" runat="server" GroupingText="Select Driver" 
             BackColor="White" BorderWidth="2px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table style="width: 255px" >
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtSearchDriver" runat="server" Width="158px" />
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnSearchDriver" runat="server" Text="Search" CssClass="Button" onclick="btnSearchDriver_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvDriverList" runat="server" AllowPaging="True" 
              CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                AutoGenerateColumns="False" 
                onpageindexchanging="gvDriverList_PageIndexChanging" 
                onselectedindexchanged="gvDriverList_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" />
                <asp:BoundField DataField="EmpName" HeaderText="EmpName" />
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
                <asp:Button ID="btnDriverOk" runat="server" Text="Ok" />
            </td>
            <td>
            <asp:Button ID="btnDriverCancel" runat="server" Text="Cancel" />
            </td>
            </tr>
            </table>
        </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSearchDriver" />
           
            </Triggers>
        </asp:UpdatePanel>
            </asp:Panel>
    <asp:Panel ID="Panel4" runat="server" CssClass="entry-panel">
            <%--<asp:UpdatePanel ID="upProductList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                     <table>
                   
            <tr>

            <td colspan="3">

                <asp:Panel ID="Panel5" runat="server" BorderWidth="1" BorderColor="#FFFFCC" 
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
                     <asp:BoundField DataField="Rent" DataFormatString="{0:F}" HeaderText="Rent">
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
            
                       
            
            </table>
               
            </asp:Panel>

                
            
           
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

</asp:Content>
