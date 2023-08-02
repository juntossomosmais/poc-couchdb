// Initialize PouchDB
const localDB = new PouchDB('my_local_db');
const remoteDB = new PouchDB('http://admin:admin_password@localhost:5984/poc-jsm');

// Function to display data in the table
function displayData(data) {
  const table = document.getElementById('data-table');

  // Clear the table before displaying new data
  while (table.rows.length > 1) {
    table.deleteRow(1);
  }

  for (const item of data) {
    const row = table.insertRow();
    row.insertCell().textContent = item.name;
    row.insertCell().textContent = item.surname;
    row.insertCell().textContent = item.age;
  }
}

// Function to fetch data from PouchDB
function fetchAndDisplayData() {
  localDB.allDocs({ include_docs: true }).then(result => {
    const data = result.rows.map(row => row.doc);
    displayData(data);
  });
}

// Function to sync data with the remote CouchDB
function syncData() {
  localDB.sync(remoteDB, { live: true, retry: true }).on('change', info => {
    console.log('Alterações detectadas:', info);
    fetchAndDisplayData();
  });
}

// Main function to kick off the application
function main() {
  fetchAndDisplayData();
  syncData();
}

main();