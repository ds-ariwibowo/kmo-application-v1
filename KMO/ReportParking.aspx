<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="ReportParking.aspx.cs" Inherits="KMO.ReportParking" %>
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
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="CPH_BODY_CONTENT" runat="server">
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
            <div class="col-xs-12 col-sm-12"><h4>Invoice Parking</h4></div>
            <div class="col-xs-12 col-sm-12" style="padding:0 5 0 0;" >
                <div class="col-xs-12 col-sm-1">Period</div>
                    <div class="col-xs-12 col-sm-2" align="left">
                        <asp:DropDownList ID="ddlMonthPeriod" runat="server" 
                            AutoPostBack="true" >
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
                        <asp:TextBox ID="txtYearPeriod" runat="server"  placeholder="Year" MaxLength="4" Width="40px" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                    </div>
            </div>

            <asp:Button ID="btnOpenPDF" runat="server" Text="Proceed" CssClass="btn btn-success" OnClick="btnOpenPDF_Click"/>             
                   
        </div>
    </div>
</asp:Content>
<asp:Content ID="FooterContent" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
