
const SinhVienURL = `/api/SinhVien`

async function getAllSinhVienByRoom(roomid) {
    let result = await fetch(`${SinhVienURL}/GetAllSinhVienByRoom?roomid=${roomid}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    return result.json();
    
}

async function PostSinhVien(SinhVienData) {
    let result = await fetch(`${SinhVienURL}/AddingSinhVien`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(SinhVienData),
    });
    result = await result.json();
    console.log(result) 
}
async function findSinhVienById(id) {
    let result = await fetch(`${SinhVienURL}/GetByID?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    const resulttosend = await result.json();
    return resulttosend;
}
async function findSinhVienByMSSV(MSSV) {
    let result = await fetch(`${SinhVienURL}/GetByMSSV?mssv=${MSSV}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    const resulttosend = await result.json();
    return resulttosend;
}
async function PutSinhVien(id,SinhVienData) {
    let result = await fetch(`${SinhVienURL}/PutSinhVien?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
        body: JSON.stringify(SinhVienData),
    });
    result = await result.json();
    console.log(result)
}
