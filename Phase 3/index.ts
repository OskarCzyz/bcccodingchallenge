import express, { Express, Request, Response, Router } from 'express';
import dotenv from 'dotenv';
import bodyParser from 'body-parser';
import cors from 'cors';
import {getUsers} from './test_folder/queries.controller';
dotenv.config();


const router = Router();
const app: Express = express();
const port = process.env.PORT;

app.use(bodyParser.json())
app.use(
  cors(),
  bodyParser.urlencoded({
    extended: true,
  })
)


app.get('/', (req: Request, res: Response) => {
  res.send('Express + TypeScript Server is running');
});

app.get('/random', getUsers)

console.log(port);

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at https://localhost:${port}`);
});
export default router;