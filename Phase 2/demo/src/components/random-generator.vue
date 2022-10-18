<template>
  <table>
    <tr>
      <td rowspan="2">How many times?</td>
      <td><input type="text" v-model="net" /></td>
      <td>
        <button @click="show()" style="width: 100%">
          Get data from /random endpoint (.net) nie yo
        </button>
      </td>
      <td rowspan="2">
        <button @click="node(), show()">2 at the same time</button>
      </td>
    </tr>
    <tr>
      <td><input type="text" v-model="nodejs" /></td>
      <td>
        <button @click="node()" style="width: 100%">
          Get data from /random endpoint (nodejs)
        </button>
      </td>
    </tr>
  </table>
  <div>
    <div style="display: inline-block">
      .Net core api <br />
      <div v-html="Numbers"></div>
    </div>
    <div style="display: inline-block; margin-left: 100px">
      Node <br />
      <div v-html="Numbers2"></div>
    </div>
  </div>
</template>
<script>
import axios from "axios";

export default {
  name: "generateNumbers",
  data: () => {
    return {
      result1: "",
      result2: "",
      net: "",
      nodejs: "",
      Numbers: [],
      Numbers2: [],
    };
  },
  methods: {
    show: async function () {
      try {
        let startTime = new Date().getTime();
        let str = "<table border = 2><tr>";
        let res;
        for (let i = 0; i < this.net; i++) {
          res = await axios.get(`https://netcoreapi-j56xipfh6a-lm.a.run.app/random`);
        }
        for (let i = 0; i < res.data.length; i++) {
          if ((i + 1) % 3 == 0) {
            str += `<td>${res.data[i]["number"]}</td></tr>`;
            if (i !== res.data.length) {
              str += "<tr>";
            }
          } else {
            str += `<td>${res.data[i]["number"]}</td>`;
          }
        }

        let endTime = new Date().getTime();
        str += `<td colspan=3 style="text-align: center;">.Net ≈	${Math.round(
          endTime - 4 - startTime
        )} milliseconds</td></tr></table>`;
        return (this.Numbers = str);
      } catch (error) {
        console.log(error);
      }
    },
    node: async function () {
      try {
        let startTime = new Date().getTime();
        let str = "<table border = 2><tr>";
        let res;
        for (let i = 0; i < this.nodejs; i++) {
          res = await axios.get(`https://nodejs-image-j56xipfh6a-lm.a.run.app/random`);
        }
        for (let i = 0; i < res.data.length; i++) {
          if ((i + 1) % 3 == 0) {
            str += `<td>${res.data[i]["number"]}</td></tr>`;
            if (i !== res.data.length) {
              str += "<tr>";
            }
          } else {
            str += `<td>${res.data[i]["number"]}</td>`;
          }
        }

        let endTime = new Date().getTime();
        str += `<td colspan=3 style="text-align: center;">.Node ≈	${Math.round(
          endTime - 4 - startTime
        )} milliseconds</td></tr></table>`;
        return (this.Numbers2 = str);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
