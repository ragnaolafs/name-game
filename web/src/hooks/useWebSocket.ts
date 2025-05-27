// hooks/useWebSocket.ts
import { useEffect } from "react";

export default function useWebSocket(url: string, onMessage: (data: any) => void) {
    console.log("useWebSocket initialized for URL:", url);
    useEffect(() => {
        const ws = new WebSocket(url);
        ws.onmessage = (event) => {
            try {
                const data = JSON.parse(event.data);
                onMessage(data);
            } catch (err) {
                console.error("Failed to parse WebSocket message", err);
            }
        };

        return () => {
            ws.close();
        };
    }, [url, onMessage]);
}
