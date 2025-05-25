// hooks/useWebSocket.ts
import { useEffect } from "react";
import { WS_BASE_URL } from "@/config";

export default function useWebSocket(url: string, onMessage: (data: any) => void) {
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
