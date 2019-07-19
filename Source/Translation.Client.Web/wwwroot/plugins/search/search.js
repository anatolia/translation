let _index = -1;
let lastFilter = "";

function searchWork() {
    let searchResults = document.getElementById("searchResults");
    let txtSearch = document.getElementById("txtSearch");
    txtSearch.addEventListener("keydown", onUpDownKeyPress);
    let filter = txtSearch.value;

    if (filter !== lastFilter) {
        _index = -1;

        doGet('/Label/SearchData?search=' + filter, function (req) {
            if (199 < req.status && req.status < 300) {
                bindLabelSearchDropdown(req.responseText);
            }
        });
    }
    lastFilter = filter;

    if (filter === "") {
        hide(searchResults);
        txtSearch.removeEventListener("keydown", onUpDownKeyPress);
    } else {
        show(searchResults);
    }
}

function bindLabelSearchDropdown(responseText) {
    let searchResults = document.getElementById('searchResults');
    while (searchResults.childElementCount > 1) {
        searchResults.removeChild(searchResults.firstChild);
    }
    let results = JSON.parse(responseText);
    if (results == null) {
        return;
    }
    for (let i = 0; i < results.length; i++) {
        let result = results[i];
        let link = document.createElement('a');
        link.setAttribute('uid', result.uid);
        link.innerHTML = result.key;
        searchResults.insertBefore(link, searchResults.firstChild);
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
    let item = e.target;
    if (item.id !== "searchResults"
        || item.id !== "txtSearch") {
        let searchResults = document.getElementById("searchResults");
        hide(searchResults);
    }
}

function show(element) {
    if (element != undefined) {
        element.classList.add("show");
    }
}

function hide(element) {
    if (element != undefined) {
        element.classList.remove("show");
    }
}

let KEY = {
    UP: 38,
    DOWN: 40,
    ENTER: 13
}

function onUpDownKeyPress(event) {
    let searchResults = document.getElementById('searchResults');
    let results = searchResults.getElementsByTagName("a");
    let key = event.which || event.keyCode;

    if (key === KEY.DOWN
        && _index < results.length - 1) {
        event.preventDefault();
        _index++;
    } else if (key === KEY.UP
               && _index > 0) {
        event.preventDefault();
        _index--;
    }

    markSelected(results, _index);

    if (key === KEY.ENTER) {
        if (_index === results.length - 1) {
            openLabelSearchListPage();
        } else {
            openLabelDetailPage(results[_index].getAttribute("uid"));
        }
    }
}

function markSelected(results, resultIndexToSelect) {
    if (results.length === 1) {
        return;
    }

    for (let i = 0; i < results.length; i++) {
        let result = results[i];
        if (i === resultIndexToSelect) {
            result.classList.add("list-item-selected");
        } else {
            result.classList.remove("list-item-selected");
        }
    }
}