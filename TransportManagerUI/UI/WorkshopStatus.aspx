<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="WorkshopStatus.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.WorkshopStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "casewithdrawn.aspx/PageLoad",
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
        
   <asp:Button ID="btnNewDO" runat="server" Text="New VMF Issue" onclick="btnNewDO_Click"  />
           
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />

     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderWidth="1" 
            BorderColor="Black"  GroupingText="Workshop Status">
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
               
                <asp:BoundField DataField="IssVouchNo" HeaderText="VMF No">
                    <ItemStyle HorizontalAlign="Left" Width="150px"/>
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="IssDate" HeaderText="VMF Date">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>

                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No">
                    <ItemStyle HorizontalAlign="Left" Width="150px"/>
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                <asp:BoundField DataField="EmpName" HeaderText="Driver">
                    <ItemStyle HorizontalAlign="Left" Width="120px"/>
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                   
                    
                   
                    <asp:BoundField DataField="ProdSubCatName" HeaderText="Sub Dept.">
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VMFStatus" HeaderText="Status" >
                    <ItemStyle HorizontalAlign="Left" Width="150px" ForeColor="Red" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>

                    <asp:BoundField DataField="VMFType" HeaderText="VMF Type">
                     <ItemStyle HorizontalAlign="Left" Width="150px" ForeColor="Red" />
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
</asp:Content>