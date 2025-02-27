let baseURL = "/api"
const url = `${baseURL}/account`
async function checkingLogin(username, password) {
    const logindata = {
        "username": username,
        "password": password
    }
    let result = await fetch(`${url}/CheckingLogin`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(logindata),
    });
    result = await result.json();
}
async function changePassword(username, password) {
    const logindata = {
        "username": username,
        "password": password
    }
    let result = await fetch(`${url}/ChangingPassword`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(logindata),
    });
    result = await result.json();
}
