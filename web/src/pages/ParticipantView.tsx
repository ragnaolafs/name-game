import React from "react";
import { useParams } from "react-router-dom";
import { useState } from "react";
import { API_BASE_URL } from "@/config";
import { WS_BASE_URL } from "@/config";
import useLocalStorage from "@/hooks/useLocalStorage";
import useWebSocket from "@/hooks/useWebSocket";
import StandingsPanel from "@/components/StandingsPanel";
import GuessStream from "@/components/GuessStream";
import UsernameForm from "@/components/UsernameForm";
import GuessForm from "@/components/GuessForm";

const API_BASE = "/api";

export default function ParticipantView() {
  const { gameId } = useParams<{ gameId: string }>();

  if (!gameId) {
    throw new Error("Game ID is missing from the route.");
  }
  const [username, setUsername] = useLocalStorage("username", "");
  const [guess, setGuess] = useState("");
  const [guesses, setGuesses] = useState<any[]>([]);
  const [standings, setStandings] = useState<any[]>([]);

  const guessStreamUrl = `${WS_BASE_URL}/game/${gameId}/guess-stream`;
  const standingsStreamUrl = `${WS_BASE_URL}/game/${gameId}/standings`;

  useWebSocket(guessStreamUrl, (g) => setGuesses((prev) => [...prev, g]));
  useWebSocket(standingsStreamUrl, (s) => setStandings(s.topGuesses));

  async function submitGuess(gameId: string, user: string, guess: string) {
    const res = await fetch(`${API_BASE_URL}/game/${gameId}/guess`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ user, guess }),
    });

    if (!res.ok) {
      // handle error, show message
      console.error("Failed to submit guess", res.statusText);
      throw new Error("Failed to submit guess");
    }
  }

  return (
    <div className="flex flex-col items-center gap-4 p-6 min-h-screen bg-gradient-to-br from-green-100 to-blue-200">
      <h1 className="text-2xl font-bold">Join Game: {gameId}</h1>
      {!username ? (
        <UsernameForm onSubmit={setUsername} />
      ) : (
        <div className="w-full max-w-md space-y-4">
          <p className="text-lg">
            Hello, <strong>{username}</strong>
          </p>
          <GuessForm
            guess={guess}
            setGuess={setGuess}
            onSubmit={() => submitGuess(gameId, username, guess)}
          />
          <StandingsPanel standings={standings} />
          <GuessStream guesses={guesses} />
        </div>
      )}
    </div>
  );
}
