<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="TransportManagerUI.UI.Voucher" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "Voucher.aspx/PageLoad",
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


        //$(document).ready(function () {
        //    var total = 0.00;
        //    $("#gvExpenses input[id $= 'txtAmount']").each(function () {
        //        var value = $(this).val();
        //        total = parseFloat(total) + Math.round(parseFloat(value));
        //    });
        //    $("#txtTotalAmount").val(total);
        //});
        function CalculateNetIncome() {
            var TotalAdvance = document.getElementById('<%=txtAdvance.ClientID %>').value;
            document.getElementById('<%=txtTotalAdvance.ClientID %>').value = TotalAdvance;
            var TotalIncome = document.getElementById('<%=txtIncome.ClientID %>').value;
            document.getElementById('<%=txtTotalIncome.ClientID %>').value = TotalIncome;
            var TotalExpenses = document.getElementById('<%=txtTotalAmount.ClientID %>').value;
            document.getElementById('<%=txtTotalExpense.ClientID %>').value = TotalExpenses;


            var txtNet = 0.00;
           
            txtNet = parseFloat(TotalIncome) - (parseFloat(TotalAdvance) + parseFloat(TotalExpenses));

            document.getElementById('<%=txtNetIncome.ClientID %>').value = txtNet.toFixed(2);

        }

        function calculateAdditionalFuel() {
            var txtTotal = 0.00;
            //var passed = false;
            //var id = 0;
            var grd = document.getElementById('<%=gvExpenses.ClientID %>');
            var dv = document.getElementById('<%=dvTrip.ClientID %>');
            var txtAdditionalKM = document.getElementById("<%=txtAdditionalKM.ClientID %>").value;

            var kmperliter = document.getElementById('cphMainContent_dvTrip_lblKMPerLiter').innerHTML;

            var FuelRate = document.getElementById('cphMainContent_dvTrip_lblFuelRate').innerHTML;

            var totalAmount = (parseFloat(txtAdditionalKM) / parseFloat(kmperliter)) * parseFloat(FuelRate);

            grd.rows[8].cells[2].children[0].value = totalAmount.toFixed(2);
            CalculateNetIncome();
            calculate();

        }


        function calculate() {
            var txtTotal = 0.00;
            //var passed = false;
            //var id = 0;

            $(".calculate").each(function (index, value) {
                var val = value.value;

                val = val.replace(",", ".");
                txtTotal = MathRound(parseFloat(txtTotal) + parseFloat(val));

            });


            document.getElementById("<%=txtTotalAmount.ClientID %>").value = txtTotal.toFixed(2);
            CalculateNetIncome();
        }

        function MathRound(number) {
            return Math.round(number * 100) / 100;
        }

</script>


    
    <style type="text/css">
        .auto-style1 {
            width: 176px;
        }
        </style>
   
    
  
   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
     <asp:Panel ID="Panel5" runat="server">
    <asp:UpdatePanel  ID="uplMembership" runat="server" >
