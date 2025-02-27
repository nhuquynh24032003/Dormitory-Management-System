const Feeurl = `/api/fee`

async function getFeeBySinhVienID(id) {
    let result = await fetch(`${Feeurl}/getFeeBySinhVienID?SinhVienID=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
    return result;
}
async function AnalysticFee() {
    let result = await fetch(`${Feeurl}/GetFeesByMonth`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
    return result
}
async function putFee(FeeData, id) {
    let result = await fetch(`${Feeurl}/putCategory?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
        body: JSON.stringify(FeeData),
    });
    result = await result.json();
}
async function addingFee(FeeData) {
    let result = await fetch(`${Feeurl}/AddingFee`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(FeeData),
    });
    result = await result.json();
}