let overlay = getElement('overlay');
let overlayMessage = getElement('overlayMessage');
let popup = getElement('popup');
let popupContent = getElement('popupContent');
let popupMessage = getElement('popupMessage');
let popupMessageContent = getElement('popupMessageContent');

function hidePopup() {
    popupContent.innerHTML = '';

    hideElement(popup);
    hideElement(overlay);
}

function hidePopupMessage() {
    popupMessageContent.innerHTML = '';

    hideElement(popupMessage);
    hideElement(overlayMessage);
}

function showPopup(title, content, isConfirm, onConfirm) {

    document.querySelector('#popup h1').innerHTML = title;
    popupContent.innerHTML = content;

    showElement(popup, 1);
    showElement(overlay, 0.8);

    if (isConfirm) {
        let btnOk = createButton('ok', onConfirm);
        btnOk.className = 'btnGreen';

        let btnCancel = createButton('cancel', hidePopup);
        btnCancel.className = 'btnRed';

        let divBtn = createElement('div');
        divBtn.style.marginTop = '13px';

        divBtn.appendChild(btnOk);
        divBtn.appendChild(btnCancel);
        popupContent.appendChild(divBtn);
    }
}

function showPopupMessage(message) {

    document.querySelector('#popupMessage h1').innerHTML = message;

    showElement(popupMessage, 1);
    showElement(overlayMessage, 0.8);

    let btnOk = createButton('ok', function () {
        hidePopupMessage();
        hidePopup();
    });
    btnOk.className = 'btnGreen';

    let divBtn = createElement('div');
    divBtn.style.marginTop = '13px';

    divBtn.appendChild(btnOk);
    popupMessageContent.appendChild(divBtn);
}
if (document.getElementById('overlay')) {
  var popupOverlay = document.getElementById('overlay');
  popupOverlay.onclick = function () {
    hidePopup();
  };
}