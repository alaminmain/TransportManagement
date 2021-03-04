<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="Technician.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.Technician" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

    $(document).ready(function () {

        $.ajax({
            type: "POST",
            url: "Technician.aspx/PageLoad",
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
            <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate OnClick="btnReport_Click" />


     <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" formnovalidate/>
        <asp:HiddenField ID="hfShowList" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        </asp:Panel>
      <asp:Panel ID="Panel7" runat="server" BackColor="White"  BorderStyle="Double" BorderWidth="3px" ScrollBars="Auto" >
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
                     OnSelectedIndexChanged="gvlistofBasicData_SelectedIndexChanged" 
                     OnPageIndexChanging="gvlistofBasicData_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
               <asp:BoundField DataField="EmpCode" HeaderText="Code">
                  
                    </asp:BoundField>
                <asp:BoundField DataField="EmpName" HeaderText="Name" />
                 <asp:BoundField DataField="FatherName" HeaderText="FatherName" />
                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
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
       <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Technician" CssClass="entry-panel">
        
               <asp:Panel ID="Panel6" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black">

                   <table>
                <tr>
                    <td class="AlignRight">
                        Id</td>
                    <td  align="left">
                        <asp:Label ID="txtTechnicianId" runat="server" Width="200px" CssClass="IdStyle"></asp:Label>
                         
                        &nbsp;
                        <asp:Label ID="LblMsg" runat="server" CssClass="FillUpText" ForeColor="#006600"></asp:Label>
                         
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Name</td>
                    <td  align="left">
                        <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Father Name</td>
                    <td  align="left">
                        <asp:TextBox ID="txtFatherName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Mother Name</td>
                    <td  align="left">
                        <asp:TextBox ID="txtMotherName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Blood Group</td>
                    <td  align="left">
                        <asp:DropDownList ID="ddlBloodGroup" runat="server" Width="120px" >
                            <asp:ListItem>A+</asp:ListItem>
                            <asp:ListItem>A-</asp:ListItem>
                            <asp:ListItem>B+</asp:ListItem>
                            <asp:ListItem>B-</asp:ListItem>
                            <asp:ListItem>AB+</asp:ListItem>
                            <asp:ListItem>AB-</asp:ListItem>
                            <asp:ListItem>O+</asp:ListItem>
                            <asp:ListItem>O-</asp:ListItem>
                           
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Sub Department</td>
                    <td  align="left">
                        <asp:DropDownList ID="ddlSubDepartment" runat="server" Width="120px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Designation</td>
                    <td  align="left">
                        <asp:DropDownList ID="ddlDesignation" runat="server" Width="120px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Address1</td>
                    <td  align="left">
                        <asp:TextBox ID="txtAddress1" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Address 2</td>
                    <td  align="left">
                        <asp:TextBox ID="txtAddress2" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Address 3</td>
                    <td  align="left">
                        <asp:TextBox ID="txtAddress3" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Mobile</td>
                    <td  align="left">
                        <asp:TextBox ID="txtMobile" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        Status</td>
                    <td  align="left">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="120px" >
                            <asp:ListItem>Continued</asp:ListItem>
                            <asp:ListItem>Discontinued</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="AlignRight">
                        &nbsp;</td>
                    <td  align="left">
                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="#CC0000"></asp:Label>
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
