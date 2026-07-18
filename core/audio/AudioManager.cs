using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    public readonly Dictionary<string, AudioStream> _sounds = new();

    private readonly int _parallelAudioPlayable = 16;

    private readonly List<AudioStreamPlayer2D> _sfxPlayers = new();

    private readonly AudioStreamPlayer2D _bgmPlayer = new AudioStreamPlayer2D();

    
    private readonly HashSet<string> validAudioFiles = [".wav", ".mp3"];
    public static void Initialise(SceneTree rootTree)
    {
        if(Instance != null) return;

        var node = new Node { Name="AudioManager" };
        Instance = new AudioManager();
        node.AddChild(Instance);

        rootTree.Root.CallDeferred(Node.MethodName.AddChild, node);
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
        using var rootDir = DirAccess.Open(audioFolderPath);
        if (rootDir == null)
        {
            throw new DirectoryNotFoundException($"Directory Not Found at {audioFolderPath}");
        }
        var subFolders = rootDir.GetDirectories();

        foreach (var subFolder in subFolders)
        {
            var subFolderPath = $"{audioFolderPath}/{subFolder}";
            using var subFolderDir = DirAccess.Open(subFolderPath);
            if (subFolderDir == null)
            {
                throw new DirectoryNotFoundException($"Directory Not Found at {subFolderPath}");
            }

            foreach (var audioFileName in subFolderDir.GetFiles().Where(file => validAudioFiles.Contains(Path.GetExtension(file.Replace(".import", "")))))
            {
                var cleanName = audioFileName.Replace(".import", "");
                var audioPath = $"{subFolderPath}/{cleanName}";
                string key = $"{subFolder}_{Path.GetFileNameWithoutExtension(cleanName)}";
                var audioStream = GD.Load<AudioStream>(audioPath);
                _sounds[key] = audioStream;
            }
        }
    }

    public AudioStreamRandomizer _BuildAudioStreamRandomiser(string directoryPath, float pitchScale)
    {
        using var dir = DirAccess.Open(directoryPath);
        if (dir == null)
        {
            throw new DirectoryNotFoundException($"Directory Not Found at {directoryPath}");
        }

        var audioPool = new AudioStreamRandomizer
        {
            RandomPitch = pitchScale
        };

        foreach (var audioFileName in dir.GetFiles().Where(file => validAudioFiles.Contains(Path.GetExtension(file.Replace(".import", "")))))
        {
            var cleanName = audioFileName.Replace(".import", "");
            var audioPath = $"{directoryPath}/{cleanName}";
            audioPool.AddStream(0, GD.Load<AudioStream>(audioPath));
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