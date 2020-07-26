<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="AssignMenuToRole.aspx.cs" Inherits="TransportManagerUI.UI.AdminPanel.AssignMenuToRole" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "AssignMenuToRole.aspx/PageLoad",
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
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" />
        
        </asp:Panel>
      <asp:Panel ID="Panel7" runat="server" BackColor="White"  BorderStyle="Double" BorderWidth="3px" ScrollBars="Auto" >
         <asp:UpdatePanel ID="upListofbasicData" runat="server">
             <ContentTemplate>
             <table>
                 
             <tr>
             <td>Select Role</td>
             <td><asp:DropDownList ID="ddlRoles" runat="server" Width="120px" 
                     onselectedindexchanged="ddlRoles_SelectedIndexChanged" AutoPostBack="True">
                 </asp:DropDownList></td>
             </tr>
             </table>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvlistofBasicData" runat="server" 
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                    
                     >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="chkActive">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsActive" runat="server" Enabled="true" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="menuId" HeaderText="Menu Id" />
                <asp:BoundField DataField="menuName" HeaderText="menuName" />
                <asp:BoundField DataField="menuUrl" HeaderText="menuUrl" />
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
                &nbsp;</td>
            <td>
                &nbsp;</td>
            </tr>
            </table>
                  </ContentTemplate>
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
                <img src="../../Images/ajax-loader.gif" alt="Processing..."/>
            </div>
            </ProgressTemplate>
    </asp:UpdateProgress>
   </asp:Panel>
</asp:Content>
