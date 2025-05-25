// components/GuessStream.tsx
import getScoreColor from "./ScoreColor";

type Props = {
  guesses: { id: string; user: string; guess: string; score: number }[];
};

export default function GuessStream({ guesses }: Props) {
  console.log("Rendering GuessStream with guesses:", guesses);
  return (
    <div className="bg-white rounded-xl shadow p-4 w-full max-w-xl max-h-60 overflow-y-auto">
      <h2 className="text-xl font-semibold mb-2">Recent Guesses</h2>
      <ul className="space-y-1">
        {guesses
          .slice(-50)
          .reverse()
          .map(({ id, user, guess, score }) => (
            <li key={id} className="text-sm flex justify-between">
              <span>
                <strong>{user}</strong>: {guess}
              </span>
              <span className={`font-semibold ${getScoreColor(score)}`}>
                {score}
              </span>
            </li>
          ))}
      </ul>
    </div>
  );
}
