// components/GuessForm.tsx
import { Input } from "@/components/ui/Input";
import { Button } from "@/components/ui/Button";

type Props = {
  guess: string;
  setGuess: (g: string) => void;
  onSubmit: () => void;
};

export default function GuessForm({ guess, setGuess, onSubmit }: Props) {
  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
        onSubmit();
      }}
      className="flex gap-2"
    >
      <Input
        value={guess}
        onChange={(e) => setGuess(e.target.value)}
        placeholder="Your guess"
        className="flex-1"
      />
      <Button type="submit">Guess</Button>
    </form>
  );
}
