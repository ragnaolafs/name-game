// components/StandingsPanel.tsx
import getScoreColor from "./ScoreColor";

type Props = {
  standings: { id: string; user: string; guess: string; score: number }[];
};

export default function StandingsPanel({ standings }: Props) {
  return (
    <div className="bg-white rounded-xl shadow-lg p-4 w-full max-w-xl">
      <h2 className="text-xl font-semibold mb-2">Top Guesses</h2>
      <ul className="space-y-2">
        {standings.map(({ id, user, guess, scorePercent }) => (
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
