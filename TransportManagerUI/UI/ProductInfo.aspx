<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="ProductInfo.aspx.cs" Inherits="TransportManagerUI.UI.ProductInfo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "ProductInfo.aspx/PageLoad",
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
        BorderColor="#3399FF" ScrollBars="Auto">
        <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate/>
   <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate OnClick="btnReport_Click" />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" formnovalidate/>
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7"  BackgroundCssClass="modalBackground" >
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
                <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" />
                <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                <asp:BoundField DataField="ProductName1" HeaderText="Short Name" >
              
                </asp:BoundField>
                <asp:BoundField DataField="UnitType" HeaderText="UnitType" />
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
   
       <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" ForeColor="Black" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Product" CssClass="entry-panel">
        
               <asp:Panel ID="Panel6" runat="server">

                   <table cellpadding="3px">
                   <tr>
        <td align="right">
            Product Code
        </td>
         <td align="left">
             <asp:Label ID="lblProductCode" runat="server" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
                   
            
            <tr>
            <td align="right">
                Category
            </td>
             <td align="left">
                 <asp:DropDownList ID="ddlCategory" runat="server" Width="120px">
                     <asp:ListItem Value="0">Cement</asp:ListItem>
                     <asp:ListItem Value="1">Other</asp:ListItem>
                 </asp:DropDownList>
                 
            </td>
            </tr>
            <tr>
            <td align="right" >
                Name</td>
             <td align="left">
                 <asp:TextBox ID="txtProductName" runat="server" Width="306px" 
                    ></asp:TextBox>
                 
            </td>
            </tr>
            
            <tr>
            <td align="right">
                Short Name</td>
             <td align="left">
                <asp:TextBox ID="txtShortName" runat="server" Width="306px" 
                     ></asp:TextBox>
            </td>
            </tr>
           
            <tr>
            
                <td align="right">
                    Product Description</td>
                 <td align="left">
                     <asp:TextBox ID="txtProductDescription" runat="server" Width="306px"></asp:TextBox>
                     
                </td>
                </tr>
            <tr>
                <td align="right" >Reorder
            </td>
            <td align="left">
                <asp:TextBox ID="txtReorder" runat="server" >0</asp:TextBox>
            </td>
            </tr>
            <tr>
            
                <td align="right">
                    Unit Type
                </td>
                 <td align="left">
                     <asp:DropDownList ID="ddlUnitType" runat="server" Width="120px">
                         <asp:ListItem Value="0">BAG</asp:ListItem>
                         <asp:ListItem Value="1">MT</asp:ListItem>
                     </asp:DropDownList>
                     
                </td>
                
            </tr>
            <tr>
            <td align="right" >Distributor Rate  
            </td>
            <td align="left">
                <asp:TextBox ID="txtDistributorRate" runat="server" >0</asp:TextBox>
            </td>
            </tr>
            <tr>
            
                <td align="right">
                    Deal Rate</td>
                 <td align="left">
                     <asp:TextBox ID="txtDealPrice" runat="server">0</asp:TextBox>
                     
                </td>
                </tr>
            <tr>
                <td align="right" >Sales Rate 
            </td>
            <td align="left">
                <asp:TextBox ID="txtSalesRate" runat="server" >0</asp:TextBox>
            </td>
            </tr>

          
            <tr>
            
                <td align="right">
                    Discontinued</td>
                 <td align="left">
                     <asp:CheckBox ID="chkDiscontinued" runat="server" />
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
