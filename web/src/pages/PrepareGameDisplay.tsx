import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "@/config";

export default function PrepareGameDisplay() {
  const [gameCode, setGameCode] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await fetch(`${API_BASE_URL}/game/join/${gameCode}`, {
        method: "GET",
      });

      if (!response.ok) {
        console.error(response);
        throw new Error("Failed to join game");
      }

      const data = await response.json();
      const gameId = data.id;

      navigate(`/display/${gameId}`);
    } catch (error) {
      console.error("Error joining game:", error);
    }
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gradient-to-br from-yellow-100 to-pink-200">
      <h1 className="text-3xl font-bold mb-4">Enter game code</h1>
      <form
        className="flex flex-col items-center gap-4"
        onSubmit={handleSubmit}
      >
        <input
          type="text"
          placeholder="Game ID"
          value={gameCode}
          onChange={(e) => setGameCode(e.target.value)}
          className="p-2 border border-gray-300 rounded w-64"
        />
        <button
          type="submit"
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        >
          Join Game
        </button>
      </form>
    </div>
  );
}
