const trucCongURL = `/api/truccong`
async function getTrucCongByDate(date) {
    let result = await fetch(`${trucCongURL}/getByDate?dateString=${date}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    return result.json();
}
async function postTrucCong(TrucCongData) {
    let result = await fetch(`${trucCongURL}/AddingTrucCong`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(TrucCongData),
    });
    result = await result.json();
}