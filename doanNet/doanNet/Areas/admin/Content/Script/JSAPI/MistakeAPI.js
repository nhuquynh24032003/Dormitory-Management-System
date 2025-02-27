const mistakeURL = `/api/mistake`

async function getMisTakesBySinhVienID(id) {
    let result = await fetch(`${mistakeURL}/getMisTakesBySinhVienID?IDSinhVien=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
    return result;
}
async function putMistake(id,MistakeData) {
    var data = new FormData()
    if (MistakeData.ImageDescription) {
        for (var i = 0; i < MistakeData.ImageDescription.length; i++) {
            data.append(MistakeData.ImageDescription[i].name, MistakeData.ImageDescription[i]);
        }
    }
    
    data.append("MistakeDes", MistakeData.MistakeDes);
    data.append("IDRoom", MistakeData.IDRoom)
    data.append("BedID", MistakeData.BedID)
    data.append("IDSinhVien", MistakeData.IDSinhVien)
    data.append("IDAccount", MistakeData.IDAccount);
    $.ajax({
        type: "PUT",
        url: `${mistakeURL}/PutMistake?id=${id}`,
        contentType: false,
        processData: false,
        data: data,
        success: function () {

        },
        error: function () {

        }
    });
}
/*
const MistakeData = {
            MistakeDes: $(".Mistake_des_typing").val(),
            IDRoom: $(".typing_room").attr("id"),
            BedID: $('#mySelect').val(),
            IDSinhVien: $(".typing_name").attr("id"),
            IDAccount: sessionStorage.getItem("IDAccount") == null ? 1 : sessionStorage.getItem("IDAccount"),
            ImageDescription: imagearr,
        }
*/
async function postMistake(MistakeData) {
    var data = new FormData()
    for (var i = 0; i < MistakeData.ImageDescription.length; i++) {
        data.append(MistakeData.ImageDescription[i].name, MistakeData.ImageDescription[i]);
    }
    data.append("MistakeDes", MistakeData.MistakeDes);
    data.append("IDRoom", MistakeData.IDRoom)
    data.append("BedID", MistakeData.BedID)
    data.append("IDSinhVien", MistakeData.IDSinhVien)
    data.append("IDAccount", MistakeData.IDAccount);
    $.ajax({
        type: "POST",
        url: `${mistakeURL}/PostMistake`,
        contentType: false,
        processData: false,
        data: data,
        success: function () {

        },
        error: function () {

        }
    });
}
async function hideMistake(id) {
    let result = await fetch(`${mistakeURL}/PostMistake?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
    });
    result = await result.json();
}