$(function () {
    var status = $("#ProcessMessage").val();
    $("#imgloader").css("display", "none");
    if (status == "True" || status == "true")
        MessageBox("İşlem Başarıyla Tamamlandı", "info");
    else if (status == "False" || status == "false")
        MessageBox("İşlem Sırasında Bir Hata Oluştu.", "alert");

    $("#tabs").tabs();

    $("#Hardware").attr("checked",false);
    $('.pprice').css("display", "none");

   
    $('#txtPrice').numeric();
 

    $('#Hardware').click(function() {
        var stat = $('#Hardware:checked').val();
        if (stat == "True" || stat == "true") {
            $('.pprice').css("display", "block");
            $('#txtHardWarePrice').addClass("required");
            $('#txtHardWarePrice').numeric();
        }
        else {
            $('.pprice').css("display", "none");
            $('#txtHardWarePrice').removeClass("required");
        }
    });


    var selval = $("#Language option:selected").val();
    
    if (selval == "") {
        $("#ProductGroupId").attr("disabled", "disabled");
        $("#ProductGroupId").empty().append($("<option></option>").val("").html("Ürün Grubunu Seçiniz..."));
    }

    $("#Language").change(function () {
        
        var val = $("#Language option:selected").val();
        if (val == "") { $("#ProductGroupId").attr("disabled", true); }
        else {
            $("#imgloader").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadGroup',
                data: '{lang:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductGroupId").empty().append($("<option></option>").val("").html("Ürün Grubunu Seçiniz..."));

                    $.each(result, function (i, item) {
                        $("#ProductGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductGroupId").removeAttr("disabled");
                    $("#imgloader").css("display", "none");
                },
                error: function () {

                }
            });


        }
             
      
    });

    $("#ProductGroupId").change(function () {
        var val = $("#ProductGroupId option:selected").val();
        if (val == "") { $("#ProductSubGroupId").attr("disabled", true); }
        else {
            $("#imgloader2").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadSubGroup',
                data: '{id:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductSubGroupId").empty().append($("<option></option>").val("").html("Ürün Alt Grubunu Seçiniz..."));
                    $("#g1").css("display","block");
                    $.each(result, function (i, item) {
                        $("#ProductSubGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductSubGroupId").removeAttr("disabled");
                    $("#imgloader2").css("display", "none");
                },
                error: function () {

                }
            });
        }
    });

    $("#ProductSubGroupId").change(function () {
        var val = $("#ProductSubGroupId option:selected").val();
        if (val == "") { $("#ProductSubGroupId").attr("disabled", true); }
        else {
            $("#imgloader3").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadSubbestGroup',
                data: '{id:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductSubbestGroupId").empty().append($("<option></option>").val("37").html("Alt Grup Yok..."));
                    $("#g2").css("display", "block");

                    $.each(result, function (i, item) {
                        $("#ProductSubbestGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductSubbestGroupId").removeAttr("disabled");
                    $("#imgloader3").css("display", "none");
                },
                error: function () {

                }
            });
        }
    });

    $("#ProductSubbestGroupId").change(function () {
        var val = $("#ProductSubbestGroupId option:selected").val();
        if (val == "") { $("#ProductSubbestGroupId").attr("disabled", true); }
        else {
            $("#imgloader4").css("display", "inline-block");
            $.ajax({
                type: 'POST',
                url: '/Product/LoadSubSubbestGroup',
                data: '{id:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#ProductSubSubbestGroupId").empty().append($("<option></option>").val("5").html("Alt Grup Yok..."));
                    $("#g3").css("display", "block");

                    $.each(result, function (i, item) {
                        $("#ProductSubSubbestGroupId").append($("<option></option>").val(item.Value).html(item.Text));
                    });
                    $("#ProductSubSubbestGroupId").removeAttr("disabled");
                    $("#imgloader4").css("display", "none");
                },
                error: function () {

                }
            });
        }
    });

    
   
   
});
