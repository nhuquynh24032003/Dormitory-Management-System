const MenuURL = `/api/Menu`

async function postMenu(MenuData) {
    let result = await fetch(`${MenuURL}/AddingMenu`, {
        headers: {
            "Content-Type": "application/json"
        },
        method: 'POST',
        body: JSON.stringify(MenuData),
    });
    result = await result.json();
}