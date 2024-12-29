const WebSocket = require("ws");

const socket = new WebSocket("ws://localhost:5056/ws/game/guesses");

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
