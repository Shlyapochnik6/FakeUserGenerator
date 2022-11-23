let count = 1;
let seed = +Number(0);
let countLoad = 1;
let enteredSeed = document.getElementById('input-seed');
let seedBtn = document.getElementById('seed-btn');
let selCountry = document.getElementById('sel-country');
let bodyTable = document.getElementById('body-table');
let allTable = document.getElementById('all-table');

function generateRandomValue(min, max){
    return Math.round(min + Math.random() * (max - min));
}

function generateRandomSeed() {
    let max = 999999;
    let min = 0;
    enteredSeed.value = Math.round(min + Math.random() * (max - min));
}

async function sendServer(addRowsCount) {
    let country = selCountry.options[selCountry.selectedIndex].value;
    return await fetch(`User/Index?rowsNum=${addRowsCount}&country=${country}`, {
        method: "POST",
        body: JSON.stringify(seed),
        headers: {
            "Content-Type": "application/json"
        }
    });
}

function addRowsTable(response) {
    let tr = document.createElement('tr');
    let json = response.json();
    json.then(data => {
        data.forEach(item => {
            let html = `<tr>
            <td>${count++}</td>
            <td>${generateRandomValue(1000000, 9999999999)}</td>
            <td>${item.name.substring(0, 40)}</td>
            <td>${item.address.substring(0 ,40)}</td>
            <td>${item.phoneNumber.substring(0, 40)}</td>
        </tr>`;
            let tr = document.createElement('tr');
            tr.innerHTML = html.trim();
            bodyTable.appendChild(tr);
        });
        bodyTable.appendChild(tr)
    })
}

async function findTablePos() {
    const hBody = bodyTable.offsetHeight;
    const hTable = allTable.offsetHeight;
    const scroll = allTable.scrollTop;
    const threshold = hBody - hTable / 5;
    const position = scroll + hTable;
    if (position >= threshold && countLoad !== count) {
        countLoad = count;
        seed += 10;
        let response = await sendServer(10);
        addRowsTable(response);
        enteredSeed.value = +enteredSeed.value + countLoad;
        seed = seed + countLoad;
        countLoad = count;
        seed.value = seed;
    }
}

window.onload = async function(){
    seed = 20;
    let response = await sendServer(20);
    addRowsTable(response);
    allTable.scrollTo(0, 0);
    count = 1;
    $(bodyTable).find('tr').remove();
}

seedBtn.addEventListener('click', async () => {
    generateRandomSeed();
    seed = +enteredSeed.value;
    let response = await sendServer(20);
    addRowsTable(response);
    allTable.scrollTo(0, 0);
    count = 1;
    $(bodyTable).find('tr').remove();
})

enteredSeed.addEventListener('change', async () => {
    seed = +enteredSeed.value;
    let response = await sendServer(20);
    addRowsTable(response);
    allTable.scrollTo(0, 0);
    count = 1;
    $(bodyTable).find('tr').remove();
})

window.onscroll = async function() {
    setTimeout(await findTablePos(), 150);
};

selCountry.addEventListener('click', async () => {
    let response = await sendServer(20);
    addRowsTable(response);
    allTable.scrollTo(0, 0);
    count = 1;
    $(bodyTable).find('tr').remove();
    
})