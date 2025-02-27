
const AccountURL = `/api/account`
async function postAccountStudent(accountData) {
    let result = await fetch(`${AccountURL}/AddingAccountStudent`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(accountData),
    });
    result = await result.json();
}
async function CheckingLogin(accountData) {
    let result = await fetch(`${AccountURL}/CheckingLogin`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(accountData),
    });
    return result
}

async function getAccountByID(AccountID) {
    let result = await fetch(`${AccountURL}/GetByID?id=${AccountID}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    return result.json();
}

async function promotedAccountByAccountID(AccountID) {
    let result = await fetch(`${AccountURL}/PromotingAccountType?id=${AccountID}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
    });
    return result.json();
}
async function changingStatus(AccountID) {
    let result = await fetch(`${AccountURL}/ChangingStatus?id=${AccountID}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
    });
    return result.json();
}
async function changingPassword(AccountID,Password) {
    let result = await fetch(`${AccountURL}/ChangingPassword?id=${AccountID}&password=${Password}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
    });
    return result.json();
}



