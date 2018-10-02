<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="SumCFS.aspx.cs" Inherits="KMO.Report.SumCFS" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="CPH_HEAD">
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
    <asp:ScriptManager runat="server" ID="ScriptManager1" />
    <div style="padding:10px 0 0 0;">
        <div class="col-xs-12 col-sm-12">
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

            <div class="col-xs-12 col-sm-2 col-sm-offset-10">
                <asp:Button ID="btnAdd" runat="server" Text="Add New Record" 
                    CssClass="btn btn-info btn-xs right" 
                    style="width: 100%;"
                    OnClick="btnAdd_Click" /> <br /><br />            
            </div>

            <!-- GridView -->
            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate>                                                
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
                        <asp:ButtonField CommandName="detail" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="90px"
                            ButtonType="Button" Text="Detail" HeaderText="Detailed View"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="editRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="90px" 
                            ButtonType="Button" Text="Edit" HeaderText="Edit Record"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="deleteRecord" ControlStyle-CssClass="btn btn-info btn-xs"
                            ItemStyle-Width="90px"
                            ButtonType="Button" Text="Delete" HeaderText="Delete Record"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center">
                            <ControlStyle CssClass="btn btn-info btn-xs"></ControlStyle>
                        </asp:ButtonField>
                        
                        <asp:BoundField DataField="CID" HeaderText="CID" />
                        <asp:BoundField DataField="LOO Signed Date" HeaderText="LOO Signed Date" DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Company Name" HeaderText="Company Name" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="PIC Name" HeaderText="PIC Name" />
                        <asp:BoundField DataField="PIC Mobile Phone" HeaderText="PIC Mobile Phone"/>
                        <asp:BoundField DataField="PIC Email" HeaderText="PIC Email"/>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- <asp:BoundField DataField="SuiteNo" HeaderText="Suite"/> -->
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
                                        <asp:Label ID="lblIDSuite" Visible="false" runat="server"></asp:Label>
                                
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
        </div>
    </div>
</asp:Content>

