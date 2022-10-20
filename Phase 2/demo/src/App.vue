<template>
  <nav class="center">
    <router-link to="/" custom v-slot="{ navigate }">
      <button @click="navigate" role="button">Home</button>
    </router-link> <span style="color: white;"> | </span>
    <router-link to="/Random" custom v-slot="{ navigate }">
      <button @click="navigate" role="button">Random</button>
    </router-link>
  </nav>
  <router-view/>
</template>
<style>
  @import './assets/style.css';
  @import url('https://fonts.googleapis.com/css2?family=Inter&display=swap');
</style>
