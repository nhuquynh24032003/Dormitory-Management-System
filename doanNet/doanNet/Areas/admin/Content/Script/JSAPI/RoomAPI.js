const Roomurl = `/api/Room`

async function postRoom(RoomData) {
    let result = await fetch(`${Roomurl}/PostRoom`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(RoomData),
    });
    result = await result.json();
    console.log(result)
}
async function findRoomById(id) {
    let result = await fetch(`${Roomurl}/GetByRoomID?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    const resulttosend = await result.json();
    return resulttosend;
}
async function findRoomBySinhVienId(id) {
    let result = await fetch(`${Roomurl}/GetBySinhVienId?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    const resulttosend = await result.json();
    return resulttosend;
}
async function putRoom(id, RoomData) {
    let result = await fetch(`${Roomurl}/PutRoom?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
        body: JSON.stringify(RoomData),
    });
    result = await result.json();
    console.log(result)
}
function Hideroom(id) { }