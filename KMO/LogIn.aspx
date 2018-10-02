<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="KMO.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_HEAD" runat="server">
    <script type='text/javascript'>
        $(function () {
            $(".close").click(function () {
                $("#myAlert").alert();
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

            //check textbox empty or not
            $("#<%= btnLogin.ClientID %>").click(function () {
                if ($("#<%= txtUserName.ClientID %>").val() != "") {
                    if ($("#<%= txtPassword.ClientID %>").val() != "") {
                        return true;
                    }
                    else {
                        alert('Please enter your password')
                        return false;
                    }
                }
                else {
                    alert('Please enter your user name')
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
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_BODY_CONTENT" runat="server">
    <div class="container">
        <div class="row col-xs-12 col-sm-12">
        <asp:ScriptManager runat="server" ID="ScriptManager1" />
        <div style="padding:10px 0 0 0;">
        <div class="row">
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


            <!-- form area -->
            <div class="col-xs-12 col-sm-12">
                <div class="well well-lg">
                    <div class="row">
                        <div class="col-xs-12 col-sm-3">
                            <img src="<%= Page.ResolveUrl("~") %>Assets/_img/KMO_Building.jpg" style="margin:0 auto;" class="img-responsive" />
                        </div>
                        <div class="col-xs-12 col-sm-9" align="center">
                            <img src="<%= Page.ResolveUrl("~") %>Assets/_img/login.png" class="img-responsive" />
                            <div class="row">User Name : <asp:TextBox ID="txtUserName" runat="server" placeholder="username"></asp:TextBox></div>
                            <div class="row"> &nbsp</div>
                            <div class="row">Password &nbsp&nbsp&nbsp: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="password"></asp:TextBox></div>
                            <div class="row"> &nbsp</div>
                            <div class="row">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-default" OnClick="btnLogin_Click"/>                                
                            </div>
                            <div class="row">19092016</div>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
