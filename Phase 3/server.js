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
  response.json({ info: 'Node.js, Express, and Postgres API' })
})



app.get('/random', db.getUsers)


app.listen(port, () => {
    console.log(`App running on port ${port}.`)
  })