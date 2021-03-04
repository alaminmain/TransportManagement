<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.Supplier" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "Supplier.aspx/PageLoad",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: Error
        });

    });

  </script>
    });

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel5" runat="server">
          <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
  <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate/>
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"   />
            <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" formnovalidate/>
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel7" runat="server" BackColor="White"  BorderStyle="Double" BorderWidth="3px" ScrollBars="Auto" >
         <asp:UpdatePanel ID="upListofbasicData" runat="server">
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
                     OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged" 
                     OnPageIndexChanging="gvlistofBasicData_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="SupplierID" HeaderText="SupplierID">
                  
                    </asp:BoundField>
                <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
                 <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
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
       <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Supplier Information" CssClass="entry-panel">
        
               <asp:Panel ID="Panel6" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black">

                   <table>
                       <tr>
                           <td class="auto-style2" align="right">Supplier Id</td>
                           <td align="left">
                               <asp:TextBox ID="txtSupplierID" runat="server" TabIndex="1" BackColor="#66CCFF"
                                   ReadOnly="True" Width="160px" CssClass="IdStyle"></asp:TextBox>
                               &nbsp;
                               <asp:Label ID="lblMsg" runat="server" CssClass="FillUpText" ForeColor="#009933"></asp:Label>
                           </td>

                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Name</td>
                           <td align="left">
                               <asp:TextBox ID="txtSupplierName" runat="server" TabIndex="2" Width="160px"></asp:TextBox>
                           </td>

                       </tr>


                       <tr>
                           <td class="auto-style2" align="right">Phone</td>
                           <td align="left">
                               <asp:TextBox ID="txtPhone" runat="server" TabIndex="4" Width="160px"></asp:TextBox>
                           </td>

                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Address</td>
                           <td align="left">
                               <asp:TextBox ID="txtAdd1" runat="server" TabIndex="4" Width="200px"></asp:TextBox>
                           </td>

                       </tr>
                       <tr>
                           <td class="auto-style2" align="right"></td>
                           <td align="left">
                               <asp:TextBox ID="txtAdd2" runat="server" TabIndex="4" Width="200px"></asp:TextBox>
                           </td>

                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">&nbsp;</td>
                           <td align="left">
                               <asp:TextBox ID="txtAdd3" runat="server" TabIndex="4" Width="200px"></asp:TextBox>
                           </td>

                       </tr>

                       <tr>

                           <td class="auto-style2" align="right">Post Code</td>
                           <td align="left">
                               <asp:TextBox ID="txtPostCode" runat="server" TabIndex="8"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Contact Name</td>
                           <td align="left">
                               <asp:TextBox ID="txtContactName" runat="server" TabIndex="3" Width="160px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Fax</td>
                           <td align="left">
                               <asp:TextBox ID="txtFax" runat="server" TabIndex="9"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Mobile</td>
                           <td align="left">
                               <asp:TextBox ID="txtMobile" runat="server" TabIndex="10"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Email</td>
                           <td align="left">
                               <asp:TextBox ID="txtEmail" runat="server" TabIndex="11" TextMode="Email" Width="200px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Web Address</td>
                           <td align="left">
                               <asp:TextBox ID="txtWebAdd" runat="server" TabIndex="12" TextMode="Url" Width="200px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Remarks</td>
                           <td align="left">
                               <asp:TextBox ID="txtRemark" runat="server" TabIndex="12" TextMode="MultiLine" Width="200px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td class="auto-style2" align="right">Status</td>
                           <td align="left">
                               <asp:CheckBox ID="CkActive" runat="server"  TabIndex="14" Text="Active" Checked="True" />
                               <br />
                           </td>
                       </tr>
                       <tr>
                           <td colspan="2" align="left">
                               &nbsp;</td>

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
