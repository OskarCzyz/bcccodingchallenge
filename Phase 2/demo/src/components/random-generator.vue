<template>
  <table>
    <tr>
      <td rowspan="2">How many requests?</td>
      <td><input type="text" v-model="net" /></td>
      <td id="modal-container">
        <button class="btn" @click="show()" style="width: 100%">
          <span>From .net</span>
        </button>
      </td>
      <td rowspan="2">
        <button class="btn" @click="node(), show()">Get data from both</button>
      </td>
    </tr>
    <tr>
      <td><input type="text" v-model="nodejs" /></td>
      <td>
        <button class="btn" @click="node()" style="width: 100%">
          <span>From node</span>
        </button>
      </td>
    </tr>
  </table>
  <div class="container">
    <div>
      <img src="https://porozmawiajmyoit.pl/wp-content/uploads/2021/04/1200px-.NET_Logo.svg_.png" alt=".net"> <br />
      <div v-html="Numbers"></div>
    </div>
    <div>
      <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Node.js_logo.svg/1920px-Node.js_logo.svg.png" alt="node"> <br />
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
        let str = "<tr>";
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
        let str1 = `<table class='result1' border = 1><tr><td class="speed" colspan=3 style="text-align: center;">.Net ≈	${Math.round(
          endTime - 4 - startTime
        )} milliseconds</td><tr>`;
        str += `</tr></table>`;
        str1 += str;
        return (this.Numbers = str1);
      } catch (error) {
        console.log(error);
      }
    },
    node: async function () {
      try {
        let startTime = new Date().getTime();
        let str = "<tr>";
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
        let str1 = `<table class='result2' border = 1><tr><td class="speed" colspan=3 style="text-align: center;">Node ≈	${Math.round(
          endTime - 4 - startTime
        )} milliseconds</td><tr>`
        str += `</tr></table>`;
        str1 += str;
        return (this.Numbers2 = str1);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
