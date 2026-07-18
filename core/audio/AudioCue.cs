// ========================================== 
// AUTO-GENERATED AT BUILD TIME. DO NOT EDIT.
// ========================================== 
using System;
public enum AudioCue
{
    ExplosionSound1,
    ExplosionSound2,
    ExplosionSound3,
    LaserSound1,
    LaserSound2,
    LaserSound3,
    LaserSound4,
    MusicBgmFast,
    MusicBgmSlow,
    PlayerHit,
}
public static class AudioCueExtensions
{
    public static string GetCue(this AudioCue audioCue) => audioCue switch
    {
       AudioCue.ExplosionSound1 => "explosion_sound_1",
       AudioCue.ExplosionSound2 => "explosion_sound_2",
       AudioCue.ExplosionSound3 => "explosion_sound_3",
       AudioCue.LaserSound1 => "laser_sound_1",
       AudioCue.LaserSound2 => "laser_sound_2",
       AudioCue.LaserSound3 => "laser_sound_3",
       AudioCue.LaserSound4 => "laser_sound_4",
       AudioCue.MusicBgmFast => "music_bgm_fast",
       AudioCue.MusicBgmSlow => "music_bgm_slow",
       AudioCue.PlayerHit => "player_hit",
        _ => throw new ArgumentOutOfRangeException(nameof(audioCue))
    };
}
