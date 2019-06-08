function getSelect(name) { return document.getElementById('select-' + name); }
function hasClass(element, className) { return (' ' + element.className + ' ').indexOf(' ' + className + ' ') > -1; }
function trimSearchTerm(term) { return term.trim().toLowerCase().replace(/<br>/g, '').replace(/&nbsp;/g, ''); }
function toggleSelectContent(selectButton) {
    let theSelect = selectButton.closest('.select');
    if (hasClass(theSelect, 'show')) { theSelect.classList.remove('show'); }
    else { theSelect.classList.add('show'); }
}

var snappySelectChangeEvent = new Event('snappySelectChange');
function onSelectChange(name, onChange) {
    var s = getSelect(name);
    s.addEventListener('snappySelectChange', function (e) {
        if (name != this.dataset.name) {
            return;
        }
        onChange(this.dataset.value, this.dataset.name);
    });
}

let urlArray = [];
setInterval(function () { urlArray = []; }, 5000);

(function () {
    let loadCount = 5;  // item count which load each time
    let lastValue = 0;
    let oldSearchTerm = '';
    let isNotChanged = false;
    let allSelects = document.querySelectorAll('div.select');

    allSelects.forEach(function (s) {
        doGet(s.dataset.url + '?take=' + loadCount, function (req) { selectDataFiller(req, s); });
    });

    function selectDataFiller(req, s) {
        let json = [];
        let originalJson = JSON.parse(req.responseText);
        for (let i = 0; i < originalJson.length; i++) {
            json.push(originalJson[i]);
        }
        // init select by type
        if (hasClass(s, 'radio-select')) {
            initRadioSelect(s, json);
        } else {
            if (hasClass(s, 'single-select')) {
                initSingleSelect(s, json);
            } else if (hasClass(s, 'multiple-select')) {
                initMultipleSelect(s, json);
            }
        }
    }

    function initSelectItems(parent, item, type) {
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
        parent.appendChild(selectItem);
        return selectItem;
    }

    function reFillChildsData(value, parentName) {
        allSelects.forEach(function (s) {

            if (s.dataset.parent != parentName) {
                return;
            }

            let url = s.dataset.url + '?take=' + loadCount + '&parent=' + value + '&selectId=' + s.id;
            if (!urlArray.includes(url)) {
                urlArray.push(url);
                doGet(url, function (req) {
                    s.innerHTML = ' ';
                    selectDataFiller(req, s);
                });
            }
        });
    }

    function addSelectContent(selectContent) {
        //visible height + pixel scrolled = total height 
        if (selectContent.offsetHeight + selectContent.scrollTop >= selectContent.scrollHeight) {
            let theSelect = selectContent.closest('.select');
            let parentId = theSelect.dataset.parent;
            let searchTerm = trimSearchTerm(theSelect.querySelector('.select-button-input').innerHTML);

            let selectItems = selectContent.querySelectorAll('.select-item');
            lastValue = selectItems.length;

            if (selectItems.length > 0) {
                let lastUid = selectItems[(lastValue - 1)].getAttribute('value');
                let getUrl = theSelect.dataset.url + '?q=' + searchTerm + '&take=' + loadCount + '&lastUid=' + lastUid;
                if (parentId) {
                    let parent = document.querySelector('[data-name=' + parentId + ']');
                    getUrl += '&parent=' + parent.dataset.value;
                }
                if (!urlArray.includes(getUrl)) {
                    urlArray.push(getUrl);
                    doGet(getUrl, function (req) {
                        let json = JSON.parse(req.responseText);
                        let type = theSelect.dataset.type;
                        for (let i = 0; i < json.length; i++) {
                            let selectItem = initSelectItems(selectContent, json[i], type);
                            selectItem.onclick = function () {
                                // If single select
                                if (hasClass(theSelect, 'single-select')) { onClickSingleSelectItem(this); }
                                // If multiple select
                                else { onClickMultipleSelectItem(this); }
                            }
                        }
                    });
                }
            }
        }
    }

    // Single Select Functions
    function initSingleSelect(parent, json) {
        initSingleSelectButton(parent, json);
        if (parent.dataset.detail == "True") {
            initSingleSelectInfo(parent, parent.dataset.detailUrl);
        }
        initSingleSelectContent(parent, json);
    }

    function initSingleSelectButton(theSelect, json) {
        // Select Button
        let selectButton = createElement('div');
        selectButton.className = "select-button";
        let hiddeninput = createElement('input');
        hiddeninput.className = "select-value";
        hiddeninput.setAttribute('type', 'hidden');
        hiddeninput.id = theSelect.dataset.name;
        hiddeninput.name = theSelect.dataset.name;
        let selectButtonInput = createElement('div');
        selectButtonInput.className = "select-button-input";
        selectButtonInput.setAttribute('contenteditable', 'true');
        let clearSelectButton = createButton('x', clearSelect);
        clearSelectButton.className = "clear-select-button";
        selectButton.appendChild(hiddeninput);
        selectButton.appendChild(selectButtonInput);
        selectButton.appendChild(clearSelectButton);
        theSelect.appendChild(selectButton);
        if (json == undefined
            || json.length < 1) {
            selectButton.classList.add('disabled');
            return;
        }
        if (hasClass(selectButton, 'disabled')) {
            selectButton.classList.remove('disabled');
        }
        // If first item true, init child data
        if (theSelect.dataset.setFirstItem == 'True') {
            selectButtonInput.innerHTML = json[0].text;
            hiddeninput.value = json[0].value;
            theSelect.dispatchEvent(snappySelectChangeEvent);

            reFillChildsData(json[0].value, theSelect.dataset.name);
        }

        selectButton.onclick = function () { toggleSelectContent(this); };
        // show content when input
        selectButtonInput.onkeydown = function (e) {
            let parent = this.closest('.select');
            parent.classList.add('show');
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
            let theSelect = this.closest('.select');
            let selectButton = theSelect.querySelector('.select-button');
            let parentId = theSelect.dataset.parent;
            let searchTerm = trimSearchTerm(this.innerHTML);

            // input empty and press backspace block request
            if (oldSearchTerm.length == 0 && e.keyCode == 8) {
                console.log(oldSearchTerm);
                return;
            }
            // prevent request when pressed up arrow and down arrow
            if (e.keyCode == 38 || e.keyCode == 40) { return; }
            //prevent request when input value is not changed when press enter
            if (oldSearchTerm == this.innerHTML) {
                isNotChanged = true;
                return;
            }
            oldSearchTerm = this.innerHTML;
            isNotChanged = false;
            if (e.keyCode == 13) { return; }

            if (searchTerm.length == 1) { return; }

            // delete existing loader
            if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
            // create new loader
            let loader = createElement('div');
            loader.className = 'loader';
            loader.style.right = '-30px';
            selectButton.insertBefore(loader, selectButton.firstChild);

            let url = theSelect.dataset.url + '?q=' + searchTerm + '&take=' + loadCount + '&selectId=' + theSelect.id;
            if (parentId) {
                let parent = document.querySelector('[data-name = ' + parentId + ']');
                url += '&parent=' + parent.dataset.value;
            }
            // send request 
            if (!urlArray.includes(url)) {
                urlArray.push(url);
                doGet(url, function (req) {
                    let updateItems = JSON.parse(req.responseText);
                    if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
                    updateSingleSelectContent(theSelect, updateItems);
                });
            }
        }
        // remove select content when click close button
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
        // Select content
        let selectContent = createElement('div');
        selectContent.className = "select-content";
        for (let i = 0; i < json.length; i++) {
            let selectItem = initSelectItems(selectContent, json[i], parent.dataset.type);
            selectItem.onclick = function () { onClickSingleSelectItem(this); }
        }
        parent.appendChild(selectContent);

        selectContent.onscroll = function () { addSelectContent(this); }
    }

    function updateSingleSelectContent(parent, updateItem) {
        let selectContent = parent.querySelector('.select-content');
        let type = parent.dataset.type;
        selectContent.innerHTML = ' ';
        if (updateItem.length == 0) {
            let noItem = createElement('div');
            noItem.style.padding = "5px";
            noItem.style.color = "grey";
            noItem.innerHTML = "no_item";
            selectContent.appendChild(noItem);
        } else {
            for (let i = 0; i < updateItem.length; i++) {
                let selectItem = initSelectItems(selectContent, updateItem[i], type);
                selectItem.onclick = function () { onClickSingleSelectItem(this); }
            }
        }
    }

    function onClickSingleSelectItem(item) {
        let theSelect = item.closest('.select');
        let type = theSelect.dataset.type;
        if (type === 'text') {
            theSelect.querySelector('.select-button-input').innerHTML = item.innerHTML;
        } else if (type === 'content') {
            let text = item.querySelector('.select-item-text').innerHTML;
            theSelect.querySelector('.select-button-input').innerHTML = text;
        }
        reFillChildsData(item.getAttribute('value'), theSelect.dataset.name);
        // update hidden input value
        theSelect.querySelector('.select-value').setAttribute('value', item.getAttribute('value'));
        // bind custom event to the hidden input
        theSelect.dispatchEvent(snappySelectChangeEvent);
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
                parentId = s.querySelector('.select-value').getAttribute('value');
            }
            // If parent cleared, clear child select also
            if (s.dataset.parent == parentName) {
                if (hasClass(s, 'radio-select')) {
                    s.querySelector('.select-radio-items').innerHTML = ' ';
                } else {
                    s.querySelector('.select-button').classList.add('disabled');
                    s.querySelector('.select-button-input').innerHTML = ' ';
                    s.querySelector('.select-content').innerHTML = ' ';
                }
            }
        });

        let url = theSelect.dataset.url + '?q=' + '&take=' + loadCount + '&selectId=' + theSelect.id + '&parent=' + parentId;
        if (!urlArray.includes(url)) {
            doGet(url, function (req) {
                let json = JSON.parse(req.responseText);
                updateSingleSelectContent(theSelect, json);
            });
        }
    }

    // Multiple Select Functions
    function initMultipleSelect(parent, json) {
        initMultipleSelectButton(parent);
        initMultipleSelectContent(parent, json);
    }

    function initMultipleSelectButton(parent) {
        // Select Button
        let selectButton = createElement('div');
        selectButton.className = "select-button";
        let selectButtonInput = createElement('div');
        selectButtonInput.className = "select-button-input";
        selectButtonInput.setAttribute('contenteditable', 'true');
        selectButton.appendChild(selectButtonInput);
        parent.appendChild(selectButton);

        selectButton.onclick = function () { toggleSelectContent(this); };
        // show content when input
        selectButtonInput.onkeydown = function (e) {
            let parent = this.closest('.select');
            parent.classList.add('show');
            // when click backspace with empty content
            if (e.keyCode == 13) {
                document.execCommand('insertHTML', false, '');
                // prevent the default behaviour of return key pressed
                return false;
            }
            if (!this.innerHTML && e.keyCode == 8) {
                let prev = this.previousSibling;
                if (prev && prev.className == 'active-item') {
                    let itemText = prev.querySelector('span').innerHTML;
                    prev.remove();
                    let type = parent.dataset.type;
                    let selectItems = parent.querySelectorAll('.select-item');
                    for (let i = 0; i < selectItems.length; i++) {
                        if (hasClass(selectItems[i], 'active')) {
                            if (type === 'text' && selectItems[i].innerHTML === itemText) {
                                selectItems[i].classList.remove('active');
                            } else if (type === 'content' &&
                                selectItems[i].querySelector('.select-item-text').innerHTML === itemText) {
                                selectItems[i].classList.remove('active');
                            }
                        }
                    }
                }
            }
        }
        // filter text
        selectButtonInput.onkeyup = function (e) {
            let theSelect = this.closest('.select');
            let selectButton = theSelect.querySelector('.select-button');
            // when click backspace with empty content
            if (e.keyCode != 38 && e.keyCode != 40 && e.keyCode != 13) {
                let filter = this.innerHTML.toLowerCase();
                // delete existing loader
                if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
                // create new loader
                let loader = createElement('div');
                loader.className = 'loader';
                loader.style.right = '-30px';
                selectButton.insertBefore(loader, selectButton.firstChild);
                let url = theSelect.dataset.url + '?q=' + filter + '&take=' + loadCount + '&selectId' + theSelect.id;
                if (theSelect.dataset.parent) {
                    let parent = getSelect(theSelect.dataset.parent);
                    url += '&parent=' + parent.dataset.value;
                }
                if (!urlArray.includes(url)) {
                    urlArray.push(url);
                    doGet(url, function (req) {
                        let json = JSON.parse(req.responseText);
                        if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
                        updateMultipleSelectContent(theSelect, json);
                    });
                }
            }
        }
    }

    function initMultipleSelectContent(parent, json) {
        // Select content
        let type = parent.dataset.type;
        let selectContent = createElement('div');
        selectContent.className = "select-content";
        for (let i = 0; i < json.length; i++) {
            let selectItem = initSelectItems(selectContent, json[i], type);
            selectItem.onmouseover = function () {
                selectItems = selectContent.querySelectorAll('.select-item');
                for (let j = 0; j < selectItems.length; j++) {
                    if (hasClass(selectItems[j], 'hover'))
                        selectItems[j].classList.remove('hover');
                }
                this.classList.add('hover');
            }
            selectItem.onclick = function () { onClickMultipleSelectItem(this); }
        }
        parent.appendChild(selectContent);

        selectContent.onscroll = function () { addSelectContent(selectContent); }
    }

    function updateMultipleSelectContent(parent, updateItem) {
        let type = parent.dataset.type;
        let selectContent = parent.querySelector('.select-content');
        selectContent.innerHTML = ' ';
        if (updateItem.length == 0) {
            let noItem = createElement('div');
            noItem.style.padding = "5px";
            noItem.style.color = "grey";
            noItem.innerHTML = "no_item";
            selectContent.appendChild(noItem);
        } else {
            for (let i = 0; i < updateItem.length; i++) {
                let selectItem = initSelectItems(selectContent, updateItem[i], type);
                selectItem.onclick = function () { onClickMultipleSelectItem(this); }
            }
        }
    }

    function onClickMultipleSelectItem(item) {
        item.classList.add('active');
        let theSelect = item.closest('.select');
        let parent = theSelect.querySelector('.select-button');
        let selectInput = parent.querySelector('.select-button-input');
        let activeItem = createElement('div');
        activeItem.className = "active-item";
        activeItem.setAttribute('draggable', 'true');
        let activeItemText = createElement('span');
        if (theSelect.dataset.type === 'text') {
            activeItemText.innerHTML = item.innerHTML;
        } else if (theSelect.dataset.type === 'content') {
            activeItemText.innerHTML = item.querySelector('.select-item-text').innerHTML;
        }
        activeItem.id = parent.closest('.select').id + '-' + activeItemText.innerHTML;

        let removeItem = createButton('x', removeMultipleItem);
        removeItem.className = "remove-active-item";
        activeItem.appendChild(activeItemText);
        activeItem.appendChild(removeItem);
        parent.insertBefore(activeItem, selectInput);
    }

    function removeMultipleItem(e) {
        e.preventDefault();
        e.stopPropagation();
        let theSelect = this.closest('.select');
        let activeItem = this.closest('.active-item');
        let itemText = activeItem.querySelector('span').innerHTML;
        activeItem.remove();
        let selectItems = theSelect.querySelectorAll('.select-item');
        for (let i = 0; i < selectItems.length; i++) {
            if (hasClass(selectItems[i], 'active')) {
                if (theSelect.dataset.type === 'text'
                    && selectItems[i].innerHTML === itemText) { selectItems[i].classList.remove('active'); }
                else if (theSelect.dataset.type === 'content'
                         && selectItems[i].querySelector('.select-item-text').innerHTML === itemText) { selectItems[i].classList.remove('active'); }
            }
        }
    }

    // Radio Select Functions
    function initRadioSelect(parent, json) {
        let type = parent.dataset.type;
        let hiddeninput = createElement('input');
        hiddeninput.className = "select-value";
        hiddeninput.setAttribute('type', 'hidden');
        hiddeninput.id = parent.dataset.name;
        hiddeninput.name = parent.dataset.name;
        parent.appendChild(hiddeninput);
        let selectItemWrapper = createElement('div');
        selectItemWrapper.className = "select-radio-items";
        for (let i = 0; i < json.length; i++) {
            let selectItem = createElement('button');
            selectItem.setAttribute('type', 'button');
            selectItem.setAttribute('value', json[i].value);
            selectItem.className = 'select-radio-item-btn';
            let selectItemContent = createElement('div');
            selectItemContent.className = 'select-radio-item-content';
            // show images if data-type is content
            if (type == 'content') {
                let selectItemImg = createElement('img');
                selectItemImg.className = 'select-radio-item-img';
                selectItemImg.setAttribute('src', json[i].image);
                selectItemContent.appendChild(selectItemImg);
            }
            // show only texts
            let selectItemText = createElement('label');
            selectItemText.innerHTML = json[i].text;
            selectItemContent.appendChild(selectItemText);
            selectItem.appendChild(selectItemContent);
            selectItemWrapper.appendChild(selectItem);
            // onclick radio select item
            selectItem.onclick = function () {
                onClickRadioSelectItem(this);
            }
        }
        // Init Info
        if (parent.dataset.detail == "True") {
            initRadioSelectInfo(parent, parent.dataset.detailUrl);
        }
        parent.appendChild(selectItemWrapper);
        // Set First Item
        let selectItems = parent.querySelectorAll('.select-radio-item-btn');
        let firstItem = parent.dataset.setFirstItem;
        if (firstItem == "True") {
            onClickRadioSelectItem(selectItems[0]);
        }
    }

    function initRadioSelectInfo(theSelect, detailUrl) {
        let infoBtn = createElement('button');
        infoBtn.className = "select-item-info-btn";
        infoBtn.innerHTML = 'i';
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
        theSelect.appendChild(infoBtn);
        theSelect.appendChild(infoWrapper);
        // Show popover when hover the info button
        infoBtn.onmouseover = function () {
            let activeItem = theSelect.querySelector('.select-radio-item-btn.active');
            if (activeItem) {
                infoWrapper.classList.add('show');
                infoImg.setAttribute('src', 'static');
                infoTitle.innerHTML = 'dummy text';
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

    function onClickRadioSelectItem(elmnt) {
        let theSelect = elmnt.closest('.radio-select');
        let parentName = theSelect.dataset.name;
        let isChildActive = false;
        // check child select item is not selected
        allSelects.forEach(function (s) {
            if (s.dataset.parent == parentName) {
                let activeItem = s.querySelectorAll('.select-radio-item-btn.active');
                if (activeItem.length > 0) {
                    isChildActive = true;
                    return;
                }
            }
        });
        if (isChildActive == true) {
            showPopup('are_sure_to_change_selection', 'you_may_loose_related_values', true, function () {
                hidePopup();
                activeRadioSelectItem(elmnt);
            });
        } else {
            activeRadioSelectItem(elmnt);
        }
    }

    function activeRadioSelectItem(elmnt) {
        let theSelect = elmnt.closest('.radio-select');
        if (hasClass(theSelect, 'single-select')) {
            let activeItem = theSelect.querySelector('.select-radio-item-btn.active');
            if (activeItem) { activeItem.classList.remove('active'); }
            elmnt.classList.add('active');
        } else if (hasClass(theSelect, 'multiple-select')) {
            elmnt.classList.toggle('active');
        }
        theSelect.querySelector('.select-value').setAttribute('value', elmnt.getAttribute('value'));
        reFillChildsData(elmnt.getAttribute('value'), theSelect.dataset.name);
    }

    document.addEventListener('click',
        function (e) {
            let activeSelect = document.querySelector('.select.show');
            if (activeSelect && activeSelect.contains(e.target) == false) {
                activeSelect.classList.remove('show');
            }
            // Detail Popover
            let activePopover = document.querySelector('.select-info-wrapper.show');
            if (activePopover && activePopover.contains(e.target) == false) {
                activePopover.classList.remove('show');
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
                    else if (hasClass(activeSelect, 'multiple-select')) { onClickMultipleSelectItem(activeSelectItems[currentSelectItemId]); }
                }
            }

        },
        false);
})();
