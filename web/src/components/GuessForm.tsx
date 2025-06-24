// components/GuessForm.tsx
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import React, { useRef, useEffect, useState } from "react";
import { API_URL } from "@/config";
import { useGame } from "@/context/GameContext";

// Props now include gameId and username
interface GuessFormProps {
  gameId: string;
  username: string;
  onGuessSubmitted?: () => void; // Optional callback
}

export default function GuessForm({
  gameId,
  username,
  onGuessSubmitted,
}: GuessFormProps) {
  const [guess, setGuess] = useState("");
  const [namesList, setNamesList] = useState<string[]>([]);
  const inputRef = useRef<HTMLInputElement>(null);

  const { status } = useGame();
  const { data, isLoading } = status;

  // Load names.txt once
  useEffect(() => {
    async function loadNames() {
      try {
        const res = await fetch("/names.txt");
        if (!res.ok) throw new Error("Failed to load names.txt");
        const text = await res.text();
        setNamesList(text.split(/\r?\n/).filter(Boolean));
      } catch (e) {
        console.error(e);
      }
    }
    loadNames();
  }, []);

  // Focus input when guess is cleared
  useEffect(() => {
    if (guess === "" && inputRef.current) {
      inputRef.current.focus();
    }
  }, [guess]);

  async function submitGuess() {
    if (!guess.trim()) return;
    const res = await fetch(`${API_URL}/game/${gameId}/guess`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ user: username, guess }),
    });
    if (!res.ok) {
      // handle error, show message
      console.error("Failed to submit guess", res.statusText);
    } else {
      setGuess("");
      if (onGuessSubmitted) onGuessSubmitted();
    }
  }

  function randomizeGuess() {
    if (namesList.length < 2) return;
    const shuffled = namesList.slice().sort(() => 0.5 - Math.random());
    setGuess(`${shuffled[0]} ${shuffled[1]}`);
  }

  const disabled = isLoading || !data || data.status !== "Active";

  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
        submitGuess();
      }}
      className="flex gap-2"
    >
      <Input
        inputRef={inputRef}
        value={guess}
        onChange={(e) => setGuess(e.target.value)}
        placeholder="Your guess"
        className="flex-1"
        disabled={disabled}
      />
      <Button
        type="button"
        className="bg-blue-500 text-white hover:bg-blue-600"
        onClick={randomizeGuess}
        disabled={disabled}
      >
        Random
      </Button>
      <Button type="submit" disabled={disabled}>
        Guess
      </Button>
    </form>
  );
}
