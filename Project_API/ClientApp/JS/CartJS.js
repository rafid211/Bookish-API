$(document).ready(function () {
    
    loadGrid();
});



function loadGrid() {
    var adsList;
    let cartData = null;
    $.ajax({
        url: "http://localhost:56991/api/Carts/Users/2/Books",
        method: "GET",
        headers: {
            "Authorization": "Basic " + btoa("admin:123")
        },
        dataType: "json",
        success: function (data) {
            adsList = data;
            var AdsDiv = document.getElementById('AdsDiv');
            AdsDiv.innerHTML = "";
            for (var i = 0; i < data.length; i++) {
                AdsDiv.innerHTML +=
                    "<tr><td>" + adsList[i].Title + "</td><td>" + adsList[i].Edition + "</td>" +
                        "<td>" + adsList[i].Language + "</td>" +
                        "<td>" + adsList[i].Price + "</td>" +
                        "<td width='1'><img src='" + adsList[i].Image + "' width='100' height='150' alt='Image not available' /></td>" +
                        "<td><button onclick='addToCart(this.value, this)' id='Cart' value=" + adsList[i].Id + ">Remove from cart</button></td></tr>";

            }

        },
        error: function (err) {
            reportRes.innerHTML = "Something Went wrong!!! Please try again";
        }

    });
    getCart()
}





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

function checkCoupon() {
    var couponKey = document.getElementById("CouponKey");
    var couponValidation = document.getElementById("CouponValidation");
    var couponPercentage = document.getElementById("CouponPercentage");

    $.ajax({
        url: "/Cart/CheckCoupon",
        method: "GET",
        data: {
            couponKey: couponKey.value
        },
        success: function (data) {
            if (data == "false") {
                couponValidation.style.color = "red";
                couponKey.value = "";
                couponValidation.innerHTML = "Coupon key in invalid!";
            } else if (data == "expired") {
                couponKey.value = "";
                couponValidation.style.color = "red";
                couponValidation.innerHTML = "Coupon date has expaired!"
            } else {
                var data = JSON.parse(data);
                couponValidation.style.color = "green";
                couponPercentage.value = data.Percentage;
                couponValidation.innerHTML = "Congratulations! "+ data.Percentage +"% discount applied.";
                getCart();
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

function setQuantity(qty, type) {
    var counter = type;
    userId = document.getElementById("User-Id");
    var obj = {
        QuantityOrdered: qty,
        Book_Id: counter.id,
        User_Id: userId.value
    };

    $.ajax({
        url: "/Cart/SetQuantity",
        method: "POST",
        data: {
            obj: obj
        },
        success: function (data) {
            console.log(data);
            getCart();
        },
        error: function (err) {
            console.log(err);
        }

    });
}

function deleteCart(bookId, cart) {
    bId = parseInt(bookId);
    if (confirm("Remove from cart?"))
        window.location.href = "/Cart/CartRemove/"+bId+"";
    else
        return false;

    //var userId = document.getElementById("User_Id");
    //if (userId.value == "") {
    //    if (confirm("Login to add to cart. Continue to login page?"))
    //        window.location.href = "/Account/Login";
    //    else
    //        return false;
    //}
    //else {
    //    addCart(userId, bookId);
    //}

}