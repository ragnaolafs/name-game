import { useEffect, useRef } from 'react';

export function useWebSocket(
    url: string,
    onMessage: (data: any) => void
) {
    const wsRef = useRef<WebSocket | null>(null);

    useEffect(() => {
        const ws = new WebSocket(url);
        wsRef.current = ws;

        ws.onmessage = (event) => {
            try {
                const data = JSON.parse(event.data);
                onMessage(data);
            } catch (err) {
                console.warn('Failed to parse WS message:', err);
            }
        };

        ws.onerror = (err) => console.error('WebSocket error', err);
        ws.onclose = () => console.log('WebSocket closed');

        return () => ws.close();
    }, [url]);
}
