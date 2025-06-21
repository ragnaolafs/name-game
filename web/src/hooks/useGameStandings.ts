import { useState, useEffect } from 'react';
import axios from 'axios';
import { useWebSocket } from './useWebSocket';
import { API_URL, WS_URL } from '@/config';
import { GuessResult } from './useGuessStream';

type StandingsResult = {
    gameId: string;
    topGuesses: GuessResult[];
    timestamp: Date;
}

export function useGameStandings(gameId: string) {
    const [standings, setStandings] = useState<StandingsResult>(null);
    const [isLoading, setLoading] = useState(true);
    const [error, setError] = useState<Error | null>(null);

    useEffect(() => {
        axios.get(`${API_URL}/game/${gameId}`)
            .then(res => {
                setStandings(res.data.standings);
                setLoading(false);
            })
            .catch(err => {
                setError(err);
                setLoading(false);
            });
    }, [gameId]);

    useWebSocket(`${WS_URL}/game/${gameId}/standings`, (data) => {
        setStandings(data);
    });

    return { standings, isLoading, error };
}
