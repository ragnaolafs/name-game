import { useState, useEffect } from 'react';
import axios from 'axios';
import { useWebSocket } from './useWebSocket';
import { API_URL, WS_URL } from '@/config';

export type GuessResult = {
    id: string;
    user: string;
    guess: string;
    score: number;
    scorePercent: number;
    timestamp: Date;
};

export function useGuessStream(gameId: string, limit = 50) {
    const [guesses, setGuesses] = useState<GuessResult[]>([]);
    const [isLoading, setLoading] = useState(true);
    const [error, setError] = useState<Error | null>(null);

    useEffect(() => {
        axios.get(`${API_URL}/game/${gameId}/guesses?Limit=${limit}`)
            .then(res => {
                setGuesses(res.data);
                setLoading(false);
            })
            .catch(err => {
                setError(err);
                setLoading(false);
            });
    }, [gameId, limit]);

    useWebSocket(`${WS_URL}/game/${gameId}/guess-stream`, (data) => {
        setGuesses(prev => [data, ...prev].slice(0, limit));
    });

    return { guesses, isLoading, error };
}
