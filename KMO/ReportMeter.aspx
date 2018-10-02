<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="ReportMeter.aspx.cs" Inherits="KMO.ReportMeter" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
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

            //datepicker
            var dpPD = $('#<%=txtPrintedDate.ClientID%>');
            dpPD.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });

            var dpDD = $('#<%=txtDueDate.ClientID%>');
            dpDD.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy",
                language: "tr"
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
            
        //check textbox empty or not
        $("#<%= btnOpenPDF.ClientID %>").click(function () {
                if ($(" #<%= ddlMonthPeriod.ClientID %>").val() != "") {
                    if ($("#<%= txtYearPeriod.ClientID %>").val() != "") {
                        return true;
                    }
                    else {
                        alert('Year period is empty')
                        return false;
                    }
                }
                else {
                    alert('Month period is empty')
                    return false;
                }
            });
        });
    </script>
    <style>
        .GridViewStyle td {
            padding: 2px 10px;
        }
        
        .datepicker{z-index:1151 !important;}

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
                    <a href="#" class="close" data-dismiss="alert ">&times;</a>
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
            <div class="col-xs-12 col-sm-12"><h4>Invoice PLN</h4></div>
            <div class="row">&nbsp</div>
            <div class="row">
                <div class="col-xs-12 col-sm-2">Payment Period</div>
                <div class="col-xs-12 col-sm-1" align="left">
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
                    <asp:TextBox ID="txtYearPeriod" runat="server" class="form-control input-sm"  
                        placeholder="Year" MaxLength="4" Width="60px" OnKeyPress="return isNumberKey(this, event);">
                    </asp:TextBox>
                </div> 
                <div class="col-xs-12 col-sm-2">
                    <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-success" 
                            OnClick="btnExportToExcel_Click" />
                </div> 

                <div class="col-xs-12 col-sm-1" style="display: none;">Printed Date</div>
                <div class="col-xs-12 col-sm-2" align="left" style="display: none;">
                    <asp:TextBox ID="txtPrintedDate" runat="server"  placeholder="Printed Date" Width="50%" ></asp:TextBox>
                </div>
                <div class="col-xs-12 col-sm-1" style="display: none;">Due Date</div>
                <div class="col-xs-12 col-sm-2" align="left" style="display: none;">
                    <asp:TextBox ID="txtDueDate" runat="server"  placeholder="Due Date" Width="50%" ></asp:TextBox>
                </div>
            </div>            
            <div class="row">&nbsp</div>
            <div class="row" style="display: none;" >
                <asp:Button ID="btnOpenPDF" runat="server" Text="Proceed" CssClass="btn btn-success" OnClick="btnOpenPDF_Click"/>           
            </div>
            
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
                        OnRowDataBound="GridView1_RowDataBound" 
                        OnRowCommand="GridView1_RowCommand"
                        OnPageIndexChanging="GridView1_PageIndexChanging" 
                        ForeColor="#333333" AllowSorting="True">
                        <AlternatingRowStyle BackColor="#EEE" />
                        <SelectedRowStyle BackColor="#FFF" Font-Bold="False" ForeColor="Navy" />
                    
                        <Columns>
                        <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="center"
                            ItemStyle-Width="30px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="printRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="50px"
                            ButtonType="Button" Text="Print" HeaderText ="Print INV:PLN" 
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        
                        <asp:BoundField DataField="InvNo" HeaderText="Invoice No."/>
                        <asp:BoundField DataField="BulanPemakaian" HeaderText="Month"/>
                        <asp:BoundField DataField="Year" HeaderText="Year"/>
                        <asp:BoundField DataField="PrintDate" HeaderText="Print Date"/>
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date"/>

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
                    </br></br>

                    <div id="dvData" style="display: none;" >
                    <asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center"
                        AutoGenerateColumns="False" AllowPaging="false"
                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                        CssClass="GridViewStyle" Width="95%" DataKeyNames="ID"
                        ShowHeader="true" ShowFooter="false" PageSize="10" valign="left">
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel> 

            <!-- create inv form -->
            <div class="modal fade" id="printInvModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
                <div class="modal-dialog" style="z-index: 1050">
                    <asp:UpdatePanel ID="upPrintInv" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <strong>
                                        <asp:Label ID="Label1" runat="server">Print PLN Invoice</asp:Label> | 
                                        <asp:Label ID="lblCompanyName" runat="server"></asp:Label> | Suite No :
                                        <asp:Label ID="lblSuite" runat="server"></asp:Label>
                                        <asp:Label ID="lblMonth" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Visible="false"></asp:Label>
                                    </strong>
                                </div>

                                <div class="modal-body">
                                    <div class="row" >
                                        <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                                        <div class="col-xs-12 col-sm-2">Invoice No.</div>
                                        <div class="col-xs-12 col-sm-4" align="left">
                                            <asp:Label ID="lblInvNo" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-12 col-sm-2">
                                            <asp:Button ID="btnCreateInv" runat="server" Text="Create Invoice No" CssClass="btn btn-success btn-xs" OnClick="btnCreateInv_Click"/>
                                        </div>                                        
                                    </div>
                                    <div class="row" >&nbsp</div>
                                    <div class="row" >
                                        <asp:Label ID="lblInvID" runat="server" Visible="false"></asp:Label>
                                        <div class="col-xs-12 col-sm-2">Printed Date</div>
                                        <div class="col-xs-12 col-sm-2" align="left">
                                            <asp:TextBox ID="txtInvPrintedDate" runat="server"  placeholder="Printed Date" ></asp:TextBox>
                                            dd-MM-yyyy
                                        </div>                                
                                    </div>
                                    <div class="row" >&nbsp</div>
                                    <div class="row" >
                                        <div class="col-xs-12 col-sm-2">Due Date</div>
                                        <div class="col-xs-12 col-sm-2" align="left">
                                            <asp:TextBox ID="txtInvDueDate" runat="server"  placeholder="Due Date" ></asp:TextBox>
                                            dd-MM-yyyy
                                        </div>
                                    </div>                                    
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btnInvPrint" runat="server" Text="Print" CssClass="btn btn-success" OnClick="btnInvPrint_Click"/>                                    
                                    <button  type="button" class="btn btn-danger" data-dismiss="modal" aria-hidden="Close">Cancel</button>
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
<asp:Content ID="FooterContent" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
