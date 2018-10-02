<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="UTLECDMRD.aspx.cs" Inherits="KMO.Transaction.UTLECDMRD" %>
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

            var dpAdd = $('#<%=txtUTLRecordDate.ClientID%>');
            dpAdd.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy",
                language: "tr"
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
                      
            top: 50%;
            margin-left: 0px;  
            margin-top: -250px;
        } 
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CPH_BODY_CONTENT">
    <div class="container">
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

            <div class="col-xs-12 col-sm-12">
                <div class="col-xs-12 col-sm-2">UTL Record Date</div>
                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtUTLRecordDate" runat="server" placeholder="UTL Record Date" Width="100%" ></asp:TextBox></div>
                <div class="col-xs-12 col-sm-2">KWh Rate This Period</div>
                <div class="col-xs-12 col-sm-2" align="left"><asp:TextBox ID="txtKWhRate" runat="server" placeholder="KWh Rate This Period" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></div>
                <div >&nbsp</div>
                <div >&nbsp</div>
            </div>
            
            <div class="col-xs-12 col-sm-12">
                <table border="1 solid black" width="100%">
                  <tr>
                    <th rowspan="3">Floor</th>
                    <th colspan="3">Tenant</th>
                    <th colspan="6">kWh</th>
                  </tr>
                  <tr align="center">
                    <td rowspan="2">Company Name</td>
                    <td rowspan="2">Capacity KVA</td>
                    <td rowspan="2">Suite No</td>
                    <td colspan="2">AC</td>
                    <td colspan="2">Non -AC</td>
                    <td colspan="2">AC: Outdoor Units</td>
                  </tr>
                  <tr align="center">
                    <td>Reading</td>
                    <td>Initial</td>
                    <td>Reading</td>
                    <td>Initial</td>
                    <td>Reading</td>
                    <td>Initial</td>
                  </tr>
                  <tr align="center">
                    <td rowspan="4">9</td>
                    <td><asp:TextBox ID="txtCompanyName901" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>901</td>
                    <td>
                        <asp:TextBox ID="txtACReading901" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                        <!--<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" 
                        ControlToValidate="txtACReading901" ErrorMessage="Value must be a whole number" />-->
                    </td>
                    <td><asp:TextBox ID="txtACInitial901" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading901" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial901" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading901" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial901" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName905" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>905</td>
                    <td><asp:TextBox ID="txtACReading905" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial905" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading905" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial905" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName910" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>910</td>
                    <td><asp:TextBox ID="txtACReading910" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial910" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading910" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial910" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 9</td>
                    <td>3.95</td>
                    <td>Lobby 9</td>
                    <td><asp:TextBox ID="txtACReadingLoby901" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby901" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby901" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby901" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                  
                  <tr align="center">
                    <td rowspan="4">8</td>
                    <td><asp:TextBox ID="txtCompanyName801" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>801</td>
                    <td><asp:TextBox ID="txtACReading801" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial801" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading801" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial801" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading801" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial801" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName805" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>805</td>
                    <td><asp:TextBox ID="txtACReading805" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial805" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading805" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial805" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName810" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>810</td>
                    <td><asp:TextBox ID="txtACReading810" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial810" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading810" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial810" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 8</td>
                    <td>3.95</td>
                    <td>Lobby 8</td>
                    <td><asp:TextBox ID="txtACReadingLoby801" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby801" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby801" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby801" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                  <tr align="center">
                    <td rowspan="4">7</td>
                    <td><asp:TextBox ID="txtCompanyName701" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>701</td>
                    <td><asp:TextBox ID="txtACReading701" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial701" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading701" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial701" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading701" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial701" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName705" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>705</td>
                    <td><asp:TextBox ID="txtACReading705" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial705" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading705" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial705" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName710" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>710</td>
                    <td><asp:TextBox ID="txtACReading710" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial710" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading710" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial710" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 7</td>
                    <td>3.95</td>
                    <td>Lobby 7</td>
                    <td><asp:TextBox ID="txtACReadingLoby701" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby701" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby701" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby701" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                  <tr align="center">
                    <td rowspan="4">6</td>
                    <td><asp:TextBox ID="txtCompanyName601" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>601</td>
                    <td><asp:TextBox ID="txtACReading601" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial601" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading601" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial601" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading601" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial601" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName605" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>605</td>
                    <td><asp:TextBox ID="txtACReading605" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial605" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading605" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial605" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName610" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>610</td>
                    <td><asp:TextBox ID="txtACReading610" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial610" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading610" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial610" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 6</td>
                    <td>3.95</td>
                    <td>Lobby 6</td>
                    <td><asp:TextBox ID="txtACReadingLoby601" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby601" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby601" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby601" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                  <tr align="center">
                    <td rowspan="4">5</td>
                    <td><asp:TextBox ID="txtCompanyName501" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>501</td>
                    <td><asp:TextBox ID="txtACReading501" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial501" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading501" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial501" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading501" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial501" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName505" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>505</td>
                    <td><asp:TextBox ID="txtACReading505" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial505" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading505" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial505" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName510" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>510</td>
                    <td><asp:TextBox ID="txtACReading510" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial510" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading510" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial510" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 5</td>
                    <td>3.95</td>
                    <td>Lobby 5</td>
                    <td><asp:TextBox ID="txtACReadingLoby501" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby501" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby501" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby501" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                  <tr align="center">
                    <td rowspan="4">3</td>
                    <td><asp:TextBox ID="txtCompanyName301" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>301</td>
                    <td><asp:TextBox ID="txtACReading301" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial301" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading301" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial301" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading301" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial301" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName305" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>305</td>
                    <td><asp:TextBox ID="txtACReading305" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial305" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading305" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial305" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName310" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>310</td>
                    <td><asp:TextBox ID="txtACReading310" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial310" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading310" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial310" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 3</td>
                    <td>3.95</td>
                    <td>Lobby 3</td>
                    <td><asp:TextBox ID="txtACReadingLoby301" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby301" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby301" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby301" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                  <tr align="center">
                    <td rowspan="4">2</td>
                    <td><asp:TextBox ID="txtCompanyName201" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>201</td>
                    <td><asp:TextBox ID="txtACReading201" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial201" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading201" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial201" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading201" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial201" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName205" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>205</td>
                    <td><asp:TextBox ID="txtACReading205" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial205" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading205" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial205" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName210" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>210</td>
                    <td><asp:TextBox ID="txtACReading210" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial210" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading210" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial210" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 2</td>
                    <td>3.95</td>
                    <td>Lobby 2</td>
                    <td><asp:TextBox ID="txtACReadingLoby201" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby201" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby201" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby201" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                  <tr align="center">
                    <td rowspan="4">1</td>
                    <td><asp:TextBox ID="txtCompanyName101" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>101</td>
                    <td><asp:TextBox ID="txtACReading101" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial101" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading101" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial101" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorReading101" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td rowspan="4"><asp:TextBox ID="txtACOutdoorInitial101" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center" >
                    <td><asp:TextBox ID="txtCompanyName105" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>31.59</td>
                    <td>105</td>
                    <td><asp:TextBox ID="txtACReading105" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial105" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading105" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial105" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td><asp:TextBox ID="txtCompanyName110" runat="server"  placeholder="N/A" Width="100%" Enabled="false"></asp:TextBox></td>
                    <td>49.36</td>
                    <td>110</td>
                    <td><asp:TextBox ID="txtACReading110" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitial110" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReading110" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitial110" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>
                  <tr align="center">
                    <td>Lobby 1</td>
                    <td>3.95</td>
                    <td>Lobby 1</td>
                    <td><asp:TextBox ID="txtACReadingLoby101" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtACInitialLoby101" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACReadingLoby101" runat="server"  placeholder="KWh" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtNonACInitialLoby101" runat="server"  placeholder="Initial Officer" Width="100%" ></asp:TextBox></td>
                  </tr>                
                  <tr>
                    <td colspan="10">&nbsp</td>
                  </tr>
                </table>
            </div>

            <div class="col-xs-12 col-sm-12" align="right">
                <div class="row">&nbsp</div>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click"/>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                <div class="row">&nbsp</div>
            </div>   
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
