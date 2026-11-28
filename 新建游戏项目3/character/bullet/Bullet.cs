using Godot;
using System;

public partial class Bullet : Node2D
{
	[Export] public Timer DelayTimer;
	[Export] public Area2D HitArea;
	[Export] public Node2D WorldRoot;
	[Export] public PackedScene AudioPrefab;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DelayTimer.Timeout += () =>{
			QueueFree();
		};
		HitArea.AreaEntered += (otherArea) =>	{
			PlayHitSound();
			QueueFree();
		};
	}

	// 命中音效挂到场景树根（Window）下，而不是挂在子弹/关卡节点下：
	// 这样子弹被 QueueFree、甚至整个游戏场景被释放后，声音仍会播完。
	void PlayHitSound()
	{
		if (AudioPrefab == null) return;

		var e = AudioPrefab.Instantiate() as AudioPlayer;
		if (e == null) return;

		GetTree().Root.AddChild(e);
		e.GlobalPosition = GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var p = GlobalPosition;
		p.Y-=10;
		GlobalPosition=p;
	}
	
}
