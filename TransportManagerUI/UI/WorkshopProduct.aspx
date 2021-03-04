<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="WorkshopProduct.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.WorkshopProduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "WorkshopProduct.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });

        });

    </script>
    });

    

    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>



    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>



    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>



    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>



    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>



    <style type="text/css">
        .auto-style1 {
            text-align: left;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel5" runat="server">
        <asp:UpdatePanel ID="uplMembership" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" ForeColor="Black" BackColor="#CCCCCC"
                    BorderColor="#3399FF" Direction="LeftToRight" ScrollBars="Auto">
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left">
                        <asp:Button ID="btnAddNew" runat="server" Text="New" OnClick="btnAddNew_Click" formnovalidate />
                        <asp:Button ID="btnShowList" runat="server" Text="List" OnClick="btnShowList_Click" formnovalidate />
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" formnovalidate OnClick="btnReport_Click" />


                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" formnovalidate />
                        <asp:HiddenField ID="hfShowList" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="hfShowList_ModalPopupExtender" runat="server" BehaviorID="hfShowList_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfShowList" PopupControlID="Panel7" OkControlID="btnSearchOk" CancelControlID="btnSearchCancel" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" BackColor="White" BorderStyle="Double" BorderWidth="3px" ScrollBars="Auto">
                        <asp:UpdatePanel ID="upListofbasicData" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" formnovalidate />
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
                                        <asp:BoundField DataField="ProductCode" HeaderText="ProductCode"></asp:BoundField>
                                        <asp:BoundField DataField="StoreCode" HeaderText="StoreCode" />
                                        <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                        <asp:BoundField DataField="ProductBName" HeaderText="ProductBName" >
                                        <ItemStyle Font-Names="SutonnyMJ" Font-Size="Medium" />
                                        </asp:BoundField>
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
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" BorderStyle="Outset" BorderWidth="3px" BorderColor="Silver" GroupingText="Product" CssClass="entry-panel">

                    <asp:Panel ID="Panel6" runat="server" GroupingText="" CssClass="entry-panel" ForeColor="Black">

                        <table style="border: 1px solid Green;">
                            <tr>
                                <td align="right">Category</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductCategoryID" runat="server" AutoPostBack="True"
                                        
                                        OnSelectedIndexChanged="ddlProductCategoryID_SelectedIndexChanged" Width="100px"
                                        >
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td class="style4" align="right">Sub Category</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlProductSubCategoryID" runat="server"
                                        
                                       AutoPostBack="True" Width="100px">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td align="right">Product Code</td>
                                <td align="left">
                                    <asp:Label ID="lblProductCode" runat="server" CssClass="IdStyle"></asp:Label>
                                    &nbsp;&nbsp;</td>

                            </tr>
                            <tr>
                                <td class="style51" align="right">Store Code</td>
                                <td align="left">
                                    <asp:TextBox ID="txtStoreCode" runat="server" BackColor="#66CCFF"
                                        ReadOnly="True" Enabled="False"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="right">Product Name</td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                                    &nbsp;</td>

                            </tr>
                            <tr>
                                <td align="right">
                                    Product Name (Bangla)
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductBName" runat="server" Font-Names="SutonnyMJ"
                                        Font-Size="Small" Width="200px"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="style4" align="right">Descripton</td>
                                <td align="left">
                                    <asp:TextBox ID="txtProductDiscription" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                </td>

                            </tr>
                           


                            <tr>
                                <td align="right">Reorder</td>
                                <td align="left">
                                    <asp:TextBox ID="txtReorder" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" align="right">Unit Type</td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlUniteType" runat="server" Height="22px"
                                        Style="margin-bottom: 0px" Width="142px">
                                        <asp:ListItem>PCS</asp:ListItem>
                                        <asp:ListItem>PACK</asp:ListItem>
                                        <asp:ListItem>DOZEN</asp:ListItem>
                                        <asp:ListItem>POUND</asp:ListItem>
                                        <asp:ListItem>KG</asp:ListItem>
                                        <asp:ListItem>M.T</asp:ListItem>
                                        <asp:ListItem>PAIR</asp:ListItem>
                                        <asp:ListItem>FEET</asp:ListItem>
                                        <asp:ListItem>RFT</asp:ListItem>
                                        <asp:ListItem>SFT</asp:ListItem>
                                        <asp:ListItem>LITTER</asp:ListItem>
                                        <asp:ListItem>COIL</asp:ListItem>
                                        <asp:ListItem>METER</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Unit Price</td>
                                <td align="left">
                                    <asp:TextBox ID="txtDistPrice" runat="server">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">Deal Price</td>
                                <td align="left">
                                    <asp:TextBox ID="txtDealerPrice" runat="server" Visible="True">0</asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="style4" align="right">Sale Price</td>
                                <td align="left">
                                    <asp:TextBox ID="txtSalePrice" runat="server" Visible="True" Wrap="False">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" align="right">Add Store Info</td>
                                <td align="left">
                                    <asp:CheckBox ID="chkAddStoreInfo" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel4" runat="server">
                        <table >
                            <tr>
                                
                                <td class="style54">Preset Balance</td>
                                <td>
                                    <asp:TextBox ID="txtPhyStock" runat="server"></asp:TextBox>
                                </td>

                            </tr>

                             <tr>
                               
                                <td colspan="2">
                                    &nbsp;</td>
                                
                            </tr>
                        </table>
                            </asp:Panel>
                    </asp:Panel>



                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />

            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="uplMembership">
            <ProgressTemplate>
                <div class="UpdateProgress">
                    <img src="../Images/ajax-loader.gif" alt="Processing..." />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
</asp:Content>
