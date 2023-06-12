import axios from "axios";
import { useGamesStore } from "../store/games";

export const axiosInstance = axios.create({
    baseURL: "http://localhost:5245/",
})


export const SaveGame = async (game) => {
    const result = await axiosInstance.post('/Game/saveGame', game)
    console.log(result)
    if (result.status !== 200) {
        console.error("Failed saving game")
    }

    await LoadBestScores();
    return result.data
}

export const LoadBestScores = async () => {
    const store = useGamesStore()
    const result = await axiosInstance.get('/Game/getBests') 
    console.log(result)
    if (result.status !== 200) {
        console.error("Error while getting best scores")
    }
    store.bestScores = result.data.results
    return result.data.results
}