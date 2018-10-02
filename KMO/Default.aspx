<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KMO._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="CPH_HEAD">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CPH_BODY_CONTENT">
<div style="padding:10px 0 0 0;">
    <div class="row">
        <div class="col-xs-12 col-sm-12">
            <div class="well well-lg">
                <div class="row">
                    <div class="col-xs-12 col-sm-3">
                        <img src="<%= Page.ResolveUrl("~") %>Assets/_img/KMO_Building.jpg" style="margin:0 auto;" class="img-responsive" />
                    </div>
                    <div class="col-xs-12 col-sm-9">
                        <p style="text-align: justify;">KMO building which stands for Kyai Maja Office building is under the management of PT. Primanusa Graha &ndash; a real estate company and an affiliated company of CONNUSA Group.</p>

                        <p style="text-align: justify;">In addition to PT Primanusa Graha, CONNUSA Group is diversified into many businesses with affiliated companies as follows:</p>

                        <p style="text-align: justify;">PT CONNUSA Energindo, one of the leading engineering consultants in the energy sector in Indonesia.<br />
                        PT CALNUSA Pratama whose main business is in the procurement and installation of high voltage electrical equipment for substations.<br />
                        PT HEBINUSA Prakasa whose business is in the supply of high-strength steel pipes for the oil and gas industries.<br />
                        PT OSHA Asia, a safety shoes manufacturer under the brand name of Dr.OSHA Safety Shoes.<br />
                        Although CONNUSA&rsquo;s core business is in the energy sector, KMO Building was designed and erected by PT CONNUSA&rsquo;s professional engineers from Feasibility Study, Architectural Design, Detailed Engineering Design, Construction Management through Testing and Commissioning. The building was completed in December 2015.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
