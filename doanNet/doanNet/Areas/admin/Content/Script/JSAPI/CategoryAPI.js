const Categoryurl = `/api/category`

async function getAllCategory() {
    let result = await fetch(`${Categoryurl}/getAll`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
    return result;

}
async function getCategoryByID(id) {
    let result = await fetch(`${Categoryurl}/getCategoryByID?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
    return result;
}
async function putCategory(CategoryData, id) {
    let result = await fetch(`${Categoryurl}/putCategory?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'PUT',
        body: JSON.stringify(CategoryData),
    });
    result = await result.json();
}
async function postCategory(CategoryData) {
    let result = await fetch(`${Categoryurl}/postCategory`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(CategoryData),
    });
    result = await result.json();
}