<script async setup>
import Riddle from '../components/Riddle.vue';
import GameResults from '../components/GameResults.vue';
import { onMounted } from 'vue';
import { useGamesStore } from '../store/games';
import { LoadBestScores } from '../api/games.api'

onMounted( async () => {
    await LoadBestScores();
}) 

const store = useGamesStore()

if (localStorage.gameResults) {
    store.games = JSON.parse(localStorage.getItem("gameResults")) 
} 
</script>

<template>
        <div class="game">
            <Riddle/>
            <GameResults/>
        </div>
</template>

<style scoped>
    .game {
        width: 100%;
        display: flex;
        justify-content: space-around;
        align-items: center;
    }

    @media screen and (max-width: 768px) {
        .game {
            width: 100%;
            display: flex;
            flex-direction: column;
            align-items: center;
        }
    }
</style>
