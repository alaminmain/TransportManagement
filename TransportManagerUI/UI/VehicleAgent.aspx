<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="VehicleAgent.aspx.cs" Inherits="TransportManagerUI.UI.VehicleAgent" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <title>Transport Agent</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel5" runat="server">
        <asp:UpdatePanel  ID="uplMembership" runat="server">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC" 
        BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
        <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left" >
        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate  />
        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate   />
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  />
            <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate 
                onclick="btnReport_Click"  />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate OnClick="btnCancel_Click"   />
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderStyle="Double" 
            BorderWidth="1px">
         <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
     <table >
                            <tr>
                            <td>
                                <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                                    formnovalidate />
                            </td>
                            </tr>
                        </table>
        <asp:GridView ID="gvlistofBasicData" runat="server" AllowPaging="True" PageSize="15" 
                     EmptyDataText="No Data To Show" GridLines="None" CellPadding="4" 
                     ForeColor="#333333" AutoGenerateColumns="False" 
                     onpageindexchanging="gvlistofBasicData_PageIndexChanging" OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="AgentID" HeaderText="AgentID" />
                <asp:BoundField DataField="AgentName" HeaderText="AgentName" />
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
                <asp:Button ID="btnSearchOk" runat="server" Text="Ok" Width="60px" />
            </td>
            <td>
            <asp:Button ID="btnSearchCancel" runat="server" Text="Cancel"  />
            </td>
            </tr>
            </table>
                  </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
    
     
        
    </asp:Panel>
 
       <asp:Panel ID="Panel2" runat="server" ScrollBars="Horizontal" 
        ForeColor="Black" GroupingText="Transport Agent">
        
               <asp:Panel ID="Panel6" runat="server" CssClass="entry-panel">

                   <table cellpadding="3px">
                   <tr>
        <td align="right">
            Agent Code</td>
         <td align="left">
             <asp:Label ID="lblAgentId" runat="server" CssClass="IdStyle"></asp:Label>
        </td>
        </tr>
                  
            
            <tr>
            <td align="right">
                &nbsp;Agent Name
            </td>
             <td align="left">
                 <asp:TextBox ID="txtAgentName" runat="server" Width="306px" required></asp:TextBox>
                 
            </td>
            </tr>
            <tr>
            <td align="right">
                &nbsp;Contact Name
            </td>
             <td align="left">
                 <asp:TextBox ID="txtContactname" runat="server" Width="306px" ></asp:TextBox>
                 
            </td>
            </tr>
           
            <tr>
            
                <td align="right">
                    Address1
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtAddres1" runat="server" Width="306px"></asp:TextBox>
                     
                </td>
                </tr>
            <tr>
                <td align="right" >Address2
            </td>
            <td align="left">
                <asp:TextBox ID="txtAddress2" runat="server" Width="306px" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            
                <td align="right">
                    Address3
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtAddress3" runat="server" Width="306px"></asp:TextBox>
                     
                </td>
                
            </tr>
            <tr>
            <td align="right" >PostCode</td>
            <td align="left">
                <asp:TextBox ID="txtPostCode" runat="server" ></asp:TextBox>
            </td>
            </tr>
            <tr>
            
                <td align="right">
                    Phone
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                     
                </td>
                </tr>
                <tr>
            
                <td align="right">
                    Fax
                </td>
                 <td align="left">
                     <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                     
                </td>
                </tr>
            <tr>
                <td align="right" >Mobile 
            </td>
            <td align="left">
                <asp:TextBox ID="txtMobile" runat="server" ></asp:TextBox>
            </td>
            </tr>
             <tr>
                <td align="right" >Email 
            </td>
            <td align="left">
                <asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox>
            </td>
            </tr>
             <tr>
                <td align="right" >Web Addres 
            </td>
            <td align="left">
                <asp:TextBox ID="txtWebAddress" runat="server" Width="306px" ></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td align="right" >Remarks 
            </td>
            <td align="left">
                <asp:TextBox ID="txtRemarks" runat="server" Width="306px" ></asp:TextBox>
            </td>
            </tr>

          
            <tr>
            
                <td align="right">
                    Status
                </td>
                 <td align="left">
                     <asp:DropDownList ID="ddlStatus" runat="server">
                     <asp:ListItem Value="0" Text="Active"></asp:ListItem>
                     <asp:ListItem Value="1" Text="InActive"></asp:ListItem>
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
