<script>
import { shuffleArray } from '../utils/shuffle'
import {useGamesStore} from '../store/games'
import { mapStores } from 'pinia'
import SaveGameModal from './SaveGameModal.vue';

export default {
    data() {
        return {
            tiles: [],
            time: 0,
            slides: 0,
            gameResults: [],
            timer: null,
            slideIndex: null,
            lastGame: null,
            showModal: false
        };
    },
    mounted() {
        this.restart();
    },
    computed: {
        ...mapStores(useGamesStore)
    },
    methods: {
        restart() {
            this.lastGame = null;
            this.shuffleTiles();
            this.time = 0;
            this.slides = 0;
            this.slideIndex = null;
            clearInterval(this.timer);
            this.timer = setInterval(() => {
                this.time++;
            }, 1000);
        },
        shuffleTiles() {
            let numbers = Array.from({ length: 15 }, (_, i) => i + 1);
            numbers.push(0);
            let shuffledArray = shuffleArray(numbers)
            // let shuffledArray = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 15];
            this.tiles = shuffledArray;
        },
        move(index) {
            if (this.tiles[index] === 0)
                return;
            if (this.isSolved()) {
              console.log("solved")
              this.restart()
              return;
            }
            let emptyIndex = this.tiles.indexOf(0);
            let row = Math.floor(index / 4);
            let col = index % 4;
            let emptyRow = Math.floor(emptyIndex / 4);
            let emptyCol = emptyIndex % 4;
            if ((row === emptyRow && Math.abs(col - emptyCol) === 1) ||
                (col === emptyCol && Math.abs(row - emptyRow) === 1)) {
                this.slideIndex = index;
                this.slideIndex = null;
                this.tiles.splice(emptyIndex, 1, this.tiles[index]);
                this.tiles.splice(index, 1, 0);
                this.slides++;
                if (this.isSolved()) {
                    clearInterval(this.timer);
                    this.lastGame = {
                        time: this.time,
                        slides: this.slides
                    };
                    this.saveGameResult();
                }
            }
        },
        isSolved() {
            for (let i = 0; i < this.tiles.length - 1; i++) {
                if (this.tiles[i] !== i + 1) {
                    return false;
                }
            }
            return true;
        },
        saveGameResult() {
            let result = {
                time: this.time,
                slides: this.slides
            };
            this.gameResults.push(result);
            localStorage.setItem("gameResults", JSON.stringify(this.gameResults));
            this.gamesStore.games = this.gameResults;
        },
        saveGameToDb() {
            this.showModal = true;
            console.log(this.lastGame);
            console.log(this.gameResults[this.gameResults.length - 1]);
        },
    },
    created() {
        this.gameResults = this.gamesStore.games;
    },
    components: { SaveGameModal }
}
</script>

<template>
  <div class="puzzle">
    <div class="board">
      <div
        v-for="(tile, index) in tiles"
        :key="tile"
        :class="{ empty: tile === 0, correct: tiles[index] === index + 1 }"
        @click="() => move(index)"
      >
        {{ tile === 0 ? '' : tile }}
      </div>
    </div>
    <div class="stats">
      <div>Time: {{ time }} seconds</div>
      <div>Slides: {{ slides }}</div>
    </div>
    <div class="button-group">
      <button @click="restart">Restart</button>
      <button v-if="lastGame" @click="saveGameToDb">Save</button>
      <SaveGameModal v-if="showModal" @close-modal="() => showModal = false" :game="this.lastGame"/>
    </div>
  </div>
</template>

<style scoped>
.puzzle {
  display: flex;
  font-family: "Pacifico", sans-serif;
  flex-direction: column;
  align-items: center;
  padding: 20px;
}

.board {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  grid-gap: 5px;
  width: 300px;
  height: 300px;
  background-color: lightgray;
  padding: 5px;
  border-radius: 5px;
}

.board div {
  color: black;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 20px;
  background-color: white;
  border-radius: 5px;
  cursor: pointer;
  transition: 0.3s;
}

.board div.empty {
  background-color: gray;
  cursor: default;
}

.board div.correct {
  font-weight: bold;
  background-color: #e1fae2;
}

.stats {
  margin-top: 10px;
  font-size: 1.5rem;
}

.button-group {
  display: flex;
  justify-content: space-around;
  width: 100%;
}

button {
  font-size: 1.3rem;
  font-family: "Pacifico", sans-serif;
  margin-top: 10px;
  border-radius: 10px;
  border: 3px solid black;
  padding: 5px 15px;
}

</style>
