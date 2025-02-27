const url = `/api/post`

async function putPost(id,PostData) {
    var data = new FormData()
    data.append("PostTitle", PostData.PostTittle);
    data.append("PostDetail", PostData.PostDetail)
    data.append("meta", PostData.meta)
    data.append("PostImage", PostData.PostImage)
    data.append("CategoryList", JSON.stringify(PostData.CategoryList))
    data.append("IDAccount", PostData.IDAccount);
    $.ajax({
        type: "PUT",
        url: `${url}/PutPost?id=${id}`,
        contentType: false,
        processData: false,
        data: data,
        success: function () {

        },
        error: function () {

        }
    });
}
async function postPost(PostData) {
    var data = new FormData()
    data.append("PostTitle", PostData.PostTittle);
    data.append("PostDetail", PostData.PostDetail)
    data.append("meta", PostData.meta)
    data.append("PostImage", PostData.PostImage)
    data.append("CategoryList", JSON.stringify(PostData.CategoryList))
    data.append("IDAccount", PostData.IDAccount);
    $.ajax({
        type: "POST",
        url: `${url}/AddingPost`,
        contentType: false,
        processData: false,
        data: data,
        success: function () {

        },
        error: function () {

        }
    });
    /*let result = await fetch(`${url}/AddingPost`, {
        method: 'POST',
        body: data,
    });
    result = await result.json();*/
}