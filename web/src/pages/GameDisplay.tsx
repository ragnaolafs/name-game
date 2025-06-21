import { useParams } from "react-router-dom";
import StandingsPanel from "@/components/StandingsPanel";
import GuessStream from "@/components/GuessStream";
import React from "react";
import { GameProvider } from "@/context/GameContext";

export default function GameDisplay() {
  const { gameId } = useParams();

  return (
    <GameProvider gameId={gameId}>
      <div className="flex flex-col items-center gap-4 p-6 min-h-screen bg-gradient-to-br from-yellow-100 to-pink-200">
        <h1 className="text-3xl font-bold mb-4">Game ID: {gameId}</h1>
        <StandingsPanel />
        <GuessStream />
      </div>
    </GameProvider>
  );
}
