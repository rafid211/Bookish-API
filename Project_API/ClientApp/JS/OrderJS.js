$(document).ready(function () {
    getCart();

    $.ajax({
        url: "/Json/dhaka.json",
        method: "GET",
        success: function (data) {
            let area = document.getElementById('Area');

            for (let index = 0; index < data.length; index++) {

                let option = document.createElement("option");
                option.text = data[index].area;


                area.add(option);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });

    //On load set name and phone number
    var name = document.getElementById("Name");
    var phoneNumber = document.getElementById("PhoneNumber");
    //$.ajax({
    //    url: "http://localhost:56991/api/User/",
    //    method: "GET",
    //    headers: {
    //        "Authorization": "Basic " + btoa("admin:123")
    //    },
    //    dataType: "json",
    //    success: function (data) {
    //        adsList = data;
    //        var AdsDiv = document.getElementById('AdsDiv');
    //        AdsDiv.innerHTML = "";
    //        for (var i = 0; i < data.length; i++) {
    //            AdsDiv.innerHTML +=
    //                "<tr><td>" + adsList[i].Title + "</td><td>" + adsList[i].Edition + "</td>" +
    //                    "<td>" + adsList[i].Language + "</td>" +
    //                    "<td>" + adsList[i].Price + "</td>" +
    //                    "<td width='1'><img src='" + adsList[i].Image + "' width='100' height='150' alt='Image not available' /></td>" +
    //                    "<td><button onclick='addToCart(this.value, this)' id='Cart' value=" + adsList[i].Id + ">Remove from cart</button></td></tr>";

    //        }

    //    },
    //    error: function (err) {
    //        reportRes.innerHTML = "Something Went wrong!!! Please try again";
    //    }

    //});


});

function getCart() {
    $.ajax({
        url: "http://localhost:56991/api/Carts",
        method: "GET",
        headers: {
            "Authorization": "Basic " + btoa("admin:123")
        },
        dataType: "json",
        success: function (data) {
            //var data = JSON.parse(data);
            //calculation(data);
            var btn = document.getElementById("btnSubmit");
            if (data.length == 0) {
                btn.disabled = true;
            } else {
                btn.disabled = false;
                calculation(data);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

function calculation(d) {
    var subtotal = document.getElementById("Subtotal");
    var couponKey = document.getElementById("CouponKey");
    var total = document.getElementById("Total");
    var couponPercentage = document.getElementById("CouponPercentage");
    var payableTotal = document.getElementById("Payable-Total");
    var shippingPrice = 40;

    let sub = 0;
    for (var i = 0; i < d.length; i++) {
        sub += d[i].QuantityOrdered * d[i].Price;
    }
    subtotal.innerHTML = sub;
    total.value = sub + shippingPrice;
    total.innerHTML = sub + shippingPrice;

    payableTotal.value = sub + shippingPrice;
    payableTotal.innerHTML = sub + shippingPrice;

    if (couponPercentage.value != "") {
        payableTotal.value -= total.value * (couponPercentage.value / 100);
        payableTotal.innerHTML = Math.ceil(payableTotal.value);
    }
}

function addOrder(type) {
    if (!validation()) {
        return;
    }

    var userId = document.getElementById("User_Id");
    var name = document.getElementById("Name");
    var phoneNumber = document.getElementById("PhoneNumber");
    var area = document.getElementById("Area");
    var address = document.getElementById("Address");
    var addedDate = document.getElementById("AddedDate");

    var order = {
        User_Id: userId.value,
        Name: name.value,
        PhoneNumber: phoneNumber.value,
        Area: area.value,
        Address: address.value,
        AddedDate: addedDate.value,
        Status: "Pending"
    };

    $.ajax({
        url: "/Cart/AddOrder",
        method: "POST",
        data: order,
        success: function (data) {
            var data = JSON.parse(data);
            calculation(data);

        },
        error: function (err) {
            console.log(err);
        }

    });
}


function validation() {
    var name = document.getElementById("Name").value;
    var phoneNumber = document.getElementById("PhoneNumber").value;
    var area = document.getElementById("Area").value;
    var address = document.getElementById("Address").value;

    if (name!="" && phoneNumber!="" && area!="" && address!="") {
        return true;
    }
    else {
        return false;
    }
}