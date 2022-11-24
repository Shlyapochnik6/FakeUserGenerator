let count = 1;
let seed = +Number(0);
let countLoad = 0;
let enteredSeed = document.getElementById('input-seed');
let seedBtn = document.getElementById('seed-btn');
let selCountry = document.getElementById('sel-country');
let bodyTable = document.getElementById('table-body');
let allTable = document.getElementById('all-table');
let errorsRange = document.getElementById('range-errors');
let errorsInput = document.getElementById('input-count-errors');

function generateRandomValue(min, max){
    return Math.round(min + Math.random() * (max - min));
}

function generateRandomSeed() {
    let max = 999999;
    let min = 0;
    enteredSeed.value = Math.round(min + Math.random() * (max - min));
}

async function sendServer(addRowsCount, errorCount) {
    let country = selCountry.options[selCountry.selectedIndex].value;
    return await fetch(`User/Index?rowsNum=${addRowsCount}&country=${country}`, {
        method: "POST",
        body: JSON.stringify({
            seed: seed, numberErrors: errorCount
        }),
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
            <td>${item.address.substring(0, 40)}</td>
            <td>${item.name.substring(0 ,40)}</td>
            <td>${item.phoneNumber.substring(0, 40)}</td>
        </tr>`;
            let tr = document.createElement('tr');
            tr.innerHTML = html.trim();
            bodyTable.appendChild(tr);
        });
        bodyTable.appendChild(tr)
    })
}

function downloadCSVFile(csv_data) {
    let csvFile = new Blob([csv_data], {
        type: "text/csv"
    });
    const tempLink = document.createElement('a');
    tempLink.download = "table.csv";
    tempLink.href = window.URL.createObjectURL(csvFile);
    tempLink.style.display = "none";
    document.body.appendChild(tempLink);
    tempLink.click();
    document.body.removeChild(tempLink);
}

function tableToCSV() {
    let csvData = [];
    const rows = document.getElementsByTagName('tr');
    for (let i = 0; i < rows.length; i++) {
        const cols = rows[i].querySelectorAll('td,th');
        const csvrow = [];
        for (let j = 0; j < cols.length; j++) {
            csvrow.push(cols[j].innerHTML);
        }
        if (csvrow.length !== 0) {
            csvData.push(csvrow.join(":"));
        }
    }
    csvData = csvData.join('\n');
    downloadCSVFile(csvData);
}

async function findTablePos() {
    const hBody = bodyTable.offsetHeight;
    const hTable = allTable.offsetHeight;
    const scroll = allTable.scrollTop;
    const threshold = hBody - hBody / 5;
    const position = scroll + hTable;
    if (position >= threshold && countLoad !== count) {
        countLoad = count;
        countLoad = count;

        seed += Number(10)
        let response = await sendServer(10, errorsInput.value);
        addRowsTable(response);

        countLoad = count;
    }
}

window.onload = async function(){
    let response = await sendServer(20)
    addRowsTable(response)
    seed = 10
}

seedBtn.addEventListener('click', async () => {
    enteredSeed.value = +Number(+generateRandomValue(1000000, 9999999))
    let text = +enteredSeed.value
    bodyTable.innerHTML = ''
    count = 1
    countLoad = 1
    seed = +Number(+text);
    let response = await sendServer(20, errorsInput.value);
    addRowsTable(response);
    seed = +Number(+text)+20;
})

enteredSeed.addEventListener("change", async function () {
    let text = enteredSeed.value
    bodyTable.innerHTML = ''
    count = 1
    countLoad = 1
    seed = +Number(+text)
    let response = await sendServer(20, errorsInput.value);
    addRowsTable(response);
    seed = +Number(+text)+20
})

selCountry.addEventListener("click", async function (){
    let text = enteredSeed.value
    bodyTable.innerHTML = ''
    count = 1
    countLoad = 1
    seed = +Number(+text)
    let response = await sendServer(20, errorsInput.value)
    addRowsTable(response)
    seed = +Number(+text)+20
})

allTable.addEventListener('scroll', async function () {
    await findTablePos();
})

errorsRange.addEventListener('input', async () => {
    errorsInput.value = Number(+errorsRange.value)
    if (errorsRange.value === "0"){
        errorsRange.value = 0
    }
})

errorsRange.addEventListener('change', async () => {
    let text = +enteredSeed.value
    bodyTable.innerHTML = ''
    count = 1
    countLoad = 1
    text = +enteredSeed.value
    seed = +Number(+text);
    let response = await sendServer(20, +errorsInput.value);
    addRowsTable(response);
    seed = +Number(+text)+20;
})

errorsInput.addEventListener("change", async function () {
    let text = errorsInput.value
    if (errorsInput.value > 1000)
        errorsInput.value = 1000
    text = enteredSeed.value
    if (errorsInput.value <= 10)
        errorsRange.value = Number(+errorsInput.value)
    else errorsRange.value = 10
    text = enteredSeed.value
    bodyTable.innerHTML = ''
    count = 1
    countLoad = 1
    text = enteredSeed.value
    seed = +Number(+text)
    let response = await sendServer(20, +errorsInput.value);
    addRowsTable(response);
    seed = +Number(+text)+20;
})


