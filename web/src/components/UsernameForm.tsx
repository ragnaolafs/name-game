// components/UsernameForm.tsx
import { useState } from "react";
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";

type Props = {
  onSubmit: (name: string) => void;
};

export default function UsernameForm({ onSubmit }: Props) {
  const [name, setName] = useState("");

  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
        if (name) onSubmit(name);
      }}
      className="space-y-4"
    >
      <h2 className="text-lg font-semibold">Enter your name</h2>
      <Input
        value={name}
        onChange={(e) => setName(e.target.value)}
        placeholder="Your name"
      />
      <Button type="submit">Continue</Button>
    </form>
  );
}
