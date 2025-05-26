// components/GuessStream.tsx
import React from "react";
import getScoreColor from "./ScoreColor";

type Props = {
  guesses: { id: string; user: string; guess: string; scorePercent: number }[];
};

export default function GuessStream({ guesses }: Props) {
  return (
    <div className="bg-white rounded-xl shadow p-4 w-full max-w-xl max-h-60 overflow-y-auto">
      <h2 className="text-xl font-semibold mb-2">Recent Guesses</h2>
      <ul className="space-y-1">
        {guesses
          .slice(-50)
          .reverse()
          .map(({ id, user, guess, scorePercent }) => (
            <li key={id} className="text-sm flex justify-between">
              <span>
                <strong>{user}</strong>: {guess}
              </span>
              <span className={`font-semibold ${getScoreColor(scorePercent)}`}>
                {scorePercent.toFixed(2)}%
              </span>
            </li>
          ))}
      </ul>
    </div>
  );
}
