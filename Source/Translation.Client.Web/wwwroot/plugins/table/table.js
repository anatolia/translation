let tables = document.querySelectorAll('table');
tables.forEach(function (t) {
    if (t.dataset.result != undefined) {
        fillTable(t.dataset.result, t);
    }
});

function fillTable(dataPath, table) {
    doGet(dataPath, function (res) {
        if (res == undefined
            || res.responseText == undefined
            || res.responseText.length < 1
            || res.status > 299
            || res.status < 199) {
            return;
        }

        let dataResult = JSON.parse(res.responseText);

        appendHeader(dataResult.headers, table);

        if (dataResult.data.length > 0) {

            appendRows(dataResult.data, table, dataPath);

            if (dataResult.data.length < dataResult.pagingInfo.totalItemCount) {
                appendPaginationPanel(dataResult.pagingInfo, table);
            }
        }
        translateElement(table);
    });
}

function appendHeader(headers, table) {
    if (table.firstChild) {
        if (table.firstChild.tagName == 'THEAD') {
            return;
        }
    }

    let tr = createElement('tr');

    if (table.dataset.selectable === "true") {

        let chk = createElement('input');
        chk.type = 'checkbox';
        chk.onchange = function (c) {
            var rows = table.lastChild.childNodes;
            rows.forEach(function (r) {
                r.firstChild.firstChild.checked = chk.checked;
            });
        }

        let th = createElement('th');
        th.className = "selectable";
        th.appendChild(chk);
        tr.appendChild(th);
    }

    headers.forEach(function (head) {
        let th = createElement('th');
        th.innerHTML = head.key;
        th.dataset.translation = head.key;
        tr.appendChild(th);
    });

    let thead = createElement('thead');
    thead.appendChild(tr);

    table.appendChild(thead);
}

function prepareRow(line, table) {
    let cols = line.split(',_,');
    let row = document.createElement('tr');

    row.dataset.uid = cols[0];

    if (table.dataset.selectable === "true") {

        let chk = createElement('input');
        chk.type = 'checkbox';

        let td = createElement('td');
        td.className = "selectable";

        td.appendChild(chk);
        row.appendChild(td);
    }

    for (let j = 1; j < (cols.length - 1); j++) {
        let col = cols[j];

        let column = document.createElement('td');
        let chk = createElement('input');
        chk.type = 'checkbox';
        chk.disabled = "disabled";
        if (col.toLowerCase() === 'true') {
            chk.checked = "checked";
            column.appendChild(chk);
        }
        else if (col.toLowerCase() === 'false') {
            column.appendChild(chk);
        }
        else {
            column.innerHTML = col;
        }
        row.appendChild(column);
    }

    return row;
}

function appendRows(items, table) {

    if (table.firstChild.nextElementSibling) {
        if (table.firstChild.nextElementSibling.tagName == 'TBODY') {
            table.firstChild.nextElementSibling.remove();
        }
    }

    let tbody = createElement('tbody');

    for (let i = 0; i < items.length; i++) {
        let row = prepareRow(items[i], table);
        tbody.appendChild(row);
    }

    table.appendChild(tbody);
}

