<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="Others.aspx.cs" Inherits="KMO.Others" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="CPH_HEAD" runat="server">
    <script type='text/javascript'>
        function isNumberKey(sender, evt) {
            var txt = sender.value;
            var dotcontainer = txt.split('.');
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (!(dotcontainer.length == 1 && charCode == 46) && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        $(document).ready(function () {

        });
    </script>

    <style>
        .GridViewStyle td {
            padding: 2px 10px;
        }
        
        .modal-dialog1{
            position: absolute;
            left: 50%;           
                      
            top: 30%;
            margin-left: -200px;  
            margin-top: -25px;

            padding: 20px
        } 

        .datepicker{z-index:1151 !important;}
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="CPH_BODY_CONTENT" runat="server">
    <div class="container">
        <asp:ScriptManager runat="server" ID="ScriptManager1" />
        <div class="row col-xs-12 col-sm-12">
            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate> 
                    <div class="col-xs-12 col-sm-12"><h4>Parking Service</h4></div>
                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                        <div class="col-xs-12 col-sm-12" style="padding:0 5 0 0;" >
                        <!-- -->
                        <div class="row">
                            <div class="col-xs-12 col-sm-1">CID</div>
                            <div class="col-xs-12 col-sm-2" align="left">
                                <asp:TextBox class="form-control input-sm" ID="txtCompanyID" runat="server"  placeholder="Company ID" Width="87%" Visible="false" ></asp:TextBox>
                                <asp:TextBox class="form-control input-sm" ID="txtCFSID" runat="server"  placeholder="Client ID" Width="100%" Enabled="false"></asp:TextBox>                        
                            </div>
                            <div class="col-xs-12 col-sm-2">Company Name</div>
                            <div class="col-xs-12 col-sm-3" align="left">
                                <asp:TextBox class="form-control input-sm" ID="txtCompanyName" runat="server"  placeholder="Company Name" Width="100%" Enabled="false"></asp:TextBox>                        
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-1">Floor</div>
                            <div class="col-xs-12 col-sm-1" align="left">
                                <asp:TextBox class="form-control input-sm" ID="txtFloor" runat="server" MaxLength="3" placeholder="Floor" Width="50px" OnKeyPress="return isNumberKey(this, event);" Enabled="false"></asp:TextBox>                        
                            </div>
                            <div class="col-xs-12 col-sm-1">Suite No</div>
                            <div class="col-xs-12 col-sm-1" align="left">                        
                                <asp:TextBox class="form-control input-sm" ID="txtSuiteNo" runat="server" MaxLength="3" placeholder="Suite No" Width="50px" OnKeyPress="return isNumberKey(this, event);" Enabled="false"></asp:TextBox>                        
                            </div>                            
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-10">&nbsp</div>
                            <div class="col-xs-12 col-sm-2" >
                                <asp:Button CssClass="btn btn-info btn-xs" ID="btnSearch" runat="server" Text="Search Tenant" OnClick="btnSearch_Click"/>
                            </div>
                        </div>

                        <!-- -->
                        <div class="row">&nbsp</div>
                        <div class="row">                            
                            <div class="col-xs-12 col-sm-1">Item</div>
                            <div class="col-xs-12 col-sm-1" align="left">
                                <asp:TextBox ID="txtItem" class="form-control input-sm" runat="server" placeholder="Item" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-1">Description</div>
                            <div class="col-xs-12 col-sm-6" align="left">
                                <asp:TextBox ID="txtDescription" class="form-control input-sm" runat="server" 
                                    Rows="4" MaxLength="250" style="text-align:left;" TextMode="MultiLine" 
                                    placeholder="Item description"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-1">Amount</div>
                            <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtAmount" class="form-control input-sm" runat="server" placeholder="Item Amount" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-10">&nbsp</div>
                            <div class="col-xs-12 col-sm-2">
                                <asp:Button ID="btnAddParking" runat="server" Text="Add" CssClass="btn btn-success" OnClick="btnAddParking_Click"/>
                                <asp:Button ID="btnCancelParking" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancelParking_Click"/>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>             
                        <div class="row">
                            <!-- GridView -->
                            <asp:UpdatePanel ID="upParking" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>                                                
                                    <asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center"
                                        AutoGenerateColumns="False" AllowPaging="True"
                                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                                        CssClass="GridViewStyle" Width="95%" DataKeyNames="OthersID"
                                        ShowHeader="true" ShowFooter="false" PageSize="10" valign="left"
                                        OnRowCommand="GridView2_RowCommand"
                                        OnPageIndexChanging="GridView2_PageIndexChanging" ForeColor="#333333"
                                        >
                                        <AlternatingRowStyle BackColor="#EEE" />
                                        <SelectedRowStyle BackColor="#FFF" Font-Bold="False" ForeColor="Navy" />
                    
                                        <Columns>
                                        <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="center"
                                            ItemStyle-Width="30px">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                                            ItemStyle-Width="90px"
                                            ButtonType="Button" Text="Edit" HeaderText="Edit Record"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                                            ItemStyle-Width="90px"
                                            ButtonType="Button" Text="Void" HeaderText="Void Record"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                                        </asp:ButtonField>
                        
                                        <asp:BoundField DataField="InvNo" HeaderText="Invoice OTH"/>
                                        <asp:BoundField DataField="Item" HeaderText="Item"/>
                                        <asp:BoundField DataField="Description" HeaderText="Description"/>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                    </Columns>

                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle CssClass="tbl_foot" />
                                        <HeaderStyle CssClass="tbl_head" />
                                        <PagerStyle CssClass="tbl_page" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="#FFFFFF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="False" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">&nbsp</div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Select tenant form -->
            <div class="modal fade" id="selectTenantModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
                <div class="modal-dialog" style="z-index: 1070">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <strong><asp:Label ID="Label2" runat="server">Select tenant</asp:Label></strong>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                    <!-- GridView -->
                                    <asp:GridView ID="GridViewSelectTenant" runat="server" HorizontalAlign="Center"
                                        AutoGenerateColumns="False" AllowPaging="True"
                                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                                        CssClass="GridViewStyle" Width="100%" DataKeyNames="ID"
                                        ShowHeader="true" ShowFooter="false" PageSize="10" valign="left"  
                                        OnRowCommand="GridViewSelectTenant_RowCommand"
                                        OnPageIndexChanging="GridViewSelectTenant_PageIndexChanging">

                                        <AlternatingRowStyle BackColor="#EEE" />
                                        <SelectedRowStyle BackColor="#FFF" Font-Bold="False" ForeColor="Navy" />
                    
                                        <Columns>
                                            <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="selectRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                                                ItemStyle-Width="70px"
                                                ButtonType="Button" Text="Select" HeaderText="Select"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                                <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                                            </asp:ButtonField>
                                            
                                            <asp:BoundField DataField="CFSIDx" HeaderText="CID"/>
                                            <asp:BoundField DataField="Name" HeaderText="Company Name"/>
                                            <asp:BoundField DataField="SuiteNo" HeaderText="Suite No"/>
                                        </Columns>

                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle CssClass="tbl_foot" />
                                        <HeaderStyle CssClass="tbl_head" />
                                        <PagerStyle CssClass="tbl_page" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <AlternatingRowStyle BackColor="#FFFFFF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="False" />
                                    </asp:GridView>

                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <button  type="button" class="btn btn-info" data-dismiss="modal" aria-hidden="Close">Close</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Delete Modal Start Here --->
            <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H2">Delete Master Item</h4>
                    </div>

                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="form-horizontal">
                                    Are you sure to delete this record?
                                    <asp:HiddenField ID="hfCode" runat="server" />
                                    <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
                                
                                    <div class="form-group">
                                        <label for="delete alasan" class="col-sm-3 control-label">
                                            Reason</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="txtReasonToDelete" class="form-control input-sm" 
                                                Rows="3" Width="250px" placeholder="Reason to delete" 
                                                MaxLength="100" style="text-align:left;" TextMode="MultiLine" />
                                        </div>
                                    </div>                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click"/>
                        <button type="button" class="btn btn-info" data-dismiss="modal aria-hidden="Close">Close</button>
                    </div>
                </div>
                </div>
            </div>

            <!-- Message form -->
            <div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
                <div class="modal-dialog1" style="z-index: 1060">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <strong><asp:Label ID="lblMessageModal_Title" runat="server"></asp:Label></strong>
                                </div>

                                <div class="modal-body">
                                    <asp:Label ID="lblMessageModal_Body" runat="server"></asp:Label>
                                </div>

                                <div class="modal-footer">
                                    <button  type="button" class="btn btn-info" data-dismiss="modal" aria-hidden="Close">Ok</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="FooterContent" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
