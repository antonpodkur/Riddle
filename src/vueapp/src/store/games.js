import { defineStore } from "pinia";
import { computed, ref } from "vue";

export const useGamesStore = defineStore('games', () => {
    const games  = ref([])
    const bestScores = ref([])

    return {
        games, 
        bestScores
    }
})