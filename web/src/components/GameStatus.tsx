import { useGame } from "@/context/GameContext";
import React from "react";
import Confetti from "react-confetti";
import { useWindowSize } from "react-use";
import { QRCode } from "./QRCode";

const statusLabels: Record<string, string> = {
  Setup: "Waiting for game to start...",
  Active: "Game in progress!",
  Finished: "Game finished!",
};

interface GameStatusProps {
  displayQR?: boolean;
}

export default function GameStatus({ displayQR = false }: GameStatusProps) {
  const { status, gameId } = useGame();
  const { data, isLoading, error } = status;

  if (isLoading) return <div className="text-gray-500">Loading status...</div>;
  if (error) return <div className="text-red-500">Error loading status</div>;
  if (!status) return <div className="text-gray-500">No status available</div>;

  const { width, height } = useWindowSize();

  // Handle Finished state with winner and answer
  if (data.status === "Finished" && data.winner) {
    return (
      <div className="relative flex flex-col items-center justify-center py-12">
        <Confetti
          numberOfPieces={400}
          recycle={true}
          width={width}
          height={height}
          style={{
            position: "fixed",
            top: 0,
            left: 0,
            pointerEvents: "none",
            zIndex: 50,
          }}
        />
        <div className="text-5xl font-extrabold text-green-600 drop-shadow mb-4 animate-bounce">
          🎉 Winner: {data.winner.winner} 🎉
        </div>
        <div className="text-3xl font-bold text-blue-500 mb-2 animate-pulse">
          Correct Answer:{" "}
          <span className="underline">{data.winner.answer}</span>
        </div>
        <div className="text-xl text-gray-700 mt-4">Game finished!</div>
        {displayQR && gameId && (
          <div className="mt-6 flex flex-col items-center">
            <div className="mb-2 text-lg font-medium">Join this game:</div>
            <QRCode
              value={`${window.location.origin}/play/${gameId}`}
              size={160}
            />
            <div className="mt-2 text-xs text-gray-500">
              Scan to join: /play/{gameId}
            </div>
          </div>
        )}
      </div>
    );
  }

  return (
    <div className="text-lg font-semibold flex flex-col items-center">
      {statusLabels[data.status] || `Status: ${data.status}`}
      {displayQR && gameId && (
        <div className="mt-4 flex flex-col items-center">
          <div className="mb-2 text-base font-medium">Join this game:</div>
          <QRCode
            value={`${window.location.origin}/play/${gameId}`}
            size={120}
          />
          <div className="mt-2 text-xs text-gray-500">
            Scan to join: /play/{gameId}
          </div>
        </div>
      )}
    </div>
  );
}
