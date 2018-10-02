<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="RentService.aspx.cs" Inherits="KMO.RentService" %>
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

        $(function () {
            $(".close").click(function () {
                $("#myAlert").alert();
            });
        });

        $(document).ready(function () {

            $('.alert-autocloseable-success').delay(5000).fadeOut("slow", function () {
                // Animation complete.
                $('#autoclosable-btn-success').prop("disabled", false);
            });

            $('.alert-autocloseable-warning').delay(3000).fadeOut("slow", function () {
                // Animation complete.
                $('#autoclosable-btn-warning').prop("disabled", false);
            });

            $('.alert-autocloseable-danger').delay(5000).fadeOut("slow", function () {
                // Animation complete.
                $('#autoclosable-btn-danger').prop("disabled", false);
            });

            //date picker
            var dpPF = $('#<%=txtPeriodFrom.ClientID%>');
            dpPF.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
            var dpPT = $('#<%=txtPeriodTo.ClientID%>');
            dpPT.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

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
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="CPH_BODY_CONTENT" runat="server">
    <div class="container">
        <div class="row col-xs-12 col-sm-12">
            <asp:ScriptManager runat="server" ID="ScriptManager1" />

            <!-- message area -->
            <div class="col-xs-12 col-sm-12">
                <div runat="server" id="mySuccess" class="alert alert-success alert-autocloseable-success">
                    <a href="#" class="close" data-dismiss="alert">&times;</a>
                    <strong>SUCCESS!</strong></br>
                    <asp:Label ID="lblTitleMySuccess" runat="server"></asp:Label></br>
                    <asp:Label ID="lblMySuccess" runat="server"></asp:Label> 
                </div>
                <div runat="server" id="myWarning" class="alert alert-warning alert-autocloseable-warning">
                    <a href="#" class="close" data-dismiss="alert">&times;</a>
                    <strong>WARNING!</strong></br>
                    <asp:Label ID="lblTitleMyWarning" runat="server"></asp:Label></br>
                    <asp:Label ID="lblMyWarning" runat="server"></asp:Label>
                </div>         
                <div runat="server" id="myError" class="alert alert-danger alert-autocloseable-danger">
                    <a href="#" class="close" data-dismiss="alert">&times;</a>
                    <strong>ERROR!</strong></br>
                    <asp:Label ID="lblTitleMyError" runat="server"></asp:Label></br>
                    <asp:Label ID="lblMyError" runat="server"></asp:Label>
                </div>        
            </div>

            <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate> 
            <div class="col-xs-12 col-sm-12"><h4>Rent & Service</h4></div>
            <div class="col-xs-12 col-sm-12" style="padding:0 5 0 0;" >
                <div class="row">
                    <div class="col-xs-12 col-sm-1">Floor</div>
                    <div class="col-xs-12 col-sm-1" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtFloor" runat="server" MaxLength="3" placeholder="Floor" Width="50px" OnKeyPress="return isNumberKey(this, event);" Enabled="false"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-12 col-sm-1">Suite No</div>
                    <div class="col-xs-12 col-sm-1" align="left">                        
                        <asp:TextBox class="form-control input-sm" ID="txtSuiteNo" runat="server" MaxLength="3" placeholder="Suite No" Width="50px" OnKeyPress="return isNumberKey(this, event);" Enabled="false"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-12 col-sm-1">Rent Period</div>
                    <div class="col-xs-12 col-sm-1" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtRentPeriod" runat="server" MaxLength="3" placeholder="Rent Period" Width="30px" OnKeyPress="return isNumberKey(this, event);" Enabled="false"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-12 col-sm-1">Rent Area</div>
                    <div class="col-xs-12 col-sm-1" align="left">                        
                        <asp:TextBox class="form-control input-sm" ID="txtRentArea" runat="server" MaxLength="3" placeholder="Rent Area" Width="60px" OnKeyPress="return isNumberKey(this, event);" Enabled="false"></asp:TextBox>                        
                    </div>
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-1">CID</div>
                    <div class="col-xs-12 col-sm-2" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtCompanyID" runat="server"  placeholder="Company ID" Width="87%" Visible="false" ></asp:TextBox>
                        <asp:TextBox class="form-control input-sm" ID="txtCFSID" runat="server"  placeholder="Client ID" Width="100%" Enabled="false"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-12 col-sm-2">Company Name</div>
                    <div class="col-xs-12 col-sm-2" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtCompanyName" runat="server"  placeholder="Company Name" Width="100%" Enabled="false"></asp:TextBox>                        
                    </div>
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-4">Rent & Service Charge</div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-2">1.1 Rental Area</div>
                    <div class="col-xs-12 col-sm-3">
                        <asp:Label ID="lblRentalArea" runat="server">xxxxxx</asp:Label> m2 x
                        <asp:Label ID="lblRentalPeriod" runat="server">xxxxxx</asp:Label> 
                        <asp:Label ID="lblRentalPeriodMY" runat="server">xxxxxx</asp:Label> x Rp. 
                        <asp:Label ID="lblRentalCharge" runat="server">xxxxxx</asp:Label>
                    </div>
                    <div class="col-xs-12 col-sm-2" style="text-align:right"><asp:Label ID="lblRentalAmount" runat="server">xxxxxx</asp:Label></div>
                </div>                        
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-2">1.2 Service Area</div>
                    <div class="col-xs-12 col-sm-3">
                        <asp:Label ID="lblServiceArea" runat="server">xxxxxx</asp:Label> m2 x
                        <asp:Label ID="lblServicePeriod" runat="server">xxxxxx</asp:Label> 
                        <asp:Label ID="lblServicePeriodMY" runat="server">xxxxxx</asp:Label> x Rp. 
                        <asp:Label ID="lblServiceCharge" runat="server">xxxxxx</asp:Label>
                    </div>
                    <div class="col-xs-12 col-sm-2" style="text-align:right"><asp:Label ID="lblServiceAmount" runat="server">xxxxxx</asp:Label></div>
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-2">1.3 Others</div>
                    <div class="col-xs-12 col-sm-3">
                        <asp:Label ID="lblOtherArea" runat="server">xxxxxx</asp:Label> m2 x
                        <asp:Label ID="lblOtherPeriod" runat="server">xxxxxx</asp:Label> 
                        <asp:Label ID="lblOtherPeriodMY" runat="server">xxxxxx</asp:Label> x Rp. 
                        <asp:Label ID="lblOtherCharge" runat="server">xxxxxx</asp:Label>
                    </div>
                    <div class="col-xs-12 col-sm-2" style="text-align:right"><asp:Label ID="lblOtherAmount" runat="server">xxxxxx</asp:Label></div>
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-3">&nbsp</div>
                    <div class="col-xs-12 col-sm-2">Subtotal</div>
                    <div class="col-xs-12 col-sm-2" style="text-align:right">
                        <asp:Label ID="lblSubTotal" runat="server">xxxxxx</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-3">&nbsp</div>
                    <div class="col-xs-12 col-sm-2">10% PPN</div>
                    <div class="col-xs-12 col-sm-2" style="text-align:right">
                        <asp:Label ID="lblPPN" runat="server">xxxxxx</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-3">&nbsp</div>
                    <div class="col-xs-12 col-sm-2">Materai</div>
                    <div class="col-xs-12 col-sm-2" style="text-align:right">
                        <asp:Label ID="lblMaterai" runat="server">xxxxxx</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-3">&nbsp</div>
                    <div class="col-xs-12 col-sm-2">Grand Total</div>
                    <div class="col-xs-12 col-sm-2" style="text-align:right">
                        <asp:Label ID="lblGrandTotal" runat="server">xxxxxx</asp:Label>
                    </div>
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-10">&nbsp</div>
                    <div class="col-xs-12 col-sm-2" >
                        <asp:Button CssClass="btn btn-info btn-xs" ID="btnSearch" runat="server" Text="Search Tenant" OnClick="btnSearch_Click"/>
                    </div>
                </div>                      
                <div class="row">&nbsp</div>
                <!--  -->
                <div class="row" style="display: none;">
                    <div class="col-xs-12 col-sm-1" align="left">
                        <asp:TextBox runat="server" ID="txtKVA" class="form-control input-sm"/>

                        <asp:TextBox runat="server" ID="txtRentalArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtRentalPeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtRentalPeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtRentalCharge" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtServiceArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtServicePeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtServicePeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtServiceCharge" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtOtherArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtOtherPeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtOtherPeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtOtherCharge" class="form-control input-sm"/>

                        <asp:TextBox runat="server" ID="txtDepositRentalArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositRentalPeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositRentalPeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositRentalCharge" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositServiceArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositServicePeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositServicePeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositServiceCharge" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositOtherArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositOtherPeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositOtherPeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtDepositOtherCharge" class="form-control input-sm"/>

                        <asp:TextBox runat="server" ID="txtParkingReservedArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingReservedPeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingReservedPeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingReservedCharge" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingUnreservedArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingUnreservedPeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingUnreservedPeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingUnreservedCharge" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingMotorcycleArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingMotorcyclePeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingMotorcyclePeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingMotorcycleCharge" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingOtherArea" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingOtherPeriod" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingOtherPeriodMY" class="form-control input-sm"/>
                        <asp:TextBox runat="server" ID="txtParkingOtherCharge" class="form-control input-sm"/>
                    </div>
                </div>
                
                <!-- input area -->
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-2">Payment Period</div>
                    <div class="col-xs-12 col-sm-2">
                        <asp:TextBox runat="server" ID="txtPeriodFrom" class="form-control input-sm" 
                        OnKeyPress="return isNumberKey(this, event);"
                        placeholder="From Date" style="text-align:left;" enabled="false"/>
                    </div>
                    <div class="col-xs-12 col-sm-1">
                        To
                    </div>
                    <div class="col-xs-12 col-sm-2">         
                        <asp:TextBox runat="server" ID="txtPeriodTo" class="form-control input-sm" 
                        OnKeyPress="return isNumberKey(this, event);"
                        placeholder="To Date" style="text-align:left;" enabled="false"/>
                    </div>
                </div>
                <div class="row">&nbsp</div>
                <div align="right">
                    <div class="row">&nbsp</div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                    <div class="row">&nbsp</div>
                </div> 
                <div class="row">&nbsp</div>
                <div class="row">
                    <!-- GridView -->
                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center"
                        AutoGenerateColumns="False" AllowPaging="True"
                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                        CssClass="GridViewStyle" Width="100%" DataKeyNames="ID"
                        ShowHeader="true" ShowFooter="false" PageSize="10" valign="left"
                        OnRowCommand="GridView1_RowCommand"
                        OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333"
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
                        <asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="70px"
                            ButtonType="Button" Text="Void" HeaderText="Void"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        
                        <asp:BoundField DataField="Month" HeaderText="Month" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-HorizontalAlign="Center"/>                        
                        <asp:BoundField DataField="InvNo" HeaderText="Invoice No" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="PrintDate" HeaderText="Print Date" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="FromPeriod" HeaderText="Payment Period From" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="ToPeriod" HeaderText="Payment Period To" ItemStyle-HorizontalAlign="Center"/>
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
            </ContentTemplate>
            </asp:UpdatePanel>
            
            <!--
                <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="70px" 
                            ButtonType="Button" Text="Edit" HeaderText="Edit"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField> 
                -->
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
                        <button class="btn btn-info" data-dismiss="Close" 
                        aria-hidden="true">Close</button>
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
