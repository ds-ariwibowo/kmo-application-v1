<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="CFS.aspx.cs" Inherits="KMO.Setup.CFS" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="CPH_HEAD">
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

        var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "cfs";
        $('#Tabs a[href="#' + tabName + '"]').tab('show');
        $("#Tabs a").click(function () {
            $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
        });

    });

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
        

        //set suite value
        $('#<%=txtRentPeriod.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtDepositPeriod.ClientID%>').val(txtVal); });
        $('#<%=ddlRentPeriod.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=ddlDepositPeriod.ClientID%>').val(txtVal); });
        $('#<%=txtRentCharge.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtDepositRentCharge.ClientID%>').val(txtVal); });
        $('#<%=txtServiceCharge.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtDepositServiceCharge.ClientID%>').val(txtVal); });
        $('#<%=txtOtherCharge.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtDepositOtherCharge.ClientID%>').val(txtVal); });

        $('#<%=txtSuite101.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite101RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite110.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite110RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite201.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite201RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite210.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite210RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite301.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite301RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite305.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite305RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite310.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite310RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite501.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite501RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite505.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite505RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite510.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite510RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite601.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite601RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite605.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite605RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite610.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite610RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite701.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite701RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite705.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite705RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite710.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite710RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite801.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite801RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite805.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite805RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite810.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite810RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite901.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite901RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite905.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite905RS.ClientID%>').val(txtVal); });
        $('#<%=txtSuite910.ClientID%>').keyup(function (e) { var txtVal = $(this).val(); $('#<%=txtSuite910RS.ClientID%>').val(txtVal); });

        $('#<%=chkSuite101.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite101RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite101RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite105.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite105RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite105RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite110.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite110RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite110RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite201.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite201RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite201RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite205.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite205RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite205RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite210.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite210RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite210RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite301.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite301RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite301RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite305.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite305RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite305RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite310.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite310RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite310RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite501.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite501RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite501RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite505.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite505RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite505RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite510.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite510RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite510RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite601.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite601RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite601RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite605.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite605RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite605RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite610.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite610RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite610RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite701.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite701RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite701RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite705.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite705RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite705RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite710.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite710RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite710RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite801.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite801RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite801RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite805.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite805RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite805RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite810.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite810RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite810RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite901.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite901RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite901RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite905.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite905RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite905RS.ClientID%>').prop("checked", false) } });
        $('#<%=chkSuite910.ClientID%>').click(function (e) { if ($(this).prop("checked") == true) { $('#<%=chkSuite910RS.ClientID%>').prop("checked", true) } else if ($(this).prop("checked") == false) { $('#<%=chkSuite910RS.ClientID%>').prop("checked", false) } });
        
        //date pictker format
        var dpAdd = $('#<%=txtLOODate.ClientID%>');
        dpAdd.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd-mm-yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        });

        var dpFit = $('#<%=txtFittingOutDate.ClientID%>');
        dpFit.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd-mm-yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        });

        var dpContractExpiredDate = $('#<%=txtContractExpiredDate.ClientID%>');
        dpContractExpiredDate.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd-mm-yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        });
        
        var dpMemorandumExpiredDate = $('#<%=txtMemorandumExpiredDate.ClientID%>');
        dpMemorandumExpiredDate.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd-mm-yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        }); 

        var dpSeviceCommencementDate = $('#<%=txtServiceCommencementDate.ClientID%>');
        dpSeviceCommencementDate.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd-mm-yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        });

        var dpRentCommencementDate = $('#<%=txtRentCommencementDate.ClientID%>');
        dpRentCommencementDate.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd-mm-yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        });

        var dpParkingPeriod = $('#<%=txtParkingStartPeriod.ClientID%>');
        dpParkingPeriod.datepicker({
            changeMonth: true,
            changeYear: true,
            format: "dd-mm-yyyy",
            language: "tr"
        }).on('changeDate', function (ev) {
            $(this).blur();
            $(this).datepicker('hide');
        });

        //check textbox empty or not
        $("#<%= btnAddContract.ClientID %>").click(function () {
            if ($("#<%= txtContractNo.ClientID %>").val() != "") {
                if ($("#<%= txtContractExpiredDate.ClientID %>").val() != "") {
                    return true;
                }
                else {
                    alert('Contract expired date still empty.')
                    return false;
                }
            }
            else {
                alert('Contract number still empty.')
                return false;
            }
        });

        $("#<%= btnSave.ClientID %>").click(function () {                
                if ($("#<%= txtCompanyName.ClientID %>").val() != "") {
                    if ($("#<%= txtCity.ClientID %>").val() != "") {
                        if ($("#<%= txtPIC1Name.ClientID %>").val() != "") {
                            if ($("#<%= cmbLoB.ClientID %>").val() != "") {
                                return true;
                            }
                            else {
                                alert('Line of Business still empty.')
                                return false;
                            }
                        }
                        else {
                            alert('PIC name still empty.')
                            return false;
                        }
                    }
                    else {
                        alert('City name still empty.')
                        return false;
                    }
                }
                else {
                    alert('Company name still empty.')
                    return false;
                }                
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
                      
            top: 50%;
            margin-left: 0px;  
            margin-top: -250px;
        } 
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CPH_BODY_CONTENT">
    <div class="container">
    <asp:ScriptManager runat="server" ID="ScriptManager1" />
    <div class="row col-xs-12 col-sm-12">        
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

        
        <div class="panel panel-default" style="padding: 10px; margin: 10px">
            <div id="Tabs" role="tabpanel">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li class="active"><a href="#cfs" aria-controls="cfs" role="tab" data-toggle="tab">Client Fact Sheet</a></li>
                <li><a href="#cfsContract" aria-controls="cfsContract" role="tab" data-toggle="tab">Contract Info</a></li>
                <li><a href="#cfsRent" aria-controls="cfsRent" role="tab" data-toggle="tab">Rent & Service</a></li>
                <li><a href="#cfsSecurity" aria-controls="cfsSecurity" role="tab" data-toggle="tab">Security Deposit</a></li>
                <!--<li><a href="#cfsParking" aria-controls="cfsParking" role="tab" data-toggle="tab">Parking</a></li>-->
                <li><a href="#cfsParkingNew" aria-controls="cfsParkingNew" role="tab" data-toggle="tab">Parking</a></li>
            </ul>

            <!-- Tab panes -->
            <asp:HiddenField ID="TabName" runat="server" />
            <div class="tab-content" style="padding-top: 20px">
                <div role="tabpanel" class="tab-pane active" id="cfs">
                    <div style="border:1px solid black;">
                            <div class="row">&nbsp</div>
                            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp CID</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:Label ID="lblCID" runat="server">[Auto Generated by System]</asp:Label></div>
                                <div class="col-xs-12 col-sm-2">
                                    <asp:CheckBox ID="chkIsTenant" runat="server" />
                                    <asp:Label ID="Label64" runat="server">Is Tenant</asp:Label>
                                </div>
                                
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp NPWP</div>
                                <div class="col-xs-12 col-sm-3" align="left"><asp:TextBox ID="txtNPWP" runat="server"  placeholder="NPWP" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">LOO Signed Date</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtLOODate" runat="server" placeholder="LOO Signed Date" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Proposed Suite</div>
                                <div class="col-xs-12 col-sm-3" align="left"><asp:Label ID="lblProposedSuite" runat="server">[Auto Generated by System]</asp:Label></div>
                            </div>
                            <div class="row">&nbsp</div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div style="border:1px solid black;">
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp Name</div>
                                <div class="col-xs-12 col-sm-11" align="left"><asp:TextBox ID="txtCompanyName" runat="server" placeholder="Company Name" Width="98%"></asp:TextBox></div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp Address</div>
                                <div class="col-xs-12 col-sm-11" align="left"><asp:TextBox ID="txtAddress" runat="server"  placeholder="Address" Width="98%" multiline="true" Rows="5"></asp:TextBox></div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp City</div>
                                <div class="col-xs-12 col-sm-3" align="left"><asp:TextBox ID="txtCity" runat="server"  placeholder="City" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Zip</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtZIP" runat="server"  placeholder="ZIP" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Telp</div>
                                <div class="col-xs-12 col-sm-4" align="left"><asp:TextBox ID="txtTelp" runat="server"  placeholder="Telp" Width="94%"></asp:TextBox></div>
                            </div>
                            <div class="row">&nbsp</div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div style="border:1px solid black;">
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp PIC Name</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC1Name" runat="server"  placeholder="Name" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Position</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC1Position" runat="server"  placeholder="Position" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Mobile Ph</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC1MobilePh" runat="server"  placeholder="Mobile Ph" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Email</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC1Email" runat="server"  placeholder="Email" Width="87%"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp PIC Name</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC2Name" runat="server"  placeholder="Name" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Position</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC2Position" runat="server"  placeholder="Position" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Mobile Ph</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC2MobilePh" runat="server"  placeholder="Mobile Ph" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Email</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC2Email" runat="server"  placeholder="Email" Width="87%"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp PIC Name</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC3Name" runat="server"  placeholder="Name" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Position</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC3Position" runat="server"  placeholder="Position" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Mobile Ph</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC3MobilePh" runat="server"  placeholder="Mobile Ph" Width="100%"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Email</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtPIC3Email" runat="server"  placeholder="Email" Width="87%"></asp:TextBox></div>
                            </div>
                            <div class="row">&nbsp</div>
                        </div>
            
                        <div class="row">&nbsp</div>
                        <div style="border:1px solid black;">
                            <div class="row">&nbsp</div>
                
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">&nbsp&nbsp Line of Business</div>
                                <div class="col-xs-12 col-sm-2" align="left">
                                    <asp:DropDownList ID="cmbLoB" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="90">BUMN</asp:ListItem>
                                        <asp:ListItem Value="11">Service/Consultant</asp:ListItem>
                                        <asp:ListItem Value="12">Contractor</asp:ListItem>
                                        <asp:ListItem Value="13">Manufacturer</asp:ListItem>
                                        <asp:ListItem Value="14">Food</asp:ListItem>
                                        <asp:ListItem Value="00">Other</asp:ListItem>
                                    </asp:DropDownList>                        
                                </div>
                                <div class="col-xs-12 col-sm-2" align="left">
                                <asp:TextBox ID="txtLoB" runat="server"  placeholder="Other" Width="100%"></asp:TextBox>
                                </div>
                                <div class="col-xs-12 col-sm-1" align="left"></div>
                                <div class="col-xs-12 col-sm-2">No of Employees</div>
                                <div class="col-xs-12 col-sm-2" align="left">
                                    <asp:DropDownList ID="cmbNoE" runat="server" Width="147%">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1-10 Employees">1-10 Employees</asp:ListItem>
                                        <asp:ListItem Value="11-25 Employees">11-25 Employees</asp:ListItem>
                                        <asp:ListItem Value="26-50 Employees">26-50 Employees</asp:ListItem>
                                        <asp:ListItem Value="51-75 Employees">51-75 Employees</asp:ListItem>
                                        <asp:ListItem Value="76-100 Employees">76-100 Employees</asp:ListItem>
                                        <asp:ListItem Value="101-125 Employees">101-125 Employees</asp:ListItem>
                                        <asp:ListItem Value="126-150 Employees">126-150 Employees</asp:ListItem>
                                        <asp:ListItem Value="151-175 Employees">151-175 Employees</asp:ListItem>
                                        <asp:ListItem Value=">175 Employees">>175 Employees</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">&nbsp</div>
                        </div>                        
                        <div class="row">&nbsp</div>                        
                        <div style="border:1px solid black;">
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">&nbsp&nbsp Telp.</div>
                                <div class="col-xs-12 col-sm-10">
                                    <table border="1 solid black">
                                      <tr>
                                        <th colspan="15">Required Telephone Lines</th>
                                      </tr>
                                        <tr><th colspan="15"><asp:RadioButton ID="rbTL0" runat="server" GroupName="rTL" /> None</th></tr>
                                      <tr>
                                        <td>1</td>
                                        <td>2</td>
                                        <td>3</td>
                                        <td>4</td>
                                        <td>5</td>
                                        <td>6</td>
                                        <td>7</td>
                                        <td>8</td>
                                        <td>9</td>
                                        <td>10</td>
                                        <td>11</td>
                                        <td>12</td>
                                        <td>13</td>
                                        <td>14</td>
                                        <td>15</td>
                                      </tr>
                                      <tr>
                                        <td><asp:RadioButton ID="rbTL1" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL2" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL3" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL4" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL5" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL6" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL7" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL8" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL9" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL10" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL11" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL12" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL13" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL14" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL15" runat="server" GroupName="rTL" /></td>
                                      </tr>
                                      <tr>
                                        <td>16</td>
                                        <td>17</td>
                                        <td>18</td>
                                        <td>19</td>
                                        <td>20</td>
                                        <td>21</td>
                                        <td>22</td>
                                        <td>23</td>
                                        <td>24</td>
                                        <td>25</td>
                                        <td>26</td>
                                        <td>27</td>
                                        <td>28</td>
                                        <td>29</td>
                                        <td>30</td>
                                      </tr>
                                      <tr>
                                        <td><asp:RadioButton ID="rbTL16" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL17" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL18" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL19" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL20" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL21" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL22" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL23" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL24" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL25" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL26" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL27" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL28" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL29" runat="server" GroupName="rTL" /></td>
                                        <td><asp:RadioButton ID="rbTL30" runat="server" GroupName="rTL" /></td>
                                      </tr>
                                    </table>
                                </div>                                                                       
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">&nbsp&nbsp Telp. Line Charge</div>
                                <div class="col-xs-12 col-sm-2">
                                    <asp:TextBox ID="txtProsTelpLineCharge" runat="server"  placeholder="Telp Line Charge"></asp:TextBox>
                                </div>
                            </div>                            
                            <div class="row">&nbsp</div>                            
                        </div>

                        <div class="row">&nbsp</div>

                        <div style="border:1px solid black;">
                            <div class="row">&nbsp</div>
                
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp Special Conditions</div>
                                <div class="col-xs-12 col-sm-11" align="left">
                                    <asp:TextBox ID="txtSpecialConditions" runat="server"  placeholder="Special Conditions" Rows="5" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                        </div>
                        
                        <div class="row">&nbsp</div>

                        <div style="border:1px solid black;">
                            <div class="row">&nbsp</div>

                            <div class="row">
                                <div class="col-xs-12 col-sm-2">&nbsp&nbsp Fitting Out Date</div>                                
                                <div class="col-xs-12 col-sm-2" align="left">
                                    <asp:TextBox ID="txtFittingOutDate" runat="server" placeholder="Fitting Out Date" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-2">&nbsp&nbsp (Based on UTL:ECT Form)</div>
                            </div>
                            <div class="row">&nbsp</div>
                        </div>                        
                </div>
                <div role="tabpanel" class="tab-pane" id="cfsContract">
                    <div style="border:1px solid black;">
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Contract No.</div>
                            <div class="col-xs-12 col-sm-3" align="left"><asp:TextBox ID="txtContractNo" runat="server"  placeholder="Contract No" Width="100%"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">Expired Date</div>
                            <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtContractExpiredDate" runat="server" placeholder="Expired Date" Width="100%"></asp:TextBox></div>                            
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Contract Period</div>
                            <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtContractPeriod" runat="server" placeholder="Period" Width="25%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>&nbsp Month(s)</div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Memorandum No.</div>
                            <div class="col-xs-12 col-sm-3" align="left"><asp:TextBox ID="txtMemorandumNo" runat="server"  placeholder="Memorandum No" Width="100%"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">Expired Date</div>
                            <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtMemorandumExpiredDate" runat="server" placeholder="Expired Date" Width="100%"></asp:TextBox></div>                            
                        </div>
                        <div class="row">&nbsp</div>  
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Note</div>
                            <div class="col-xs-12 col-sm-3" align="left"><asp:TextBox ID="txtContractNote" runat="server" Rows="5" TextMode="MultiLine" placeholder="Note" Width="100%"></asp:TextBox></div>                            
                            <div class="col-xs-12 col-sm-2" align="left"><asp:Button ID="btnAddContract" runat="server" Text="Add" CssClass="btn btn-success" OnClick="btnAddContract_Click"/></div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <!-- GridView -->
                            <asp:UpdatePanel ID="upContract" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                                </Triggers>
                                <ContentTemplate>                                                
                                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center"
                                        AutoGenerateColumns="False" AllowPaging="True"
                                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                                        CssClass="GridViewStyle" Width="95%" DataKeyNames="ContractID"
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
                                            ItemStyle-Width="90px"
                                            ButtonType="Button" Text="Delete" HeaderText="Delete Record"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                                        </asp:ButtonField>
                        
                                        <asp:BoundField DataField="ContractNo" HeaderText="Contract No."/>
                                        <asp:BoundField DataField="ContractExpiredDate" HeaderText="Contract Exp Date" DataFormatString="{0:dd-MM-yyyy}"/>
                                        <asp:BoundField DataField="ContractPeriod" HeaderText="Contract Period"/>
                                        <asp:BoundField DataField="MemorandumNo" HeaderText="Memorandum No."/>
                                        <asp:BoundField DataField="MemorandumExpiredDate" HeaderText="Memorandum Exp Date" DataFormatString="{0:dd-MM-yyyy}"/>
                                        <asp:BoundField DataField="ContractNote" HeaderText="Note"/>
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
                </div>
                <div role="tabpanel" class="tab-pane" id="cfsRent">
                    <div style="border:1px solid black;">
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp Service Charge Commencement Date</div>
                            <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtServiceCommencementDate" runat="server" placeholder="Date" Width="100%"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-3">Rent Charge Commencement Date</div>
                            <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtRentCommencementDate" runat="server" placeholder="Date" Width="100%"></asp:TextBox></div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-4">&nbsp&nbsp 
                                <asp:Label ID="Label27" runat="server">Charge Period</asp:Label>
                                <asp:TextBox ID="txtRentPeriod" runat="server"  placeholder="Rent Period" Width="20%" 
                                    OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>                             
                                <asp:DropDownList ID="ddlRentPeriod" runat="server" Width="30%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Month(s)">Month(s)</asp:ListItem>                                    
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-2"><asp:Label ID="Label7" runat="server">Rental Charge</asp:Label>&nbsp<asp:TextBox ID="txtRentCharge" runat="server"  placeholder="Rent Charge" Width="60%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2"><asp:Label ID="Label8" runat="server">Service Charge</asp:Label>&nbsp<asp:TextBox ID="txtServiceCharge" runat="server"  placeholder="Service Charge" Width="56%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2"><asp:Label ID="Label9" runat="server">Other Charge</asp:Label>&nbsp<asp:TextBox ID="txtOtherCharge" runat="server"  placeholder="Other Charge" Width="70%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">&nbsp</div>                
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 1st Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite101" runat="server" />
                                <asp:Label ID="lblSuite1" runat="server">101</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite101" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite101KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite105" runat="server" enabled="false"/>
                                <asp:Label ID="lblSuite2" runat="server">105</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite105" runat="server"  placeholder="" Width="35%" enabled="false"></asp:TextBox>
                                <asp:TextBox ID="txtSuite105KVA" runat="server"  placeholder="" Width="15%" enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite110" runat="server" />
                                <asp:Label ID="lblSuite3" runat="server">110</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite110" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite110KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 2nd Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite201" runat="server" />
                                <asp:Label ID="Label1" runat="server">201</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite201" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite201KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite205" runat="server" enabled="false"/>
                                <asp:Label ID="Label2" runat="server">205</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite205" runat="server"  placeholder="" Width="35%" enabled="false"></asp:TextBox>
                                <asp:TextBox ID="txtSuite205KVA" runat="server"  placeholder="" Width="15%" enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite210" runat="server" />
                                <asp:Label ID="Label3" runat="server">210</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite210" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite210KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 3th Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite301" runat="server" />
                                <asp:Label ID="Label4" runat="server">301</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite301" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite301KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite305" runat="server" />
                                <asp:Label ID="Label5" runat="server">305</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite305" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite305KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite310" runat="server" />
                                <asp:Label ID="Label6" runat="server">310</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite310" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite310KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 5th Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite501" runat="server" />
                                <asp:Label ID="Label10" runat="server">501</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite501" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite501KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite505" runat="server" />
                                <asp:Label ID="Label11" runat="server">505</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite505" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite505KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite510" runat="server" />
                                <asp:Label ID="Label12" runat="server">510</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite510" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite510KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 6th Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite601" runat="server" />
                                <asp:Label ID="Label13" runat="server">601</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite601" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite601KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite605" runat="server" />
                                <asp:Label ID="Label14" runat="server">605</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite605" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite605KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite610" runat="server" />
                                <asp:Label ID="Label15" runat="server">610</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite610" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite610KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 7th Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite701" runat="server" />
                                <asp:Label ID="Label16" runat="server">701</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite701" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite701KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite705" runat="server" />
                                <asp:Label ID="Label17" runat="server">705</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite705" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite705KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite710" runat="server" />
                                <asp:Label ID="Label18" runat="server">710</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite710" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite710KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 8th Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite801" runat="server" />
                                <asp:Label ID="Label19" runat="server">801</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite801" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite801KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite805" runat="server" />
                                <asp:Label ID="Label20" runat="server">805</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite805" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite805KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite810" runat="server" />
                                <asp:Label ID="Label21" runat="server">810</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite810" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite810KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 9th Floor</div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite901" runat="server" />
                                <asp:Label ID="Label22" runat="server">901</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite901" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite901KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite905" runat="server" />
                                <asp:Label ID="Label23" runat="server">905</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite905" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite905KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">&nbsp&nbsp 
                                <asp:CheckBox ID="chkSuite910" runat="server" />
                                <asp:Label ID="Label24" runat="server">910</asp:Label>&nbsp Area (m2)&nbsp
                                <asp:TextBox ID="txtSuite910" runat="server"  placeholder="Area (m2)" Width="35%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                <asp:TextBox ID="txtSuite910KVA" runat="server"  placeholder="KVA" Width="15%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="cfsSecurity">
                    <div style="border:1px solid black;">
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-4">&nbsp&nbsp 
                                <asp:Label ID="Label31" runat="server">Charge Period</asp:Label>
                                <asp:TextBox ID="txtDepositPeriod" runat="server"  placeholder="Rent Period" Width="20%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>                             
                                <asp:DropDownList ID="ddlDepositPeriod" runat="server" Width="30%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Month(s)">Month(s)</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-2"><asp:Label ID="Label32" runat="server">Rental Area</asp:Label>&nbsp<asp:TextBox ID="txtDepositRentCharge" runat="server"  placeholder="Rent Charge" Width="60%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2"><asp:Label ID="Label33" runat="server">Service Area</asp:Label>&nbsp<asp:TextBox ID="txtDepositServiceCharge" runat="server"  placeholder="Service Charge" Width="56%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2"><asp:Label ID="Label34" runat="server">Other</asp:Label>&nbsp<asp:TextBox ID="txtDepositOtherCharge" runat="server"  placeholder="Other Charge" Width="70%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">&nbsp</div>                
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 1st Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite101RS" runat="server" enabled="false"/><asp:Label ID="Label35" runat="server">101</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite101RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite105RS" runat="server" enabled="false"/><asp:Label ID="Label36" runat="server">105</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite105RS" runat="server"  placeholder="" Width="35%" enabled="false"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite110RS" runat="server" enabled="false"/><asp:Label ID="Label37" runat="server">110</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite110RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 2nd Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite201RS" runat="server" enabled="false"/><asp:Label ID="Label38" runat="server">201</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite201RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite205RS" runat="server" enabled="false"/><asp:Label ID="Label39" runat="server">205</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite205RS" runat="server"  placeholder="" Width="35%" enabled="false"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite210RS" runat="server" enabled="false"/><asp:Label ID="Label40" runat="server">210</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite210RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 3th Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite301RS" runat="server" enabled="false"/><asp:Label ID="Label41" runat="server">301</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite301RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite305RS" runat="server" enabled="false"/><asp:Label ID="Label42" runat="server">305</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite305RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite310RS" runat="server" enabled="false"/><asp:Label ID="Label43" runat="server">310</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite310RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 5th Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite501RS" runat="server" enabled="false"/><asp:Label ID="Label44" runat="server">501</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite501RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite505RS" runat="server" enabled="false"/><asp:Label ID="Label45" runat="server">505</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite505RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite510RS" runat="server" enabled="false"/><asp:Label ID="Label46" runat="server">510</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite510RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 6th Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite601RS" runat="server" enabled="false"/><asp:Label ID="Label47" runat="server">601</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite601RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite605RS" runat="server" enabled="false"/><asp:Label ID="Label48" runat="server">605</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite605RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite610RS" runat="server" enabled="false"/><asp:Label ID="Label49" runat="server">610</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite610RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 7th Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite701RS" runat="server" enabled="false"/><asp:Label ID="Label50" runat="server">701</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite701RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite705RS" runat="server" enabled="false"/><asp:Label ID="Label51" runat="server">705</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite705RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite710RS" runat="server" enabled="false"/><asp:Label ID="Label52" runat="server">710</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite710RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 8th Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite801RS" runat="server" enabled="false"/><asp:Label ID="Label53" runat="server">801</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite801RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite805RS" runat="server" enabled="false"/><asp:Label ID="Label54" runat="server">805</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite805RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite810RS" runat="server" enabled="false"/><asp:Label ID="Label55" runat="server">810</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite810RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Suite 9th Floor</div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite901RS" runat="server" enabled="false"/><asp:Label ID="Label56" runat="server">901</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite901RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite905RS" runat="server" enabled="false"/><asp:Label ID="Label57" runat="server">905</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite905RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp <asp:CheckBox ID="chkSuite910RS" runat="server" enabled="false"/><asp:Label ID="Label58" runat="server">910</asp:Label>&nbsp Area (m2)&nbsp<asp:TextBox ID="txtSuite910RS" runat="server"  placeholder="Area (m2)" Width="35%" enabled="false" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>
                        <div class="row">&nbsp</div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane" id="cfsParking">
                    <div style="border:1px solid black;">
                        <div class="row">&nbsp</div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">&nbsp&nbsp Proposed Parking</div>
                            <div class="col-xs-12 col-sm-3">
                                <table border="1 solid black">
                                    <tr>
                                    <th colspan="10">&nbsp&nbsp Total Reserved Parking, Cars &nbsp&nbsp</th>
                                    </tr>
                                    <tr><th colspan="15"><asp:RadioButton ID="rbRCars0" runat="server" GroupName="rCars" /> None</th></tr>
                                    <tr>
                                    <td>1</td>
                                    <td>2</td>
                                    <td>3</td>
                                    <td>4</td>
                                    <td>5</td>
                                    <td>6</td>
                                    <td>7</td>
                                    <td>8</td>
                                    <td>9</td>
                                    <td>10</td>
                                    </tr>                            
                                    <tr>
                                    <td><asp:RadioButton ID="rbRCars1" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars2" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars3" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars4" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars5" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars6" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars7" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars8" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars9" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars10" runat="server" GroupName="rCars" /></td>
                                    </tr>
                                    <tr>
                                    <td>11</td>
                                    <td>12</td>
                                    <td>13</td>
                                    <td>14</td>
                                    <td>15</td>
                                    <td>16</td>
                                    <td>17</td>
                                    <td>18</td>
                                    <td>19</td>
                                    <td>20</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbRCars11" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars12" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars13" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars14" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars15" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars16" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars17" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars18" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars19" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars20" runat="server" GroupName="rCars" /></td>
                                    </tr>
                                    <tr>
                                    <td>21</td>
                                    <td>22</td>
                                    <td>23</td>
                                    <td>24</td>
                                    <td>25</td>
                                    <td>26</td>
                                    <td>27</td>
                                    <td>28</td>
                                    <td>29</td>
                                    <td>30</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbRCars21" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars22" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars23" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars24" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars25" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars26" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars27" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars28" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars29" runat="server" GroupName="rCars" /></td>
                                    <td><asp:RadioButton ID="rbRCars30" runat="server" GroupName="rCars" /></td>
                                    </tr>
                                </table>
                                </br>
                                <asp:Label ID="Label25" runat="server">Period</asp:Label>    
                                <asp:TextBox ID="txtRpcPeriod" runat="server"  placeholder="Period" Width="13%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>                             
                                <asp:DropDownList ID="ddlRpc" runat="server" Width="30%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Month(s)">Month(s)</asp:ListItem>
                                </asp:DropDownList>
                                </br></br>
                                <asp:Label ID="Label26" runat="server">Charge</asp:Label>
                                <asp:TextBox ID="txtRpcCharge" runat="server"  placeholder="Charge" Width="43%" 
                                    OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">
                                <table border="1 solid black">
                                    <tr>
                                    <th colspan="10">&nbsp&nbsp Total Unreserved Parking, Cars &nbsp&nbsp</th>
                                    </tr>
                                    <tr><th colspan="15"><asp:RadioButton ID="rbUCars0" runat="server" GroupName="uCars" /> None</th></tr>
                                    <tr>
                                    <td>1</td>
                                    <td>2</td>
                                    <td>3</td>
                                    <td>4</td>
                                    <td>5</td>
                                    <td>6</td>
                                    <td>7</td>
                                    <td>8</td>
                                    <td>9</td>
                                    <td>10</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbUCars1" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars2" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars3" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars4" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars5" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars6" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars7" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars8" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars9" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars10" runat="server" GroupName="uCars" /></td>
                                    </tr>
                                    <tr>
                                    <td>11</td>
                                    <td>12</td>
                                    <td>13</td>
                                    <td>14</td>
                                    <td>15</td>
                                    <td>16</td>
                                    <td>17</td>
                                    <td>18</td>
                                    <td>19</td>
                                    <td>20</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbUCars11" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars12" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars13" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars14" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars15" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars16" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars17" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars18" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars19" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars20" runat="server" GroupName="uCars" /></td>
                                    </tr>
                                    <tr>
                                    <td>21</td>
                                    <td>22</td>
                                    <td>23</td>
                                    <td>24</td>
                                    <td>25</td>
                                    <td>26</td>
                                    <td>27</td>
                                    <td>28</td>
                                    <td>29</td>
                                    <td>30</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbUCars21" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars22" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars23" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars24" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars25" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars26" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars27" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars28" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars29" runat="server" GroupName="uCars" /></td>
                                    <td><asp:RadioButton ID="rbUCars30" runat="server" GroupName="uCars" /></td>
                                    </tr>
                                </table></br>
                                <asp:Label ID="Label59" runat="server">Period</asp:Label>    
                                <asp:TextBox ID="txtUrcPeriod" runat="server"  placeholder="Period" Width="13%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>                             
                                <asp:DropDownList ID="ddlUrc" runat="server" Width="30%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Month(s)">Month(s)</asp:ListItem>                                    
                                </asp:DropDownList>
                                </br></br>
                                <asp:Label ID="Label28" runat="server">Charge</asp:Label>
                                <asp:TextBox ID="txtUrcCharge" runat="server"  placeholder="Charge" Width="43%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-3">
                                <table border="1 solid black" widht="200px">
                                    <tr>
                                    <th colspan="10">&nbsp&nbsp Total Reserved Parking, Motorbike &nbsp&nbsp</th>
                                    </tr>
                                    <tr><th colspan="15"><asp:RadioButton ID="rbRMotos0" runat="server" GroupName="rMotos" /> None</th></tr>
                                    <tr>
                                    <td>1</td>
                                    <td>2</td>
                                    <td>3</td>
                                    <td>4</td>
                                    <td>5</td>
                                    <td>6</td>
                                    <td>7</td>
                                    <td>8</td>
                                    <td>9</td>
                                    <td>10</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbRMotos1" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos2" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos3" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos4" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos5" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos6" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos7" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos8" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos9" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos10" runat="server" GroupName="rMotos" /></td>
                                    </tr>
                                    <tr>
                                    <td>11</td>
                                    <td>12</td>
                                    <td>13</td>
                                    <td>14</td>
                                    <td>15</td>
                                    <td>16</td>
                                    <td>17</td>
                                    <td>18</td>
                                    <td>19</td>
                                    <td>20</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbRMotos11" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos12" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos13" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos14" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos15" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos16" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos17" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos18" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos19" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos20" runat="server" GroupName="rMotos" /></td>
                                    </tr>
                                    <tr>
                                    <td>21</td>
                                    <td>22</td>
                                    <td>23</td>
                                    <td>24</td>
                                    <td>25</td>
                                    <td>26</td>
                                    <td>27</td>
                                    <td>28</td>
                                    <td>29</td>
                                    <td>30</td>
                                    </tr>
                                    <tr>
                                    <td><asp:RadioButton ID="rbRMotos21" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos22" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos23" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos24" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos25" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos26" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos27" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos28" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos29" runat="server" GroupName="rMotos" /></td>
                                    <td><asp:RadioButton ID="rbRMotos30" runat="server" GroupName="rMotos" /></td>
                                    </tr>
                                </table></br>
                                <asp:Label ID="Label60" runat="server">Period</asp:Label>    
                                <asp:TextBox ID="txtRpmPeriod" runat="server"  placeholder="Period" Width="13%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>                             
                                <asp:DropDownList ID="ddlRpm" runat="server" Width="30%">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Month(s)">Month(s)</asp:ListItem>
                                </asp:DropDownList>
                                </br></br>
                                <asp:Label ID="Label30" runat="server">Charge</asp:Label>
                                <asp:TextBox ID="txtRpmCharge" runat="server"  placeholder="Charge" Width="56%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2"><asp:Label ID="Label29" runat="server">&nbsp Other Charge</asp:Label></div>
                            <div class="col-xs-12 col-sm-2"><asp:TextBox ID="txtParkingOtherCharge" runat="server"  placeholder="Other Charge" Width="92%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                        </div>                        
                        <div class="row">&nbsp</div>
                    </div>
                    <div class="row">&nbsp</div>
                </div>
                <div role="tabpanel" class="tab-pane" id="cfsParkingNew">
                    <div style="border:1px solid black;">
                        <div class="row">&nbsp</div>
                        <div runat="server" id="divParking" class="alert alert-warning" widht="90%">
                            <div class="row">
                                
                                <strong>Information</strong></br>
                                <asp:Label ID="Label63" runat="server">Please save CFS data first before add tenant parking data.</asp:Label></br>                                
                            </div>
                        </div>
                        <div runat="server" id="divParking2">
                            <div class="row">
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp Start </br>&nbsp&nbsp Period</div>
                                <div class="col-xs-12 col-sm-1" align="left">
                                    <asp:TextBox ID="txtParkingStartPeriod" runat="server" placeholder="Start Period"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">                            
                                <div class="col-xs-12 col-sm-1">&nbsp&nbsp Type</div>
                                <div class="col-xs-12 col-sm-1" align="left">
                                    <asp:DropDownList ID="ddlParkingType" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="Reserved">Reserved</asp:ListItem>
                                        <asp:ListItem Value="Unreserved">Unreserved</asp:ListItem>
                                        <asp:ListItem Value="Motorcycle">Motorcycle</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 col-sm-1">Parking Lot</div>
                                <div class="col-xs-12 col-sm-1" align="left"><asp:TextBox ID="txtParkingLot" runat="server" placeholder="Lot" Width="50%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">Period</div>
                                <div class="col-xs-12 col-sm-2" align="left">
                                    <asp:TextBox ID="txtParkingPeriod" runat="server" placeholder="Period" Width="30%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                                    <asp:DropDownList ID="ddlParkingPeriod" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="Month(s)">Month(s)</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-12 col-sm-1">Price</div>
                                <div class="col-xs-12 col-sm-1" align="left"><asp:TextBox ID="txtParkingPrice" runat="server" placeholder="Price" Width="98%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                                <div class="col-xs-12 col-sm-1">License No</div>
                                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtParkingLicenseNo" runat="server" placeholder="LicenseNo" Width="90%"></asp:TextBox></div>
                            </div>
                            <div class="row">&nbsp</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-10">&nbsp</div>
                                <div class="col-xs-12 col-sm-2"><asp:Button ID="btnAddParking" runat="server" Text="Add" CssClass="btn btn-success" OnClick="btnAddParking_Click"/></div>
                            </div>                        
                            <div class="row">&nbsp</div>             
                            <div class="row">
                                <!-- GridView -->
                                <asp:UpdatePanel ID="upParking" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>                                                
                                        <asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center"
                                            AutoGenerateColumns="False" AllowPaging="True"
                                            BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                                            CssClass="GridViewStyle" Width="95%" DataKeyNames="ParkingID"
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
                                            <asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                                                ItemStyle-Width="90px"
                                                ButtonType="Button" Text="Delete" HeaderText="Delete Record"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                                <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                                            </asp:ButtonField>
                        
                                            <asp:BoundField DataField="ParkingType" HeaderText="Parking Type"/>
                                            <asp:BoundField DataField="Lot" HeaderText="Lot"/>
                                            <asp:BoundField DataField="Period" HeaderText="Period"/>                                        
                                            <asp:BoundField DataField="PeriodMY" HeaderText=" "/>
                                            <asp:BoundField DataField="LicenseNo" HeaderText="License No"/>
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
                    </div>
                </div>
            </div>
        </div>
        </div>    
        
        <div class="row">&nbsp</div>
        <div align="right">
            <div class="row">&nbsp</div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
            <div class="row">&nbsp</div>
        </div>

        <!-- Delete Modal Start Here --->
        <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H2">Delete CFS Data</h4>
                    </div>

                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="form-horizontal">
                                    Are you sure to delete this record?
                                    <asp:HiddenField ID="hfCode" runat="server" />
                                    <asp:Label ID="Label61" Visible="false" runat="server"></asp:Label>
                                
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
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" 
                            OnClick="btnDelete_Click"/>
                        <button class="btn btn-info" data-dismiss="Close" 
                        aria-hidden="true">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="deleteModalParking" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H2">Delete Parking Data</h4>
                    </div>

                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="form-horizontal">
                                    Are you sure to delete this record?
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:Label ID="Label62" Visible="false" runat="server"></asp:Label>
                                
                                    <div class="form-group">
                                        <label for="delete alasan" class="col-sm-3 control-label">
                                            Reason</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox runat="server" ID="txtReasonToDeleteParking" class="form-control input-sm" 
                                                Rows="3" Width="250px" placeholder="Reason to delete" 
                                                MaxLength="100" style="text-align:left;" TextMode="MultiLine" />
                                        </div>
                                    </div>                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="modal-footer">            
                        <asp:Button ID="btnDeleteParking" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteParking_Click"/>
                        <button class="btn btn-info" data-dismiss="Close" 
                        aria-hidden="true">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>    
</div>
    </asp:UpdatePanel>
</asp:Content>