<ContentTemplate>
   
   <asp:Panel ID="plTopControl" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel7" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" onclick="btnAddNew_Click" formnovalidate   />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate   />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"/>


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="plBasicData" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
       <br />
      <asp:Panel ID="plBasicData" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="1px" >
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" 
                     EmptyDataText="No Data To Show" GridLines="None" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" 
                     onselectedindexchanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="VoucherNo" HeaderText="VoucherNo" />
                <asp:BoundField DataField="VoucherDate" DataFormatString="{0:dd/MMM/yyyy}" 
                    HeaderText="VoucherDate" />
                <asp:BoundField DataField="Income" DataFormatString="{0:f2}" 
                    HeaderText="Income" />
                <asp:BoundField DataField="TotExpense" DataFormatString="{0:f2}" 
                    HeaderText="Expense" />
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
       

   
    
        
        <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Voucher" CssClass="entry-panel">
        
            <table>
                <tr>
                    <td><table cellpadding="3px" cellspacing="1" align="left" >
            <tr>
        <td align="right" >
        Voucher No
        </td>
         <td align="left">
             <asp:Label ID="lblVoucherNo" runat="server" Font-Bold="True" ForeColor="White" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
            <tr>
            <td align="right" >
            Voucher Date
            </td>
             <td align="left">
                 <asp:TextBox ID="txtVoucherDate" runat="server" Width="120px" ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="txtVoucherDate_CalendarExtender" 
                     runat="server" BehaviorID="txtVoucherDate_CalendarExtender" 
                     TargetControlID="txtVoucherDate" Format="dd/MMM/yyyy" />
            </td>
             
            
            
            </tr>
            <tr>
            <td align="right" >
            Return Date
            </td>
             <td align="left">
                 <asp:TextBox ID="txtReturnDate" runat="server" Width="120px"  ></asp:TextBox>
                 <ajaxToolkit:CalendarExtender ID="txtReturnDate_CalendarExtender" 
                     runat="server" BehaviorID="txtReturnDate_CalendarExtender" 
                     TargetControlID="txtReturnDate" Format="dd/MMM/yyyy" />
            </td>
            </tr>

            <tr>
            <td align="right" >
                Trip No
            </td>
             <td align="left">
                 <asp:ImageButton ID="btnSearchTrip" runat="server" ImageUrl="~/Images/1488717192_Search.png" CssClass="ImageButtonSytle" OnClick="btnSearchTrip_Click" />
                 <asp:HiddenField ID="hfSearchTrip" runat="server" />
                 <asp:HiddenField ID="hfInsertorUpdate" runat="server" Value="Insert" />
                 <br />
                  <ajaxToolkit:ModalPopupExtender ID="hfTC_ModalPopupExtender" runat="server" 
                     BehaviorID="hfTC_ModalPopupExtender" DynamicServicePath="" 
                     TargetControlID="hfSearchTrip" PopupControlID="Panel1" BackgroundCssClass="modalBackground">
                 </ajaxToolkit:ModalPopupExtender>
            </td>
                            
            </tr>
            <tr>
            <td  align="right">Remarks
            </td>
            <td colspan="3" align="left">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td  align="right">Additional KM
            </td>
            <td colspan="3" align="left">
                <asp:TextBox ID="txtAdditionalKM" runat="server" onkeyup="calculateAdditionalFuel()">0</asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="txtAdditionalKM_FilteredTextBoxExtender" 
                    runat="server" BehaviorID="txtAdditionalKM_FilteredTextBoxExtender" 
                    ValidChars="0123456789." TargetControlID="txtAdditionalKM" />
            </td>
            </tr>
                         <tr>
            <td  align="right">Advance
            </td>
            <td colspan="3" align="left">
                <asp:TextBox ID="txtAdvance" runat="server" onkeyup="CalculateNetIncome()" >0</asp:TextBox>
                <ajaxToolkit:FilteredTextBoxExtender ID="txtAdvance_FilteredTextBoxExtender" 
                    runat="server" BehaviorID="txtAdvance_FilteredTextBoxExtender" 
                    TargetControlID="txtAdvance" ValidChars="0123456789."/>
            </td>
            </tr>
            <tr>
            <td  align="right">Status
            </td>
            <td colspan="3" align="left">
                <asp:DropDownList ID="ddlStatus" runat="server">

                    <asp:ListItem Value="0" Text="Open"></asp:ListItem>
               <asp:ListItem Value="1" Text="Confirm"></asp:ListItem>
               <asp:ListItem Value="2" Text="Cancel"></asp:ListItem>
                  <asp:ListItem Value="3" Text="Close"></asp:ListItem>
                </asp:DropDownList>
            </td>
            </tr>
            </table>

                    </td>
                    <td class="auto-style1" valign="top">
                        <table >
                <tr>
                <td>
                
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
                                        Text='<%# Bind("FuelRate", "{0:f2}") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("FuelRate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" 
                                        Text='<%# Bind("FuelRate", "{0:f2}") %>'></asp:TextBox>
                                </InsertItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                    
                    </asp:DetailsView>
                    </td>
                </tr>
            </table>

                    </td>
                </tr>

            </table>
            
            
        
             <asp:Panel ID="Panel6" runat="server" BorderStyle="Dotted" BorderWidth="2px" 
                    ScrollBars="Auto" Direction="LeftToRight">
          <table>
            <tr>
           <td>
               <asp:Panel ID="Panel3" runat="server" Font-Names="Raavi">
                  
