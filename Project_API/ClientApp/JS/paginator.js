﻿var adsList;
//show page next previoue controls
function paginator(pageNo) {
    let pageNav = document.getElementById('pageNav');
    pageNav.innerHTML = "";
    //console.log("pageNo: "+pageNo);
    let rpp = 5;
    let total_rows = adsList.length;
    //console.log();
    let last = parseInt(Math.ceil(total_rows / rpp));
    
    if (last < 1) {
        last = 1;
    }
    //console.log("last:"+last);
    let paginationControls = "";
    if (last > 1) {
        if (pageNo > 1) {
            let p = parseInt(pageNo);
            p = (p - 1);
            paginationControls += "<button onclick='request_page(" + p + ")'>&lt;</button>";
        }
        paginationControls += "&nbsp; &nbsp; <b>Page " + pageNo + " of " + last + "</b> &nbsp; &nbsp; ";
        if (pageNo != last) {
            let p = parseInt(pageNo);
            p = (p + 1);
            paginationControls += "<button onclick='request_page(" + p + ")'>&gt;</button>";
        }
        pageNav.innerHTML = paginationControls;
        paginationControls = "";
    }
}

function request_page(pageNo) {
    paginator(pageNo);
    //console.log(pageNo);
    AdsDiv = document.getElementById('AdsDiv');
    //console.log(adsList.length);
    start = (pageNo - 1) * 5;
    //console.log("start=>"+start);
    AdsDiv.innerHTML = "";
    //alert(arrayList);
    for (let i = start; i < pageNo * 5; i++) {
        AdsDiv.innerHTML +=
            "<tr><td>" + adsList[i].Title + "</td><td>" + adsList[i].Edition + "</td>" +
                "<td>" + adsList[i].Language + "</td>" +
                "<td>" + adsList[i].Price + "</td>" +
                "<td width='1'><img src='" + adsList[i].Image + "' width='100' height='150' alt='Image not available' /></td>" +
                "<td><a href='/Home/Details/" + adsList[i].Id + "'>Details</a> | <button onclick='addToCart(this.value, this)' id='Cart' value=" + adsList[i].Id + ">Add to Cart</button></td></tr>";
    }
}


function addToCart(bookId, cart)
{

    var userId = document.getElementById("User_Id");
    if (userId.value == "") {
        if (confirm("Login to add to cart. Continue to login page?"))
            window.location.href = "/Account/Login";
        else
            return false;
    }
    else {
        addCart(userId, bookId);
    }

}

function addCart(userId, bookId){

    //var Book_Id = bookId;

    let cart = {
        User_Id: parseInt(userId.value),
        Book_Id: parseInt(bookId),
        QuantityOrdered: 1
    };

    $.ajax({
        url: "http://localhost:56991/api/Carts",
        method: "POST",
        dataType: "json",
        crossDomain: true,
        data: cart,
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }
    });
}


function loadAllBooks(str, type) {
    console.log("im in");
    //let title = document.getElementById('title');
    var searchStr = $("#SearchStr").val(),
        orderBy = $("#Order-By").val(),
        language = $("#Language").val(),
        category = $("#Category").val(),
        author = $("#Author").val(),
        publisher = $("#Publisher").val();
    console.log(orderBy);
    let model = {
            searchStr: searchStr,
            orderBy: orderBy,
            category: category,
            author: author,
            language: language,
            publisher: publisher
    };

    $.ajax({
        url: "http://localhost:56991/api/Books/Search",
        type: "POST",
        headers: {
            "Authorization": "Basic " + btoa("admin:123")
        },
        data: model,
        dataType: "json",
        success: function (data) {

            adsList = data;
            //console.log(data);
            request_page('1');
        },
        error: function (err) {
            console.log(err);
        }
    });
    
}

function loadAuthor()
{
    $.ajax({
        url: "http://localhost:56991/api/Authors",
        method: "GET",
        headers: {
            "Authorization": "Basic " + btoa("admin:123")
        },
        dataType: "json",
        success: function (data) {
            let area = document.getElementById('Author');
            for (let index = 0; index < data.length; index++) {
                let option = document.createElement("option");
                option.text = data[index].Name;
                area.add(option);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

function loadPublishers() {
    $.ajax({
        url: "http://localhost:56991/api/Publishers",
        method: "GET",
        headers: {
            "Authorization": "Basic " + btoa("admin:123")
        },
        dataType: "json",
        success: function (data) {
            let area = document.getElementById('Publisher');
            for (let index = 0; index < data.length; index++) {
                let option = document.createElement("option");
                option.text = data[index].Name;
                area.add(option);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

function loadCategories() {
    $.ajax({
        url: "http://localhost:56991/api/Categories",
        method: "GET",
        headers: {
            "Authorization": "Basic " + btoa("admin:123")
        },
        dataType: "json",
        success: function (data) {
            let area = document.getElementById('Category');
            for (let index = 0; index < data.length; index++) {
                let option = document.createElement("option");
                option.text = data[index].Name;
                area.add(option);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}

function loadSearch() {
    var searchStr = $("#SearchStr").val(),
        orderBy = $("#Order-By").val(),
        language = $("#Language").val(),
        category = $("#Category").val(),
        author = $("#Author").val(),
        publisher = $("#Publisher").val();
    var model = {
        searchStr: searchStr,
        orderBy: orderBy,
        category: category,
        author: author,
        language: language,
        publisher: publisher
    };

    $.ajax({
        url: "http://localhost:56991/api/Books/Search",
        method: "POST",
        headers: {
            "Authorization": "Basic " + btoa("admin:123")
        },
        data: model,
        dataType: "json",
        success: function (data) {
            let area = document.getElementById('Author');

            for (let index = 0; index < data.length; index++) {

                let option = document.createElement("option");
                option.text = data[index].Name;


                area.add(option);
            }
        },
        error: function (err) {
            console.log(err);
        }

    });
}