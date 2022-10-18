require('@google-cloud/trace-agent').start();
const express = require('express')
const bodyParser = require('body-parser')
const cors = require('cors');
const app = express()
const db = require('./test_folder/queries.js')
const port = 3000


app.use(bodyParser.json())
app.use(
  cors(),
  bodyParser.urlencoded({
    extended: true,
  })
)

app.get('/', (request, response) => {
  response.json({ godzina: '15:08' })
})

app.get('/random', db.getUsers)

app.get('/test', (request, response) => {
  response.json({ number: `${Math.floor(Math.random() * 2147483647)}` })
})

app.get('/loaderio-a043f9e4a31a2275fc611c597f6e0896', (request, response) => {
  response.send(`loaderio-a043f9e4a31a2275fc611c597f6e0896`)
})

app.listen(port, () => {
    console.log(`App running on port ${port}.`)
  })