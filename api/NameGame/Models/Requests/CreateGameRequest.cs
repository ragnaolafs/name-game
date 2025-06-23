namespace NameGame.Models.Requests;

public record CreateGameRepuest(
    string Answer,
    bool EnableHints = false,
    bool StartNow = false);