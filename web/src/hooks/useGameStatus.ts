import { useState, useEffect } from 'react';
import axios from 'axios';
import { useWebSocket } from './useWebSocket';
import { API_URL, WS_URL } from '@/config';

export function useGameStatus(gameId: string) {
    const [status, setStatus] = useState<string | null>(null);
    const [isLoading, setLoading] = useState(true);
    const [error, setError] = useState<Error | null>(null);

    useEffect(() => {
        axios.get(`${API_URL}/game/${gameId}`)
            .then(res => {
                setStatus(res.data.status);
                setLoading(false);
            })
            .catch(err => {
                setError(err);
                setLoading(false);
            });
    }, [gameId]);

    useWebSocket(`${WS_URL}/game/${gameId}/status`, (data) => {
        if (data.status) setStatus(data.status);
    });

    return { status, isLoading, error };
}
