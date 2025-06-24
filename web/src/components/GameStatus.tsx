import { useGame } from "@/context/GameContext";
import React from "react";
import Confetti from "react-confetti";

const statusLabels: Record<string, string> = {
  Setup: "Waiting for game to start...",
  Active: "Game in progress!",
  Finished: "Game finished!",
};

export default function GameStatus() {
  const { status } = useGame();
  const { data, isLoading, error } = status;

  if (isLoading) return <div className="text-gray-500">Loading status...</div>;
  if (error) return <div className="text-red-500">Error loading status</div>;
  if (!status) return <div className="text-gray-500">No status available</div>;

  // Handle Finished state with winner and answer
  if (data.status === "Finished" && data.winner) {
    return (
      <div className="relative flex flex-col items-center justify-center py-12">
        <Confetti numberOfPieces={400} recycle={false} />
        <div className="text-5xl font-extrabold text-green-600 drop-shadow mb-4 animate-bounce">
          🎉 Winner: {data.winner.winner} 🎉
        </div>
        <div className="text-3xl font-bold text-blue-500 mb-2 animate-pulse">
          Correct Answer:{" "}
          <span className="underline">{data.winner.answer}</span>
        </div>
        <div className="text-xl text-gray-700 mt-4">Game finished!</div>
      </div>
    );
  }

  return (
    <div className="text-lg font-semibold">
      {statusLabels[data.status] || `Status: ${data.status}`}
    </div>
  );
}
