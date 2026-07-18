using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public partial class AudioManager : Node
{
    public static AudioManager Instance
    {
        get;
        private set;
    }

    private readonly Dictionary<string, AudioStream> _sounds = new();

    private readonly int _parallelAudioPlayable = 16;

    private readonly List<AudioStreamPlayer2D> _sfxPlayers = new();

    private readonly AudioStreamPlayer2D _bgm = new AudioStreamPlayer2D();

    public override void _Ready()
    {
        if(Instance != null)
        {
            QueueFree();
            return;
        }

        Instance = this;

        ProcessMode = ProcessModeEnum.Always;

        // init sound players
        for (int i = 0; i < _parallelAudioPlayable; i++)
        {
            var player = new AudioStreamPlayer2D();
            AddChild(player);
            _sfxPlayers.Add(player);
        }
        AddChild(_bgm);

        ScanAndAddAudio(Paths.audioPath);
    }

    private void ScanAndAddAudio(string audioFolderPath)
    {
        HashSet<string> validAudioFiles = [".wav", ".mp3"];

        if (!Directory.Exists(audioFolderPath))
        {
            throw new DirectoryNotFoundException($"Directory Not Found at {audioFolderPath}");
        }
        var subFolders = Directory.GetDirectories(audioFolderPath);

        foreach (var folder in subFolders)
        {
            if(!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"Directory Not Found at {folder}");
            }

            foreach (var audioFile in Directory.GetFiles(folder).Where(file => validAudioFiles.Contains(Path.GetExtension(file))))
            {
                string key = $"{Path.GetFileName(folder)}_{Path.GetFileNameWithoutExtension(audioFile)}";
                var audioStream = GD.Load<AudioStream>(audioFile);
                
                _sounds.Add(key, audioStream);
            }
        }
    }

    public void PlaySfx(AudioCue cueName)
    {
        var freePlayer = _sfxPlayers.Find(player => !player.Playing);
        freePlayer.Stream = _sounds[cueName.GetCue()];
        freePlayer.Play();
    }
}