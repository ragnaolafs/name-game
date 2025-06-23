import React from "react";
import { useParams } from "react-router-dom";
import { useState, useEffect, useRef } from "react";
import { API_URL } from "@/config";
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
  const [guess, setGuess] = useState("");
  const namesListRef = useRef<string[]>([]);

  useEffect(() => {
    async function loadNames() {
      try {
        const res = await fetch("/names.txt");
        if (!res.ok) throw new Error("Failed to load names.txt");
        const text = await res.text();
        namesListRef.current = text.split(/\r?\n/).filter(Boolean);
      } catch (e) {
        console.error(e);
      }
    }
    loadNames();
  }, []);

  async function submitGuess(gameId: string, user: string, guess: string) {
    const res = await fetch(`${API_URL}/game/${gameId}/guess`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ user, guess }),
    });

    if (!res.ok) {
      // handle error, show message
      console.error("Failed to submit guess", res.statusText);
    }
  }

  async function randomizeGuess() {
    const names = namesListRef.current;
    if (names.length < 2) return;
    const shuffled = names.slice().sort(() => 0.5 - Math.random());
    setGuess(`${shuffled[0]} ${shuffled[1]}`);
  }

  // todo clear input on submit

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
            <GuessForm
              guess={guess}
              setGuess={setGuess}
              onSubmit={() => submitGuess(gameId, username, guess)}
            />
            <button
              type="button"
              className="mt-2 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
              onClick={randomizeGuess}
            >
              Random
            </button>
            <StandingsPanel />
            <GuessStream />
          </div>
        )}
      </div>
    </GameProvider>
  );
}
