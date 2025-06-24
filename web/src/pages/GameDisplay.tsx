import { useParams } from "react-router-dom";
import StandingsPanel from "@/components/StandingsPanel";
import GuessStream from "@/components/GuessStream";
import React from "react";
import { GameProvider } from "@/context/GameContext";
import GameStatus from "@/components/GameStatus";

export default function GameDisplay() {
  const { gameId } = useParams();

  return (
    <GameProvider gameId={gameId}>
      <div className="flex flex-col items-center gap-4 p-6 min-h-screen bg-gradient-to-br from-yellow-100 to-pink-200">
        <GameStatus displayQR={true} />
        <StandingsPanel />
        <GuessStream />
      </div>
    </GameProvider>
  );
}
