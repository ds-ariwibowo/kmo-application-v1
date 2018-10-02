<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="copOTH.aspx.cs" Inherits="KMO.copOTH" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="CPH_HEAD" runat="server">
    <script type='text/javascript'>
        function isNumberKey(sender, evt) {
            var txt = sender.value;
            var dotcontainer = txt.split('.');
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (!(dotcontainer.length == 1 && charCode == 46) && charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45)
                return false;

            return true;
        }       

        $(document).ready(function () {


            //message animation
            $('.alert-autocloseable-success').delay(5000).fadeOut("slow", function () {
                $('#autoclosable-btn-success').prop("disabled", false);
            });

            $('.alert-autocloseable-warning').delay(3000).fadeOut("slow", function () {
                $('#autoclosable-btn-warning').prop("disabled", false);
            });

            $('.alert-autocloseable-danger').delay(5000).fadeOut("slow", function () {
                $('#autoclosable-btn-danger').prop("disabled", false);
            });

            //date picker
            var dpAdd = $('#<%=txtReceivedDate.ClientID%>');
            dpAdd.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            //total payment calculation
            $('#<%=txtPaymentNominal.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtVariance.ClientID%>').val(txtVal); });            
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
        <div class="col-xs-12 col-sm-12">
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
            
            <div class="col-xs-12 col-sm-12"><h4>COP: Others</h4></div>
            <div class="row">
                <div class="col-xs-12 col-sm-2">Payment Period</div>
                <div class="col-xs-12 col-sm-1">
                    <asp:DropDownList ID="ddlMonthPeriod" runat="server" 
                        class="form-control input-sm" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlMonthPeriod_Change">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">Jan</asp:ListItem>
                        <asp:ListItem Value="2">Feb</asp:ListItem>
                        <asp:ListItem Value="3">Mar</asp:ListItem>
                        <asp:ListItem Value="4">Apr</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">Jun</asp:ListItem>
                        <asp:ListItem Value="7">Jul</asp:ListItem>
                        <asp:ListItem Value="8">Aug</asp:ListItem>
                        <asp:ListItem Value="9">Sep</asp:ListItem>
                        <asp:ListItem Value="10">Oct</asp:ListItem>
                        <asp:ListItem Value="11">Nov</asp:ListItem>
                        <asp:ListItem Value="12">Dec</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 col-sm-1">
                    <asp:TextBox ID="txtYearPeriod" runat="server"  placeholder="Year" 
                        class="form-control input-sm" 
                        MaxLength="4" OnKeyPress="return isNumberKey(this, event);">
                    </asp:TextBox> 
                </div>
                <div class="col-xs-12 col-sm-2">
                    <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-success" 
                            OnClick="btnExportToExcel_Click" />
                </div>       
            </div>
            <div class="row">&nbsp</div>
            <div class="row">
                <div class="col-xs-12 col-sm-1">Order By</div>
                <div class="col-xs-12 col-sm-2">
                    <asp:DropDownList ID="ddlSort" runat="server" 
                        class="form-control input-sm" 
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSort_Change">
                        <asp:ListItem Value="Floor">Floor</asp:ListItem>
                        <asp:ListItem Value="InvNo">Invoice</asp:ListItem>
                        <asp:ListItem Value="Name">Name</asp:ListItem>                        
                        <asp:ListItem Value="Status">Status</asp:ListItem>  
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">&nbsp</div>
            <!-- GridView -->
            <asp:UpdatePanel ID="upContract" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                </Triggers>
                <ContentTemplate>                    
                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center"
                        AutoGenerateColumns="False" AllowPaging="True"
                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                        CssClass="GridViewStyle" Width="95%" DataKeyNames="ID"
                        ShowHeader="true" ShowFooter="false" PageSize="10" valign="left"
                        ForeColor="#333333"
                        OnRowCommand="GridView1_RowCommand"
                        OnPageIndexChanging="GridView1_PageIndexChanging" 
                        OnRowDataBound="GridView1_RowDataBound" 
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
                        <asp:ButtonField CommandName="previewRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="50px"
                            ButtonType="Button" Text="Preview" HeaderText ="Preview INV:OTH" 
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="printRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="50px"
                            ButtonType="Button" Text="Print" HeaderText ="Print COP:OTH" 
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="paymentRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="50px"
                            ButtonType="Button" Text="Payment" HeaderText ="Payment" 
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        
                        <asp:BoundField DataField="InvNo" HeaderText="Invoice No."/>
                        <asp:BoundField DataField="Month" HeaderText="Month"/>
                        <asp:BoundField DataField="Year" HeaderText="Year"/>
                        <asp:BoundField DataField="Floor" HeaderText="Floor"/>
                        <asp:BoundField DataField="SuiteNo" HeaderText="No Suite"/>
                        <asp:BoundField DataField="CID" HeaderText="CID"/>
                        <asp:BoundField DataField="Name" HeaderText="Company Name"/>
                        <asp:BoundField DataField="Pic1Name" HeaderText="Contact Person"/>
                        <asp:BoundField DataField="Status" HeaderText="Status"/>
                    </Columns>

                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle CssClass="tbl_foot" />
                        <HeaderStyle CssClass="tbl_head" />
                        <PagerStyle CssClass="tbl_page" />
                        <RowStyle BackColor="#EFF3FB" />
                        <AlternatingRowStyle BackColor="#FFFFFF" />
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="False" />
                    </asp:GridView>
                    <br>
                    </br>
                    <br></br> <!---->
                    <div id="dvData" style="display: none;">
                        <asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center"
                            AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID"
                            BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                            CssClass="GridViewStyle" Width="100%" 
                            ShowHeader="true" ShowFooter="false" PageSize="10" valign="left"
                            ForeColor="#333333" 
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
                            </Columns>

                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle CssClass="tbl_foot" />
                            <HeaderStyle CssClass="tbl_head" />
                            <PagerStyle CssClass="tbl_page" />
                            <RowStyle BackColor="#EFF3FB" />
                            <AlternatingRowStyle BackColor="#FFFFFF" />
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="False" />
                        </asp:GridView>
                    <%--<asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center"
                        AutoGenerateColumns="False" AllowPaging="false"
                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                        CssClass="GridViewStyle" Width="95%" DataKeyNames="ID"
                        ShowHeader="true" ShowFooter="false" PageSize="10" valign="left"
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
                        
                        <asp:BoundField DataField="Year" HeaderText="Year"/>                            
                        <asp:BoundField DataField="Bulan" HeaderText="Month"/>
                        <asp:BoundField DataField="InvNo" HeaderText="INV:PLN No"/>
                        
                        
                        <asp:BoundField DataField="Floor" HeaderText="Floor"/>
                        <asp:BoundField DataField="SuiteNo" HeaderText="No Suite"/>
                        <asp:BoundField DataField="CID" HeaderText="CID"/>
                        <asp:BoundField DataField="Name" HeaderText="Company Name"/>
                        <asp:BoundField DataField="Pic1Name" HeaderText="Contact Person"/>
                    </Columns>

                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle CssClass="tbl_foot" />
                        <HeaderStyle CssClass="tbl_head" />
                        <PagerStyle CssClass="tbl_page" />
                        <RowStyle BackColor="#EFF3FB" />
                        <AlternatingRowStyle BackColor="#FFFFFF" />
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="False" />
                    </asp:GridView>--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel> 
                       
            <!-- COOP form -->
            <div class="modal fade" id="infoModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
                <div class="modal-dialog" style="z-index: 1050">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnF5" EventName="Click" />
                        </Triggers>
                    <ContentTemplate>
                    <asp:Button ID="btnF5" runat="server" style="display:none;"/>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12"><asp:Label ID="lblID" runat="server" Visible="false"></asp:Label></div>
                                <div class="col-xs-12 col-sm-12"><asp:Label ID="lblMeterID" runat="server" Visible="false"></asp:Label></div>
                                <div class="col-xs-12 col-sm-5"><h4 class="modal-title" id="H21">No. COP-OTH-<asp:Label ID="lblCopNo" runat="server">xxx-xxx-xxxx</asp:Label></h4></div>
                                <div class="col-xs-12 col-sm-6"><h4 class="modal-title" id="H211">Period : <asp:Label ID="lblCopPeriod" runat="server">xxx - xxxx</asp:Label></h4></div>
                                <div class="col-xs-12 col-sm-5"><h4 class="modal-title" id="H22"><asp:Label ID="lblCID" runat="server">CID-xx-xx-xxxx-xx-xx</asp:Label></H4></div>
                                <div class="col-xs-12 col-sm-6"><h4 class="modal-title" id="H221">Suite No : <asp:Label ID="lblSuiteNo" runat="server">xxx</asp:Label></h4></div>
                                <div class="col-xs-12 col-sm-12"><h4 class="modal-title" id="H23">Company Name : <asp:Label ID="lblCompanyName" runat="server">PT. xxxxxxxxx</asp:Label></h4></div>
                            </div>
                        </div>

                        <div class="modal-body">                              
                            <div class="row">
                                <div class="col-xs-12 col-sm-4">Other</div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Invoice No</div>
                                <div class="col-xs-12 col-sm-6">
                                    <asp:Label ID="lblInvNO" runat="server">xxxxxx</asp:Label>
                                </div>
                                <div class="col-xs-12 col-sm-2" style="text-align:right"><asp:Label ID="lblTotalAmount" runat="server">xxxxxx</asp:Label></div>
                            </div>                        
                            
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">&nbsp</div>
                                <div class="col-xs-12 col-sm-2">Subtotal</div>
                                <div class="col-xs-12 col-sm-2" style="text-align:right">
                                    <asp:Label ID="lblSubTotal" runat="server">xxxxxx</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">&nbsp</div>
                                <div class="col-xs-12 col-sm-2">10% PPN</div>
                                <div class="col-xs-12 col-sm-2" style="text-align:right">
                                    <asp:Label ID="lblPPN" runat="server">xxxxxx</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">&nbsp</div>
                                <div class="col-xs-12 col-sm-2">Materai</div>
                                <div class="col-xs-12 col-sm-2" style="text-align:right">
                                    <asp:Label ID="lblMaterai" runat="server">xxxxxx</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-6">&nbsp</div>
                                <div class="col-xs-12 col-sm-2">Grand Total</div>
                                <div class="col-xs-12 col-sm-2" style="text-align:right">
                                    <asp:Label ID="lblGrandTotal" runat="server">xxxxxx</asp:Label>
                                    <asp:Label ID="lblGT" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row">&nbsp</div><div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Payment Method</div>
                                <div class="col-xs-12 col-sm-4" class="form-control input-sm" >
                                    <asp:RadioButtonList ID="rblPaymentMethod" runat="server" AutoPostBack="true" OnTextChanged="rblPaymentMethod_Change">
                                        <asp:ListItem Text="Direct Transfer" Value="Direct Transfer" Selected="True"/>
                                        <asp:ListItem Text="Check" Value="Check" />
                                        <asp:ListItem Text="Giro" Value="Giro"/>                                        
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-xs-12 col-sm-2">Received Date (dd-mm-yyyy)</div>
                                <div class="col-xs-12 col-sm-3">
                                    <asp:TextBox runat="server" ID="txtReceivedDate" class="form-control input-sm" 
                                        placeholder="Received Date" style="text-align:left;" />
                                </div>
                            </div>
                            <div class="row" style="display: none;">&nbsp</div>
                            <div class="row" style="display: none;">
                                <div class="col-xs-12 col-sm-2">Payment No</div>
                                <div class="col-xs-12 col-sm-4">
                                    <asp:TextBox runat="server" ID="txtPaymentNo" class="form-control input-sm" 
                                        placeholder="Payment Number" style="text-align:left;" />
                                </div>

                                <div class="col-xs-12 col-sm-1">Bank Name</div>
                                <div class="col-xs-12 col-sm-4">
                                    <asp:TextBox runat="server" ID="txtBankName" class="form-control input-sm" 
                                        placeholder="Bank Name" style="text-align:left;"/>
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Payment Nominal</div>                                
                                <div class="col-xs-12 col-sm-9">
                                    <asp:TextBox runat="server" ID="txtPaymentNominal" class="form-control input-sm" 
                                        OnKeyPress="return isNumberKey(this, event); "  
                                        placeholder="Payment Nominal" style="text-align:left;" />
                                </div>
                                <div class="col-xs-12 col-sm-1"><asp:Label ID="lblOkNg" runat="server"></asp:Label></div>
                            </div>                            
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12">
                                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="btn btn-info" OnClick="btnCalculate_Click"/>
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Variance</div>
                                <div class="col-xs-12 col-sm-9">
                                    <asp:TextBox runat="server" ID="txtVariance" class="form-control input-sm" 
                                        placeholder="Variance" style="text-align:left;" Enabled="false"/>
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">PPh : FNL</div>
                                <div class="col-xs-12 col-sm-9">
                                    <asp:TextBox runat="server" ID="txtPPhFnl" class="form-control input-sm" 
                                        OnKeyPress="return isNumberKey(this, event);"
                                        placeholder="PPh Final" style="text-align:left;" />
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Bank Charge</div>
                                <div class="col-xs-12 col-sm-9">
                                    <asp:TextBox runat="server" ID="txtBankCharge" class="form-control input-sm" 
                                        OnKeyPress="return isNumberKey(this, event);"
                                        placeholder="Payment Nominal" style="text-align:left;" />
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Other</div>
                                <div class="col-xs-12 col-sm-9">
                                    <asp:TextBox runat="server" ID="txtOther" class="form-control input-sm" 
                                        OnKeyPress="return isNumberKey(this, event);"
                                        placeholder="Payment Nominal" style="text-align:left;" />
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Total</div>                                
                                <div class="col-xs-12 col-sm-9">
                                    <asp:TextBox runat="server" ID="txtTotal" class="form-control input-sm" 
                                        placeholder="Total" style="text-align:left;" enabled="false"/>
                                </div>
                                <div class="col-xs-12 col-sm-1"><asp:Label ID="lblTotalOkNg" runat="server"></asp:Label></div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">Note</div>
                                <div class="col-xs-12 col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNote" class="form-control input-sm" 
                                        Rows="3" placeholder="Note" 
                                        MaxLength="100" style="text-align:left;" TextMode="MultiLine" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">          
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" 
                            OnClick="btnSave_Click" Enabled="false"/>
                            <button  type="button" class="btn btn-info" data-dismiss="modal" aria-hidden="Close">Close</button>                         
                        </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
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

<asp:Content ID="Content3" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
