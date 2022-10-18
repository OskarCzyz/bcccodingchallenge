<template>
  <nav>
    <router-link to="/" custom v-slot="{ navigate }">
      <button @click="navigate" role="button">Home</button>
    </router-link> |
    <router-link to="/Random" custom v-slot="{ navigate }">
      <button @click="navigate" role="button">Random</button>
    </router-link>
  </nav>
  <router-view/>
</template>
<style>
  @import './assets/style.css';
</style>

