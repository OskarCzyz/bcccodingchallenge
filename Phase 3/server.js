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
<<<<<<< HEAD
  response.json({ info: 'Test' })
})

app.get('/random', db.getUsers)

app.get('/test', (request, response) => {
  response.json({ number: `${Math.floor(Math.random() * 2147483647)}` })
})
=======
  response.json({ info: 'Node.js, Express, and Postgres API' })
})



app.get('/random', db.getUsers)

>>>>>>> f25f5e6 (initial commit)

app.listen(port, () => {
    console.log(`App running on port ${port}.`)
  })