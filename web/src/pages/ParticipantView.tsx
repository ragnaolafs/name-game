import React from "react";
import { useParams } from "react-router-dom";
import useLocalStorage from "@/hooks/useLocalStorage";
import StandingsPanel from "@/components/StandingsPanel";
import GuessStream from "@/components/GuessStream";
import UsernameForm from "@/components/UsernameForm";
import GuessForm from "@/components/GuessForm";
import { GameProvider } from "@/context/GameContext";

export default function ParticipantView() {
  const { gameId } = useParams<{ gameId: string }>();

  if (!gameId) {
    throw new Error("Game ID is missing from the route.");
  }
  const [username, setUsername] = useLocalStorage("username", "");

  return (
    <GameProvider gameId={gameId}>
      <div className="flex flex-col items-center gap-4 p-6 min-h-screen bg-gradient-to-br from-green-100 to-blue-200">
        <h1 className="text-2xl font-bold">Join Game: {gameId}</h1>
        {!username ? (
          <UsernameForm onSubmit={setUsername} />
        ) : (
          <div className="w-full max-w-md space-y-4">
            <p className="text-lg">
              Hello, <strong>{username}</strong>
            </p>
            <GuessForm gameId={gameId} username={username} />
            <StandingsPanel />
            <GuessStream />
          </div>
        )}
      </div>
    </GameProvider>
  );
}
