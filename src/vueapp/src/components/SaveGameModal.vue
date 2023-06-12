<script>
import { SaveGame } from '../api/games.api'

    export default {
        props: {
            game: {}
        },
        data() {
            return {
               username: '', 
               password: '' 
            }
        }, 
        methods: {
            async SaveResult() {
                console.log(this.username, this.password)
                console.log(this.game.time, this.game.slides)
                const game = {
                    username: this.username,
                    password: this.password,
                    time: this.game.time,
                    slides: this.game.slides
                }
                const result = await SaveGame(game);
                this.$emit('close-modal')
            }
        }
    }
</script>

<template>
  <div class="modal-overlay">
    <div class="modal">
      <h1>Enter you credentials to save your game</h1>
      <label for="username">Enter username:</label>
      <input id="username" type="text" v-model="this.username" placeholder="username">
      <label for="password">Enter password:</label>
      <input type="password" v-model="this.password" placeholder="password">
      <div class="button-group">
        <button @click="SaveResult">Save</button>
        <button @click="() => $emit('close-modal')">Close</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.modal-overlay {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    display: flex;
    justify-content: center;
    background-color: #000000da;
}

.modal {
    display: flex;
    flex-direction: column;
    justify-content: center;
    text-align: center;
    background-color: white;
    height: 400px;
    width: 400px;
    margin-top: 10%;
    padding: 60px 0;
    border-radius: 20px;
}
.close {
    margin: 10% 0 0 16px;
    cursor: pointer;
}

h1 {
    font-weight: bold;
    font-size: 2rem;
    margin: 20px 0;
    margin: 0 1rem 1rem 1rem;
}

label {
    font-size: 1.5rem;
}

input {
    padding: 0.5rem;
    font-size: 1.1rem;
    margin: 0 2rem;
    border-radius: 10px;
    border: 3px solid black;
}


button {
    font-family: "Pacifico", sans-serif;
    font-size: 1.1rem;
    background-color: #ac003e;
    width: 150px;
    height: 40px;
    color: white;
    border-radius: 10px;
    border: 3px solid black;
    margin-top: 50px;
}

.button-group {
    margin: 0 10%;
    display: flex;
    justify-content: space-around;
}
</style>
