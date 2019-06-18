function getSelect(name) { return document.getElementById('select-' + name); }
function hasClass(element, className) { return (' ' + element.className + ' ').indexOf(' ' + className + ' ') > -1; }
function trimSearchTerm(term, selectContent) {

    let items = selectContent.childNodes;
    for (var i = 0, l = items.length; i < l; i++) {
        if (items[i]
            && items[i].innerHTML) {
            if (items[i].innerHTML.toLowerCase() == term.trim().toLowerCase()) {
                return '';
            }
        }
    }

    return term.trim().toLowerCase().replace(/<br>/g, '').replace(/&nbsp;/g, '');
}

var selectChangeEvent = new Event('theSelectChangeEvent');
function onSelectChange(name, onChange) {
    var s = getSelect(name);
    s.addEventListener('theSelectChangeEvent', function (e) {
        if (name != this.dataset.name) {
            return;
        }
        onChange(this.dataset.value, this.dataset.name);
    });
}

let urlArray = [];
setInterval(function () { urlArray = []; }, 21000);

(function () {
    let loadCount = 5;  // item count which load each time
    let oldSearchTerm = '';
    let isNotChanged = false;
    let allSelects = document.querySelectorAll('div.select');

    allSelects.forEach(function (s) {
        if (s.dataset.value == '' && s.dataset.text == '') {

            if (s.dataset.setFirstItem == 'True') {
                doGet(s.dataset.url + '?take=' + loadCount, function (req) { selectDataFiller(s, req); });
            }

        } else {
            selectDataFiller(s);
        }
    });

    function selectDataFiller(s, req) {
        let json = [];
        if (req != undefined) { json = JSON.parse(req.responseText); }

        if (hasClass(s, 'single-select')) { initSingleSelect(s, json); }
    }

    function toggleSelectContent(selectButton) {
        let theSelect = selectButton.closest('.select');
        if (hasClass(theSelect, 'show')) {
            theSelect.classList.remove('show');
        } else {
            theSelect.classList.add('show');

            let parentValue = '';

            if (theSelect.dataset.parent != ''
                && theSelect.dataset.parent != undefined) {
                let parent = getSelect(theSelect.dataset.parent);

                if (parent.dataset.value != undefined) {
                    parentValue = parent.dataset.value;
                }
            }

            let lastUid = '';
            var itemCount = theSelect.lastElementChild.childElementCount;
            if (theSelect.lastElementChild.childNodes != undefined
                && theSelect.lastElementChild.childNodes[itemCount] != undefined
                && itemCount > 0) {
                lastUid = theSelect.lastElementChild.childNodes[itemCount].getAttribute('value');
            }

            doGet(theSelect.dataset.url + '?take=' + loadCount + '&parent=' + parentValue + '&lastUid=' + lastUid, function (req) {
                let json = JSON.parse(req.responseText);
                updateSelectContent(theSelect, json);
            });
        }
    }

    function initSelectItems(selectContent, item, type) {

        let items = selectContent.childNodes;
        for (var i = 0, l = items.length; i < l; i++) {
            if (items[i].innerHTML == item.text) {
                return null;
            }
        }

        let selectItem = createElement('div');
        selectItem.className = "select-item";
        if (type === 'text') {
            selectItem.innerHTML = item.text;
        } else if (type === 'content') {
            selectItem.classList.add('content-item');
            let img = createElement('img');
            img.className = "select-item-image";
            img.setAttribute('src', item.image);
            let selectItemText = createElement('label');
            selectItemText.className = 'select-item-text';
            selectItemText.innerHTML = item.text;
            selectItem.appendChild(img);
            selectItem.appendChild(selectItemText);
        }
        selectItem.setAttribute('value', item.value);
        selectContent.appendChild(selectItem);
        return selectItem;
    }

    function reFillChildsData(value, parentName) {
        allSelects.forEach(function (s) {
            if (s.dataset.parent != parentName) { return; }

            let url = s.dataset.url + '?take=' + loadCount + '&parent=' + value;
            if (!urlArray.includes(url)) {
                urlArray.push(url);
                doGet(url, function (req) {
                    s.innerHTML = ' ';
                    selectDataFiller(s, req);
                });
            }
        });
    }

    function addSelectContent(selectContent) {
        //visible height + pixel scrolled = total height 
        if (selectContent.offsetHeight + selectContent.scrollTop >= selectContent.scrollHeight) {
            let theSelect = selectContent.closest('.select');
            let parentId = theSelect.dataset.parent;
            let searchTerm = trimSearchTerm(theSelect.querySelector('.select-button-input').innerHTML, selectContent);

            let selectItems = selectContent.querySelectorAll('.select-item');
            let lastLength = selectItems.length;

            let getUrl = theSelect.dataset.url + '?q=' + searchTerm + '&take=' + loadCount;
            if (selectItems.length > 0) {

                let lastUid = selectItems[(lastLength - 1)].getAttribute('value');
                getUrl += '&lastUid=' + lastUid;
            }

            if (parentId) {
                let parent = getSelect(parentId);
                getUrl += '&parent=' + parent.dataset.value;
            }
            
            if (!urlArray.includes(getUrl)) {
                urlArray.push(getUrl);

                doGet(getUrl, function (req) {
                    let json = JSON.parse(req.responseText);
                    let type = theSelect.dataset.type;
                    for (let i = 0; i < json.length; i++) {
                        let selectItem = initSelectItems(selectContent, json[i], type);
                        if (selectItem != null) {
                            selectItem.onclick = function () { onClickSingleSelectItem(this); }
                        }
                    }
                });
            }
        }
    }

    function updateSelectContent(theSelect, updateItem) {
        let selectContent = theSelect.querySelector('.select-content');
        let type = theSelect.dataset.type;
        selectContent.innerHTML = ' ';

        if (updateItem.length == 0 && selectContent.childElementCount == 0) {
            let noItem = createElement('div');
            noItem.style.padding = "5px";
            noItem.style.color = "grey";
            noItem.innerHTML = "no_item";
            selectContent.appendChild(noItem);
        }

        for (let i = 0; i < updateItem.length; i++) {
            let selectItem = initSelectItems(selectContent, updateItem[i], type);
            if (selectItem != null) {
                selectItem.onclick = function () {
                    if (hasClass(theSelect, 'single-select')) { onClickSingleSelectItem(this); }
                }
            }
        }
    }

    function searchSelectContent(selectInput, event) {
        let theSelect = selectInput.closest('.select');
        let selectButton = theSelect.querySelector('.select-button');
        let parentId = theSelect.dataset.parent;
        let searchTerm = trimSearchTerm(selectInput.innerHTML, theSelect.lastElementChild);

        // input empty and press backspace block request
        if (oldSearchTerm.length == 0 && event.keyCode == 8) {
            return;
        }
        // prevent request when pressed up arrow, down arrow, enter,
        if (event.keyCode == 38 ||
            event.keyCode == 40 ||
            event.keyCode == 13 ||
            searchTerm.length == 1) { return; }

        //prevent request when input value is not changed when press enter
        if (oldSearchTerm == selectInput.innerHTML) {
            isNotChanged = true;
            return;
        }
        oldSearchTerm = selectInput.innerHTML;
        isNotChanged = false;

        // delete existing loader
        if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
        // create new loader
        let loader = createElement('div');
        loader.className = 'loader';
        loader.style.right = '-30px';
        selectButton.insertBefore(loader, selectButton.firstChild);

        let url = theSelect.dataset.url + '?q=' + searchTerm + '&take=' + loadCount;
        if (parentId) {
            let parent = getSelect('select-' + parentId);
            if (parent != undefined
                && parent.dataset.value != undefined) {
                url += '&parent=' + parent.dataset.value;
            }
        }
        // send request 
        if (!urlArray.includes(url)) {
            urlArray.push(url);
            doGet(url, function (req) {
                let updateItems = JSON.parse(req.responseText);
                if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
                updateSelectContent(theSelect, updateItems);
            });
        }
    }

    // Single Select Functions
    function initSingleSelect(theSelect, json) {
        initSingleSelectButton(theSelect, json);
        if (theSelect.dataset.detail == "True") {
            initSingleSelectInfo(theSelect, theSelect.dataset.detailUrl);
        }
        initSingleSelectContent(theSelect, json);
    }

    function initSingleSelectButton(theSelect, json) {
        let selectButton = createElement('div');
        selectButton.className = "select-button";
        let uidInput = createElement('input');
        uidInput.className = "select-value";
        uidInput.setAttribute('type', 'hidden');
        uidInput.id = theSelect.dataset.name;
        uidInput.name = theSelect.dataset.name;

        let textInput = createElement('input');
        textInput.setAttribute('type', 'hidden');
        textInput.id = theSelect.dataset.nameText;
        textInput.name = theSelect.dataset.nameText;

        let selectButtonInput = createElement('div');
        selectButtonInput.className = "select-button-input";
        selectButtonInput.setAttribute('contenteditable', 'true');
        let clearSelectButton = createButton('x', clearSelect);
        clearSelectButton.className = "clear-select-button";
        selectButton.appendChild(uidInput);
        selectButton.appendChild(textInput);
        selectButton.appendChild(selectButtonInput);
        selectButton.appendChild(clearSelectButton);
        theSelect.appendChild(selectButton);
        if (json == undefined) {
            selectButton.classList.add('disabled');
            return;
        }

        if (hasClass(selectButton, 'disabled')) {
            selectButton.classList.remove('disabled');
        }

        if (theSelect.dataset.value != ''
            && theSelect.dataset.text != '') {
            selectButtonInput.innerHTML = theSelect.dataset.text;
            uidInput.value = theSelect.dataset.value;
            textInput.value = theSelect.dataset.text;

        } else {
            if (theSelect.dataset.setFirstItem == 'True') {
                if (json.length > 0) {
                    selectButtonInput.innerHTML = json[0].text;
                    uidInput.value = json[0].value;
                    textInput.value = json[0].text;
                    theSelect.dataset.value = json[0].value;
                    theSelect.dataset.text = json[0].text;
                    theSelect.dispatchEvent(selectChangeEvent);

                    reFillChildsData(json[0].value, theSelect.dataset.name);
                }
            }
        }

        selectButton.onclick = function () { toggleSelectContent(this); };
        // show content when input
        selectButtonInput.onkeydown = function (e) {
            let theSelect = this.closest('.select');
            theSelect.classList.add('show');
            oldSearchTerm = this.innerHTML;
            // when click backspace with empty content
            if (e.keyCode == 13) {
                document.execCommand('insertHTML', false, '');
                // prevent the default behaviour of return key pressed
                return false;
            }
        }
        // filter text
        selectButtonInput.onkeyup = function (e) {
            e.preventDefault();
            searchSelectContent(this, e);
        }
    }

    function initSingleSelectInfo(parent, detailUrl) {
        // Create Info Btn
        let infoBtn = createElement('button');
        infoBtn.className = "select-item-info-btn";
        infoBtn.innerHTML = 'i';
        parent.querySelector('.select-button').appendChild(infoBtn);

        // Create Info Popup
        let infoWrapper = createElement('div');
        infoWrapper.className = "select-info-wrapper";
        let closePopover = createElement('button');
        closePopover.className = "close-popover-btn";
        closePopover.innerHTML = "x";
        infoWrapper.appendChild(closePopover);
        closePopover.onclick = function () { infoWrapper.classList.remove('show'); }
        let infoHeader = createElement('div');
        infoHeader.className = "select-info-header";
        let infoImg = createElement('img');
        infoImg.className = "select-info-img";
        let infoTitle = createElement('label');
        infoTitle.className = 'select-info-title';
        infoHeader.appendChild(infoImg);
        infoHeader.appendChild(infoTitle);
        let infoDesc = createElement('div');
        infoDesc.className = 'select-info-desc';
        infoWrapper.appendChild(infoHeader);
        infoWrapper.appendChild(infoDesc);
        parent.appendChild(infoWrapper);

        // Show popover when hover the info button
        infoBtn.onmouseover = function () {
            let text = parent.querySelector('.select-button-input').innerHTML.toLowerCase();
            if (text) {
                infoWrapper.classList.add('show');
                infoImg.setAttribute('src', 'url');
                infoTitle.innerHTML = 'select item text';
            }
        }
        // Show popup when click info button
        infoBtn.onclick = function (e) {
            e.preventDefault();
            e.stopPropagation();
            infoWrapper.classList.remove('show');
            let strHTML = '<div class="select-item-header">';
            strHTML += '<img class="select-item-image" src="' + 'Sample' + '">';
            strHTML += '<label class="select-item-text">' + 'sample text' + '</label>';
            strHTML += '</div>';
            showPopup("Detail", strHTML, true, function () { });
        }
    }

    function initSingleSelectContent(parent, json) {
        let selectContent = createElement('div');
        selectContent.className = "select-content";
        for (let i = 0; i < json.length; i++) {
            let selectItem = initSelectItems(selectContent, json[i], parent.dataset.type);
            if (selectItem != null) {
                selectItem.onclick = function () { onClickSingleSelectItem(this); }
            }
        }
        parent.appendChild(selectContent);

        selectContent.onscroll = function () { addSelectContent(this); }
    }

    function onClickSingleSelectItem(item) {
        let theSelect = item.closest('.select');

        var theValue = item.getAttribute('value');
        var theText = item.innerHTML;

        if (theSelect.dataset.type === 'text') {
            theSelect.querySelector('.select-button-input').innerHTML = theText;
        } else if (theSelect.dataset.type === 'content') {
            theText = item.querySelector('.select-item-text').innerHTML;
            theSelect.querySelector('.select-button-input').innerHTML = theText;
        }

        getElement(theSelect.dataset.name).setAttribute('value', theValue);
        getElement(theSelect.dataset.nameText).setAttribute('value', theText);
        theSelect.dataset.value = theValue;
        theSelect.dataset.text = theText;

        reFillChildsData(theValue, theSelect.dataset.name);

        theSelect.dispatchEvent(selectChangeEvent);
        theSelect.classList.remove('show');
    }

    function clearSelect(e) {
        e.preventDefault();
        e.stopPropagation();
        let theSelect = this.closest('.select');
        let selectButton = theSelect.querySelector('.select-button');
        let selectButtonInput = selectButton.querySelector('.select-button-input');
        selectButtonInput.innerHTML = '';
        // Refills the data
        let parentId;
        let parentName = theSelect.dataset.name;
        allSelects.forEach(function (s) {
            if (s.dataset.name == theSelect.dataset.parent) {
                parentId = s.querySelector(theSelect.dataset.name).getAttribute('value');
            }
            // If parent cleared, clear child select also
            if (s.dataset.parent == parentName) {

                s.querySelector('.select-button').classList.add('disabled');
                s.querySelector('.select-button-input').innerHTML = ' ';
                s.querySelector('.select-content').innerHTML = ' ';

            }
        });

        let url = theSelect.dataset.url + '?q=' + '&take=' + loadCount;
        if (parentId != undefined) {
            url += '&parent=' + parentId;
        }

        if (!urlArray.includes(url)) {
            doGet(url, function (req) {
                let json = JSON.parse(req.responseText);
                updateSelectContent(theSelect, json);
            });
        }
    }

    document.addEventListener('click',
        function (e) {
            let activeSelect = document.querySelector('.select.show');
            if (activeSelect && activeSelect.contains(e.target) == false) {
                activeSelect.classList.remove('show');
            }
        });

    document.addEventListener('keyup',
        function (e) {
            let activeSelect;
            for (let i = 0; i < allSelects.length; i++) {
                if (hasClass(allSelects[i], 'show')) {
                    activeSelect = allSelects[i];
                }
            }
            let activeSelectContent, activeSelectItems;
            let currentSelectItemId = -1;
            if (activeSelect) {
                activeSelectContent = activeSelect.querySelector('.select-content');
                activeSelectItems = activeSelectContent.querySelectorAll('.select-item');
                for (let i = 0; i < activeSelectItems.length; i++) {
                    if (hasClass(activeSelectItems[i], 'hover')) { currentSelectItemId = i; }
                }
                // Up arrow key
                if (e.keyCode == 38) {
                    e.preventDefault();
                    if (currentSelectItemId - 1 >= 0) {
                        activeSelectContent.scrollTop -= activeSelectItems[currentSelectItemId].offsetHeight;
                        activeSelectItems[currentSelectItemId].classList.remove('hover');
                        activeSelectItems[currentSelectItemId - 1].classList.add('hover');
                    }
                }
                // Down arrow key
                if (e.keyCode == 40) {
                    e.preventDefault();
                    if (currentSelectItemId + 1 < activeSelectItems.length) {
                        if (currentSelectItemId == -1) {
                            activeSelectItems[currentSelectItemId + 1].classList.add('hover');
                        } else {
                            activeSelectContent.scrollTop += activeSelectItems[currentSelectItemId].offsetHeight;
                            activeSelectItems[currentSelectItemId].classList.remove('hover');
                            activeSelectItems[currentSelectItemId + 1].classList.add('hover');
                        }
                    }
                }
                // Press enter key 
                if (e.keyCode == 13
                    && activeSelectItems[currentSelectItemId]
                    && hasClass(activeSelectItems[currentSelectItemId], 'active') == false
                    && isNotChanged == false) {
                    if (hasClass(activeSelect, 'single-select')) { onClickSingleSelectItem(activeSelectItems[currentSelectItemId]); }
                }
            }

        },
        false);
})();
