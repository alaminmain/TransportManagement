<%@ Page Title="VehicleInfo" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="VehicleInfo.aspx.cs" Inherits="TransportManagerUI.UI.VehicleInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "VehicleInfo.aspx/PageLoad",
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
        <asp:Panel ID="Panel7" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" formnovalidate  />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate  />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate  />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel8" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel8" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="1">
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
                     OnPageIndexChanging="gvlistofBasicData_PageIndexChanging" 
                     onselectedindexchanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="VehicleType" HeaderText="VehicleType" />
                <asp:TemplateField HeaderText="VehicleID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("VehicleID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("VehicleID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="VehicleNo" HeaderText="VehicleNo" />
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

       <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" 
        ForeColor="Black" BorderStyle="Outset" BorderWidth="1px" BorderColor="Silver" 
        GroupingText="Vehicle" CssClass="entry-panel">
        
               <asp:Panel ID="Panel6" runat="server" GroupingText="">

                   <table cellpadding="3px">
                        <tr>
        <td align="right">
       Vehicle Type
        </td>
         <td colspan="3" align="left">
             <asp:DropDownList ID="ddlVehicleType" runat="server" Width="200px">
                 <asp:ListItem Text="Open Truck" Value="Open Truck"></asp:ListItem>
                 <asp:ListItem Text="Covered Van" Value="Covered Van"></asp:ListItem>
                 <asp:ListItem Text="Dump Truck" Value="Dump Truck"></asp:ListItem>
                 <asp:ListItem Text="Vessel" Value="Vessel"></asp:ListItem>
                  <asp:ListItem Text="Bulk Carrier" Value="Bulk Carrier"></asp:ListItem>
                 <asp:ListItem Text="Trailer" Value="Trailer"></asp:ListItem>
                 <asp:ListItem Text="Ready Mix" Value="Ready Mix"></asp:ListItem>
                
                 
             </asp:DropDownList>
        </td>
        </tr>
            <tr>
                   <tr>
        <td align="right">
        Vehicle Id
        </td>
         <td colspan="3" align="left">
             <asp:Label ID="lblVehicleId" runat="server" Font-Bold="True" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
        <tr>
        <td align="right">
        Ghat
        </td>
         <td colspan="3" align="left">
             <asp:DropDownList ID="ddlGhatList" runat="server" Width="200px">
             </asp:DropDownList>
        </td>
        </tr>
            <tr>
        <td align="right">
        Vehicle No
        </td>
         <td colspan="3" align="left">
             <asp:TextBox ID="txtVehicleNo" runat="server" Width="200px" required></asp:TextBox>
        </td>
        </tr>
        <tr>
            <td align="right">
            Model No
            </td>
             <td align="left">
                 <asp:TextBox ID="txtModelNo" runat="server" Width="200px" ></asp:TextBox>
                 
            </td>
            </tr>
             <tr>
            <td align="right">
               Engine No
            </td>
             <td align="left">
                <asp:TextBox ID="txtEngineNo" runat="server" Width="200px" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td align="right">
            Chesis No
            </td>
             <td align="left">
                 <asp:TextBox ID="txtChesisNo" runat="server" Width="200px" ></asp:TextBox>
                 
            </td>
            </tr>
              <tr>
            
                <td align="right">
                    Engine Volume
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtEngineVolume" runat="server"></asp:TextBox>
                     
                </td>
                </tr>
            <tr>
            
                <td align="right">
                    Purchase Date
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtPurchaseDate" runat="server"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="txtPurchaseDate_CalendarExtender" runat="server" BehaviorID="txtPurchaseDate_CalendarExtender" TargetControlID="txtPurchaseDate" Format="dd/MMM/yyyy" />
                </td>
                </tr>
            
            
           
            
            <tr>
            <td align="right">
           Vehicle Description
            </td>
             <td align="left">
                <asp:TextBox ID="txtVehicleDescription" runat="server" Width="200px" ></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td align="right">Capacity
            </td>
            <td align="left">
                <asp:TextBox ID="txtCapacity" runat="server" Width="200px" required  ></asp:TextBox>
            </td>
            </tr>

            <tr>
                <td align="right">Capacity Unit
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlCapacityUnit" runat="server" Width="200px">
                <asp:ListItem Value="0" Text="MT"></asp:ListItem>
                <asp:ListItem Value="1" Text="BAG"></asp:ListItem>
                </asp:DropDownList> 
            </td>

            </tr>
                       <tr>
            
                <td align="right">
                    Mobile No
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtMobileNo" runat="server" Width="200px"></asp:TextBox>
                     
                </td>
                </tr>
                     
            
            
            <tr>
            
                <td align="right">
                    K.M Per Litre
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtKmPerLitre" runat="server" Width="200px"  required>0</asp:TextBox>
                     
                     <ajaxToolkit:FilteredTextBoxExtender ID="txtKmPerLitre_FilteredTextBoxExtender" 
                         runat="server" BehaviorID="txtKmPerLitre_FilteredTextBoxExtender" 
                         TargetControlID="txtKmPerLitre" ValidChars=".0123456789" />
                     
                </td>
                </tr>
            
            <tr>
                <td align="right">Fuel Type
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlFuelType" runat="server" Width="200px">
                </asp:DropDownList> 
            </td>

            </tr>
            <%--<tr>
            
                <td align="right">
                    &nbsp;Hired
                </td>
                 <td align="left">
                     <asp:CheckBox ID="chkIsHired" runat="server" />
                     
                </td>
                </tr>--%>
            <tr>
            
                <td align="right">
                    Remarks
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtRemarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                     
                </td>
                </tr>
            
            <tr>
                <td align="right">
                    Status
                </td>
                 <td align="left">
                     <asp:DropDownList ID="ddlStatus" runat="server" Width="200px">
                     <asp:ListItem Value="0" Text="Pull"></asp:ListItem>
                         <asp:ListItem Value="2" Text="Workshop" ></asp:ListItem>
                          <asp:ListItem Value="4" Text="Advance Loading"></asp:ListItem>
                     <asp:ListItem Value="1" Text="On Trip"></asp:ListItem>
                     <asp:ListItem Value="3" Text="Not In Service"></asp:ListItem>
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
   </asp:Panel>

</asp:Content>
