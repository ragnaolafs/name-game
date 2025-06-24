import React from "react";
import { useParams } from "react-router-dom";
import useLocalStorage from "@/hooks/useLocalStorage";
import StandingsPanel from "@/components/StandingsPanel";
import GuessStream from "@/components/GuessStream";
import UsernameForm from "@/components/UsernameForm";
import GuessForm from "@/components/GuessForm";
import { GameProvider } from "@/context/GameContext";
import GameStatus from "@/components/GameStatus";

export default function ParticipantView() {
  const { gameId } = useParams<{ gameId: string }>();

  if (!gameId) {
    throw new Error("Game ID is missing from the route.");
  }
  const [username, setUsername] = useLocalStorage("username", "");

  return (
    <GameProvider gameId={gameId}>
      <div className="flex flex-col items-center gap-4 p-2 min-h-screen bg-gradient-to-br from-green-100 to-blue-200 sm:p-6">
        <div className="w-full max-w-full sm:max-w-md space-y-4 px-0 sm:px-0">
          <GameStatus />
          {!username ? (
            <UsernameForm onSubmit={setUsername} />
          ) : (
            <div className="w-full space-y-4">
              <p className="text-lg text-center">
                Hello, <strong>{username}</strong>
              </p>
              <StandingsPanel />
              <GuessStream />
              <GuessForm gameId={gameId} username={username} />
            </div>
          )}
        </div>
      </div>
    </GameProvider>
  );
}
