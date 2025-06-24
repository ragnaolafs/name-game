import { useGameStatus } from './useGameStatus';
import { useGameStandings } from './useGameStandings';
import { useGuessStream } from './useGuessStream';

export function useGameData(gameId: string) {
    const {
        status, isLoading: loadingStatus, error: errorStatus
    } = useGameStatus(gameId);

    const {
        standings, isLoading: loadingStandings, error: errorStandings
    } = useGameStandings(gameId);

    const {
        guesses, isLoading: loadingGuesses, error: errorGuesses
    } = useGuessStream(gameId);

    return {
        gameId,
        status: {
            data: status,
            isLoading: loadingStatus,
            error: errorStatus
        },
        standings: {
            data: standings,
            isLoading: loadingStandings,
            error: errorStandings
        },
        guesses: {
            data: guesses,
            isLoading: loadingGuesses,
            error: errorGuesses
        }
    };
}
