using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public partial class AudioManager : Node
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    public readonly Dictionary<string, AudioStream> _sounds = new();

    private readonly int _parallelAudioPlayable = 16;

    private readonly List<AudioStreamPlayer2D> _sfxPlayers = new();

    private readonly AudioStreamPlayer2D _bgmPlayer = new AudioStreamPlayer2D();

    
    private readonly HashSet<string> validAudioFiles = [".wav", ".mp3"];
    public static void Initialise(SceneTree rootTree)
    {
        if(_instance != null) return;

        var node = new Node { Name="AudioManager" };
        _instance = new AudioManager();
        node.AddChild(_instance);

        rootTree.Root.CallDeferred(Window.MethodName.AddChild, node);
    }

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;

        // init sound players
        for (int i = 0; i < _parallelAudioPlayable; i++)
        {
            var player = new AudioStreamPlayer2D();
            AddChild(player);
            _sfxPlayers.Add(player);
        }
        AddChild(_bgmPlayer);

        ScanAndAddAudio(Paths.audioPath);
    }

    private void ScanAndAddAudio(string audioFolderPath)
    {
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

    public AudioStreamRandomizer _BuildAudioStreamRandomiser(string directoryPath, float pitchScale)
    {
        if(!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException($"Directory Not Found at {directoryPath}");
        }

        var audioPool = new AudioStreamRandomizer
        {
            RandomPitch = pitchScale
        };

        foreach (var audioFile in Directory.GetFiles(directoryPath).Where(file => validAudioFiles.Contains(Path.GetExtension(file))))
        {
            audioPool.AddStream(0, GD.Load<AudioStream>(audioFile));
        }

        return audioPool;
    }

    public void PlaySfx(AudioCue cueName, float volume = -6.0f)
    {
        var freePlayer = _sfxPlayers.Find(player => !player.Playing);
        freePlayer.Stream = _sounds[cueName.GetCue()];
        freePlayer.VolumeDb = volume;
        freePlayer.Play();
    }

    public void PlayBGM(AudioCue cueName, float volume = -6.0f)
    {
        if(_bgmPlayer.Playing)
        {
            _bgmPlayer.Stop();
        }
        _bgmPlayer.Stream = _sounds[cueName.GetCue()];
        _bgmPlayer.VolumeDb = volume;
        _bgmPlayer.Play();
    }

    public void StopBGM(AudioCue cueName)
    {
        if(_bgmPlayer.Playing)
        {
            _bgmPlayer.Stop();
        }
    }
}