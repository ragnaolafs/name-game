// pages/GamePrompt.tsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";
import React from "react";

export default function GamePrompt() {
  const [id, setId] = useState("");
  const navigate = useNavigate();

  return (
    <div className="flex flex-col items-center justify-center min-h-screen gap-4 bg-gradient-to-br from-purple-100 to-orange-200">
      <h1 className="text-3xl font-bold">Enter Game ID</h1>
      <div className="flex gap-2">
        <Input
          value={id}
          onChange={(e) => setId(e.target.value)}
          placeholder="game-id"
        />
        <Button onClick={() => navigate(`/play/${id}`)}>Join</Button>
        <Button variant="secondary" onClick={() => navigate(`/display/${id}`)}>
          Display
        </Button>
      </div>
    </div>
  );
}