function appendPaginationPanel(pagingInfo, table) {

    if (table.parentElement.lastElementChild.classList.contains('pagination')) {
        table.parentElement.lastElementChild.remove();
    }

    let parentDiv = createElement('div');
    parentDiv.classList = 'pagination';

    let dataPath = table.dataset.result;

    let prmLastUid = '&lastUid=' + pagingInfo.lastUid;
    let prmTake = '&take=' + pagingInfo.take;
    let skipToLast = pagingInfo.totalItemCount - (pagingInfo.totalItemCount % pagingInfo.take);
    if (skipToLast === pagingInfo.totalItemCount) {
        skipToLast = ((pagingInfo.totalItemCount / pagingInfo.take) - 1) * pagingInfo.take;
    }
    let pageCount = (skipToLast / pagingInfo.take) + 1;

    if (pagingInfo.isHavingPrevious) {

        appendGotoFirst(parentDiv, function () { updateTableRows(dataPath, table); });
        appendGotoPrevious(parentDiv, function () {
            updateTableRows(dataPath + '?skip=' + (pagingInfo.skip - pagingInfo.take) + prmTake + prmLastUid, table);
        });
    }

    if (pagingInfo.type === 'page_numbers') {
        appendCurrentPage(parentDiv, pagingInfo.currentPage);
    }

    if (pagingInfo.isHavingNext) {
        appendGotoNext(parentDiv, function () {
            updateTableRows(dataPath + '?skip=' + (pagingInfo.skip + pagingInfo.take) + prmTake + prmLastUid, table);
        });
    }

    if (pagingInfo.type === 'page_numbers' && pagingInfo.isHavingNext) {
        appendGotoLast(parentDiv, function () {
            updateTableRows(dataPath + '?skip=' + skipToLast + prmTake, table);
        });
    }

    appendGotoPage(parentDiv, function () {

        var pageInput = parentDiv.getElementsByTagName('input')[0];

        let page = pageInput.value;
        if (page < 1) {
            page = 1;
        }

        let skip = (page - 1) * pagingInfo.take;
        if (skipToLast <= skip) {
            skip = skipToLast;
        }

        updateTableRows(dataPath + '?skip=' + skip + prmTake + prmLastUid, table);
    });

    appendTotalItemCount(parentDiv, pagingInfo.totalItemCount);

    table.parentElement.appendChild(parentDiv);
}

function updateTableRows(dataPath, table) {
    doGet(dataPath, function (req) {
        let dataResult = JSON.parse(req.responseText);
        if (dataResult.pagingInfo.skip >= dataResult.pagingInfo.totalItemCount
            || dataResult.data.length === 0) {
            return;
        }

        clearChildren(table.lastChild);

        appendRows(dataResult.data, table, dataPath);

        table.parentElement.lastChild.remove();
        appendPaginationPanel(dataResult.pagingInfo, table);
    });
}

function appendGotoFirst(pagingElement, onClick) { pagingElement.appendChild(createButton('<<', onClick)); }
function appendGotoPrevious(pagingElement, onClick) { pagingElement.appendChild(createButton('<', onClick)); }
function appendGotoNext(pagingElement, onClick) { pagingElement.appendChild(createButton('>', onClick)); }
function appendGotoLast(pagingElement, onClick) { pagingElement.appendChild(createButton('>>', onClick)); }

function appendCurrentPage(pagingElement, pageNumber) {
    let span = createElement('span');
    span.innerHTML = pageNumber;
    pagingElement.appendChild(span);
}

function appendGotoPage(pagingElement, btnGoClick) {
    let inputPage = createElement('input');
    inputPage.onkeypress = function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();

            btnGoClick();
        }
    }

    pagingElement.appendChild(inputPage);
    pagingElement.appendChild(createButton('go', btnGoClick));
}

function appendTotalItemCount(pagingElement, totalItemCount) {
    var span = createElement('span');
    span.innerHTML = totalItemCount + ' records';
    pagingElement.appendChild(span);
}

function handleSelectedRows(btn) {
    doIfConfirmed(btn,
        function () {
            let rows = btn.parentElement.querySelectorAll('table tbody tr');

            let ids = '';
            rows.forEach(function (r) {
                let firstColumn = r.firstChild;
                let chk = firstColumn.firstChild;

                if (chk.checked) {
                    ids += 'Uids=' + firstColumn.parentElement.dataset.uid + '&';
                }
            });

            if (ids.length < 1) {
                showPopupMessage('please_select_at_least_one_item');
                return;
            }

            doPostWithFormUrlEncodedContent(btn.dataset.url, ids,
                function (req) {

                    let response = JSON.parse(req.response);
                    if (response.isOk === true) {
                        rows.forEach(function (r) {
                            let firstColumn = r.firstChild;
                            let chk = firstColumn.firstChild;

                            if (chk.checked) {
                                r.remove();
                            }
                        });

                        hidePopup();
                    } else {
                        let messages = response.messages.join('<br/>');
                        showPopupMessage(messages);
                    }
                },
                function (req) {
                    let messages = JSON.parse(req.response);
                    showPopupMessage(messages["messages"].join('<br/>'));
                });
        });
}

function handleRow(btn, urlEncodedData, url, onSuccess) {
    doIfConfirmed(btn,
        function () {
            doPostWithFormUrlEncodedContent(url, urlEncodedData, onSuccess,
                function (req) {
                    let messages = JSON.parse(req.response);
                    showPopupMessage(messages["messages"].join('<br/>'));
                });
        });
}

