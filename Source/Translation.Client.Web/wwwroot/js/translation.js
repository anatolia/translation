let currentUser = null;
let labels = JSON.parse(localStorage.getItem('labels'));

let promise1 = new Promise ((resolve, reject) => {
    doGet('/Data/GetCurrentUser', function (req) {
        if (199 < req.status && req.status < 300) {
            if (req.status === 200
                && req.responseText !== null) {
                resolve(req.responseText);
                localStorage.setItem('currentUser', req.responseText);
                currentUser = JSON.parse(localStorage.getItem('currentUser'));
            } else if (req.status === 200
                && req.responseText === null) {
                currentUser = null;
                window.redirect('/Login');
            } else if (req.status === 500) {
                localStorage.clear();
                window.redirect('/Login');
            }
        }
    });
}).then(res => {
    if (labels == null
    || labels.length === 0) {
    doGet('/Data/GetMainLabels', function (req) {
        if (199 < req.status && req.status < 300) {
            labels = localStorage.setItem('labels', req.responseText);
            translateScreen();
        }
    });
} else {
    translateScreen();
}}).catch(e => {});

function translateScreen() {
        translateElement(document.head);
        translateElement(document.body);
}

function translateElement(element) {
    if (element === null
        || element === undefined) {
        return;
    }

    if (labels === null
        || labels === undefined) {
        return;
    }

    let defaultLang = 'en';
    if (currentUser !== undefined
        && currentUser !== null) {
        defaultLang = currentUser.languageCode;
    }

    let placeholders = element.querySelectorAll('[placeholder]');
    placeholders.forEach(function (item) {
        for (let i = 0; i < labels.length; i++) {
            let label = labels[i];
            if (label.key === item.placeholder) {
                label.translations.forEach(function (translation) {
                    if (translation.languageIsoCode2 === defaultLang) {
                        item.setAttribute('placeholder', translation.translation);

                        return;
                    }
                });

                break;
            }
        }
    });

    let items = element.querySelectorAll('[data-translation]');
    items.forEach(function (item) {
        for (let i = 0; i < labels.length; i++) {
            let label = labels[i];
            if (label.key === item.dataset.translation) {
                label.translations.forEach(function (translation) {
                    if (translation.languageIsoCode2 === defaultLang) {
                        item.innerHTML = translation.translation;
                        return;
                    }
                });

                break;
            }
        }
    });
}