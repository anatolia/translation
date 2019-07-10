function filterLabels() {
    var dropdown = document.getElementById("dropdown");
    var filter, txtSearch;
    txtSearch = document.getElementById("txtSearch");
    filter = txtSearch.value;

    doGet('/Label/SearchData?search=' + filter, function (req) {
        if (199 < req.status && req.status < 300) {
            bindLabelSearchDropdown(req.responseText);
        }
    });

    if (filter == "") {
        hide(dropdown);
    } else {
        show(dropdown);
    }
}

function bindLabelSearchDropdown(responseText) {
    var dropdown = document.getElementById('dropdown');
    while (dropdown.childElementCount > 1) {
        dropdown.removeChild(dropdown.firstChild);
    }
    var labels = JSON.parse(responseText);
    if (labels == null) {
        return;
    }
    for (var i = 0; i < labels.length; i++) {
        var label = labels[i];
        var aTag = document.createElement('a');
        aTag.setAttribute('href', "/Label/Detail/" + label.key);
        aTag.setAttribute('uid', label.uid);
        aTag.innerHTML = label.key;
        dropdown.insertBefore(aTag, dropdown.firstChild);
    }
}

function openLabelSearchListPage() {
    window.location.href = "/Label/SearchList?search=" + getLabelSearchTerm();
}

function openLabelDetailPage(uid) {
    window.location.href = "/Label/Detail/" + uid;
}

function getLabelSearchTerm() {
    return document.getElementById("txtSearch").value;
}

document.onclick = function (e) {
    var item = e.target;
    if (item.tagName !== "dropdown") {
        var dropdown = document.getElementById("dropdown");
        hide(dropdown);
    }
}

function show(element) {
    if (element != undefined) {
        element.classList.remove("hide");
        element.classList.add("show");
    }
}

function hide(element) {
    if (element != undefined) {
        element.classList.remove("show");
        element.classList.add("hide");
    }
}

var Key = {
    UP: 38,
    DOWN: 40,
    ENTER: 13
}

window.addEventListener("keyup", onUpDownKeyPress);
var labelSearchListIndex = -1;

function onUpDownKeyPress(event) {
    var txtSearch = document.getElementById('txtSearch');
    var dropdown = document.getElementById('dropdown');
    txtSearch.blur();
    var itemList = dropdown.getElementsByTagName("a");
    var key = event.which || event.keyCode;

    if (key === Key.DOWN
        && labelSearchListIndex < itemList.length - 2) {
        labelSearchListIndex++;
    } else if (key === Key.UP
        && labelSearchListIndex > 0) {
        labelSearchListIndex--;
    }

    select(itemList, labelSearchListIndex);

    if (key === Key.ENTER) {
        openLabelDetailPage(itemList[labelSearchListIndex].getAttribute("uid"));
    }
}

function select(itemList, itemIndexToSelect) {
    if (itemList.length === 1) {
        return;
    }

    for (i = 0; i < itemList.length; i++) {
        item = itemList[i];
        if (i === itemIndexToSelect) {
            item.classList.add("list-item-selected");
        } else {
            item.classList.remove("list-item-selected");
        }
    }
}