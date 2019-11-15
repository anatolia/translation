function getPathFromUrl(url) { return url.split("?")[0]; }
function clearChildren(element) { while (element.firstChild) { element.removeChild(element.firstChild); } }

function createDiv(attributes, text) {
    return createElement('div', attributes, text);
}

function createButton(attributes, text) {
    return createElement('button', attributes, text);
}

function createElement(tag) { return document.createElement(tag); }
function createElement(tag, attributes, text) {
    let $element = document.createElement(tag);
    if (attributes) {
        for (let key of Object.keys(attributes)) {
            $element.setAttribute(key, attributes[key]);
        }
    }
    if (text !== undefined && text !== null) {
        $element.innerHTML = text;
    }
    return $element;
}

function getParent(element, parentClass) {
    let targetParent = false;
    try {
        let parent = element.parentNode;
        while (parent) {
            if (parent.classList.contains(parentClass)) {
                targetParent = parent;
                break;
            }
            parent = parent.parentNode;
        }
    } catch (error) {

    }

    return targetParent;
}

 function guid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function getElement(id) { return document.getElementById(id); }
function getValue(id) { return document.getElementById(id).value; }
function getAntiForgeryTokenFromLayout() { return document.getElementsByName('__RequestVerificationToken')[0].value; }

function getLowerCase(text) {
    return text.toLowerCase().replace(/ğ/gim, "g").replace(/ü/gim, "u").replace(/ş/gim, "s").replace(/ö/gim, "o").replace(/ç/gim, "c").replace(/ /gim, "_").replace(/-/gim, "_");
}

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

function openTab(evt, tabName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tab-content");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace("active", "");
    }
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
}

function SecretField(element, order) {
    this.el = element;
    this.order = order;
    this.isVisible = false;
    this.text = this.el.innerText;
    this.uniqueId = this.el.parentElement.getAttribute('data-uid') + this.order;
    this.el.setAttribute('data-id', this.uniqueId);

    this.getStars = function (countOfStars) {
        return "*".repeat(countOfStars);
    }

    this.getButton = function (type) {
        return "<span class='secretButton' id='" + this.uniqueId + "'>" + type + "</span>";
    }

    this.getSecretSpan = function () {
        return "<span class='secretSpan'>" + this.getStars(5) + "</span>";
    }

    this.enableButtons = function () {
        var button = document.querySelector('.secretButton[id="' + this.uniqueId + '"]');

        button.addEventListener('click', () => {
            if (this.isVisible) {
                this.hide();
            } else {
                this.show();
            }
        });
    }

    this.hide = function () {
        this.el.innerHTML = this.getSecretSpan();
        this.el.innerHTML += this.getButton('show');
        this.isVisible = false;
        this.enableButtons();
    }

    this.show = function () {
        this.el.innerHTML = this.text;
        this.el.innerHTML += this.getButton('hide');
        this.isVisible = true;
        this.enableButtons();
    }
}
