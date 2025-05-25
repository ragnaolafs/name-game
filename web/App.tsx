// App.tsx
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import GamePrompt from "./pages/GamePrompt";
import GameDisplay from "./pages/GameDisplay";
import ParticipantView from "./pages/ParticipantView";

export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<GamePrompt />} />
        <Route path="/display/:gameId" element={<GameDisplay />} />
        <Route path="/play/:gameId" element={<ParticipantView />} />
      </Routes>
    </Router>
  );
}
