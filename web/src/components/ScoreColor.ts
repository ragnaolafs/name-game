// components/ScoreColor.ts
export default function getScoreColor(score: number): string {
    if (score >= 75) return "text-green-600";
    if (score >= 40) return "text-yellow-600";
    return "text-red-500";
}
