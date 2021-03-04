<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Site.Master" AutoEventWireup="true" CodeBehind="VMFApproved.aspx.cs" Inherits="TransportManagerUI.UI.Workshop.VMFApproved" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: "POST",
                url: "VMFApproved.aspx/PageLoad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: Error
            });


            
            //var selected = [];
            //function loadDatatable() {
            //    var oTable = $("#cphMainContent_gvVMFApproved").DataTable({

            //        //"sScrollY": (0.5 * $(window).height()),
            //        //"bPaginate": false,
            //        //"bJQueryUI": true,
            //        //"bScrollCollapse": true,
            //        "processing": true,
            //        "serverSide": true,
            //        "ajax": "scripts/ids-arrays.php",
            //        "rowCallback": function (row, data) {
            //            if ($.inArray(data.DT_RowId, selected) !== -1) {
            //                $(row).addClass('selected');
            //            }
            //        },
            //        select: {
            //            style: 'multi'
            //        }
            //    });

            //    $('#cphMainContent_gvVMFApproved tbody').on('click', 'tr', function () {
            //        var id = this.id;
            //        var index = $.inArray(id, selected);

            //        if (index === -1) {
            //            selected.push(id);
            //        } else {
            //            selected.splice(index, 1);
            //        }

            //        $(this).toggleClass('selected');

            //    });

            //    var getValues = function () {
            //        array_store = document.getElementById("array_store");
            //        document.getElementById("array_disp").innerHTML = array_store.value;
            //    };

            //    //if ($.browser.webkit) {
            //    //    setTimeout(function () {
            //    //        oTable.fnAdjustColumnSizing();
            //    //    }, 10);
            //    //}
            //    //setTimeout(function () {
            //    //    oTable.fnAdjustColumnSizing();
            //    //}, 10);

            //};
        });

    </script>
    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <asp:Panel ID="Panel5" runat="server">
        <asp:UpdatePanel ID="uplMembership" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" 
                    Direction="LeftToRight" ScrollBars="Auto"  CssClass="entry-panel">
                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" HorizontalAlign="Left">
                        
                        <asp:Button ID="btnSave" runat="server" Text="Approved" OnClick="btnSave_Click" />
                        
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" formnovalidate />
                       
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" BorderStyle="Double" BorderWidth="3px" ScrollBars="Auto">
                        <asp:UpdatePanel ID="upListofbasicData" runat="server">
                            <ContentTemplate>

                           
                                <table>
                                    <tr>
                                        <td> <asp:Button ID="btnPendingByDH" runat="server" 
                                     Text="Pending By Department Head"  formnovalidate 
                                     onclick="btnPendingByDH_Click" Visible="False" /></td>
                                        <td> 
                                            <asp:Button ID="btnFactoryHead" runat="server" 
                                                Text="Pending By Factory Head"  formnovalidate 
                                                onclick="btnFactoryHead_Click" Visible="False" /></td>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </td>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" Width="158px" placeholder="Search" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" formnovalidate />
                                         <asp:HiddenField ID="hfPendingBy" runat="server" />
                                            
                                        </td>
                                       
                                    </tr>
                                </table>
                                 <asp:Panel ID="Panel8" runat="server"  BorderStyle="Double" BorderWidth="3px" ScrollBars="Auto">
                                <asp:GridView ID="gvVMFApproved" class="display" runat="server" AutoGenerateColumns="False" 
                                          EmptyDataText="No Data to Show" CellPadding="4" ForeColor="#333333" 
                                         GridLines="Horizontal">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ckALL" runat="server" AutoPostBack="True" 
                                                    oncheckedchanged="ckALL_CheckedChanged" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ckVMFApproved" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IssVouchNo" HeaderText="VMF No">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IssDate" HeaderText="VMF Date">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProdSubCatName" HeaderText="Sub Dept.">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpName" HeaderText="Driver" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                        <ItemStyle Font-Names="SutonnyMJ" />
                                        </asp:BoundField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="" OnClick="LinkButton1_Click" Text="Show Detail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                               
                                
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
