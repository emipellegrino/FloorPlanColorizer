using Spectre.Console;

namespace FloorPlanColorizer.Services
{
    public class SpectreConsoleRenderer : Interfaces.IRenderer
    {
        private readonly string[] _colors = { "red", "green", "blue", "yellow", "cyan", "magenta", "white" };

        public void Render(char[,] coloredPlan)
        {
            int rows = coloredPlan.GetLength(0);
            int cols = coloredPlan.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (coloredPlan[i, j] == '#' || coloredPlan[i, j] == ' ')
                    {
                        AnsiConsole.Markup($"[white]{coloredPlan[i, j]}[/]");
                    }
                    else
                    {
                        int colorIndex = (coloredPlan[i, j] - 'A') % _colors.Length;
                        AnsiConsole.Markup($"[{_colors[colorIndex]}]{coloredPlan[i, j]}[/]");
                    }
                }
                AnsiConsole.WriteLine();
            }
        }
    }
}