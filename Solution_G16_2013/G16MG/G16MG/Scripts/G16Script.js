//註冊地址
function BookAddressEvent(InputZipCode, SelectCity, SelectDistrict) {
    var ZipCode = InputZipCode;
    var City = SelectCity;
    var District = SelectDistrict;

    function GetZipCode(did) {
        var zipcode = "";
        var ActionUrl = "/Person/GetZipCode?did=" + did + "";
        $.ajax({
            type: "Post",
            cache: false,
            url: ActionUrl,
            dataType: 'text',
            async: false,
            success: function (data) {
                // this assumes that the data-ajax-mode is always "replace": 
                zipcode = data;
            },
            error: function () {
                alert('載入失敗!');
            }
        });
        return zipcode;
    }

    function GetDistrict(cid) {
        var ActionUrl = "/Person/GetDistrict?cid=" + cid + "";
        $.ajax({
            type: "Post",
            cache: false,
            url: ActionUrl,
            dataType: 'html',
            async: false,
            success: function (data) {
                // this assumes that the data-ajax-mode is always "replace":                         
                District.html(data);
                ZipCode.val(GetZipCode(District.val()));
            },
            error: function () {
                alert('載入失敗!');
            }
        });
    }


    //載入ZipCode  
    ZipCode.val(GetZipCode(District.val()));

    //註冊CityChange 事件 
    City.change(function () {
        GetDistrict($(this).val());
    });
    //註冊DistrictChange事件
    District.change(function () {
        ZipCode.val(GetZipCode(District.val()));
    });

}

//註冊Datepicker 自訂初始日
function BookDatepickerEvent(obj, date) {
    obj.datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy/mm/dd'
    }).datepicker("setDate", date);
}

//註冊子母公司選單切換
function BookParentSubSelectChanged(parentSelect, subSelect, actionUrl) {

    function GetSubSelectOptions(id) {
        $.ajax({
            type: "Post",
            cache: false,
            url: actionUrl + id,
            dataType: 'html',
            async: true,
            success: function (data) {
                // this assumes that the data-ajax-mode is always "replace":                 
                subSelect.html(data);
            },
            error: function () {

                alert('載入失敗!');
            }
        });
    }

    //註冊parentSelectChange 事件 
    parentSelect.change(function () {
        GetSubSelectOptions($(this).val());
    });

}

//非同步更新,Replace
function AsyncUpdateReplace(actionUrl, type, divToReplace) {
    $.ajax({
        type: type,
        cache: false,
        url: actionUrl,
        dataType: 'html',
        async: true,
        success: function (data) {
            divToReplace.replaceWith(data);
        },
        error: function () {
           
            alert('載入失敗!');
            
        }
    });

}
//非同步更新,html
function AsyncUpdate(actionUrl, type, divToReplace) {
    $.ajax({
        type: type,
        cache: false,
        url: actionUrl,
        dataType: 'html',
        async: false,
        success: function (data) {

            divToReplace.html(data);

        },
        error: function () {
           
            alert('載入失敗!');
        }
    });
}
//自訂訊息提示
function G16Alert(dialogObj, positionId, timeout) {
    var myString = "center";
    var atString = "bottom";

    if (positionId == "") {
        myString = "center";
        atString = "center";
        positionId = window;
    }

    var Dialog = dialogObj.dialog({
        //modal: true,   //背景是否要變暗
        //buttons: {
        //    Ok: function () {
        //        $(this).dialog("close");
        //    }
        //},
        position: {
            my: myString,
            at: atString,
            of: positionId,
        },
        resizable: false,
    });
    $(".ui-dialog-titlebar").hide();
    setTimeout(function () { $(Dialog).dialog("close"); }, timeout);
}





