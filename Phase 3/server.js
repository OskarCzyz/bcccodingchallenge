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
  response.json({ info: 'Test 015' })
})

app.get('/random', db.getUsers)

app.get('/test', (request, response) => {
  response.json({ number: `${Math.floor(Math.random() * 2147483647)}` })
})

app.get('/loaderio-53ea7526e9151fc1384f1cec39b9c739', (request, response) => {
  response("loaderio-53ea7526e9151fc1384f1cec39b9c73")
})

app.listen(port, () => {
    console.log(`App running on port ${port}.`)
  })