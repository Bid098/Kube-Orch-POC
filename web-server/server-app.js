const express = require('express')
const path = require('path');
const redis = require('redis');
const client = redis.createClient({
    socket: { host: '172.27.200.198', port: 6379 }
});
const app = express()
const port = 3000


client.on('error', (err) => {
    console.log('Errore di connessione a Redis: ' + err);
});

app.get('*', (req, res) => {
    res.sendFile(path.join(__dirname, 'client', 'index.html'));
});

app.post('/getdata', async (req, res) => {
    // Connessione al client Redis
    client.connect();

    let result = [];

    // Prelevare tutte le chiavi
    let keys = await client.keys("*");

    for(let i = 0; i < keys.length; i++)
    {
        result.push({"value":await client.get(keys[i]), "name":keys[i]});
    }

    // Connessione al client Redis
    client.quit();

    res.status(200).send(JSON.stringify(result));
});

app.listen(port, () => console.log(`Example app listening on port ${port}!`))