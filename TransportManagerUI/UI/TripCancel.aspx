<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="TripCancel.aspx.cs" Inherits="TransportManagerUI.UI.TripCancel" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "TripCancel.aspx/PageLoad",
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
    <asp:Button ID="btnSave" runat="server" Text="Cancel Trip" OnClick="btnSave_Click" />
       
        </asp:Panel>
     
    
     
        
    </asp:Panel>

     <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" CssClass="entry-panel"
            GroupingText="Cancel Trip" ForeColor="Black">
         <table>
             <tr>
                 
            <td align="right">
                Select Trip No
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
            <td></td>
       
                 <td valign="top" >
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
