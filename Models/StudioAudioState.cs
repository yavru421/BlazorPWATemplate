namespace BlazorPwaTemplate.Models;

public class StudioAudioState
{
    public bool IsPlaying { get; set; } = false;
    public int VibeIndex { get; set; } = 0; // 0 to 49
    public double Tempo { get; set; } = 140; 
    
    // Generative Knobs (0.0 to 1.0)
    public double KickComplexity { get; set; } = 0.5;
    public double SnareComplexity { get; set; } = 0.5;
    public double HatComplexity { get; set; } = 0.5;
    public double BassComplexity { get; set; } = 0.5;
    public double MelodyComplexity { get; set; } = 0.5;
}
