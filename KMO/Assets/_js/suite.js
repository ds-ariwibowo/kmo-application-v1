function isNumberKey(sender, evt) {
    var txt = sender.value;
    var dotcontainer = txt.split('.');
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (!(dotcontainer.length == 1 && charCode == 46) && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

//set suite value 
function f101() {
    var a = document.getElementById('<%=CPH_BODY_CONTENT_txtSuite101.ClientID%>').value
    document.getElementById('<%=CPH_BODY_CONTENT_txtSuite101RS.ClientID%>').value = a
}
function f110() {
    var a = document.getElementById('<%=CPH_BODY_CONTENT_txtSuite110.ClientID%>').value
    document.getElementById('<%=CPH_BODY_CONTENT_txtSuite110RS.ClientID%>').value = a
}
function f201() {
    var a = document.getElementById('<%=CPH_BODY_CONTENT_txtSuite201.ClientID%>').value
    document.getElementById('<%=CPH_BODY_CONTENT_txtSuite201RS.ClientID%>').value = a
}
function f210() {
    var a = document.getElementById('<%=CPH_BODY_CONTENT_txtSuite210.ClientID%>').value
    document.getElementById('<%=CPH_BODY_CONTENT_txtSuite210RS.ClientID%>').value = a
}
function f301() {
    var a = document.getElementById('<%=CPH_BODY_CONTENT_txtSuite301.ClientID%>').value
    document.getElementById('<%=CPH_BODY_CONTENT_txtSuite301RS.ClientID%>').value = a
}
function f305() {
    var a = document.getElementById('<%=CPH_BODY_CONTENT_txtSuite305.ClientID%>').value
    document.getElementById('<%=CPH_BODY_CONTENT_txtSuite305RS.ClientID%>').value = a
}
function f310() {
    var a = document.getElementById('<%=CPH_BODY_CONTENT_txtSuite310.ClientID%>').value
    document.getElementById('<%=CPH_BODY_CONTENT_txtSuite310RS.ClientID%>').value = a
}