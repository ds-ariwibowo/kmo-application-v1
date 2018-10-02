<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="LOO.aspx.cs" Inherits="KMO.LOO" %>
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
            //dtpicker
            var dp = $('#<%=txtCommencementDate.ClientID%>, #<%=txtHandoverDate.ClientID%>, #<%=txtServiceChargeCommencementDate.ClientID%>, #<%=txtLeaseAgreementComencementDate.ClientID%>, #<%=txtSubjectDate.ClientID%>');
            dp.datepicker({
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
<asp:Content ID="BodyContent" ContentPlaceHolderID="CPH_BODY_CONTENT" runat="server">
    <div class="container">
        <asp:ScriptManager runat="server" ID="ScriptManager1" />
        <div class="col-xs-12 col-sm-12"><h4>Letter of Offer</h4></div>

        <div class="row col-xs-12 col-sm-12">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">1.0 Tenant & Premises (LOO 2,0 & 4.0)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">2.1 Company Name</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtCompanyName" class="form-control input-sm" 
                                    placeholder="Company name" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">CID No</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtCID" class="form-control input-sm" 
                                    runat="server" Width="100%" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">4.3 Total Area Rented, M2</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtTotalAreaRented" class="form-control input-sm"
                                    placeholder="Total area rented (m2)"
                                    OnKeyPress="return isNumberKey(this, event);" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">4.5 Unit Number</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtUnitNumber" class="form-control input-sm" 
                                    placeholder="Unit number"
                                    OnKeyPress="return isNumberKey(this, event);" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">4.4 Floor Level</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:DropDownList class="form-control input-sm" ID="ddlFloor" runat="server" >
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="9">9th</asp:ListItem>
                                    <asp:ListItem Value="8">8th</asp:ListItem>
                                    <asp:ListItem Value="7">7th</asp:ListItem>
                                    <asp:ListItem Value="6">6th</asp:ListItem>
                                    <asp:ListItem Value="5">5th</asp:ListItem>
                                    <asp:ListItem Value="3">3rd</asp:ListItem>
                                    <asp:ListItem Value="2">2nd</asp:ListItem>
                                    <asp:ListItem Value="1">1st</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-2">4.6 Condition (Floor)</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtConditionFloor" class="form-control input-sm" 
                                    runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row col-xs-12 col-sm-12">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">2.0 Dates (LOO Terms 5.0)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">5.1 Lease Period</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtLeasedPeriod" class="form-control input-sm" 
                                    OnKeyPress="return isNumberKey(this, event);" placeholder="Leased period (year)" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">5.5 Rent Commencement Date or Occupation Date, whichever Earlier</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtCommencementDate" class="form-control input-sm" 
                                    placeholder="Rent commencement date" runat="server" Width="100%" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">5.2 Hand-over Date for Fitting-Out</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtHandoverDate" class="form-control input-sm" 
                                    placeholder="Handover date" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">5.6 Service Charge Commencement Date</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtServiceChargeCommencementDate" class="form-control input-sm" 
                                    placeholder="Service charge commencement date" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">5.3 Fitting-Out Period</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtFittingOutPeriod" class="form-control input-sm" 
                                    OnKeyPress="return isNumberKey(this, event);" placeholder="Fitting-out period (weeks)" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">Lease Expiry Date</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtLeaseExpiryDate" class="form-control input-sm" 
                                    runat="server" Width="100%" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">5.4 Lease Agreement Commencement Date</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtLeaseAgreementComencementDate" class="form-control input-sm" 
                                    placeholder="Lease agreement commencement date" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
        </div>
        <div class="row col-xs-12 col-sm-12">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">3.0 Payment (LOO Terms 6.0&7.0)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">6.1 Rental Rate (psmpm) & 7.1 Security Deposit</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtRentalRate" class="form-control input-sm" 
                                    OnKeyPress="return isNumberKey(this, event);"
                                    placeholder="Rental rate" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">6.3 Service Charge (psmpm) & 7.2 Service Charge Deposit</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtServiceCharge" class="form-control input-sm" 
                                    OnKeyPress="return isNumberKey(this, event);"
                                    placeholder="Service charge" runat="server" Width="100%" ></asp:TextBox>
                            </div>                            
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">6.2 Rent Payable in Advance</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:DropDownList ID="ddlRentPayable" runat="server" class="form-control input-sm">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="10">1 (One) Month</asp:ListItem>
                                <asp:ListItem Value="11">3 (Three) Months</asp:ListItem>
                                <asp:ListItem Value="12">6 (Six) Months</asp:ListItem>
                                <asp:ListItem Value="13">12 (Twelve) Months</asp:ListItem>
                                <asp:ListItem Value="99">Other</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <asp:TextBox ID="txtRentPayableOther" class="form-control input-sm" runat="server" placeholder="Other" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row col-xs-12 col-sm-12">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">4.0 Special Conditions (LOO Terms 28.0: Additional Terms)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">28.1</div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt281a" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" enabled="false"
                                    placeholder="" runat="server" Width="100%">Car Parking
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt281b" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="Rental rate" runat="server" Width="100%" Enabled="false">Lessor shall provide on Unreserved Parking to the Lesse free of charge
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">28.2</div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt282a" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%">                                    
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt282b" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%">                                    
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">28.3</div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt283a" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%">                                    
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt283b" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%">                                    
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">28.4</div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt285a" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%">                                    
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5" >
                                <asp:TextBox ID="txt285b" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%">                                    
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>                                        
                </div>
            </div>
        </div>
        <div class="row col-xs-12 col-sm-12">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">5.0 Special Conditions (LOO Terms 28.0: Amendements)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">22.2 Was</div>
                            <div class="col-xs-12 col-sm-10" >
                                <asp:TextBox ID="txt222a" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%" Enabled="false">The Lessee shall responsible for all lost or damages including damaged by fire, storm, smoke, and electricity and shallarrange insurance through a reputable and approved insurance company, acceptable to the Lessor. The Lessee is required to keep all insurances valid. The insurance policies, certificate and endorsements are subject to inspection by the Lessot at any time throughout the period covered by the Lease Agreement.
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">22.2 Now</div>
                            <div class="col-xs-12 col-sm-10" >
                                <asp:TextBox ID="txt222b" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%" >                                    
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">22.3 Was</div>
                            <div class="col-xs-12 col-sm-10" >
                                <asp:TextBox ID="txt223a" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%" >                                    
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">22.3 Now</div>
                            <div class="col-xs-12 col-sm-10" >
                                <asp:TextBox ID="txt223b" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine" 
                                    placeholder="" runat="server" Width="100%" >                                    
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row col-xs-12 col-sm-12">
            <div class="col-xs-12 col-sm-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Cover Letter for LOO</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">Subject: Office Space Lease per Our Last</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtSubject" class="form-control input-sm" 
                                    placeholder="Subject letter" runat="server" Width="100%">
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">Date</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtSubjectDate" class="form-control input-sm" 
                                    placeholder="Dated" runat="server" Width="100%">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row col-xs-12 col-sm-12">
            <div class="row col-xs-12 col-sm-12" align="right">
                <div class="row">&nbsp</div>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                <div class="row">&nbsp</div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="FooterContent" ContentPlaceHolderID="CPH_SCRIPT" runat="server">
</asp:Content>
