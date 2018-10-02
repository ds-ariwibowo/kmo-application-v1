<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="MeterReading.aspx.cs" Inherits="KMO.MeterReading" %>
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

            $('#<%=txtACInitialOfficer.ClientID%>').keyup(function (e) {
                var txtVal = $(this).val(); $('#<%=txtNonACInitialOfficer.ClientID%>').val(txtVal);
                var txtVal = $(this).val(); $('#<%=txtACOutdoorInitialOfficer.ClientID%>').val(txtVal);
                var txtVal = $(this).val(); $('#<%=txtACInitialOfficerLobby.ClientID%>').val(txtVal);
            });

            
            //var dpAdd = $('#<%=txtInputDate.ClientID%>');
            //dpAdd.datepicker({
            //    changeMonth: true,
            //    changeYear: true,
            //  format: "dd-mm-yyyy"
            //}).on('changeDate', function (ev) {
            //    $(this).blur();
            //    $(this).datepicker('hide');
            //});
            
            //check textbox empty or not
            $("#<%= btnSave.ClientID %>").click(function () {
            if ($(" #<%= ddlMonthPeriod.ClientID %>").val() != "") {
                if ($("#<%= txtYearPeriod.ClientID %>").val() != "") {
                        return true;
                    }
                    else {
                        alert('Year periiode is empty.')
                        return false;
                    }
                }
                else {
                    alert('Month period is empty.')
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

            <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
            <div class="col-xs-12 col-sm-12"><h4>PLN Meter Reading</h4></div>
            <div class="col-xs-12 col-sm-12" style="padding:0 5 0 0;" >
                <div class="row">
                    <asp:Label ID="lblEdit" Visible="false" runat="server"></asp:Label>
                    <div class="col-xs-12 col-sm-1">Payment Period</div>
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
                        <asp:TextBox  class="form-control input-sm" ID="txtYearPeriod" runat="server"  
                            placeholder="Year" MaxLength="4" Width="60px"></asp:TextBox>
                    </div>                                        
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-1">Usage Month</div>
                    <div class="col-xs-12 col-sm-1" align="left"><asp:TextBox class="form-control input-sm" ID="txtUsageMonth" runat="server"  placeholder="Month" Width="100%" Enabled="false"></asp:TextBox></div>
                    <div class="col-xs-12 col-sm-1">Usage Year</div>
                    <div class="col-xs-12 col-sm-1" align="left"><asp:TextBox class="form-control input-sm" ID="txtUsageYear" runat="server"  placeholder="Year" Width="100%" Enabled="false"></asp:TextBox></div>
                    <div class="col-xs-12 col-sm-1">KWh Rate</div>
                    <div class="col-xs-12 col-sm-1" align="left"><asp:TextBox class="form-control input-sm" ID="txtKWhRate" runat="server"  placeholder="KWh Rate" Width="100%" Enabled="false"></asp:TextBox></div>
                </div>                
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-1">Floor</div>
                    <div class="col-xs-12 col-sm-1" align="left">
                        <asp:DropDownList class="form-control input-sm" ID="ddlFloor" runat="server" Enabled="false" >
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
                    <div class="col-xs-12 col-sm-1">Suite No</div>
                    <div class="col-xs-12 col-sm-1" align="left">                        
                        <asp:TextBox class="form-control input-sm" ID="txtSuiteNo" runat="server" MaxLength="3" placeholder="Suite No" Width="50px" OnKeyPress="return isNumberKey(this, event);" Enabled="false"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-12 col-sm-1">CID</div>
                    <div class="col-xs-12 col-sm-2" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtCompanyID" runat="server"  placeholder="Company ID" Width="87%" Visible="false" ></asp:TextBox>
                        <asp:TextBox class="form-control input-sm" ID="txtCFSID" runat="server"  placeholder="Client ID" Width="100%" Enabled="false"></asp:TextBox>                        
                    </div>
                    <div class="col-xs-12 col-sm-2">Company Name</div>
                    <div class="col-xs-12 col-sm-2" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtCompanyName" runat="server"  placeholder="Company Name" Width="100%" Enabled="false"></asp:TextBox>                        
                    </div>
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-1" align="left">
                        <asp:Button CssClass="btn btn-info btn-xs" ID="btnSearch" runat="server" Text="Search Tenant" OnClick="btnSearch_Click"/>
                    </div>
                </div>                
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-2" align="center">UTL:ECD Record Date</div>
                    <div class="col-xs-12 col-sm-4" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtInputDate" runat="server"  placeholder="UTL:ECD Record Date" Width="100%"></asp:TextBox>(dd-MM-yyyy)
                    </div>             
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-2" align="center"></div>
                    <div class="col-xs-12 col-sm-5" align="center">Reading Meter (Kwh)</div>
                    <div class="col-xs-12 col-sm-5" align="center">Initial Officer</div>
                </div>              
                <div class="row">
                    <div class="col-xs-12 col-sm-2" align="center">AC</div>
                     <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtACReading" runat="server"  placeholder="AC Meter" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                    </div>
                    <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtACInitialOfficer" runat="server"  placeholder="AC Initial Officer" Width="100%"></asp:TextBox>
                    </div>                    
                </div>
                <div class="row">&nbsp</div>          
                <div class="row">
                    <div class="col-xs-12 col-sm-2" align="center">Non AC</div>
                    <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtNonACReading" runat="server"  placeholder="Non AC Meter" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                    </div>
                    <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtNonACInitialOfficer" runat="server"  placeholder="Non AC Initial Officer" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row">&nbsp</div>
                <div class="row">
                    <div class="col-xs-12 col-sm-2" align="center">AC Outdoor Unit</div>
                    <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtACOutdoor" runat="server"  placeholder="AC Outdoor Meter" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                    </div>
                    <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtACOutdoorInitialOfficer" runat="server"  placeholder="AC Outdoor Initial Officer" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row">&nbsp</div>          
                <div class="row">
                    <div class="col-xs-12 col-sm-2" align="center">Lobby</div>
                    <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtACReadingLobby" runat="server"  placeholder="AC Lobby Meter" Width="100%" OnKeyPress="return isNumberKey(this, event);"></asp:TextBox>
                    </div>
                    <div class="col-xs-12 col-sm-5" align="left">
                        <asp:TextBox class="form-control input-sm" ID="txtACInitialOfficerLobby" runat="server"  placeholder="AC Lobby Initial Officer" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <div class="row">&nbsp</div>
                <div align="right">
                    <div class="row">&nbsp</div>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                    <div class="row">&nbsp</div>
                </div> 
                <div class="row">&nbsp</div>
                <div class="row">
                    <!-- GridView -->
                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center"
                        AutoGenerateColumns="False" AllowPaging="True"
                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                        CssClass="GridViewStyle" Width="100%" DataKeyNames="ID"
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
                        <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="70px" 
                            ButtonType="Button" Text="Edit" HeaderText="Edit"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="70px"
                            ButtonType="Button" Text="Delete" HeaderText="Delete"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        
                        <asp:BoundField DataField="Month" HeaderText="Month"/>
                        <asp:BoundField DataField="Year" HeaderText="Year" />                        
                        <asp:BoundField DataField="CID" HeaderText="CID"/>
                        <asp:BoundField DataField="Name" HeaderText="Company Name" />
                        <asp:BoundField DataField="Floor" HeaderText="Floor" />
                        <asp:BoundField DataField="SuiteNo" HeaderText="Suite No" />
                        <asp:BoundField DataField="ReadingAC" HeaderText="AC Reading"/>
                        <asp:BoundField DataField="InitialOfficerAC" HeaderText="AC Initiator Officer"/>
                        <asp:BoundField DataField="ReadingNonAC" HeaderText="Non AC Reading"/>
                        <asp:BoundField DataField="InitialOfficerNonAC" HeaderText="Non AC Initiator Officer"/>
                        <asp:BoundField DataField="ReadingACOutdoor" HeaderText="AC Outdoor Reading"/>
                        <asp:BoundField DataField="InitialOfficerACOutdoor" HeaderText="AC Outdoor Initiator Officer"/>
                        <asp:BoundField DataField="ReadingACLobby" HeaderText="Lobby AC Reading"/>
                        <asp:BoundField DataField="InitialOfficerACLobby" HeaderText="Lobby AC Initiator Officer"/>
                        <asp:BoundField DataField="InputDate" HeaderText="Input Date"/>
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
            </div>            
            </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Select tenant form -->
            <div class="modal fade" id="selectTenantModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
                <div class="modal-dialog" style="z-index: 1070">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <strong><asp:Label ID="Label2" runat="server">Select tenant</asp:Label></strong>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                    <!-- GridView -->
                                    <asp:GridView ID="GridViewSelectTenant" runat="server" HorizontalAlign="Center"
                                        AutoGenerateColumns="False" AllowPaging="True"
                                        BorderStyle="Double" BorderColor="Black" EnableModelValidation="True" EmptyDataText="Data Not Found"
                                        CssClass="GridViewStyle" Width="100%" DataKeyNames="ID"
                                        ShowHeader="true" ShowFooter="false" PageSize="10" valign="left"  
                                        OnRowCommand="GridViewSelectTenant_RowCommand"
                                        OnPageIndexChanging="GridViewSelectTenant_PageIndexChanging">

                                        <AlternatingRowStyle BackColor="#EEE" />
                                        <SelectedRowStyle BackColor="#FFF" Font-Bold="False" ForeColor="Navy" />
                    
                                        <Columns>
                                            <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="center"
                                                ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="selectRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                                                ItemStyle-Width="70px"
                                                ButtonType="Button" Text="Select" HeaderText="Select"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                                                <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                                            </asp:ButtonField>
                                            
                                            <asp:BoundField DataField="CID" HeaderText="CID"/>
                                            <asp:BoundField DataField="Company Name" HeaderText="Company Name"/>
                                            <asp:BoundField DataField="Floor" HeaderText="Floor"/>
                                            <asp:BoundField DataField="SuiteNo" HeaderText="Suite No"/>
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
                                </div>

                                <div class="modal-footer">
                                    <button  type="button" class="btn btn-info" data-dismiss="modal" aria-hidden="Close">Close</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <!-- Delete Modal Start Here --->
            <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H2">Delete Master Item</h4>
                    </div>

                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="form-horizontal">
                                    Are you sure to delete this record?
                                    <asp:HiddenField ID="hfCode" runat="server" />
                                    <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
                                
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
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click"/>
                        <button class="btn btn-info" data-dismiss="Close" 
                        aria-hidden="true">Close</button>
                    </div>
                </div>
                </div>
            </div>

            <!-- Message form -->
            <div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
                <div class="modal-dialog" style="z-index: 1060">
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
