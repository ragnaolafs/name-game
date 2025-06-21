import React, { createContext, useContext, ReactNode } from "react";
import { useGameData } from "../hooks/useGameData";

type GameContextType = ReturnType<typeof useGameData>;

const GameContext = createContext<GameContextType | null>(null);

export const GameProvider = ({
  gameId,
  children,
}: {
  gameId: string;
  children: ReactNode;
}) => {
  const gameData = useGameData(gameId);

  return (
    <GameContext.Provider value={gameData}>{children}</GameContext.Provider>
  );
};

export function useGame() {
  const context = useContext(GameContext);
  if (!context) {
    throw new Error("useGame must be used inside a <GameProvider>");
  }
  return context;
}
