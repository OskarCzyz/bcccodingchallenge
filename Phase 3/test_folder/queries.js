const Pool = require("pg").Pool;
const pool = new Pool({
  user: "postgres",
  host: "34.116.203.145",
  database: "test",
  password: "BazyDanych123",
  port: 5432,
});

const getUsers = (request, response) => {
  function getqueries() {
    let RandomNums = [
      Math.floor(Math.random() * 2147483647),
      Math.floor(Math.random() * 2147483647),
      Math.floor(Math.random() * 2147483647),
    ];
    return `INSERT INTO numbers (number) VALUES (${RandomNums[0]});INSERT INTO numbers (number) VALUES (${RandomNums[1]});INSERT INTO numbers (number) VALUES (${RandomNums[2]});`;
  }
  console.log(getqueries());
  pool.query(`${getqueries()}SELECT number FROM numbers;`, (error, results) => {
    if (error) {
      throw error;
    }
    response.status(200).json(results[3].rows);
  });
};

module.exports = {
  getUsers,
};
