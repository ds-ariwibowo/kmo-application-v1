<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="UnitPrice.aspx.cs" Inherits="KMO.UnitPrice" %>
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
            
            <asp:TextBox ID="txtID" runat="server"  placeholder="" Width="100%" visible="false"></asp:TextBox>
            <div class="row">
                <div class="col-xs-12 col-sm-2" align="center">Used Period</div>
                <div class="col-xs-12 col-sm-5" align="left">
                    <asp:DropDownList ID="ddlMonthPeriod" runat="server" 
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
                    <asp:TextBox ID="txtYearPeriod" runat="server"  placeholder="Year" MaxLength="4" Width="40px"></asp:TextBox>
                </div>
            </div>
            <div class="row">&nbsp</div>
            <div class="row">
                <div class="col-xs-12 col-sm-2" align="center">PLN Rate</div>
                <div class="col-xs-12 col-sm-5" align="left">
                    <asp:TextBox ID="txtKWhRate" runat="server"  placeholder="KWh Rate" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                </div>            
            </div>
            <div class="row">&nbsp</div>
            <div class="row">
                <div class="col-xs-12 col-sm-2" align="center">PPJ (%)</div>
                    <div class="col-xs-12 col-sm-5" align="left">
                    <asp:TextBox ID="txtPPJ" runat="server"  placeholder="PPJ" CostWidth="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                </div>            
            </div>
            <div class="row">&nbsp</div>
            <div class="row">
                <div class="col-xs-12 col-sm-2" align="center">Materai</div>
                    <div class="col-xs-12 col-sm-5" align="left">
                    <asp:TextBox ID="txtMaterai" runat="server"  placeholder="Materai Cost" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                </div>            
            </div>
            <div class="row">&nbsp</div>
            <div class="row">
                <div class="col-xs-12 col-sm-2" align="center">Admin (%)</div>
                    <div class="col-xs-12 col-sm-5" align="left">
                    <asp:TextBox ID="txtAdmin" runat="server"  placeholder="Admin Cost" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                </div>            
            </div>
            <div class="row">&nbsp</div>
                <div align="right">
                    <div class="row">&nbsp</div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                    <div class="row">&nbsp</div>
                </div> 
        </div>
    </div>
</asp:Content>
<asp:Content ID="FooterContent" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
