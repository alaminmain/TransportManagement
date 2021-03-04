<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="PaperRenewalStatus.aspx.cs" Inherits="TransportManagerUI.UI.PaperRenewalStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "PaperRenewalStatus.aspx/PageLoad",
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
        
   <asp:Button ID="btnNewDO" runat="server" Text="New Paper Renewal" 
                onclick="btnNewDO_Click"  />
           
            <asp:Button ID="btnReport" runat="server" Text="Report" 
                onclick="btnReport_Click"   />

     <asp:Button ID="btnCancel" runat="server" Text="Cancle" formnovalidate OnClick="btnCancel_Click"  />
      
        </asp:Panel>
        <br />
      <asp:Panel ID="Panel7" runat="server"  BorderWidth="1" 
            BorderColor="Black"  GroupingText="Paper Renewal Status" CssClass="entry-panel">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional" >
             <ContentTemplate>
     <table >
     <tr>
   
          <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search Vehicle" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                     />
                            </td>
                            <td>
                                &nbsp;</td>
                     
     </tr>
                         
                        </table>

                   <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>

    <div style="overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
    
        <asp:GridView ID="gvPaperRenewal" runat="server" CellPadding="4" Width="100%" 
                ForeColor="#333333" AutoGenerateColumns="False" HorizontalAlign="Center" 
                GridLines="Horizontal" ShowFooter="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" >
                    <ItemStyle Width="150px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                  <%--  <asp:BoundField DataField="ItemName" HeaderText="Expire Paper" >
                    <ItemStyle Width="200px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                     
                    <asp:BoundField DataField="ItemName" HeaderText="Paper Name" >
                    <ItemStyle Width="200px" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>                 
                   
                  

                    <asp:BoundField DataField="ExpDate" HeaderText="Expiry Date" 
                        NullDisplayText="No Data" DataFormatString="{0:d}">
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DueDate" HeaderText="Duration" >
                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Red" />
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                   
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#BACFF8" />
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
