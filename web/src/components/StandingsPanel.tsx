// components/StandingsPanel.tsx
import React from "react";
import getScoreColor from "./ScoreColor";
import { useGame } from "@/context/GameContext";

export default function StandingsPanel() {
  const {
    standings: { data, isLoading, error },
  } = useGame();

  const wrapperClass = "bg-white rounded-xl shadow-lg p-4 w-full max-w-xl";

  if (error) {
    console.error("Error loading standings:", error);
    return (
      <div className={wrapperClass}>
        <h2>Error loading standings</h2>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className={wrapperClass}>
        <h2>Loading...</h2>
      </div>
    );
  }

  return (
    <div className="bg-white rounded-xl shadow-lg p-4 w-full max-w-xl">
      <h2 className="text-xl font-semibold mb-2">Top Guesses</h2>
      <ul className="space-y-2">
        {data.topGuesses.map(({ id, user, guess, scorePercent }) => (
          <li
            key={id}
            className="flex justify-between items-center border-b border-gray-100 pb-1"
          >
            <div>
              <span className="font-medium">{user}</span>: {guess}
            </div>
            <div className={`font-bold ${getScoreColor(scorePercent)}`}>
              {scorePercent.toFixed(2)}%
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
