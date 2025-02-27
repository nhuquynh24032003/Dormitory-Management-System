const Priorityurl = `/api/priority`

async function findPriorityById(id) {
    let result = await fetch(`${Priorityurl}/GetByPriorityId?id=${id}`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'GET',
    });
    result = await result.json();
    return result;
}
