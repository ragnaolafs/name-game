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
      <div className="flex flex-col items-center justify-center gap-12 p-6 min-h-screen bg-gradient-to-br from-yellow-100 to-pink-200 w-full max-w-none">
        <div className="w-full flex justify-center mb-8">
          <div className="w-full max-w-2xl">
            <GameStatus displayQR={true} />
          </div>
        </div>
        <div className="flex flex-col md:flex-row items-center justify-center gap-12 w-full">
          <div className="flex flex-col gap-12 md:w-1/2 w-full max-w-xl">
            <StandingsPanel />
          </div>
          <div className="md:w-1/2 w-full flex max-w-xl">
            <GuessStream />
          </div>
        </div>
      </div>
    </GameProvider>
  );
}
