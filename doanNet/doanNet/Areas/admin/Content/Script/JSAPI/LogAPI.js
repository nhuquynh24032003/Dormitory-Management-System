const Logurl = `api/log`
async function putLog(FeeData, id) {
    let result = await fetch(`${Logurl}/putLog?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
        body: JSON.stringify(FeeData),
    });
    result = await result.json();
}
async function AddingLog(FeeData) {
    let result = await fetch(`${Logurl}/AddingLog`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(FeeData),
    });
    result = await result.json();
}