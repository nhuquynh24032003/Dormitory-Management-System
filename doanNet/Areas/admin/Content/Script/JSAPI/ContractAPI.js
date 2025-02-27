const ContractURL = `/api/contract`
async function getContractBySinhVienID(id) {
    let result = await fetch(`${ContractURL}/getContractsBySinhVienID?IDSinhVien=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
    return result;
}
async function putContract(ContractData) {
    var data = new FormData()
    data.append("ContractID", ContractData.ContractID);
    data.append("IDSinhVien", ContractData.IDSinhVien);
    data.append("IDCitizen", ContractData.IDCitizen)
    data.append("ProfilePlace", ContractData.ProfilePlace)
    data.append("IDPlace", ContractData.IDPlace)
    data.append("Description", ContractData.Description)
    data.append("TimeExpired", ContractData.TimeExpired);
    data.append("IDPriority", ContractData.IDPriority);
    /*
    ${ContractURL}/AddingContract
    */
    $.ajax({
        type: "PUT",
        url: `${ContractURL}/PutContract`,
        contentType: false,
        processData: false,
        data: data,
        success: function () {
            window.location.reload();
        },
        error: function () {

        }
    });
}
async function postContract(ContractData) {
    
    var data = new FormData()
    data.append("IDSinhVien", ContractData.IDSinhVien);
    data.append("IDCitizen", ContractData.IDCitizen)
    data.append("ProfilePlace", ContractData.ProfilePlace)
    data.append("IDPlace", ContractData.IDPlace)
    data.append("Description", ContractData.Description)
    data.append("TimeExpired", ContractData.TimeExpired);
    data.append("IDPriority", ContractData.IDPriority);
    /*
    ${ContractURL}/AddingContract
    */
    $.ajax({
        type: "POST",
        url: `${ContractURL}/AddingContract`,
        contentType: false,
        processData: false,
        data: data,
        success: function () {

        },
        error: function () {

        }
    });
}
async function GetContractByIDSinhvien(IDSinhVien) {
    let result = await fetch(`${ContractURL}/GetContractByIDSinhvien?IDSinhVien=${IDSinhVien}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
}
async function GetContractById(IDContract) {
    let result = await fetch(`${ContractURL}/GetByContractId?id=${IDContract}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    return await result.json();
}
async function ApproveContract(ContractID) {
    let result = await fetch(`${ContractURL}/ApproveContract`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
        body: JSON.stringify(ContractID),
    });
    result = await result.json();
}