<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" ScrollBars="Auto" 
                        TabStripPlacement="TopRight" Width="100%" 
                       >
                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Income" Font-Names='"Helvetica Neue", Helvetica, Arial, sans-serif'><ContentTemplate>
                            

                            <table>
                                <tr>
                                    <th>
                                        Products</th>
                                    

                                    <th>
                                        Trip From</th>
                                    <th>
                                        Trip To</th>
                                    

                                    <th>
                                        Rent Amount</th>
                                    <th>
                                    </th>
                                </tr>
                                

                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtProduct" runat="server" ></asp:TextBox>
                                    </td>
                                    

                                    <td>
                                        <asp:TextBox ID="txtTripFrom" runat="server" ></asp:TextBox>
                                    </td>
                                    

                                    <td>
                                        <asp:TextBox ID="txtTripTo" runat="server"></asp:TextBox>
                                    </td>
                                    

                                    <td>
                                        <asp:TextBox ID="txtRentAmount" runat="server" ></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtRentAmount_FilteredTextBoxExtender" runat="server" BehaviorID="txtRentAmount_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtRentAmount" />
                                    </td>
                                    

                                    <td>
                                        <asp:Button ID="btnAddIncome" runat="server" Text="Add" OnClick="btnAddIncome_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    

                                    <td colspan="5">
                                        <asp:GridView ID="gvIncome" runat="server" CellPadding="4" ForeColor="#333333" 
                                            GridLines="None" AutoGenerateColumns="False" 
                                            OnRowDeleting="gvIncome_RowDeleting" Width="537px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns><asp:CommandField ShowDeleteButton="True" />
                                            <asp:BoundField DataField="Product" HeaderText="Product" />
                                <asp:BoundField DataField="TripFrom" HeaderText="TripFrom" />
                                <asp:BoundField DataField="TripTo" HeaderText="TripTo" />
                                <asp:BoundField DataField="Rent" HeaderText="Rent" />
                                            </Columns>
                                            
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <sortedascendingcellstyle backcolor="#F5F7FB" />
                            <sortedascendingheaderstyle backcolor="#6D95E1" />
                            <sorteddescendingcellstyle backcolor="#E9EBEF" />
                            <sorteddescendingheaderstyle backcolor="#4870BE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                             <td bgcolor="#0066FF"></td>
                                             <td bgcolor="#0066FF" style="font-weight: bold; color: #FFFFFF">Total</td>
                                             <td bgcolor="#0066FF" style="font-weight: bold; color: #FFFFFF">
                                                 
                                                 
                                             <td bgcolor="#0066FF" style="font-weight: bold; color: #FFFFFF">
                                             <asp:TextBox ID="txtIncome" runat="server" AutoPostBack="True" onkeyup="CalculateNetIncome()" ReadOnly="True" ></asp:TextBox>
                                             </td>
                                         </tr>
                            </table>
                            </ContentTemplate></ajaxToolkit:TabPanel>
                         <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Expenses" Font-Names='"Helvetica Neue", Helvetica, Arial, sans-serif'><ContentTemplate>
                             

                             <asp:Panel ID="Panel4" runat="server">
                                     <table>
                                         <tr><td colspan="4">
                                             <asp:GridView ID="gvExpenses" runat="server" CellPadding="4" ForeColor="#333333" 
                                                 GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvExpenses_RowDeleting">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                                 <asp:BoundField DataField="AccountCode" HeaderText="AccountCode" ReadOnly="True" >
                                                 <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                 </asp:BoundField>
                                 <asp:BoundField DataField="AccountDesc" HeaderText="AccountDesc" ReadOnly="True" >
                                                 <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                 </asp:BoundField>
                                 <asp:TemplateField HeaderText="Amount">
                                     <EditItemTemplate>
                                         <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Amount") %>' ></asp:TextBox>
                                     </EditItemTemplate>
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount","{0:F2}") %>' style="text-align: right" onchange="calculate()" class="calculate" Width="100px" ></asp:TextBox>
                                        
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Right" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Comments">
                                     <EditItemTemplate>
                                         <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Comments") %>'></asp:TextBox>
                                     </EditItemTemplate>
                                     <ItemTemplate>
                                         <asp:TextBox ID="txtComments" runat="server" Text='<%# Bind("Comments") %>'></asp:TextBox>
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" Width="200px" />
                                 </asp:TemplateField>
                                                 </Columns>
                                                 
                             <EditRowStyle BackColor="#2461BF" />
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                             <RowStyle BackColor="#EFF3FB" />
                             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                             <sortedascendingcellstyle backcolor="#F5F7FB" />
                             <sortedascendingheaderstyle backcolor="#6D95E1" />
                             <sorteddescendingcellstyle backcolor="#E9EBEF" />
                             <sorteddescendingheaderstyle backcolor="#4870BE" />
                                             </asp:GridView></td>

                                         </tr>
                                         <tr>
                                             <td bgcolor="#0066FF"></td>
                                             <td bgcolor="#0066FF" style="font-weight: bold; color: #FFFFFF">Total</td>
                                             <td bgcolor="#0066FF" style="font-weight: bold; color: #FFFFFF">
                                                 <asp:TextBox ID="txtTotalAmount" runat="server" onchange="CalculateNetIncome()"  ReadOnly="True"></asp:TextBox>
                                             </td>
                                             <td bgcolor="#0066FF" style="font-weight: bold; color: #FFFFFF"></td>
                                         </tr>
                                     </table></asp:Panel>
                             </ContentTemplate></ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                     
            </asp:Panel>
        
             </td>
    </tr>
              
    </table>
                 </asp:Panel>

                 <asp:Panel ID="Panel8" runat="server" BorderStyle="Dotted" BorderWidth="2px" 
                   BackColor="White" ScrollBars="Auto">
                  <asp:UpdatePanel ID="updnetIncome" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <table>
                            <tr>
                            <td>
                                Total Income
                            </td>
                            <td>
                            <asp:TextBox ID="txtTotalIncome" runat="server" ReadOnly="True" 
                                    style="text-align: right">0</asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td >
                                Advance
                            </td>
                            <td>
                            <asp:TextBox ID="txtTotalAdvance" runat="server" ReadOnly="True" 
                                    style="text-align: right">0</asp:TextBox>
                            </td>
                            </tr>
                             <tr>
                            <td>
                                Total Expenses
                            </td>
                            <td>
                            <asp:TextBox ID="txtTotalExpense" runat="server" ReadOnly="True" 
                                    style="text-align: right">0</asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td>
                                Net Income/Loss
                            </td>
                            <td>
                            <asp:TextBox ID="txtNetIncome" runat="server" ReadOnly="True" 
                                    style="text-align: right">0</asp:TextBox>
                            </td>
                            </tr>
                           
                        </table>
        
                   </ContentTemplate>
<Triggers>
 <asp:AsyncPostBackTrigger ControlID="btnSearchdTrip" />
           
            </Triggers>
</asp:UpdatePanel>
                  </asp:Panel>
 
                 
             
            <asp:Panel ID="Panel1" runat="server" BorderStyle="Dotted" BorderWidth="2px" 
                   BackColor="White" ScrollBars="Auto" GroupingText="Select Trip">
                  <asp:UpdatePanel ID="upTrip" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
     <table>
                            <tr>
                            <td style="width:160px;">
                                <asp:TextBox ID="txtSearchTrip" runat="server"/>
                            </td>
                            <td style="width:90px;">
                                <asp:Button ID="btnSearchdTrip" runat="server" Text="Search" CssClass="Button" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvTrip" runat="server" AllowPaging="True" 
                      onselectedindexchanged="gvTrip_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvTrip_PageIndexChanging"  AutoGenerateColumns="False">
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
                <asp:Button ID="btnTripOk" runat="server" Text="Ok" OnClick="btnTripOk_Click" />
            </td>
            <td>
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
