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
    let loadCount = 5;
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
        initSingleSelect(s, json);
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
                                onClickSingleSelectItem(this);
                            }
                        }
                    });
                }
            }
        }
    }

    function initSingleSelect(parent, json) {
        initSingleSelectButton(parent, json);
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

            if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
            return false;
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
                s.querySelector('.select-button').classList.add('disabled');
                s.querySelector('.select-button-input').innerHTML = ' ';
                s.querySelector('.select-content').innerHTML = ' ';
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
                    onClickSingleSelectItem(activeSelectItems[currentSelectItemId]);
                }
            }

        },
        false);
})();
