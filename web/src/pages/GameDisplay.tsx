import { useParams } from "react-router-dom";
import { useState } from "react";
import { WS_BASE_URL } from "@/config";
import StandingsPanel from "@/components/StandingsPanel";
import GuessStream from "@/components/GuessStream";
import useWebSocket from "@/hooks/useWebSocket";
import React from "react";

export default function GameDisplay() {
  const { gameId } = useParams();
  const [guesses, setGuesses] = useState<any[]>([]);
  const [standings, setStandings] = useState<any[]>([]);

  const guessStreamUrl = `${WS_BASE_URL}/game/${gameId}/guess-stream`;
  const standingsStreamUrl = `${WS_BASE_URL}/game/${gameId}/standings`;

  useWebSocket(guessStreamUrl, (g) => setGuesses((prev) => [...prev, g]));
  useWebSocket(standingsStreamUrl, (s) => setStandings(s.topGuesses));

  return (
    <div className="flex flex-col items-center gap-4 p-6 min-h-screen bg-gradient-to-br from-yellow-100 to-pink-200">
      <h1 className="text-3xl font-bold mb-4">Game ID: {gameId}</h1>
      <StandingsPanel standings={standings} />
      <GuessStream guesses={guesses} />
    </div>
  );
}
