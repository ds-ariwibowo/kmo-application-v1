<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="LeaseAgreement.aspx.cs" Inherits="KMO.LeaseAgreement" %>
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
            var dp = $('#<%=txtLOOSignedDate.ClientID%>');
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
                <div class="row">
                    <div class="col-xs-12 col-sm-2">LOO No</div>
                    <div class="col-xs-12 col-sm-4" >
                        <asp:TextBox ID="txtLOONo" class="form-control input-sm" 
                            placeholder="LOO No" runat="server" Width="100%"></asp:TextBox>
                    </div>
                    <div class="col-xs-12 col-sm-2">LOO Signed Date</div>
                    <div class="col-xs-12 col-sm-4" >
                        <asp:TextBox ID="txtLOOSignedDate" class="form-control input-sm" 
                            placeholder="LOO signed date" 
                            runat="server" Width="100%" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">&nbsp</div>
        <div class="row">&nbsp</div>
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
                                <asp:TextBox ID="txtCompanyName" class="form-control input-sm" enabled="false"
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
                                    placeholder="Total area rented (m2)" enabled="false"
                                    OnKeyPress="return isNumberKey(this, event);" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">4.5 Unit Number</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtUnitNumber" class="form-control input-sm" 
                                    placeholder="Unit number" enabled="false"
                                    OnKeyPress="return isNumberKey(this, event);" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">4.4 Floor Level</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:DropDownList class="form-control input-sm" ID="ddlFloor" enabled="false" runat="server" >
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
                                    enabled="false" runat="server" Width="100%"></asp:TextBox>
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
                                <asp:TextBox ID="txtLeasedPeriod" class="form-control input-sm" enabled="false"
                                    OnKeyPress="return isNumberKey(this, event);" placeholder="Leased period (year)" 
                                    runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">5.5 Rent Commencement Date</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtCommencementDate" class="form-control input-sm" enabled="false"
                                    placeholder="Rent commencement date" runat="server" Width="100%" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">5.2 Hand-over Date for Fitting-Out</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtHandoverDate" class="form-control input-sm" enabled="false"
                                    placeholder="Handover date" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">5.6 Service Charge Commencement Date</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtServiceChargeCommencementDate" class="form-control input-sm" enabled="false" 
                                    placeholder="Service charge commencement date" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">5.3 Fitting-Out Period</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtFittingOutPeriod" class="form-control input-sm" enabled="false"
                                    OnKeyPress="return isNumberKey(this, event);" placeholder="Fitting-out period (weeks)" 
                                    runat="server" Width="100%"></asp:TextBox>
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
                                <asp:TextBox ID="txtLeaseAgreementComencementDate" class="form-control input-sm" enabled="false"
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
                                    OnKeyPress="return isNumberKey(this, event);" enabled="false"
                                    placeholder="Rental rate" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-2">6.3 Service Charge (psmpm) & 7.2 Service Charge Deposit</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:TextBox ID="txtServiceCharge" class="form-control input-sm" 
                                    OnKeyPress="return isNumberKey(this, event);" enabled="false"
                                    placeholder="Service charge" runat="server" Width="100%" ></asp:TextBox>
                            </div>                            
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">6.2 Rent Payable in Advance</div>
                            <div class="col-xs-12 col-sm-4" >
                                <asp:DropDownList ID="ddlRentPayable" runat="server" enabled="false" class="form-control input-sm">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="10">1 (One) Month</asp:ListItem>
                                <asp:ListItem Value="11">3 (Three) Months</asp:ListItem>
                                <asp:ListItem Value="12">6 (Six) Months</asp:ListItem>
                                <asp:ListItem Value="13">12 (Twelve) Months</asp:ListItem>
                                <asp:ListItem Value="99">Other</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <asp:TextBox ID="txtRentPayableOther" class="form-control input-sm" runat="server" Enabled="false" placeholder="Other" Width="100%"></asp:TextBox>
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
                        <h3 class="panel-title">4.0 Special Conditions of Lease Agreement (Additional Articles)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <asp:Label ID="lblLOONo" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="TextBox1" class="form-control input-sm" 
                                        Rows="3" TextMode="MultiLine"
                                        placeholder="Rental rate" runat="server" Width="100%" Enabled="false">The Special Condition stipulated in the LOO no. 900-xxx (Appendix. A) shall be integral part of the following Special Conditions of Lease Agreement and can not be separated.
                                    </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:Label ID="Label1" runat="server">Article No.</asp:Label>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:Label ID="Label2" runat="server">Title</asp:Label>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:Label ID="Label3" runat="server">Content</asp:Label>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:TextBox ID="txtArticleNo1" class="form-control input-sm" 
                                        placeholder="Article no" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtTitle1" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Title" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtContent1" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Content" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:TextBox ID="txtArticleNo2" class="form-control input-sm" 
                                        placeholder="Article no" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtTitle2" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Title" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtContent2" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Content" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:TextBox ID="txtArticle3" class="form-control input-sm" 
                                        placeholder="Article no" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtTitle3" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Title" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtContent3" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Content" runat="server" Width="100%" >
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
                        <h3 class="panel-title">5.0 Special Conditions (Amendements)</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:Label ID="Label4" runat="server">Article No.</asp:Label>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:Label ID="Label5" runat="server">Title</asp:Label>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:Label ID="Label6" runat="server">Content</asp:Label>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:TextBox ID="txtArticleNoWas1" class="form-control input-sm" 
                                        placeholder="Article no (was)" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtTitleWas1" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Title" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtContentWas1" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Content" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:TextBox ID="txtArticleNoNow1" class="form-control input-sm" 
                                        placeholder="Article no (now)" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtTitleNow1" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Title" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtContentNow1" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Content" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:TextBox ID="txtArticleNoWas2" class="form-control input-sm" 
                                        placeholder="Article no (was)" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtTitleWas2" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Title" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtContentWas2" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Content" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="row">&nbsp</div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <asp:TextBox ID="txtArticleNoNow2" class="form-control input-sm" 
                                        placeholder="Article no (now)" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtTitleNow2" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Title" runat="server" Width="100%" >
                                </asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-5">
                                <asp:TextBox ID="txtContentNow2" class="form-control input-sm" 
                                    Rows="3" TextMode="MultiLine"    
                                    placeholder="Content" runat="server" Width="100%" >
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
                        <h3 class="panel-title">6.0 Appendix C</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-sm-1">Add file</div>
                            <div class="col-xs-12 col-sm-11" >
                                <asp:TextBox ID="txtFileAddress" class="form-control input-sm" 
                                    placeholder="File address" runat="server" Width="100%">
                                </asp:TextBox>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-success" />
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
