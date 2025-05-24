const WebSocket = require("ws");

// Get the game ID from command-line arguments
const gameId = process.argv[2];

if (!gameId) {
  console.error("Please provide a game ID as the first argument.");
  process.exit(1);
}

const socket = new WebSocket(
  `ws://localhost:5056/api/ws/game/${gameId}/guess-stream`
);

socket.on("open", () => {
  console.log("Connected to the WebSocket server");
});

socket.on("message", (data) => {
  console.log("Submitted Guesses:", data.toString());
});

socket.on("error", (error) => {
  console.error("WebSocket error:", error);
});

socket.on("close", () => {
  console.log("WebSocket connection closed");
});
