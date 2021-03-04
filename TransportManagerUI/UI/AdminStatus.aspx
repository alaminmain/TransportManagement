<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="AdminStatus.aspx.cs" Inherits="TransportManagerUI.UI.AdminStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "AdminStatus.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="PlToolBox" runat="server">
       
    <asp:UpdatePanel  ID="uplMembership" runat="server">
    <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" >
        <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh"  formnovalidate 
                onclick="btnRefresh_Click"   />
        
   <asp:Button ID="btnNewDO" runat="server" Text="New Admin Work" onclick="btnNewDO_Click"  />
           
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />

     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server"  BorderWidth="1" 
            BorderColor="Black"  GroupingText="Admin Status" CssClass="entry-panel">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
     <table >
     
                            <tr>
                                <td >
                        Work Type</td>
                    <td >
                       <asp:DropDownList ID="ddlWorkType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWorkType_SelectedIndexChanged">
                                        <%--<asp:ListItem Value="01" >All Case</asp:ListItem>--%>
                                        <asp:ListItem Value="02" Selected="True">Pending Case</asp:ListItem>
                                        <asp:ListItem Value="03">Accident</asp:ListItem>
                                        
                                    </asp:DropDownList>
                    </td>
                     <td >
                        
                    </td>
                    <td>
                          <asp:DropDownList ID="ddlVehicleID" runat="server" Width="160px" Visible="False">
                                    </asp:DropDownList>
                    </td>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                     />
                            </td>
                            </tr>
                        </table>

                   <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
    
        <asp:GridView ID="gvlistofBasicData" runat="server" RowStyle-CssClass="GvGrid"
                     EmptyDataText="No Data To Show" GridLines="Horizontal" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     
            Width="100%" Font-Size="Small" 
                     
            ShowFooter="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>               
               <asp:BoundField DataField="AdministrativNo" HeaderText="Admin ID">
                    <ItemStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No">
                    <ItemStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                    <ItemStyle Width="120px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpName" HeaderText="Driver Name">
                    <ItemStyle Width="220px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Dhara" HeaderText="Dhara">
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ItemName" HeaderText="Case Details">
                    <ItemStyle Font-Names="SutonnyMJ" Width="500px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IssDate" HeaderText="Issue Date">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                   
                    <asp:BoundField DataField="WidrawDate" HeaderText="withdraw Date">
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Amount" HeaderText="Amount">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                    <ItemStyle ForeColor="Red" HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
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

        <asp:GridView ID="gvAccident" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Center" 
                Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="AdministrativNo" HeaderText="Admin ID">
                    <ItemStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No">
                    <ItemStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                    <ItemStyle Width="100px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpName" HeaderText="Driver Name">
                    <ItemStyle Width="250px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Remarks" HeaderText="Case Details">
                    <ItemStyle Width="500px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IssDate" HeaderText="Issue Date">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WidrawDate" HeaderText="Event Date">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                    <ItemStyle ForeColor="Red" HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
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
         </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
</div>
         
                  </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    
     
        
    </asp:Panel>

                
        </ContentTemplate>

</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMembership" >
            <ProgressTemplate>
            <div class="UpdateProgress">
                <img src="../Images/ajax-loader.gif" alt="Processing..."/>
            </div>
            </ProgressTemplate>
    </asp:UpdateProgress>
    </asp:Panel>
     <script type="text/javascript">

         $(document).ready(function () {
             $('#' + '<%= ddlVehicleID.ClientID %>').select2({

         });
        
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
         function EndRequestHandler(sender, args) {
             $('#' + '<%= ddlVehicleID.ClientID %>').select2({

         });
         
     }
 });
          </script>
</asp:Content>