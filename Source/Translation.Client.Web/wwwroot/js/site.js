function getPathFromUrl(url) { return url.split("?")[0]; }
function clearChildren(element) { while (element.firstChild) { element.removeChild(element.firstChild); } }
function createElement(tag) { return document.createElement(tag); }
function getElement(id) { return document.getElementById(id); }
function getValue(id) { return document.getElementById(id).value; }
function getAntiForgeryTokenFromLayout() { return document.getElementsByName('__RequestVerificationToken')[0].value; }

function createLink(text, href) {
    let a = createElement('a');
    a.innerText = text;
    a.href = href;
    return a;
}

function createButton(text, onClick) {
    let btn = createElement('button');
    btn.innerText = text;
    btn.onclick = onClick;
    return btn;
}

function hideElement(item) {
    item.style.opacity = 0;
    setTimeout(function () { item.style.display = 'none'; }, 377);
}

function showElement(item, opacity) {
    item.style.opacity = opacity;
    setTimeout(function () { item.style.display = 'block'; }, 89);
}

function doGet(url, onSuccess) {
    let req = new XMLHttpRequest();
    req.open('GET', url, true);
    req.onreadystatechange = function () {
        if (req.readyState === XMLHttpRequest.DONE) {
            if (199 < req.status && req.status < 300) {
                onSuccess(req);
            } else {
                if (req.status != 404) {
                    console.log('error', req.status.toString(), req);
                }
            }
        }
    }
    req.send(null);
}

function doPostWithJsonContent(url, data, onSuccess, onError) {
    doPostRequest(url, 'application/json', data, onSuccess, onError);
}

function doPostWithFormUrlEncodedContent(url, data, onSuccess, onError) {
    doPostRequest(url, 'application/x-www-form-urlencoded', data, onSuccess, onError);
}

function doPostRequest(url, contentType, data, onSuccess, onError) {
    let req = new XMLHttpRequest();
    req.open('POST', url, true);
    req.setRequestHeader('Content-type', contentType);
    req.setRequestHeader('RequestVerificationToken', getAntiForgeryTokenFromLayout());
    req.onreadystatechange = function () {
        if (req.readyState === XMLHttpRequest.DONE) {
            if (199 < req.status && req.status < 300) {
                onSuccess(req);
            } else {
                onError(req);
            }
        }
    };
    req.send(data);
}

function doIfConfirmed(btn, onConfirm) {
    showPopup(btn.dataset.confirmTitle, btn.dataset.confirmContent, true, onConfirm);
}

function doRedirectIfConfirmedSuccess(btn, redirectUrl) {
    showPopup(btn.dataset.confirmTitle, btn.dataset.confirmContent, true, function () {
        doPostWithFormUrlEncodedContent(btn.dataset.url, btn.dataset.prm,
            function (req) {
                let response = JSON.parse(req.response);
                if (response.isOk === true) {
                    window.location.href = redirectUrl;
                } else {
                    let messages = response.messages.join('<br/>');
                    showPopupMessage(messages);
                }
            },
            function (req) {
                let messages = JSON.parse(req.response).messages.join('<br/>');
                showPopupMessage(messages);
            });
    });
}


function translateScreen() {
    var items = document.querySelectorAll('[data-translation]');
    var labels = JSON.parse(localStorage.getItem('labels'));
    var userLanguage = localStorage.getItem('userLanguage');
    if (userLanguage === null) {
        userLanguage = 'en';
    }

    items.forEach(function (item) {
        for (var i = 0; i < labels.length; i++) {
            var label = labels[i];
            if (label.key === item.dataset.translation) {

                label.translations.forEach(function (translation) {
                    if (translation.languageIsoCode2 === userLanguage) {
                        item.innerHTML = translation.translation;
                        return;
                    }
                });

                break;
            }
        }
    });
}

if (localStorage.getItem('labels') == undefined) {
    doGet('/Data/GetMainLabels', function (req) {
        if (199 < req.status && req.status < 300) {
            localStorage.setItem('labels', req.responseText);
            translateScreen();
        }
    });
} else {
    translateScreen();
}

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
        aTag.innerHTML = label.key;
        dropdown.insertBefore(aTag, dropdown.firstChild);
    }
}

function openLabelSearchListPage() {
    window.location.href = "/Label/SearchList?search=" + getLabelSearchTerm();
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
