import React from "react";

interface GameCodeInputProps {
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  className?: string;
}

export const GameCodeInput: React.FC<GameCodeInputProps> = ({
  value,
  onChange,
  placeholder = "----",
  className = "",
}) => {
  // Only allow 4 letters, uppercase
  const handleInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    let val = e.target.value.toUpperCase().replace(/[^A-Z]/g, "");
    if (val.length > 4) val = val.slice(0, 4);
    onChange(val);
  };

  return (
    <input
      type="text"
      inputMode="text"
      pattern="[A-Za-z]{4}"
      maxLength={4}
      value={value}
      onChange={handleInput}
      placeholder={placeholder}
      className={`tracking-widest text-center font-mono text-2xl p-2 border border-gray-300 rounded w-32 bg-white ${className}`}
      autoComplete="off"
      autoCorrect="off"
      spellCheck={false}
    />
  );
};
