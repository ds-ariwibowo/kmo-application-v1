<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="C_Script.ascx.cs" Inherits="KMO.Controls.C_Script" %>
<!-- This File should be placed at the end of the document (before body or html) so the pages load faster -->
<!-- Hanya untuk settingan jquery spesifik. Library tetap diload di tag <head> -->

<!-- Jquery Marquee : Animasi Text dari kanan ke kiri -->
<script type="text/javascript">
    $('.marquee').marquee({
        duration: 20000,
        gap: 1,
        pauseOnHover: true,
    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        
//        $('.datepicker').datepicker({
//            format: 'dd-mm-yyyy',
//            autoclose: 'true',
//            todayHighlight: 'true',
//        })

//        $('.input-group.date').datepicker({
//            format: 'dd-mm-yyyy',
//            autoclose: 'true',
//            todayHighlight: 'true',
//        });

    });

    //// fungsi utk textbox number only
    function isNumber(event, id) {
        event = (event) ? event : window.event;
        var x = document.getElementById(id).value;
        var y = x.substring(0,1);
        var result = true;

        var charCode = (event.which) ? event.which : event.keyCode;
        //alert(x + ', ' + y + ', ' + charCode);
        if (charCode > 31 && (charCode < 48 || charCode > 57)) { result = false; } // harus 0 -> 9
        if (x == '' && y == '' && charCode == 48) { result = false; } // tidak boleh input angka 0 di sebelah kiri
        //if (x.length > 0 && charCode == 48) { result = false; } // angka paling kiri tidak boleh 0 setelah ada angka

        return result;
    }

    //// fungsi utk separator seribu
    function numberFormat(event, num, sep) {
        if (event.keyCode < 37 || event.keyCode > 40) {
            //if (!event.shiftKey) {
                a = num.value;
                b = a.replace(/[^\d]/g, "");
                c = "";
                nlength = b.length;
                j = 0;
                for (i = nlength; i > 0; i--) {
                    j = j + 1;
                    if (((j % 3) == 1) && (j != 1)) {
                        c = b.substr(i - 1, 1) + sep + c;
                    }
                    else {
                        c = b.substr(i - 1, 1) + c;
                    }
                }
                num.value = c;
            //}
        }
    }


</script>

