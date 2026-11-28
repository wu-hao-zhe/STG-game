using Godot;
using System;

public partial class AudioPlayer : Node2D
{
	[Export] AudioStreamPlayer _audio;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (_audio == null)
		{
			_audio = GetNodeOrNull<AudioStreamPlayer>("AudioStreamPlayer");
		}
		if (_audio == null)
		{
			GD.PushWarning("AudioPlayer: 找不到 AudioStreamPlayer 子节点，无法播放。");
			QueueFree();
			return;
		}

		// 播完自动释放：音效节点挂在场景树根上，不主动清理会一直累积。
		_audio.Finished += QueueFree;
		_audio.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
