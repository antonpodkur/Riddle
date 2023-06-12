<script>
import { mapStores } from 'pinia';
import { useGamesStore } from '../store/games';


export default {
    data() {
        return {
        activeTab: 'myGames',
        };
    },
    computed: {
        ...mapStores(useGamesStore),
        myGames() {
            return this.gamesStore.games
        },
        bestScores() {
            return this.gamesStore.bestScores
        }
    }
};
</script>

<template>
    <div class="results">
      <div class="tabs">
        <button class="tab" :class="{ active: activeTab === 'myGames' }" @click="activeTab = 'myGames'">My Games</button>
        <button class="tab" :class="{ active: activeTab === 'bestScores' }" @click="activeTab = 'bestScores'">World Records</button>
      </div>
      <div v-if="activeTab === 'myGames'">
        <table>
          <thead>
            <tr>
              <th>Time (seconds)</th>
              <th>Slides</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="game, index in myGames" :key="index">
              <td>{{ game.time }}</td>
              <td>{{ game.slides }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div v-if="activeTab === 'bestScores'">
        <table>
          <thead>
            <tr>
              <th>Player</th>
              <th>Time (seconds)</th>
              <th>Slides</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="record, index in bestScores" :key="index">
              <td>{{ record.username }}</td>
              <td>{{ record.time }}</td>
              <td>{{ record.slides }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </template>
  
  
  <style scoped>

  .results {
    font-family: "Pacifico", sans-serif;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
  .tabs {
    display: flex;
  }
  
  .tab {
    font-family: "Pacifico", sans-serif;
    font-size: 1.5rem;
    padding: 10px 20px;
    background-color: #f0f0f0;
    border-radius: 5px;
    border: none;
    cursor: pointer;
  }
  
  .tab.active {
    background-color: #ccc;
  }
  
  table {
    margin-top: 10px;
    border-collapse: collapse;
    border-radius: 5px;
  }
  
  table th,
  table td {
    font-size: 1.2rem;
    padding: 5px 10px;
    border: 3px solid #ccc;
  }
  </style>
  