import { useState, useEffect } from 'react';
import axios from 'axios';
import { useWebSocket } from './useWebSocket';
import { API_URL, WS_URL } from '@/config';

type GameStatusResult = {
    status: string;
    gameHandle: string;
    winner?: WinnerResult;
}

type WinnerResult = {
    winner: string;
    answer: string;
};

export function useGameStatus(gameId: string) {
    const [status, setStatus] = useState<GameStatusResult | null>(null);
    const [isLoading, setLoading] = useState(true);
    const [error, setError] = useState<Error | null>(null);

    useEffect(() => {
        axios.get(`${API_URL}/game/${gameId}/status`)
            .then(res => {
                setStatus(res.data);
                setLoading(false);
            })
            .catch(err => {
                setError(err);
                setLoading(false);
            });
    }, [gameId]);

    useWebSocket(`${WS_URL}/game/${gameId}/status`, (data) => {
        if (data) setStatus(data);
    });

    return { status, isLoading, error };
}