function handleDeleteRow(btn, url) {
    let uid = btn.parentElement.parentElement.dataset.uid;
    handleRow(btn, 'id=' + uid, url,
        function (req) {
            let response = JSON.parse(req.response);
            if (response.isOk === true) {
                btn.parentElement.parentElement.remove();
                hidePopup();
            } else {
                let messages = response.messages.join('<br/>');
                showPopupMessage(messages);
            }
        });
}

function handlePostAndAppendRow(btn) {
    showPopup(btn.dataset.confirmTitle, btn.dataset.confirmContent, true, function () {
        doPostWithFormUrlEncodedContent(btn.dataset.url, btn.dataset.prm,
            function (req) {
                let response = JSON.parse(req.response);
                if (response.isOk === true) {
                    let table = btn.nextElementSibling;
                    let row = prepareRow(response.item, table);
                    table.lastChild.appendChild(row);

                    hidePopup();
                } else {
                    let messages = response.messages.join('<br/>');
                    showPopupMessage(messages);
                }
            },
            function (req) {
                if (req.status == 500) {
                    showPopupMessage('server_error');
                } else {
                    let messages = JSON.parse(req.response).messages;
                    showPopupMessage(messages["messages"].join('<br/>'));
                }
            });
    });
}

function handleChangeActivationRow(btn, url) {
    let row = btn.parentElement.parentElement;
    let table = row.parentElement.parentElement;
    let thead = table.firstChild;
    let isActiveIndex = 0;
    for (var i = 0; i < thead.firstChild.childNodes.length; i++) {
        if (thead.firstChild.childNodes[i].dataset.translation === 'is_active') {
            break;
        }
        isActiveIndex++;
    }

    let uid = row.dataset.uid;
    handleRow(btn, 'id=' + uid, url,
        function (req) {
            
            let response = JSON.parse(req.response);
            if (response.isOk === true) {
                var children = row.children[isActiveIndex];
                let old = children.children[0].checked;
                let isTrue = old === true;
                children.children[0].checked = !isTrue;

                hidePopup();
            } else {
                let messages = JSON.parse(req.response);
                showPopupMessage(messages["messages"].join('<br/>'));
            }
        });
}

function handleChangeActivationAllRow(btn, url) {
    let row = btn.parentElement.parentElement;
    let table = row.parentElement.parentElement;
    let thead = table.firstChild;
    let isActiveIndex = 0;
    for (var i = 0; i < thead.firstChild.childNodes.length; i++) {
        if (thead.firstChild.childNodes[i].dataset.translation === 'is_active') {
            break;
        }
        isActiveIndex++;
    }

    let uid = row.dataset.uid;
    handleRow(btn, 'id=' + uid, url,
        function (req) {
            
            let response = JSON.parse(req.response);
            if (response.isOk === true) {

                var children = row.children[isActiveIndex];
                var selectedInput = children.children[0];
                var checkboxses=row.parentElement.querySelectorAll('input[type=checkbox]');
                checkboxses.forEach(function (box) {
                    if (box === selectedInput) {

                        let old = children.children[0].checked;
                        let isTrue = old === true;
                        children.children[0].checked = !isTrue;
                    } else {
                        box.checked = false;
                    }

                });


                hidePopup();
            } else {
                let messages = JSON.parse(req.response);
                showPopupMessage(messages["messages"].join('<br/>'));
            }
        });
}

function handleRestoreRow(btn, url, redirectUrl) {
    let row = btn.parentElement.parentElement;
    let table = row.parentElement.parentElement;
    let thead = table.firstChild;

    let revisionIndex = 0;
    for (var i = 0; i < thead.firstChild.childNodes.length; i++) {
        if (thead.firstChild.childNodes[i].dataset.translation === 'revision') {
            break;
        }
        revisionIndex++;
    }

    var theRevision = row.children[revisionIndex].innerText;

    let uid = btn.parentElement.parentElement.dataset.uid;
    handleRow(btn, 'id=' + uid + '&revision=' + theRevision, url,
        function (req) {
            let response = JSON.parse(req.response);
            if (response.isOk === true) {
                window.location.href = redirectUrl + "/" + uid;
            } else {
                let messages = response.messages.join('<br/>');
                showPopupMessage(messages);
            }
        });
}