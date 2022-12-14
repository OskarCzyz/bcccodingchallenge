"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.getUsers = void 0;
const dotenv_1 = __importDefault(require("dotenv"));
dotenv_1.default.config();
const pg_1 = __importDefault(require("pg"));
// const Pool = require("pg").Pool;
const pool = new pg_1.default.Pool({
    user: process.env.DB_USER,
    host: process.env.INSTANCE_CONNECTION_NAME,
    database: process.env.DB_NAME,
    password: process.env.DB_PASS,
});
const getUsers = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    function getqueries() {
        let RandomNums = [
            Math.floor(Math.random() * 2147483647),
            Math.floor(Math.random() * 2147483647),
            Math.floor(Math.random() * 2147483647),
        ];
        return `INSERT INTO numbers (number) VALUES (${RandomNums[0]});INSERT INTO numbers (number) VALUES (${RandomNums[1]});INSERT INTO numbers (number) VALUES (${RandomNums[2]});`;
    }
    console.log(getqueries());
    const response2 = yield pool.query(`${getqueries()}`);
    res.status(200);
    const response = yield pool.query(`SELECT number FROM numbers;`);
    res.status(200).json(response.rows);
});
exports.getUsers = getUsers;
