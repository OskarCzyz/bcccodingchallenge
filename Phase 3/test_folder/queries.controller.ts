import {Request, response, Response } from "express";
import dotenv from 'dotenv';
dotenv.config();
import pg from 'pg';
// const Pool = require("pg").Pool;
const pool = new pg.Pool({
  user: process.env.DB_USER,
  host: `cloudsql/${process.env.INSTANCE_CONNECTION_NAME}`,
  database: process.env.DB_NAME,
  password: process.env.DB_PASS,
});
export const getUsers = async (req: Request, res: Response) => {
    function getqueries() {
      let RandomNums = [
        Math.floor(Math.random() * 2147483647),
        Math.floor(Math.random() * 2147483647),
        Math.floor(Math.random() * 2147483647),
      ];
      return `INSERT INTO numbers (number) VALUES (${RandomNums[0]});INSERT INTO numbers (number) VALUES (${RandomNums[1]});INSERT INTO numbers (number) VALUES (${RandomNums[2]});`;
    }
    console.log(getqueries());
    const response2 = await pool.query(`${getqueries()}`)
      res.status(200)
    const response = await pool.query(`SELECT number FROM numbers;`)
      res.status(200).json(response.rows);
  }
  