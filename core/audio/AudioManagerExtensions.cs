using Godot;

public static class AudioManagerExtensions
{
    public static void PlayRandomLaserSound(this AudioManager audioManager)
    {
        if(!audioManager._sounds.ContainsKey(AudioCue.RandomLaser.GetCue()))
        {
            audioManager._sounds.Add(AudioCue.RandomLaser.GetCue(), audioManager._BuildAudioStreamRandomiser(Paths.laserAudioPath, 1f));
        }

        audioManager.PlaySfx(AudioCue.RandomLaser);
    }

    public static void PlayRandomExplosionSound(this AudioManager audioManager)
    {
        if(!audioManager._sounds.ContainsKey(AudioCue.RandomExplosion.GetCue()))
        {
            audioManager._sounds.Add(AudioCue.RandomExplosion.GetCue(), audioManager._BuildAudioStreamRandomiser(Paths.explosionAudioPath, 1f));
        }

        audioManager.PlaySfx(AudioCue.RandomExplosion);
    }
}