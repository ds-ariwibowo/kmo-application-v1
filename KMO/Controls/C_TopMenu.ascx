<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="C_TopMenu.ascx.cs" Inherits="KMO.Controls.C_TopMenu" %>
<div class="col-xs-12">
    <br />
    <ul class="nav nav-tabs">
        <li role="presentation" runat="server" id="li_home"><a href="./Default.aspx">HOME</a></li>
        <% if (Session["userid"] != null) {%>

        <% if (Session["userdept"].ToString() == "32" || Session["userdept"].ToString() == "19" || Session["userdept"].ToString() == "3" || Session["userdept"].ToString() == "16" ) {%>
        <li role="presentation" class="dropdown" runat="server" id="li_setup">
            <a class="dropdown-toggle" data-toggle="dropdown" style="cursor:pointer;" href="#" role="button" aria-expanded="false">
                SETUP <span class="caret"></span>
            </a>
            <ul class="dropdown-menu" role="menu">
                <li class="dropdown-submenu"><a tabindex="-1" href="#">Parameter</a>
                    <ul class="dropdown-menu">
                    <li><a tabindex="-1" href="./UnitPrice.aspx">Unit Price</a></li>
                    <li><a tabindex="-1" href="#">Par. 2</a></li>
                    </ul>
                </li>
                <li><a href="#">Master</a></li>
            </ul>
        </li>
        
        <li role="presentation" class="dropdown" runat="server" id="li1">
            <a class="dropdown-toggle" data-toggle="dropdown" style="cursor:pointer;" href="#" role="button" aria-expanded="false">
                TRANSACTION <span class="caret"></span>
            </a>
            <ul class="dropdown-menu" role="menu">
                <li><a href="./CFS.aspx">CFS - Client Fact Sheet</a></li>
                <li><a href="./MeterReading.aspx">UTL:ECD - Utility: Electricity Consumption Distribution</a></li>
                <li><a href="./RentService.aspx">Rent & Service Charge</a></li>
                <li><a href="./SecurityDeposit.aspx">Security Deposit</a></li>
                <li><a href="./Parking.aspx">Parking</a></li>
                <li><a href="./Others.aspx">Others</a></li>
            </ul>
        </li>

        <li role="presentation" class="dropdown" runat="server" id="li_laporan">
            <a class="dropdown-toggle" data-toggle="dropdown" style="cursor:pointer;" href="#" role="button" aria-expanded="false">
                REPORT <span class="caret"></span>
            </a>
            <ul class="dropdown-menu" role="menu">
                <li><a href="./SumCFS.aspx">SUM:CFS - Summary: Client Fact Sheet</a></li>                
                <!--<li><a href="#">SUM:UTL:ECD - Summary: Utility: Electricity Consumption Distribution</a></li>-->
                <li><a href="./ReportMeter.aspx">Print Invoice PLN</a></li>
                <li><a href="./ReportRentService.aspx">Print Invoice Rent & Service Charge</a></li>
                <li><a href="./ReportSecurityDeposit.aspx">Print Invoice Security Deposit</a></li>
                <li><a href="./ReportParkir.aspx">Print Invoice Parking</a></li>              
                <li><a href="./ReportOthers.aspx">Print Invoice Others</a></li>
            </ul>
        </li>     
        <%} %>

        <% if (Session["userdept"].ToString() == "32" || Session["userdept"].ToString() == "3" || Session["userdept"].ToString() == "16" ) {%>
        <li role="presentation" class="dropdown" runat="server" id="li_cop">
            <a class="dropdown-toggle" data-toggle="dropdown" style="cursor:pointer;" href="#" role="button" aria-expanded="false">
                COP <span class="caret"></span>
            </a>
            <ul class="dropdown-menu" role="menu">
                <li><a href="./copPLN.aspx">COP:Invoice PLN</a></li>
                <li><a href="./copRAS.aspx">COP:Invoice Rent & Service Charge</a></li>
                <li><a href="./copSEC.aspx">COP:Invoice Security Deposit</a></li>
                <li><a href="#">COP:Invoice Parking</a></li>                                
                <li><a href="./copOTH.aspx">COP:Invoice Others</a></li>
            </ul>
        </li>
        <%} %>
        <li role="presentation" runat="server" id="li2"><a href="./Default.aspx?LogOut=true">LOGOUT</a></li>
        <%} %>
    </ul>
</div>
