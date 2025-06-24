// pages/GamePrompt.tsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/Button";
import { GameCodeInput } from "@/components/ui/GameCodeInput";
import React from "react";
import { API_URL } from "@/config";

export default function GamePrompt() {
  const [code, setCode] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async () => {
    if (!code.trim()) {
      return;
    }

    try {
      const response = await fetch(`${API_URL}/game/join/${code}`, {
        method: "GET",
      });

      if (!response.ok) {
        console.error(response);
        throw new Error("Failed to join game");
      }

      const data = await response.json();
      const gameId = data.id;

      navigate(`/play/${gameId}`);
    } catch (error) {
      console.error("Error joining game:", error);
    }
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen gap-4 bg-gradient-to-br from-purple-100 to-orange-200">
      <h1 className="text-3xl font-bold">Enter Game Code</h1>
      <div className="flex gap-2">
        <GameCodeInput value={code} onChange={setCode} />
        <Button onClick={() => handleSubmit()}>Join</Button>
      </div>
    </div>
  );
}
