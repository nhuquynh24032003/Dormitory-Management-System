const Attendanceurl = `/api/attendance`

async function putAttendance(AttedanceData,id) {
    let result = await fetch(`${Attendanceurl}/PutAttendance?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
        body: JSON.stringify(AttedanceData),
    });
    result = await result.json();
}
async function postAttendance(AttedanceData) {
    
    let result = await fetch(`${Attendanceurl}/AddingAttendance`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(AttedanceData),
    });
    result = await result.json();
}
async function getAllAttendanceBySinhVienIDNow(SinhVienID) {
    let result = await fetch(`${Attendanceurl}/GetAllAttendanceBySinhVienIDNow?SinhvienID=${SinhVienID}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    return result.json();
}
